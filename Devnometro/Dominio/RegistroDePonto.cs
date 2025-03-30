using Devnometro.Dominio.Enumeradores;
using System.Text.Json.Serialization;

namespace Devnometro.Dominio;

public class RegistroDePonto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public StatusBatida Status { get; set; }
    public TipoBatida Tipo { get; set; }
    public int Turno { get; set; }
    public DateTime Horario { get; set; }

    public TipoBatida? TipoOriginal { get; set; } = null;
    public int? TurnoOriginal { get; set; }
    public DateTime? HorarioOriginal { get; set; } = null;
    public string? Explicacao { get; set; } = null;

    [JsonIgnore]
    public string TipoAbreviado
    {
        get => Tipo switch
        {
            TipoBatida.Entrada => "E",
            TipoBatida.Saida => "S",
            TipoBatida.Duplicada => "D",
            _ => "?",
        };
    }

    public RegistroDePonto Clone() => new()
    {
        Horario = this.Horario,
        Tipo = this.Tipo,
        Turno = this.Turno,
        Status = this.Status,
    };

    public bool Equivale(RegistroDePonto that) =>
        this.Turno == that.Turno
        && this.Horario.Year == that.Horario.Year
        && this.Horario.Month == that.Horario.Month
        && this.Horario.Day == that.Horario.Day
        && this.Horario.Hour == that.Horario.Hour
        && this.Horario.Minute == that.Horario.Minute
        && this.Horario.Second == that.Horario.Second
        && this.Tipo == that.Tipo;

    public void Atualiza(RegistroDePonto that)
    {
        this.Status = StatusBatida.Editada;
        this.TipoOriginal ??= this.Tipo;
        this.TurnoOriginal ??= this.Turno;
        this.HorarioOriginal ??= this.Horario;
        this.Explicacao += $"{DateTime.Now:yyyy/MM/dd HH:mm:ss} = {that.Explicacao ?? "[explicação não fornecida]"}\n";
        this.Tipo = that.Tipo;
        this.Turno = that.Turno;
        this.Horario = that.Horario;
    }
}
