using Devnometro.Aplicacao;
using Devnometro.Dominio;
using Devnometro.Dominio.Enumeradores;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Windows;

namespace Devnometro.TelasConfig.Cadastros;

public class PadraoBase : ComponentBase
{
    [Parameter] public required Preferencias Preferencias { get; set; }
    [Inject] IDialogService DialogService { get; set; } = null!;

    private readonly ManipuladorDeArquivo manipulador = new();

    protected override void OnInitialized()
    {
        Model = manipulador.CarregarPadroesCronometros();
        ModelPadrao = PadraoDeCronometroModel.ListaMocada();
        Contadores = manipulador.CarregarContadores();
    }

    private async Task SalvaAtualiza()
    {
        await manipulador.SalvarPadroesCronometrosAsync(Model);
        StateHasChanged();
    }

    protected List<PadraoDeCronometroModel> Model { get; set; } = [];
    protected List<PadraoDeCronometroModel> ModelPadrao { get; set; } = [];
    protected PadraoDeCronometroModel itemSelecionado = new();
    protected PadraoDeCronometroModel? itemSelecionadoPadrao;
    protected ContadorModel contadorSelecionado = new();
    protected ContadorModel? contadorSelecionadoPadrao;
    protected List<ContadorModel> Contadores { get; set; } = [];

