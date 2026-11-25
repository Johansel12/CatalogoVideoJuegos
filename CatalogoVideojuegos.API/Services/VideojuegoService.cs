using CatalogoVideojuegos.API.Data;
using CatalogoVideojuegos.API.Dtos;
using CatalogoVideojuegos.API.Exceptions;
using CatalogoVideojuegos.API.Models;

namespace CatalogoVideojuegos.API.Services;

public class VideojuegoService : IVideojuegoService
{
    private readonly IRepositorio<Videojuego> _videojuegos;
    private readonly IRepositorio<Desarrolladora> _desarrolladoras;
    private readonly IRepositorio<Plataforma> _plataformas;

    public VideojuegoService(
        IRepositorio<Videojuego> videojuegos,
        IRepositorio<Desarrolladora> desarrolladoras,
        IRepositorio<Plataforma> plataformas)
    {
        _videojuegos = videojuegos;
        _desarrolladoras = desarrolladoras;
        _plataformas = plataformas;
    }

    public IEnumerable<VideojuegoDto> ObtenerTodos() =>
        _videojuegos.ObtenerTodos()
            .OrderBy(v => v.Titulo)
            .Select(ADto)
            .ToList();

    public VideojuegoDto ObtenerPorId(int id) => ADto(Buscar(id));

    public VideojuegoDto Crear(VideojuegoGuardarDto dto)
    {
        ValidarRelaciones(dto);

        var videojuego = new Videojuego();
        AsignarDatos(videojuego, dto);

        return ADto(_videojuegos.Agregar(videojuego));
    }

    public VideojuegoDto Actualizar(int id, VideojuegoGuardarDto dto)
    {
        var videojuego = Buscar(id);
        ValidarRelaciones(dto);
        AsignarDatos(videojuego, dto);

        _videojuegos.Actualizar(videojuego);
        return ADto(videojuego);
    }

    public void Eliminar(int id)
    {
        Buscar(id);
        _videojuegos.Eliminar(id);
    }

    private Videojuego Buscar(int id) =>
        _videojuegos.ObtenerPorId(id)
        ?? throw new NoEncontradoException($"No existe un videojuego con el id {id}.");

    private void ValidarRelaciones(VideojuegoGuardarDto dto)
    {
        if (_desarrolladoras.ObtenerPorId(dto.DesarrolladoraId!.Value) is null)
            throw new ReglaNegocioException($"La desarrolladora con id {dto.DesarrolladoraId} no existe.");

        if (_plataformas.ObtenerPorId(dto.PlataformaId!.Value) is null)
            throw new ReglaNegocioException($"La plataforma con id {dto.PlataformaId} no existe.");
    }

    private static void AsignarDatos(Videojuego videojuego, VideojuegoGuardarDto dto)
    {
        videojuego.Titulo = dto.Titulo.Trim();
        videojuego.Genero = dto.Genero.Trim();
        videojuego.Precio = dto.Precio!.Value;
        videojuego.FechaLanzamiento = dto.FechaLanzamiento;
        videojuego.DesarrolladoraId = dto.DesarrolladoraId!.Value;
        videojuego.PlataformaId = dto.PlataformaId!.Value;
    }

    private VideojuegoDto ADto(Videojuego v) => new(
        v.Id,
        v.Titulo,
        v.Genero,
        v.Precio,
        v.FechaLanzamiento,
        v.DesarrolladoraId,
        _desarrolladoras.ObtenerPorId(v.DesarrolladoraId)?.Nombre ?? "(desconocida)",
        v.PlataformaId,
        _plataformas.ObtenerPorId(v.PlataformaId)?.Nombre ?? "(desconocida)");
}
