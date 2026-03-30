# ⚡ QUICK REFERENCE - Snippets Prontos
## Use quando precisar criar algo específico (sem copiar MEMORY inteiro)

---

## 🔧 TEMPLATE: Entity Base (ALWAYS USE THIS)

```csharp
public class NomeEntity
{
    public int Id { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    
    // Relacionamentos
    public int? ForeignKeyId { get; set; }
    public RelatedEntity RelatedEntity { get; set; }
}
```

---

## 📚 TEMPLATE: Repository Interface

```csharp
public interface IXxxRepository
{
    Task<Xxx> GetByIdAsync(int id);
    Task<List<Xxx>> GetAllAsync();
    Task<List<Xxx>> GetByUsuarioIdAsync(int usuarioId);
    Task AddAsync(Xxx entity);
    Task UpdateAsync(Xxx entity);
    Task DeleteAsync(int id);
}
```

---

## 📚 TEMPLATE: Repository Implementation

```csharp
public class XxxRepository : IXxxRepository
{
    private readonly AppDbContext _context;
    
    public XxxRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Xxx> GetByIdAsync(int id)
    {
        return await _context.Xxxs.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<List<Xxx>> GetAllAsync()
    {
        return await _context.Xxxs.ToListAsync();
    }
    
    public async Task AddAsync(Xxx entity)
    {
        await _context.Xxxs.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Xxx entity)
    {
        _context.Xxxs.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _context.Xxxs.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
```

---

## 🧠 TEMPLATE: Service Interface

```csharp
public interface IXxxService
{
    Task<XxxDto> GetByIdAsync(int id);
    Task<List<XxxDto>> GetAllAsync();
    Task<XxxDto> CreateAsync(CreateXxxDto dto);
    Task<bool> UpdateAsync(int id, UpdateXxxDto dto);
    Task<bool> DeleteAsync(int id);
}
```

---

## 🧠 TEMPLATE: Service Implementation

```csharp
public class XxxService : IXxxService
{
    private readonly IXxxRepository _repository;
    private readonly IMapper _mapper;
    
    public XxxService(IXxxRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<XxxDto> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<XxxDto>(entity);
    }
    
    public async Task<List<XxxDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<List<XxxDto>>(entities);
    }
    
    public async Task<XxxDto> CreateAsync(CreateXxxDto dto)
    {
        // Validações
        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("Nome é obrigatório");
        
        // Criar entidade
        var entity = _mapper.Map<Xxx>(dto);
        await _repository.AddAsync(entity);
        
        return _mapper.Map<XxxDto>(entity);
    }
}
```

---

## 🔌 TEMPLATE: Controller

```csharp
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class XxxController : ControllerBase
{
    private readonly IXxxService _service;
    
    public XxxController(IXxxService service)
    {
        _service = service;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await _service.GetAllAsync();
        return Ok(results);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateXxxDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}
```

---

## 📝 TEMPLATE: DTO

```csharp
public class XxxDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime DataCriacao { get; set; }
}

public class CreateXxxDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
}

public class UpdateXxxDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
}
```

---

## 🔐 TEMPLATE: Program.cs (DI Setup)

```csharp
// Add services
builder.Services.AddScoped<IXxxRepository, XxxRepository>();
builder.Services.AddScoped<IXxxService, XxxService>();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=csharp_academy.db")
);

// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"])),
            ValidateIssuer = true,
            ValidIssuer = _config["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = _config["Jwt:Audience"]
        };
    });

// Add controllers
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
```

---

## 🗄️ TEMPLATE: AppDbContext

```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    // Duolingo Core
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Modulo> Modulos { get; set; }
    public DbSet<Licao> Licoes { get; set; }
    public DbSet<Exercicio> Exercicios { get; set; }
    public DbSet<RespostaUsuario> RespostasUsuario { get; set; }
    public DbSet<Progresso> Progressos { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    
    // IA Assistant
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<ChatSession> ChatSessions { get; set; }
    public DbSet<AssistantFAQ> AssistantFAQs { get; set; }
    public DbSet<AssistantFeedback> AssistantFeedbacks { get; set; }
    public DbSet<CustomExercise> CustomExercises { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configurações específicas (se precisar)
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Progressos)
            .WithOne(p => p.Usuario)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
```

---

## 🎨 TEMPLATE: Angular Component (Estrutura)

```typescript
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MyService } from '@app/core/services/my.service';

@Component({
  selector: 'app-my-component',
  templateUrl: './my.component.html',
  styleUrls: ['./my.component.css']
})
export class MyComponent implements OnInit {
  
  data: any;
  loading = false;
  error: string | null = null;
  
  constructor(
    private myService: MyService,
    private route: ActivatedRoute
  ) { }
  
  ngOnInit() {
    this.loadData();
  }
  
  loadData() {
    this.loading = true;
    this.myService.getData().subscribe(
      (result) => {
        this.data = result;
        this.loading = false;
      },
      (error) => {
        this.error = error.message;
        this.loading = false;
      }
    );
  }
}
```

