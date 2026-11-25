using CatalogoVideojuegos.API.Dtos;
using CatalogoVideojuegos.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoVideojuegos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlataformasController : ControllerBase
{
    private readonly IPlataformaService _servicio;

    public PlataformasController(IPlataformaService servicio)
    {
        _servicio = servicio;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlataformaDto>> ObtenerTodas() =>
        Ok(_servicio.ObtenerTodas());

    [HttpGet("{id:int}")]
    public ActionResult<PlataformaDto> ObtenerPorId(int id) =>
        Ok(_servicio.ObtenerPorId(id));

    [HttpPost]
    public ActionResult<PlataformaDto> Crear(PlataformaGuardarDto dto)
    {
        var creada = _servicio.Crear(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
    }

    [HttpPut("{id:int}")]
    public ActionResult<PlataformaDto> Actualizar(int id, PlataformaGuardarDto dto) =>
        Ok(_servicio.Actualizar(id, dto));

    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        _servicio.Eliminar(id);
        return NoContent();
    }
}
