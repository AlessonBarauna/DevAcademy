using System.Security.Claims;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpAcademy.API.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OnboardingController(IUsuarioRepository usuarioRepo) : ControllerBase
{
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    private static readonly List<PerguntaOnboarding> Perguntas =
    [
        new(
            "O que é um tipo de valor (value type) em C#?",
            ["Uma classe que herda de object", "Um tipo armazenado diretamente na stack", "Uma referência para um objeto na heap", "Um tipo que pode ser nulo por padrão"],
            1,
            "Types como int, double e struct são value types — armazenados na stack."
        ),
        new(
            "Qual é a diferença entre 'class' e 'struct' em C#?",
            ["Não há diferença, são intercambiáveis", "struct é reference type, class é value type", "class é reference type, struct é value type", "struct não pode ter métodos"],
            2,
            "class é reference type (heap), struct é value type (stack). Structs são copiados por valor."
        ),
        new(
            "O que faz a palavra-chave 'async/await' em C#?",
            ["Cria múltiplas threads para executar código em paralelo", "Permite programação assíncrona sem bloquear a thread atual", "Garante execução sequencial bloqueando o programa", "Substitui try/catch para tratamento de erros"],
            1,
            "async/await libera a thread atual enquanto aguarda operações I/O, sem criar threads extras."
        ),
        new(
            "O que é LINQ em C#?",
            ["Uma biblioteca para conexão com banco de dados", "Uma linguagem de consulta integrada para coleções e fontes de dados", "Um padrão de design para repositórios", "Um tipo especial de array"],
            1,
            "LINQ (Language Integrated Query) permite consultas expressivas sobre coleções, XML, bancos de dados e mais."
        ),
        new(
            "O que são Generics em C#?",
            ["Apenas uma forma de criar listas tipadas", "Um recurso para criar código com tipos como parâmetros, aumentando reuso e segurança de tipo", "Classes que herdam de múltiplas bases", "Métodos que aceitam qualquer número de parâmetros"],
            1,
            "Generics permitem escrever código reutilizável que funciona com qualquer tipo: List<T>, Dictionary<K,V>, etc."
        )
    ];

    [HttpGet("perguntas")]
    public IActionResult ObterPerguntas()
    {
        var dto = Perguntas.Select((p, i) => new
        {
            id = i,
            texto = p.Texto,
            opcoes = p.Opcoes
        });
        return Ok(dto);
    }

    [HttpPost("definir-nivel")]
    public async Task<IActionResult> DefinirNivel([FromBody] DefinirNivelDto dto)
    {
        var usuario = await usuarioRepo.ObterPorIdAsync(UsuarioId);
        if (usuario == null) return NotFound();

        // Só define nível se o usuário ainda não fez onboarding (nivel 1 e sem XP)
        if (usuario.NivelAtual > 1 || usuario.XP > 0)
            return Ok(new { nivelAtual = usuario.NivelAtual, jaFez = true });

        var totalPerguntas = Perguntas.Count;
        var acertos = 0;

        if (dto.Respostas != null)
        {
            for (var i = 0; i < Math.Min(dto.Respostas.Count, totalPerguntas); i++)
            {
                if (dto.Respostas[i] == Perguntas[i].RespostaCorreta)
                    acertos++;
            }
        }

        usuario.NivelAtual = acertos switch
        {
            5 => 4,
            4 => 3,
            >= 2 => 2,
            _ => 1
        };

        await usuarioRepo.AtualizarAsync(usuario);
        await usuarioRepo.SalvarAsync();

        var nomesNivel = new[] { "", "Iniciante", "Intermediário", "Avançado", "Expert" };
        return Ok(new
        {
            nivelAtual = usuario.NivelAtual,
            nomeNivel = nomesNivel[usuario.NivelAtual],
            acertos,
            total = totalPerguntas
        });
    }

    [HttpGet("gabarito")]
    public IActionResult Gabarito()
    {
        var dto = Perguntas.Select((p, i) => new
        {
            id = i,
            texto = p.Texto,
            opcoes = p.Opcoes,
            respostaCorreta = p.RespostaCorreta,
            explicacao = p.Explicacao
        });
        return Ok(dto);
    }
}

public record PerguntaOnboarding(string Texto, List<string> Opcoes, int RespostaCorreta, string Explicacao);

public class DefinirNivelDto
{
    public List<int> Respostas { get; set; } = [];
}
