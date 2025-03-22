using Devnometro.Aplicacao;
using Devnometro.Dominio;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Windows;

namespace Devnometro.TelasConfig.Cadastros;

public class ExpressoBase : ComponentBase
{
    [Parameter] public required Preferencias Preferencias { get; set; }

    private readonly ManipuladorDeArquivo manipulador = new();

    protected override void OnInitialized()
    {
        Model = manipulador.CarregarExpressos();
        ModelPadrao = ExpressoModel.ListaMocada();
        Contadores = manipulador.CarregarContadores();
    }
    private async Task SalvaAtualiza()
    {
        await manipulador.SalvarExpressosAsync(Model);
        StateHasChanged();
    }
    protected List<ExpressoModel> Model { get; set; } = [];
    protected List<ExpressoModel> ModelPadrao { get; set; } = [];
    protected ExpressoModel itemSelecionado = new();
    protected ExpressoModel? itemSelecionadoPadrao;
    protected List<ContadorModel> Contadores { get; set; } = [];

    #region Consulta
    protected string textoConsulta = "";
    protected Func<ExpressoModel, bool> Filtrar => x =>
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
        await manipulador.ExportarExpressosAsync(Model);
        exportando = false;
    }
    protected bool exportando = false;
    protected async Task ImportarCadastro()
    {
        if (importando) return;
        importando = true;
        var lista = await manipulador.ImportarExpressosAsync();
        if (lista != null)
        {
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
        var retorno = await MetodosEstaticos.MensagemAsync("Esse processo apagará todas as personalizações e restaurará a lista de cronômetros expressos que vem pré-instalada no Devnômetro.\nÉ isso mesmo que quer fazer?", "Tem certeza?", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (retorno != MessageBoxResult.Yes)
        {
            restaurando = false;
            return;
        }
        await Task.Delay(1000);
        if (ManipuladorDeArquivo.LimparExpressos())
        {
            Model = ExpressoModel.ListaMocada();
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
    protected bool DrawerAberto { get; set; }
    protected void CancelarDrawer() => DrawerAberto = false;
    protected async Task SalvarDrawer()
    {
        if (!Model.Any(x => x.Id == itemSelecionado.Id))
        {
            var contadorZerado = new ExpressoModel(true);
            if (contadorZerado.Nome != itemSelecionado.Nome
             || contadorZerado.Contador.Id != itemSelecionado.Contador.Id)
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
        var cont = new ContadorModel();
        cont.Update(Contadores.First(x => x.Id == itemSelecionadoPadrao.Contador.Id));
        itemSelecionado.Contador = cont;
    }
    protected bool EstaNoPadrao
    {
        get
        {
            if (itemSelecionadoPadrao == null) return true;

            return itemSelecionado.Nome == itemSelecionadoPadrao.Nome
                && itemSelecionado.Contador.Id == itemSelecionadoPadrao.Contador.Id;
        }
    }
    #endregion

    #region CRUD
    protected void CriarExpresso()
    {
        itemSelecionado = new ExpressoModel(true);
        DrawerAberto = true;
    }
    protected void EditarExpresso(ExpressoModel expresso)
    {
        if (expresso == null) return;

        itemSelecionado = new();
        itemSelecionado.Update(expresso);

        if (itemSelecionado.Padrao)
        {
            var padrao = ModelPadrao.First(x => x.Id == itemSelecionado.Id);
            itemSelecionadoPadrao = new();
            itemSelecionadoPadrao.Update(padrao);
        }
        
        DrawerAberto = true;
    }
    protected async Task ExcluirExpresso(ExpressoModel expresso)
    {
        if (expresso.ConfirmacaoPendente) return;
        expresso.ConfirmacaoPendente = true;

        var resposta = await MetodosEstaticos.MensagemAsync($"Excluir [{expresso.Nome}]?\nEssa ação não pode ser desfeita.",
                                                            "Confirmar exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
        expresso.ConfirmacaoPendente = false;
        if (resposta != MessageBoxResult.Yes) return;
        Model.Remove(expresso);
        await SalvaAtualiza();
    }
    #endregion
}
