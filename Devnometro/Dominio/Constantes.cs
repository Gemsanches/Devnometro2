namespace Devnometro.Dominio;

public static class PrefereciaPadrao
{
    #region Geral
    public const int Exibicao = 2;
    public const bool LigarJunto = false;
    public const string CaminhoAtendimento = "https://gr3bhelp.freshdesk.com/a/tickets/";
    public const string CaminhoDesenvolvimento = "https://gr3b.visualstudio.com/S.Adm/_workitems/edit/";
    #endregion

    #region Salvamento Automático
    public const int AutoSaveTempo = 1;
    public const bool AutoSave = true;
    public const bool AutoSaveMarcarPonto = true;
    public const bool AutoSaveEditarPonto = true;
    public const bool AutoSaveExcluirPonto = true;
    public const bool AutoSaveCriarCronometro = false;
    public const bool AutoSaveEditarCronometro = true;
    public const bool AutoSaveExcluirCronometro = false;
    public const bool AutoSaveFinalizarCronometro = true;
    public const bool AutoSaveAbandonarCronometro = true;
    #endregion

    #region Comunicação
    public const bool AvisarFecharPrograma = false;

    public const bool AvisarIniciarExpotacaoPonto = false;
    public const bool AvisarSucessoExpotacaoPonto = true;
    public const bool AvisarIniciarExpotacaoTempo = false;
    public const bool AvisarSucessoExpotacaoTempo = true;

    public const bool AvisarIniciarCriacaoPonto = false;
    public const bool AvisarSucessoCriacaoPonto = false;
    public const bool AvisarIniciarExclusaoPonto = true;
    public const bool AvisarSucessoExclusaoPonto = true;
    public const bool AvisarIniciarEdicaoPonto = false;
    public const bool AvisarSucessoEdicaoPonto = false;

    public const bool AvisarIniciarCriacaoCronometro = false;
    public const bool AvisarSucessoCriacaoCronometro = false;
    public const bool AvisarIniciarExclusaoCronometro = false;
    public const bool AvisarSucessoExclusaoCronometro = false;
    public const bool AvisarIniciarEdicaoCronometro = false;
    public const bool AvisarSucessoEdicaoCronometro = false;
    public const bool AvisarIniciarAbandonoCronometro = true;
    public const bool AvisarSucessoAbandonoCronometro = false;
    public const bool AvisarIniciarFinalizacaoCronometro = false;
    public const bool AvisarSucessoFinalizacaoCronometro = false;
    #endregion

    #region Ponto
    public const int PontoTempoBatidaRepetida = 60;
    public const bool PontoPrimeiroAutomatico = false;
    public const bool PontoAutoExportarComecoDoMes = true;
    public const string CaminhoPonto = "";
    #endregion

    #region Cronômetro
    public const int CronometrosSimultaneos = 5;
    public const bool CronometroExcluirAutomaticamente = true;
    public const bool CronometroApenasUmAtivo = true;
    public const string CaminhoCronometro = "";
    #endregion
}

public static class TooltipPadrao
{
    public const int delay = 1000;
    public const int duration = 2000;
    public const MudBlazor.Color corTolltip = MudBlazor.Color.Info;
}

