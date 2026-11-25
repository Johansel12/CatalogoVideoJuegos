using CatalogoVideojuegos.API.Dtos;

namespace CatalogoVideojuegos.API.Services;

public interface IDesarrolladoraService
{
    IEnumerable<DesarrolladoraDto> ObtenerTodas();
    DesarrolladoraDto ObtenerPorId(int id);
    DesarrolladoraDto Crear(DesarrolladoraGuardarDto dto);
    DesarrolladoraDto Actualizar(int id, DesarrolladoraGuardarDto dto);
    void Eliminar(int id);
}
