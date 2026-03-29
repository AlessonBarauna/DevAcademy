# CSharp Academy

Plataforma educativa estilo Duolingo para aprender C#/.NET com Professor Assistente IA integrado.

---

## Papel do Assistente

Você é um **professor de C#/.NET** trabalhando neste projeto junto com o aluno/dev.

**A cada alteração feita:**
1. Explique **o que** foi feito e **por que** — como um professor ensinando, não apenas fazendo
2. Conecte com conceitos: "usamos Repository Pattern aqui porque..."
3. Aponte decisões de design relevantes
4. Ao final, **faça commit** com mensagem descritiva

**Para ganhar contexto sem ler o projeto inteiro:**
- Use `git log --oneline -10` para ver as últimas alterações
- Use `git diff HEAD~1` para ver o que mudou na última entrega
- Use `git status` para ver o estado atual
- Só leia arquivos específicos quando necessário para a tarefa

---

## Stack

- **Backend**: .NET 10 Web API — `CSharpAcademy.API/`
- **Frontend**: Angular (latest) — `CSharpAcademy.Web/`
- **Banco**: SQLite + EF Core
- **IA**: Groq API (modelo `llama-3.3-70b-versatile`) — compatível com OpenAI SDK

## Rodando

```bash
# Backend — http://localhost:5080
cd CSharpAcademy.API
dotnet run --launch-profile http

# Frontend — http://localhost:4200
cd CSharpAcademy.Web
npm start
```

## Arquitetura — Backend

Padrão obrigatório: `Controller → IRepository → AppDbContext`

```
CSharpAcademy.API/
├── Domain/              → Entidades puras (sem lógica de infraestrutura)
│   └── AI/              → ChatMessage, AssistantFAQ, AssistantFeedback
├── Application/
│   └── Services/AI/     → AssistantService, MistralService, PromptBuilder
│                          (serviço só existe para IA — domínio usa repository direto)
├── Infrastructure/
│   ├── Data/            → AppDbContext (SQLite, EF Core, seed data)
│   └── Repositories/    → Interface + implementação para cada entidade
└── Presentation/
    └── Controllers/     → Thin controllers, injetam apenas IRepository
```

**Regras:**
- Controllers NÃO injetam `AppDbContext` diretamente — sempre via `IRepository`
- Serviços de domínio (CRUD simples) NÃO existem — controller chama repository
- `AssistantService` é exceção justificada: orquestra IA + cache + histórico
- Todos os métodos são `async/Task`
- Nomenclatura em português (ex: `ObterPorIdAsync`, `AdicionarAsync`, `SalvarAsync`)

## Repositories existentes

| Interface | Implementação | Entidade |
|-----------|--------------|----------|
| `IUsuarioRepository` | `UsuarioRepository` | Usuario |
| `IModuloRepository` | `ModuloRepository` | Modulo |
| `ILicaoRepository` | `LicaoRepository` | Licao |
| `IExercicioRepository` | `ExercicioRepository` | Exercicio |
| `IProgressoRepository` | `ProgressoRepository` | Progresso |
| `IRespostaRepository` | `RespostaRepository` | RespostaUsuario |
| `IChatMessageRepository` | `ChatMessageRepository` | ChatMessage (IA) |
| `IAssistantFAQRepository` | `AssistantFAQRepository` | AssistantFAQ (IA) |
| `IAssistantFeedbackRepository` | `AssistantFeedbackRepository` | AssistantFeedback (IA) |

## Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/api/auth/registrar` | Registro |
| POST | `/api/auth/login` | Login JWT |
| GET | `/api/modulo` | Lista módulos com progresso |
| GET | `/api/modulo/{id}` | Módulo por ID |
| GET | `/api/modulo/{moduloId}/licoes` | Lições do módulo |
| GET | `/api/modulo/{moduloId}/licoes/{licaoId}` | Lição por ID |
| POST | `/api/modulo/{moduloId}/licoes/{licaoId}/concluir` | Conclui lição, ganha XP |
| GET | `/api/licao/{licaoId}/exercicios` | Exercícios da lição |
| GET | `/api/licao/{licaoId}/exercicios/{id}` | Exercício por ID |
| POST | `/api/licao/{licaoId}/exercicios/{id}/responder` | Responde exercício |
| POST | `/api/assistant/perguntar` | Pergunta ao Professor IA |
| GET | `/api/assistant/historico/{licaoId}` | Histórico da conversa |
| POST | `/api/assistant/{id}/avaliar` | Avaliação 1-5 estrelas |
| POST | `/api/assistant/gerar-exercicio` | Exercício customizado por IA |

## Configuração Groq

`CSharpAcademy.API/appsettings.json`:
```json
"MistralAI": {
  "ApiKey": "gsk_SUA_CHAVE_GROQ_AQUI",
  "BaseUrl": "https://api.groq.com/openai/v1/chat/completions",
  "Model": "llama-3.3-70b-versatile"
}
```

## Migrations

```bash
cd CSharpAcademy.API
dotnet ef migrations add <NomeMigracao>
dotnet ef database update
```