public static class ContadoresPreCadastrados
{
    //Guid("00000000-4335-454e-9949-00000000000X")
    public static ContadorModel Requerimento
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000000"),
            Nome = "Requerimento",
            Descricao = "Fase de entendimento e análise dos requisitos do projeto ou tarefa. Inclui reuniões com stakeholders, levantamento de necessidades e definição de escopo.",
            ContaTempo = true,
            Icone = MudBlazor.Icons.Material.Filled.ContentPasteSearch,
            CorIcone = MudBlazor.Color.Primary
        };
    }
    public static ContadorModel Desenho
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000001"),
            Nome = "Desenho",
            Descricao = "Criação de diagramas, arquitetura e design da solução. Planejamento de como o sistema ou funcionalidade será implementado.",
            ContaTempo = true,
            Icone = MudBlazor.Icons.Material.Filled.DesignServices,
            CorIcone = MudBlazor.Color.Secondary
        };
    }
    public static ContadorModel Desenvolvimento
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000002"),
            Nome = "Desenvolvimento",
            Descricao = "Implementação do código, seguindo as especificações e boas práticas. Inclui programação, integração de APIs, e configuração de ferramentas.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.Code,
            CorIcone = MudBlazor.Color.Dark
        };
    }
    public static ContadorModel Testes
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000003"),
            Nome = "Testes",
            Descricao = "Verificação da qualidade do código e da funcionalidade implementada. Pode incluir testes unitários, integração, manuais ou automatizados.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.Ballot,
            CorIcone = MudBlazor.Color.Primary
        };
    }
    public static ContadorModel Documentacao
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000004"),
            Nome = "Documentação",
            Descricao = "Elaboração de documentos técnicos, manuais de uso ou registros de decisões. Importante para manter o conhecimento organizado e acessível.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.AutoStories,
            CorIcone = MudBlazor.Color.Secondary
        };
    }
    public static ContadorModel Implantacao
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000005"),
            Nome = "Implantação",
            Descricao = "Publicação da solução em ambiente de produção ou staging. Inclui deploy, configuração de servidores e monitoramento inicial.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.Commit,
            CorIcone = MudBlazor.Color.Secondary
        };
    }

    public static ContadorModel Aguardando
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000006"),
            Nome = "Aguardando",
            Descricao = "Tempo ocioso enquanto se espera por feedback, aprovações ou resolução de dependências externas.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.AccessTimeFilled,
            CorIcone = MudBlazor.Color.Warning
        };
    }
    public static ContadorModel Aprendendo
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000007"),
            Nome = "Aprendendo",
            Descricao = "Tempo desenvolvendo o conhecimento ou habilidade necessários para execução da tarefa.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.School,
            CorIcone = MudBlazor.Color.Info
        };
    }
    public static ContadorModel AjudandoOutros
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000008"),
            Nome = "Ajudando Outros",
            Descricao = "Tempo dedicado a auxiliar colegas com dúvidas, revisão de código ou resolução de problemas.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.EscalatorWarning,
            CorIcone = MudBlazor.Color.Info
        };
    }
    public static ContadorModel TrabalhoParalelo
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000009"),
            Nome = "Trabalho Paralelo",
            Descricao = "Atividades secundárias que não estão diretamente relacionadas ao projeto principal.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.AutoAwesomeMotion,
            CorIcone = MudBlazor.Color.Warning
        };
    }
    public static ContadorModel ChamadoNatureza
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-00000000000a"),
            Nome = "Chamado da Natureza",
            Descricao = "Interrupções inevitáveis, como alimentação, manutenção biológica ou emergências pessoais.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.Dining,
            CorIcone = MudBlazor.Color.Info
        };
    }
    public static ContadorModel OutrasInterrupcoes
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-00000000000b"),
            Nome = "Outras Interrupções",
            Descricao = "Qualquer interrupção ou atividade que não se encaixe nas categorias anteriores.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.AllInclusive,
            CorIcone = MudBlazor.Color.Warning
        };
    }

    public static ContadorModel Daily
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-00000000000c"),
            Nome = "Daily",
            Descricao = "Participação na reunião diária de acompanhamento. Momento para compartilhar progressos, planejar o dia e identificar bloqueios.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.Event,
            CorIcone = MudBlazor.Color.Info
        };
    }
    public static ContadorModel AtualizandoBanco
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-00000000000d"),
            Nome = "Atualizando Banco",
            Descricao = "Atualização ou manutenção do banco de dados, como migrações, backups ou ajustes de schemas.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.Article,
            CorIcone = MudBlazor.Color.Info
        };
    }
    public static ContadorModel SubindoVersao
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-00000000000e"),
            Nome = "Subindo versão",
            Descricao = "Publicação de novas versões do software em ambientes de teste ou produção.",
            ContaTempo = false,
            Icone = MudBlazor.Icons.Material.Filled.Backup,
            CorIcone = MudBlazor.Color.Info
        };
    }
}

