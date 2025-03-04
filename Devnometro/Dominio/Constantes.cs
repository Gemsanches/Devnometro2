namespace Devnometro.Dominio;

public static class PrefereciaPadrao
{
    #region Geral
    public const int Exibicao = 2;
    public const bool LigarJunto = false;
    public const string CaminhoAtendimento = "https://gr3bhelp.freshdesk.com/a/tickets/";
    public const string CaminhoDesenvolvimento = "https://gr3b.visualstudio.com/S.Adm/_workitems/edit/";
    #endregion

    #region Salvamento Automático
    public const int AutoSaveTempo = 1;
    public const bool AutoSave = true;
    public const bool AutoSaveMarcarPonto = true;
    public const bool AutoSaveEditarPonto = true;
    public const bool AutoSaveExcluirPonto = true;
    public const bool AutoSaveCriarCronometro = false;
    public const bool AutoSaveEditarCronometro = true;
    public const bool AutoSaveExcluirCronometro = false;
    public const bool AutoSaveFinalizarCronometro = true;
    public const bool AutoSaveAbandonarCronometro = true;
    #endregion

    #region Comunicação
    public const bool AvisarFecharPrograma = false;
    
    public const bool AvisarIniciarExpotacaoPonto = false;
    public const bool AvisarSucessoExpotacaoPonto = true;
    public const bool AvisarIniciarExpotacaoTempo = false;
    public const bool AvisarSucessoExpotacaoTempo = true;
    
    public const bool AvisarIniciarCriacaoPonto = false;
    public const bool AvisarSucessoCriacaoPonto = false;
    public const bool AvisarIniciarExclusaoPonto = true;
    public const bool AvisarSucessoExclusaoPonto = true;
    public const bool AvisarIniciarEdicaoPonto = false;
    public const bool AvisarSucessoEdicaoPonto = false;
    
    public const bool AvisarIniciarCriacaoCronometro = false;
    public const bool AvisarSucessoCriacaoCronometro = false;
    public const bool AvisarIniciarExclusaoCronometro = false;
    public const bool AvisarSucessoExclusaoCronometro = false;
    public const bool AvisarIniciarEdicaoCronometro = false;
    public const bool AvisarSucessoEdicaoCronometro = false;
    public const bool AvisarIniciarAbandonoCronometro = true;
    public const bool AvisarSucessoAbandonoCronometro = false;
    public const bool AvisarIniciarFinalizacaoCronometro = false;
    public const bool AvisarSucessoFinalizacaoCronometro = false;
    #endregion

    #region Ponto
    public const int PontoTempoBatidaRepetida = 60;
    public const bool PontoPrimeiroAutomatico = false;
    public const bool PontoAutoExportarComecoDoMes = true;
    public const string CaminhoPonto = "";
    #endregion

    #region Cronômetro
    public const int CronometrosSimultaneos = 5;
    public const bool CronometroExcluirAutomaticamente = true;
    public const bool CronometroApenasUmAtivo = true;
    public const string CaminhoCronometro = "";
    #endregion
}

public static class TooltipPadrao
{
    public const int delay = 1000;
    public const int duration = 2000;
    public const MudBlazor.Color corTolltip = MudBlazor.Color.Info;
}
