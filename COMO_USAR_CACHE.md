# 🧠 COMO USAR CACHE DE CONTEXTO COM CLAUDE CODE
## Economize 80% de tokens! Veja como...

---

## 📋 O PROBLEMA

❌ **ANTES (Ineficiente):**
```
Você: [Cola 600 linhas de prompt inteiro]
Claude Code: Lê 600 linhas, processa, gasta 5.000 tokens
Você: [Cola 600 linhas novamente em próxima conversa]
Claude Code: Lê 600 linhas de novo, gasta mais 5.000 tokens
TOTAL: 10.000 tokens por 2 iterações! 💸
```

✅ **DEPOIS (Eficiente):**
```
Você: Leia MEMORY.md, depois crie [coisa específica]
Claude Code: Lê 2 páginas (500 tokens), já tem contexto
Você: [Segunda conversa] Leia MEMORY.md, crie [outra coisa]
Claude Code: Lê 2 páginas (500 tokens), já tem contexto
TOTAL: 1.000 tokens por 2 iterações! 💰
```

**ECONOMIA: 80-90% menos tokens!**

---

## 📁 ARQUIVOS DE CACHE (Use sempre em ordem)

### **1️⃣ MEMORY.md (SEMPRE LEIA PRIMEIRO)**
```
O quê: Visão geral + Status + Estrutura
Tamanho: ~4 KB (2 páginas)
Tempo de leitura: 2 minutos
Quando: Toda conversa com Claude Code

Exemplo de uso:
"Leia MEMORY.md primeiro, depois [ação]"
```

### **2️⃣ ENTITIES_SCHEMA.md (Quando trabalhar com BD)**
```
O quê: Schema exato de cada entity
Tamanho: ~3 KB
Tempo de leitura: 1 minuto
Quando: Criar entities, migrations, repositories

Exemplo de uso:
"Crie a entity AssistantFAQ seguindo ENTITIES_SCHEMA.md"
```

### **3️⃣ QUICK_REFERENCE.md (Para snippets rápidos)**
```
O quê: Templates prontos para copiar/colar
Tamanho: ~5 KB
Tempo de leitura: 30 segundos (você busca o que precisa)
Quando: Criar services, controllers, components

Exemplo de uso:
"Use template de Service em QUICK_REFERENCE.md para criar AssistantService"
```

### **4️⃣ PROMPT_CSHARP_DUOLINGO_COM_IA_REFINADO.md (Inicial)**
```
O quê: Prompt completo original (600+ linhas)
Tamanho: ~60 KB
Quando: PRIMEIRA VEZ APENAS (criar estrutura base)

Exemplo de uso:
"Crie toda estrutura base usando PROMPT_CSHARP_DUOLINGO_COM_IA_REFINADO.md"
DEPOIS: Use MEMORY.md para futuras ações
```

---

## 🎯 WORKFLOW OTIMIZADO

### **PASSO 1: Primeira Conversa (Estrutura Base)**

```
VOCÊ FALA:
"Claude Code, você vai criar a estrutura base de uma C# Academy.
Leia primeiro: PROMPT_CSHARP_DUOLINGO_COM_IA_REFINADO.md
Crie tudo que estiver lá em FASE 1"

CLAUDE CODE:
1. Lê o prompt completo (5.000 tokens)
2. Entende contexto
3. Cria estrutura base
4. Salva tudo

TOKENS GASTOS: ~5.000
```

### **PASSO 2: Segunda Conversa (Adicionar IA)**

```
❌ ERRADO:
"Claude Code, crie os services de IA..."
[Claude Code lê 600 linhas de novo = 5.000 tokens gastos]

✅ CERTO:
"Claude Code:
1. Leia MEMORY.md
2. Baseado no contexto, crie os services de IA da FASE 5"

Claude Code:
1. Lê MEMORY.md (500 tokens)
2. Já sabe contexto inteiro
3. Cria apenas os services de IA
4. Atualiza MEMORY.md

TOKENS GASTOS: ~500 (POUPANÇA: 90%!)
```

### **PASSO 3: Terceira Conversa (Components Angular)**

```
✅ CERTO:
"Claude Code:
1. Leia MEMORY.md
2. Leia QUICK_REFERENCE.md para ver template de componente
3. Crie AssistantChatComponent da FASE 6"

TOKENS GASTOS: ~500-700
POUPANÇA: 85-90%
```

---

## 📝 TEMPLATE DE MENSAGEM OTIMIZADA

Use SEMPRE este formato para Claude Code:

