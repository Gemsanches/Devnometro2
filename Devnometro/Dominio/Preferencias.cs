using Devnometro.Dominio.Enumeradores;
using MudBlazor;
using System.Text.Json.Serialization;

namespace Devnometro.Dominio;

public class Preferencias
{
    public event Action? OnChanged;

    #region Tema
    public MudTheme Tema { get => _tema; set { _tema = value; OnChanged?.Invoke(); } }
    private MudTheme _tema = new();
    public bool TemaNoturno { get => _temaNoturno; set { _temaNoturno = value; OnChanged?.Invoke(); } }
    private bool _temaNoturno;
    public ETema TemaSelecionado
    {
        get => _temaSelecionado;
        set
        {
            _temaSelecionado = value;
            Tema = value switch
            {
                ETema.TemaMudBlazor => new(),
                ETema.TemaTeal => TemaModel.TemaTeal(),
                ETema.TemaPersonalizado => TemaPersonalizado.Tema,
                _ => TemaModel.TemaPadrao(),
            };
            OnChanged?.Invoke();
        }
    }
    private ETema _temaSelecionado = ETema.TemaPadrao;
    public TemaPersonalizado TemaPersonalizado { get; set; } = new();
    #endregion

    #region Dados
    public PreferenciasGerais Geral { get; set; } = new();
    public PreferenciasSalvamentoAutomatico AutoSave { get; set; } = new();
    public PreferenciasComunicacao Comunicacao { get; set; } = new();
    public PreferenciasPonto Ponto { get; set; } = new();
    public PreferenciasCronometro Cronometro { get; set; } = new();
    #endregion

    #region Métodos Estáticos
    public static Preferencias PreferenciasPadroes()
    {
        var retorno = new Preferencias()
        {
            TemaSelecionado = ETema.TemaPadrao,
            TemaPersonalizado = Aplicacao.ManipuladorDeArquivo.CarregarTemaPersonalizado(),
        };
        retorno.TemaPersonalizado.AplicarCoresAoTema();
        return retorno;
    }
    #endregion
}

public class PreferenciasGerais
{
    public int Exibicao { get; set; } = PrefereciaPadrao.Exibicao;
    public bool LigarJunto { get; set; } = PrefereciaPadrao.LigarJunto;
    public string CaminhoAtendimento { get; set; } = PrefereciaPadrao.CaminhoAtendimento;
    public string CaminhoDesenvolvimento { get; set; } = PrefereciaPadrao.CaminhoDesenvolvimento;
}

public class PreferenciasSalvamentoAutomatico
{
    public bool Ativo { get; set; } = PrefereciaPadrao.AutoSave;
    public int Tempo { get; set; } = PrefereciaPadrao.AutoSaveTempo;
    public bool MarcarPonto { get; set; } = PrefereciaPadrao.AutoSaveMarcarPonto;
    public bool EditarPonto { get; set; } = PrefereciaPadrao.AutoSaveEditarPonto;
    public bool ExcluirPonto { get; set; } = PrefereciaPadrao.AutoSaveExcluirPonto;
    public bool CriarCronometro { get; set; } = PrefereciaPadrao.AutoSaveCriarCronometro;
    public bool EditarCronometro { get; set; } = PrefereciaPadrao.AutoSaveEditarCronometro;
    public bool ExcluirCronometro { get; set; } = PrefereciaPadrao.AutoSaveExcluirCronometro;
    public bool FinalizarCronometro { get; set; } = PrefereciaPadrao.AutoSaveFinalizarCronometro;
    public bool AbandonarCronometro { get; set; } = PrefereciaPadrao.AutoSaveAbandonarCronometro;
}

public class PreferenciasComunicacao
{
    public bool AvisarFecharPrograma { get; set; } = PrefereciaPadrao.AvisarFecharPrograma;

    #region Exportação
    public bool AvisarIniciarExpotacaoPonto { get; set; } = PrefereciaPadrao.AvisarIniciarExpotacaoPonto;
    public bool AvisarSucessoExpotacaoPonto { get; set; } = PrefereciaPadrao.AvisarSucessoExpotacaoPonto;
    public bool AvisarIniciarExpotacaoTempo { get; set; } = PrefereciaPadrao.AvisarIniciarExpotacaoTempo;
    public bool AvisarSucessoExpotacaoTempo { get; set; } = PrefereciaPadrao.AvisarSucessoExpotacaoTempo;
    #endregion

