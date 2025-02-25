using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro.Dominio;

public class Preferencias
{
    public event Action OnChanged;

    #region Caminhos

    #endregion

    #region Tema
    private MudTheme _tema = new();
    public MudTheme Tema { get => _tema; set { _tema = value; OnChanged?.Invoke(); } }
    
    private bool _temaNoturno;
    public bool TemaNoturno { get => _temaNoturno; set { _temaNoturno = value; OnChanged?.Invoke(); } }

    public TemaPersonalizado TemaPersonalizado { get; set; } = new();
    #endregion

}


