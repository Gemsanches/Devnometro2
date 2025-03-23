using Devnometro.Dominio;
using Microsoft.AspNetCore.Components;
using System.Timers;

namespace Devnometro.TelasCorpo;

public class ComponentePontoBase : ComponentBase, IDisposable
{
    [Inject] public required Preferencias Preferencias { get; set; }

    private System.Timers.Timer? timer;
    protected string horaAtual = "";

    private int nroRegistrosValidoDia = 0;
    protected string ProximoRegistro { get; set; } = "";
    
    protected bool MostrarDrawer { get; set; }

    protected override void OnInitialized()
    {
        AtualizarTextoProximoRegistro();
        AtualizarHora();
        timer = new System.Timers.Timer(1000);
        timer.Elapsed += (sender, args) => AtualizarHora();
        timer.AutoReset = true;
        timer.Enabled = true;
    }
    public void Dispose()
    {
        timer?.Dispose();
    }

    private void AtualizarHora()
    {
        horaAtual = DateTime.Now.ToString("HH:mm:ss");
        InvokeAsync(StateHasChanged);
    }
    
    protected void Registrar()
    {
        nroRegistrosValidoDia++;
        AtualizarTextoProximoRegistro();
        InvokeAsync(StateHasChanged);
    }
    private void AtualizarTextoProximoRegistro()
    {
        var prefixo = nroRegistrosValidoDia % 2 == 0 ? "Entrada" : "Saída";
        var contagem = ((nroRegistrosValidoDia / 2) + 1).ToString("N0");
        ProximoRegistro = $"{prefixo} {contagem}";
    }
}
