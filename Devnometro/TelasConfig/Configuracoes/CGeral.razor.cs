using Devnometro.Aplicacao;
using Devnometro.Dominio;
using Devnometro.Dominio.Enumeradores;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Devnometro.TelasConfig.Configuracoes;

public class CGeralBase : ComponentBase
{
    [Parameter] public required Devnometro.Dominio.Preferencias Preferencias { get; set; }
    [Inject] private ManipuladorDeArquivo Manipulador { get; set; } = new();

    protected bool mensagemAberta = false;

    #region Nomenclatura
    private const string tituloPadrao = "Tem certeza?";
    private const string alertaPadrao = "\n\nEssa ação não pode ser desfeita";

    protected const string excluir = "Excluir";
    protected const string temaPersonalizado = "Tema Personalizado";
    protected const string configuracoes = "Configurações";
    protected const string historicoPonto = "Histórico de Ponto";
    protected const string historicoTempo = "Histórico de Tempos";
    protected const string cronometrosAbertos = "Cronômetros Abertos";
    protected const string cadastrosContadores = "Cadastros de Contadores";
    protected const string cadastrosPadroes = "Cadastros de Padrões";
    protected const string cadastrosExpressos = "Cadastros de Expressos";
    #endregion

    #region Constantes
    protected const MudBlazor.Variant varianteBotoesExclusao = Variant.Outlined;
    protected const MudBlazor.Color corBotoesExclusao = Color.Warning;
    #endregion

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

    #region Trava de exclusão
    protected int? ContadorDestravar { get; set; } = 3;
    protected bool Destravado { get; set; }
    protected Color CorContador 
    {
        get
        {
            if (ContadorDestravar.GetValueOrDefault(0) >= 3)
                return Color.Info;
            else if (ContadorDestravar.GetValueOrDefault(0) == 2)
                return Color.Warning;
            else if (ContadorDestravar.GetValueOrDefault(0) == 1)
                return Color.Error;
            else
                return Color.Transparent;
        }
    }

    protected void Destravar()
    {
        if (ContadorDestravar.GetValueOrDefault(0) > 0)
        {
            ContadorDestravar--;
            if (ContadorDestravar == 0) ContadorDestravar = null;
            return;
        }
        Destravado = true;
    }
    #endregion

    #region Exclusões
    protected bool JahExcluido(bool tipo) => mensagemAberta || tipo;

    protected async Task ExcluirTemaPersonalizado()
    {
        if (mensagemAberta) return;
        mensagemAberta = true;

        var resposta = await MetodosEstaticos.MensagemAsync($"{excluir} {temaPersonalizado}?{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        mensagemAberta = false;
        
        if (resposta != MessageBoxResult.Yes) return;

        if (ManipuladorDeArquivo.LimparTemaPersonalizado())
        {
            this.Preferencias.TemaPersonalizado = TemaPersonalizado.CriarTemaPersonalizado();
            this.Preferencias.TemaPersonalizado.AplicarCoresAoTema();
            var temp = this.Preferencias.TemaSelecionado == ETema.TemaPersonalizado
                     ? ETema.TemaPadrao
                     : this.Preferencias.TemaSelecionado;
            this.Preferencias.TemaSelecionado = ETema.TemaPersonalizado;
            this.Preferencias.TemaSelecionado = temp;
            StateHasChanged();
            await MetodosEstaticos.MensagemAsync($"{temaPersonalizado} excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            temaPersonalizadoJahExcluido = true;
        }
        else
            await MetodosEstaticos.MensagemAsync($"{temaPersonalizado} não foi excluído!", "Falha", MessageBoxButton.OK, MessageBoxImage.Error);
    }
    protected bool temaPersonalizadoJahExcluido = false;
    protected async Task ExcluirConfiguracoes()
    {
        if (mensagemAberta) return;
        mensagemAberta = true;

        var resposta = await MetodosEstaticos.MensagemAsync($"{excluir} {configuracoes}?\n(todas as {configuracoes} retornarão ao padrão)", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        mensagemAberta = false;

        if (resposta != MessageBoxResult.Yes) return;

        this.Preferencias = Preferencias.PreferenciasPadroes();
        if (await Manipulador.SalvarPreferencias(this.Preferencias))
        {
            StateHasChanged();
            await MetodosEstaticos.MensagemAsync($"{configuracoes} excluídas com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        configuracoesJahExcluido = true;
    }
    protected bool configuracoesJahExcluido = false;
    protected async Task ExcluirHistoricoPonto()
    {
        if (mensagemAberta) return;
        mensagemAberta = true;

        var resposta = await MetodosEstaticos.MensagemAsync($"{excluir} {historicoPonto}?{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        mensagemAberta = false;

        if (resposta != MessageBoxResult.Yes) return;
        historicoPontoJahExcluido = true;
    }
    protected bool historicoPontoJahExcluido = false;
    protected async Task ExcluirHistoricoTempo()
    {
        if (mensagemAberta) return;
        mensagemAberta = true;

        var resposta = await MetodosEstaticos.MensagemAsync($"{excluir} {historicoTempo}?{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        mensagemAberta = false;

        if (resposta != MessageBoxResult.Yes) return;
        historicoTempoJahExcluido = true;
    }
    protected bool historicoTempoJahExcluido = false;
    protected async Task ExcluirCronometrosAbertos()
    {
        if (mensagemAberta) return;
        mensagemAberta = true;

        var resposta = await MetodosEstaticos.MensagemAsync($"{excluir} {cronometrosAbertos}?{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        mensagemAberta = false;

        if (resposta != MessageBoxResult.Yes) return;
        cronometrosAbertosJahExcluido = true;
    }
    protected bool cronometrosAbertosJahExcluido = false;
    protected async Task ExcluirCadastrosContadores()
    {
        if (mensagemAberta) return;
        mensagemAberta = true;

        var resposta = await MetodosEstaticos.MensagemAsync($"{excluir} {cadastrosContadores}?\nIsso excluirá também:\n{cadastrosPadroes}\n{cadastrosExpressos}{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        mensagemAberta = false;

        if (resposta != MessageBoxResult.Yes) return;
        cadastrosContadoresJahExcluido = true;
    }
    protected bool cadastrosContadoresJahExcluido = false;
    protected async Task ExcluirCadastrosPadroes()
    {
        if (mensagemAberta) return;
        mensagemAberta = true;

        var resposta = await MetodosEstaticos.MensagemAsync($"{excluir} {cadastrosPadroes}?{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        mensagemAberta = false;

        if (resposta != MessageBoxResult.Yes) return;
        cadastrosPadroesJahExcluido = true;
    }
    protected bool cadastrosPadroesJahExcluido = false;
    protected async Task ExcluirCadastrosExpressos()
    {
        if (mensagemAberta) return;
        mensagemAberta = true;

        var resposta = await MetodosEstaticos.MensagemAsync($"{excluir} {cadastrosExpressos}?{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        mensagemAberta = false;

        if (resposta != MessageBoxResult.Yes) return;
        cadastrosExpressosJahExcluido = true;
    }
    protected bool cadastrosExpressosJahExcluido = false;

    #endregion
}