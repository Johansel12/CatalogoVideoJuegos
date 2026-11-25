using CatalogoVideojuegos.API.Data;
using CatalogoVideojuegos.API.Dtos;
using CatalogoVideojuegos.API.Exceptions;
using CatalogoVideojuegos.API.Models;

namespace CatalogoVideojuegos.API.Services;

public class ResenaService : IResenaService
{
    private readonly IRepositorio<Resena> _resenas;
    private readonly IRepositorio<Videojuego> _videojuegos;

    public ResenaService(IRepositorio<Resena> resenas, IRepositorio<Videojuego> videojuegos)
    {
        _resenas = resenas;
        _videojuegos = videojuegos;
    }

    public IEnumerable<ResenaDto> ObtenerTodas(int? videojuegoId) =>
        _resenas.ObtenerTodos()
            .Where(r => videojuegoId is null || r.VideojuegoId == videojuegoId)
            .OrderByDescending(r => r.Fecha)
            .Select(ADto)
            .ToList();

    public ResenaDto ObtenerPorId(int id) => ADto(Buscar(id));

    public ResenaDto Crear(ResenaGuardarDto dto)
    {
        ValidarVideojuego(dto.VideojuegoId!.Value);

        var resena = new Resena
        {
            VideojuegoId = dto.VideojuegoId.Value,
            Autor = dto.Autor.Trim(),
            Puntuacion = dto.Puntuacion!.Value,
            Comentario = dto.Comentario?.Trim(),
            Fecha = DateTime.Now
        };

        return ADto(_resenas.Agregar(resena));
    }

    public ResenaDto Actualizar(int id, ResenaGuardarDto dto)
    {
        var resena = Buscar(id);
        ValidarVideojuego(dto.VideojuegoId!.Value);

        resena.VideojuegoId = dto.VideojuegoId.Value;
        resena.Autor = dto.Autor.Trim();
        resena.Puntuacion = dto.Puntuacion!.Value;
        resena.Comentario = dto.Comentario?.Trim();

        _resenas.Actualizar(resena);
        return ADto(resena);
    }

    public void Eliminar(int id)
    {
        Buscar(id);
        _resenas.Eliminar(id);
    }

    private Resena Buscar(int id) =>
        _resenas.ObtenerPorId(id)
        ?? throw new NoEncontradoException($"No existe una reseña con el id {id}.");

    private void ValidarVideojuego(int videojuegoId)
    {
        if (_videojuegos.ObtenerPorId(videojuegoId) is null)
            throw new ReglaNegocioException($"El videojuego con id {videojuegoId} no existe.");
    }

    private ResenaDto ADto(Resena r) => new(
        r.Id,
        r.VideojuegoId,
        _videojuegos.ObtenerPorId(r.VideojuegoId)?.Titulo ?? "(desconocido)",
        r.Autor,
        r.Puntuacion,
        r.Comentario,
        r.Fecha);
}
