using Microsoft.AspNetCore.Mvc;

namespace CatalogoVideojuegos.API.Controllers;

/// <summary>
/// Endpoint simple para verificar que la API está en funcionamiento.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EstadoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new
    {
        aplicacion = "Catálogo de Videojuegos",
        estado = "En funcionamiento",
        fecha = DateTime.Now
    });
}
