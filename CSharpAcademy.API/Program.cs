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
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

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

// ── Rate Limiting — protege endpoints de IA (10 req/min por usuário) ──────────
builder.Services.AddRateLimiter(opt =>
{
    opt.AddPolicy("ai", context => RateLimitPartition.GetFixedWindowLimiter(
        partitionKey: context.User.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anon",
        factory: _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10,
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

// ── CORS ──────────────────────────────────────────────────────────────────────
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? ["http://localhost:4200"];

builder.Services.AddCors(opt => opt.AddPolicy("Default", p =>
    p.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// ── Auto-migrate ──────────────────────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// ── Pipeline (ordem importa) ──────────────────────────────────────────────────
app.UseMiddleware<ExceptionMiddleware>();   // 1. Captura exceções globais
app.UseCors("Default");                    // 2. CORS antes de auth
app.UseRateLimiter();                      // 3. Rate limiting
app.MapOpenApi();                          // /openapi/v1.json
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");            // GET /health → { "status": "Healthy" }

app.Run();
