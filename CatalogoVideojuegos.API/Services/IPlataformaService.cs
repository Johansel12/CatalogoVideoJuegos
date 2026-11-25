using CatalogoVideojuegos.API.Dtos;

namespace CatalogoVideojuegos.API.Services;

public interface IPlataformaService
{
    IEnumerable<PlataformaDto> ObtenerTodas();
    PlataformaDto ObtenerPorId(int id);
    PlataformaDto Crear(PlataformaGuardarDto dto);
    PlataformaDto Actualizar(int id, PlataformaGuardarDto dto);
    void Eliminar(int id);
}
