using Devnometro.Dominio;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro.TelasConfig.Cadastros;

public class ContadorBase : ComponentBase
{
    [Parameter] public required Devnometro.Dominio.Preferencias Preferencias { get; set; }
    [Inject] IDialogService DialogService { get; set; } = null!;

    protected List<Dominio.Contador> Model = [];
    protected string textoConsulta;
    protected bool drawerAberto = false;

    protected override void OnInitialized()
    {
        Model = Dominio.Contador.ListaMocada();
    }

    protected Func<Dominio.Contador, bool> Filtrar => x =>
    {
        if (string.IsNullOrWhiteSpace(textoConsulta))
            return true;

        if (x.Nome.Contains(textoConsulta, StringComparison.OrdinalIgnoreCase))
            return true;

        //if (x.Descricao.Contains(textoConsulta, StringComparison.OrdinalIgnoreCase))
        //    return true;

        return false;
    };

    protected async Task SelcionarIcone(string descricao, Color cor, string iconeAtual, string iconePadrao = "")
    {
        var dadosEnvio = new IconeDados(descricao: descricao,
                                        cor: cor,
                                        iconeAtual: iconeAtual,
                                        iconePadrao: iconePadrao);
        var dialogo = await DialogService.ShowAsync<SelecaoIcone>("",
                            new DialogParameters { { "Dados", dadosEnvio } },
                            new DialogOptions { CloseOnEscapeKey = true });
        var resultado = await dialogo.Result;

        if (resultado == null) return;
        if (!resultado.Canceled)
        {
            var dadosRetorno = resultado.Data as IconeDados;
            dadosRetorno ??= new();

            cor = dadosRetorno.Cor;
            iconeAtual = dadosRetorno.IconeAtual;
        }
    }
}
