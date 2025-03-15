using Devnometro.Dominio;
using Devnometro.TelasConfig.Configuracoes;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro;

public class MenuBase : ComponentBase, IDisposable
{
    [Inject] public required Aplicacao.ManipuladorDeArquivo Manipulador { get; set; }
    [Parameter] public required Preferencias Preferencias { get; set; }
    [Parameter] public MenuWindow? Janela { get; set; }

    public bool expandido = true;
    public void ExpandirRecolherMenu() => expandido = !expandido;

    public int selecionado;
    public void Selecionar(int i) => selecionado = i;

    public string Exibicao()
    {
        switch (selecionado)
        {
            case 0: return "Exportações";
            case 10: return "Configurações";
            case 11: return "Tema";
            case 20: return "Cadastros";
            case 99: return "Sobre";
            default: return "d̶̘͓͝ä̸̭͈͂s̸̫͚̭̃͠ ̴̖̻́̓͑͜͠s̵̲͔͑̽ͅó̸̜͇̱̥̖m̴͈̱̺̎͂̈̊͜b̵͈͇̈̊̾r̸̹̻̻͓̪̝̔̊̉̊̂͐̊̇̕̚ą̵̡̢̣̜̺͔̘͉̬͍̼͋̂́͋̆̐͘ṣ̷͚͖̩̩̥̎ ̶̛̙̦̟̫̋̿̃͘̕͝ ̸͙̑̉͂é̵̻̊̌͝u̵̝̎ ̶̪̉̍̔̇̇ ̶̞͓̀v̵̹̬̚e̷̛͍j̴̲́o̴͎̱̐͠";
        }
    }
    public string Icone()
    {
        switch (selecionado)
        {
            case 0: return Icons.Material.Rounded.DriveFolderUpload;
            case 10: return Icons.Material.Rounded.Settings;
            case 11: return Icons.Material.Rounded.Palette;
            case 20: return Icons.Material.Rounded.Dataset;
            case 99: return Icons.Material.Rounded.Info;
            default: return Icons.Material.TwoTone.RemoveRedEye;
        }
    }

    protected override void OnInitialized()
    {
        AcordarTema();
        Preferencias.OnChanged += StateHasChanged;
        Manipulador = new();
    }
    private void AcordarTema()
    {
        var temaTemp = Preferencias.TemaSelecionado;
        Preferencias.TemaSelecionado = Dominio.Enumeradores.ETema.TemaPadrao;
        Preferencias.TemaSelecionado = temaTemp;
    }

    public void Teste()
    {
        var adds = Preferencias.Tema.PaletteLight.Primary.ToString();
        Janela?.Alterar();
    }

    public void Dispose()
    {
        Preferencias.OnChanged -= StateHasChanged;
    }

}
