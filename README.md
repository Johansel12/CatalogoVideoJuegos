# 🎮 Catálogo de Videojuegos

Proyecto CRUD desarrollado en **ASP.NET Core 8 (C#)** para la Tarea 3 de Programación III (ITLA),
aplicando la metodología **Git Flow**.

**Autor:** Johansel Beltre de León · Matrícula 2025-1877

## Tecnologías

- ASP.NET Core 8 Web API (controladores)
- Persistencia en archivos JSON (no requiere instalar base de datos)
- Interfaz web en HTML, CSS y JavaScript (carpeta `wwwroot`)

## Cómo ejecutar

Requisito: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

```bash
cd CatalogoVideojuegos.API
dotnet run
```

Luego abrir <http://localhost:5035> en el navegador.

## Estructura

```
CatalogoVideojuegos.API/
├── Controllers/   Endpoints REST
├── Data/          Repositorio genérico (JSON)
├── Dtos/          Objetos de entrada y salida
├── Exceptions/    Excepciones del negocio
├── Middleware/    Manejo centralizado de errores
├── Models/        Entidades
├── Services/      Lógica de negocio
└── wwwroot/       Interfaz web
```

## Flujo de trabajo (Git Flow)

| Rama | Propósito |
|------|-----------|
| `main` | Código estable en producción |
| `develop` | Integración de nuevas funcionalidades |
| `qa` | Pruebas de calidad antes de pasar a `main` |
| `feature/*` | Desarrollo de cada funcionalidad |
| `hotfix/*` | Correcciones urgentes sobre producción |
