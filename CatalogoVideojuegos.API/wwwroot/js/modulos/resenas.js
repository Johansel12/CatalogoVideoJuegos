registrarModulo({
    titulo: 'Reseñas',
    endpoint: '/api/resenas',
    campos: [
        {
            nombre: 'videojuegoId', etiqueta: 'Videojuego', tipo: 'select', numerico: true,
            opciones: async () => {
                const respuesta = await fetch('/api/videojuegos');
                const juegos = respuesta.ok ? await respuesta.json() : [];
                return juegos.map(j => ({ valor: j.id, texto: j.titulo }));
            }
        },
        { nombre: 'autor', etiqueta: 'Autor', tipo: 'text' },
        { nombre: 'puntuacion', etiqueta: 'Puntuación (1 a 10)', tipo: 'number' },
        { nombre: 'comentario', etiqueta: 'Comentario', tipo: 'textarea' }
    ],
    columnas: [
        { campo: 'id', etiqueta: 'ID' },
        { campo: 'videojuegoTitulo', etiqueta: 'Videojuego' },
        { campo: 'autor', etiqueta: 'Autor' },
        { campo: 'puntuacion', etiqueta: 'Puntuación', formato: v => `${v} / 10` },
        { campo: 'comentario', etiqueta: 'Comentario' },
        { campo: 'fecha', etiqueta: 'Fecha', formato: v => new Date(v).toLocaleString('es-DO') }
    ]
});
