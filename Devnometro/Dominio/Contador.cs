using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Devnometro.Dominio;

public class Contador
{
    public Contador() {}
    public Contador(bool mocar)
    {
        if (mocar)
        {
            Nome = "Novo contador";
            Descricao = "Explique a atividade que esse contador irá monitorar";
            Icone = MudBlazor.Icons.Material.Filled.QuestionMark;
        }
    }

    public int Seq { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Icone { get; set; } = string.Empty;
    public MudBlazor.Color CorIcone { get; set; } = MudBlazor.Color.Dark;
    public bool ContaTempo { get; set; } = true;

    public TimeSpan DeltaT { get; set; }
    public DateTime Tzero { get; set; }

    
    [JsonIgnore] public bool EmAtividade { get; set; }
    [JsonIgnore] public bool ConfirmacaoPendente { get; set; } = false;
    [JsonIgnore] public string ContaTempoString { get => ContaTempo ? "Sim" : "Não"; }
    [JsonIgnore] public Timer? Timer { get; set; }

    public static List<Contador> ListaMocada()
    {
        return 
        [
            new Contador
            {
                Nome = "Requerimento",
                Descricao = "Fase de entendimento e análise dos requisitos do projeto ou tarefa. Inclui reuniões com stakeholders, levantamento de necessidades e definição de escopo.",
                ContaTempo = true,
                Icone = MudBlazor.Icons.Material.Filled.ContentPasteSearch,
                CorIcone = MudBlazor.Color.Primary
            },
            new Contador
            {
                Nome = "Desenho",
                Descricao = "Criação de diagramas, arquitetura e design da solução. Planejamento de como o sistema ou funcionalidade será implementado.",
                ContaTempo = true,
                Icone = MudBlazor.Icons.Material.Filled.DesignServices,
                CorIcone = MudBlazor.Color.Secondary
            },
            new Contador
            {
                Nome = "Desenvolvimento",
                Descricao = "Implementação do código, seguindo as especificações e boas práticas. Inclui programação, integração de APIs, e configuração de ferramentas.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.Code,
                CorIcone = MudBlazor.Color.Dark
            },
            new Contador
            {
                Nome = "Testes",
                Descricao = "Verificação da qualidade do código e da funcionalidade implementada. Pode incluir testes unitários, integração, manuais ou automatizados.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.Ballot,
                CorIcone = MudBlazor.Color.Primary
            },
            new Contador
            {
                Nome = "Documentação",
                Descricao = "Elaboração de documentos técnicos, manuais de uso ou registros de decisões. Importante para manter o conhecimento organizado e acessível.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.AutoStories,
                CorIcone = MudBlazor.Color.Secondary
            },
            new Contador
            {
                Nome = "Implantação",
                Descricao = "Publicação da solução em ambiente de produção ou staging. Inclui deploy, configuração de servidores e monitoramento inicial.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.Commit,
                CorIcone = MudBlazor.Color.Secondary
            },
            new Contador
            {
                Nome = "Aguardando",
                Descricao = "Tempo ocioso enquanto se espera por feedback, aprovações ou resolução de dependências externas.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.AccessTimeFilled,
                CorIcone = MudBlazor.Color.Warning
            },
            new Contador
            {
                Nome = "Aprendendo",
                Descricao = "Tempo desenvolvendo o conhecimento ou habilidade necessários para execução da tarefa.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.School,
                CorIcone = MudBlazor.Color.Info
            },
            new Contador
            {
                Nome = "AjudandoOutros",
                Descricao = "Tempo dedicado a auxiliar colegas com dúvidas, revisão de código ou resolução de problemas.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.EscalatorWarning,
                CorIcone = MudBlazor.Color.Info
            },
            new Contador
            {
                Nome = "TrabalhoParalelo",
                Descricao = "Atividades secundárias que não estão diretamente relacionadas ao projeto principal.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.AutoAwesomeMotion,
                CorIcone = MudBlazor.Color.Warning
            },
            new Contador
            {
                Nome = "Chamado da Natureza",
                Descricao = "Interrupções inevitáveis, como alimentação, manutenção biológica ou emergências pessoais.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.Dining,
                CorIcone = MudBlazor.Color.Info
            },
            new Contador
            {
                Nome = "Outras Interrupções",
                Descricao = "Qualquer interrupção ou atividade que não se encaixe nas categorias anteriores.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.AllInclusive,
                CorIcone = MudBlazor.Color.Warning
            },
            new Contador
            {
                Nome = "Daily",
                Descricao = "Participação na reunião diária de acompanhamento. Momento para compartilhar progressos, planejar o dia e identificar bloqueios.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.Event,
                CorIcone = MudBlazor.Color.Info
            },
            new Contador
            {
                Nome = "Atualizando Banco",
                Descricao = "Atualização ou manutenção do banco de dados, como migrações, backups ou ajustes de schemas.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.Article,
                CorIcone = MudBlazor.Color.Info
            },
            new Contador
            {
                Nome = "Subindo versão",
                Descricao = "Publicação de novas versões do software em ambientes de teste ou produção.",
                ContaTempo = false,
                Icone = MudBlazor.Icons.Material.Filled.Backup,
                CorIcone = MudBlazor.Color.Info
            },
        ];
    }
}