```markdown
Claude Code:

**Contexto:** Leia MEMORY.md (2 min)
**Referência:** Se precisar snippets, use QUICK_REFERENCE.md
**Schema:** Se mexer com BD, leia ENTITIES_SCHEMA.md

**Tarefa:**
1. [Descrição clara do que quer]
2. [Se aplicável: que arquivo usar como base]
3. [Resultado esperado]

**Exemplo específico:**
"Crie o MistralService em Infrastructure/Services/
Siga o template de Service em QUICK_REFERENCE.md
Deve ter métodos: ExecutarPromptAsync, GerarExercicioAsync, ValidarRespostaSeguraAsync"

**Após completar:**
- Atualize MEMORY.md com novo status (FASE X: YY% completo)
- Salve o arquivo
```

---

## 💡 EXEMPLOS DE USO REAL

### **EXEMPLO 1: Criar um Service**

```
Mensagem CURTA e CLARA:

Claude Code, leia MEMORY.md.
Usando QUICK_REFERENCE.md como template, crie o IAssistantFAQService 
e AssistantFAQService em Infrastructure/Services/AI/

Métodos necessários:
- GetFAQAsync(pergunta)
- CacheFAQAsync(pergunta, resposta)
- BuscarPorSimilarAsync(pergunta)
```

### **EXEMPLO 2: Criar um Controller**

```
Claude Code, leia MEMORY.md.
Crie o AssistantController em Presentation/Controllers/ 

Endpoints (veja em API_ENDPOINTS.md se precisar):
- POST /api/assistant/perguntar
- GET /api/assistant/historico/{licaoId}
- POST /api/assistant/{id}/avaliar
```

### **EXEMPLO 3: Criar um Componente Angular**

```
Claude Code, leia MEMORY.md e QUICK_REFERENCE.md.
Crie AssistantChatComponent em features/exercicio/assistant-chat/

Use template de Component em QUICK_REFERENCE.md.
Deve ter:
- Modal popup com dark/light mode
- Sistema de ⭐ feedback
- Chat messages com markdown support
```

---

## 🚀 WORKFLOW COMPLETO DO PROJETO

```
TOTAL: ~7 FASES DE DESENVOLVIMENTO

FASE 1: ESTRUTURA BASE
Mensagem 1:
├─ Arquivo: PROMPT_CSHARP_DUOLINGO_COM_IA_REFINADO.md
├─ Tokens: ~5.000
└─ Resultado: Entities + DbContext + Migrations

FASE 2: DUOLINGO CORE (Services, Controllers)
Mensagem 2:
├─ Arquivo: MEMORY.md
├─ Tokens: ~500 + cálculo
├─ Tarefa: "Leia MEMORY.md, crie Services e Controllers da FASE 2"
└─ Resultado: 6 Controllers funcionando

FASE 3: AUTENTICAÇÃO
Mensagem 3:
├─ Arquivo: MEMORY.md + QUICK_REFERENCE.md
├─ Tokens: ~500-700
└─ Resultado: JWT setup, Auth Controller

FASE 4: GAMIFICAÇÃO
Mensagem 4:
├─ Arquivo: MEMORY.md
├─ Tokens: ~500
└─ Resultado: XP, Streak, Achievement services

FASE 5: IA ASSISTANT
Mensagem 5:
├─ Arquivo: MEMORY.md + ENTITIES_SCHEMA.md + QUICK_REFERENCE.md
├─ Tokens: ~500-700
└─ Resultado: MistralService, PromptBuilder, AssistantService

FASE 6: FRONTEND IA
Mensagem 6:
├─ Arquivo: MEMORY.md + QUICK_REFERENCE.md
├─ Tokens: ~500-700
└─ Resultado: AssistantChatComponent + CSS

FASE 7: TESTES & POLISH
Mensagem 7:
├─ Arquivo: MEMORY.md
├─ Tokens: ~500
└─ Resultado: Testes básicos, documentação

TOTAL TOKENS:
❌ SEM CACHE: 7 × 5.000 = 35.000 tokens
✅ COM CACHE: 5.000 + (6 × 500) = 8.000 tokens
💰 POUPANÇA: 77% (27.000 tokens economizados!)
```

---

## 🎓 REGRAS DE OURO

### **✅ FAÇA:**
```
1. Leia MEMORY.md no início de cada conversa
2. Use QUICK_REFERENCE.md para templates
3. Use ENTITIES_SCHEMA.md para entities
4. Mensagens CURTAS (máx 100 palavras)
5. Seja ESPECÍFICO ("Crie IAssistantService" não "Crie um service")
6. Atualize MEMORY.md após cada fase
7. Reutilize código já criado
```

