using Devnometro.Dominio.Enumeradores;
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
    public string? Nome { get; set; }
    public MudBlazor.Color Cor { get; set; } = MudBlazor.Color.Dark;
    public MudBlazor.Color? CorPadrao { get; set; }
    public EIcone Icone { get; set; }
    public EIcone? IconePadrao { get; set; }
    #endregion

    #region Construtores
    public IconeDados() {}

    public IconeDados(Color cor, EIcone icone, string? nome = null, Color? corPadrao = null, EIcone? iconePadrao = null)
    {
        Nome = nome;
        Cor = cor;
        CorPadrao = corPadrao;
        Icone = icone;
        IconePadrao = iconePadrao;
    }
    #endregion
}
