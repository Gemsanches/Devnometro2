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
    protected async Task ExcluirTemaPersonalizado()
    {
        var resposta = await MetodosEstaticos.Mensagem($"{excluir} {temaPersonalizado}?{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
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
            await MetodosEstaticos.Mensagem($"{temaPersonalizado} excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
            await MetodosEstaticos.Mensagem($"{temaPersonalizado} não foi excluído!", "Falha", MessageBoxButton.OK, MessageBoxImage.Error);
    }
    protected async Task ExcluirConfiguracoes()
    {
        var resposta = await MetodosEstaticos.Mensagem($"{excluir} {configuracoes}?\n(todas as {configuracoes} retornarão ao padrão)", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (resposta != MessageBoxResult.Yes) return;

        this.Preferencias = Preferencias.PreferenciasPadroes();
        Manipulador.SalvarPreferencias(this.Preferencias);
        StateHasChanged();
        await MetodosEstaticos.Mensagem($"{configuracoes} excluídas com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        
    }
    protected async Task ExcluirHistoricoPonto()
    {
        var resposta = await MetodosEstaticos.Mensagem($"{excluir} {historicoPonto}?{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (resposta != MessageBoxResult.Yes) return;
               
    }
    protected async Task ExcluirHistoricoTempo()
    {
        var resposta = await MetodosEstaticos.Mensagem($"{excluir} {historicoTempo}?{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (resposta != MessageBoxResult.Yes) return;
               
    }
    protected async Task ExcluirCronometrosAbertos()
    {
        var resposta = await MetodosEstaticos.Mensagem($"{excluir} {cronometrosAbertos}?{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (resposta != MessageBoxResult.Yes) return;
               
    }
    protected async Task ExcluirCadastrosContadores()
    {
        var resposta = await MetodosEstaticos.Mensagem($"{excluir} {cadastrosContadores}?\nIsso excluirá também:\n{cadastrosPadroes}\n{cadastrosExpressos}{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (resposta != MessageBoxResult.Yes) return;
               
    }
    protected async Task ExcluirCadastrosPadroes()
    {
        var resposta = await MetodosEstaticos.Mensagem($"{excluir} {cadastrosPadroes}?{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (resposta != MessageBoxResult.Yes) return;
               
    }
    protected async Task ExcluirCadastrosExpressos()
    {
        var resposta = await MetodosEstaticos.Mensagem($"{excluir} {cadastrosExpressos}?{alertaPadrao}", tituloPadrao, MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (resposta != MessageBoxResult.Yes) return;
               
    }

    #endregion
}