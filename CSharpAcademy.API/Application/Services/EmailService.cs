using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CSharpAcademy.API.Application.Services;

public interface IEmailService
{
    Task EnviarResetSenhaAsync(string destinatario, string nome, string linkReset);
}

public class EmailService(IHttpClientFactory httpClientFactory, IConfiguration config) : IEmailService
{
    public async Task EnviarResetSenhaAsync(string destinatario, string nome, string linkReset)
    {
        var client = httpClientFactory.CreateClient("resend");
        var remetente = config["Email:Remetente"] ?? "DevAcademy <onboarding@resend.dev>";

        var body = new
        {
            from = remetente,
            to = new[] { destinatario },
            subject = "Redefinição de senha — DevAcademy",
            html = $"""
                <div style="font-family:sans-serif;max-width:480px;margin:0 auto;padding:32px">
                  <h2 style="color:#4ec9b0">DevAcademy</h2>
                  <p>Olá, <strong>{nome}</strong>!</p>
                  <p>Recebemos uma solicitação para redefinir sua senha.</p>
                  <p style="margin:24px 0">
                    <a href="{linkReset}"
                       style="background:#4ec9b0;color:#1e1e1e;padding:12px 24px;border-radius:6px;text-decoration:none;font-weight:bold">
                      Redefinir senha
                    </a>
                  </p>
                  <p style="color:#888;font-size:13px">
                    Este link expira em <strong>1 hora</strong>.<br>
                    Se você não solicitou isso, ignore este e-mail.
                  </p>
                </div>
                """
        };

        var json = JsonSerializer.Serialize(body);
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.resend.com/emails")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var apiKey = config["Email:ResendApiKey"]
            ?? throw new InvalidOperationException("Email:ResendApiKey não configurada.");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}
