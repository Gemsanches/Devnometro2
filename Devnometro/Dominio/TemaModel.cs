using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MudBlazor.Colors;

namespace Devnometro.Dominio;

public static class TemaModel
{
    public static MudTheme TemaPadrao() => new()
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

    public static MudTheme TemaTeal() => new()
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
}

public class TemaPersonalizado
{
    #region Cores
    public string PrimaryLight { get; set; } = null!;
    public string PrimaryDark { get; set; } = null!;
    public string SecondaryLight { get; set; } = null!;
    public string SecondaryDark { get; set; } = null!;
    public string TertiaryLight { get; set; } = null!;
    public string TertiaryDark { get; set; } = null!;
    public string InfoLight { get; set; } = null!;
    public string InfoDark { get; set; } = null!;
    public string WarningLight { get; set; } = null!;
    public string WarningDark { get; set; } = null!;
    public string ErrorLight { get; set; } = null!;
    public string ErrorDark { get; set; } = null!;
    public string SuccessLight { get; set; } = null!;
    public string SuccessDark { get; set; } = null!;
    public string SurfaceLight { get; set; } = null!;
    public string SurfaceDark { get; set; } = null!;
    #endregion

    public MudTheme Tema { get; private set; } = new();

    public TemaPersonalizado() => RetornarAoPadrao();
    public void RetornarAoPadrao()
    {
        PrimaryLight = (string)Tema.PaletteLight.Primary;
        PrimaryDark = (string)Tema.PaletteDark.Primary;
        SecondaryLight = (string)Tema.PaletteLight.Secondary;
        SecondaryDark = (string)Tema.PaletteDark.Secondary;
        TertiaryLight = (string)Tema.PaletteLight.Tertiary;
        TertiaryDark = (string)Tema.PaletteDark.Tertiary;
        InfoLight = (string)Tema.PaletteLight.Info;
        InfoDark = (string)Tema.PaletteDark.Info;
        WarningLight = (string)Tema.PaletteLight.Warning;
        WarningDark = (string)Tema.PaletteDark.Warning;
        ErrorLight = (string)Tema.PaletteLight.Error;
        ErrorDark = (string)Tema.PaletteDark.Error;
        SuccessLight = (string)Tema.PaletteLight.Success;
        SuccessDark = (string)Tema.PaletteDark.Success;
        SurfaceLight = (string)Tema.PaletteLight.Surface;
        SurfaceDark = (string)Tema.PaletteDark.Surface;
    }
    public void AplicarCoresAoTema()
    {
        Tema.PaletteLight.Primary = PrimaryLight;
        Tema.PaletteDark.Primary = PrimaryDark;
        Tema.PaletteLight.Secondary = SecondaryLight;
        Tema.PaletteDark.Secondary = SecondaryDark;
        Tema.PaletteLight.Tertiary = TertiaryLight;
        Tema.PaletteDark.Tertiary = TertiaryDark;
        Tema.PaletteLight.Info = InfoLight;
        Tema.PaletteDark.Info = InfoDark;
        Tema.PaletteLight.Warning = WarningLight;
        Tema.PaletteDark.Warning = WarningDark;
        Tema.PaletteLight.Error = ErrorLight;
        Tema.PaletteDark.Error = ErrorDark;
        Tema.PaletteLight.Success = SuccessLight;
        Tema.PaletteDark.Success = SuccessDark;
        Tema.PaletteLight.Surface = SurfaceLight;
        Tema.PaletteDark.Surface = SurfaceDark;
    }

    public static TemaPersonalizado CriarTemaPersonalizado() => new()
    {
        PrimaryLight = "#bcbcbc",
        PrimaryDark = "#bcbcbc",
        SecondaryLight = "#bcbcbc",
        SecondaryDark = "#bcbcbc",
        TertiaryLight = "#bcbcbc",
        TertiaryDark = "#bcbcbc",
        InfoLight = "#bcbcbc",
        InfoDark = "#bcbcbc",
        WarningLight = "#bcbcbc",
        WarningDark = "#bcbcbc",
        ErrorLight = "#bcbcbc",
        ErrorDark = "#bcbcbc",
        SuccessLight = "#bcbcbc",
        SuccessDark = "#bcbcbc",
        SurfaceLight = "#bcbcbc",
        SurfaceDark = "#bcbcbc"
    };
}
