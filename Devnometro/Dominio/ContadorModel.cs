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
            Icone = MudBlazor.Icons.Material.Filled.QuestionMark;
        }
    }
    
    public Guid Id { get; set; } = new();
    public int Seq { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Icone { get; set; } = string.Empty;
    public MudBlazor.Color CorIcone { get; set; } = MudBlazor.Color.Dark;
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
}