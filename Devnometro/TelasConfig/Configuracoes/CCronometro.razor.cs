using Microsoft.AspNetCore.Components;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro.TelasConfig.Configuracoes;

public class CCronometroBase : ComponentBase
{
    #region Constantes
    protected const int padraoCronometrosSimultaneos = 5;
    protected const bool padraoExcluirAutomaticamente = true;
    protected const bool padraoApenasUmAtivo = true;
    protected const string padraoCaminhoTempo = "";
    #endregion

    #region Propriedades
    protected int CronometrosSimultaneos { get; set; } = padraoCronometrosSimultaneos;
    protected bool ExcluirAutomaticamente { get; set; } = padraoExcluirAutomaticamente;
    protected bool ApenasUmAtivo { get; set; } = padraoApenasUmAtivo;
    protected string CaminhoTempo { get; set; } = padraoCaminhoTempo;
    #endregion

    #region Botão Restaurar padrões
    protected void RestauraPadroes()
    {
        CronometrosSimultaneos = padraoCronometrosSimultaneos;
        //CaminhoPonto = padraoCaminhoPonto;
        ExcluirAutomaticamente = padraoExcluirAutomaticamente;
        ApenasUmAtivo = padraoApenasUmAtivo;
    }
    protected bool EstaNosPadroes
    {
        get => CronometrosSimultaneos == padraoCronometrosSimultaneos
            //&& CaminhoPonto == padraoCaminhoPonto
            && ExcluirAutomaticamente == padraoExcluirAutomaticamente
            && ApenasUmAtivo == padraoApenasUmAtivo;
    }
    protected static string BooleanoSimNao(bool valor) => valor ? "Sim" : "Não";
    #endregion

    protected void SelecionarPasta()
    {
        var dialog = new OpenFolderDialog();
        var result = dialog.ShowDialog();
        if (result.GetValueOrDefault(false))
            CaminhoTempo = dialog.FolderName;
    }
    protected void AjustadoPeloSlider(int valor) => CronometrosSimultaneos = valor;
}
