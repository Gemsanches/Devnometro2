using Devnometro.Dominio;
using System.Printing;
using System.Windows;

namespace Devnometro.Aplicacao;

public static class MetodosEstaticos
{
    #region Auxiliares de exibição
    public static string BooleanoSimNao(bool valor) => valor ? "Sim" : "Não";
    public static async Task<MessageBoxResult> MensagemAsync(string corpo, string titulo, MessageBoxButton botao, MessageBoxImage icone)
    {
        MessageBoxResult resposta = await Task.Run(() =>
        {
            return MessageBox.Show(corpo, titulo, botao, icone);
        });
        return resposta;
    }
    #endregion
}
