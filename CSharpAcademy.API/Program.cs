using System.Text;
using System.Threading.RateLimiting;
using CSharpAcademy.API.Application.Services;
using CSharpAcademy.API.Application.Services.AI;
using CSharpAcademy.API.Infrastructure.Data;
using CSharpAcademy.API.Infrastructure.Repositories;
using CSharpAcademy.API.Presentation.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ── Database ──────────────────────────────────────────────────────────────────
// Em produção: DATABASE_URL vem como URI postgresql://user:pass@host:port/db
// Em desenvolvimento: usa a connection string do appsettings.json (key=value)
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
string connectionString;

if (!string.IsNullOrEmpty(databaseUrl) && databaseUrl.StartsWith("postgres"))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':', 2);
    var port = uri.Port > 0 ? uri.Port : 5432;
    connectionString = $"Host={uri.Host};Port={port};Database={uri.AbsolutePath.TrimStart('/')};" +
                       $"Username={userInfo[0]};Password={userInfo[1]};" +
                       $"SSL Mode=Require;Trust Server Certificate=true";
}
else
{
    connectionString = builder.Configuration.GetConnectionString("Default")
        ?? throw new InvalidOperationException(
            "Connection string não configurada. " +
            "Em produção, defina a variável de ambiente DATABASE_URL com a URI do PostgreSQL.");
}

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(connectionString));

// ── JWT Auth ──────────────────────────────────────────────────────────────────
var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

// ── Rate Limiting ─────────────────────────────────────────────────────────────
builder.Services.AddRateLimiter(opt =>
{
    // IA: 10 req/min por usuário
    opt.AddPolicy("ai", context => RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: context.User.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anon",
        factory: _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromMinutes(1),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        }));

    // Playground: 20 execuções/min por usuário (execução de código é cara)
    opt.AddPolicy("playground", context => RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: context.User.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anon",
        factory: _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 20,
            Window = TimeSpan.FromMinutes(1),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        }));

    opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// ── HttpClient para Groq ──────────────────────────────────────────────────────
builder.Services.AddHttpClient<IMistralService, MistralService>(client =>
{
    var apiKey = builder.Configuration["MistralAI:ApiKey"]!;
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    client.Timeout = TimeSpan.FromSeconds(60);
});

// ── Repositories — domínio ────────────────────────────────────────────────────
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IConquistaRepository, ConquistaRepository>();
builder.Services.AddScoped<ConquistaService>();
builder.Services.AddScoped<IModuloRepository, ModuloRepository>();
builder.Services.AddScoped<ILicaoRepository, LicaoRepository>();
builder.Services.AddScoped<IExercicioRepository, ExercicioRepository>();
builder.Services.AddScoped<IProgressoRepository, ProgressoRepository>();
builder.Services.AddScoped<IRespostaRepository, RespostaRepository>();
builder.Services.AddScoped<IMissaoRepository, MissaoRepository>();
builder.Services.AddScoped<INotaLicaoRepository, NotaLicaoRepository>();

// ── Repositories — IA ─────────────────────────────────────────────────────────
builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddScoped<IAssistantFAQRepository, AssistantFAQRepository>();
builder.Services.AddScoped<IAssistantFeedbackRepository, AssistantFeedbackRepository>();

// ── AI Services ───────────────────────────────────────────────────────────────
builder.Services.AddScoped<IPromptBuilder, PromptBuilder>();
builder.Services.AddScoped<IAssistantService, AssistantService>();

// ── Controllers ───────────────────────────────────────────────────────────────
builder.Services.AddControllers();

// ── OpenAPI ───────────────────────────────────────────────────────────────────
builder.Services.AddOpenApi();

// ── Health Checks ─────────────────────────────────────────────────────────────
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

// ── CORS (dev only — em produção o Angular é servido pelo próprio .NET) ───────
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? ["http://localhost:4200"];

builder.Services.AddCors(opt => opt.AddPolicy("Default", p =>
    p.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// ── Auto-create schema ────────────────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// ── Pipeline (ordem importa) ──────────────────────────────────────────────────
app.UseMiddleware<ExceptionMiddleware>();   // 1. Captura exceções globais
app.UseCors("Default");                    // 2. CORS (dev) antes de auth
app.UseRateLimiter();                      // 3. Rate limiting
app.MapOpenApi();                          // /openapi/v1.json
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");            // GET /health → { "status": "Healthy" }

// ── SPA: serve o Angular em produção ───────────────────���─────────────────────
app.UseDefaultFiles();   // index.html como padrão
app.UseStaticFiles();    // serve wwwroot/

// Fallback para rotas do Angular (ex: /dashboard, /modulos/1)
app.MapFallbackToFile("index.html");

app.Run();
