using CatalogoVideojuegos.API.Dtos;

namespace CatalogoVideojuegos.API.Services;

public interface IVideojuegoService
{
    IEnumerable<VideojuegoDto> ObtenerTodos();
    VideojuegoDto ObtenerPorId(int id);
    VideojuegoDto Crear(VideojuegoGuardarDto dto);
    VideojuegoDto Actualizar(int id, VideojuegoGuardarDto dto);
    void Eliminar(int id);
}
