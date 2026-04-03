using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Application.Services;

/// <summary>
/// Hearts System: recarrega 1 vida por hora, máximo 5.
/// Deve ser chamado antes de qualquer operação que leia ou deduza vidas.
/// </summary>
public static class VidasHelper
{
    public const int MaxVidas = 5;
    private static readonly TimeSpan IntervaloRecarga = TimeSpan.FromHours(1);

    /// <summary>Aplica recargas pendentes e retorna se houve mudança.</summary>
    public static bool AplicarRecarga(Usuario usuario)
    {
        if (usuario.Vidas >= MaxVidas) return false;

        var agora = DateTime.UtcNow;
        var referencia = usuario.UltimoRecargaVida ?? agora;
        var horasPassadas = (int)((agora - referencia) / IntervaloRecarga);

        if (horasPassadas <= 0) return false;

        usuario.Vidas = Math.Min(MaxVidas, usuario.Vidas + horasPassadas);
        usuario.UltimoRecargaVida = referencia.Add(IntervaloRecarga * horasPassadas);
        return true;
    }

    /// <summary>Deduz uma vida. Inicia o timer de recarga se for a primeira perda.</summary>
    public static void Deduzir(Usuario usuario)
    {
        AplicarRecarga(usuario);
        if (usuario.Vidas > 0)
            usuario.Vidas--;
        usuario.UltimoRecargaVida ??= DateTime.UtcNow;
    }

    /// <summary>Minutos até a próxima recarga (0 se cheio).</summary>
    public static int MinutosParaProximaRecarga(Usuario usuario)
    {
        if (usuario.Vidas >= MaxVidas) return 0;
        var referencia = usuario.UltimoRecargaVida ?? DateTime.UtcNow;
        var proxima = referencia + IntervaloRecarga;
        return (int)Math.Ceiling(Math.Max(0, (proxima - DateTime.UtcNow).TotalMinutes));
    }
}
