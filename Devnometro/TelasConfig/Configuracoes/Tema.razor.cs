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
    [Inject] public required Aplicacao.ManipuladorDeArquivo Manipulador { get; set; }
    [Parameter] public MenuWindow? Janela { get; set; }
    [Parameter] public required Preferencias Preferencias { get; set; }

    protected override void OnInitialized()
    {
        Preferencias.OnChanged += StateHasChanged;
    }

    public void Dispose()
    {
        Preferencias.OnChanged -= StateHasChanged;
    }

    protected async Task AplicarTemaImportado() 
    {
        var temaImportado = await Manipulador.ImportarTemaPersonalizadoAsync();
        if (temaImportado is not null)
        {
            this.Preferencias.TemaPersonalizado = temaImportado;
            this.Preferencias.TemaPersonalizado.AplicarCoresAoTema();
            this.Preferencias.TemaSelecionado = ETema.TemaPersonalizado;
        }
    }
}
