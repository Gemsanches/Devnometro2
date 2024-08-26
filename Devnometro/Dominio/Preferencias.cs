using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro.Dominio;

class Preferencias
{
    #region Caminhos

    #endregion

    #region Tema
    public MudTheme TemaPadrao { get; set; }
    public bool TemaNoturno { get; set; }

    public TemaPersonalizado temaPersonalizado { get; set; } = new();
    #endregion

}


