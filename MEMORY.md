# 🧠 CONTEXT CACHE - C# Academy IA
## Arquivo de Memória Permanente (Leia Primeiro!)

**📌 STATUS:** Projeto C# Academy (Duolingo) com Professor Assistente IA
**⏱️ ÚLTIMA ATUALIZAÇÃO:** [DATA]
**💾 LOCALIZAÇÃO:** `/CSharpAcademy/MEMORY.md`

---

## ⚡ RESUMO EXECUTIVO (30 segundos)

```
PROJETO: C# Academy - Duolingo de C# com IA (Mistral/Groq)

STACK:
├── Frontend: Angular 17 + Tailwind
├── Backend: .NET 8 Web API
├── BD: SQLite + EF Core
└── IA: Mistral/Groq (free + rápido)

ARQUITETURA: Clean Architecture (Domain → Application → Infrastructure → Presentation)
PADRÕES: Repository, DI, SOLID, Async/Await

STATUS: [TODO | EM DESENVOLVIMENTO | PRONTO]
```

---

## 🏗️ ESTRUTURA PERMANENTE

```
CSharpAcademy/
├── CSharpAcademy.API/
│   ├── Domain/          (Entities: Usuario, Modulo, Licao, Exercicio, + ChatMessage, ChatSession, AssistantFAQ, AssistantFeedback, CustomExercise)
│   ├── Application/     (Services: *Service.cs)
│   ├── Infrastructure/  (Repositories, DbContext)
│   ├── Presentation/    (Controllers)
│   ├── DTOs/
│   ├── Program.cs       (DI + Middleware)
│   └── appsettings.json (Mistral API Key)
│
└── CSharpAcademy.Web/
    └── src/app/
        ├── features/exercicio/assistant-chat/
        ├── features/licoes/
        ├── core/services/
        └── shared/components/
```

---

## 📊 ENTITIES PRINCIPAIS

### **Duolingo Base (SEMPRE MANTER)**
```csharp
Usuario → [XP, Streak, NivelAtual, Progressos, Respostas, Achievements]
Modulo → [Titulo, Descricao, Ordem, NivelMinimo]
Licao → [ModuloId, Titulo, ConteudoTeoricoMarkdown, Exercicios]
Exercicio → [LicaoId, Enunciado, Tipo (enum), ConteudoJson]
RespostaUsuario → [UsuarioId, ExercicioId, Correta, XPObtido]
Progresso → [UsuarioId, LicaoId, Completada, PercentualConclusao]
Achievement → [Titulo, Tipo (enum), UsuariosQueObtiveram]
```

### **IA Assistant (ADIÇÕES NOVAS)**
```csharp
ChatMessage → [UsuarioId, LicaoId, PerguntaUsuario, RespostaAssistente, AvaliacaoEstrelas, IdiomaUsado, NivelUsuarioNaMomentoPergunta]
ChatSession → [UsuarioId, LicaoId, TotalMensagens, MediaAvaliacoes]
AssistantFAQ → [ModuloId, LicaoId, Pergunta, Resposta, Idioma, TotalUsos]
AssistantFeedback → [ChatMessageId, Estrelas, RespostaAjudou]
CustomExercise → [UsuarioId, LicaoId, ChatMessageId, Enunciado, Tipo]
```

---

## 🔌 ENDPOINTS CRÍTICOS

### **DUOLINGO CORE**
```
GET    /api/modulos
GET    /api/modulos/{id}
GET    /api/licoes
GET    /api/licoes/{id}
POST   /api/exercicios/{id}/responder
GET    /api/progresso/resumo
```

### **IA ASSISTANT (NOVOS)**
```
POST   /api/assistant/perguntar              (Faz pergunta → Retorna resposta IA)
GET    /api/assistant/historico/{licaoId}   (Carrega chat history)
POST   /api/assistant/{idMensagem}/avaliar  (Salva ⭐⭐⭐⭐⭐)
POST   /api/assistant/gerar-exercicio       (Cria exercício customizado)
```

---

## 🤖 INTEGRAÇÃO MISTRAL/GROQ

### **Configuração**
```json
{
  "MistralAI": {
    "ApiKey": "gsk_xxxxx",
    "BaseUrl": "https://api.groq.com/openai/v1",
    "Model": "mixtral-8x7b-32768",
    "MaxTokens": 1024,
    "Temperature": 0.7
  }
}
```

### **Services**
```
IMistralService.ExecutarPromptAsync(prompt) → string resposta
IPromptBuilder.ConstroirPromptAssistente(usuario, licao, pergunta, historico) → string prompt dinâmico
IAssistantService.ResponderPerguntaAsync(usuarioId, request) → ChatResponseDto
```

