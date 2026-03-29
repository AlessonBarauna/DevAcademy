using System.Text;
using CSharpAcademy.API.Application.Services.AI;
using CSharpAcademy.API.Infrastructure.Data;
using CSharpAcademy.API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// JWT Auth
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

// HttpClient for Groq/Mistral
builder.Services.AddHttpClient<IMistralService, MistralService>(client =>
{
    var apiKey = builder.Configuration["MistralAI:ApiKey"]!;
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
    client.Timeout = TimeSpan.FromSeconds(60);
});

// Repositories
builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddScoped<IAssistantFAQRepository, AssistantFAQRepository>();
builder.Services.AddScoped<IAssistantFeedbackRepository, AssistantFeedbackRepository>();

// AI Services
builder.Services.AddScoped<IPromptBuilder, PromptBuilder>();
builder.Services.AddScoped<IAssistantService, AssistantService>();

// Controllers
builder.Services.AddControllers();

// OpenAPI (built-in .NET 10)
builder.Services.AddOpenApi();

// CORS (Angular dev)
builder.Services.AddCors(opt => opt.AddPolicy("Dev", p =>
    p.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Auto-migrate
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseCors("Dev");
app.MapOpenApi(); // /openapi/v1.json
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
