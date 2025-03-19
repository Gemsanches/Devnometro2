using Devnometro.Aplicacao;
using Devnometro.Dominio;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
    }

    protected List<ContadorModel> Model { get; set; } = [];
    protected ContadorModel itemSelecionado = new();
    protected ContadorModel? novoItem;
    
    #region Consulta
    protected string textoConsulta = "";
    protected bool nadaPraSalvar = true;
    
    protected Func<ContadorModel, bool> Filtrar => x =>
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
        await manipulador.SalvarContadoresAsync(Model);
        nadaPraSalvar = true;
        salvando = false;
    }
    protected bool NaoPodeSalvar { get => nadaPraSalvar || salvando; }
    protected bool salvando = false;
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
        var retorno = await MetodosEstaticos.MensagemAsync("Esse processo apagará todas as personalizações e restaurará a lista de contadores que vem pré-instalada no Devnômetro.\nÉ isso mesmo que quer fazer?", "Tem certeza?", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (retorno != MessageBoxResult.Yes)
        {
            restaurando = false;
            return;
        }

        if (ManipuladorDeArquivo.LimparContadores())
        {
            Model = ContadorModel.ListaMocada();
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
                var contadorZerado = new ContadorModel(true);
                if (contadorZerado.Nome != novoItem.Nome
                 || contadorZerado.Descricao != novoItem.Descricao
                 || contadorZerado.Icone != novoItem.Icone)
                {
                    Model.Add(novoItem);
                    nadaPraSalvar = false;
                }
                novoItem = null;
            }
            StateHasChanged();
        }
    }
    private bool _drawerAberto = false;
    
    protected static string DescricaoContaTempo(bool contaTempo) => string.Concat("Tempo gasto conta como ", contaTempo ? "em atividade" : "interrupção de trabalho.");

    protected async Task SelcionarIcone(string nome, Color cor, string icone)
    {
        var dadosEnvio = new IconeDados(cor, icone, nome);
        var dialogo = await DialogService.ShowAsync<SelecaoIcone>("",
                            new DialogParameters { { "Dados", dadosEnvio } },
                            new DialogOptions { CloseOnEscapeKey = true });
        var resultado = await dialogo.Result;

        if (resultado == null) return;
        if (!resultado.Canceled)
        {
            var dadosRetorno = resultado.Data as IconeDados;
            dadosRetorno ??= new();

            itemSelecionado.CorIcone = dadosRetorno.Cor;
            itemSelecionado.Icone = dadosRetorno.Icone;
        }
    }
    #endregion

    #region CRUD
    protected void CriarContador()
    {
        novoItem = new ContadorModel(true);
        itemSelecionado = novoItem;
        DrawerAberto = true;
    }

    protected void EditarContador(ContadorModel contador)
    {
        if (contador == null) return;

        itemSelecionado = contador;
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
        nadaPraSalvar = false;
        StateHasChanged();
    }
    #endregion
}
