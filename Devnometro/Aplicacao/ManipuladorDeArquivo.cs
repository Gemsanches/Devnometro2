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
            await MetodosEstaticos.MensagemAsync($"Erro ao verificar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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

            await MetodosEstaticos.MensagemAsync($"Arquivo salvo com sucesso em: {filePath}", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            await MetodosEstaticos.MensagemAsync($"Erro ao salvar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    public async static Task<Cliente?> ExemploCarregar()
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
                await MetodosEstaticos.MensagemAsync("Arquivo não encontrado.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            // Lê o conteúdo do arquivo JSON
            string json = File.ReadAllText(filePath);

            // Desserializa o JSON para um objeto Cliente
            Cliente? cliente = JsonSerializer.Deserialize<Cliente>(json);

            await MetodosEstaticos.MensagemAsync("Arquivo carregado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            return cliente;
        }
        catch (Exception ex)
        {
            await MetodosEstaticos.MensagemAsync($"Erro ao carregar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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

                await MetodosEstaticos.MensagemAsync("Arquivo excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                await MetodosEstaticos.MensagemAsync("Arquivo não encontrado.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (Exception ex)
        {
            await MetodosEstaticos.MensagemAsync($"Erro ao excluir o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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

                await MetodosEstaticos.MensagemAsync($"Arquivo salvo com sucesso em: {saveFileDialog.FileName}", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            await MetodosEstaticos.MensagemAsync($"Erro ao exportar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
                Cliente? cliente = JsonSerializer.Deserialize<Cliente>(json);

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

                    await MetodosEstaticos.MensagemAsync($"Arquivo importado e salvo com sucesso em: {filePath}", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    await MetodosEstaticos.MensagemAsync("O arquivo selecionado não é um JSON válido para a classe Cliente.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        catch (Exception ex)
        {
            await MetodosEstaticos.MensagemAsync($"Erro ao importar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public class Cliente
    {
        public bool EhVip { get; set; }
        public int PontosAcumulados { get; set; }
        public string Nome { get; set; } = "";
        public string CPF { get; set; } = "";
        public DateTime Nascimento { get; set; }
        public List<Produto> ProdutosComprados { get; set; } = [];
    }

    public class Produto
    {
        public string Descricao { get; set; } = "";
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
    public async Task<TemaPersonalizado?> ImportarTemaPersonalizadoAsync()
    {
        try
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Arquivos TEMA (*.tema)|*.tema",
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
                Filter = "Arquivos TEMA (*.tema)|*.tema",
                DefaultExt = "tema",
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
    public List<ContadorModel> CarregarContadores()
    {
        try
        {
            if (VerificarArquivo(arquivoCadatrosContadores))
            {
                string json = File.ReadAllText(Path.Combine(pastaPrograma, arquivoCadatrosContadores));
                List<ContadorModel>? contadores = JsonSerializer.Deserialize<List<ContadorModel>>(json);
                return contadores ?? ContadorModel.ListaMocada();
            }
        }
        catch { }

        return ContadorModel.ListaMocada();
    }
    public async Task SalvarContadoresAsync(List<ContadorModel> lista)
    {
        try
        {
            string json = JsonSerializer.Serialize(lista, JSO);
            string filePath = Path.Combine(pastaPrograma, arquivoCadatrosContadores);

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao salvar cadastro de Contadores: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    public static bool LimparContadores()
    {
        try
        {
            string filePath = Path.Combine(pastaPrograma, arquivoCadatrosContadores);
            if (File.Exists(filePath))
                File.Delete(filePath);
            return true;
        }
        catch { return false; }
    }
    public async Task<List<ContadorModel>?> ImportarContadoresAsync()
    {
        try
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Arquivos CNT (*.cnt)|*.cnt",
                Title = "Selecionar Arquivo de Lista de Contadores"
            };

            bool? resposta = openFileDialog.ShowDialog();

            if (resposta == true)
            {
                string json = File.ReadAllText(openFileDialog.FileName);

                List<ContadorModel>? lista = JsonSerializer.Deserialize<List<ContadorModel>>(json);

                if (lista != null)
                {
                    string filePath = Path.Combine(pastaPrograma, arquivoCadatrosContadores);
                    File.WriteAllText(filePath, json);

                    await MetodosEstaticos.MensagemAsync($"Arquivo {openFileDialog.SafeFileName} importado com sucesso", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return lista;
                }
                else
                    await MetodosEstaticos.MensagemAsync("O arquivo selecionado não é uma lista válida de Contadores.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao importar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
        return null;
    }
    public async Task ExportarContadoresAsync(List<ContadorModel> lista)
    {
        try
        {
            string json = JsonSerializer.Serialize(lista, JSO);

            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Arquivos CNT (*.cnt)|*.cnt",
                DefaultExt = "cnt",
                Title = "Salvar Lista de Contadores"
            };

            bool? resposta = saveFileDialog.ShowDialog();
            if (resposta == true)
                File.WriteAllText(saveFileDialog.FileName, json);
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao exportar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    #endregion

    #region Padrões de Cronômetros
    public List<PadraoDeCronometroModel> CarregarPadroesCronometros()
    {
        try
        {
            if (VerificarArquivo(arquivoCadatrosPadraoCronometros))
            {
                string json = File.ReadAllText(Path.Combine(pastaPrograma, arquivoCadatrosPadraoCronometros));
                List<PadraoDeCronometroModel>? padroes = JsonSerializer.Deserialize<List<PadraoDeCronometroModel>>(json);
                return padroes ?? PadraoDeCronometroModel.ListaMocada();
            }
        }
        catch { }

        return PadraoDeCronometroModel.ListaMocada();
    }
    public async Task SalvarPadroesCronometrosAsync(List<PadraoDeCronometroModel> lista)
    {
        try
        {
            string json = JsonSerializer.Serialize(lista, JSO);
            string filePath = Path.Combine(pastaPrograma, arquivoCadatrosPadraoCronometros);

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao salvar cadastro de Padrões de Cronômetro: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    public static bool LimparPadroesCronometros()
    {
        try
        {
            string filePath = Path.Combine(pastaPrograma, arquivoCadatrosPadraoCronometros);
            if (File.Exists(filePath))
                File.Delete(filePath);
            return true;
        }
        catch { return false; }
    }
    public async Task<List<PadraoDeCronometroModel>?> ImportarPadroesCronometrosAsync()
    {
        try
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Arquivos PCR (*.pcr)|*.pcr",
                Title = "Selecionar Arquivo de Lista de Padrões de Cronômetro"
            };

            bool? resposta = openFileDialog.ShowDialog();

            if (resposta == true)
            {
                string json = File.ReadAllText(openFileDialog.FileName);

                List<PadraoDeCronometroModel>? lista = JsonSerializer.Deserialize<List<PadraoDeCronometroModel>>(json);

                if (lista != null)
                {
                    string filePath = Path.Combine(pastaPrograma, arquivoCadatrosPadraoCronometros);
                    File.WriteAllText(filePath, json);

                    await MetodosEstaticos.MensagemAsync($"Arquivo {openFileDialog.SafeFileName} importado com sucesso", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return lista;
                }
                else
                    await MetodosEstaticos.MensagemAsync("O arquivo selecionado não é uma lista válida de Padrões de Cronômetro.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao importar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
        return null;
    }
    public async Task ExportarPadroesCronometrosAsync(List<PadraoDeCronometroModel> lista)
    {
        try
        {
            string json = JsonSerializer.Serialize(lista, JSO);

            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Arquivos PCR (*.pcr)|*.pcr",
                DefaultExt = "pcr",
                Title = "Salvar Lista de Padrões de Cronômetro"
            };

            bool? resposta = saveFileDialog.ShowDialog();
            if (resposta == true)
                File.WriteAllText(saveFileDialog.FileName, json);
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao exportar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    #endregion

    #region Cronômetros Expressos
    public List<ExpressoModel> CarregarExpressos()
    {
        try
        {
            if (VerificarArquivo(arquivoCadatrosExpressos))
            {
                string json = File.ReadAllText(Path.Combine(pastaPrograma, arquivoCadatrosExpressos));
                List<ExpressoModel>? expressos = JsonSerializer.Deserialize<List<ExpressoModel>>(json);
                return expressos ?? ExpressoModel.ListaMocada();
            }
        }
        catch { }

        return ExpressoModel.ListaMocada();
    }
    public async Task SalvarExpressosAsync(List<ExpressoModel> lista)
    {
        try
        {
            string json = JsonSerializer.Serialize(lista, JSO);
            string filePath = Path.Combine(pastaPrograma, arquivoCadatrosExpressos);

            File.WriteAllText(filePath, json);
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao salvar cadastro de Cronômetros Expressos: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    public static bool LimparExpressos()
    {
        try
        {
            string filePath = Path.Combine(pastaPrograma, arquivoCadatrosExpressos);
            if (File.Exists(filePath))
                File.Delete(filePath);
            return true;
        }
        catch { return false; }
    }
    public async Task<List<ExpressoModel>?> ImportarExpressosAsync()
    {
        try
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Arquivos CREX (*.crex)|*.crex",
                Title = "Selecionar Arquivo de Lista de Cronômetros Expressos"
            };

            bool? resposta = openFileDialog.ShowDialog();

            if (resposta == true)
            {
                string json = File.ReadAllText(openFileDialog.FileName);

                List<ExpressoModel>? lista = JsonSerializer.Deserialize<List<ExpressoModel>>(json);

                if (lista != null)
                {
                    string filePath = Path.Combine(pastaPrograma, arquivoCadatrosExpressos);
                    File.WriteAllText(filePath, json);

                    await MetodosEstaticos.MensagemAsync($"Arquivo {openFileDialog.SafeFileName} importado com sucesso", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    return lista;
                }
                else
                    await MetodosEstaticos.MensagemAsync("O arquivo selecionado não é uma lista válida de Cronômetros Expressos.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao importar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
        return null;
    }
    public async Task ExportarExpressosAsync(List<ExpressoModel> lista)
    {
        try
        {
            string json = JsonSerializer.Serialize(lista, JSO);

            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Arquivos CREX (*.crex)|*.crex",
                DefaultExt = "crex",
                Title = "Salvar Lista de Cronômetros Expressos"
            };

            bool? resposta = saveFileDialog.ShowDialog();
            if (resposta == true)
                File.WriteAllText(saveFileDialog.FileName, json);
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao exportar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    #endregion

    #endregion
}

