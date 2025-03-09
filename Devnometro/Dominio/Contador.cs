using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Devnometro.Dominio;

public class Contador
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Icone { get; set; } = string.Empty;

    [JsonIgnore]
    public bool EmAtividade { get; set; }
    public bool ContaTempo { get; set; }
    public TimeSpan DeltaT { get; set; }
    public DateTime Tzero { get; set; }

    [JsonIgnore]
    public Timer? Timer { get; set; }
}