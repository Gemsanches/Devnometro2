using Microsoft.Win32;
using Microsoft.AspNetCore.Components;
using Devnometro.Dominio;
using Devnometro.Aplicacao;
using System.Threading.Tasks;

namespace Devnometro.TelasConfig.Configuracoes;

public class CPontoBase : ComponentBase
{
    [Parameter] public required Preferencias Preferencias { get; set; }

    protected bool termoDeAceiteSelecionado = false;

    protected override void OnInitialized()
    {
        if (Preferencias.Ponto.AFD_Habilitado)
            termoDeAceiteSelecionado = true;
    }

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

    protected async Task ConfirmarCiencia()
    {
        Preferencias.Ponto.AFD_ConfirmacaoDeCiencia = true;
        Preferencias.Ponto.AFD_DataHoraUltimaConfirmacaoDeCiencia = DateTime.Now;
        await (new ManipuladorDeArquivo()).SalvarLogAceite(Preferencias);
    }
}