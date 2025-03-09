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
    protected void ExcluirTemaPersonalizado()
    {
        var resposta = MessageBox.Show("Excluir Tema Personalizado?", "Tem certeza?", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
            MessageBox.Show("Tema Personalizado excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
            MessageBox.Show("Tema Personalizado não foi excluído!", "Falha", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    #endregion
}