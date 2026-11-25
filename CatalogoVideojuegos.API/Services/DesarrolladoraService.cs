using CatalogoVideojuegos.API.Data;
using CatalogoVideojuegos.API.Dtos;
using CatalogoVideojuegos.API.Exceptions;
using CatalogoVideojuegos.API.Models;

namespace CatalogoVideojuegos.API.Services;

public class DesarrolladoraService : IDesarrolladoraService
{
    private readonly IRepositorio<Desarrolladora> _repositorio;
    private readonly IRepositorio<Videojuego> _videojuegos;

    public DesarrolladoraService(IRepositorio<Desarrolladora> repositorio, IRepositorio<Videojuego> videojuegos)
    {
        _repositorio = repositorio;
        _videojuegos = videojuegos;
    }

    public IEnumerable<DesarrolladoraDto> ObtenerTodas() =>
        _repositorio.ObtenerTodos()
            .OrderBy(d => d.Nombre)
            .Select(ADto);

    public DesarrolladoraDto ObtenerPorId(int id) => ADto(Buscar(id));

    public DesarrolladoraDto Crear(DesarrolladoraGuardarDto dto)
    {
        ValidarNombreUnico(dto.Nombre, idActual: null);

        var desarrolladora = new Desarrolladora
        {
            Nombre = dto.Nombre.Trim(),
            Pais = dto.Pais?.Trim(),
            AnioFundacion = dto.AnioFundacion
        };

        return ADto(_repositorio.Agregar(desarrolladora));
    }

    public DesarrolladoraDto Actualizar(int id, DesarrolladoraGuardarDto dto)
    {
        var desarrolladora = Buscar(id);
        ValidarNombreUnico(dto.Nombre, idActual: id);

        desarrolladora.Nombre = dto.Nombre.Trim();
        desarrolladora.Pais = dto.Pais?.Trim();
        desarrolladora.AnioFundacion = dto.AnioFundacion;

        _repositorio.Actualizar(desarrolladora);
        return ADto(desarrolladora);
    }

    public void Eliminar(int id)
    {
        Buscar(id);

        if (_videojuegos.ObtenerTodos().Any(v => v.DesarrolladoraId == id))
            throw new ReglaNegocioException("No se puede eliminar la desarrolladora porque tiene videojuegos asociados.");

        _repositorio.Eliminar(id);
    }

    private Desarrolladora Buscar(int id) =>
        _repositorio.ObtenerPorId(id)
        ?? throw new NoEncontradoException($"No existe una desarrolladora con el id {id}.");

    private void ValidarNombreUnico(string nombre, int? idActual)
    {
        var existe = _repositorio.ObtenerTodos().Any(d =>
            d.Id != idActual &&
            string.Equals(d.Nombre, nombre.Trim(), StringComparison.OrdinalIgnoreCase));

        if (existe)
            throw new ReglaNegocioException($"Ya existe una desarrolladora llamada \"{nombre.Trim()}\".");
    }

    private static DesarrolladoraDto ADto(Desarrolladora d) =>
        new(d.Id, d.Nombre, d.Pais, d.AnioFundacion);
}
