using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CSharpAcademy.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNovosModulos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Modulos",
                columns: new[] { "Id", "Ativo", "Descricao", "NivelMinimo", "Ordem", "PreRequisitoId", "Titulo" },
                values: new object[,]
                {
                    { 6, true, "DbContext, Migrations, consultas e operações CRUD com EF Core", 2, 6, 5, "Entity Framework Core" },
                    { 7, true, "Criando APIs REST completas com controllers, verbos HTTP e status codes", 2, 7, 6, "CRUD com ASP.NET Web API" },
                    { 8, true, "Data Annotations, FluentValidation e padrão DTO para transferência de dados", 2, 8, 7, "Validação e DTOs" },
                    { 9, true, "Tokens JWT, autenticação, autorização e roles em APIs .NET", 3, 9, 8, "Autenticação JWT" },
                    { 10, true, "Exception handling global, criação de middleware e logging com ILogger", 3, 10, 9, "Middleware e Tratamento de Erros" },
                    { 11, true, "Testes unitários, mocking com Moq e introdução ao TDD", 3, 11, 5, "Testes com xUnit" },
                    { 12, true, "Princípios de DI, IoC Container do .NET e lifetimes de serviços", 3, 12, 5, "Injeção de Dependência" },
                    { 13, true, "Separação de camadas, Use Cases, Entities e aplicando Clean Arch em APIs .NET", 4, 13, 12, "Clean Architecture" }
                });

            migrationBuilder.InsertData(
                table: "Licoes",
                columns: new[] { "Id", "Ativo", "ConteudoTeoricoMarkdown", "Descricao", "ModuloId", "Ordem", "Titulo", "XPRecompensa" },
                values: new object[,]
                {
                    { 16, true, "## Entity Framework Core — DbContext\n\nO **Entity Framework Core** é o ORM oficial do .NET. Ele mapeia classes C# para tabelas do banco, eliminando SQL manual para operações comuns.\n\n### Instalando\n\n```bash\ndotnet add package Microsoft.EntityFrameworkCore.Sqlite\ndotnet add package Microsoft.EntityFrameworkCore.Tools\n```\n\n### Criando o DbContext\n\n```csharp\npublic class AppDbContext : DbContext\n{\n    public DbSet<Produto> Produtos => Set<Produto>();\n\n    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }\n}\n```\n\n`DbSet<T>` representa uma tabela. Cada propriedade `DbSet` vira uma tabela no banco.\n\n### Registrando no Program.cs\n\n```csharp\nbuilder.Services.AddDbContext<AppDbContext>(opt =>\n    opt.UseSqlite(\"Data Source=app.db\"));\n```\n\n### A entidade\n\n```csharp\npublic class Produto\n{\n    public int Id { get; set; }        // PK por convenção\n    public string Nome { get; set; } = string.Empty;\n    public decimal Preco { get; set; }\n}\n```\n\n> **Convenção vs configuração**: EF infere `Id` como chave primária automaticamente. Para casos especiais, use `[Key]` ou `modelBuilder.Entity<T>().HasKey(...)`.", "Configurando o EF Core, DbContext, DbSet e connection string", 6, 1, "DbContext e Configuração", 15 },
                    { 17, true, "## Migrations no EF Core\n\nMigrations são **snapshots incrementais** do schema. Cada migration registra o que mudou no modelo, permitindo evoluir o banco sem perder dados.\n\n### Fluxo básico\n\n```bash\n# 1. Criar migration (detecta mudanças no modelo)\ndotnet ef migrations add CriarTabelaProdutos\n\n# 2. Aplicar ao banco\ndotnet ef database update\n\n# 3. Reverter última migration\ndotnet ef database update NomeDaMigrationAnterior\n```\n\n### O que o EF gera\n\n```csharp\npublic partial class CriarTabelaProdutos : Migration\n{\n    protected override void Up(MigrationBuilder migrationBuilder)\n    {\n        migrationBuilder.CreateTable(\n            name: \"Produtos\",\n            columns: table => new\n            {\n                Id    = table.Column<int>(nullable: false).Annotation(\"Sqlite:Autoincrement\", true),\n                Nome  = table.Column<string>(nullable: false),\n                Preco = table.Column<decimal>(nullable: false)\n            },\n            constraints: t => t.PrimaryKey(\"PK_Produtos\", x => x.Id));\n    }\n\n    protected override void Down(MigrationBuilder migrationBuilder)\n    {\n        migrationBuilder.DropTable(name: \"Produtos\");\n    }\n}\n```\n\n> **Dica**: Commite as migrations junto com o código. Elas são parte do contrato do sistema.", "Criando e aplicando migrations para versionar o schema do banco", 6, 2, "Migrations", 15 },
                    { 18, true, "## CRUD com Entity Framework Core\n\nCom o DbContext configurado, todas as operações são feitas via C# — sem escrever SQL.\n\n### Create\n\n```csharp\nvar produto = new Produto { Nome = \"Teclado\", Preco = 299.90m };\nctx.Produtos.Add(produto);\nawait ctx.SaveChangesAsync(); // persiste no banco\n```\n\n### Read\n\n```csharp\n// Todos\nvar todos = await ctx.Produtos.ToListAsync();\n\n// Por ID\nvar p = await ctx.Produtos.FindAsync(1);\n\n// Com filtro\nvar baratos = await ctx.Produtos\n    .Where(p => p.Preco < 100)\n    .OrderBy(p => p.Nome)\n    .ToListAsync();\n```\n\n### Update\n\n```csharp\nvar produto = await ctx.Produtos.FindAsync(id);\nif (produto is not null)\n{\n    produto.Preco = 349.90m;\n    await ctx.SaveChangesAsync(); // EF detecta a mudança automaticamente\n}\n```\n\n### Delete\n\n```csharp\nvar produto = await ctx.Produtos.FindAsync(id);\nif (produto is not null)\n{\n    ctx.Produtos.Remove(produto);\n    await ctx.SaveChangesAsync();\n}\n```\n\n> **Change Tracking**: O EF monitora todas as entidades carregadas. `SaveChangesAsync()` gera o SQL mínimo necessário para sincronizar as mudanças.", "Create, Read, Update e Delete usando DbContext e LINQ", 6, 3, "CRUD com EF Core", 15 },
                    { 19, true, "## ASP.NET Core Web API\n\nO ASP.NET Core é o framework web do .NET. Uma **Web API REST** expõe recursos via HTTP, usando verbos (GET, POST, PUT, DELETE) e retornando JSON.\n\n### Program.cs mínimo\n\n```csharp\nvar builder = WebApplication.CreateBuilder(args);\nbuilder.Services.AddControllers();\n\nvar app = builder.Build();\napp.MapControllers();\napp.Run();\n```\n\n### Atributos de rota\n\n```csharp\n[ApiController]          // habilita validação automática e binding\n[Route(\"api/produtos\")] // rota base do controller\npublic class ProdutosController : ControllerBase { }\n```\n\n### ControllerBase vs Controller\n\n| | ControllerBase | Controller |\n|---|---|---|\n| API REST | ✅ preferido | ✅ |\n| Views MVC | ❌ | ✅ |\n| Peso | leve | completo |\n\n> Use sempre `ControllerBase` para APIs — `Controller` carrega suporte a Views que você não precisa.", "Estrutura de uma Web API, roteamento e o pipeline do ASP.NET", 7, 1, "Criando uma API REST", 15 },
                    { 20, true, "## Controllers e Actions\n\nCada método público de um controller é uma **action** — um endpoint HTTP.\n\n### CRUD completo\n\n```csharp\n[HttpGet]                          // GET /api/produtos\npublic async Task<IActionResult> Listar()\n    => Ok(await repo.ObterTodosAsync());\n\n[HttpGet(\"{id}\")]                  // GET /api/produtos/5\npublic async Task<IActionResult> ObterPorId(int id)\n{\n    var p = await repo.ObterPorIdAsync(id);\n    return p is null ? NotFound() : Ok(p);\n}\n\n[HttpPost]                         // POST /api/produtos\npublic async Task<IActionResult> Criar([FromBody] ProdutoDto dto)\n{\n    var produto = new Produto { Nome = dto.Nome, Preco = dto.Preco };\n    await repo.AdicionarAsync(produto);\n    return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);\n}\n\n[HttpPut(\"{id}\")]                  // PUT /api/produtos/5\npublic async Task<IActionResult> Atualizar(int id, [FromBody] ProdutoDto dto) { ... }\n\n[HttpDelete(\"{id}\")]               // DELETE /api/produtos/5\npublic async Task<IActionResult> Deletar(int id) { ... }\n```\n\n### Binding de parâmetros\n\n| Atributo | Origem |\n|---|---|\n| `[FromRoute]` | URL `/api/produtos/{id}` |\n| `[FromQuery]` | Query string `?page=2` |\n| `[FromBody]` | Corpo JSON da requisição |\n| `[FromHeader]` | Cabeçalho HTTP |", "Criando endpoints, recebendo parâmetros e retornando IActionResult", 7, 2, "Controllers e Actions", 15 },
                    { 21, true, "## Status Codes HTTP\n\nUsar os status codes corretos é fundamental para uma API REST bem projetada.\n\n### Métodos do ControllerBase\n\n```csharp\nOk(objeto)           // 200 — sucesso com corpo\nCreatedAtAction(...) // 201 — criado com Location header\nNoContent()          // 204 — sucesso sem corpo (PUT/DELETE)\nBadRequest(erro)     // 400 — dados inválidos\nUnauthorized()       // 401 — não autenticado\nForbidden()          // 403 — autenticado mas sem permissão\nNotFound()           // 404 — recurso não encontrado\nConflict(...)        // 409 — conflito (ex: email duplicado)\n```\n\n### Exemplo prático — DELETE\n\n```csharp\n[HttpDelete(\"{id}\")]\npublic async Task<IActionResult> Deletar(int id)\n{\n    var produto = await ctx.Produtos.FindAsync(id);\n    if (produto is null) return NotFound();   // 404\n\n    ctx.Produtos.Remove(produto);\n    await ctx.SaveChangesAsync();\n    return NoContent();                       // 204\n}\n```\n\n### Por que 201 em vez de 200 no POST?\n\n`CreatedAtAction` retorna 201 **e** um header `Location` apontando para o recurso criado. Clientes REST podem usar esse header para buscar o recurso recém-criado sem hardcodar URLs.", "Retornando os códigos HTTP corretos em cada situação", 7, 3, "Status Codes e Respostas HTTP", 15 },
                    { 22, true, "## Data Annotations\n\nData Annotations são **atributos** que definem regras de validação diretamente na classe. Com `[ApiController]`, o ASP.NET valida automaticamente antes de executar a action.\n\n### Atributos principais\n\n```csharp\npublic class CriarProdutoDto\n{\n    [Required(ErrorMessage = \"Nome é obrigatório\")]\n    [StringLength(100, MinimumLength = 3)]\n    public string Nome { get; set; } = string.Empty;\n\n    [Range(0.01, 99999.99, ErrorMessage = \"Preço deve estar entre 0,01 e 99.999,99\")]\n    public decimal Preco { get; set; }\n\n    [EmailAddress]\n    public string? EmailFornecedor { get; set; }\n\n    [RegularExpression(@\"^\\d{2}\\.\\d{3}-\\d{3}$\")]\n    public string? CodigoInterno { get; set; }\n}\n```\n\n### Como funciona com [ApiController]\n\n```csharp\n[HttpPost]\npublic async Task<IActionResult> Criar([FromBody] CriarProdutoDto dto)\n{\n    // Se dto for inválido, o ASP.NET retorna 400 automaticamente\n    // ModelState.IsValid sempre será true aqui\n    ...\n}\n```\n\n> Sem `[ApiController]`, você precisaria verificar `if (!ModelState.IsValid) return BadRequest(ModelState);` manualmente.", "Validando modelos com atributos [Required], [Range], [StringLength] e outros", 8, 1, "Data Annotations", 15 },
                    { 23, true, "## Data Transfer Objects (DTOs)\n\nDTO é um objeto simples que transporta dados entre camadas. **Nunca exponha sua entidade de domínio diretamente na API** — isso vaza detalhes internos e cria acoplamento.\n\n### Por que usar DTOs?\n\n```csharp\n// ❌ Expor a entidade diretamente\n[HttpGet]\npublic async Task<IActionResult> Listar()\n    => Ok(await ctx.Usuarios.ToListAsync()); // vaza SenhaHash, CreatedAt, etc.\n\n// ✅ Usar um DTO\n[HttpGet]\npublic async Task<IActionResult> Listar()\n{\n    var usuarios = await ctx.Usuarios\n        .Select(u => new UsuarioResponseDto\n        {\n            Id = u.Id,\n            Nome = u.Nome,\n            Email = u.Email\n        }).ToListAsync();\n    return Ok(usuarios);\n}\n```\n\n### Separando Request e Response\n\n```csharp\n// Request — o que o cliente envia\npublic class CriarProdutoDto\n{\n    public string Nome { get; set; } = string.Empty;\n    public decimal Preco { get; set; }\n}\n\n// Response — o que a API retorna\npublic class ProdutoResponseDto\n{\n    public int Id { get; set; }\n    public string Nome { get; set; } = string.Empty;\n    public decimal Preco { get; set; }\n    public DateTime CriadoEm { get; set; }\n}\n```\n\n> **Regra de ouro**: Request DTOs definem o **contrato de entrada**. Response DTOs definem o **contrato de saída**. Mantenha-os separados e versionáveis.", "O que são DTOs, por que usá-los e como separar Request de Response", 8, 2, "Padrão DTO", 15 },
                    { 24, true, "## FluentValidation\n\nFluentValidation é uma biblioteca que define regras de validação em classes dedicadas, separando a validação do modelo e permitindo lógica complexa.\n\n```bash\ndotnet add package FluentValidation.AspNetCore\n```\n\n### Criando um validator\n\n```csharp\npublic class CriarProdutoValidator : AbstractValidator<CriarProdutoDto>\n{\n    public CriarProdutoValidator()\n    {\n        RuleFor(x => x.Nome)\n            .NotEmpty().WithMessage(\"Nome é obrigatório\")\n            .MinimumLength(3).WithMessage(\"Mínimo 3 caracteres\")\n            .MaximumLength(100);\n\n        RuleFor(x => x.Preco)\n            .GreaterThan(0).WithMessage(\"Preço deve ser positivo\");\n\n        // Validação condicional\n        When(x => x.EmailFornecedor is not null, () =>\n        {\n            RuleFor(x => x.EmailFornecedor).EmailAddress();\n        });\n    }\n}\n```\n\n### Registrando\n\n```csharp\nbuilder.Services.AddValidatorsFromAssemblyContaining<CriarProdutoValidator>();\n```\n\n> **Data Annotations vs FluentValidation**: Use Annotations para validações simples e diretas. Use FluentValidation quando precisar de lógica condicional, mensagens dinâmicas ou reuso entre validators.", "Validações expressivas e reutilizáveis com FluentValidation", 8, 3, "FluentValidation", 15 },
                    { 25, true, "## JSON Web Token (JWT)\n\nJWT é um padrão para transmitir informações de forma segura entre partes como um token compacto e autocontido. Diferente de sessões, é **stateless** — o servidor não precisa armazenar estado.\n\n### Estrutura do JWT\n\nUm JWT tem 3 partes separadas por `.`:\n\n```\neyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjMiLCJuYW1lIjoiTWFyaWEifQ.SflKxwRJS...\n   HEADER               PAYLOAD                              SIGNATURE\n```\n\n- **Header**: algoritmo usado (ex: HS256)\n- **Payload**: claims (dados do usuário)\n- **Signature**: garante que o token não foi adulterado\n\n### Claims\n\nClaims são pares chave-valor no payload:\n\n```csharp\nvar claims = new[]\n{\n    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),\n    new Claim(ClaimTypes.Name, usuario.Nome),\n    new Claim(ClaimTypes.Email, usuario.Email),\n    new Claim(ClaimTypes.Role, \"Admin\")  // roles para autorização\n};\n```\n\n> **Não coloque dados sensíveis no JWT!** O payload é apenas Base64 — qualquer pessoa pode decodificá-lo. A assinatura garante autenticidade, não confidencialidade.", "Estrutura do token JWT, claims e como funciona a autenticação stateless", 9, 1, "Fundamentos do JWT", 20 },
                    { 26, true, "## Implementando JWT no ASP.NET Core\n\n### Instalação\n\n```bash\ndotnet add package Microsoft.AspNetCore.Authentication.JwtBearer\n```\n\n### Gerando o token\n\n```csharp\npublic string GerarToken(Usuario usuario)\n{\n    var chave = new SymmetricSecurityKey(\n        Encoding.UTF8.GetBytes(\"sua-chave-secreta-minimo-32-chars\"));\n    var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);\n\n    var claims = new[]\n    {\n        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),\n        new Claim(ClaimTypes.Name, usuario.Nome)\n    };\n\n    var token = new JwtSecurityToken(\n        issuer: \"minha-api\",\n        audience: \"meu-app\",\n        claims: claims,\n        expires: DateTime.UtcNow.AddHours(8),\n        signingCredentials: credenciais);\n\n    return new JwtSecurityTokenHandler().WriteToken(token);\n}\n```\n\n### Configurando no Program.cs\n\n```csharp\nbuilder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)\n    .AddJwtBearer(opt =>\n    {\n        opt.TokenValidationParameters = new()\n        {\n            ValidateIssuerSigningKey = true,\n            IssuerSigningKey = new SymmetricSecurityKey(\n                Encoding.UTF8.GetBytes(config[\"Jwt:Key\"]!)),\n            ValidateIssuer = false,\n            ValidateAudience = false\n        };\n    });\n\napp.UseAuthentication();\napp.UseAuthorization();\n```", "Gerando tokens, configurando autenticação e protegendo endpoints", 9, 2, "Implementando JWT no .NET", 20 },
                    { 27, true, "## Autorização no ASP.NET Core\n\n**Autenticação** = quem você é. **Autorização** = o que você pode fazer.\n\n### [Authorize] básico\n\n```csharp\n[Authorize]  // qualquer usuário autenticado\n[HttpGet]\npublic IActionResult Protegido() => Ok(\"Apenas logados\");\n\n[AllowAnonymous]  // explicitamente público\n[HttpGet(\"publico\")]\npublic IActionResult Publico() => Ok(\"Todos podem\");\n```\n\n### Roles\n\n```csharp\n[Authorize(Roles = \"Admin\")]  // somente Admins\n[HttpDelete(\"{id}\")]\npublic IActionResult Deletar(int id) { ... }\n\n[Authorize(Roles = \"Admin,Moderador\")]  // Admin OU Moderador\n[HttpPut(\"{id}\")]\npublic IActionResult Atualizar(int id) { ... }\n```\n\n### Lendo claims na action\n\n```csharp\n[Authorize]\n[HttpGet(\"meu-perfil\")]\npublic IActionResult MeuPerfil()\n{\n    var userId = int.Parse(\n        User.FindFirstValue(ClaimTypes.NameIdentifier)!);\n    var nome = User.FindFirstValue(ClaimTypes.Name);\n    return Ok(new { userId, nome });\n}\n```\n\n> A propriedade `User` no controller é um `ClaimsPrincipal` populado automaticamente pelo middleware JWT a partir do token.", "Protegendo endpoints com [Authorize], roles e claims personalizadas", 9, 3, "Autorização com Roles", 20 },
                    { 28, true, "## Tratamento Global de Erros\n\nTratar erros individualmente em cada action gera código duplicado e inconsistente. O ASP.NET Core oferece mecanismos para centralizar o tratamento.\n\n### UseExceptionHandler\n\n```csharp\napp.UseExceptionHandler(errApp =>\n{\n    errApp.Run(async ctx =>\n    {\n        ctx.Response.StatusCode = 500;\n        ctx.Response.ContentType = \"application/json\";\n        var erro = ctx.Features.Get<IExceptionHandlerFeature>();\n        await ctx.Response.WriteAsJsonAsync(new\n        {\n            erro = \"Ocorreu um erro interno.\",\n            detalhe = erro?.Error.Message\n        });\n    });\n});\n```\n\n### Problem Details (recomendado no .NET 7+)\n\n```csharp\nbuilder.Services.AddProblemDetails();\napp.UseExceptionHandler();\napp.UseStatusCodePages();\n```\n\nO ASP.NET passa a retornar respostas de erro no formato RFC 7807:\n\n```json\n{\n  \"type\": \"https://tools.ietf.org/html/rfc7231#section-6.6.1\",\n  \"title\": \"An error occurred while processing your request.\",\n  \"status\": 500\n}\n```\n\n> **Nunca exponha stack traces em produção.** Use `app.Environment.IsDevelopment()` para mostrar detalhes apenas no desenvolvimento.", "Capturando exceções globalmente com UseExceptionHandler e Problem Details", 10, 1, "Exception Handler Global", 18 },
                    { 29, true, "## Middleware no ASP.NET Core\n\nO pipeline do ASP.NET é uma cadeia de middlewares. Cada um pode processar a requisição, passar para o próximo (`next`) ou encerrar a resposta.\n\n```\nRequest →  [Auth] → [Logging] → [Routing] → Controller → Response\n```\n\n### Criando um middleware de logging\n\n```csharp\npublic class LoggingMiddleware : IMiddleware\n{\n    private readonly ILogger<LoggingMiddleware> _logger;\n\n    public LoggingMiddleware(ILogger<LoggingMiddleware> logger)\n        => _logger = logger;\n\n    public async Task InvokeAsync(HttpContext context, RequestDelegate next)\n    {\n        var sw = Stopwatch.StartNew();\n        _logger.LogInformation(\"{Method} {Path} iniciado\",\n            context.Request.Method, context.Request.Path);\n\n        await next(context); // passa para o próximo middleware\n\n        sw.Stop();\n        _logger.LogInformation(\"{Method} {Path} → {Status} em {Ms}ms\",\n            context.Request.Method, context.Request.Path,\n            context.Response.StatusCode, sw.ElapsedMilliseconds);\n    }\n}\n```\n\n### Registrando\n\n```csharp\nbuilder.Services.AddTransient<LoggingMiddleware>();\napp.UseMiddleware<LoggingMiddleware>();\n```\n\n> A ordem importa! Middlewares são executados na ordem em que são registrados.", "Pipeline de requisições, criando middleware customizado com IMiddleware", 10, 2, "Criando Middleware", 18 },
                    { 30, true, "## Logging no .NET\n\nO .NET tem um sistema de logging built-in via `ILogger<T>`. Evite `Console.WriteLine` em produção — use logging estruturado.\n\n### Injetando e usando\n\n```csharp\npublic class ProdutosController : ControllerBase\n{\n    private readonly ILogger<ProdutosController> _logger;\n\n    public ProdutosController(ILogger<ProdutosController> logger)\n        => _logger = logger;\n\n    [HttpGet]\n    public async Task<IActionResult> Listar()\n    {\n        _logger.LogInformation(\"Listando produtos\");\n        try\n        {\n            var produtos = await repo.ObterTodosAsync();\n            return Ok(produtos);\n        }\n        catch (Exception ex)\n        {\n            _logger.LogError(ex, \"Erro ao listar produtos\");\n            return StatusCode(500);\n        }\n    }\n}\n```\n\n### Níveis de log (em ordem crescente de severidade)\n\n| Nível | Uso |\n|---|---|\n| `LogTrace` | Diagnóstico muito detalhado |\n| `LogDebug` | Informações de depuração |\n| `LogInformation` | Fluxo normal da aplicação |\n| `LogWarning` | Situação inesperada mas recuperável |\n| `LogError` | Erro que causou falha na operação |\n| `LogCritical` | Falha grave — sistema pode parar |\n\n> Use **logging estruturado**: `_logger.LogInformation(\"Usuário {Id} criado\", usuario.Id)` em vez de interpolação. Facilita busca e análise nos sistemas de log.", "Usando ILogger, níveis de log e configurando providers", 10, 3, "Logging com ILogger", 18 },
                    { 31, true, "## Testes Unitários com xUnit\n\nTestes unitários verificam uma **unidade isolada** de código (método ou classe) sem dependências externas.\n\n### Setup\n\n```bash\ndotnet new xunit -n MeuProjeto.Tests\ndotnet add reference ../MeuProjeto/MeuProjeto.csproj\n```\n\n### Estrutura AAA\n\n```csharp\npublic class CalculadoraTests\n{\n    [Fact]  // marca como teste\n    public void Somar_DoisNumeros_RetornaSoma()\n    {\n        // Arrange — prepara o cenário\n        var calc = new Calculadora();\n\n        // Act — executa a ação\n        var resultado = calc.Somar(2, 3);\n\n        // Assert — verifica o resultado\n        Assert.Equal(5, resultado);\n    }\n\n    [Theory]  // teste parametrizado\n    [InlineData(2, 3, 5)]\n    [InlineData(-1, 1, 0)]\n    [InlineData(0, 0, 0)]\n    public void Somar_Parametrizado(int a, int b, int esperado)\n    {\n        var calc = new Calculadora();\n        Assert.Equal(esperado, calc.Somar(a, b));\n    }\n}\n```\n\n> **[Fact]** = um teste único. **[Theory]** + **[InlineData]** = mesmo teste com múltiplos inputs.", "O que testar, estrutura Arrange-Act-Assert e escrevendo testes com xUnit", 11, 1, "Fundamentos de Testes Unitários", 18 },
                    { 32, true, "## Mocking com Moq\n\nMocks substituem dependências reais (banco, email, API externa) por objetos controlados.\n\n```bash\ndotnet add package Moq\n```\n\n### Exemplo — testando um service\n\n```csharp\npublic class ProdutoServiceTests\n{\n    [Fact]\n    public async Task ObterPorId_ProdutoExistente_RetornaProduto()\n    {\n        // Arrange\n        var mockRepo = new Mock<IProdutoRepository>();\n        mockRepo.Setup(r => r.ObterPorIdAsync(1))\n                .ReturnsAsync(new Produto { Id = 1, Nome = \"Teclado\" });\n\n        var service = new ProdutoService(mockRepo.Object);\n\n        // Act\n        var produto = await service.ObterPorIdAsync(1);\n\n        // Assert\n        Assert.NotNull(produto);\n        Assert.Equal(\"Teclado\", produto.Nome);\n        mockRepo.Verify(r => r.ObterPorIdAsync(1), Times.Once);\n    }\n}\n```\n\n### Operações principais do Moq\n\n```csharp\nmock.Setup(x => x.Metodo()).Returns(valor);         // configura retorno\nmock.Setup(x => x.MetodoAsync()).ReturnsAsync(valor); // async\nmock.Verify(x => x.Metodo(), Times.Once());           // verifica chamada\nmock.Verify(x => x.Metodo(), Times.Never());          // verifica que NÃO foi chamado\n```", "Isolando dependências com mocks para testar unidades em isolamento", 11, 2, "Mocking com Moq", 18 },
                    { 33, true, "## TDD — Test-Driven Development\n\nTDD inverte o fluxo: você escreve o **teste primeiro**, depois o código para fazê-lo passar.\n\n### Ciclo Red-Green-Refactor\n\n```\n🔴 RED    → Escreva um teste que falha (o código ainda não existe)\n🟢 GREEN  → Escreva o mínimo de código para o teste passar\n🔵 REFACTOR → Melhore o código sem quebrar o teste\n```\n\n### Exemplo prático\n\n```csharp\n// 🔴 RED — escrevo o teste primeiro\n[Fact]\npublic void Desconto_PedidoAcima100_Aplica10Porcento()\n{\n    var pedido = new Pedido { Total = 200m };\n    pedido.AplicarDesconto();\n    Assert.Equal(180m, pedido.Total);\n}\n\n// 🟢 GREEN — mínimo para passar\npublic void AplicarDesconto()\n{\n    if (Total >= 100) Total *= 0.9m;\n}\n\n// 🔵 REFACTOR — melhoro o design\npublic void AplicarDesconto()\n{\n    const decimal LimiteDesconto = 100m;\n    const decimal PercentualDesconto = 0.10m;\n    if (Total >= LimiteDesconto)\n        Total -= Total * PercentualDesconto;\n}\n```\n\n> **Benefício principal**: TDD força você a pensar na **interface** antes da implementação, resultando em código mais modular e testável.", "O ciclo Red-Green-Refactor e como o TDD guia o design do código", 11, 3, "Test-Driven Development", 18 },
                    { 34, true, "## Injeção de Dependência\n\n**Injeção de Dependência (DI)** é um padrão onde um objeto recebe suas dependências de fora, em vez de criá-las internamente. Isso implementa o princípio **Dependency Inversion (D do SOLID)**.\n\n### Sem DI — fortemente acoplado\n\n```csharp\npublic class PedidoService\n{\n    private readonly EmailService _email = new EmailService(); // acoplamento!\n    private readonly SqlRepository _repo = new SqlRepository(\"connection...\");\n\n    public void Processar(Pedido p)\n    {\n        _repo.Salvar(p);\n        _email.Enviar(p.Email, \"Pedido confirmado\");\n    }\n    // impossível testar sem banco e servidor de email reais\n}\n```\n\n### Com DI — desacoplado\n\n```csharp\npublic class PedidoService\n{\n    private readonly IPedidoRepository _repo;\n    private readonly IEmailService _email;\n\n    public PedidoService(IPedidoRepository repo, IEmailService email)\n    {\n        _repo = repo;   // recebe de fora\n        _email = email; // recebe de fora\n    }\n\n    public void Processar(Pedido p)\n    {\n        _repo.Salvar(p);\n        _email.Enviar(p.Email, \"Pedido confirmado\");\n    }\n    // testável: basta passar mocks no construtor\n}\n```\n\n> **IoC (Inversion of Control)**: em vez de a classe controlar suas dependências, o **contêiner** injeta o que ela precisa.", "O que é injeção de dependência, inversão de controle e por que usar", 12, 1, "Princípios de DI", 18 },
                    { 35, true, "## IoC Container do ASP.NET Core\n\nO .NET tem um contêiner de DI built-in via `IServiceCollection`. Você registra serviços no `Program.cs` e o framework injeta automaticamente onde necessário.\n\n### Registrando serviços\n\n```csharp\n// Interfaces → implementações\nbuilder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();\nbuilder.Services.AddScoped<IEmailService, SmtpEmailService>();\nbuilder.Services.AddSingleton<ICacheService, MemoryCacheService>();\nbuilder.Services.AddTransient<IValidationService, ValidationService>();\n\n// Classe concreta (sem interface)\nbuilder.Services.AddScoped<ReportGenerator>();\n```\n\n### Resolvendo fora do construtor\n\n```csharp\n// Em casos especiais, resolução manual\nusing var scope = app.Services.CreateScope();\nvar repo = scope.ServiceProvider.GetRequiredService<IProdutoRepository>();\n```\n\n### Injeção em controllers\n\n```csharp\npublic class ProdutosController : ControllerBase\n{\n    public ProdutosController(\n        IProdutoRepository repo,    // injetado automaticamente\n        ILogger<ProdutosController> logger)  // também injetado\n    { ... }\n}\n```\n\n> O contêiner resolve recursivamente: se `ProdutoRepository` depende de `AppDbContext`, o contêiner injeta o `DbContext` também.", "Registrando serviços no IServiceCollection e resolvendo dependências", 12, 2, "IoC Container no .NET", 18 },
                    { 36, true, "## Lifetimes de Serviços\n\nO lifetime define **por quanto tempo** uma instância do serviço é reutilizada.\n\n### Os três lifetimes\n\n| Lifetime | Instância criada | Quando usar |\n|---|---|---|\n| **Transient** | A cada injeção | Serviços leves, stateless |\n| **Scoped** | Uma por requisição HTTP | DbContext, repositórios |\n| **Singleton** | Uma para toda a app | Cache, configuração, logger |\n\n### Exemplos\n\n```csharp\n// Transient — nova instância toda vez\nbuilder.Services.AddTransient<IEmailValidator, EmailValidator>();\n\n// Scoped — mesma instância durante a requisição\nbuilder.Services.AddScoped<AppDbContext>();  // EF Core requer Scoped\nbuilder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();\n\n// Singleton — mesma instância para sempre\nbuilder.Services.AddSingleton<IConfiguration>(builder.Configuration);\nbuilder.Services.AddSingleton<ICacheService, InMemoryCacheService>();\n```\n\n### ⚠️ Captive Dependency\n\n```csharp\n// ERRO: Singleton capturando um Scoped\n// O Scoped vive mais do que deveria!\npublic class MeuSingleton\n{\n    public MeuSingleton(AppDbContext ctx) { }  // DbContext é Scoped!\n}\n// O .NET lança exceção se ValidateScopes = true\n```\n\n> Regra: **Singleton** nunca deve depender de **Scoped** ou **Transient**. Um serviço de vida longa não pode capturar um de vida curta.", "Entendendo os ciclos de vida dos serviços e quando usar cada um", 12, 3, "Lifetimes: Transient, Scoped, Singleton", 18 },
                    { 37, true, "## Clean Architecture\n\nProposta por Robert C. Martin (Uncle Bob), a Clean Architecture organiza o código em **camadas concêntricas**, onde as dependências sempre apontam para dentro — do código de infraestrutura para o núcleo de domínio.\n\n### Os círculos (de dentro para fora)\n\n```\n┌─────────────────────────────────────────┐\n│  Infrastructure (EF, HTTP, Email)       │\n│  ┌───────────────────────────────────┐  │\n│  │  Interface Adapters (Controllers) │  │\n│  │  ┌─────────────────────────────┐  │  │\n│  │  │  Application (Use Cases)    │  │  │\n│  │  │  ┌───────────────────────┐  │  │  │\n│  │  │  │  Domain (Entities)    │  │  │  │\n│  │  │  └───────────────────────┘  │  │  │\n│  │  └─────────────────────────────┘  │  │\n│  └───────────────────────────────────┘  │\n└─────────────────────────────────────────┘\n```\n\n### A Regra da Dependência\n\n> *Dependências de código-fonte só podem apontar para dentro.*\n\n- **Domain**: entidades e regras de negócio puras — **zero dependências externas**\n- **Application**: casos de uso, orquestra o domínio — depende só do Domain\n- **Infrastructure**: EF Core, HTTP, email — implementa interfaces do Domain/Application\n- **Presentation**: controllers, CLI, gRPC — depende da Application\n\n> O Domain não conhece EF Core, ASP.NET ou qualquer framework. Ele é **puro C#** e pode ser testado sem infraestrutura.", "Por que separar em camadas, a Regra da Dependência e os círculos da Clean Architecture", 13, 1, "Separação de Camadas", 25 },
                    { 38, true, "## Entities, Use Cases e Interfaces\n\n### Entities (Domain Layer)\n\n```csharp\n// Domínio puro — sem atributos de EF, sem DTOs\npublic class Pedido\n{\n    public int Id { get; private set; }\n    public decimal Total { get; private set; }\n    public StatusPedido Status { get; private set; }\n\n    public void Confirmar()\n    {\n        if (Status != StatusPedido.Pendente)\n            throw new DomainException(\"Apenas pedidos pendentes podem ser confirmados\");\n        Status = StatusPedido.Confirmado;\n    }\n}\n```\n\n### Use Cases (Application Layer)\n\n```csharp\n// Um Use Case = uma ação do sistema\npublic class ConfirmarPedidoUseCase\n{\n    private readonly IPedidoRepository _repo;  // interface do Domain\n    private readonly IEmailService _email;     // interface do Domain\n\n    public ConfirmarPedidoUseCase(\n        IPedidoRepository repo, IEmailService email)\n    { _repo = repo; _email = email; }\n\n    public async Task ExecutarAsync(int pedidoId)\n    {\n        var pedido = await _repo.ObterPorIdAsync(pedidoId)\n            ?? throw new NotFoundException(\"Pedido não encontrado\");\n\n        pedido.Confirmar();  // lógica no domínio\n        await _repo.SalvarAsync();\n        await _email.EnviarConfirmacaoAsync(pedido);\n    }\n}\n```\n\n### Dependency Inversion aplicado\n\n```csharp\n// Domain define a interface\npublic interface IPedidoRepository\n{\n    Task<Pedido?> ObterPorIdAsync(int id);\n    Task SalvarAsync();\n}\n\n// Infrastructure implementa\npublic class EfPedidoRepository : IPedidoRepository\n{\n    // usa EF Core — Domain não sabe disso\n}\n```", "Modelando o domínio, definindo Use Cases e usando o Dependency Inversion", 13, 2, "Entities, Use Cases e Interfaces", 25 },
                    { 39, true, "## Aplicando Clean Architecture em uma API .NET\n\n### Estrutura de projetos (solution)\n\n```\nMeuApp.sln\n├── MeuApp.Domain/           → Entities, Interfaces, Domain Exceptions\n├── MeuApp.Application/      → Use Cases, DTOs, Validators\n├── MeuApp.Infrastructure/   → EF Core, Repositórios, Serviços externos\n└── MeuApp.API/              → Controllers, Program.cs, Middleware\n```\n\n**Referências entre projetos:**\n```\nAPI → Application → Domain\nInfrastructure → Domain  (implementa interfaces)\nAPI → Infrastructure     (apenas para registrar DI)\n```\n\n### Program.cs — montando tudo\n\n```csharp\n// API sabe de tudo (apenas para registrar)\nbuilder.Services.AddScoped<IPedidoRepository, EfPedidoRepository>();\nbuilder.Services.AddScoped<IEmailService, SmtpEmailService>();\nbuilder.Services.AddScoped<ConfirmarPedidoUseCase>();\n```\n\n### Controller delegando para Use Case\n\n```csharp\n[HttpPost(\"{id}/confirmar\")]\npublic async Task<IActionResult> Confirmar(\n    int id,\n    [FromServices] ConfirmarPedidoUseCase useCase)\n{\n    await useCase.ExecutarAsync(id);\n    return NoContent();\n}\n// Controller tem zero lógica de negócio\n```\n\n> **Clean Architecture vs camadas tradicionais**: a diferença chave é que as interfaces ficam no **Domain** (centro), não na Infrastructure. Isso inverte a dependência e protege o núcleo do negócio.", "Estrutura de projetos, organização de pastas e montando a aplicação", 13, 3, "Clean Architecture em APIs .NET", 25 }
                });

            migrationBuilder.InsertData(
                table: "Exercicios",
                columns: new[] { "Id", "DicaTexto", "Enunciado", "Explicacao", "LicaoId", "OpcoesJson", "Ordem", "RespostaCorreta", "Tipo", "XPRecompensa" },
                values: new object[,]
                {
                    { 50, null, "Qual classe deve ser herdada para criar um contexto do Entity Framework Core?", "DbContext é a classe base do EF Core que gerencia a conexão, o rastreamento de entidades e as operações no banco.", 16, "[\"DbContext\",\"DbSet\",\"EntityBase\",\"DataContext\"]", 1, "DbContext", 1, 10 },
                    { 51, null, "Como declarar uma tabela 'Produtos' no DbContext?", "DbSet<T> representa a tabela no banco. A propriedade Set<T>() é a forma recomendada de expor o DbSet.", 16, "[\"public DbSet<Produto> Produtos => Set<Produto>();\",\"public List<Produto> Produtos;\",\"public Table<Produto> Produtos;\",\"public IQueryable<Produto> Produtos;\"]", 2, "public DbSet<Produto> Produtos => Set<Produto>();", 1, 10 },
                    { 52, null, "Complete o registro do DbContext no Program.cs:\n\nbuilder.Services.___<AppDbContext>(opt => opt.UseSqlite(\"Data Source=app.db\"));", "AddDbContext registra o AppDbContext no contêiner de DI com o lifetime Scoped (uma instância por requisição).", 16, "[\"AddDbContext\",\"AddSingleton\",\"UseDbContext\",\"RegisterContext\"]", 3, "AddDbContext", 3, 10 },
                    { 53, null, "Qual comando cria uma nova migration no EF Core?", "O comando 'add' detecta diferenças entre o modelo C# e o banco, gerando os arquivos Up() e Down() da migration.", 17, "[\"dotnet ef migrations add NomeMigration\",\"dotnet ef database update\",\"dotnet ef schema create\",\"dotnet migrations new\"]", 1, "dotnet ef migrations add NomeMigration", 1, 10 },
                    { 54, null, "O que faz o método Down() em uma migration?", "Down() é o inverso de Up(). Ele desfaz a migration, permitindo rollback para o estado anterior do schema.", 17, "[\"Reverte as mudanças aplicadas pelo Up()\",\"Aplica as mudanças no banco\",\"Deleta o banco de dados\",\"Lista as migrations pendentes\"]", 2, "Reverte as mudanças aplicadas pelo Up()", 1, 10 },
                    { 55, null, "Para aplicar as migrations pendentes ao banco, use:\n\ndotnet ef database ___", "dotnet ef database update aplica todas as migrations ainda não executadas no banco de dados.", 17, "[\"update\",\"apply\",\"migrate\",\"run\"]", 3, "update", 3, 10 },
                    { 56, null, "Como salvar um novo objeto no banco com EF Core?", "Add() rastreia o objeto como 'Added'. SaveChangesAsync() gera o INSERT SQL e persiste no banco.", 18, "[\"ctx.Produtos.Add(p); await ctx.SaveChangesAsync();\",\"ctx.Produtos.Insert(p);\",\"await ctx.Produtos.SaveAsync(p);\",\"ctx.Save(p);\"]", 1, "ctx.Produtos.Add(p); await ctx.SaveChangesAsync();", 1, 10 },
                    { 57, null, "Qual método busca uma entidade pelo ID de forma otimizada (verifica o cache antes do banco)?", "FindAsync verifica primeiro o Change Tracker (cache em memória). Se não encontrar, vai ao banco. É a forma mais eficiente para busca por PK.", 18, "[\"FindAsync(id)\",\"FirstOrDefaultAsync(x => x.Id == id)\",\"GetByIdAsync(id)\",\"SingleAsync(id)\"]", 2, "FindAsync(id)", 1, 10 },
                    { 58, null, "O código abaixo tenta atualizar um produto mas não persiste:\n\nvar p = await ctx.Produtos.FindAsync(id);\np.Preco = 499.90m;\n// faltou algo aqui", "O EF rastreia a mudança automaticamente, mas só persiste quando SaveChangesAsync() é chamado. Sem ele, a alteração fica apenas em memória.", 18, "[\"await ctx.SaveChangesAsync()\",\"ctx.Produtos.Update(p)\",\"ctx.Commit()\",\"ctx.Produtos.Save()\"]", 3, "await ctx.SaveChangesAsync()", 5, 10 },
                    { 59, null, "Qual atributo identifica um controller como uma API REST no ASP.NET Core?", "[ApiController] habilita validação automática do modelo, binding de [FromBody] por padrão e respostas de erro padronizadas.", 19, "[\"[ApiController]\",\"[RestController]\",\"[HttpController]\",\"[RouteController]\"]", 1, "[ApiController]", 1, 10 },
                    { 60, null, "Para APIs REST, qual classe base é recomendada?", "ControllerBase não inclui suporte a Views (Razor), tornando-o mais leve e adequado para APIs.", 19, "[\"ControllerBase\",\"Controller\",\"ApiBase\",\"RestController\"]", 2, "ControllerBase", 1, 10 },
                    { 61, null, "Para mapear todos os controllers no pipeline, use:\n\napp.___();", "MapControllers registra as rotas de todos os controllers no pipeline de requisições do ASP.NET Core.", 19, "[\"MapControllers\",\"UseControllers\",\"AddControllers\",\"RegisterControllers\"]", 3, "MapControllers", 3, 10 },
                    { 62, null, "Qual atributo mapeia um método para requisições GET com parâmetro na rota?", "[HttpGet(\"{id}\")] define que o método responde a GET /api/controller/5, onde 5 é vinculado ao parâmetro id.", 20, "[\"[HttpGet(\\\"{id}\\\")]\",\"[Get(\\\"{id}\\\")]\",\"[Route(\\\"GET/{id}\\\")]\",\"[HttpParam(\\\"{id}\\\")]\"]", 1, "[HttpGet(\"{id}\")]", 1, 10 },
                    { 63, null, "Qual atributo indica que um parâmetro vem do corpo JSON da requisição?", "[FromBody] instrui o ASP.NET a desserializar o JSON do corpo da requisição para o parâmetro. Com [ApiController], é inferido automaticamente para tipos complexos.", 20, "[\"[FromBody]\",\"[FromJson]\",\"[FromRequest]\",\"[JsonParam]\"]", 2, "[FromBody]", 1, 10 },
                    { 64, null, "Para retornar 201 com o cabeçalho Location no POST:\n\nreturn ___(nameof(ObterPorId), new { id = produto.Id }, produto);", "CreatedAtAction retorna HTTP 201 e inclui o header Location com a URL do recurso criado, seguindo o padrão REST.", 20, "[\"CreatedAtAction\",\"Created\",\"OkCreated\",\"PostResult\"]", 3, "CreatedAtAction", 3, 10 },
                    { 65, null, "Qual status code retornar quando um recurso não é encontrado?", "404 indica que o recurso solicitado não existe no servidor. Use NotFound() no ControllerBase.", 21, "[\"404 NotFound\",\"400 BadRequest\",\"204 NoContent\",\"500 InternalServerError\"]", 1, "404 NotFound", 1, 10 },
                    { 66, null, "Qual é o status code correto para um DELETE bem-sucedido sem corpo de resposta?", "204 indica sucesso sem corpo de resposta. É o padrão para DELETE e PUT quando não há dados a retornar.", 21, "[\"204 NoContent\",\"200 Ok\",\"201 Created\",\"202 Accepted\"]", 2, "204 NoContent", 1, 10 },
                    { 67, null, "O status 400 BadRequest deve ser retornado quando:", "400 é o erro do cliente — dados malformados, campos obrigatórios ausentes ou validações negadas. 404 = não encontrado, 500 = erro do servidor, 401 = não autenticado.", 21, "[\"Os dados enviados pelo cliente são inválidos\",\"O recurso não foi encontrado\",\"O servidor encontrou um erro interno\",\"O cliente não está autenticado\"]", 3, "Os dados enviados pelo cliente são inválidos", 1, 10 },
                    { 68, null, "Qual atributo marca uma propriedade como obrigatória na validação?", "[Required] indica que o campo não pode ser nulo nem vazio. Com [ApiController], o ASP.NET retorna 400 automaticamente se a validação falhar.", 22, "[\"[Required]\",\"[NotNull]\",\"[Mandatory]\",\"[NotEmpty]\"]", 1, "[Required]", 1, 10 },
                    { 69, null, "Para validar que um número está entre 1 e 100, use:", "[Range(min, max)] valida que o valor está dentro do intervalo especificado, inclusive nos extremos.", 22, "[\"[Range(1, 100)]\",\"[Between(1, 100)]\",\"[Min(1)][Max(100)]\",\"[Limit(1, 100)]\"]", 2, "[Range(1, 100)]", 1, 10 },
                    { 70, null, "Para limitar o tamanho de uma string entre 3 e 50 caracteres:\n\n[___(50, MinimumLength = 3)]", "[StringLength(max, MinimumLength = min)] valida tanto o máximo quanto o mínimo de caracteres em uma string.", 22, "[\"StringLength\",\"MaxLength\",\"LengthRange\",\"TextSize\"]", 3, "StringLength", 3, 10 },
                    { 71, null, "Por que não expor a entidade de domínio diretamente na API?", "Expor a entidade diretamente revela campos como SenhaHash, audit fields e relacionamentos internos. DTOs definem um contrato explícito e estável para a API.", 23, "[\"Vaza dados internos e cria acoplamento entre camadas\",\"Entidades não podem ser serializadas em JSON\",\"O EF Core não permite isso\",\"DTOs são mais rápidos de serializar\"]", 1, "Vaza dados internos e cria acoplamento entre camadas", 1, 10 },
                    { 72, null, "O que é um DTO de Request vs um DTO de Response?", "Request DTOs definem o que o cliente envia (POST/PUT body). Response DTOs definem o que a API retorna. Mantê-los separados permite evoluir cada um independentemente.", 23, "[\"Request = entrada do cliente; Response = saída da API\",\"Request = saída da API; Response = entrada do cliente\",\"São a mesma coisa com nomes diferentes\",\"Request é usado no GET, Response no POST\"]", 2, "Request = entrada do cliente; Response = saída da API", 1, 10 },
                    { 73, null, "O código abaixo vaza a senha dos usuários:\n\n[HttpGet]\npublic async Task<IActionResult> Listar()\n    => Ok(await ctx.Usuarios.ToListAsync());", "Projetar com .Select() em um DTO de Response garante que somente os campos desejados sejam retornados, eliminando dados sensíveis.", 23, "[\"Usar .Select() para projetar em um DTO sem SenhaHash\",\"Adicionar [JsonIgnore] na entidade\",\"Usar .AsNoTracking()\",\"Retornar NoContent()\"]", 3, "Usar .Select() para projetar em um DTO sem SenhaHash", 5, 10 },
                    { 74, null, "Qual classe deve ser herdada para criar um validator com FluentValidation?", "AbstractValidator<T> é a classe base do FluentValidation. As regras são definidas no construtor usando RuleFor().", 24, "[\"AbstractValidator<T>\",\"BaseValidator<T>\",\"FluentValidator<T>\",\"ModelValidator<T>\"]", 1, "AbstractValidator<T>", 1, 10 },
                    { 75, null, "Para validar que o campo Nome não é vazio com FluentValidation:\n\nRuleFor(x => x.Nome).___()", "NotEmpty() verifica que a string não é nula, vazia ou só espaços em branco. Equivale ao [Required] das Data Annotations, mas permite encadeamento.", 24, "[\"NotEmpty\",\"Required\",\"NotNull\",\"IsRequired\"]", 2, "NotEmpty", 3, 10 },
                    { 76, null, "Quando preferir FluentValidation em vez de Data Annotations?", "Use Annotations para regras simples e diretas. FluentValidation brilha em validações condicionais (When), mensagens dinâmicas e validators reutilizáveis entre classes.", 24, "[\"Quando precisar de lógica condicional ou reuso entre validators\",\"Quando o modelo tiver menos de 3 campos\",\"Quando a entidade for usada com EF Core\",\"FluentValidation sempre substitui Data Annotations\"]", 3, "Quando precisar de lógica condicional ou reuso entre validators", 1, 10 },
                    { 77, null, "Quais são as 3 partes de um JWT?", "Header contém o algoritmo, Payload contém as claims (dados) e Signature garante a integridade do token.", 25, "[\"Header, Payload, Signature\",\"Header, Body, Footer\",\"Token, Claims, Hash\",\"Key, Value, Signature\"]", 1, "Header, Payload, Signature", 1, 12 },
                    { 78, null, "Por que JWT é considerado stateless?", "Com JWT, o servidor valida a assinatura e lê as claims diretamente do token. Nenhum estado é armazenado server-side, facilitando escalabilidade horizontal.", 25, "[\"O servidor não precisa armazenar sessões — toda informação está no token\",\"O token nunca expira\",\"Não usa banco de dados\",\"O cliente não armazena o token\"]", 2, "O servidor não precisa armazenar sessões — toda informação está no token", 1, 12 },
                    { 79, null, "É seguro armazenar senhas no payload de um JWT?", "A assinatura do JWT garante que o token não foi adulterado, mas não cifra o conteúdo. Qualquer pessoa com o token pode decodificar o payload.", 25, "[\"Não — o payload é apenas Base64 e pode ser decodificado\",\"Sim — a assinatura criptografa o conteúdo\",\"Sim — desde que use HTTPS\",\"Depende do algoritmo usado\"]", 3, "Não — o payload é apenas Base64 e pode ser decodificado", 1, 12 },
                    { 80, null, "Qual a ordem correta dos middlewares de auth no pipeline?", "UseAuthentication() identifica o usuário a partir do token. UseAuthorization() verifica as permissões. A autenticação deve ocorrer primeiro.", 26, "[\"UseAuthentication() antes de UseAuthorization()\",\"UseAuthorization() antes de UseAuthentication()\",\"A ordem não importa\",\"Apenas UseAuthorization() é necessário\"]", 1, "UseAuthentication() antes de UseAuthorization()", 1, 12 },
                    { 81, null, "Para proteger uma rota exigindo autenticação, use o atributo:\n\n[___]", "[Authorize] indica que somente usuários autenticados podem acessar o endpoint. Retorna 401 para não autenticados e 403 para autenticados sem permissão.", 26, "[\"Authorize\",\"Protected\",\"RequireAuth\",\"Authenticated\"]", 2, "Authorize", 3, 12 },
                    { 82, null, "Qual classe é usada para criar as credenciais de assinatura do JWT?", "SigningCredentials combina a chave secreta com o algoritmo (ex: HmacSha256) para assinar o token.", 26, "[\"SigningCredentials\",\"JwtCredentials\",\"TokenCredentials\",\"SecurityCredentials\"]", 3, "SigningCredentials", 1, 12 },
                    { 83, null, "Para restringir um endpoint apenas a usuários com role 'Admin':", "[Authorize(Roles = \"Admin\")] verifica se o ClaimsPrincipal possui a claim de role 'Admin'. Retorna 403 Forbidden se não tiver.", 27, "[\"[Authorize(Roles = \\\"Admin\\\")]\",\"[RequireRole(\\\"Admin\\\")]\",\"[AdminOnly]\",\"[Authorize][Role(\\\"Admin\\\")]\"]", 1, "[Authorize(Roles = \"Admin\")]", 1, 12 },
                    { 84, null, "Para tornar um endpoint público em um controller com [Authorize] global:\n\n[___]\n[HttpGet(\"publico\")]\npublic IActionResult Publico() => Ok();", "[AllowAnonymous] sobrescreve qualquer [Authorize] no controller ou policy global, permitindo acesso sem autenticação.", 27, "[\"AllowAnonymous\",\"Public\",\"NoAuth\",\"SkipAuthorization\"]", 2, "AllowAnonymous", 3, 12 },
                    { 85, null, "Como acessar o ID do usuário autenticado dentro de uma action?", "A propriedade User do ControllerBase é um ClaimsPrincipal. FindFirstValue busca o valor da claim NameIdentifier, que por convenção armazena o ID do usuário.", 27, "[\"User.FindFirstValue(ClaimTypes.NameIdentifier)\",\"Request.Headers[\\\"UserId\\\"]\",\"HttpContext.User.Id\",\"this.UserId\"]", 3, "User.FindFirstValue(ClaimTypes.NameIdentifier)", 1, 12 },
                    { 86, null, "Qual a vantagem de centralizar o tratamento de erros?", "Com tratamento centralizado, todos os erros não tratados recebem a mesma resposta padronizada sem precisar de try/catch em cada action.", 28, "[\"Elimina código duplicado e garante respostas consistentes\",\"Melhora a performance da API\",\"Permite usar menos controllers\",\"É obrigatório pelo ASP.NET\"]", 1, "Elimina código duplicado e garante respostas consistentes", 1, 12 },
                    { 87, null, "Por que não expor stack traces em produção?", "Stack traces expõem caminhos de arquivo, nomes de classes, versões de bibliotecas e lógica interna — informações valiosas para ataques.", 28, "[\"Revela detalhes internos que podem ser explorados por atacantes\",\"Stack traces aumentam o tamanho da resposta\",\"O cliente não consegue interpretar\",\"O ASP.NET bloqueia automaticamente\"]", 2, "Revela detalhes internos que podem ser explorados por atacantes", 1, 12 },
                    { 88, null, "Para verificar se a aplicação está em ambiente de desenvolvimento:\n\nif (app.Environment.___()){...}", "IsDevelopment() retorna true quando ASPNETCORE_ENVIRONMENT é 'Development'. Use para habilitar detalhes de erro e outros recursos apenas no dev.", 28, "[\"IsDevelopment\",\"IsDebug\",\"IsDev\",\"IsLocal\"]", 3, "IsDevelopment", 3, 12 },
                    { 89, null, "O que o parâmetro 'next' representa em um middleware?", "Chamar 'await next(context)' passa o controle para o próximo middleware. Não chamar 'next' encerra o pipeline e retorna a resposta atual.", 29, "[\"O próximo middleware no pipeline\",\"A próxima requisição HTTP\",\"O controller a ser executado\",\"O retorno da action\"]", 1, "O próximo middleware no pipeline", 1, 12 },
                    { 90, null, "Qual interface implementar para criar um middleware tipado no .NET?", "IMiddleware define o contrato com um único método InvokeAsync(HttpContext, RequestDelegate). É a forma recomendada para middlewares com dependências injetadas.", 29, "[\"IMiddleware\",\"IRequestHandler\",\"IHttpMiddleware\",\"IPipelineStep\"]", 2, "IMiddleware", 1, 12 },
                    { 91, null, "A ordem de registro dos middlewares importa?", "O pipeline é uma cadeia sequencial. UseAuthentication() antes de UseAuthorization() é obrigatório, por exemplo. A ordem errada pode causar comportamentos inesperados.", 29, "[\"Sim — são executados na ordem em que são registrados\",\"Não — o ASP.NET otimiza a ordem\",\"Apenas para middlewares de autenticação\",\"Apenas em produção\"]", 3, "Sim — são executados na ordem em que são registrados", 1, 12 },
                    { 92, null, "Qual nível de log usar para erros que causaram falha em uma operação?", "LogError é para erros que causaram falha na operação atual mas não derrubaram a aplicação. LogCritical é para falhas graves que podem parar o sistema.", 30, "[\"LogError\",\"LogWarning\",\"LogCritical\",\"LogDebug\"]", 1, "LogError", 1, 12 },
                    { 93, null, "Por que preferir logging estruturado em vez de interpolação de string?", "Com logging estruturado, os parâmetros são indexados como campos separados. Você pode buscar 'UserId = 42' em vez de fazer full-text search em strings.", 30, "[\"Permite busca e análise nos sistemas de log como Seq ou Elasticsearch\",\"É mais rápido de escrever\",\"Consome menos memória\",\"É obrigatório pelo ILogger\"]", 2, "Permite busca e análise nos sistemas de log como Seq ou Elasticsearch", 1, 12 },
                    { 94, null, "Para injetar o logger no controller:\n\npublic ProdutosController(___<ProdutosController> logger) => _logger = logger;", "ILogger<T> é a interface de logging do .NET. O parâmetro de tipo T define a categoria do log (geralmente o nome da classe que está logando).", 30, "[\"ILogger\",\"Logger\",\"ILog\",\"LogService\"]", 3, "ILogger", 3, 12 },
                    { 95, null, "Qual atributo marca um método como teste unitário no xUnit?", "[Fact] é o atributo do xUnit para testes sem parâmetros. NUnit usa [Test] e MSTest usa [TestMethod], mas xUnit usa [Fact].", 31, "[\"[Fact]\",\"[Test]\",\"[TestMethod]\",\"[UnitTest]\"]", 1, "[Fact]", 1, 12 },
                    { 96, null, "O que representa a fase 'Arrange' no padrão AAA?", "Arrange = preparar (instâncias, mocks, dados). Act = executar (chamar o método). Assert = verificar (o resultado é o esperado).", 31, "[\"Prepara os dados e dependências para o teste\",\"Executa o código sendo testado\",\"Verifica o resultado esperado\",\"Limpa os recursos após o teste\"]", 2, "Prepara os dados e dependências para o teste", 1, 12 },
                    { 97, null, "Qual atributo usar para um teste com múltiplos conjuntos de dados?", "[Theory] indica que o teste aceita parâmetros. [InlineData] fornece os valores. O xUnit executa o teste uma vez para cada [InlineData].", 31, "[\"[Theory] com [InlineData]\",\"[Fact] com [InlineData]\",\"[ParameterizedTest]\",\"[DataDriven]\"]", 3, "[Theory] com [InlineData]", 1, 12 },
                    { 98, null, "Por que usar mocks em testes unitários?", "Mocks substituem banco, email, APIs externas por objetos controlados. Isso garante que o teste falha só por causa do código testado, não de dependências.", 32, "[\"Para isolar a unidade testada de dependências externas\",\"Para tornar os testes mais rápidos de escrever\",\"Porque o banco de dados não funciona em testes\",\"Para testar a integração entre componentes\"]", 1, "Para isolar a unidade testada de dependências externas", 1, 12 },
                    { 99, null, "Para configurar um mock para retornar um valor async:\n\nmockRepo.___(r => r.ObterAsync(1)).___(new Produto());", "Setup() configura qual método interceptar. ReturnsAsync() define o valor retornado em operações async. Para síncronos, use Returns().", 32, "[\"Setup / ReturnsAsync\",\"Configure / Returns\",\"Mock / ReturnAsync\",\"When / ThenReturn\"]", 2, "Setup / ReturnsAsync", 3, 12 },
                    { 100, null, "Como verificar que um método do mock foi chamado exatamente uma vez?", "Verify() é usado após o Act para confirmar que determinada interação ocorreu. Times.Once(), Times.Never(), Times.Exactly(n) controlam a contagem esperada.", 32, "[\"mock.Verify(x => x.Metodo(), Times.Once())\",\"mock.Assert(x => x.Metodo(), 1)\",\"mock.Check(x => x.Metodo())\",\"Assert.Called(mock, 1)\"]", 3, "mock.Verify(x => x.Metodo(), Times.Once())", 1, 12 },
                    { 101, null, "Qual a ordem correta do ciclo TDD?", "Red = teste falha (código não existe). Green = mínimo para passar. Refactor = melhora sem quebrar. Repetir para cada nova funcionalidade.", 33, "[\"Red → Green → Refactor\",\"Green → Red → Refactor\",\"Refactor → Red → Green\",\"Green → Refactor → Red\"]", 1, "Red → Green → Refactor", 1, 12 },
                    { 102, null, "Qual o principal benefício de TDD além dos testes em si?", "Ao escrever o teste primeiro, você define como quer usar o código. Isso naturalmente leva a APIs mais simples, classes menores e melhor separação de responsabilidades.", 33, "[\"Força pensar na interface antes da implementação, melhorando o design\",\"Elimina a necessidade de documentação\",\"Garante zero bugs no código\",\"Dobra a velocidade de desenvolvimento\"]", 2, "Força pensar na interface antes da implementação, melhorando o design", 1, 12 },
                    { 103, null, "Na fase Green do TDD, qual deve ser o objetivo?", "Na fase Green, qualidade não importa — só fazer o teste passar. A refatoração vem depois, protegida pelos testes. Isso evita over-engineering prematuro.", 33, "[\"Escrever o mínimo de código para o teste passar\",\"Escrever o código mais elegante possível\",\"Refatorar o código existente\",\"Adicionar mais casos de teste\"]", 3, "Escrever o mínimo de código para o teste passar", 1, 12 },
                    { 104, null, "O que é Injeção de Dependência?", "Com DI, a classe declara o que precisa (interfaces) e o contêiner fornece as implementações. Isso desacopla e torna o código testável.", 34, "[\"Um padrão onde as dependências são recebidas de fora, não criadas internamente\",\"Um método para injetar código em tempo de execução\",\"Uma forma de herança avançada\",\"Um padrão para criar singletons\"]", 1, "Um padrão onde as dependências são recebidas de fora, não criadas internamente", 1, 12 },
                    { 105, null, "Qual o principal benefício de depender de interfaces em vez de classes concretas?", "Dependendo de IProdutoRepository em vez de ProdutoRepository, você pode injetar um mock em testes sem tocar no banco, e trocar para PostgreSqlRepository sem mudar o service.", 34, "[\"Permite trocar implementações e facilita testes com mocks\",\"Interfaces são mais rápidas que classes\",\"Reduz o consumo de memória\",\"É obrigatório pelo compilador\"]", 2, "Permite trocar implementações e facilita testes com mocks", 1, 12 },
                    { 106, null, "O código abaixo cria a dependência internamente (acoplamento):\n\npublic class PedidoService\n{\n    private readonly EmailService _email = new EmailService();\n}", "Instanciar com 'new' cria acoplamento forte. Receber IEmailService pelo construtor inverte o controle — quem cria é o contêiner, não a classe.", 34, "[\"Receber IEmailService pelo construtor em vez de instanciar\",\"Usar 'static' no EmailService\",\"Remover o campo _email\",\"Usar 'new' com interface\"]", 3, "Receber IEmailService pelo construtor em vez de instanciar", 5, 12 },
                    { 107, null, "Como registrar IProdutoRepository com implementação ProdutoRepository?", "AddScoped registra a interface com sua implementação com lifetime Scoped. O primeiro tipo genérico é a interface, o segundo é a implementação concreta.", 35, "[\"builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>()\",\"builder.Services.Register<IProdutoRepository>(new ProdutoRepository())\",\"services.Inject<IProdutoRepository, ProdutoRepository>()\",\"builder.AddRepository<IProdutoRepository>()\"]", 1, "builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>()", 1, 12 },
                    { 108, null, "Onde os serviços devem ser registrados no ASP.NET Core?", "O registro acontece na fase de configuração, antes de builder.Build(). Após o Build(), o contêiner é construído e não aceita mais registros.", 35, "[\"Em Program.cs, antes de builder.Build()\",\"Em cada controller que os usa\",\"No construtor do DbContext\",\"Em appsettings.json\"]", 2, "Em Program.cs, antes de builder.Build()", 1, 12 },
                    { 109, null, "Para obter um serviço registrado dentro de um método (fora do construtor):\n\nvar svc = scope.ServiceProvider.___<IEmailService>();", "GetRequiredService<T>() lança InvalidOperationException se o serviço não estiver registrado. Use GetService<T>() se o serviço for opcional (retorna null).", 35, "[\"GetRequiredService\",\"Resolve\",\"Get\",\"Inject\"]", 3, "GetRequiredService", 3, 12 },
                    { 110, null, "Qual lifetime usar para o DbContext do Entity Framework?", "DbContext deve ser Scoped — uma instância por requisição. Singleton causaria problemas de concorrência e Transient causaria múltiplos change trackers na mesma requisição.", 36, "[\"Scoped\",\"Singleton\",\"Transient\",\"Qualquer um\"]", 1, "Scoped", 1, 12 },
                    { 111, null, "O que é 'Captive Dependency'?", "Se um Singleton recebe um Scoped no construtor, ele captura essa instância para sempre — o Scoped passa a viver enquanto o Singleton viver, quebrando seu lifecycle.", 36, "[\"Singleton capturando um Scoped/Transient, fazendo o serviço viver mais do que deveria\",\"Uma dependência circular entre dois serviços\",\"Um serviço que não consegue ser resolvido\",\"Uma dependência registrada mais de uma vez\"]", 2, "Singleton capturando um Scoped/Transient, fazendo o serviço viver mais do que deveria", 1, 12 },
                    { 112, null, "Qual lifetime usar para um serviço de cache em memória que deve persistir entre requisições?", "Cache em memória deve ser Singleton — a mesma instância para toda a aplicação. Scoped criaria um cache novo por requisição, perdendo o valor entre calls.", 36, "[\"Singleton\",\"Scoped\",\"Transient\",\"Static\"]", 3, "Singleton", 1, 12 },
                    { 113, null, "Na Clean Architecture, qual camada pode depender de frameworks externos como EF Core?", "A camada de Infrastructure implementa interfaces do Domain usando frameworks concretos (EF Core, SMTP, etc.). O Domain e Application permanecem puros.", 37, "[\"Infrastructure\",\"Domain\",\"Application\",\"Use Cases\"]", 1, "Infrastructure", 1, 15 },
                    { 114, null, "O que garante a 'Regra da Dependência' na Clean Architecture?", "Infrastructure conhece Domain, mas Domain não conhece Infrastructure. Isso garante que o núcleo do negócio é independente de frameworks e pode ser testado isoladamente.", 37, "[\"Dependências de código sempre apontam para dentro (em direção ao domínio)\",\"Cada camada tem exatamente 3 classes\",\"Controllers não podem ter lógica\",\"Interfaces só existem na camada de Application\"]", 2, "Dependências de código sempre apontam para dentro (em direção ao domínio)", 1, 15 },
                    { 115, null, "Por que o Domain não deve ter dependências de frameworks externos?", "Um Domain puro pode ser testado com simples testes unitários sem banco, sem HTTP e sem configuração. Também pode trocar de ORM ou framework sem alterar a lógica de negócio.", 37, "[\"Para poder ser testado e evoluído independentemente de infraestrutura\",\"Porque frameworks são muito pesados\",\"O compilador não permite\",\"Para reduzir o número de arquivos\"]", 3, "Para poder ser testado e evoluído independentemente de infraestrutura", 1, 15 },
                    { 116, null, "O que é um Use Case na Clean Architecture?", "Use Cases (ou Interactors) representam uma ação do sistema: ConfirmarPedido, CriarUsuario, GerarRelatorio. Cada Use Case tem uma única responsabilidade.", 38, "[\"Uma classe que orquestra uma ação específica do sistema\",\"Um método em um controller\",\"Uma entidade de domínio\",\"Um repositório\"]", 1, "Uma classe que orquestra uma ação específica do sistema", 1, 15 },
                    { 117, null, "Onde devem ficar as interfaces de repositório (ex: IPedidoRepository)?", "Interfaces no Domain aplicam o Dependency Inversion: Infrastructure depende do Domain para implementar, não o contrário. O Domain define o contrato, Infrastructure cumpre.", 38, "[\"Domain — o centro da arquitetura\",\"Infrastructure — junto com a implementação\",\"Application — junto com os Use Cases\",\"API — junto com os Controllers\"]", 2, "Domain — o centro da arquitetura", 1, 15 },
                    { 118, null, "A entity abaixo tem lógica de negócio vazando para fora:\n\npublic class Pedido\n{\n    public StatusPedido Status { get; set; }  // set público!\n}", "Entities devem proteger seus invariantes com setters privados e métodos de domínio. Status = Confirmado diretamente viola o encapsulamento e permite estados inválidos.", 38, "[\"Tornar o set privado e criar método Confirmar() com validação\",\"Remover o Status\",\"Usar [Required] no Status\",\"Mover o Status para um DTO\"]", 3, "Tornar o set privado e criar método Confirmar() com validação", 5, 15 },
                    { 119, null, "Em uma solução Clean Architecture, qual projeto deve referenciar todos os outros?", "A API conhece todos os projetos somente para registrar as dependências no contêiner. Mas essa dependência é apenas de configuração — a lógica flui pelo Application até o Domain.", 39, "[\"API/Presentation — apenas para compor o DI\",\"Domain — por ser o centro\",\"Infrastructure — por ter as implementações\",\"Application — por ter os Use Cases\"]", 1, "API/Presentation — apenas para compor o DI", 1, 15 },
                    { 120, null, "Qual o papel do Controller numa Clean Architecture bem aplicada?", "Controllers são adaptadores: traduzem HTTP para chamadas da Application. A lógica fica nos Use Cases. Um controller gordo é sinal de violação da Clean Architecture.", 39, "[\"Receber a requisição e delegar para o Use Case — zero lógica de negócio\",\"Conter toda a lógica da feature\",\"Acessar o banco diretamente\",\"Instanciar entidades de domínio\"]", 2, "Receber a requisição e delegar para o Use Case — zero lógica de negócio", 1, 15 },
                    { 121, null, "Qual a diferença principal entre Clean Architecture e arquitetura em camadas tradicional?", "Na arquitetura tradicional, o domínio depende da infraestrutura (Domain → Infrastructure). Na Clean, é o contrário: Infrastructure implementa interfaces do Domain, protegendo o núcleo.", 39, "[\"As interfaces ficam no Domain (centro), invertendo a dependência em vez de apontar para Infrastructure\",\"Clean Architecture tem mais camadas\",\"Clean Architecture não usa banco de dados\",\"Não há diferença — são a mesma coisa\"]", 3, "As interfaces ficam no Domain (centro), invertendo a dependência em vez de apontar para Infrastructure", 1, 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Exercicios",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Licoes",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Modulos",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
