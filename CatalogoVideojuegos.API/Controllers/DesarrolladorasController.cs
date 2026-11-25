using CatalogoVideojuegos.API.Dtos;
using CatalogoVideojuegos.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoVideojuegos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DesarrolladorasController : ControllerBase
{
    private readonly IDesarrolladoraService _servicio;

    public DesarrolladorasController(IDesarrolladoraService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DesarrolladoraDto>> ObtenerTodas() =>
        Ok(_servicio.ObtenerTodas());

    [HttpGet("{id:int}")]
    public ActionResult<DesarrolladoraDto> ObtenerPorId(int id) =>
        Ok(_servicio.ObtenerPorId(id));

    [HttpPost]
    public ActionResult<DesarrolladoraDto> Crear(DesarrolladoraGuardarDto dto)
    {
        var creada = _servicio.Crear(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
    }

    [HttpPut("{id:int}")]
    public ActionResult<DesarrolladoraDto> Actualizar(int id, DesarrolladoraGuardarDto dto) =>
        Ok(_servicio.Actualizar(id, dto));

    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        _servicio.Eliminar(id);
        return NoContent();
    }
}
