using System.ComponentModel.DataAnnotations;

namespace CatalogoVideojuegos.API.Dtos;

/// <summary>
/// Datos que la API devuelve de una plataforma.
/// </summary>
public record PlataformaDto(int Id, string Nombre, string? Fabricante, int? AnioLanzamiento);

/// <summary>
/// Datos que se reciben para crear o actualizar una plataforma.
/// </summary>
public class PlataformaGuardarDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(60, ErrorMessage = "El nombre no puede superar los 60 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(60, ErrorMessage = "El fabricante no puede superar los 60 caracteres.")]
    public string? Fabricante { get; set; }

    [Range(1970, 2100, ErrorMessage = "El año de lanzamiento debe estar entre 1970 y 2100.")]
    public int? AnioLanzamiento { get; set; }
}
