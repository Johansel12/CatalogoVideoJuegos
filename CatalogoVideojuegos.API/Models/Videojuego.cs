namespace CatalogoVideojuegos.API.Models;

/// <summary>
/// Videojuego del catálogo. Pertenece a una desarrolladora y se publica en una plataforma.
/// </summary>
public class Videojuego : IEntidad
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public DateOnly? FechaLanzamiento { get; set; }
    public int DesarrolladoraId { get; set; }
    public int PlataformaId { get; set; }
}
