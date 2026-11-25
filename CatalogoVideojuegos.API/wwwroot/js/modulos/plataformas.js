registrarModulo({
    titulo: 'Plataformas',
    endpoint: '/api/plataformas',
    campos: [
        { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
        { nombre: 'fabricante', etiqueta: 'Fabricante', tipo: 'text' },
        { nombre: 'anioLanzamiento', etiqueta: 'Año de lanzamiento', tipo: 'number', min: 1970, max: 2100 }
    ],
    columnas: [
        { campo: 'id', etiqueta: 'ID' },
        { campo: 'nombre', etiqueta: 'Nombre' },
        { campo: 'fabricante', etiqueta: 'Fabricante' },
        { campo: 'anioLanzamiento', etiqueta: 'Lanzamiento' }
    ]
});