### **Fluxo**
1. Usuário pergunta
2. Valida segurança (palavras proibidas?)
3. Busca FAQ cache (já respondemos essa pergunta?)
4. Se não: constrói prompt dinâmico com contexto + nível
5. Chama Mistral/Groq
6. Salva no BD
7. Retorna resposta

---

## 🎨 COMPONENTES ANGULAR

### **Já Criados**
```
DashboardComponent
LicoesListComponent
ExercicioPlayerComponent
ResultadoModalComponent
PerfilComponent
```

### **NOVOS (IA)**
```
AssistantChatComponent      (Modal popup principal)
  ├── ChatMessageComponent  (Exibe mensagem individual)
  └── FeedbackModalComponent (Sistema ⭐)
```

### **Estrutura Chat Modal**
```html
Modal (fixed, bottom-right)
├── Header (Título + Idioma + Theme Toggle)
├── ChatContainer (scroll, messages)
│   ├── Pergunta do Usuário (balloon right)
│   ├── Resposta IA (balloon left, markdown)
│   ├── Stars ⭐ (feedback system)
│   └── Btn "Gerar Exercício"
└── Input (textarea + send button)
```

---

## 🔐 SEGURANÇA INTEGRADA

### **BLOQUEADO**
```
❌ Perguntas fora de C#/.NET
❌ Código malicioso/hacking
❌ Spoilers de exercícios
❌ Palavras: "gabarito", "resposta_pronta", "hack", "exploit", "virus"
```

### **VALIDAÇÃO**
```csharp
AssistantSecurityFilter.EhSeguro(string pergunta) → bool
```

---

## 📈 NÍVEIS DO ALUNO (Adaptação)

```
Nível 1: INICIANTE
└─ Respostas curtas, exemplos simples, explicações básicas

Nível 2: INTERMEDIÁRIO
└─ Respostas médias, conceitos avançados, patterns

Nível 3: AVANÇADO
└─ Discussões arquiteturais, SOLID, design patterns

Nível 4: ESPECIALISTA
└─ DDD, CQRS, microserviços, código profundo
```

**PromptBuilder muda:**
- Tamanho da resposta
- Tom (casual → técnico)
- Profundidade (simples → arquitetural)
- Exemplos (básicos → reais/profissionais)

---

## 🌍 SUPORTE IDIOMA

```
pt-BR (Português Brasil) - DEFAULT
en-US (Inglês)

ChatRequestDto.Idioma → Determina respostas
PromptBuilder inclui: "Idioma: {idioma}"
```

---

## ⭐ SISTEMA DE FEEDBACK

```
Usuário avalia resposta: 1 ⭐ a 5 ⭐⭐⭐⭐⭐

Salva em:
├── ChatMessage.AvaliacaoEstrelas
└── AssistantFeedback (detalhado)
    ├── Estrelas (1-5)
    ├── Comentario (texto)
    ├── RespostaAjudou (bool)
    ├── RespostaClara (bool)
    └── RespostaCompleta (bool)

ANÁLISE:
- Responses com < 3 ⭐ = investigar
- Padrões de feedback = melhorar assistente
```

---

## 📚 FAQ CACHE SYSTEM

```
PROBLEMA: Chamadas repetidas à Groq = desperdício de tokens

SOLUÇÃO: FAQ Cache
1. Usuário pergunta "O que é Repository Pattern?"
2. Sistema busca em AssistantFAQ
3. Se encontrou (match > 80%) → Retorna do cache (⚡ rápido)
4. Se não → Chama Mistral, salva no FAQ
5. Próximas vezes: cache hit automaticamente

BENEFÍCIO: -70% chamadas API após warm-up
```

---

## 🧪 DADOS SEED (Sempre Popular)

### **Módulos (12)**
```
1-4: Iniciante (Tipos, OOP, Coleções, Erros)
5-8: Intermediário (ASP.NET, EF Core, Repository, DI)
9-10: Avançado (Clean Arch, SOLID)
11-12: Especialista (DDD, CQRS)
```

### **Lições (40+)**
```
Modulo 1
├─ 1.1 Tipos de Dados
├─ 1.2 Variáveis
├─ 1.3 Operadores
├─ 1.4 Condicionais
└─ 1.5 Loops
```

### **Exercícios (200+)**
```
Tipos:
- 40% MultiplaEscolha (simples)
- 20% VerdadeiroFalso
- 15% Correspondencia
- 15% CompletarCodigo
- 10% EscreverCodigo

SEMPRE: Explicação + Dica
```

