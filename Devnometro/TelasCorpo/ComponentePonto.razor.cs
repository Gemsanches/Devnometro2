using Devnometro.Aplicacao;
using Devnometro.Dominio;
using Devnometro.Dominio.Enumeradores;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Timers;

namespace Devnometro.TelasCorpo;

public class ComponentePontoBase : ComponentBase, IDisposable
{
    #region Propriedades
    [Parameter] public required Preferencias Preferencias { get; set; }
    private readonly ManipuladorDeArquivo manipulador = new();
    protected bool MostrarDrawer { get => _mostrarDrawer; set { _mostrarDrawer = value; if (abreEdicao && !value) abreEdicao = false; } }
    private bool _mostrarDrawer = false;
    protected bool mostrarDetalhesLegais = false;
    protected bool mostrarDetalhesSobre = false;
    private List<RegistroDePonto> Model { get; set; } = [];
    protected List<RegistroDePonto> PontosDeHoje { get; set; } = [];

    private System.Timers.Timer? timer;
    protected string horaAtual = "";

    private System.Timers.Timer? countdownTimer;
    protected int tempoRemanescente;

    protected string ProximoRegistro { get; set; } = "";
    protected bool proximoRegistroEhEntrada = true;
    private bool batidaDuplicada = false;
    private int nroRegistrosValidoDia = 0;
    private int nroTurnoAtivo = 1;
    #endregion

    #region Inicalização
    protected override void OnInitialized()
    {
        AtualizarHora();
        InicializarRelogio();
        CarregarHistoricoDePontos();
        AtualizarTextoProximoRegistro();

        //nsr = sequencia.ToString().PadLeft(9, '0');
        //dataHora = DateTime.Now.ToString("ddMMyyyyHHmm");
        //linha = $"{nsr}{tipoRegistro}{dataHora}{pis}";
    }
    private void AtualizarHora()
    {
        horaAtual = DateTime.Now.ToString("HH:mm:ss");
        InvokeAsync(StateHasChanged);
    }
    private void AtualizarTextoProximoRegistro()
    {
        proximoRegistroEhEntrada = nroRegistrosValidoDia % 2 == 0;
        nroTurnoAtivo = (nroRegistrosValidoDia / 2) + 1;

        var prefixo = proximoRegistroEhEntrada ? "Entrada" : "Saída";
        var contagem = nroTurnoAtivo.ToString("N0");

        ProximoRegistro = $"{prefixo} {contagem}";
    }
    private void InicializarRelogio()
    {
        timer = new System.Timers.Timer(1000);
        timer.Elapsed += (sender, args) => AtualizarHora();
        timer.AutoReset = true;
        timer.Enabled = true;
    }
    private void CarregarHistoricoDePontos()
    {
        Model = ManipuladorDeArquivo.CarregarPontos();
        Model ??= [];
        AtualizaPontosDoDia();
    }
    private void AtualizaPontosDoDia()
    {
        PontosDeHoje = Model.Where(x => x.Horario.Date == DateTime.Today && x.Status != StatusBatida.Excluida).ToList();
        PontosDeHoje ??= [];
        nroRegistrosValidoDia = PontosDeHoje.Where(x => (x.Tipo & TipoBatida.Valida) != 0).Count();
    }
    #endregion

    #region Implementando a interface
    public void Dispose()
    {
        timer?.Dispose();
        countdownTimer?.Dispose();
    }
    #endregion

    #region Registrar ponto
    protected async Task Registrar()
    {
        registrando = true;
        if (Preferencias.Comunicacao.AvisarIniciarCriacaoPonto
             && System.Windows.MessageBoxResult.Yes !=
                await MetodosEstaticos.MensagemAsync("Registrar batida de ponto?"
                                                    , "Confirmar Operação"
                                                    , System.Windows.MessageBoxButton.YesNo
                                                    , System.Windows.MessageBoxImage.Question))
        {
            registrando = false;
            return;
        }
        batidaDuplicada = true;
        IniciarContagemRegressiva(Preferencias.Ponto.TempoBatidaRepetida);
        await AdicionaPontoAoRegistro();
        AtualizarTextoProximoRegistro();
        await InvokeAsync(StateHasChanged);
        registrando = false;
    }
    private bool registrando = false;
    protected bool BloquearRegistro { get => registrando || batidaDuplicada; }

