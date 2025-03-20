using Devnometro.Aplicacao;
using Devnometro.Dominio;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Devnometro.TelasConfig.Cadastros;

public class ExpressoBase : ComponentBase
{
    [Parameter] public required Preferencias Preferencias { get; set; }
    [Inject] IDialogService DialogService { get; set; } = null!;

    private readonly ManipuladorDeArquivo manipulador = new();

    protected override void OnInitialized()
    {
        Model = manipulador.CarregarExpressos();
        Contadores = manipulador.CarregarContadores();
    }

    protected List<ExpressoModel> Model { get; set; } = [];
    protected ExpressoModel itemSelecionado = new();
    protected ExpressoModel? novoItem;
    protected List<ContadorModel> Contadores { get; set; } = [];

    #region Consulta
    protected string textoConsulta = "";
    protected bool nadaPraSalvar = true;

    protected Func<ExpressoModel, bool> Filtrar => x =>
    {
        if (string.IsNullOrWhiteSpace(textoConsulta))
            return true;

        if (x.Nome.Contains(textoConsulta, StringComparison.OrdinalIgnoreCase))
            return true;

        return false;
    };
    protected async Task SalvarCadastro()
    {
        if (salvando) return;
        salvando = true;
        await manipulador.SalvarExpressosAsync(Model);
        nadaPraSalvar = true;
        salvando = false;
    }
    protected bool NaoPodeSalvar { get => nadaPraSalvar || salvando; }
    protected bool salvando = false;
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
            nadaPraSalvar = false;
            StateHasChanged();
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

        if (ManipuladorDeArquivo.LimparExpressos())
        {
            Model = ExpressoModel.ListaMocada();
            nadaPraSalvar = true;
            StateHasChanged();
        }
        else
            await MetodosEstaticos.MensagemAsync("Ocorreu algum problema ao restaurar os padrões", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        restaurando = false;
    }
    protected bool restaurando = false;
    #endregion

    #region Drawer
    protected bool DrawerAberto
    {
        get => _drawerAberto;
        set
        {
            _drawerAberto = value;
            if (!value && novoItem != null)
            {
                var expressoZerado = new ExpressoModel(true);
                if (expressoZerado.Nome != novoItem.Nome
                 || expressoZerado.Contador.Nome != novoItem.Contador.Nome
                 || expressoZerado.Contador.Descricao != novoItem.Contador.Descricao
                 || expressoZerado.Contador.Icone != novoItem.Contador.Icone)
                {
                    Model.Add(novoItem);
                }
                novoItem = null;
            }
            nadaPraSalvar = false;
            StateHasChanged();
        }
    }
    private bool _drawerAberto = false;
    #endregion

    #region CRUD
    protected void CriarExpresso()
    {
        novoItem = new ExpressoModel(true);
        itemSelecionado = novoItem;
        DrawerAberto = true;
    }

    protected void EditarExpresso(ExpressoModel expresso)
    {
        if (expresso == null) return;

        itemSelecionado = expresso;
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
        nadaPraSalvar = false;
        StateHasChanged();
    }
    #endregion
}
