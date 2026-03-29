using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CSharpAcademy.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Ordem = table.Column<int>(type: "INTEGER", nullable: false),
                    NivelMinimo = table.Column<int>(type: "INTEGER", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    SenhaHash = table.Column<string>(type: "TEXT", nullable: false),
                    NivelAtual = table.Column<int>(type: "INTEGER", nullable: false),
                    XP = table.Column<int>(type: "INTEGER", nullable: false),
                    Idioma = table.Column<string>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Licoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModuloId = table.Column<int>(type: "INTEGER", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    ConteudoTeoricoMarkdown = table.Column<string>(type: "TEXT", nullable: false),
                    Ordem = table.Column<int>(type: "INTEGER", nullable: false),
                    XPRecompensa = table.Column<int>(type: "INTEGER", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licoes_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssistantFAQs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ModuloId = table.Column<int>(type: "INTEGER", nullable: false),
                    LicaoId = table.Column<int>(type: "INTEGER", nullable: true),
                    Pergunta = table.Column<string>(type: "TEXT", nullable: false),
                    Resposta = table.Column<string>(type: "TEXT", nullable: false),
                    Idioma = table.Column<string>(type: "TEXT", nullable: false),
                    NivelMinimo = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalUsos = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Ativa = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistantFAQs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssistantFAQs_Licoes_LicaoId",
                        column: x => x.LicaoId,
                        principalTable: "Licoes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AssistantFAQs_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    LicaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Titulo = table.Column<string>(type: "TEXT", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataFim = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Ativa = table.Column<bool>(type: "INTEGER", nullable: false),
                    TotalMensagens = table.Column<int>(type: "INTEGER", nullable: false),
                    MediaAvaliacoes = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatSessions_Licoes_LicaoId",
                        column: x => x.LicaoId,
                        principalTable: "Licoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatSessions_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    LicaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChatMessageId = table.Column<int>(type: "INTEGER", nullable: false),
                    Enunciado = table.Column<string>(type: "TEXT", nullable: false),
                    RespostaCorreta = table.Column<string>(type: "TEXT", nullable: false),
                    Explicacao = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Completado = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataGeracao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataCompletacao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomExercises_Licoes_LicaoId",
                        column: x => x.LicaoId,
                        principalTable: "Licoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomExercises_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercicios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LicaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Enunciado = table.Column<string>(type: "TEXT", nullable: false),
                    RespostaCorreta = table.Column<string>(type: "TEXT", nullable: false),
                    Explicacao = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    OpcoesJson = table.Column<string>(type: "TEXT", nullable: false),
                    Ordem = table.Column<int>(type: "INTEGER", nullable: false),
                    XPRecompensa = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercicios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercicios_Licoes_LicaoId",
                        column: x => x.LicaoId,
                        principalTable: "Licoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Progressos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    LicaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Completada = table.Column<bool>(type: "INTEGER", nullable: false),
                    XPGanho = table.Column<int>(type: "INTEGER", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progressos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progressos_Licoes_LicaoId",
                        column: x => x.LicaoId,
                        principalTable: "Licoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Progressos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChatSessionId = table.Column<int>(type: "INTEGER", nullable: true),
                    ModuloId = table.Column<int>(type: "INTEGER", nullable: true),
                    LicaoId = table.Column<int>(type: "INTEGER", nullable: true),
                    ExercicioId = table.Column<int>(type: "INTEGER", nullable: true),
                    PerguntaUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    RespostaAssistente = table.Column<string>(type: "TEXT", nullable: false),
                    IdiomaUsado = table.Column<string>(type: "TEXT", nullable: false),
                    NivelUsuarioNaMomentoPergunta = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AvaliacaoEstrelas = table.Column<int>(type: "INTEGER", nullable: true),
                    ComentarioFeedback = table.Column<string>(type: "TEXT", nullable: true),
                    TempoRespostaMs = table.Column<int>(type: "INTEGER", nullable: false),
                    UsouFAQCache = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatSessions_ChatSessionId",
                        column: x => x.ChatSessionId,
                        principalTable: "ChatSessions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMessages_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespostasUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExercicioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Resposta = table.Column<string>(type: "TEXT", nullable: false),
                    Correta = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataResposta = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostasUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespostasUsuarios_Exercicios_ExercicioId",
                        column: x => x.ExercicioId,
                        principalTable: "Exercicios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespostasUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssistantFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChatMessageId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Estrelas = table.Column<int>(type: "INTEGER", nullable: false),
                    Comentario = table.Column<string>(type: "TEXT", nullable: true),
                    DataAvaliacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RespostaAjudou = table.Column<bool>(type: "INTEGER", nullable: false),
                    RespostaClara = table.Column<bool>(type: "INTEGER", nullable: false),
                    RespostaCompleta = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistantFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssistantFeedbacks_ChatMessages_ChatMessageId",
                        column: x => x.ChatMessageId,
                        principalTable: "ChatMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssistantFeedbacks_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Modulos",
                columns: new[] { "Id", "Ativo", "Descricao", "NivelMinimo", "Ordem", "Titulo" },
                values: new object[,]
                {
                    { 1, true, "Variáveis, tipos, operadores e controle de fluxo", 1, 1, "Fundamentos C#" },
                    { 2, true, "Classes, herança, polimorfismo e encapsulamento", 1, 2, "Orientação a Objetos" },
                    { 3, true, "List, Dictionary, Array e consultas LINQ", 2, 3, "Coleções e LINQ" },
                    { 4, true, "Programação assíncrona, Tasks e threading", 2, 4, "Async/Await e Concorrência" },
                    { 5, true, "Repository, Factory, Singleton e outros padrões", 3, 5, "Design Patterns" }
                });

            migrationBuilder.InsertData(
                table: "Licoes",
                columns: new[] { "Id", "Ativo", "ConteudoTeoricoMarkdown", "Descricao", "ModuloId", "Ordem", "Titulo", "XPRecompensa" },
                values: new object[,]
                {
                    { 1, true, "## Variáveis em C#\n\nC# é uma linguagem fortemente tipada...", "int, string, bool, double e inferência de tipo", 1, 1, "Variáveis e Tipos", 10 },
                    { 2, true, "## Controle de Fluxo\n\nDecisões e repetições em C#...", "if/else, switch, loops for e foreach", 1, 2, "Controle de Fluxo", 10 },
                    { 3, true, "## Métodos\n\nMétodos encapsulam comportamento...", "Declaração, parâmetros, retorno e sobrecarga", 1, 3, "Métodos e Funções", 15 },
                    { 4, true, "## Classes em C#\n\nClasses são moldes para objetos...", "Propriedades, construtores e instanciação", 2, 1, "Classes e Objetos", 15 },
                    { 5, true, "## Herança\n\nReutilização de código através de herança...", "Herança, override e interfaces", 2, 2, "Herança e Polimorfismo", 20 },
                    { 6, true, "## Coleções\n\nListas são coleções dinâmicas...", "List<T>, Array e operações comuns", 3, 1, "Listas e Arrays", 20 },
                    { 7, true, "## LINQ\n\nLanguage Integrated Query permite consultas...", "Where, Select, OrderBy e First", 3, 2, "LINQ Básico", 25 },
                    { 8, true, "## Programação Assíncrona\n\nasync/await simplifica código assíncrono...", "Tarefas assíncronas com Task e async/await", 4, 1, "async/await", 30 },
                    { 9, true, "## Repository Pattern\n\nO padrão Repository isola a lógica de acesso a dados...", "Abstraindo o acesso a dados com Repository", 5, 1, "Repository Pattern", 35 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssistantFAQs_LicaoId",
                table: "AssistantFAQs",
                column: "LicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssistantFAQs_ModuloId",
                table: "AssistantFAQs",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_AssistantFeedbacks_ChatMessageId",
                table: "AssistantFeedbacks",
                column: "ChatMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_AssistantFeedbacks_UsuarioId",
                table: "AssistantFeedbacks",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatSessionId",
                table: "ChatMessages",
                column: "ChatSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_UsuarioId",
                table: "ChatMessages",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSessions_LicaoId",
                table: "ChatSessions",
                column: "LicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSessions_UsuarioId",
                table: "ChatSessions",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomExercises_LicaoId",
                table: "CustomExercises",
                column: "LicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomExercises_UsuarioId",
                table: "CustomExercises",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercicios_LicaoId",
                table: "Exercicios",
                column: "LicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Licoes_ModuloId",
                table: "Licoes",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Progressos_LicaoId",
                table: "Progressos",
                column: "LicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Progressos_UsuarioId_LicaoId",
                table: "Progressos",
                columns: new[] { "UsuarioId", "LicaoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RespostasUsuarios_ExercicioId",
                table: "RespostasUsuarios",
                column: "ExercicioId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostasUsuarios_UsuarioId",
                table: "RespostasUsuarios",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssistantFAQs");

            migrationBuilder.DropTable(
                name: "AssistantFeedbacks");

            migrationBuilder.DropTable(
                name: "CustomExercises");

            migrationBuilder.DropTable(
                name: "Progressos");

            migrationBuilder.DropTable(
                name: "RespostasUsuarios");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "Exercicios");

            migrationBuilder.DropTable(
                name: "ChatSessions");

            migrationBuilder.DropTable(
                name: "Licoes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Modulos");
        }
    }
}
