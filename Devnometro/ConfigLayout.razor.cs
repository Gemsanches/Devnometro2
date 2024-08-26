using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devnometro;

public class ConfigLayoutBase : ComponentBase
{
    [Inject]
    public MudTheme _theme { get; set; }

    [Parameter]
    public Configuracao? janela { get; set; }
    public bool expandido = true;
    public void ExpandirRecolherMenu() => expandido = !expandido;

    protected bool temaEscuro;
    protected MudTheme TemaSelecionado { get; set; } = new();

    public int selecionado;
    public void Selecionar(int i) => selecionado = i;

    public string Exibicao()
    {
        switch (selecionado)
        {
            case 0: return "Configurações: Geral";
            case 1: return "Configurações: Caminhos";
            case 2: return "Configurações: Tema";
            case 10: return "Cadastros: Contadores";
            case 11: return "Cadastros: Padrões de cronômetros";
            default: return "C̷̢̪͓̮̼͓̹̪̃͊͐͂̈͐̌͌͌͘͝ö̶̧̖͎̣͂̍n̶̬̫̹̞͓̈́f̸̟͇̦̪̟̄̏́̀͋͐͐̎̎̐̚̚͝i̵̡̘̩̘͕͋͆̐̑̌̈́̌͐̓͑g̶̢̢̘̬̩͉̪̯̜͕͚͎̣̪͕̒̓̔̂͊̅͒͌̂͊̀͑͗̇͌u̴̢̡̙̙͉͓̰͉͍̙̭̻̟͛̓̅̆͂͌̌̂̽̏͝r̷̹͍͎̦̆͌̄͘͝à̴̜̳̥̻͓͆͛͂́̇͐͂͐͘͜ç̵̣̗̪͑͗́͋̏͛̍̒͐̓͘͝͠õ̴̱͓͑̆̂̅͐̐́͗ę̶̢̼̺̙̩͔͇͎̈́s̶̡̖̳̗̱̹͇̯̣̞̗̤̩̆̈̆̒͠͝ ̸̫̐̿̈̽̉͛̉͑̄̌͑̇͠ͅ/̷̢̛̬̥̻͊̍̆͗͝ ̸̨̡̝̟̪͎̈́C̴̻͛̋͛͌̌̽̐̓̃̐͌̋̐à̴̡̧̧͇͇̮̙̻̝͚͍̫͉̃̔̇̈́ḏ̸̨͇̮̠̈̄͛͒͋̋̈̃̕͠͝a̵̳͕͇͚̙͆́̃̎ṣ̷̹̣̰͉̗̰̹͍̑̒̂̓̒͂t̷̯̒̒̋̎̃̓͂͗̿̑͒͂͝ṟ̶̠̲̺̩̜͇͖̹͛̈́̾̿̿͘ó̶̧͓̬̱͔̫̝͙̦͔̟͆̊̆̎̀͌͘͠ͅs̷̨͈̠̯̲̟̼̀̈́̂͆̆̍̍̉̚ͅ";
        }
    }

    public string Icone()
    {
        switch (selecionado)
        {
            case 0: return MudBlazor.Icons.Material.Rounded.Settings;
            case 1: return MudBlazor.Icons.Material.Rounded.DriveFolderUpload;
            case 2: return MudBlazor.Icons.Material.Rounded.Palette;
            case 10: return MudBlazor.Icons.Material.Rounded.Timer;
            case 11: return MudBlazor.Icons.Material.Rounded.GroupWork;
            default: return MudBlazor.Icons.Material.TwoTone.RemoveRedEye;
        }
    }

    public void Teste()
    {
        var adds = _theme.PaletteLight.Primary.ToString();
        TemaSelecionado = _theme;
        janela?.Alterar();
    }
}