### **FAQ Initial (50+)**
```
Modulo 1:
├─ "O que é int?"
├─ "Diferença int vs decimal?"
├─ "Como declarar uma classe?"
└─ ... (preencher com perguntas comuns)

Modulo 5:
├─ "O que é um Controller?"
├─ "Como fazer async/await?"
└─ ...
```

---

## 🎮 GAMIFICAÇÃO

### **XP System**
```
MultiplaEscolha: 10 XP
VerdadeiroFalso: 10 XP
Correspondencia: 15 XP
CompletarCodigo: 20 XP
EscreverCodigo: 25 XP
LicaoCompleta: 100 XP bônus
ModuloCompleto: 300 XP bônus
```

### **Streaks**
```
+1 dia cada atividade
Reseta se não fizer nada 24h
Badges: 7 dias, 30 dias, 100 dias
```

### **Achievements**
```
Primeiro_Exercicio
Uma_Semana (7 dias)
Um_Mes (30 dias)
Modulo_100_Porcento
Mil_Pontos
Dez_Mil_Pontos
Especialista (completar tudo)
```

---

## 📋 CONVENÇÕES DE CÓDIGO

### **C# (Backend)**
```csharp
// Services: IXxxService.cs + XxxService.cs
// Repositories: IXxxRepository.cs + XxxRepository.cs
// DTOs: XxxDto.cs
// Entities: Xxx.cs (domain)

// Métodos: PascalCase
// Propriedades: PascalCase com auto-property { get; set; }
// Locais: camelCase
// Privados: _camelCase

// SEMPRE async/await
public async Task<T> GetByIdAsync(int id)
{
    return await _repository.GetByIdAsync(id);
}

// SEMPRE validações no Service
// SEMPRE DTOs para API
// NUNCA entidades direto na API
```

### **Angular (Frontend)**
```typescript
// Components: xxx.component.ts
// Services: xxx.service.ts
// Models/Interfaces: xxx.model.ts
// Métodos: camelCase
// Propriedades: camelCase

// SEMPRE tipos tipados (TypeScript)
interface Usuario {
    id: number;
    nome: string;
}

// SEMPRE observables com subscribe pattern
this.service.getUsuario(id).subscribe(
    (data) => { this.usuario = data; }
);
```

---

## 🚀 CHECKLIST POR FASE

### **FASE 1: ESTRUTURA BASE**
- [ ] Entities (7 principais)
- [ ] AppDbContext
- [ ] Migrations iniciais
- [ ] Repositories (base)
- [ ] Seed data (módulos, lições)

### **FASE 2: DUOLINGO CORE**
- [ ] Services (Licao, Exercicio, Progresso)
- [ ] Controllers (Modulo, Licao, Exercicio)
- [ ] DTOs
- [ ] Angular components básicos
- [ ] Dashboard

### **FASE 3: AUTENTICAÇÃO**
- [ ] JWT setup
- [ ] Auth Controller
- [ ] Auth Guard
- [ ] Login/Registro components

### **FASE 4: GAMIFICAÇÃO**
- [ ] XP calculation
- [ ] Streak logic
- [ ] Achievement system
- [ ] Notificações

### **FASE 5: IA ASSISTANT**
- [ ] Novas entities (Chat*, FAQ, Feedback)
- [ ] Mistral service
- [ ] PromptBuilder
- [ ] AssistantService
- [ ] AssistantController

### **FASE 6: FRONTEND IA**
- [ ] AssistantChatComponent
- [ ] Modal popup
- [ ] Feedback system
- [ ] Dark/Light theme

### **FASE 7: REFINAMENTO**
- [ ] Testes
- [ ] Otimizações
- [ ] Documentação
- [ ] Deploy

---

## 🔄 WORKFLOW FUTURO (Para Claude Code)

**REGRA DE OURO:** Sempre comece assim:

```
1. LEIA MEMORY.md (este arquivo)
2. Entenda o contexto
3. Identifique qual fase está
4. Execute apenas o necessário
5. Salve mudanças relevantes em MEMORY.md
```

**Exemplo:**
```
Claude Code:
"Preciso criar o Service de Assistente.
Lá está o contexto em MEMORY.md. Vamos lá?"

Claude Code executa:
1. Lê MEMORY.md
2. Vê que é FASE 5
3. Cria: IMistralService, IAssistantService, PromptBuilder
4. Atualiza MEMORY.md: "FASE 5: IA Assistant - 30% completo"
```

---

## 📝 TEMPLATE PARA UPDATES RÁPIDOS

Quando terminar uma parte, SEMPRE atualize:

