namespace CSharpAcademy.API.Application.Services;

/// <summary>
/// Algoritmo de espaçamento (SM-2 simplificado).
/// NivelRetencao 1→5 mapeia para intervalos crescentes.
/// Acerto: sobe nível, intervalo aumenta.
/// Erro: volta ao nível 1, revisão em 1 dia.
/// </summary>
public static class SrsHelper
{
    private static readonly int[] Intervalos = [1, 3, 7, 14, 30];

    public static int IntervaloDias(int nivelRetencao)
        => Intervalos[Math.Clamp(nivelRetencao - 1, 0, Intervalos.Length - 1)];

    public static (int novoNivel, DateTime proximaRevisao) Acertou(int nivelAtual)
    {
        var novoNivel = Math.Min(nivelAtual + 1, 5);
        return (novoNivel, DateTime.UtcNow.AddDays(IntervaloDias(novoNivel)));
    }

    public static (int novoNivel, DateTime proximaRevisao) Errou(int nivelAtual)
    {
        var novoNivel = Math.Max(nivelAtual - 1, 1);
        return (novoNivel, DateTime.UtcNow.AddDays(IntervaloDias(novoNivel)));
    }
}
