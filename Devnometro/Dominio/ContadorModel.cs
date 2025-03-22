using Devnometro.Dominio.Enumeradores;
using System.Text.Json.Serialization;

namespace Devnometro.Dominio;

public class ContadorModel
{
    public ContadorModel() { }
    public ContadorModel(bool mocar)
    {
        if (mocar)
        {
            Nome = "Novo contador";
            Descricao = "Explique a atividade que esse contador irá monitorar";
        }
    }
    public void Update(ContadorModel that)
    {
        this.Id = that.Id;
        this.Seq = that.Seq;
        this.Padrao = that.Padrao;

        this.Nome = that.Nome;
        this.Icone = that.Icone;
        this.IconeCor = that.IconeCor;
        this.Descricao = that.Descricao;
        this.ContaTempo = that.ContaTempo;
    }

    public Guid Id { get; set; } = new();
    public bool Padrao { get; set; } = false;
    public int Seq { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public EIcone Icone { get; set; } = EIcone.QuestionMark;
    public MudBlazor.Color IconeCor { get; set; } = MudBlazor.Color.Dark;
    public bool ContaTempo { get; set; } = true;
    [JsonIgnore] public string ContaTempoString { get => ContaTempo ? "Sim" : "Não"; }
    [JsonIgnore] public bool ConfirmacaoPendente { get; set; } = false;

    //public TimeSpan DeltaT { get; set; }
    //public DateTime Tzero { get; set; }

    //[JsonIgnore] public bool EmAtividade { get; set; }
    //[JsonIgnore] public Timer? Timer { get; set; }

    public static List<ContadorModel> ListaMocada() =>
        [
            ContadoresPreCadastrados.Requerimento,
            ContadoresPreCadastrados.Desenho,
            ContadoresPreCadastrados.Desenvolvimento,
            ContadoresPreCadastrados.Testes,
            ContadoresPreCadastrados.Documentacao,
            ContadoresPreCadastrados.Implantacao,

            ContadoresPreCadastrados.Aguardando,
            ContadoresPreCadastrados.Aprendendo,
            ContadoresPreCadastrados.AjudandoOutros,
            ContadoresPreCadastrados.TrabalhoParalelo,
            ContadoresPreCadastrados.ChamadoNatureza,
            ContadoresPreCadastrados.OutrasInterrupcoes,

            ContadoresPreCadastrados.Daily,
            ContadoresPreCadastrados.AtualizandoBanco,
            ContadoresPreCadastrados.SubindoVersao,
        ];

    public override string ToString() => this.Nome;
    public EIcone? PadraoIcone()
    {
        var padrao = ListaMocada().FirstOrDefault(x => x.Id == this.Id);
        
        if (padrao is null) return null;

        return padrao.Icone;
    }
    public MudBlazor.Color? PadraoCor()
    {
        var padrao = ListaMocada().FirstOrDefault(x => x.Id == this.Id);
        
        if (padrao is null) return null;

        return padrao.IconeCor;
    }
    public string? PadraoNome()
    {
        var padrao = ListaMocada().FirstOrDefault(x => x.Id == this.Id);
        
        if (padrao is null) return null;

        return padrao.Nome;
    }
    public bool EhEspacador() => Padrao && string.IsNullOrEmpty(Nome);
}