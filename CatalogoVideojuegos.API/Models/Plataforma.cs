namespace CatalogoVideojuegos.API.Models;

/// <summary>
/// Consola o sistema donde se ejecutan los videojuegos (por ejemplo: PC, PlayStation 5, Nintendo Switch).
/// </summary>
public class Plataforma : IEntidad
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Fabricante { get; set; }
    public int? AnioLanzamiento { get; set; }
}
