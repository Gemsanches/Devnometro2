using Devnometro.Dominio;
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
    [Parameter]
    public required Devnometro.Dominio.Preferencias Preferencias { get; set; }

    #region Botão Restaurar padrões
    protected void RestauraPadroes()
    {
        Preferencias.Cronometro.CronometrosSimultaneos = PrefereciaPadrao.CronometrosSimultaneos;
        //Preferencias.Cronometro.Caminho = PrefereciaPadrao.CaminhoCronometro;
        Preferencias.Cronometro.ExcluirAutomaticamente = PrefereciaPadrao.CronometroExcluirAutomaticamente;
        Preferencias.Cronometro.ApenasUmAtivo = PrefereciaPadrao.CronometroApenasUmAtivo;
    }
    protected bool EstaNosPadroes
    {
        get => Preferencias.Cronometro.CronometrosSimultaneos == PrefereciaPadrao.CronometrosSimultaneos
            //&& Preferencias.Cronometro.Caminho == PrefereciaPadrao.CaminhoCronometro
            && Preferencias.Cronometro.ExcluirAutomaticamente == PrefereciaPadrao.CronometroExcluirAutomaticamente
            && Preferencias.Cronometro.ApenasUmAtivo == PrefereciaPadrao.CronometroApenasUmAtivo;
    }
    #endregion

    protected void SelecionarPasta()
    {
        var dialog = new OpenFolderDialog();
        var result = dialog.ShowDialog();
        if (result.GetValueOrDefault(false))
            Preferencias.Cronometro.Caminho = dialog.FolderName;
    }
    protected void AjustadoPeloSlider(int valor) => Preferencias.Cronometro.CronometrosSimultaneos = valor;
}
