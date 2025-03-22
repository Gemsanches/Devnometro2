using System.Text.Json.Serialization;
using System.Windows.Media.TextFormatting;

namespace Devnometro.Dominio;

public class PadraoDeCronometroModel
{
    public PadraoDeCronometroModel() {}
    public PadraoDeCronometroModel(bool mocar)
    {
        if (mocar)
        {
            Nome = "Novo padrão de cronômetro";
        }
    }
    public void Update(PadraoDeCronometroModel that)
    {
        this.Id = that.Id;
        this.Padrao = that.Padrao;
        this.Ativo = that.Ativo;

        this.Nome = that.Nome;
        this.Descricao = that.Descricao;
        this.IndicePlayPadrao = that.IndicePlayPadrao;
        this.IncidePausePadrao = that.IncidePausePadrao;

        this.TransferirContadores(that.Contadores);
    }

    public Guid Id { get; set; } = new();
    public bool Padrao { get; set; } = false;
    public bool Ativo { get; set; } = true;
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public List<ContadorModel> Contadores { get; set; } = [];
    public int IndicePlayPadrao { get; set; }
    public int? IncidePausePadrao { get; set; }
    [JsonIgnore] public bool ConfirmacaoPendente { get; set; } = false;

    //public int? IncideEmAtividade { get; set; }

    //public string Chamado { get; set; } = string.Empty;
    //public string Backlog { get; set; } = string.Empty;
    //public string Observacao { get; set; } = string.Empty;
    //public DateTime DataAbertura { get; set; } = DateTime.MinValue;
    //public DateTime? DataConclusao { get; set; }
    //
    //public Timer? TimerTotal { get; set; }
    //public TimeSpan DeltaTTotal { get; set; }
    //public DateTime TzeroTotal { get; set; }

    //public bool JahIniado()
    //{
    //    return (DeltaTTotal > TimeSpan.Zero
    //         || Chamado != string.Empty
    //         || Backlog != string.Empty);
    //}

    //public void Play(int? indice = null)
    //{
    //    if (indice < 0 || indice > Contadores.Length) return;
    //    if (indice == IncideEmAtividade) return;
    //
    //    for (int c = 0; c < Contadores.Length; c++)
    //            Contadores[c].EmAtividade = c == indice.GetValueOrDefault(IndicePlayPadrao);
    //    IncideEmAtividade = IndicePlayPadrao;
    //}
    //public void Pausa(bool PausaTudo = false)
    //{
    //    for (int c = 0; c < Contadores.Length; c++)
    //        Contadores[c].EmAtividade = !PausaTudo && c == IncidePausePadrao.GetValueOrDefault(-1);
    //    IncideEmAtividade = IncidePausePadrao;
    //}

    public static List<PadraoDeCronometroModel> ListaMocada()
    {
        List<PadraoDeCronometroModel> retorno = [
            PadroesCronometroPreCadastrados.Desenvolvedor,
            PadroesCronometroPreCadastrados.Revisor,
            PadroesCronometroPreCadastrados.Homologador,
            PadroesCronometroPreCadastrados.Suporte
        ];

        foreach (PadraoDeCronometroModel padrao in retorno)
        {   
            for (int i = 0; i < padrao.Contadores.Count; i++)
            {
                padrao.Contadores[i].Seq = i + 1; 
            }
        }
        return retorno;
    }
    public override string ToString() => this.Nome;
    public void TransferirContadores(List<ContadorModel> contadores)
    {
        this.Contadores = [];
        foreach (var item in contadores)
        {
            var contador = new ContadorModel();
            contador.Update(item);
            this.Contadores.Add(contador);
        }
    }
    public bool PossuiMesmosContadores(List<ContadorModel> contadores)
    {
        if ((this.Contadores?.Count ?? 0) != (contadores?.Count ?? 0)) return false;
        if ((this.Contadores?.Count ?? 0) == 0) return true;

        for (int i = 0; i < (Contadores?.Count ?? 0); i++)
            if ((contadores?[i].Id) != this.Contadores?[i].Id
             || (contadores?[i].Seq) != this.Contadores?[i].Seq
             || (contadores?[i].Nome) != this.Contadores?[i].Nome
             || (contadores?[i].Icone) != this.Contadores?[i].Icone
             || (contadores?[i].IconeCor) != this.Contadores?[i].IconeCor
             || (contadores?[i].Descricao) != this.Contadores?[i].Descricao)
                return false;
        
        return true;
    }
}