public static class ExpressosPreCadastrados
{
    //Guid("00000000-4335-454e-9949-10000000000X")
    public static ExpressoModel Daily
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-100000000000"),
            Nome = ContadoresPreCadastrados.Daily.Nome,
            Contador = ContadoresPreCadastrados.Daily,
        };
    }
    public static ExpressoModel AtualizandoBanco
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-100000000001"),
            Nome = ContadoresPreCadastrados.AtualizandoBanco.Nome,
            Contador = ContadoresPreCadastrados.AtualizandoBanco,
        };
    }
    public static ExpressoModel SubindoVersao
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-100000000002"),
            Nome = ContadoresPreCadastrados.SubindoVersao.Nome,
            Contador = ContadoresPreCadastrados.SubindoVersao,
        };
    }
    public static ExpressoModel AjudandoOutros
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-100000000003"),
            Nome = ContadoresPreCadastrados.AjudandoOutros.Nome,
            Contador = ContadoresPreCadastrados.AjudandoOutros,
        };
    }
    public static ExpressoModel ChamadoNatureza
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-100000000004"),
            Nome = ContadoresPreCadastrados.ChamadoNatureza.Nome,
            Contador = ContadoresPreCadastrados.ChamadoNatureza,
        };
    }
    public static ExpressoModel TrabalhoParalelo
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-10000000000X"),
            Nome = ContadoresPreCadastrados.TrabalhoParalelo.Nome,
            Contador = ContadoresPreCadastrados.TrabalhoParalelo,
        };
    }
}

public static class PadroesCronometroPreCadastrados
{
    //Guid("00000000-4335-454e-9949-20000000000X")
    public static PadraoDeCronometroModel Desenvolvedor
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-200000000000"),
            Nome = "Desenvolvedor",
            Contadores = 
            [
                ContadoresPreCadastrados.Requerimento,
                ContadoresPreCadastrados.Desenho,
                ContadoresPreCadastrados.Desenvolvimento,
                ContadoresPreCadastrados.Testes,
                ContadoresPreCadastrados.Documentacao,
                ContadoresPreCadastrados.Implantacao,

                ContadoresPreCadastrados.Aguardando,
                ContadoresPreCadastrados.Aprendendo,
                ContadoresPreCadastrados.OutrasInterrupcoes
            ],
            IndicePlayPadrao = 2,
            IncidePausePadrao = 8
        };
    }
    public static PadraoDeCronometroModel Revisor
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-200000000001"),
            Nome = "Revisor",
            Contadores = 
            [
                ContadoresPreCadastrados.Requerimento,
                ContadoresPreCadastrados.Testes,
                ContadoresPreCadastrados.Documentacao,
                ContadoresPreCadastrados.Implantacao,
                ContadoresPreCadastrados.Desenvolvimento,

                ContadoresPreCadastrados.Aguardando,
                ContadoresPreCadastrados.OutrasInterrupcoes
            ],
            IndicePlayPadrao = 1,
            IncidePausePadrao = 6
        };
    }
    public static PadraoDeCronometroModel Homologador
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-200000000002"),
            Nome = "Homologador",
            Contadores =
            [
                ContadoresPreCadastrados.Requerimento,
                ContadoresPreCadastrados.Documentacao,
                ContadoresPreCadastrados.Implantacao,
                ContadoresPreCadastrados.Testes,
                ContadoresPreCadastrados.Desenvolvimento,

                ContadoresPreCadastrados.Aguardando,
                ContadoresPreCadastrados.OutrasInterrupcoes
            ],
            IndicePlayPadrao = 1,
            IncidePausePadrao = 6
        };
    }
    public static PadraoDeCronometroModel Suporte
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-200000000003"),
            Nome = "Suporte",
            Contadores =
            [
                ContadoresPreCadastrados.Requerimento,
                ContadoresPreCadastrados.Testes,
                ContadoresPreCadastrados.Documentacao,

                ContadoresPreCadastrados.Aguardando,
                ContadoresPreCadastrados.Aprendendo,
                ContadoresPreCadastrados.OutrasInterrupcoes
            ],
            IndicePlayPadrao = 2,
            IncidePausePadrao = 5
        };
    }
}