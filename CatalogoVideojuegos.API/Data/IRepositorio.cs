using CatalogoVideojuegos.API.Models;

namespace CatalogoVideojuegos.API.Data;

/// <summary>
/// Operaciones CRUD genéricas sobre una colección de entidades.
/// </summary>
public interface IRepositorio<T> where T : class, IEntidad
{
    IReadOnlyList<T> ObtenerTodos();
    T? ObtenerPorId(int id);
    T Agregar(T entidad);
    bool Actualizar(T entidad);
    bool Eliminar(int id);
}
