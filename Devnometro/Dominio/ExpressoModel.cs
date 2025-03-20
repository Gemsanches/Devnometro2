using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

    public Guid Id { get; set; } = new(); 
    public string Nome { get => _nome; set { _nome = string.IsNullOrEmpty(value) ? Contador.Nome : value; } }
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
}
