registrarModulo({
    titulo: 'Desarrolladoras',
    endpoint: '/api/desarrolladoras',
    campos: [
        { nombre: 'nombre', etiqueta: 'Nombre', tipo: 'text' },
        { nombre: 'pais', etiqueta: 'País', tipo: 'text' },
        { nombre: 'anioFundacion', etiqueta: 'Año de fundación', tipo: 'number', min: 1800, max: 2100 }
    ],
    columnas: [
        { campo: 'id', etiqueta: 'ID' },
        { campo: 'nombre', etiqueta: 'Nombre' },
        { campo: 'pais', etiqueta: 'País' },
        { campo: 'anioFundacion', etiqueta: 'Fundación' }
    ]
});
