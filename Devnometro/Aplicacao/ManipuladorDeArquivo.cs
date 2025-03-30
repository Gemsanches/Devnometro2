using Devnometro.Dominio;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using static MudBlazor.CategoryTypes;

namespace Devnometro.Aplicacao;

public class ManipuladorDeArquivo
{
    private JsonSerializerOptions JSO { get; set; } = new JsonSerializerOptions { WriteIndented = true };

    public ManipuladorDeArquivo() {}
    public ManipuladorDeArquivo(bool writeIndented) =>
        JSO = new JsonSerializerOptions { WriteIndented = writeIndented };

    #region Constantes
    private static readonly string tipoArquivoTemaPersonalizado = "tema";
    private static readonly string tipoArquivoCadatrosContadores = "cnt";
    private static readonly string tipoArquivoPadraoCronometros = "pcr";
    private static readonly string tipoArquivoExpressos = "crex";

    private static readonly string cadastro = "cadastro de";

    private static readonly string descricaoTemaPersonalizado = "Tema Personalizado";
    private static readonly string descricaoCadatrosContadores = "Lista de Contadores";
    private static readonly string descricaoPadraoCronometros = "Lista de Padrões de Cronômetro";
    private static readonly string descricaoExpressos = "Lista de Cronômetros Expressos";
    private static readonly string descricaoPonto = "Registro de Ponto";

