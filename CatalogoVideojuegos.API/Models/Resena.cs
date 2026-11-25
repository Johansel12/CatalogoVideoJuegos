namespace CatalogoVideojuegos.API.Models;

/// <summary>
/// Opinión de un jugador sobre un videojuego, con una puntuación.
/// </summary>
public class Resena : IEntidad
{
    public int Id { get; set; }
    public int VideojuegoId { get; set; }
    public string Autor { get; set; } = string.Empty;
    public int Puntuacion { get; set; }
    public string? Comentario { get; set; }
    public DateTime Fecha { get; set; }
}
