using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro.Dominio;

public class IconeDados
{
    #region Propriedades
    public string? Descricao { get; set; }
    public MudBlazor.Color Cor { get; set; } = MudBlazor.Color.Dark;
    public string IconeAtual { get; set; } = "";
    public string? IconePadrao { get; set; }

    public bool SemPadrao { get => string.IsNullOrEmpty(IconePadrao); }
    #endregion

    #region Construtores
    public IconeDados() {}

    public IconeDados(Color cor, string iconeAtual, string? descricao = null, string? iconePadrao = null)
    {
        Descricao = descricao;
        Cor = cor;
        IconeAtual = iconeAtual;
        IconePadrao = iconePadrao;
    }
    #endregion
}
