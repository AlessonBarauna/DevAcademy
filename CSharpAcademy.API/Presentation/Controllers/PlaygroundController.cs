using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlaygroundController : ControllerBase
{
    private const int TimeoutMs = 5000;
    private const int MaxOutputChars = 4000;

    [HttpPost("executar")]
    public async Task<IActionResult> Executar([FromBody] PlaygroundDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Codigo))
            return BadRequest(new { erro = "Código não pode ser vazio." });

        if (dto.Codigo.Length > 8000)
            return BadRequest(new { erro = "Código muito longo (máximo 8.000 caracteres)." });

        var saida = new StringBuilder();
        var stdoutOriginal = Console.Out;
        var stderrOriginal = Console.Error;

        using var writer = new StringWriter(saida);
        Console.SetOut(writer);
        Console.SetError(writer);

        var sw = Stopwatch.StartNew();

        try
        {
            using var cts = new CancellationTokenSource(TimeoutMs);

            // Referencia todos os assemblies carregados na AppDomain atual,
            // garantindo acesso a System.Linq, System.Collections.Generic, etc.
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
                .ToArray();

            var options = ScriptOptions.Default
                .AddReferences(assemblies)
                .AddImports(
                    "System",
                    "System.Collections.Generic",
                    "System.Linq",
                    "System.Text",
                    "System.IO",
                    "System.Text.RegularExpressions",
                    "System.Threading.Tasks"
                )
                .WithOptimizationLevel(Microsoft.CodeAnalysis.OptimizationLevel.Debug)
                .WithEmitDebugInformation(false);

            await CSharpScript.RunAsync(dto.Codigo, options, cancellationToken: cts.Token);

            sw.Stop();
            var output = saida.ToString();

            if (output.Length > MaxOutputChars)
                output = output[..MaxOutputChars] + "\n... (saída truncada)";

            return Ok(new
            {
                saida = output,
                erro = (string?)null,
                tempoMs = sw.ElapsedMilliseconds
            });
        }
        catch (OperationCanceledException)
        {
            sw.Stop();
            return Ok(new
            {
                saida = saida.ToString(),
                erro = "Tempo limite excedido (5 segundos).",
                tempoMs = sw.ElapsedMilliseconds
            });
        }
        catch (CompilationErrorException cex)
        {
            sw.Stop();
            var erros = string.Join("\n", cex.Diagnostics.Select(d => d.ToString()));
            return Ok(new
            {
                saida = (string?)null,
                erro = erros,
                tempoMs = sw.ElapsedMilliseconds
            });
        }
        catch (Exception ex)
        {
            sw.Stop();
            return Ok(new
            {
                saida = saida.ToString(),
                erro = $"{ex.GetType().Name}: {ex.Message}",
                tempoMs = sw.ElapsedMilliseconds
            });
        }
        finally
        {
            Console.SetOut(stdoutOriginal);
            Console.SetError(stderrOriginal);
        }
    }
}

public class PlaygroundDto
{
    public string Codigo { get; set; } = string.Empty;
}
