using CatalogoVideojuegos.API.Dtos;
using CatalogoVideojuegos.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoVideojuegos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideojuegosController : ControllerBase
{
    private readonly IVideojuegoService _servicio;

    public VideojuegosController(IVideojuegoService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VideojuegoDto>> ObtenerTodos() =>
        Ok(_servicio.ObtenerTodos());

    [HttpGet("{id:int}")]
    public ActionResult<VideojuegoDto> ObtenerPorId(int id) =>
        Ok(_servicio.ObtenerPorId(id));

    [HttpPost]
    public ActionResult<VideojuegoDto> Crear(VideojuegoGuardarDto dto)
    {
        var creado = _servicio.Crear(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.Id }, creado);
    }

    [HttpPut("{id:int}")]
    public ActionResult<VideojuegoDto> Actualizar(int id, VideojuegoGuardarDto dto) =>
        Ok(_servicio.Actualizar(id, dto));

    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        _servicio.Eliminar(id);
        return NoContent();
    }
}
