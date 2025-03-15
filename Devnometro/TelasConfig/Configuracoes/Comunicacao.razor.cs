using Devnometro.Dominio;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro.TelasConfig.Configuracoes;

public class ComunicacaoBase : ComponentBase
{
    [Parameter] public required Devnometro.Dominio.Preferencias Preferencias { get; set; }

    #region Botão Restaurar padrões
    protected void RestauraPadroes()
    {
        Preferencias.Comunicacao.AvisarFecharPrograma = PrefereciaPadrao.AvisarFecharPrograma;
        Preferencias.Comunicacao.AvisarIniciarExpotacaoPonto = PrefereciaPadrao.AvisarIniciarExpotacaoPonto;
        Preferencias.Comunicacao.AvisarSucessoExpotacaoPonto = PrefereciaPadrao.AvisarSucessoExpotacaoPonto;
        Preferencias.Comunicacao.AvisarIniciarExpotacaoTempo = PrefereciaPadrao.AvisarIniciarExpotacaoTempo;
        Preferencias.Comunicacao.AvisarSucessoExpotacaoTempo = PrefereciaPadrao.AvisarSucessoExpotacaoTempo;
        Preferencias.Comunicacao.AvisarIniciarCriacaoPonto = PrefereciaPadrao.AvisarIniciarCriacaoPonto;
        Preferencias.Comunicacao.AvisarSucessoCriacaoPonto = PrefereciaPadrao.AvisarSucessoCriacaoPonto;
        Preferencias.Comunicacao.AvisarIniciarExclusaoPonto = PrefereciaPadrao.AvisarIniciarExclusaoPonto;
        Preferencias.Comunicacao.AvisarSucessoExclusaoPonto = PrefereciaPadrao.AvisarSucessoExclusaoPonto;
        Preferencias.Comunicacao.AvisarIniciarEdicaoPonto = PrefereciaPadrao.AvisarIniciarEdicaoPonto;
        Preferencias.Comunicacao.AvisarSucessoEdicaoPonto = PrefereciaPadrao.AvisarSucessoEdicaoPonto;
        Preferencias.Comunicacao.AvisarIniciarCriacaoCronometro = PrefereciaPadrao.AvisarIniciarCriacaoCronometro;
        Preferencias.Comunicacao.AvisarSucessoCriacaoCronometro = PrefereciaPadrao.AvisarSucessoCriacaoCronometro;
        Preferencias.Comunicacao.AvisarIniciarExclusaoCronometro = PrefereciaPadrao.AvisarIniciarExclusaoCronometro;
        Preferencias.Comunicacao.AvisarSucessoExclusaoCronometro = PrefereciaPadrao.AvisarSucessoExclusaoCronometro;
        Preferencias.Comunicacao.AvisarIniciarEdicaoCronometro = PrefereciaPadrao.AvisarIniciarEdicaoCronometro;
        Preferencias.Comunicacao.AvisarSucessoEdicaoCronometro = PrefereciaPadrao.AvisarSucessoEdicaoCronometro;
        Preferencias.Comunicacao.AvisarIniciarAbandonoCronometro = PrefereciaPadrao.AvisarIniciarAbandonoCronometro;
        Preferencias.Comunicacao.AvisarSucessoAbandonoCronometro = PrefereciaPadrao.AvisarSucessoAbandonoCronometro;
        Preferencias.Comunicacao.AvisarIniciarFinalizacaoCronometro = PrefereciaPadrao.AvisarIniciarFinalizacaoCronometro;
        Preferencias.Comunicacao.AvisarSucessoFinalizacaoCronometro = PrefereciaPadrao.AvisarSucessoFinalizacaoCronometro;
    }
    protected bool EstaNosPadroes
    {
        get
        {
            return Preferencias.Comunicacao.AvisarFecharPrograma == PrefereciaPadrao.AvisarFecharPrograma
                && Preferencias.Comunicacao.AvisarIniciarExpotacaoPonto == PrefereciaPadrao.AvisarIniciarExpotacaoPonto
                && Preferencias.Comunicacao.AvisarSucessoExpotacaoPonto == PrefereciaPadrao.AvisarSucessoExpotacaoPonto
                && Preferencias.Comunicacao.AvisarIniciarExpotacaoTempo == PrefereciaPadrao.AvisarIniciarExpotacaoTempo
                && Preferencias.Comunicacao.AvisarSucessoExpotacaoTempo == PrefereciaPadrao.AvisarSucessoExpotacaoTempo
                && Preferencias.Comunicacao.AvisarIniciarCriacaoPonto == PrefereciaPadrao.AvisarIniciarCriacaoPonto
                && Preferencias.Comunicacao.AvisarSucessoCriacaoPonto == PrefereciaPadrao.AvisarSucessoCriacaoPonto
                && Preferencias.Comunicacao.AvisarIniciarExclusaoPonto == PrefereciaPadrao.AvisarIniciarExclusaoPonto
                && Preferencias.Comunicacao.AvisarSucessoExclusaoPonto == PrefereciaPadrao.AvisarSucessoExclusaoPonto
                && Preferencias.Comunicacao.AvisarIniciarEdicaoPonto == PrefereciaPadrao.AvisarIniciarEdicaoPonto
                && Preferencias.Comunicacao.AvisarSucessoEdicaoPonto == PrefereciaPadrao.AvisarSucessoEdicaoPonto
                && Preferencias.Comunicacao.AvisarIniciarCriacaoCronometro == PrefereciaPadrao.AvisarIniciarCriacaoCronometro
                && Preferencias.Comunicacao.AvisarSucessoCriacaoCronometro == PrefereciaPadrao.AvisarSucessoCriacaoCronometro
                && Preferencias.Comunicacao.AvisarIniciarExclusaoCronometro == PrefereciaPadrao.AvisarIniciarExclusaoCronometro
                && Preferencias.Comunicacao.AvisarSucessoExclusaoCronometro == PrefereciaPadrao.AvisarSucessoExclusaoCronometro
                && Preferencias.Comunicacao.AvisarIniciarEdicaoCronometro == PrefereciaPadrao.AvisarIniciarEdicaoCronometro
                && Preferencias.Comunicacao.AvisarSucessoEdicaoCronometro == PrefereciaPadrao.AvisarSucessoEdicaoCronometro
                && Preferencias.Comunicacao.AvisarIniciarAbandonoCronometro == PrefereciaPadrao.AvisarIniciarAbandonoCronometro
                && Preferencias.Comunicacao.AvisarSucessoAbandonoCronometro == PrefereciaPadrao.AvisarSucessoAbandonoCronometro
                && Preferencias.Comunicacao.AvisarIniciarFinalizacaoCronometro == PrefereciaPadrao.AvisarIniciarFinalizacaoCronometro
                && Preferencias.Comunicacao.AvisarSucessoFinalizacaoCronometro == PrefereciaPadrao.AvisarSucessoFinalizacaoCronometro;
        }
    }
    #endregion

