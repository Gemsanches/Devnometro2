using Devnometro.Dominio;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro.TelasConfig.Configuracoes;

public class TemaBase : ComponentBase, IDisposable
{
    [Parameter]
    public MenuWindow? Janela { get; set; }

    [Parameter]
    public required Preferencias Preferencias { get; set; }

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
                    TemaSelecionado = TemaModel.TemaTeal();
                    break;
                case 4:
                    TemaSelecionado = TemaPersonalizado;
                    break;
                default:
                    TemaSelecionado = TemaModel.TemaPadrao();
                    break;
            }
            Preferencias.Tema = TemaSelecionado;
        }
    }

    protected MudTheme TemaSelecionado { get; set; } = new();
    protected MudTheme TemaPersonalizado { get; set; } = new();

    #region Tema Personalizado
    protected TemaPersonalizado Personalizado { get; set; } = new();
    protected  void SalvarPersonalizado()
    {
        Personalizado.AplicarCoresAoTema();
    }
    protected void ResetarPersonalizado()
    {
        Personalizado.RetornarAoPadrao();
    }

    #endregion

    protected override void OnInitialized()
    {
        TemaPersonalizado = Personalizado.Tema;
        Preferencias.OnChanged += StateHasChanged;

        Selecionado = 1;
    }

    public void Dispose()
    {
        Preferencias.OnChanged -= StateHasChanged;
    }
}