```markdown
## 🎯 STATUS ATUAL

**Última ação:** [Data/Hora]
**Claude Code gastou:** X tokens
**Fase atual:** X de 7
**Progresso:** XX%

**Completado:**
- ✅ Entities (Domain)
- ✅ AppDbContext
- ⏳ Repositories (40%)

**Próximo:**
- [ ] Controller de IA
- [ ] Components Angular

**Bloqueadores:**
- Nenhum
```

---

## 💾 FILES IMPORTANTES

```
📁 CSharpAcademy/
├── 📄 MEMORY.md                    (ESTE ARQUIVO - sempre ler primeiro)
├── 📄 QUICK_REFERENCE.md           (Snippets de código)
├── 📄 ENTITIES_SCHEMA.md           (Exato das entities)
├── 📄 API_ENDPOINTS.md             (Todos os endpoints)
├── 📄 PROMPT_RULES.md              (Regras de prompt para IA)
│
├── CSharpAcademy.API/
│   ├── Program.cs                  (DI configuration)
│   ├── appsettings.json            (Mistral Key)
│   ├── Domain/
│   ├── Application/
│   ├── Infrastructure/
│   └── Presentation/
│
└── CSharpAcademy.Web/
    └── src/app/
```

---

## ⚡ QUICK COMMANDS

```bash
# Backend
cd CSharpAcademy.API
dotnet restore
dotnet ef migrations add NomeMigracao
dotnet ef database update
dotnet run

# Frontend
cd CSharpAcademy.Web
npm install
ng generate component features/assistente/chat
ng serve

# Tests
dotnet test
ng test
```

---

## 🎓 DOCUMENTAÇÃO REFERÊNCIA

- **Angular 17:** https://angular.io/docs
- **.NET 8:** https://docs.microsoft.com/dotnet
- **EF Core:** https://docs.microsoft.com/en-us/ef/core
- **Groq API:** https://console.groq.com/docs
- **Clean Architecture:** https://blog.cleancoder.com

---

## 📞 PONTOS DE CONTATO

Se tiver dúvida em:

| Tópico | Referência |
|--------|-----------|
| Entities | ENTITIES_SCHEMA.md |
| Endpoints | API_ENDPOINTS.md |
| Código C# | QUICK_REFERENCE.md |
| Regras IA | PROMPT_RULES.md |
| Status | Abaixo (seção atual) |

---

## 🟢 STATUS ATUAL

**Projeto:** C# Academy com Professor Assistente IA
**Data Criação:** [HOJE]
**Status Geral:** 📋 DESIGN COMPLETO, AGUARDANDO CLAUDE CODE

### **Fases Completadas:**
```
FASE 1: ESTRUTURA BASE          ⏳ 0%
FASE 2: DUOLINGO CORE           ⏳ 0%
FASE 3: AUTENTICAÇÃO            ⏳ 0%
FASE 4: GAMIFICAÇÃO             ⏳ 0%
FASE 5: IA ASSISTANT            ⏳ 0%
FASE 6: FRONTEND IA             ⏳ 0%
FASE 7: REFINAMENTO             ⏳ 0%
```

### **Próximas Ações:**
1. [ ] Copiar PROMPT completo para Claude Code
2. [ ] Executar FASE 1 (estrutura base)
3. [ ] Atualizar este arquivo com progresso
4. [ ] Continuar FASE 2, 3, 4...
5. [ ] Depois IA (FASE 5, 6)

---

## 🎉 CONCLUSÃO

**Este arquivo = SUA MEMÓRIA PERMANENTE**

Cada vez que trabalhar com Claude Code:
1. ✅ Leia este arquivo primeiro (2 min)
2. ✅ Entenda contexto (1 min)
3. ✅ Peça ação específica (sem passar código gigante)
4. ✅ Claude Code já sabe tudo que precisa
5. ✅ Economiza tokens massivamente

**Economia esperada:** -80% tokens vs. reler tudo toda hora

---

**🚀 Pronto para começar? Diga: "Vamos rodar a FASE 1!" e deixa eu coordenar com Claude Code!**

---

## 📎 ANEXOS RÁPIDOS

### **Setup Inicial (1 minuto)**
```bash
# .NET
dotnet new sln -n CSharpAcademy
dotnet new webapi -n CSharpAcademy.API
cd CSharpAcademy.API
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite

# Angular
ng new CSharpAcademy.Web
cd CSharpAcademy.Web
npm install -D tailwindcss
```

### **Groq API Key Setup**
```json
appsettings.json:
{
  "MistralAI": {
    "ApiKey": "gsk_XXXXX",  // Get from https://console.groq.com
    "BaseUrl": "https://api.groq.com/openai/v1",
    "Model": "mixtral-8x7b-32768"
  }
}
```

---

**Documento versão: 1.0**
**Próxima atualização esperada: Após FASE 1**
