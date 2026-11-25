using System.ComponentModel.DataAnnotations;

namespace CatalogoVideojuegos.API.Dtos;

/// <summary>
/// Datos que la API devuelve de un videojuego, incluyendo los nombres de sus relaciones.
/// </summary>
public record VideojuegoDto(
    int Id,
    string Titulo,
    string Genero,
    decimal Precio,
    DateOnly? FechaLanzamiento,
    int DesarrolladoraId,
    string DesarrolladoraNombre,
    int PlataformaId,
    string PlataformaNombre);

/// <summary>
/// Datos que se reciben para crear o actualizar un videojuego.
/// </summary>
public class VideojuegoGuardarDto
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(120, ErrorMessage = "El título no puede superar los 120 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El género es obligatorio.")]
    [StringLength(40, ErrorMessage = "El género no puede superar los 40 caracteres.")]
    public string Genero { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0, 1000, ErrorMessage = "El precio debe estar entre 0 y 1000.")]
    public decimal? Precio { get; set; }

    public DateOnly? FechaLanzamiento { get; set; }

    [Required(ErrorMessage = "Debe seleccionar una desarrolladora.")]
    public int? DesarrolladoraId { get; set; }

    [Required(ErrorMessage = "Debe seleccionar una plataforma.")]
    public int? PlataformaId { get; set; }
}