    #region Consulta
    protected string textoConsulta = "";
    protected Func<PadraoDeCronometroModel, bool> Filtrar => x =>
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
        await manipulador.ExportarPadroesCronometrosAsync(Model);
        exportando = false;
    }
    protected bool exportando = false;
    protected async Task ImportarCadastro()
    {
        if (importando) return;
        importando = true;
        var lista = await manipulador.ImportarPadroesCronometrosAsync();
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
        var retorno = await MetodosEstaticos.MensagemAsync("Esse processo apagará todas as personalizações e restaurará a lista de padrões de cronômetros que vem pré-instalada no Devnômetro.\nÉ isso mesmo que quer fazer?", "Tem certeza?", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (retorno != MessageBoxResult.Yes)
        {
            restaurando = false;
            return;
        }
        await Task.Delay(1000);
        if (ManipuladorDeArquivo.LimparPadroesCronometros())
        {
            Model = PadraoDeCronometroModel.ListaMocada();
            await SalvaAtualiza();
            StateHasChanged();
        }
        else
            await MetodosEstaticos.MensagemAsync("Ocorreu algum problema ao restaurar os padrões", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        restaurando = false;
    }
    protected bool restaurando = false;
    #endregion

    #region Drawer Padrão
    protected bool DrawerPadraoAberto { get; set; } = false;
    protected void CancelarDrawerPadrao() => DrawerPadraoAberto = false;
    protected async Task SalvarDrawerPadrao()
    {
        if (!Model.Any(x => x.Id == itemSelecionado.Id))
        {
            var padraoZerado = new PadraoDeCronometroModel(true);
            if (padraoZerado.Nome != itemSelecionado.Nome
             || (padraoZerado.Contadores?.Count ?? 0) > 0)
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
        DrawerPadraoAberto = false;
    }
    protected void RestaurarDrawerPadrao()
    {
        if (itemSelecionadoPadrao == null) return;

        itemSelecionado.Nome = itemSelecionadoPadrao.Nome;
        itemSelecionado.Ativo = itemSelecionadoPadrao.Ativo;
        itemSelecionado.Descricao = itemSelecionadoPadrao.Descricao;
        itemSelecionado.IndicePlayPadrao = itemSelecionadoPadrao.IndicePlayPadrao;
        itemSelecionado.IncidePausePadrao = itemSelecionadoPadrao.IncidePausePadrao;
        itemSelecionado.TransferirContadores(itemSelecionadoPadrao.Contadores);
    }
    protected bool EstaNoPadraoPadrao
    {
        get
        {
            if (itemSelecionadoPadrao == null) return true;

            return itemSelecionado.Nome == itemSelecionadoPadrao.Nome
                && itemSelecionado.Ativo == itemSelecionadoPadrao.Ativo
                && itemSelecionado.Descricao == itemSelecionadoPadrao.Descricao
                && itemSelecionado.IndicePlayPadrao == itemSelecionadoPadrao.IndicePlayPadrao
                && itemSelecionado.IncidePausePadrao == itemSelecionadoPadrao.IncidePausePadrao
                && itemSelecionado.PossuiMesmosContadores(itemSelecionadoPadrao.Contadores);
        }
    }

    #region Lista de contadores
    protected void AdicionarContador()
    {
        contadorSelecionado = new ContadorModel(true);
        DrawerContadorAberto = true;
    }
    protected void AdicionarEspaçador()
    {
        var linha = ContadoresPreCadastrados.Espacador;
        linha.Seq = itemSelecionado.Contadores.Count + 1;
        itemSelecionado.Contadores.Add(linha);
        StateHasChanged();
    }
    protected void SubirContador(ContadorModel contador)
    {
        if (contador == null
         || contador.Seq == 1)
            return;

        var alvo = itemSelecionado.Contadores.Where(x => x.Seq == contador.Seq - 1).FirstOrDefault();

        if (alvo == null) return;
        if (itemSelecionado.IndicePlayPadrao == contador.Seq) itemSelecionado.IndicePlayPadrao--;
        if (itemSelecionado.IncidePausePadrao == contador.Seq) itemSelecionado.IndicePlayPadrao--;
        if (itemSelecionado.IndicePlayPadrao == alvo.Seq) itemSelecionado.IndicePlayPadrao++;
        if (itemSelecionado.IncidePausePadrao == alvo.Seq) itemSelecionado.IndicePlayPadrao++;

        alvo.Seq++;
        contador.Seq--;

        itemSelecionado.Contadores[alvo.Seq - 1] = alvo;
        itemSelecionado.Contadores[contador.Seq - 1] = contador;

        StateHasChanged();
    }
    protected void DescerContador(ContadorModel contador)
    {
        if (contador == null
         || contador.Seq == itemSelecionado.Contadores.Count + 1)
            return;

        var alvo = itemSelecionado.Contadores.Where(x => x.Seq == contador.Seq + 1).FirstOrDefault();

        if (alvo == null) return;
        if (itemSelecionado.IndicePlayPadrao == contador.Seq) itemSelecionado.IndicePlayPadrao++;
        if (itemSelecionado.IncidePausePadrao == contador.Seq) itemSelecionado.IndicePlayPadrao++;
        if (itemSelecionado.IndicePlayPadrao == alvo.Seq) itemSelecionado.IndicePlayPadrao--;
        if (itemSelecionado.IncidePausePadrao == alvo.Seq) itemSelecionado.IndicePlayPadrao--;

        alvo.Seq--;
        contador.Seq++;

        itemSelecionado.Contadores[contador.Seq - 1] = contador;
        itemSelecionado.Contadores[alvo.Seq - 1] = alvo;

        StateHasChanged();
    }
    protected void EditarContador(ContadorModel contador)
    {
        if (contador == null) return;

        contadorSelecionado = new();
        contadorSelecionado.Update(contador);

        var original = itemSelecionado.Contadores.FirstOrDefault(x => x.Id == contadorSelecionado.Id);
        if (original == null)
            contadorSelecionadoPadrao = null;
        else
        {
            contadorSelecionadoPadrao = new();
            contadorSelecionadoPadrao.Update(original);
            contadorSelecionadoPadrao.Seq = contadorSelecionado.Seq;
        }

        DrawerContadorAberto = true;
    }
    protected async Task ExcluirContador(ContadorModel contador)
    {
        if (contador.ConfirmacaoPendente) return;
        contador.ConfirmacaoPendente = true;

        var resposta = await MetodosEstaticos.MensagemAsync($"Excluir [{contador.Nome}]?\nEssa ação não pode ser desfeita.\n\nExcluir um contador não removerá ele de padrões e expressos já criados, nem impedirá tempo já registrado nele de aparecer em relatórios.",
                                                            "Confirmar exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
        contador.ConfirmacaoPendente = false;
        if (resposta != MessageBoxResult.Yes) return;
        itemSelecionado.Contadores.Remove(contador);
        foreach (var item in itemSelecionado.Contadores)
            if (item.Seq > contador.Seq) item.Seq--;

        await SalvaAtualiza();
    }

    #endregion
    #endregion

    #region Drawer Contador
    protected bool DrawerContadorAberto { get; set; } = false;
    protected static string DescricaoContaTempo(bool contaTempo) => string.Concat("Tempo gasto conta como ", contaTempo ? "em atividade" : "interrupção de trabalho.");
    protected void CancelarDrawerContador() => DrawerContadorAberto = false;
    protected async Task SalvarDrawerContador()
    {
        if (!itemSelecionado.Contadores.Any(x => x.Id == contadorSelecionado.Id))
        {
            var padraoZerado = new PadraoDeCronometroModel(true);
            if (padraoZerado.Nome != contadorSelecionado.Nome
             || (padraoZerado.Contadores?.Count ?? 0) > 0)
                itemSelecionado.Contadores.Add(contadorSelecionado);
        }
        else
        {
            var alterado = itemSelecionado.Contadores.First(x => x.Id == contadorSelecionado.Id);
            var index = itemSelecionado.Contadores.IndexOf(alterado);
            itemSelecionado.Contadores[index] = contadorSelecionado;
            contadorSelecionado = new();
        }
        await SalvaAtualiza();
        DrawerContadorAberto = false;
    }
    protected void RestaurarDrawerContador()
    {
        if (contadorSelecionadoPadrao == null) return;

        contadorSelecionado.Nome = contadorSelecionadoPadrao.Nome;
        contadorSelecionado.Seq = contadorSelecionadoPadrao.Seq;
        contadorSelecionado.Descricao = contadorSelecionadoPadrao.Descricao;
        contadorSelecionado.Icone = contadorSelecionadoPadrao.Icone;
        contadorSelecionado.IconeCor = contadorSelecionadoPadrao.IconeCor;
        contadorSelecionado.ContaTempo = contadorSelecionadoPadrao.ContaTempo;
    }
    protected bool EstaNoPadraoContador
    {
        get
        {
            if (contadorSelecionadoPadrao == null) return true;

            return contadorSelecionado.Nome == contadorSelecionadoPadrao.Nome
                && contadorSelecionado.Descricao == contadorSelecionadoPadrao.Descricao
                && contadorSelecionado.Icone == contadorSelecionadoPadrao.Icone
                && contadorSelecionado.IconeCor == contadorSelecionadoPadrao.IconeCor
                && contadorSelecionado.ContaTempo == contadorSelecionadoPadrao.ContaTempo;
        }
    }
    protected async Task SelcionarIcone(ContadorModel contador)
    {
        var dadosEnvio = new IconeDados(contador.IconeCor,
                                        contador.Icone,
                                        contador.PadraoNome() ?? contador.Nome,
                                        contador.PadraoCor(),
                                        contador.PadraoIcone());
        var dialogo = await DialogService.ShowAsync<SelecaoIcone>("",
                            new DialogParameters { { "Dados", dadosEnvio } },
                            new DialogOptions { CloseOnEscapeKey = true });
        var resultado = await dialogo.Result;

        if (resultado == null) return;
        if (!resultado.Canceled)
        {
            var dadosRetorno = resultado.Data as IconeDados;
            dadosRetorno ??= new();

            contadorSelecionado.IconeCor = dadosRetorno.Cor;
            contadorSelecionado.Icone = dadosRetorno.Icone;
        }
    }
    #endregion

    #region CRUD
    protected void CriarPadraoDeCronometro()
    {
        itemSelecionado = new PadraoDeCronometroModel(true);
        DrawerPadraoAberto = true;
    }
    protected void EditarPadraoDeCronometro(PadraoDeCronometroModel padraoDeCronometro)
    {
        if (padraoDeCronometro == null) return;

        itemSelecionado = new();
        itemSelecionado.Update(padraoDeCronometro);

        if (itemSelecionado.Padrao)
        {
            itemSelecionadoPadrao = new();
            itemSelecionadoPadrao.Update(ModelPadrao.First(x => x.Id == itemSelecionado.Id));
        }

        DrawerPadraoAberto = true;
    }
    protected async Task ExcluirPadraoDeCronometro(PadraoDeCronometroModel padraoDeCronometro)
    {
        if (padraoDeCronometro.ConfirmacaoPendente) return;
        padraoDeCronometro.ConfirmacaoPendente = true;

        var resposta = await MetodosEstaticos.MensagemAsync($"Excluir [{padraoDeCronometro.Nome}]?\nEssa ação não pode ser desfeita.\n\nExcluir um contador não removerá ele de padrões e expressos já criados, nem impedirá tempo já registrado nele de aparecer em relatórios.",
                                                            "Confirmar exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);
        padraoDeCronometro.ConfirmacaoPendente = false;
        if (resposta != MessageBoxResult.Yes) return;
        Model.Remove(padraoDeCronometro);
        await SalvaAtualiza();
    }
    #endregion

}
