using Microsoft.Win32;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MudBlazor;

namespace Devnometro.TelasConfig.Configuracoes;

public class CPontoBase : ComponentBase
{
    #region Constantes
    protected const int padraoTempoBatidaRepetida = 60;
    protected const bool padraoPrimeiroPontoAutomatico = false;
    protected const bool padraoAutoExportarComecoDoMes = true;
    protected const string padraoCaminhoPonto = "";
    #endregion

    #region Propriedades
    protected int TempoBatidaRepetida { get; set; } = padraoTempoBatidaRepetida;
    protected bool PrimeiroPontoAutomatico { get; set; } = padraoPrimeiroPontoAutomatico;
    protected bool AutoExportarComecoDoMes { get; set; } = padraoAutoExportarComecoDoMes;
    protected string CaminhoPonto { get; set; } = padraoCaminhoPonto;
    #endregion

    #region Botão Restaurar padrões
    protected void RestauraPadroes()
    {
        TempoBatidaRepetida = padraoTempoBatidaRepetida;
        PrimeiroPontoAutomatico = padraoPrimeiroPontoAutomatico;
        AutoExportarComecoDoMes = padraoAutoExportarComecoDoMes;
        //CaminhoPonto = padraoCaminhoPonto;
    }
    protected bool EstaNosPadroes
    {
        get => TempoBatidaRepetida == padraoTempoBatidaRepetida
            //&& CaminhoPonto == padraoCaminhoPonto
            && PrimeiroPontoAutomatico == padraoPrimeiroPontoAutomatico
            && AutoExportarComecoDoMes == padraoAutoExportarComecoDoMes;
    }
    protected static string BooleanoSimNao(bool valor) => valor ? "Sim" : "Não";
    #endregion

    protected void SelecionarPasta()
    {
        var dialog = new OpenFolderDialog();
        var result = dialog.ShowDialog();
        if (result.GetValueOrDefault(false))
            CaminhoPonto = dialog.FolderName;
    }
    protected void AjustadoPeloSlider(int valor) => TempoBatidaRepetida = valor;
}
