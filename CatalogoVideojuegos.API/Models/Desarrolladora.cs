namespace CatalogoVideojuegos.API.Models;

/// <summary>
/// Estudio o empresa que desarrolla videojuegos (por ejemplo: Nintendo, Rockstar Games).
/// </summary>
public class Desarrolladora : IEntidad
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Pais { get; set; }
    public int? AnioFundacion { get; set; }
}
