using Devnometro.Dominio;
using Microsoft.AspNetCore.Components;

namespace Devnometro.TelasCorpo;

public class ComponenteAndamentoBase : ComponentBase
{
    [Parameter] public required Preferencias Preferencias { get; set; }

    protected bool MostrarDrawer { get; set; }

}
