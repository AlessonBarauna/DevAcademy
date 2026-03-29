using System.Text.Json;
using CSharpAcademy.API.Domain;

namespace CSharpAcademy.API.Application.Services.AI;

public class MistralService(HttpClient httpClient, IConfiguration config) : IMistralService
{
    private static readonly string[] PalavrasProibidas =
    [
        "gabarito", "resposta pronta", "código completo", "exercicio resolvido",
        "hack", "sql injection", "exploit", "virus", "malware", "spoiler"
    ];

    public async Task<string> ExecutarPromptAsync(string prompt, string? modelo = null)
    {
        var model = modelo ?? config["MistralAI:Model"] ?? "llama-3.3-70b-versatile";
        var maxTokens = int.TryParse(config["MistralAI:MaxTokens"], out var mt) ? mt : 1024;
        var temperature = double.TryParse(config["MistralAI:Temperature"], System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out var t) ? t : 0.7;

        var request = new
        {
            model,
            messages = new[] { new { role = "user", content = prompt } },
            max_tokens = maxTokens,
            temperature
        };

        var baseUrl = config["MistralAI:BaseUrl"] ?? "https://api.groq.com/openai/v1";
        var response = await httpClient.PostAsJsonAsync($"{baseUrl}/chat/completions", request);
        response.EnsureSuccessStatusCode();

        using var json = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
        return json.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? string.Empty;
    }

    public async Task<string> GerarExercicioAsync(string topico, NivelDificuldade nivel, string idioma)
    {
        var prompt = $$"""
            Gere um exercício de programação C# sobre: {{topico}}
            Nível: {{nivel}}
            Idioma: {{idioma}}

            Responda APENAS com JSON válido neste formato (sem texto extra):
            {
              "enunciado": "texto do exercício aqui",
              "tipo": "MultiplaEscolha",
              "opcoes": ["opção A", "opção B", "opção C", "opção D"],
              "resposta_correta": "opção correta aqui",
              "explicacao": "explicação detalhada aqui"
            }
            """;

        return await ExecutarPromptAsync(prompt);
    }

    public Task<string?> ValidarRespostaSeguraAsync(string pergunta)
    {
        var lower = pergunta.ToLower();
        return Task.FromResult(
            PalavrasProibidas.Any(p => lower.Contains(p)) ? null : pergunta
        );
    }
}
