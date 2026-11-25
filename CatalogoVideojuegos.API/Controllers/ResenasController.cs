using CatalogoVideojuegos.API.Dtos;
using CatalogoVideojuegos.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoVideojuegos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResenasController : ControllerBase
{
    private readonly IResenaService _servicio;

    public ResenasController(IResenaService servicio)
    {
        _servicio = servicio;
    }

    /// <summary>
    /// Lista las reseñas. Opcionalmente filtra por videojuego: /api/resenas?videojuegoId=1
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<ResenaDto>> ObtenerTodas([FromQuery] int? videojuegoId) =>
        Ok(_servicio.ObtenerTodas(videojuegoId));

    [HttpGet("{id:int}")]
    public ActionResult<ResenaDto> ObtenerPorId(int id) =>
        Ok(_servicio.ObtenerPorId(id));

    [HttpPost]
    public ActionResult<ResenaDto> Crear(ResenaGuardarDto dto)
    {
        var creada = _servicio.Crear(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
    }

    [HttpPut("{id:int}")]
    public ActionResult<ResenaDto> Actualizar(int id, ResenaGuardarDto dto) =>
        Ok(_servicio.Actualizar(id, dto));

    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        _servicio.Eliminar(id);
        return NoContent();
    }
}
