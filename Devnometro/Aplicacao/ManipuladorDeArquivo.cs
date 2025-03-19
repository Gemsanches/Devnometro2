using Devnometro.Dominio;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Devnometro.Aplicacao;

public class ManipuladorDeArquivo
{
    private JsonSerializerOptions JSO { get; set; } = new JsonSerializerOptions { WriteIndented = true };

    #region Constantes
    private static readonly string tipoArquivoTemaPersonalizado = "tema";
    private static readonly string tipoArquivoCadatrosContadores = "cnt";
    private static readonly string tipoArquivoPadraoCronometros = "pcr";
    private static readonly string tipoArquivoExpressos = "crex";
    private static readonly string cadastro = "cadastro de";
    private static readonly string descricaoCadatrosContadores = "Lista de Contadores";
    private static readonly string descricaoPadraoCronometros = "Lista de Padrões de Cronômetro";
    private static readonly string descricaoExpressos = "Lista de Cronômetros Expressos";

    private static readonly string pastaPrograma = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly string arquivoPreferencias = "Preferencias.json";
    private static readonly string arquivoTemaPersonalizado = $"Tema.{tipoArquivoTemaPersonalizado}";
    private static readonly string arquivoPonto = "HistoricoPonto.json";
    private static readonly string arquivoCronometros = "HistoricoTempo.json";
    private static readonly string arquivoCronometrosAbertos = "Tempo.json";
    private static readonly string arquivoCadatrosContadores = $"CadContadores.{tipoArquivoCadatrosContadores}";
    private static readonly string arquivoCadatrosPadraoCronometros = $"CadPadroes.{tipoArquivoPadraoCronometros}";
    private static readonly string arquivoCadatrosExpressos = $"CadExpressos.{tipoArquivoExpressos}";
    #endregion

    #region Métodos privados
    private static bool VerificarArquivo(string arquivo)
    {
        try { return File.Exists(Path.Combine(pastaPrograma, arquivo)); }
        catch { return false; }
    }
    public static bool Limpar(string caminhoArquivo)
    {
        try
        {
            string filePath = Path.Combine(pastaPrograma, caminhoArquivo);
            if (File.Exists(filePath))
                File.Delete(filePath);
            return true;
        }
        catch { return false; }
    }

