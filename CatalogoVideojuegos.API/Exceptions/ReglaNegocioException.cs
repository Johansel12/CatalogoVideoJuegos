namespace CatalogoVideojuegos.API.Exceptions;

/// <summary>
/// Se lanza cuando una operación viola una regla del negocio. Se traduce a HTTP 400.
/// </summary>
public class ReglaNegocioException : Exception
{
    public ReglaNegocioException(string mensaje) : base(mensaje) { }
}
