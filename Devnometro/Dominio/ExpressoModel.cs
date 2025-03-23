using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static MudBlazor.Colors;

namespace Devnometro.Dominio;
public class ExpressoModel
{
    public ExpressoModel() {}
    public ExpressoModel(bool mocar)
    {
        if (mocar)
        {
            Nome = "Novo Cronômetro Expresso";
            Contador = ContadoresPreCadastrados.TrabalhoParalelo;
        }
    }
    public void Update(ExpressoModel that)
    {
        this.Id = that.Id;
        this.Padrao = that.Padrao;

        this.Nome = that.Nome;
        this.Contador.Update(that.Contador);
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public bool Padrao { get; set; } = false;
    public string Nome { get => string.IsNullOrEmpty(_nome) ? Contador.Nome : _nome;
                         set { _nome = string.IsNullOrEmpty(value) ? Contador.Nome : value; } }
    private string _nome = "";
    public ContadorModel Contador { get; set; } = new();
    [JsonIgnore] public bool ConfirmacaoPendente { get; set; } = false;

    public static List<ExpressoModel> ListaMocada() =>
        [
            ExpressosPreCadastrados.Daily,
            ExpressosPreCadastrados.AtualizandoBanco,
            ExpressosPreCadastrados.SubindoVersao,
            ExpressosPreCadastrados.AjudandoOutros,
            ExpressosPreCadastrados.ChamadoNatureza,
            ExpressosPreCadastrados.TrabalhoParalelo,
        ];

    public override string ToString() => this.Nome;
}