    private List<T> Carregar<T>(string caminhoArquivo, List<T> listaReserva)
    {
        try
        {
            if (VerificarArquivo(caminhoArquivo))
            {
                string json = File.ReadAllText(Path.Combine(pastaPrograma, caminhoArquivo));
                List<T>? lista = JsonSerializer.Deserialize<List<T>>(json);
                return lista ?? listaReserva;
            }
        }
        catch { }

        return listaReserva;
    }
    public async Task SalvarAsync<T>(List<T> lista, string caminhoArquivo, string descricao)
    {
        try
        {
            string json = JsonSerializer.Serialize(lista, JSO);
            string filePath = Path.Combine(pastaPrograma, caminhoArquivo);

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao salvar {descricao}: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    public async Task<List<T>?> ImportarAsync<T>(string caminhoArquivo, string tipoArquivo, string descricao)
    {
        try
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = $"Arquivos {tipoArquivo.ToUpper()} (*.{tipoArquivo.ToLower()})|*.{tipoArquivo.ToLower()}",
                Title = $"Selecionar Arquivo de {descricao}"
            };

            bool? resposta = openFileDialog.ShowDialog();

            if (resposta == true)
            {
                string json = File.ReadAllText(openFileDialog.FileName);

                List<T>? lista = JsonSerializer.Deserialize<List<T>>(json);

                if (lista != null)
                {
                    string filePath = Path.Combine(pastaPrograma, caminhoArquivo);
                    File.WriteAllText(filePath, json);

                    await MetodosEstaticos.MensagemAsync($"Arquivo {openFileDialog.SafeFileName} importado com sucesso", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return lista;
                }
                else
                    await MetodosEstaticos.MensagemAsync($"O arquivo selecionado não é {descricao} válida", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao importar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
        return null;
    }
    public async Task ExportarAsync<T>(List<T> lista, string tipoArquivo, string descricao)
    {
        try
        {
            string json = JsonSerializer.Serialize(lista, JSO);

            SaveFileDialog saveFileDialog = new()
            {
                Filter = $"Arquivos {tipoArquivo.ToUpper()} (*.{tipoArquivo.ToLower()})|*.{tipoArquivo.ToLower()}",
                DefaultExt = tipoArquivo.ToLower(),
                Title = $"Salvar {descricao}"
            };

            bool? resposta = saveFileDialog.ShowDialog();
            if (resposta == true)
                File.WriteAllText(saveFileDialog.FileName, json);
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao exportar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    #endregion

    #region Preferências
    public static Preferencias CarregarPreferencias()
    {
        try
        {
            if (VerificarArquivo(arquivoPreferencias))
            {
                string json = File.ReadAllText(Path.Combine(pastaPrograma, arquivoPreferencias));
                Preferencias? preferencias = JsonSerializer.Deserialize<Preferencias>(json);

                if (preferencias is not null)
                {
                    preferencias.TemaPersonalizado = CarregarTemaPersonalizado();
                    preferencias.TemaPersonalizado.AplicarCoresAoTema();
                    return preferencias;
                }
            }
        }
        catch { }

        return Preferencias.PreferenciasPadroes();
    }
    public async Task<bool> SalvarPreferenciasAsync(Preferencias preferencias)
    {
        try
        {
            string json = JsonSerializer.Serialize(preferencias, JSO);
            string filePath = Path.Combine(pastaPrograma, arquivoPreferencias);

            File.WriteAllText(filePath, json);
            return true;
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao salvar Preferências: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
    }

    #region Tema Personalizado
    public static TemaPersonalizado CarregarTemaPersonalizado()
    {
        try
        {
            if (VerificarArquivo(arquivoTemaPersonalizado))
            {
                string json = File.ReadAllText(Path.Combine(pastaPrograma, arquivoTemaPersonalizado));
                TemaPersonalizado? temaPersonalizado = JsonSerializer.Deserialize<TemaPersonalizado>(json);
                return temaPersonalizado ?? TemaPersonalizado.CriarTemaPersonalizado();
            }
        }
        catch { }

        return TemaPersonalizado.CriarTemaPersonalizado();
    }
    public async Task SalvarTemaPersonalizadoAsync(TemaPersonalizado temaPersonalizado)
    {
        try
        {
            string json = JsonSerializer.Serialize(temaPersonalizado, JSO);
            string filePath = Path.Combine(pastaPrograma, arquivoTemaPersonalizado);

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao salvar Tema personalizado: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    public static bool LimparTemaPersonalizado() => Limpar(arquivoTemaPersonalizado);
    public async Task<TemaPersonalizado?> ImportarTemaPersonalizadoAsync()
    {
        try
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = $"Arquivos {tipoArquivoTemaPersonalizado.ToUpper()} (*.{tipoArquivoTemaPersonalizado.ToLower()})|*.{tipoArquivoTemaPersonalizado.ToLower()}",
                Title = "Selecionar Arquivo de Tema Personalizado"
            };

            bool? resposta = openFileDialog.ShowDialog();
            if (resposta == true)
            {
                string json = File.ReadAllText(openFileDialog.FileName);

                TemaPersonalizado? temaPersonalizado = JsonSerializer.Deserialize<TemaPersonalizado>(json);

                if (temaPersonalizado != null)
                {
                    string filePath = Path.Combine(pastaPrograma, arquivoTemaPersonalizado);
                    File.WriteAllText(filePath, json);

                    await MetodosEstaticos.MensagemAsync($"Arquivo {openFileDialog.SafeFileName} importado e salvo com sucesso", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return temaPersonalizado;
                }
                else
                    await MetodosEstaticos.MensagemAsync("O arquivo selecionado não é um Tema Personalizado válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao importar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
        return null;
    }
    public async Task ExportarTemaPersonalizadoAsync(TemaPersonalizado temaPersonalizado)
    {
        try
        {
            string json = JsonSerializer.Serialize(temaPersonalizado, JSO);

            SaveFileDialog saveFileDialog = new()
            {
                Filter = $"Arquivos {tipoArquivoTemaPersonalizado.ToUpper()} (*.{tipoArquivoTemaPersonalizado.ToLower()})|*.{tipoArquivoTemaPersonalizado.ToLower()}",
                DefaultExt = tipoArquivoTemaPersonalizado.ToLower(),
                Title = "Salvar Arquivo de Tema Personalizado"
            };

            bool? resposta = saveFileDialog.ShowDialog();

            if (resposta == true)
                File.WriteAllText(saveFileDialog.FileName, json);
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao exportar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    #endregion

    #endregion


    #region Ponto
    public static void SalvarPontos()
    {

    }
    public static void CarregarPontos()
    {

    }
    public static void CarregarPontosDoDia()
    {

    }
    public static void LimparPontos(string filtro)
    {

    }
    public static void ExportarPontos(string lista)
    {

    }
    #endregion

    #region Cronômetro
    public static void SalvarCronometros()
    {

    }
    public static void CarregarCronometros()
    {

    }
    public static void CarregarCronometrosAbertos()
    {

    }
    public static void LimparCronometros(string filtro)
    {

    }
    public static void ExportarCronometros(string lista)
    {

    }
    #endregion


    #region Cadastros

    #region Contadores
    public List<ContadorModel> CarregarContadores() => Carregar<ContadorModel>(arquivoCadatrosContadores, ContadorModel.ListaMocada());
    public async Task SalvarContadoresAsync(List<ContadorModel> lista) =>
        await SalvarAsync<ContadorModel>(lista, arquivoCadatrosContadores, $"{cadastro} {descricaoCadatrosContadores}");
    public static bool LimparContadores() => Limpar(arquivoCadatrosContadores);
    public async Task<List<ContadorModel>?> ImportarContadoresAsync() =>
        await ImportarAsync<ContadorModel>(arquivoCadatrosContadores, tipoArquivoCadatrosContadores, descricaoCadatrosContadores);
    public async Task ExportarContadoresAsync(List<ContadorModel> lista) =>
        await ExportarAsync<ContadorModel>(lista, tipoArquivoCadatrosContadores, descricaoCadatrosContadores);
    #endregion

    #region Padrões de Cronômetros
    public List<PadraoDeCronometroModel> CarregarPadroesCronometros() =>
        Carregar<PadraoDeCronometroModel>(arquivoCadatrosPadraoCronometros, PadraoDeCronometroModel.ListaMocada());
    public async Task SalvarPadroesCronometrosAsync(List<PadraoDeCronometroModel> lista) =>
        await SalvarAsync<PadraoDeCronometroModel>(lista, arquivoCadatrosPadraoCronometros, $"{cadastro} {descricaoPadraoCronometros}");
    public static bool LimparPadroesCronometros() => Limpar(arquivoCadatrosPadraoCronometros);
    public async Task<List<PadraoDeCronometroModel>?> ImportarPadroesCronometrosAsync() =>
        await ImportarAsync<PadraoDeCronometroModel>(arquivoCadatrosPadraoCronometros, tipoArquivoPadraoCronometros, descricaoPadraoCronometros);
    public async Task ExportarPadroesCronometrosAsync(List<PadraoDeCronometroModel> lista) =>
        await ExportarAsync<PadraoDeCronometroModel>(lista, tipoArquivoPadraoCronometros, descricaoPadraoCronometros);
    #endregion

    #region Cronômetros Expressos
    public List<ExpressoModel> CarregarExpressos() =>
        Carregar<ExpressoModel>(arquivoCadatrosExpressos, ExpressoModel.ListaMocada());
    public async Task SalvarExpressosAsync(List<ExpressoModel> lista) =>
        await SalvarAsync<ExpressoModel>(lista, arquivoCadatrosExpressos, $"{cadastro} {descricaoExpressos}");
    public static bool LimparExpressos() => Limpar(arquivoCadatrosExpressos);
    public async Task<List<ExpressoModel>?> ImportarExpressosAsync() =>
        await ImportarAsync<ExpressoModel>(arquivoCadatrosExpressos, tipoArquivoExpressos, descricaoExpressos);
    public async Task ExportarExpressosAsync(List<ExpressoModel> lista) =>
        await ExportarAsync<ExpressoModel>(lista, tipoArquivoExpressos, descricaoExpressos);
    #endregion

    #endregion
}

