using CatalogoVideojuegos.API.Data;
using CatalogoVideojuegos.API.Dtos;
using CatalogoVideojuegos.API.Exceptions;
using CatalogoVideojuegos.API.Models;

namespace CatalogoVideojuegos.API.Services;

public class PlataformaService : IPlataformaService
{
    private readonly IRepositorio<Plataforma> _repositorio;
    private readonly IRepositorio<Videojuego> _videojuegos;

    public PlataformaService(IRepositorio<Plataforma> repositorio, IRepositorio<Videojuego> videojuegos)
    {
        _repositorio = repositorio;
        _videojuegos = videojuegos;
    }

    public IEnumerable<PlataformaDto> ObtenerTodas() =>
        _repositorio.ObtenerTodos()
            .OrderBy(p => p.Nombre)
            .Select(ADto);

    public PlataformaDto ObtenerPorId(int id) => ADto(Buscar(id));

    public PlataformaDto Crear(PlataformaGuardarDto dto)
    {
        ValidarNombreUnico(dto.Nombre, idActual: null);

        var plataforma = new Plataforma
        {
            Nombre = dto.Nombre.Trim(),
            Fabricante = dto.Fabricante?.Trim(),
            AnioLanzamiento = dto.AnioLanzamiento
        };

        return ADto(_repositorio.Agregar(plataforma));
    }

    public PlataformaDto Actualizar(int id, PlataformaGuardarDto dto)
    {
        var plataforma = Buscar(id);
        ValidarNombreUnico(dto.Nombre, idActual: id);

        plataforma.Nombre = dto.Nombre.Trim();
        plataforma.Fabricante = dto.Fabricante?.Trim();
        plataforma.AnioLanzamiento = dto.AnioLanzamiento;

        _repositorio.Actualizar(plataforma);
        return ADto(plataforma);
    }

    public void Eliminar(int id)
    {
        Buscar(id);

        if (_videojuegos.ObtenerTodos().Any(v => v.PlataformaId == id))
            throw new ReglaNegocioException("No se puede eliminar la plataforma porque tiene videojuegos asociados.");

        _repositorio.Eliminar(id);
    }

    private Plataforma Buscar(int id) =>
        _repositorio.ObtenerPorId(id)
        ?? throw new NoEncontradoException($"No existe una plataforma con el id {id}.");

    private void ValidarNombreUnico(string nombre, int? idActual)
    {
        var existe = _repositorio.ObtenerTodos().Any(p =>
            p.Id != idActual &&
            string.Equals(p.Nombre, nombre.Trim(), StringComparison.OrdinalIgnoreCase));

        if (existe)
            throw new ReglaNegocioException($"Ya existe una plataforma llamada \"{nombre.Trim()}\".");
    }

    private static PlataformaDto ADto(Plataforma p) =>
        new(p.Id, p.Nombre, p.Fabricante, p.AnioLanzamiento);
}