### **❌ NÃO FAÇA:**
```
1. ❌ Cole 600 linhas de prompt em cada conversa
2. ❌ Explique tudo novamente
3. ❌ Use mensagens longas/vagas
4. ❌ Forget de ler MEMORY.md
5. ❌ Pça para recriar algo que já existe
6. ❌ Ignore templates em QUICK_REFERENCE.md
7. ❌ Esqueça de atualizar MEMORY.md
```

---

## 📊 RESUMO: TOKENS POR FASE

| Fase | Arquivo Lido | Tokens | Método |
|------|-------------|--------|--------|
| 1 | PROMPT completo | 5.000 | Cole prompt inteiro |
| 2 | MEMORY.md | 500 | "Leia MEMORY.md..." |
| 3 | MEMORY.md | 500 | "Leia MEMORY.md..." |
| 4 | MEMORY.md | 500 | "Leia MEMORY.md..." |
| 5 | MEMORY.md | 500 | "Leia MEMORY.md..." |
| 6 | MEMORY.md | 500 | "Leia MEMORY.md..." |
| 7 | MEMORY.md | 500 | "Leia MEMORY.md..." |
| **TOTAL** | - | **8.000** | **77% poupança** |

---

## 🔄 FLUXO DE UMA CONVERSA TÍPICA

```
┌──────────────────────────────────────────────────┐
│ VOCÊ ABRE CLAUDE CODE                            │
└──────────────────────────────────────────────────┘
                    ↓
┌──────────────────────────────────────────────────┐
│ VOCÊ: "Claude Code, leia MEMORY.md primeiro"     │
└──────────────────────────────────────────────────┘
                    ↓
┌──────────────────────────────────────────────────┐
│ CLAUDE CODE: [Lê MEMORY.md - 500 tokens]         │
│ "Entendi o contexto. Estamos na FASE X,         │
│  proxima ação: [X]. Pronto!"                     │
└──────────────────────────────────────────────────┘
                    ↓
┌──────────────────────────────────────────────────┐
│ VOCÊ: "Crie [coisa específica], use             │
│ QUICK_REFERENCE.md como template"               │
└──────────────────────────────────────────────────┘
                    ↓
┌──────────────────────────────────────────────────┐
│ CLAUDE CODE: [Cria coisa]                       │
│ [Gasta mais ~200-500 tokens para criar]         │
│ "Pronto! Arquivo salvo. Próxima ação?"          │
└──────────────────────────────────────────────────┘
                    ↓
┌──────────────────────────────────────────────────┐
│ VOCÊ: "Atualize MEMORY.md - FASE X agora 50%"   │
└──────────────────────────────────────────────────┘
                    ↓
┌──────────────────────────────────────────────────┐
│ CLAUDE CODE: [Atualiza]                          │
│ TOTAL TOKENS GASTOS: ~1.200                      │
└──────────────────────────────────────────────────┘
```

---

## 📞 CHECKLIST: Antes de Cada Conversa com Claude Code

- [ ] Abra Claude Code
- [ ] Mensagem 1: "Leia MEMORY.md"
- [ ] Espere confirmação
- [ ] Diga o que quer (específico e curto)
- [ ] Mencione arquivo de referência se aplicável (QUICK_REFERENCE.md, ENTITIES_SCHEMA.md)
- [ ] Aguarde resultado
- [ ] Peça para atualizar MEMORY.md com novo status
- [ ] Feche conversa

**TOTAL DE TEMPO: 5-10 minutos por fase**

---

## 🎯 RESULTADO FINAL

✅ **7 fases de desenvolvimento**
✅ **8.000 tokens no total** (vs 35.000 sem cache)
✅ **77% poupança de tokens**
✅ **Máximo 15 minutos por fase**
✅ **Código modular e bem documentado**
✅ **Sempre rastreável via MEMORY.md**

---

## 🌟 BÔNUS: Sistema de Versioning

Adicione ao MEMORY.md:

```markdown
## 📦 VERSÕES

**v0.1 - Setup Base (FASE 1)**
├─ Entities criadas
├─ DbContext configurado
└─ Migrations prontas

**v0.2 - Duolingo Core (FASE 2-4)**
├─ Controllers + Services
├─ Gamificação
└─ Dashboard functional

**v1.0 - IA Assistant (FASE 5-7)**
├─ Mistral integrado
├─ Assistant chat
└─ Tudo polido
```

Assim você sempre sabe em que versão está!

---

**🎉 Pronto! Agora você sabe como economizar 80% de tokens!**

**Próxima ação:**
1. Baixe os 4 arquivos de cache
2. Cole o PROMPT completo em Claude Code (primeira vez)
3. Depois use apenas MEMORY.md nas próximas
4. Veja a magia acontecer! ✨

---

Versão: 1.0
Atualizado: [HOJE]
