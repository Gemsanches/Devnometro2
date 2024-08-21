using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro.Configuracoes;

public class TemaBase : ComponentBase
{
    [Parameter]
    public Configuracao? janela { get; set; }
    
    protected int _selecionado;
    protected int Selecionado
    {
        get => _selecionado;
        set
        {
            _selecionado = value;
            switch (value)
            {
                case 2:
                    TemaSelecionado = new();
                    break;
                case 3:
                    TemaSelecionado = TemaTeal;
                    break;
                case 4:
                    TemaSelecionado = TemaPersonalizado;
                    break;
                default:
                    TemaSelecionado = TemaPadrao;
                    break;
            }
        }
    }
    protected bool temaEscuro;

    protected MudTheme TemaSelecionado = new();

    protected MudTheme TemaPadrao = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#6e7a73",
            Secondary = "#bfb5b2",
            Tertiary = "#dab600",
            Info = "#54afcd",
            Warning = "#FF9800",
            Error = "#bf0000",
            Success = "#77ab59",
            Surface = "#F3F3FF"
        },
        PaletteDark = new PaletteDark()
        {
            Primary = "#2e4045",
            Secondary = "#5e3c58",
            Tertiary = "#a98600",
            Info = "#359aca",
            Warning = "#ffa800",
            Error = "#800000",
            Success = "#36802d",
            Surface = "#838996"
        }
    };

    protected MudTheme TemaTeal = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#006666",
            Secondary = "#008080",
            Tertiary = "#66b2b2",
            Info = "#b2d8d8",
            Warning = "#949406",
            Error = "#945306",
            Success = "#09DE4C",
            Surface = "#F3FFFF"
        },
        PaletteDark = new PaletteDark()
        {
            Primary = "#004c4c",
            Secondary = "#006666",
            Tertiary = "#008080",
            Info = "#66b2b2",
            Warning = "#949406",
            Error = "#942206",
            Success = "#07A872",
            Surface = "#718E8B"
        }
    };

    #region Cores do tema personalizado
    protected string primaryLight = "#6e7a73";
    protected string primaryDark = "#2e4045";
    protected string secondaryLight = "#bfb5b2";
    protected string secondaryDark = "#5e3c58";
    protected string tertiaryLight = "#dab600";
    protected string tertiaryDark = "#a98600";
    protected string infoLight = "#54afcd";
    protected string infoDark = "#359aca";
    protected string warningLight = "#FF9800";
    protected string warningDark = "#ffa800";
    protected string errorLight = "#bf0000";
    protected string errorDark = "#800000";
    protected string successLight = "#77ab59";
    protected string successDark = "#36802d";
    protected string surfaceLight = "#F3F3FF";
    protected string surfaceDark = "#838996";
    #endregion

    protected MudTheme TemaPersonalizado = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#6e7a73",
            Secondary = "#bfb5b2",
            Tertiary = "#dab600",
            Info = "#54afcd",
            Warning = "#FF9800",
            Error = "#bf0000",
            Success = "#77ab59",
            Surface = "#F3F3FF"
        },
        PaletteDark = new PaletteDark()
        {
            Primary = "#2e4045",
            Secondary = "#5e3c58",
            Tertiary = "#a98600",
            Info = "#359aca",
            Warning = "#ffa800",
            Error = "#800000",
            Success = "#36802d",
            Surface = "#838996"
        }
    };

    protected void AplicarCoresAoTema()
    {
        TemaPersonalizado.PaletteLight.Primary = primaryLight;
        TemaPersonalizado.PaletteDark.Primary = primaryDark;
        TemaPersonalizado.PaletteLight.Secondary = secondaryLight;
        TemaPersonalizado.PaletteDark.Secondary = secondaryDark;
        TemaPersonalizado.PaletteLight.Tertiary = tertiaryLight;
        TemaPersonalizado.PaletteDark.Tertiary = tertiaryDark;
        TemaPersonalizado.PaletteLight.Info = infoLight;
        TemaPersonalizado.PaletteDark.Info = infoDark;
        TemaPersonalizado.PaletteLight.Warning = warningLight;
        TemaPersonalizado.PaletteDark.Warning = warningDark;
        TemaPersonalizado.PaletteLight.Error = errorLight;
        TemaPersonalizado.PaletteDark.Error = errorDark;
        TemaPersonalizado.PaletteLight.Success = successLight;
        TemaPersonalizado.PaletteDark.Success = successDark;
        TemaPersonalizado.PaletteLight.Surface = surfaceLight;
        TemaPersonalizado.PaletteDark.Surface = surfaceDark;
    }
}
