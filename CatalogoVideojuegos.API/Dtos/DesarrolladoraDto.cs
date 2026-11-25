using System.ComponentModel.DataAnnotations;

namespace CatalogoVideojuegos.API.Dtos;

/// <summary>
/// Datos que la API devuelve de una desarrolladora.
/// </summary>
public record DesarrolladoraDto(int Id, string Nombre, string? Pais, int? AnioFundacion);

/// <summary>
/// Datos que se reciben para crear o actualizar una desarrolladora.
/// </summary>
public class DesarrolladoraGuardarDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(60, ErrorMessage = "El país no puede superar los 60 caracteres.")]
    public string? Pais { get; set; }

    [Range(1800, 2100, ErrorMessage = "El año de fundación debe estar entre 1800 y 2100.")]
    public int? AnioFundacion { get; set; }
}
