namespace Devnometro.Dominio.Enumeradores;

/// <summary>
/// Enum que representa os diferentes tipos de registro de uma batida de ponto.
/// <para>
/// <list type="bullet">
///   <item><description><b>SemRegistro</b>: Nenhum registro de batida foi detectado ou registrado.</description></item>
///   <item><description><b>Entrada</b>: Registro de entrada, indicando o momento em que o colaborador iniciou seu turno ou atividades.</description></item>
///   <item><description><b>Saida</b>: Registro de saída, indicando o momento em que o colaborador encerrou seu turno ou atividades.</description></item>
///   <item><description><b>Duplicada</b>: Batida duplicada, indicando que um registro foi feito muito perto do anterior.</description></item>
/// </list>
/// </para>
/// </summary>
public enum TipoBatida
{
    SemRegistro,
    Entrada,
    Saida,
    Duplicada
}

/// <summary>
/// Enum que representa os diferentes status de registro de uma batida de ponto.
/// <para>
/// <list type="bullet">
///   <item><description><b>Criada</b>: Registro criado em memória, mas ainda não salvo.</description></item>
///   <item><description><b>Registrada</b>: Registro efetivado.</description></item>
///   <item><description><b>Editada</b>: Registro editado, deve manter os dados originais e uma justificativa.</description></item>
///   <item><description><b>Excluida</b>: Registro excluído logicamente, mas preservado em arquivo.</description></item>
/// </list>
/// </para>
/// </summary>
public enum StatusBatida
{
    Criada = 0,
    Registrada = 10,
    Editada = 20,
    Excluida = 21,
}
