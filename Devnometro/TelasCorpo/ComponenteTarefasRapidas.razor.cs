using Devnometro.Dominio;
using Microsoft.AspNetCore.Components;

namespace Devnometro.TelasCorpo;

public class ComponenteTarefasRapidasBase : ComponentBase
{
    [Inject] public required Preferencias Preferencias { get; set; }

    protected bool MostrarDrawer { get; set; }
}
