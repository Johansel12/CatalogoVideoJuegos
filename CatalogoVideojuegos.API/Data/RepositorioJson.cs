using System.Text.Json;
using CatalogoVideojuegos.API.Models;

namespace CatalogoVideojuegos.API.Data;

/// <summary>
/// Repositorio que persiste cada colección en un archivo JSON dentro de la carpeta "datos".
/// No requiere instalar ningún motor de base de datos.
/// </summary>
public class RepositorioJson<T> : IRepositorio<T> where T : class, IEntidad
{
    private static readonly JsonSerializerOptions Opciones = new() { WriteIndented = true };
    private readonly string _rutaArchivo;
    private readonly object _bloqueo = new();

    public RepositorioJson(IWebHostEnvironment entorno)
    {
        var carpeta = Path.Combine(entorno.ContentRootPath, "datos");
        Directory.CreateDirectory(carpeta);
        _rutaArchivo = Path.Combine(carpeta, $"{typeof(T).Name.ToLowerInvariant()}.json");
    }

    public IReadOnlyList<T> ObtenerTodos()
    {
        lock (_bloqueo) return Leer();
    }

    public T? ObtenerPorId(int id)
    {
        lock (_bloqueo) return Leer().FirstOrDefault(e => e.Id == id);
    }

    public T Agregar(T entidad)
    {
        lock (_bloqueo)
        {
            var elementos = Leer();
            entidad.Id = elementos.Count == 0 ? 1 : elementos.Max(e => e.Id) + 1;
            elementos.Add(entidad);
            Guardar(elementos);
            return entidad;
        }
    }

    public bool Actualizar(T entidad)
    {
        lock (_bloqueo)
        {
            var elementos = Leer();
            var indice = elementos.FindIndex(e => e.Id == entidad.Id);
            if (indice < 0) return false;
            elementos[indice] = entidad;
            Guardar(elementos);
            return true;
        }
    }

    public bool Eliminar(int id)
    {
        lock (_bloqueo)
        {
            var elementos = Leer();
            var eliminados = elementos.RemoveAll(e => e.Id == id);
            if (eliminados == 0) return false;
            Guardar(elementos);
            return true;
        }
    }

    private List<T> Leer()
    {
        if (!File.Exists(_rutaArchivo)) return new List<T>();
        var json = File.ReadAllText(_rutaArchivo);
        if (string.IsNullOrWhiteSpace(json)) return new List<T>();
        return JsonSerializer.Deserialize<List<T>>(json, Opciones) ?? new List<T>();
    }

    private void Guardar(List<T> elementos)
    {
        File.WriteAllText(_rutaArchivo, JsonSerializer.Serialize(elementos, Opciones));
    }
}
