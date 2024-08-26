using Devnometro.Dominio;
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

    [Inject]
    public MudTheme _theme { get; set; }

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
            _theme = TemaSelecionado;
        }
    }

    protected bool temaEscuro;

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
        
        temaEscuro = false;
        Selecionado = 1;

        base.OnInitialized();
    }
}
