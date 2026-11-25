namespace CatalogoVideojuegos.API.Models;

/// <summary>
/// Contrato común para todas las entidades que se guardan en el repositorio.
/// </summary>
public interface IEntidad
{
    int Id { get; set; }
}
