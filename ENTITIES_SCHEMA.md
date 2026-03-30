# 🗄️ ENTITIES SCHEMA - Referência Rápida
## Copia e cola quando precisar criar Entity

---

## 📊 DUOLINGO CORE ENTITIES

### **Usuario**
```
Id: int (PK)
Nome: string (required, max 100)
Email: string (required, unique, max 100)
Senha: string (required, hashed)
XP: int (default 0)
Streak: int (default 0)
UltimaAtividadeData: DateTime
NivelAtual: int (1-4, default 1)
DataCriacao: DateTime (default: now)

Relacionamentos:
├─ Progressos: ICollection<Progresso>
├─ Respostas: ICollection<RespostaUsuario>
├─ Achievements: ICollection<Achievement>
├─ ChatMessages: ICollection<ChatMessage>
└─ ChatSessions: ICollection<ChatSession>
```

---

### **Modulo**
```
Id: int (PK)
Titulo: string (required, max 150)
Descricao: string (required)
IconUrl: string (nullable)
Ordem: int (required, unique)
NivelMinimo: NivelDificuldade enum (1=Iniciante, 2=Intermediario, 3=Avancado, 4=Especialista)
Publicado: bool (default true)
DataCriacao: DateTime (default: now)

Relacionamentos:
├─ Licoes: ICollection<Licao>
└─ AssistantFAQs: ICollection<AssistantFAQ>
```

---

### **Licao**
```
Id: int (PK)
ModuloId: int (FK)
Titulo: string (required, max 150)
Descricao: string (required)
ConteudoTeoricoMarkdown: string (required, large text)
Ordem: int (required)
XPRecompensa: int (default 50)
Nivel: NivelDificuldade enum
Publicada: bool (default true)
DataCriacao: DateTime (default: now)

Relacionamentos:
├─ Modulo: Modulo (required, many-to-one)
├─ Exercicios: ICollection<Exercicio>
├─ Progressos: ICollection<Progresso>
├─ ChatMessages: ICollection<ChatMessage>
├─ ChatSessions: ICollection<ChatSession>
└─ AssistantFAQs: ICollection<AssistantFAQ>
```

---

### **Exercicio**
```
Id: int (PK)
LicaoId: int (FK)
Enunciado: string (required, max 500)
Tipo: TipoExercicio enum
  ├─ 1 = MultiplaEscolha
  ├─ 2 = VerdadeiroFalso
  ├─ 3 = Correspondencia
  ├─ 4 = CompletarCodigo
  ├─ 5 = EscreverCodigo
  └─ 6 = SelecionarErro
ConteudoExercicioJson: string (large text, JSON format)
Ordem: int (required)
Obrigatorio: bool (default true)
DataCriacao: DateTime (default: now)

Relacionamentos:
├─ Licao: Licao (required, many-to-one)
├─ Respostas: ICollection<RespostaUsuario>
└─ ChatMessages: ICollection<ChatMessage>
```

---

### **RespostaUsuario**
```
Id: int (PK)
UsuarioId: int (FK)
ExercicioId: int (FK)
RespostaJson: string (large text, JSON format)
Correta: bool (required)
TentativasRestantes: int (default 3)
DataResposta: DateTime (default: now)
XPObtido: int (calculated)

Relacionamentos:
├─ Usuario: Usuario (required, many-to-one)
└─ Exercicio: Exercicio (required, many-to-one)
```

---

### **Progresso**
```
Id: int (PK)
UsuarioId: int (FK)
LicaoId: int (FK)
Completada: bool (default false)
DataInicio: DateTime (default: now)
DataConclusao: DateTime? (nullable)
ExerciciosCompletados: int (default 0)
ExerciciosTotal: int (required)
PercentualConclusao: int (calculated 0-100)

Relacionamentos:
├─ Usuario: Usuario (required, many-to-one)
└─ Licao: Licao (required, many-to-one)
```

---

### **Achievement**
```
Id: int (PK)
Titulo: string (required, unique, max 100)
Descricao: string (required)
IconUrl: string (nullable)
Tipo: TipoAchievement enum
  ├─ 1 = Streak_7_Dias
  ├─ 2 = Streak_30_Dias
  ├─ 3 = Primeiro_Exercicio
  ├─ 4 = Mil_XP
  ├─ 5 = Primeiro_Modulo_Completo
  ├─ 6 = Especialista_Em_Csharp
  └─ 7 = Ajudante_da_Comunidade

Relacionamentos:
└─ UsuariosQueObtiveram: ICollection<Usuario>
```

---

## 🤖 IA ASSISTANT ENTITIES

### **ChatMessage**
```
Id: int (PK)
UsuarioId: int (FK)
ChatSessionId: int? (FK, nullable)
ModuloId: int? (FK, nullable)
LicaoId: int? (FK, nullable)
ExercicioId: int? (FK, nullable)

PerguntaUsuario: string (required, max 1000)
RespostaAssistente: string (required, large text)

IdiomaUsado: string (default "pt-BR", max 10)
NivelUsuarioNaMomentoPergunta: int (1-4)
DataCriacao: DateTime (default: now)

AvaliacaoEstrelas: int? (nullable, 1-5)
ComentarioFeedback: string (nullable, max 500)

TempoRespostaMs: int (default 0)
UsouFAQCache: bool (default false)

Relacionamentos:
├─ Usuario: Usuario (required, many-to-one)
├─ ChatSession: ChatSession (optional, many-to-one)
└─ AssistantFeedback: ICollection<AssistantFeedback>
```

