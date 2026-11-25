using System.ComponentModel.DataAnnotations;

namespace CatalogoVideojuegos.API.Dtos;

/// <summary>
/// Datos que la API devuelve de una reseña, incluyendo el título del videojuego.
/// </summary>
public record ResenaDto(
    int Id,
    int VideojuegoId,
    string VideojuegoTitulo,
    string Autor,
    int Puntuacion,
    string? Comentario,
    DateTime Fecha);

/// <summary>
/// Datos que se reciben para crear o actualizar una reseña.
/// </summary>
public class ResenaGuardarDto
{
    [Required(ErrorMessage = "Debe seleccionar un videojuego.")]
    public int? VideojuegoId { get; set; }

    [Required(ErrorMessage = "El autor es obligatorio.")]
    [StringLength(60, ErrorMessage = "El autor no puede superar los 60 caracteres.")]
    public string Autor { get; set; } = string.Empty;

    [Required(ErrorMessage = "La puntuación es obligatoria.")]
    [Range(1, 10, ErrorMessage = "La puntuación debe estar entre 1 y 10.")]
    public int? Puntuacion { get; set; }

    [StringLength(500, ErrorMessage = "El comentario no puede superar los 500 caracteres.")]
    public string? Comentario { get; set; }
}
