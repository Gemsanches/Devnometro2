using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.Core;
using MudBlazor;
using MudBlazor.Services;
using Devnometro.Dominio;

namespace Devnometro;

public partial class MenuWindow : Window
{
    public MenuWindow(Preferencias preferencias)
    {
        InitializeComponent();
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWpfBlazorWebView();
        serviceCollection.AddMudServices();
        serviceCollection.AddSingleton<Devnometro.Aplicacao.ManipuladorDeArquivo>();
        this.preferencias = preferencias;
        Resources.Add("services", serviceCollection.BuildServiceProvider());
        menu.Parameters = new Dictionary<string, object?>
        {
            { "Janela", this },
            { "Preferencias", preferencias }
        };
    }

    public readonly Preferencias preferencias;

    public event EventHandler? AplicaAlteracoes;

    public void Alterar()
    {
        AplicaAlteracoes?.Invoke(this, EventArgs.Empty);
    }
}