    private void IniciarContagemRegressiva(int segundos)
    {
        tempoRemanescente = segundos;
        countdownTimer = new System.Timers.Timer(1000);
        countdownTimer.Elapsed += (sender, args) => ContagemRegressiva(sender, args);
        countdownTimer.AutoReset = true;
        countdownTimer.Enabled = true;
    }
    private void ContagemRegressiva(object? sender, System.Timers.ElapsedEventArgs e)
    {
        tempoRemanescente--;

        if (tempoRemanescente <= 0 && countdownTimer != null)
        {
            countdownTimer.Stop();
            countdownTimer.Dispose();
            batidaDuplicada = false;
        }
    }
    private async Task AdicionaPontoAoRegistro()
    {
        Model.Add(new()
        {
            Tipo = proximoRegistroEhEntrada
                 ? TipoBatida.Entrada
                 : TipoBatida.Saida,
            Status = StatusBatida.Registrada,
            Turno = nroTurnoAtivo,
            Horario = DateTime.Now
        });
        AtualizaPontosDoDia();

        if (Preferencias.AutoSave.MarcarPonto)
        {
            await manipulador.SalvarPontos(Model);

            if (Preferencias.Comunicacao.AvisarSucessoCriacaoPonto)
                await MetodosEstaticos.MensagemAsync("Ponto registrado com sucesso?"
                                                    , "Operação bem sucedida"
                                                    , System.Windows.MessageBoxButton.OK
                                                    , System.Windows.MessageBoxImage.Information);
        }
    }
    #endregion

    #region Editar Registro
    protected bool abreEdicao;
    protected bool semExplicacao = true;
    protected RegistroDePonto registroEdicao = new();
    protected RegistroDePonto? registroReferencia;
    protected TimeSpan? horarioEdicao;
    protected void EditarRegistro(RegistroDePonto registro)
    {
        registroEdicao = registro.Clone();
        registroReferencia = registro;
        horarioEdicao = new(registro.Horario.Hour, registro.Horario.Minute, registro.Horario.Second);
        abreEdicao = true;
    }
    protected void CancelaEdicao() => abreEdicao = false;
    protected void SalvaEdicao() 
    {
        if (registroReferencia == null)
        { abreEdicao = false; return; }

        if(horarioEdicao.HasValue)
            registroEdicao.Horario = new(registroEdicao.Horario.Year, registroEdicao.Horario.Month, registroEdicao.Horario.Day,
                                         horarioEdicao.Value.Hours, horarioEdicao.Value.Minutes, horarioEdicao.Value.Seconds);

        if (registroEdicao.Equivale(registroReferencia) && !marcadoParaExclusao)
        { abreEdicao = false; return; }

        registroReferencia.Atualiza(registroEdicao);
        if (marcadoParaExclusao)
            ExcluirRegistro();
        AtualizaPontosDoDia();
        registroEdicao = new();
        registroReferencia = null;
        abreEdicao = false; 
    }

    protected int index = 0;
    protected string[] icons = { Icons.Material.Filled.SouthEast, Icons.Material.Filled.NorthEast, Icons.Material.Filled.Shuffle };
    protected void CycleIcons() 
    {
        index = (index + 1) % 3;
        if (index == 0)
            registroEdicao.Tipo = TipoBatida.Entrada;
        else if (index == 1)
            registroEdicao.Tipo = TipoBatida.Saida;
        else if (index == 2)
            registroEdicao.Tipo = TipoBatida.Duplicada;
    }

    protected void ValidaExplicacao() => semExplicacao = string.IsNullOrWhiteSpace(registroEdicao.Explicacao);

    protected void ExcluirRegistro()
    {
        if (registroReferencia == null) return;
        registroReferencia.Status = StatusBatida.Excluida;
        marcadoParaExclusao = false;
    }
    protected bool marcadoParaExclusao;
    #endregion

    #region AFD
    //private int sequencia = 1;
    //private string nsr = "000000000";// sequencia.ToString().PadLeft(9, '0');
    //private string tipoRegistro = "3";
    //private string dataHora = "ddMMyyyyHHmm"; //DateTime.Now.ToString("ddMMyyyyHHmm");
    //private string pis = "000000000000";
    //protected string linha = "";
    #endregion
}
