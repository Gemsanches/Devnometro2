using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro.Dominio;

public class PadraoDeCronometroModel
{
    public string Chamado { get; set; } = string.Empty;
    public string Backlog { get; set; } = string.Empty;
    public string Observacao { get; set; } = string.Empty;
    public DateTime DataAbertura { get; set; } = DateTime.MinValue;
    public DateTime? DataConclusao { get; set; }

    public Timer? TimerTotal { get; set; }
    public TimeSpan DeltaTTotal { get; set; }
    public DateTime TzeroTotal { get; set; }
    public ContadorModel[] Contadores { get; set; } = [];
    
    public int IndicePlayPadrao { get; set; }
    public int? IncidePausePadrao { get; set; }
    public int? IncideEmAtividade { get; set; }

    public bool JahIniado()
    {
        return (DeltaTTotal > TimeSpan.Zero
             || Chamado != string.Empty
             || Backlog != string.Empty);
    }

    public void Play(int? indice = null)
    {
        if (indice < 0 || indice > Contadores.Length) return;
        if (indice == IncideEmAtividade) return;

        for (int c = 0; c < Contadores.Length; c++)
                Contadores[c].EmAtividade = c == indice.GetValueOrDefault(IndicePlayPadrao);
        IncideEmAtividade = IndicePlayPadrao;
    }
    public void Pausa(bool PausaTudo = false)
    {
        for (int c = 0; c < Contadores.Length; c++)
            Contadores[c].EmAtividade = !PausaTudo && c == IncidePausePadrao.GetValueOrDefault(-1);
        IncideEmAtividade = IncidePausePadrao;
    }

    public static List<PadraoDeCronometroModel> ListaMocada() =>
        [];
}
