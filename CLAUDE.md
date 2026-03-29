# CSharp Academy

Plataforma educativa estilo Duolingo para aprender C#/.NET com Professor Assistente IA integrado.

## Stack

- **Backend**: .NET 10 Web API — `CSharpAcademy.API/`
- **Frontend**: Angular (latest) — `CSharpAcademy.Web/`
- **Banco**: SQLite + EF Core
- **IA**: Groq API (modelo llama-3.3-70b-versatile) — compatível com OpenAI SDK

## Rodando

```bash
# Backend — http://localhost:5080
cd CSharpAcademy.API
dotnet run --launch-profile http

# Frontend — http://localhost:4200
cd CSharpAcademy.Web
npm start
```

## Configuração Groq

Edite `CSharpAcademy.API/appsettings.json`:
```json
"MistralAI": {
  "ApiKey": "gsk_SUA_CHAVE_GROQ_AQUI",
  ...
}
```

Obtenha sua chave gratuita em: console.groq.com

## Arquitetura

```
CSharpAcademy.API/
├── Domain/          → Entidades (Usuario, Modulo, Licao, Exercicio...)
│   └── AI/          → ChatMessage, ChatSession, AssistantFAQ, AssistantFeedback, CustomExercise
├── Application/
│   └── Services/AI/ → IAssistantService, IMistralService, IPromptBuilder
├── Infrastructure/
│   ├── Data/        → AppDbContext (SQLite, seed com módulos e lições)
│   └── Repositories/→ IChatMessageRepository, IAssistantFAQRepository, IAssistantFeedbackRepository
├── Presentation/
│   └── Controllers/ → AuthController, ModuloController, AssistantController
└── DTOs/AI/         → ChatRequestDto, ChatResponseDto, FeedbackDto, CustomExerciseDto
```

## Endpoints Principais

| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/api/auth/login` | Login JWT |
| POST | `/api/auth/registrar` | Registro |
| GET | `/api/modulo` | Lista módulos com progresso |
| GET | `/api/modulo/{id}/licoes` | Lições do módulo |
| POST | `/api/assistant/perguntar` | Pergunta ao Professor IA |
| GET | `/api/assistant/historico/{licaoId}` | Histórico da conversa |
| POST | `/api/assistant/{id}/avaliar` | Avaliação ⭐⭐⭐⭐⭐ |
| POST | `/api/assistant/gerar-exercicio` | Exercício customizado por IA |

## Migrations

```bash
cd CSharpAcademy.API
dotnet ef migrations add <NomeMigracao>
dotnet ef database update
```

## Funcionalidades do Assistente IA

- Context-aware: conhece a lição atual e histórico da conversa
- Adaptativo por nível: Iniciante → Especialista
- FAQ em cache: evita chamadas repetidas à API
- Filtro de segurança: bloqueia spoilers e conteúdo inadequado
- Geração de exercícios customizados
- Avaliação ⭐⭐⭐⭐⭐ com feedback detalhado
- Suporte pt-BR e en-US
- Modo dark/light
