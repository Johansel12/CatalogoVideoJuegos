/*
 * Motor CRUD genérico.
 * Cada módulo se registra con registrarModulo({ ... }) indicando su endpoint,
 * los campos del formulario y las columnas de la tabla.
 */
const modulos = [];

function registrarModulo(config) {
    modulos.push(config);
}

document.addEventListener('DOMContentLoaded', () => {
    const menu = document.getElementById('menu');

    if (modulos.length === 0) {
        document.getElementById('contenido').innerHTML =
            '<p class="vacio">Bienvenido. Todavía no hay módulos registrados.</p>';
        return;
    }

    modulos.forEach((modulo, indice) => {
        const boton = document.createElement('button');
        boton.textContent = modulo.titulo;
        boton.addEventListener('click', () => mostrarModulo(modulo, boton));
        menu.appendChild(boton);
        if (indice === 0) mostrarModulo(modulo, boton);
    });
});

function escaparHtml(valor) {
    return String(valor ?? '')
        .replaceAll('&', '&amp;')
        .replaceAll('<', '&lt;')
        .replaceAll('>', '&gt;')
        .replaceAll('"', '&quot;');
}

async function mostrarModulo(modulo, boton) {
    document.querySelectorAll('#menu button')
        .forEach(b => b.classList.toggle('activo', b === boton));

    document.getElementById('contenido').innerHTML = `
        <section class="tarjeta">
            <h2 id="titulo-formulario">Nuevo registro</h2>
            <form id="formulario" novalidate></form>
            <p id="mensaje" class="mensaje"></p>
        </section>
        <section class="tarjeta">
            <h2>${escaparHtml(modulo.titulo)}</h2>
            <table>
                <thead id="tabla-encabezado"></thead>
                <tbody id="tabla-cuerpo"></tbody>
            </table>
        </section>`;

    await construirFormulario(modulo);
    await cargarTabla(modulo);
}

async function construirFormulario(modulo) {
    const formulario = document.getElementById('formulario');
    let html = '<input type="hidden" name="id">';

    for (const campo of modulo.campos) {
        html += `<label for="campo-${campo.nombre}">${escaparHtml(campo.etiqueta)}</label>`;
        const atributos = [
            `id="campo-${campo.nombre}"`,
            `name="${campo.nombre}"`,
            campo.min !== undefined ? `min="${campo.min}"` : '',
            campo.max !== undefined ? `max="${campo.max}"` : '',
            campo.paso !== undefined ? `step="${campo.paso}"` : ''
        ].join(' ');

        if (campo.tipo === 'select') {
            const opciones = await campo.opciones();
            html += `<select ${atributos}>
                <option value="">-- Seleccione --</option>
                ${opciones.map(o => `<option value="${o.valor}">${escaparHtml(o.texto)}</option>`).join('')}
            </select>`;
        } else if (campo.tipo === 'textarea') {
            html += `<textarea ${atributos} rows="3"></textarea>`;
        } else {
            html += `<input type="${campo.tipo || 'text'}" ${atributos}>`;
        }
    }

    html += `
        <div class="acciones-form">
            <button type="submit" class="principal">Guardar</button>
            <button type="button" class="secundario" id="cancelar">Cancelar</button>
        </div>`;

    formulario.innerHTML = html;
    formulario.addEventListener('submit', evento => guardar(evento, modulo));
    document.getElementById('cancelar').addEventListener('click', () => limpiarFormulario());
}

function limpiarFormulario() {
    const formulario = document.getElementById('formulario');
    formulario.reset();
    formulario.elements.id.value = '';
    document.getElementById('titulo-formulario').textContent = 'Nuevo registro';
}

function mostrarMensaje(texto, tipo) {
    const mensaje = document.getElementById('mensaje');
    mensaje.textContent = texto;
    mensaje.className = `mensaje ${tipo}`;
}

