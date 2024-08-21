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
using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;

namespace Devnometro
{
    /// <summary>
    /// Lógica interna para Configuracao.xaml
    /// </summary>
    public partial class Configuracao : Window
    {
        public Configuracao()
        {
            InitializeComponent();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddWpfBlazorWebView();
            serviceCollection.AddMudServices();
            serviceCollection.AddSingleton<MudTheme>();
            Resources.Add("services", serviceCollection.BuildServiceProvider());
            config.Parameters = new Dictionary<string, object?>
            {
                { "janela", this }
            };
        }

        public event EventHandler? AplicaAlteracoes;

        public void Alterar()
        {
            AplicaAlteracoes?.Invoke(this, EventArgs.Empty);
        }
    }
}
