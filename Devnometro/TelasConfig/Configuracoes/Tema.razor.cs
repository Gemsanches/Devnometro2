using Devnometro.Dominio;
using Devnometro.Dominio.Enumeradores;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Devnometro.TelasConfig.Configuracoes;

public class TemaBase : ComponentBase, IDisposable
{
    [Parameter]
    public MenuWindow? Janela { get; set; }

    [Parameter]
    public required Preferencias Preferencias { get; set; }

    protected override void OnInitialized()
    {
        Preferencias.OnChanged += StateHasChanged;
    }

    public void Dispose()
    {
        Preferencias.OnChanged -= StateHasChanged;
    }
}
