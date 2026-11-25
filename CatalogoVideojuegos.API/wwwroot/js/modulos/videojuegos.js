const generosVideojuego = [
    'Acción', 'Aventura', 'RPG', 'Deportes', 'Carreras',
    'Estrategia', 'Shooter', 'Plataformas', 'Simulación', 'Terror'
];

async function opcionesDesde(endpoint) {
    const respuesta = await fetch(endpoint);
    const registros = respuesta.ok ? await respuesta.json() : [];
    return registros.map(r => ({ valor: r.id, texto: r.nombre }));
}

registrarModulo({
    titulo: 'Videojuegos',
    endpoint: '/api/videojuegos',
    campos: [
        { nombre: 'titulo', etiqueta: 'Título', tipo: 'text' },
        {
            nombre: 'genero', etiqueta: 'Género', tipo: 'select',
            opciones: async () => generosVideojuego.map(g => ({ valor: g, texto: g }))
        },
        { nombre: 'precio', etiqueta: 'Precio (US$)', tipo: 'number', min: 0, max: 1000, paso: 0.01 },
        { nombre: 'fechaLanzamiento', etiqueta: 'Fecha de lanzamiento', tipo: 'date' },
        {
            nombre: 'desarrolladoraId', etiqueta: 'Desarrolladora', tipo: 'select', numerico: true,
            opciones: () => opcionesDesde('/api/desarrolladoras')
        },
        {
            nombre: 'plataformaId', etiqueta: 'Plataforma', tipo: 'select', numerico: true,
            opciones: () => opcionesDesde('/api/plataformas')
        }
    ],
    columnas: [
        { campo: 'id', etiqueta: 'ID' },
        { campo: 'titulo', etiqueta: 'Título' },
        { campo: 'genero', etiqueta: 'Género' },
        { campo: 'precio', etiqueta: 'Precio', formato: v => `US$ ${Number(v).toFixed(2)}` },
        { campo: 'fechaLanzamiento', etiqueta: 'Lanzamiento' },
        { campo: 'desarrolladoraNombre', etiqueta: 'Desarrolladora' },
        { campo: 'plataformaNombre', etiqueta: 'Plataforma' }
    ]
});
