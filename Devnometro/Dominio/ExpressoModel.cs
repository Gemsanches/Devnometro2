using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    public string Nome { get; set; } = "";
    public ContadorModel Contador { get; set; } = new();

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