    #region Marcações em massa
    protected void MarcaAvisos(bool marca)
    {
        Preferencias.Comunicacao.AvisarFecharPrograma = marca;
        Preferencias.Comunicacao.AvisarIniciarExpotacaoPonto = marca;
        Preferencias.Comunicacao.AvisarIniciarExpotacaoTempo = marca;
        Preferencias.Comunicacao.AvisarIniciarCriacaoPonto = marca;
        Preferencias.Comunicacao.AvisarIniciarExclusaoPonto = marca;
        Preferencias.Comunicacao.AvisarIniciarEdicaoPonto = marca;
        Preferencias.Comunicacao.AvisarIniciarCriacaoCronometro = marca;
        Preferencias.Comunicacao.AvisarIniciarExclusaoCronometro = marca;
        Preferencias.Comunicacao.AvisarIniciarEdicaoCronometro = marca;
        Preferencias.Comunicacao.AvisarIniciarAbandonoCronometro = marca;
        Preferencias.Comunicacao.AvisarIniciarFinalizacaoCronometro = marca;
    }
    protected void MarcaConfirmacoes(bool marca)
    {
        Preferencias.Comunicacao.AvisarSucessoExpotacaoPonto = marca;
        Preferencias.Comunicacao.AvisarSucessoExpotacaoTempo = marca;
        Preferencias.Comunicacao.AvisarSucessoCriacaoPonto = marca;
        Preferencias.Comunicacao.AvisarSucessoExclusaoPonto = marca;
        Preferencias.Comunicacao.AvisarSucessoEdicaoPonto = marca;
        Preferencias.Comunicacao.AvisarSucessoCriacaoCronometro = marca;
        Preferencias.Comunicacao.AvisarSucessoExclusaoCronometro = marca;
        Preferencias.Comunicacao.AvisarSucessoEdicaoCronometro = marca;
        Preferencias.Comunicacao.AvisarSucessoAbandonoCronometro = marca;
        Preferencias.Comunicacao.AvisarSucessoFinalizacaoCronometro = marca;
    }
    protected void MarcaTudo(bool marca)
    {
        MarcaAvisos(marca);
        MarcaConfirmacoes(marca);
    }
    #endregion
}