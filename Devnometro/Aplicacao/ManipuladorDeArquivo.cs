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


    #region Caminhos e arquivos
    private static readonly string pastaPrograma = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly string arquivoPreferencias = "Preferencias.json";
    private static readonly string arquivoTemaPersonalizado = "Tema.tema";
    private static readonly string arquivoPonto = "HistoricoPonto.json";
    private static readonly string arquivoCronometros = "HistoricoTempo.json";
    private static readonly string arquivoCronometrosAbertos = "Tempo.json";
    private static readonly string arquivoCadatrosContadores = "CadContadores.json";
    private static readonly string arquivoCadatrosPadraoCronometros = "CadPadroes.json";
    private static readonly string arquivoCadatrosExpressos = "CadExpressos.json";
    #endregion
    private static bool VerificarArquivo(string arquivo)
    {
        try { return File.Exists(Path.Combine(pastaPrograma, arquivo)); }
        catch { return false; }
    }

    #region Exemplos
    public async static Task<bool> ExempoVerificar()
    {
        try
        {
            // Caminho para o diretório do executável
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Nome do arquivo
            string fileName = "cliente.json";

            // Caminho completo do arquivo
            string filePath = Path.Combine(appDirectory, fileName);

            // Verifica se o arquivo existe
            return File.Exists(filePath);
        }
        catch (Exception ex)
        {
            await MetodosEstaticos.Mensagem($"Erro ao verificar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }
    public async static Task ExemploSalvar()
    {
        try
        {
            // Cria um objeto Cliente de exemplo
            var cliente = new Cliente
            {
                EhVip = true,
                PontosAcumulados = 1000,
                Nome = "João Silva",
                CPF = "123.456.789-00",
                Nascimento = new DateTime(1985, 5, 15),
                ProdutosComprados = new List<Produto>
                    {
                        new Produto { Descricao = "Notebook", Preco = 3500.00, Quantidade = 1 },
                        new Produto { Descricao = "Mouse", Preco = 50.00, Quantidade = 2 }
                    }
            };

            // Serializa o objeto Cliente para JSON
            string json = JsonSerializer.Serialize(cliente, new JsonSerializerOptions { WriteIndented = true });

            // Caminho para o diretório do executável
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Nome do arquivo
            string fileName = "cliente.json";

            // Caminho completo do arquivo
            string filePath = Path.Combine(appDirectory, fileName);

            // Salva o JSON no arquivo
            File.WriteAllText(filePath, json);

            await MetodosEstaticos.Mensagem($"Arquivo salvo com sucesso em: {filePath}", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            await MetodosEstaticos.Mensagem($"Erro ao salvar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    public async static Task<Cliente> ExemploCarregar()
    {
        try
        {
            // Caminho para o diretório do executável
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Nome do arquivo
            string fileName = "cliente.json";

            // Caminho completo do arquivo
            string filePath = Path.Combine(appDirectory, fileName);

            // Verifica se o arquivo existe
            if (!File.Exists(filePath))
            {
                await MetodosEstaticos.Mensagem("Arquivo não encontrado.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            // Lê o conteúdo do arquivo JSON
            string json = File.ReadAllText(filePath);

            // Desserializa o JSON para um objeto Cliente
            Cliente cliente = JsonSerializer.Deserialize<Cliente>(json);

            await MetodosEstaticos.Mensagem("Arquivo carregado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            return cliente;
        }
        catch (Exception ex)
        {
            await MetodosEstaticos.Mensagem($"Erro ao carregar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            return null;
        }
    }
    public async static Task ExemploLimpar()
    {
        try
        {
            // Caminho para o diretório do executável
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Nome do arquivo
            string fileName = "cliente.json";

            // Caminho completo do arquivo
            string filePath = Path.Combine(appDirectory, fileName);

            // Verifica se o arquivo existe
            if (File.Exists(filePath))
            {
                // Exclui o arquivo
                File.Delete(filePath);

                await MetodosEstaticos.Mensagem("Arquivo excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                await MetodosEstaticos.Mensagem("Arquivo não encontrado.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (Exception ex)
        {
            await MetodosEstaticos.Mensagem($"Erro ao excluir o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    public async static Task ExemploExportar(Cliente cliente)
    {
        try
        {
            // Serializa o objeto Cliente para JSON
            string json = JsonSerializer.Serialize(cliente, new JsonSerializerOptions { WriteIndented = true });

            // Cria uma janela de diálogo para salvar o arquivo
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Arquivos JSON (*.json)|*.json";
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.Title = "Salvar Arquivo JSON";

            // Exibe a janela de diálogo e verifica se o usuário confirmou
            if (saveFileDialog.ShowDialog() == true)
            {
                // Salva o JSON no local escolhido pelo usuário
                File.WriteAllText(saveFileDialog.FileName, json);

                await MetodosEstaticos.Mensagem($"Arquivo salvo com sucesso em: {saveFileDialog.FileName}", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            await MetodosEstaticos.Mensagem($"Erro ao exportar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    public async static Task ExemploImportar()
    {
        try
        {
            // Cria uma janela de diálogo para abrir o arquivo
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Arquivos JSON (*.json)|*.json";
            openFileDialog.Title = "Selecionar Arquivo JSON";

            // Exibe a janela de diálogo e verifica se o usuário confirmou
            if (openFileDialog.ShowDialog() == true)
            {
                // Lê o conteúdo do arquivo JSON selecionado
                string json = File.ReadAllText(openFileDialog.FileName);

                // Tenta desserializar o JSON para um objeto Cliente
                Cliente cliente = JsonSerializer.Deserialize<Cliente>(json);

                // Se a desserialização for bem-sucedida, salva o arquivo no diretório do programa
                if (cliente != null)
                {
                    // Caminho para o diretório do executável
                    string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    // Nome do arquivo
                    string fileName = "cliente.json";

                    // Caminho completo do arquivo
                    string filePath = Path.Combine(appDirectory, fileName);

                    // Salva o JSON no diretório do programa
                    File.WriteAllText(filePath, json);

                    await MetodosEstaticos.Mensagem($"Arquivo importado e salvo com sucesso em: {filePath}", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    await MetodosEstaticos.Mensagem("O arquivo selecionado não é um JSON válido para a classe Cliente.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        catch (Exception ex)
        {
            await MetodosEstaticos.Mensagem($"Erro ao importar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public class Cliente
    {
        public bool EhVip { get; set; }
        public int PontosAcumulados { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }
        public List<Produto> ProdutosComprados { get; set; }
    }

    public class Produto
    {
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
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
        catch {}

        return Preferencias.PreferenciasPadroes();
    }
    public async Task SalvarPreferencias(Preferencias preferencias)
    {
        try
        {
            string json = JsonSerializer.Serialize(preferencias, JSO);
            string filePath = Path.Combine(pastaPrograma, arquivoPreferencias);

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex) { await MetodosEstaticos.Mensagem($"Erro ao salvar Preferências: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
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
    public async Task SalvarTemaPersonalizado(TemaPersonalizado temaPersonalizado)
    {
        try
        {
            string json = JsonSerializer.Serialize(temaPersonalizado, JSO);
            string filePath = Path.Combine(pastaPrograma, arquivoPreferencias);

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex) { await MetodosEstaticos.Mensagem($"Erro ao salvar Tema personalizado: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    public static bool LimparTemaPersonalizado()
    {
        try
        {
            string filePath = Path.Combine(pastaPrograma, arquivoTemaPersonalizado);
            if (File.Exists(filePath))
                File.Delete(filePath);
            return true;
        }
        catch { return false; }
    }
    public async Task<TemaPersonalizado?> ImportarTemaPersonalizado()
    {
        try
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Arquivos TEMA (*.tema)|*.tema",
                Title = "Selecionar Arquivo de Tema Personalizado"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(openFileDialog.FileName);

                TemaPersonalizado temaPersonalizado = JsonSerializer.Deserialize<TemaPersonalizado>(json);

                if (temaPersonalizado != null)
                {
                    string filePath = Path.Combine(pastaPrograma, arquivoTemaPersonalizado);
                    File.WriteAllText(filePath, json);

                    await MetodosEstaticos.Mensagem($"Arquivo {openFileDialog.SafeFileName} importado e salvo com sucesso", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return temaPersonalizado;
                }
                else
                    await MetodosEstaticos.Mensagem("O arquivo selecionado não é um Tema Personalizado válido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex) { await MetodosEstaticos.Mensagem($"Erro ao importar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
        return null;
    }
    public async Task ExportarTemaPersonalizado(TemaPersonalizado temaPersonalizado)
    {
        try
        {
            string json = JsonSerializer.Serialize(temaPersonalizado, JSO);

            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Arquivos TEMA (*.tema)|*.tema",
                DefaultExt = "tema",
                Title = "Salvar Arquivo de Tema Personalizado"
            };

            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, json);
        }
        catch (Exception ex) { await MetodosEstaticos.Mensagem($"Erro ao exportar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
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
    public static void SalvarContadores()
    {

    }
    public static void CarregarContadores()
    {

    }
    #endregion

    #region Padrões de Cronômetros
    public static void SalvarPadroes()
    {

    }
    public static void CarregarPadroes()
    {

    }
    #endregion
    
    #region Cronômetros Expressos
    public static void SalvarExpressos()
    {

    }
    public static void CarregarExpressos()
    {

    }
    #endregion

    #endregion
}

