using Devnometro.Dominio;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro.TelasConfig.Configuracoes;

public class CGeralBase : ComponentBase
{
    [Parameter]
    public required Devnometro.Dominio.Preferencias Preferencias { get; set; }

    #region Botão Restaurar padrões
    protected void RestauraPadroes()
    {
        Preferencias.Geral.Exibicao = PrefereciaPadrao.Exibicao;
        Preferencias.Geral.LigarJunto = PrefereciaPadrao.LigarJunto;
        Preferencias.AutoSave.Ativo = PrefereciaPadrao.AutoSave;
        Preferencias.AutoSave.MarcarPonto = PrefereciaPadrao.AutoSaveMarcarPonto;
        Preferencias.AutoSave.EditarPonto = PrefereciaPadrao.AutoSaveEditarPonto;
        Preferencias.AutoSave.ExcluirPonto = PrefereciaPadrao.AutoSaveExcluirPonto;
        Preferencias.AutoSave.CriarCronometro = PrefereciaPadrao.AutoSaveCriarCronometro;
        Preferencias.AutoSave.EditarCronometro = PrefereciaPadrao.AutoSaveEditarCronometro;
        Preferencias.AutoSave.ExcluirCronometro = PrefereciaPadrao.AutoSaveExcluirCronometro;
        Preferencias.AutoSave.FinalizarCronometro = PrefereciaPadrao.AutoSaveFinalizarCronometro;
        Preferencias.AutoSave.AbandonarCronometro = PrefereciaPadrao.AutoSaveAbandonarCronometro;
        Preferencias.AutoSave.Tempo = PrefereciaPadrao.AutoSaveTempo;
        Preferencias.Geral.CaminhoAtendimento = PrefereciaPadrao.CaminhoAtendimento;
        Preferencias.Geral.CaminhoDesenvolvimento = PrefereciaPadrao.CaminhoDesenvolvimento;
    }
    protected bool EstaNosPadroes
    {
        get => Preferencias.Geral.Exibicao == PrefereciaPadrao.Exibicao
            && Preferencias.Geral.LigarJunto == PrefereciaPadrao.LigarJunto
            && Preferencias.AutoSave.Ativo == PrefereciaPadrao.AutoSave
            && Preferencias.AutoSave.MarcarPonto == PrefereciaPadrao.AutoSaveMarcarPonto
            && Preferencias.AutoSave.EditarPonto == PrefereciaPadrao.AutoSaveEditarPonto
            && Preferencias.AutoSave.ExcluirPonto == PrefereciaPadrao.AutoSaveExcluirPonto
            && Preferencias.AutoSave.CriarCronometro == PrefereciaPadrao.AutoSaveCriarCronometro
            && Preferencias.AutoSave.EditarCronometro == PrefereciaPadrao.AutoSaveEditarCronometro
            && Preferencias.AutoSave.ExcluirCronometro == PrefereciaPadrao.AutoSaveExcluirCronometro
            && Preferencias.AutoSave.FinalizarCronometro == PrefereciaPadrao.AutoSaveFinalizarCronometro
            && Preferencias.AutoSave.AbandonarCronometro == PrefereciaPadrao.AutoSaveAbandonarCronometro
            && Preferencias.AutoSave.Tempo == PrefereciaPadrao.AutoSaveTempo
            && Preferencias.Geral.CaminhoAtendimento == PrefereciaPadrao.CaminhoAtendimento
            && Preferencias.Geral.CaminhoDesenvolvimento == PrefereciaPadrao.CaminhoDesenvolvimento;
    }
    #endregion

    #region Auxiliares de exibição
    protected static string DescricaoExibicao(int valor)
    {
        return valor switch
        {
            1 => "Compacto",
            2 => "Normal",
            3 => "Expandida",
            4 => "Legendada",
            _ => $" Ué? {valor}?",
        };
    }
    protected void AjustadoPeloSlider(int valor) => Preferencias.AutoSave.Tempo = valor;
    #endregion
}