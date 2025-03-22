using Devnometro.Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro.Dominio;

public class RegistroDePonto
{
    public Guid Id { get; set; } = new();
    public StatusBatida Status { get; set; }
    public TipoBatida Tipo { get; set; }
    public DateTime Horario { get; set; }
        
    public TipoBatida? TipoOriginal { get; set; } = null;
    public DateTime? HorarioOriginal { get; set; } = null;
    public string? Explicacao { get; set; } = null;
}