---

## 🎨 TEMPLATE: Angular Service

```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MyService {
  
  private apiUrl = 'http://localhost:5000/api/xxx';
  
  constructor(private http: HttpClient) { }
  
  getData(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
  
  getById(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }
  
  create(data: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, data);
  }
  
  update(id: number, data: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, data);
  }
  
  delete(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
```

---

## 🎯 TEMPLATE: Angular Template (HTML)

```html
<div class="container">
  <!-- Loading -->
  <div *ngIf="loading" class="spinner">Carregando...</div>
  
  <!-- Error -->
  <div *ngIf="error" class="alert alert-error">
    {{ error }}
  </div>
  
  <!-- Data -->
  <div *ngIf="!loading && !error && data">
    <h1>{{ data.titulo }}</h1>
    <p>{{ data.descricao }}</p>
  </div>
</div>
```

---

## 🎯 TEMPLATE: CSS Base

```css
.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
}

.spinner {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  font-size: 18px;
}

.alert {
  padding: 12px;
  border-radius: 8px;
  margin-bottom: 16px;
}

.alert-error {
  background: #fee;
  color: #c33;
  border: 1px solid #fcc;
}

.alert-success {
  background: #efe;
  color: #3c3;
  border: 1px solid #cfc;
}

@media (max-width: 768px) {
  .container {
    padding: 10px;
  }
}
```

---

## 🤖 TEMPLATE: PromptBuilder (para IA)

```csharp
public string ConstroirPrompt(Usuario usuario, Licao licao, string pergunta)
{
    var nivelTexto = usuario.NivelAtual switch
    {
        1 => "Iniciante - explicar simples",
        2 => "Intermediário - aprofundar",
        3 => "Avançado - arquitetura",
        4 => "Especialista - profundo",
        _ => "Genérico"
    };
    
    var tamanho = usuario.NivelAtual switch
    {
        1 => "até 200 palavras",
        2 => "200-400 palavras",
        3 => "400-600 palavras",
        4 => "600+ palavras com código",
        _ => "normal"
    };
    
    return $@"
Você é professor de C#/.NET.
Nível aluno: {nivelTexto}
Tamanho resposta: {tamanho}
Lição: {licao.Titulo}
Conteúdo: {licao.ConteudoTeoricoMarkdown}
Pergunta: {pergunta}

Responda de forma educativa.
NÃO dê spoilers de exercícios.
NÃO responda perguntas fora de C#/.NET.
";
}
```

---

## 📊 TEMPLATE: Migration

```bash
# Create migration
dotnet ef migrations add NomeMigracao

# Generated file (Auto-generated, edit se precisar):
public partial class NomeMigracao : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Usuarios",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Usuarios", x => x.Id);
            });
    }
    
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Usuarios");
    }
}
```

---

## 🔐 TEMPLATE: Autenticação JWT

```csharp
[HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginDto dto)
{
    var usuario = await _usuarioService.AutenticarAsync(dto.Email, dto.Senha);
    if (usuario == null)
        return Unauthorized("Credenciais inválidas");
    
    var token = _jwtService.GerarToken(usuario);
    
    return Ok(new
    {
        token,
        usuarioId = usuario.Id,
        nome = usuario.Nome
    });
}
```

---

## 🧪 TEMPLATE: Teste Unitário

```csharp
[TestClass]
public class XxxServiceTests
{
    private IXxxRepository _mockRepository;
    private XxxService _service;
    
    [TestInitialize]
    public void Setup()
    {
        _mockRepository = new Mock<IXxxRepository>().Object;
        _service = new XxxService(_mockRepository);
    }
    
    [TestMethod]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var id = 999;
        
        // Act
        var result = await _service.GetByIdAsync(id);
        
        // Assert
        Assert.IsNull(result);
    }
}
```

---

## 🎯 COMANDOS MAIS USADOS

```bash
# .NET
dotnet new webapi -n ProjectName
dotnet restore
dotnet build
dotnet run
dotnet ef migrations add NomeMigracao
dotnet ef database update
dotnet test

# Angular
ng new ProjectName
ng generate component features/xxx
ng generate service core/services/xxx
ng serve
ng build
ng test
```

---

## 📌 ARQUIVOS PARA SEMPRE TER À MÃO

```
1. MEMORY.md              ← SEMPRE ler primeiro (este projeto)
2. QUICK_REFERENCE.md     ← Snippets rápidos (este arquivo)
3. ENTITIES_SCHEMA.md     ← Schema exato das entities
4. API_ENDPOINTS.md       ← Todos os endpoints
5. PROMPT_RULES.md        ← Regras de prompt para IA
```

---

**Última atualização:** [HOJE]
**Versão:** 1.0