---

### **ChatSession**
```
Id: int (PK)
UsuarioId: int (FK)
LicaoId: int (FK)

Titulo: string (required, max 100)
DataInicio: DateTime (default: now)
DataFim: DateTime? (nullable)
Ativa: bool (default true)

TotalMensagens: int (calculated)
MediaAvaliacoes: decimal (calculated, average of stars)

Relacionamentos:
├─ Usuario: Usuario (required, many-to-one)
├─ Licao: Licao (required, many-to-one)
└─ Mensagens: ICollection<ChatMessage>
```

---

### **AssistantFAQ**
```
Id: int (PK)
ModuloId: int (FK)
LicaoId: int? (FK, nullable)

Pergunta: string (required, unique per modulo, max 500)
Resposta: string (required, large text)
Idioma: string (default "pt-BR", max 10)
NivelMinimo: NivelDificuldade enum

TotalUsos: int (default 0, incremented when retrieved)
DataCriacao: DateTime (default: now)
DataUltimaAtualizacao: DateTime? (nullable)
Ativa: bool (default true)

Relacionamentos:
├─ Modulo: Modulo (required, many-to-one)
└─ Licao: Licao (optional, many-to-one)
```

---

### **AssistantFeedback**
```
Id: int (PK)
ChatMessageId: int (FK)
UsuarioId: int (FK)

Estrelas: int (required, 1-5)
Comentario: string (nullable, max 500)
DataAvaliacao: DateTime (default: now)

RespostaAjudou: bool (default false)
RespostaClara: bool (default false)
RespostaCompleta: bool (default false)

Relacionamentos:
└─ ChatMessage: ChatMessage (required, many-to-one)
```

---

### **CustomExercise**
```
Id: int (PK)
UsuarioId: int (FK)
LicaoId: int (FK)
ChatMessageId: int (FK)

Enunciado: string (required, max 1000)
RespostaCorreta: string (required, large text)
Explicacao: string (required, large text)
Tipo: TipoExercicio enum

Completado: bool (default false)
DataGeracao: DateTime (default: now)
DataCompletacao: DateTime? (nullable)

Relacionamentos:
├─ Usuario: Usuario (required, many-to-one)
├─ Licao: Licao (required, many-to-one)
└─ ChatMessage: ChatMessage (required, many-to-one)
```

---

## 📌 ENUMS

### **NivelDificuldade**
```csharp
public enum NivelDificuldade
{
    Iniciante = 1,
    Intermediario = 2,
    Avancado = 3,
    Especialista = 4
}
```

### **TipoExercicio**
```csharp
public enum TipoExercicio
{
    MultiplaEscolha = 1,
    VerdadeiroFalso = 2,
    Correspondencia = 3,
    CompletarCodigo = 4,
    EscreverCodigo = 5,
    SelecionarErro = 6
}
```

### **TipoAchievement**
```csharp
public enum TipoAchievement
{
    Streak_7_Dias = 1,
    Streak_30_Dias = 2,
    Primeiro_Exercicio = 3,
    Mil_XP = 4,
    Primeiro_Modulo_Completo = 5,
    Especialista_Em_Csharp = 6,
    Ajudante_da_Comunidade = 7
}
```

---

## 🔗 RELACIONAMENTOS CRÍTICOS

```
Usuario (1) ──────→ (N) Progresso ←────── (1) Licao
Usuario (1) ──────→ (N) RespostaUsuario ←────── (1) Exercicio
Usuario (1) ──────→ (N) ChatMessage
Usuario (1) ──────→ (N) ChatSession
Modulo (1) ──────→ (N) Licao
Modulo (1) ──────→ (N) AssistantFAQ
Licao (1) ──────→ (N) Exercicio
Licao (1) ──────→ (N) Progresso
Licao (1) ──────→ (N) AssistantFAQ
ChatSession (1) ──────→ (N) ChatMessage
```

---

## ✅ CHECKLIST: O QUE CADA ENTITY PRECISA

- [ ] **Id** (PK, auto-increment)
- [ ] **DataCriacao** (timestamp)
- [ ] **Required fields** (not nullable)
- [ ] **FK se relacionado** (not nullable por padrão)
- [ ] **Enums tipados** (não string)
- [ ] **Índices** para campos frequentemente consultados (UsuarioId, LicaoId)
- [ ] **Constraints** (unique, max length)
- [ ] **Relacionamentos bidirecionais** (ambos lados)

---

## 🔥 QUICK COPY-PASTE: Full Entity com Todas as Properties

```csharp
public class ChatMessage
{
    public int Id { get; set; }
    
    // Foreign Keys
    public int UsuarioId { get; set; }
    public int? ChatSessionId { get; set; }
    public int? ModuloId { get; set; }
    public int? LicaoId { get; set; }
    public int? ExercicioId { get; set; }
    
    // Content
    public string PerguntaUsuario { get; set; }
    public string RespostaAssistente { get; set; }
    
    // Metadata
    public string IdiomaUsado { get; set; } = "pt-BR";
    public int NivelUsuarioNaMomentoPergunta { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    
    // Feedback
    public int? AvaliacaoEstrelas { get; set; }
    public string ComentarioFeedback { get; set; }
    
    // Performance
    public int TempoRespostaMs { get; set; }
    public bool UsouFAQCache { get; set; }
    
    // Navigation properties
    public Usuario Usuario { get; set; }
    public ChatSession ChatSession { get; set; }
}
```

---

**Versão:** 1.0
**Atualizado:** [HOJE]
