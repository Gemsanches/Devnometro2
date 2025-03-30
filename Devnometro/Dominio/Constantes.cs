using Devnometro.Dominio.Enumeradores;

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
    public static ContadorModel Espacador
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000000"),
            Padrao = true,
            Nome = "",
            Descricao = "",
            ContaTempo = false,
            Icone = EIcone.Commit,
            IconeCor = MudBlazor.Color.Transparent
        };
    }
    public static ContadorModel Requerimento
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000001"),
            Padrao = true,
            Nome = "Requerimento",
            Descricao = "Fase de entendimento e análise dos requisitos do projeto ou tarefa. Inclui reuniões com stakeholders, levantamento de necessidades e definição de escopo.",
            ContaTempo = true,
            Icone = EIcone.ContentPasteSearch,
            IconeCor = MudBlazor.Color.Primary
        };
    }
    public static ContadorModel Desenho
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000002"),
            Padrao = true,
            Nome = "Desenho",
            Descricao = "Criação de diagramas, arquitetura e design da solução. Planejamento de como o sistema ou funcionalidade será implementado.",
            ContaTempo = true,
            Icone = EIcone.DesignServices,
            IconeCor = MudBlazor.Color.Secondary
        };
    }
    public static ContadorModel Desenvolvimento
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000003"),
            Padrao = true,
            Nome = "Desenvolvimento",
            Descricao = "Implementação do código, seguindo as especificações e boas práticas. Inclui programação, integração de APIs, e configuração de ferramentas.",
            ContaTempo = false,
            Icone = EIcone.Code,
            IconeCor = MudBlazor.Color.Dark
        };
    }
    public static ContadorModel Testes
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000004"),
            Padrao = true,
            Nome = "Testes",
            Descricao = "Verificação da qualidade do código e da funcionalidade implementada. Pode incluir testes unitários, integração, manuais ou automatizados.",
            ContaTempo = false,
            Icone = EIcone.Ballot,
            IconeCor = MudBlazor.Color.Primary
        };
    }
    public static ContadorModel Documentacao
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000005"),
            Padrao = true,
            Nome = "Documentação",
            Descricao = "Elaboração de documentos técnicos, manuais de uso ou registros de decisões. Importante para manter o conhecimento organizado e acessível.",
            ContaTempo = false,
            Icone = EIcone.AutoStories,
            IconeCor = MudBlazor.Color.Secondary
        };
    }
    public static ContadorModel Implantacao
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000006"),
            Padrao = true,
            Nome = "Implantação",
            Descricao = "Publicação da solução em ambiente de produção ou staging. Inclui deploy, configuração de servidores e monitoramento inicial.",
            ContaTempo = false,
            Icone = EIcone.Commit,
            IconeCor = MudBlazor.Color.Secondary
        };
    }

    public static ContadorModel Aguardando
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000007"),
            Padrao = true,
            Nome = "Aguardando",
            Descricao = "Tempo ocioso enquanto se espera por feedback, aprovações ou resolução de dependências externas.",
            ContaTempo = false,
            Icone = EIcone.AccessTimeFilled,
            IconeCor = MudBlazor.Color.Warning
        };
    }
    public static ContadorModel Aprendendo
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000008"),
            Padrao = true,
            Nome = "Aprendendo",
            Descricao = "Tempo desenvolvendo o conhecimento ou habilidade necessários para execução da tarefa.",
            ContaTempo = false,
            Icone = EIcone.School,
            IconeCor = MudBlazor.Color.Info
        };
    }
    public static ContadorModel AjudandoOutros
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-000000000009"),
            Padrao = true,
            Nome = "Ajudando Outros",
            Descricao = "Tempo dedicado a auxiliar colegas com dúvidas, revisão de código ou resolução de problemas.",
            ContaTempo = false,
            Icone = EIcone.EscalatorWarning,
            IconeCor = MudBlazor.Color.Info
        };
    }
    public static ContadorModel TrabalhoParalelo
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-00000000000a"),
            Padrao = true,
            Nome = "Trabalho Paralelo",
            Descricao = "Atividades secundárias que não estão diretamente relacionadas ao projeto principal.",
            ContaTempo = false,
            Icone = EIcone.AutoAwesomeMotion,
            IconeCor = MudBlazor.Color.Warning
        };
    }
    public static ContadorModel ChamadoNatureza
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-00000000000b"),
            Padrao = true,
            Nome = "Chamado da Natureza",
            Descricao = "Interrupções inevitáveis, como alimentação, manutenção biológica ou emergências pessoais.",
            ContaTempo = false,
            Icone = EIcone.Dining,
            IconeCor = MudBlazor.Color.Info
        };
    }
    public static ContadorModel OutrasInterrupcoes
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-00000000000c"),
            Padrao = true,
            Nome = "Outras Interrupções",
            Descricao = "Qualquer interrupção ou atividade que não se encaixe nas categorias anteriores.",
            ContaTempo = false,
            Icone = EIcone.AllInclusive,
            IconeCor = MudBlazor.Color.Warning
        };
    }

    public static ContadorModel Daily
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-00000000000d"),
            Padrao = true,
            Nome = "Daily",
            Descricao = "Participação na reunião diária de acompanhamento. Momento para compartilhar progressos, planejar o dia e identificar bloqueios.",
            ContaTempo = false,
            Icone = EIcone.Event,
            IconeCor = MudBlazor.Color.Info
        };
    }
    public static ContadorModel AtualizandoBanco
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-00000000000e"),
            Padrao = true,
            Nome = "Atualizando Banco",
            Descricao = "Atualização ou manutenção do banco de dados, como migrações, backups ou ajustes de schemas.",
            ContaTempo = false,
            Icone = EIcone.Article,
            IconeCor = MudBlazor.Color.Info
        };
    }
    public static ContadorModel SubindoVersao
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-00000000000f"),
            Padrao = true,
            Nome = "Subindo versão",
            Descricao = "Publicação de novas versões do software em ambientes de teste ou produção.",
            ContaTempo = false,
            Icone = EIcone.Backup,
            IconeCor = MudBlazor.Color.Info
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
            Padrao = true,
            Nome = ContadoresPreCadastrados.Daily.Nome,
            Contador = ContadoresPreCadastrados.Daily,
        };
    }
    public static ExpressoModel AtualizandoBanco
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-100000000001"),
            Padrao = true,
            Nome = ContadoresPreCadastrados.AtualizandoBanco.Nome,
            Contador = ContadoresPreCadastrados.AtualizandoBanco,
        };
    }
    public static ExpressoModel SubindoVersao
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-100000000002"),
            Padrao = true,
            Nome = ContadoresPreCadastrados.SubindoVersao.Nome,
            Contador = ContadoresPreCadastrados.SubindoVersao,
        };
    }
    public static ExpressoModel AjudandoOutros
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-100000000003"),
            Padrao = true,
            Nome = ContadoresPreCadastrados.AjudandoOutros.Nome,
            Contador = ContadoresPreCadastrados.AjudandoOutros,
        };
    }
    public static ExpressoModel ChamadoNatureza
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-100000000004"),
            Padrao = true,
            Nome = ContadoresPreCadastrados.ChamadoNatureza.Nome,
            Contador = ContadoresPreCadastrados.ChamadoNatureza,
        };
    }
    public static ExpressoModel TrabalhoParalelo
    {
        get => new()
        {
            Id = new Guid("00000000-4335-454e-9949-100000000005"),
            Padrao = true,
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
                Padrao = true,
                Nome = "Desenvolvedor",
                Contadores =
                [
                ContadoresPreCadastrados.Requerimento,
                ContadoresPreCadastrados.Desenho,
                ContadoresPreCadastrados.Desenvolvimento,
                ContadoresPreCadastrados.Testes,
                ContadoresPreCadastrados.Documentacao,
                ContadoresPreCadastrados.Implantacao,

                ContadoresPreCadastrados.Espacador,

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
            Padrao = true,
            Nome = "Revisor",
            Contadores =
            [
                ContadoresPreCadastrados.Requerimento,
                ContadoresPreCadastrados.Testes,
                ContadoresPreCadastrados.Documentacao,

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
            Padrao = true,
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
            Padrao = true,
            Nome = "Suporte",
            Contadores =
            [
                ContadoresPreCadastrados.Espacador,

                ContadoresPreCadastrados.Requerimento,
                ContadoresPreCadastrados.Testes,
                ContadoresPreCadastrados.Documentacao,

                ContadoresPreCadastrados.Espacador,
                ContadoresPreCadastrados.Espacador,

                ContadoresPreCadastrados.Aguardando,
                ContadoresPreCadastrados.Aprendendo,
                ContadoresPreCadastrados.OutrasInterrupcoes
            ],
            IndicePlayPadrao = 2,
            IncidePausePadrao = 5
        };
    }
}

public static class DadosAGD_V1
{
    //Cabeçalho
    public const string R1_1_NSR = "000000000";
    public const string R1_2_TipoRegistro = "1";
    public const string R1_3_IdentificadorEmpregador = "1"; //1:CNPJ / 2:CPF
    public const string R1_4_CnpjCpfEmpregador = "12345678000199";
    public const string R1_5_CeiEmpregador = "000000000000";
    public const string R1_6_RazaoSocial = "ARQUIVO SIMULADO - SEM VALIDADE LEGAL                                                                                                                 ";
    public const string R1_7_Serie = "00000000000000001";
    public const string R1_8_DataInicio = "ddMMyyyy";
    public const string R1_9_DataFim = "ddMMyyyy";
    public const string R1_10_DataHoraGeracao = "ddMMyyyyHHmm";

    //Empresa
    public const string R2_1_NSR = "000000001";
    public const string R2_2_TipoRegistro = "2";
    public const string R2_3_DataHoraGravacao = "ddMMyyyyHHmm";
    public const string R2_4_IdentificadorEmpregador = R1_3_IdentificadorEmpregador;
    public const string R2_5_CnpjCpfEmpregador = R1_4_CnpjCpfEmpregador;
    public const string R2_6_CeiEmpregador = R1_5_CeiEmpregador;
    public const string R2_7_RazaoSocial = R1_6_RazaoSocial;
    public const string R2_8_Local = "                                                                                                    ";

    //Registro marcação de ponto
    public const string R3_1_NsrPrimeiroRegistro = "000000002";
    public const string R3_2_TipoRegistro = "3";
    public const string R3_3_DataHora = "ddMMyyyyHHmm";
    public const string R3_4_Pis = "000000000000";

    //Registro alteração de ponto
    public const string R4_1_NSR = "00000000X";
    public const string R4_2_TipoRegistro = "4";
    public const string R4_3_DataHoraAntes = "ddMMyyyyHHmm";
    public const string R4_4_DataHoraDepois = "ddMMyyyyHHmm";

    //Registro de inclusão ou alteração ou exclusão de empregado da MT do REP
    public const string R5_1_NSR = "00000000X";
    public const string R5_2_TipoRegistro = "5";
    public const string R5_3_DataHora = "ddMMyyyyHHmm";
    public const string R5_4_TipoOperacao = "I"; //I:inclusão / A:alteração / E:exclusão
    public const string R5_5_Pis = R3_4_Pis;
    public const string R5_6_Empregado = "Lindo usuario do Devnometro                         ";

    //Trailer
    public const string R6_1_NSR = "999999999";
    public const string R6_2_QtdeR2 = "000000001";
    public const string R6_3_QtdeR3 = "00000000X";
    public const string R6_4_QtdeR4 = "00000000X";
    public const string R6_5_QtdeR5 = "00000000X";
    public const string R6_6_TipoRegistro = "9";
}
public static class DadosAGD_V3
{
    //Cabeçalho
    public const string Cab1Padrao = "000000000";
    public const string Cab2TipoRegistro = "1";
    public const string Cab3IdentificadorEmpregador = "1"; //1:CNPJ / 2:CPF
    public const string Cab4CnpjCpfEmpregador = "12345678000199";
    public const string Cab5CnoEmpregador = "00000000000000";
    public const string Cab6RazaoSocial = "ARQUIVO SIMULADO - SEM VALIDADE LEGAL                                                                                                                 ";
    public const string Cab7Rep = "99999999999999999";
    public const string Cab11Versao = "003";
    public const string Cab12Fab = "2";
    public const string Cab13CnpjCpf = "00012345678901";


    public const int NsrInicial = 1;
    public const int TipoRegistro = 3;
    public const string PisPadrao = "000000000000";
}