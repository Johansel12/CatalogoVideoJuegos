using CatalogoVideojuegos.API.Dtos;

namespace CatalogoVideojuegos.API.Services;

public interface IResenaService
{
    IEnumerable<ResenaDto> ObtenerTodas(int? videojuegoId);
    ResenaDto ObtenerPorId(int id);
    ResenaDto Crear(ResenaGuardarDto dto);
    ResenaDto Actualizar(int id, ResenaGuardarDto dto);
    void Eliminar(int id);
}
