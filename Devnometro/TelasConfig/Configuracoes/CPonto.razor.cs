using Microsoft.Win32;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MudBlazor;
using Devnometro.Dominio;

namespace Devnometro.TelasConfig.Configuracoes;

public class CPontoBase : ComponentBase
{
    [Parameter]
    public required Devnometro.Dominio.Preferencias Preferencias { get; set; }

    #region Botão Restaurar padrões
    protected void RestauraPadroes()
    {
        Preferencias.Ponto.TempoBatidaRepetida = PrefereciaPadrao.PontoTempoBatidaRepetida;
        Preferencias.Ponto.PrimeiroPontoAutomatico = PrefereciaPadrao.PontoPrimeiroAutomatico;
        Preferencias.Ponto.AutoExportarComecoDoMes = PrefereciaPadrao.PontoAutoExportarComecoDoMes;
        //Preferencias.Ponto.Caminho = PrefereciaPadrao.CaminhoPonto;
    }
    protected bool EstaNosPadroes
    {
        get => Preferencias.Ponto.TempoBatidaRepetida == PrefereciaPadrao.PontoTempoBatidaRepetida
            //&& Preferencias.Ponto.Caminho == PrefereciaPadrao.CaminhoPonto
            && Preferencias.Ponto.PrimeiroPontoAutomatico == PrefereciaPadrao.PontoPrimeiroAutomatico
            && Preferencias.Ponto.AutoExportarComecoDoMes == PrefereciaPadrao.PontoAutoExportarComecoDoMes;
    }
    #endregion

    protected void SelecionarPasta()
    {
        var dialog = new OpenFolderDialog();
        var result = dialog.ShowDialog();
        if (result.GetValueOrDefault(false))
            Preferencias.Ponto.Caminho = dialog.FolderName;
    }
    protected void AjustadoPeloSlider(int valor) => Preferencias.Ponto.TempoBatidaRepetida = valor;
}