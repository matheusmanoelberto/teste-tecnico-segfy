using System.Net;
using SegfyInsurance.Domain.Exceptions;

namespace SegfyInsurance.Api.Middlewares;

public class MiddlewareExcecao(RequestDelegate proximo)
{
    public async Task InvokeAsync(HttpContext contexto)
    {
        try
        {
            await proximo(contexto);
        }
        catch (ExcecaoDominio excecao)
        {
            contexto.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            contexto.Response.ContentType = "application/json";
            await contexto.Response.WriteAsJsonAsync(new { erro = excecao.Message });
        }
    }
}