    #region Ponto
    public bool AvisarIniciarCriacaoPonto { get; set; } = PrefereciaPadrao.AvisarIniciarCriacaoPonto;
    public bool AvisarSucessoCriacaoPonto { get; set; } = PrefereciaPadrao.AvisarSucessoCriacaoPonto;
    public bool AvisarIniciarExclusaoPonto { get; set; } = PrefereciaPadrao.AvisarIniciarExclusaoPonto;
    public bool AvisarSucessoExclusaoPonto { get; set; } = PrefereciaPadrao.AvisarSucessoExclusaoPonto;
    public bool AvisarIniciarEdicaoPonto { get; set; } = PrefereciaPadrao.AvisarIniciarEdicaoPonto;
    public bool AvisarSucessoEdicaoPonto { get; set; } = PrefereciaPadrao.AvisarSucessoEdicaoPonto;
    #endregion

    #region Cronômetro
    public bool AvisarIniciarCriacaoCronometro { get; set; } = PrefereciaPadrao.AvisarIniciarCriacaoCronometro;
    public bool AvisarSucessoCriacaoCronometro { get; set; } = PrefereciaPadrao.AvisarSucessoCriacaoCronometro;
    public bool AvisarIniciarExclusaoCronometro { get; set; } = PrefereciaPadrao.AvisarIniciarExclusaoCronometro;
    public bool AvisarSucessoExclusaoCronometro { get; set; } = PrefereciaPadrao.AvisarSucessoExclusaoCronometro;
    public bool AvisarIniciarEdicaoCronometro { get; set; } = PrefereciaPadrao.AvisarIniciarEdicaoCronometro;
    public bool AvisarSucessoEdicaoCronometro { get; set; } = PrefereciaPadrao.AvisarSucessoEdicaoCronometro;
    public bool AvisarIniciarAbandonoCronometro { get; set; } = PrefereciaPadrao.AvisarIniciarAbandonoCronometro;
    public bool AvisarSucessoAbandonoCronometro { get; set; } = PrefereciaPadrao.AvisarSucessoAbandonoCronometro;
    public bool AvisarIniciarFinalizacaoCronometro { get; set; } = PrefereciaPadrao.AvisarIniciarFinalizacaoCronometro;
    public bool AvisarSucessoFinalizacaoCronometro { get; set; } = PrefereciaPadrao.AvisarSucessoFinalizacaoCronometro;
    #endregion
}

public class PreferenciasPonto
{
    public int TempoBatidaRepetida { get; set; } = PrefereciaPadrao.PontoTempoBatidaRepetida;
    public bool PrimeiroPontoAutomatico { get; set; } = PrefereciaPadrao.PontoPrimeiroAutomatico;
    public bool AutoExportarComecoDoMes { get; set; } = PrefereciaPadrao.PontoAutoExportarComecoDoMes;
    public string Caminho { get; set; } = PrefereciaPadrao.CaminhoPonto;

    #region Simulador de AFD
    public bool AFD_HabilitarSimulacao { get; set; } = false;
    public bool AFD_ConfirmacaoDeCiencia { get; set; } = false;
    public DateTime? AFD_DataHoraUltimaConfirmacaoDeCiencia { get; set; }
    [JsonIgnore] public bool AFD_Habilitado { get => AFD_HabilitarSimulacao && AFD_ConfirmacaoDeCiencia; }
    #endregion
}

public class PreferenciasCronometro
{
    public int CronometrosSimultaneos { get; set; } = PrefereciaPadrao.CronometrosSimultaneos;
    public bool ExcluirAutomaticamente { get; set; } = PrefereciaPadrao.CronometroExcluirAutomaticamente;
    public bool ApenasUmAtivo { get; set; } = PrefereciaPadrao.CronometroApenasUmAtivo;
    public string Caminho { get; set; } = PrefereciaPadrao.CaminhoCronometro;
}