async function leerError(respuesta) {
    try {
        const datos = await respuesta.json();
        if (datos.errors) return Object.values(datos.errors).flat().join('\n');
        if (datos.mensaje) return datos.mensaje;
        if (datos.title) return datos.title;
    } catch { /* respuesta sin cuerpo JSON */ }
    return `Error ${respuesta.status}`;
}

async function guardar(evento, modulo) {
    evento.preventDefault();
    const formulario = evento.target;
    const id = formulario.elements.id.value;

    const datos = {};
    for (const campo of modulo.campos) {
        const valor = formulario.elements[campo.nombre].value.trim();
        if (valor === '') {
            datos[campo.nombre] = null;
        } else if (campo.tipo === 'number' || campo.tipo === 'select') {
            datos[campo.nombre] = Number(valor);
        } else {
            datos[campo.nombre] = valor;
        }
    }

    const respuesta = await fetch(id ? `${modulo.endpoint}/${id}` : modulo.endpoint, {
        method: id ? 'PUT' : 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(datos)
    });

    if (!respuesta.ok) {
        mostrarMensaje(await leerError(respuesta), 'error');
        return;
    }

    mostrarMensaje(id ? 'Registro actualizado correctamente.' : 'Registro creado correctamente.', 'exito');
    limpiarFormulario();
    await cargarTabla(modulo);
}

async function cargarTabla(modulo) {
    const encabezado = document.getElementById('tabla-encabezado');
    const cuerpo = document.getElementById('tabla-cuerpo');

    encabezado.innerHTML = `<tr>
        ${modulo.columnas.map(c => `<th>${escaparHtml(c.etiqueta)}</th>`).join('')}
        <th>Acciones</th>
    </tr>`;

    const respuesta = await fetch(modulo.endpoint);
    const registros = respuesta.ok ? await respuesta.json() : [];

    if (registros.length === 0) {
        cuerpo.innerHTML = `<tr><td colspan="${modulo.columnas.length + 1}">No hay registros todavía.</td></tr>`;
        return;
    }

    cuerpo.innerHTML = registros.map(registro => `
        <tr>
            ${modulo.columnas.map(c => {
                const valor = c.formato ? c.formato(registro[c.campo]) : registro[c.campo];
                return `<td>${escaparHtml(valor)}</td>`;
            }).join('')}
            <td class="acciones">
                <button class="enlace" data-editar="${registro.id}">Editar</button>
                <button class="peligro" data-eliminar="${registro.id}">Eliminar</button>
            </td>
        </tr>`).join('');

    cuerpo.querySelectorAll('[data-editar]').forEach(boton =>
        boton.addEventListener('click', () => editar(modulo, Number(boton.dataset.editar))));
    cuerpo.querySelectorAll('[data-eliminar]').forEach(boton =>
        boton.addEventListener('click', () => eliminar(modulo, Number(boton.dataset.eliminar))));
}

async function editar(modulo, id) {
    const respuesta = await fetch(`${modulo.endpoint}/${id}`);
    if (!respuesta.ok) {
        mostrarMensaje(await leerError(respuesta), 'error');
        return;
    }

    const registro = await respuesta.json();
    const formulario = document.getElementById('formulario');
    formulario.elements.id.value = registro.id;
    for (const campo of modulo.campos) {
        formulario.elements[campo.nombre].value = registro[campo.nombre] ?? '';
    }
    document.getElementById('titulo-formulario').textContent = `Editando registro #${id}`;
    mostrarMensaje('', '');
}

async function eliminar(modulo, id) {
    if (!confirm(`¿Seguro que desea eliminar el registro #${id}?`)) return;

    const respuesta = await fetch(`${modulo.endpoint}/${id}`, { method: 'DELETE' });
    if (!respuesta.ok) {
        mostrarMensaje(await leerError(respuesta), 'error');
        return;
    }

    mostrarMensaje('Registro eliminado correctamente.', 'exito');
    await cargarTabla(modulo);
}