    private static readonly string pastaPrograma = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly string pastaBackUp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Devnometro", "BackupLogs");
    private static readonly string arquivoPreferencias = "Preferencias.json";
    private static readonly string arquivoMarcadorDeAceite = "Microsoft.Extensions.Logging.Authorazition.dll";
    private static readonly string arquivoMarcadorDeAceiteOculto = "6ec3a36f206d657865";
    private static readonly string arquivoTemaPersonalizado = $"Tema.{tipoArquivoTemaPersonalizado}";
    private static readonly string arquivoPonto = "HistoricoPonto.json";
    //private static readonly string arquivoCronometros = "HistoricoTempo.json";
    //private static readonly string arquivoCronometrosAbertos = "Tempo.json";
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
    private static bool Limpar(string caminhoArquivo)
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
    private static T Carregar<T>(string caminhoArquivo, T itemReserva)
    {
        try
        {
            if (VerificarArquivo(caminhoArquivo))
            {
                string json = File.ReadAllText(Path.Combine(pastaPrograma, caminhoArquivo));
                T? lista = JsonSerializer.Deserialize<T>(json);
                return lista ?? itemReserva;
            }
        }
        catch { }

        return itemReserva;
    }
    private async Task<bool> SalvarAsync<T>(T item, string caminhoArquivo, string descricao)
    {
        try
        {
            string json = JsonSerializer.Serialize(item, JSO);
            string filePath = Path.Combine(pastaPrograma, caminhoArquivo);

            File.WriteAllText(filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            await MetodosEstaticos.MensagemAsync($"Erro ao salvar {descricao}: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }
    private async Task<T?> ImportarAsync<T>(string caminhoArquivo, string tipoArquivo, string descricao)
    {
        try
        {
            RetornoTaskImportar retorno = await Task.Run(() =>
            {
                RetornoTaskImportar ter = new();

                OpenFileDialog openFileDialog = new()
                {
                    Filter = $"Arquivos {tipoArquivo.ToUpper()} (*.{tipoArquivo.ToLower()})|*.{tipoArquivo.ToLower()}",
                    Title = $"Selecionar Arquivo de {descricao}"
                };

                bool? resposta = openFileDialog.ShowDialog();
                ter.Caminho = openFileDialog.SafeFileName;

                if (resposta == true)
                {
                    string json = File.ReadAllText(openFileDialog.FileName);

                    T? lista = JsonSerializer.Deserialize<T>(json);

                    if (lista != null)
                    {
                        string filePath = Path.Combine(pastaPrograma, caminhoArquivo);
                        File.WriteAllText(filePath, json);

                        ter.Retorno = lista;
                        ter.Sucesso = true;
                        return ter;
                    }
                    else
                        ter.Sucesso = false;
                }
                return ter;
            });
            if (retorno.Sucesso == true && retorno.Retorno is not null)
            {
                await MetodosEstaticos.MensagemAsync($"Arquivo {retorno.Caminho} importado com sucesso", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                return (T)retorno.Retorno;
            }
            else if (retorno.Sucesso == false)
                await MetodosEstaticos.MensagemAsync($"O arquivo selecionado não é {descricao} válida", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao importar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
        return default;
    }
    private async Task ExportarAsync<T>(T item, string tipoArquivo, string descricao)
    {
        try
        {
            await Task.Run(() =>
            {
                string json = JsonSerializer.Serialize(item, JSO);

                SaveFileDialog saveFileDialog = new()
                {
                    Filter = $"Arquivos {tipoArquivo.ToUpper()} (*.{tipoArquivo.ToLower()})|*.{tipoArquivo.ToLower()}",
                    DefaultExt = tipoArquivo.ToLower(),
                    Title = $"Salvar {descricao}"
                };

                bool? resposta = saveFileDialog.ShowDialog();
                if (resposta == true)
                    File.WriteAllText(saveFileDialog.FileName, json);
            });
        }
        catch (Exception ex) { await MetodosEstaticos.MensagemAsync($"Erro ao exportar o arquivo: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error); }
    }
    
    //private async Task ExportarPlanilhaAsync<T>(List<T> item, string tipoArquivo, string descricao, string filtro) { }
    #endregion

    #region Preferências
    public static Preferencias CarregarPreferencias()
    {
        Preferencias preferencias = Carregar<Preferencias>(arquivoPreferencias, Preferencias.PreferenciasPadroes());
        preferencias.TemaPersonalizado = CarregarTemaPersonalizado();
        preferencias.TemaPersonalizado.AplicarCoresAoTema();
        if (VerificarArquivo(arquivoMarcadorDeAceite))
        {
            preferencias.Ponto.AFD_HabilitarSimulacao = true;
            preferencias.Ponto.AFD_ConfirmacaoDeCiencia = true;
            try
            {
                string json = File.ReadAllText(Path.Combine(pastaPrograma, arquivoMarcadorDeAceite));
                DateTime? data = JsonSerializer.Deserialize<DateTime>(json);
                preferencias.Ponto.AFD_DataHoraUltimaConfirmacaoDeCiencia = data;
            } catch { }
        }
        else if (File.Exists(Path.Combine(pastaBackUp, arquivoMarcadorDeAceiteOculto)))
        {
            preferencias.Ponto.AFD_HabilitarSimulacao = true;
            preferencias.Ponto.AFD_ConfirmacaoDeCiencia = true;
            try
            {
                string json = File.ReadAllText(Path.Combine(pastaBackUp, arquivoMarcadorDeAceiteOculto));
                DateTime? data = JsonSerializer.Deserialize<DateTime>(json);
                preferencias.Ponto.AFD_DataHoraUltimaConfirmacaoDeCiencia = data;
            }
            catch { }
        }
        return preferencias;
    }
    public async Task<bool> SalvarPreferenciasAsync(Preferencias preferencias) => await SalvarAsync<Preferencias>(preferencias, arquivoPreferencias, "Preferencias");

    #region Tema Personalizado
    public static bool LimparTemaPersonalizado() => Limpar(arquivoTemaPersonalizado);
    public static TemaPersonalizado CarregarTemaPersonalizado() => Carregar<TemaPersonalizado>(arquivoTemaPersonalizado, TemaPersonalizado.CriarTemaPersonalizado());
    public async Task SalvarTemaPersonalizadoAsync(TemaPersonalizado temaPersonalizado) => await SalvarAsync<TemaPersonalizado>(temaPersonalizado, arquivoTemaPersonalizado, descricaoTemaPersonalizado);
    public async Task<TemaPersonalizado?> ImportarTemaPersonalizadoAsync() => await ImportarAsync<TemaPersonalizado>(arquivoTemaPersonalizado, tipoArquivoTemaPersonalizado, descricaoTemaPersonalizado);
    public async Task ExportarTemaPersonalizadoAsync(TemaPersonalizado temaPersonalizado) => await ExportarAsync<TemaPersonalizado>(temaPersonalizado, tipoArquivoTemaPersonalizado, descricaoTemaPersonalizado);
    #endregion

    #endregion


    #region Ponto
    public static void CarregarPontosDoDia() { }
    public static bool LimparPontos(string? filtro = null)
    {
        //Salvar para arquivo com timestamp antes de excluir
        if (filtro == null)
            return Limpar(arquivoPonto);
        else
            return false;
    }
    public static List<RegistroDePonto> CarregarPontos() => Carregar<List<RegistroDePonto>>(arquivoPonto, []);
    public async Task SalvarPontos(List<RegistroDePonto> lista) => await SalvarAsync<List<RegistroDePonto>>(lista, arquivoPonto, descricaoPonto);
    //public async Task ExportarPontosEmPlanilhaAsync(List<RegistroDePonto> lista) { }
    //public async Task ExportarPontosEmAfdtAsync(List<RegistroDePonto> lista) { }
    public async Task SalvarLogAceite(Preferencias preferencias)
    {
        try
        {
            string caminhoArquivo = Path.Combine(pastaPrograma, $"LogAceiteDeTermo {DateTime.Now:yyyy_MM_dd-HH_mm}.txt");
            string caminhoArquivoOculto = Path.Combine(pastaPrograma, $"_LogAceiteDeTermo_{DateTime.Now:yyyy_MM_dd-HH_mm}.txt");
            StringBuilder conteudo = new();
            conteudo.AppendLine("[REGISTRO DE ACEITE]");
            conteudo.AppendLine($"Em { DateTime.Now} o usuário confirmou o termo de ciência sobre a simulação de AFD, declarando estar ciente de que:");
            conteudo.AppendLine("-O arquivo gerado não possui validade legal");
            conteudo.AppendLine("-Não substitui um sistema de registro oficial");
            conteudo.AppendLine("-O uso é restrito a fins de teste / análise");
            string texto = conteudo.ToString();
            try
            {
                conteudo.AppendLine("");
                conteudo.AppendLine($"Usuário Windows: {Environment.UserName}");
                conteudo.AppendLine($"(Domínio: {Environment.UserDomainName})");
                conteudo.AppendLine($"Máquina: {Environment.MachineName}");
                texto = conteudo.ToString();
            }
            catch {}

            File.WriteAllText(caminhoArquivo, texto);
            File.SetAttributes(caminhoArquivo, FileAttributes.ReadOnly);

            File.WriteAllText(caminhoArquivoOculto, texto);
            File.SetAttributes(caminhoArquivoOculto, FileAttributes.ReadOnly | FileAttributes.Hidden);

            string marcadorDeAceite = Path.Combine(pastaPrograma, arquivoMarcadorDeAceite);
            string conteudoMarcador = JsonSerializer.Serialize(DateTime.Now, JSO);
            
            File.WriteAllText(marcadorDeAceite, conteudoMarcador);
            File.SetAttributes(marcadorDeAceite, FileAttributes.ReadOnly | FileAttributes.Hidden);

            Directory.CreateDirectory(pastaBackUp);

            string caminhoBackup = Path.Combine(pastaBackUp, $"{DateTime.Now:yyyyMMddHHmmss} LogAceiteDeTermo.txt");
            File.WriteAllText(caminhoBackup, conteudo.ToString());
            File.SetAttributes(caminhoBackup, FileAttributes.ReadOnly);

            string caminhoBackupOculto = Path.Combine(pastaBackUp, $"_{DateTime.Now:yyyyMMddHHmmss} LogAceiteDeTermo.txt");
            File.WriteAllText(caminhoBackupOculto, conteudo.ToString());
            File.SetAttributes(caminhoBackupOculto, FileAttributes.ReadOnly | FileAttributes.Hidden);

            string marcadorBackup = Path.Combine(pastaBackUp, arquivoMarcadorDeAceiteOculto);
            File.WriteAllText(marcadorBackup, conteudoMarcador);
            File.SetAttributes(marcadorBackup, FileAttributes.ReadOnly | FileAttributes.Hidden);

            await SalvarPreferenciasAsync(preferencias);
        }
        catch { }
    }

    #endregion

    #region Cronômetro
    public static void LimparCronometros(string? filtro = null) {}
    public static void CarregarCronometros() {}
    public static void CarregarCronometrosAbertos() {}
    public static void SalvarCronometros() {}
    public static void ExportarCronometros(string lista) {}
    #endregion


    #region Cadastros

    #region Contadores
    public static bool LimparContadores() => Limpar(arquivoCadatrosContadores);
    public List<ContadorModel> CarregarContadores() => Carregar<List<ContadorModel>>(arquivoCadatrosContadores, ContadorModel.ListaMocada());
    public async Task SalvarContadoresAsync(List<ContadorModel> lista) => await SalvarAsync<List<ContadorModel>>(lista, arquivoCadatrosContadores, $"{cadastro} {descricaoCadatrosContadores}");
    public async Task<List<ContadorModel>?> ImportarContadoresAsync() => await ImportarAsync<List<ContadorModel>>(arquivoCadatrosContadores, tipoArquivoCadatrosContadores, descricaoCadatrosContadores);
    public async Task ExportarContadoresAsync(List<ContadorModel> lista) => await ExportarAsync<List<ContadorModel>>(lista, tipoArquivoCadatrosContadores, descricaoCadatrosContadores);
    #endregion

    #region Padrões de Cronômetros
    public static bool LimparPadroesCronometros() => Limpar(arquivoCadatrosPadraoCronometros);
    public List<PadraoDeCronometroModel> CarregarPadroesCronometros() => Carregar<List<PadraoDeCronometroModel>>(arquivoCadatrosPadraoCronometros, PadraoDeCronometroModel.ListaMocada());
    public async Task SalvarPadroesCronometrosAsync(List<PadraoDeCronometroModel> lista) => await SalvarAsync<List<PadraoDeCronometroModel>>(lista, arquivoCadatrosPadraoCronometros, $"{cadastro} {descricaoPadraoCronometros}");
    public async Task<List<PadraoDeCronometroModel>?> ImportarPadroesCronometrosAsync() => await ImportarAsync<List<PadraoDeCronometroModel>>(arquivoCadatrosPadraoCronometros, tipoArquivoPadraoCronometros, descricaoPadraoCronometros);
    public async Task ExportarPadroesCronometrosAsync(List<PadraoDeCronometroModel> lista) => await ExportarAsync<List<PadraoDeCronometroModel>>(lista, tipoArquivoPadraoCronometros, descricaoPadraoCronometros);
    #endregion

    #region Cronômetros Expressos
    public static bool LimparExpressos() => Limpar(arquivoCadatrosExpressos);
    public List<ExpressoModel> CarregarExpressos() => Carregar<List<ExpressoModel>>(arquivoCadatrosExpressos, ExpressoModel.ListaMocada());
    public async Task SalvarExpressosAsync(List<ExpressoModel> lista) => await SalvarAsync<List<ExpressoModel>>(lista, arquivoCadatrosExpressos, $"{cadastro} {descricaoExpressos}");
    public async Task<List<ExpressoModel>?> ImportarExpressosAsync() => await ImportarAsync<List<ExpressoModel>>(arquivoCadatrosExpressos, tipoArquivoExpressos, descricaoExpressos);
    public async Task ExportarExpressosAsync(List<ExpressoModel> lista) => await ExportarAsync<List<ExpressoModel>>(lista, tipoArquivoExpressos, descricaoExpressos);
    #endregion

    #endregion
}

class RetornoTaskImportar
{
    public bool? Sucesso { get; set; }
    public object? Retorno { get; set; }
    public string? Caminho { get; set; }
}