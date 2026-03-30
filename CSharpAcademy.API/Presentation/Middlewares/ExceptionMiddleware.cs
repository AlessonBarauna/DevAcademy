namespace CSharpAcademy.API.Presentation.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception on {Method} {Path}", context.Request.Method, context.Request.Path);
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = env.IsDevelopment()
            ? new { sucesso = false, mensagem = ex.Message, detalhe = ex.StackTrace }
            : new { sucesso = false, mensagem = "Ocorreu um erro interno. Tente novamente mais tarde.", detalhe = (string?)null };

        return context.Response.WriteAsJsonAsync(response);
    }
}
