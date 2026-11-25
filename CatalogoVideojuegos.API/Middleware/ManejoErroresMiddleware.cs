using CatalogoVideojuegos.API.Exceptions;

namespace CatalogoVideojuegos.API.Middleware;

/// <summary>
/// Captura las excepciones de la aplicación y devuelve una respuesta JSON uniforme.
/// </summary>
public class ManejoErroresMiddleware
{
    private readonly RequestDelegate _siguiente;
    private readonly ILogger<ManejoErroresMiddleware> _logger;

    public ManejoErroresMiddleware(RequestDelegate siguiente, ILogger<ManejoErroresMiddleware> logger)
    {
        _siguiente = siguiente;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext contexto)
    {
        try
        {
            await _siguiente(contexto);
        }
        catch (NoEncontradoException ex)
        {
            await Responder(contexto, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (ReglaNegocioException ex)
        {
            await Responder(contexto, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no controlado");
            await Responder(contexto, StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado en el servidor.");
        }
    }

    private static Task Responder(HttpContext contexto, int codigo, string mensaje)
    {
        contexto.Response.StatusCode = codigo;
        return contexto.Response.WriteAsJsonAsync(new { mensaje });
    }
}
