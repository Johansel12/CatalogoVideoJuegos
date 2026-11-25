namespace CatalogoVideojuegos.API.Exceptions;

/// <summary>
/// Se lanza cuando el registro solicitado no existe. Se traduce a HTTP 404.
/// </summary>
public class NoEncontradoException : Exception
{
    public NoEncontradoException(string mensaje) : base(mensaje) { }
}
