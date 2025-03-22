using Devnometro.Aplicacao;
using Devnometro.Dominio;
using Devnometro.Dominio.Enumeradores;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Windows;

namespace Devnometro.TelasConfig.Cadastros;

public class ContadorBase : ComponentBase
{
    [Parameter] public required Preferencias Preferencias { get; set; }
    [Inject] IDialogService DialogService { get; set; } = null!;

    private readonly ManipuladorDeArquivo manipulador = new();

    protected override void OnInitialized()
    {
        Model = manipulador.CarregarContadores();
        ModelPadrao = ContadorModel.ListaMocada();
    }

    private async Task SalvaAtualiza()
    {
        await manipulador.SalvarContadoresAsync(Model);
        StateHasChanged();
    }

    protected List<ContadorModel> Model { get; set; } = [];
    protected List<ContadorModel> ModelPadrao { get; set; } = [];
    protected ContadorModel itemSelecionado = new();
    protected ContadorModel? itemSelecionadoPadrao;
    
    #region Consulta
    protected string textoConsulta = "";
    protected Func<ContadorModel, bool> Filtrar => x =>
    {
        if (string.IsNullOrWhiteSpace(textoConsulta))
            return true;

        if (x.Nome.Contains(textoConsulta, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
    protected async Task ExportarCadastro()
    {
        if (exportando) return;
        exportando = true;
        await manipulador.ExportarContadoresAsync(Model);
        exportando = false;
    }
    protected bool exportando = false;
    protected async Task ImportarCadastro()
    {
        if (importando) return;
        importando = true;
        var lista = await manipulador.ImportarContadoresAsync();
        if (lista != null)
        {
            //TODO: Modal mostrando a lista que será importada, com a opção de confirmar ou cancelar
            Model = lista;
            await SalvaAtualiza();
        }
        importando = false;
    }
    protected bool importando = false;
    protected async Task RestaurarCadastro()
    {
        if (restaurando) return;
        restaurando = true;
        var retorno = await MetodosEstaticos.MensagemAsync("Esse processo apagará todas as personalizações e restaurará a lista de contadores que vem pré-instalada no Devnômetro.\nÉ isso mesmo que quer fazer?", "Tem certeza?", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (retorno != MessageBoxResult.Yes)
        {
            restaurando = false;
            return;
        }
        await Task.Delay(1000);
        if (ManipuladorDeArquivo.LimparContadores())
        {
            Model = ContadorModel.ListaMocada();
            await SalvaAtualiza();
            StateHasChanged();
        }
        else
            await MetodosEstaticos.MensagemAsync("Ocorreu algum problema ao restaurar os padrões", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        restaurando = false;
    }
    protected bool restaurando = false;
    #endregion

    #region Drawer
    protected bool DrawerAberto { get; set; } = false;
    protected static string DescricaoContaTempo(bool contaTempo) => string.Concat("Tempo gasto conta como ", contaTempo ? "em atividade" : "interrupção de trabalho.");
    protected void CancelarDrawer() => DrawerAberto = false;
    protected async Task SalvarDrawer()
    {
        if (!Model.Any(x => x.Id == itemSelecionado.Id))
        {
            var contadorZerado = new ContadorModel(true);
            if (!itemSelecionado.EhMocado())
                Model.Add(itemSelecionado);
        }
        else
        {
            var alterado = Model.First(x => x.Id == itemSelecionado.Id);
            var index = Model.IndexOf(alterado);
            Model[index] = itemSelecionado;
            itemSelecionado = new();
        }
        await SalvaAtualiza();
        DrawerAberto = false;
    }
    protected void RestaurarDrawer()
    {
        if (itemSelecionadoPadrao == null) return;

        itemSelecionado.Nome = itemSelecionadoPadrao.Nome;
        itemSelecionado.Descricao = itemSelecionadoPadrao.Descricao;
        itemSelecionado.Icone = itemSelecionadoPadrao.Icone;
        itemSelecionado.IconeCor = itemSelecionadoPadrao.IconeCor;
        itemSelecionado.ContaTempo = itemSelecionadoPadrao.ContaTempo;
    }
    protected bool EstaNoPadrao 
    {   get
        {
            if (itemSelecionadoPadrao == null) return true;

            return itemSelecionado.Nome == itemSelecionadoPadrao.Nome
                && itemSelecionado.Descricao == itemSelecionadoPadrao.Descricao
                && itemSelecionado.Icone == itemSelecionadoPadrao.Icone
                && itemSelecionado.IconeCor == itemSelecionadoPadrao.IconeCor
                && itemSelecionado.ContaTempo == itemSelecionadoPadrao.ContaTempo;
        }
    }
    protected async Task SelcionarIcone(string nome, Color cor, Color? corPadrao, EIcone icone, EIcone? iconePadrao)
    {
        var dadosEnvio = new IconeDados(cor, icone, nome, corPadrao, iconePadrao);
        var dialogo = await DialogService.ShowAsync<SelecaoIcone>("",
                            new DialogParameters { { "Dados", dadosEnvio } },
                            new DialogOptions { CloseOnEscapeKey = true });
        var resultado = await dialogo.Result;

        if (resultado == null) return;
        if (!resultado.Canceled)
        {
            var dadosRetorno = resultado.Data as IconeDados;
            dadosRetorno ??= new();

            itemSelecionado.IconeCor = dadosRetorno.Cor;
            itemSelecionado.Icone = dadosRetorno.Icone;
        }
    }
    #endregion

    #region CRUD
    protected void CriarContador()
    {
        itemSelecionado = new ContadorModel(true);
        DrawerAberto = true;
    }
    protected void EditarContador(ContadorModel contador)
    {
        if (contador == null) return;

        itemSelecionado = new();
        itemSelecionado.Update(contador);

        if (itemSelecionado.Padrao)
        {
            var padrao = ModelPadrao.First(x => x.Id == itemSelecionado.Id);
            itemSelecionadoPadrao = new();
            itemSelecionadoPadrao.Update(padrao);
        }

        DrawerAberto = true;
    }
    protected async Task ExcluirContador(ContadorModel contador)
    {
        if (contador.ConfirmacaoPendente) return;
        contador.ConfirmacaoPendente = true;

        var resposta = await MetodosEstaticos.MensagemAsync($"Excluir [{contador.Nome}]?\nEssa ação não pode ser desfeita.\n\nExcluir um contador não removerá ele de padrões e expressos já criados, nem impedirá tempo já registrado nele de aparecer em relatórios.",
                                                            "Confirmar exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
        contador.ConfirmacaoPendente = false;
        if (resposta != MessageBoxResult.Yes) return;
        Model.Remove(contador);
        await SalvaAtualiza();
    }
    #endregion
}
