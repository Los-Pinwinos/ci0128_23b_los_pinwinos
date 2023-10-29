// Constante de sobreestimación. Existe un adicional de espacio que se agrega por etiqueta que debe ser despreciado para el movimiento de los filtros
const sobreestimacionDeAltura = 29;

// Calcular la altura del contenido del desplegable
function calcularAlturaDesplegable(idDesplegable) {
    const contenidoDesplegable = document.getElementById('ContenidoFiltro' + idDesplegable);
    const etiquetas = contenidoDesplegable.querySelectorAll('label');
    let alturaTotal = 0;

    etiquetas.forEach(etiqueta => {
        // Obtener la altura de cada etiqueta
        const alturaEtiqueta = etiqueta.clientHeight - sobreestimacionDeAltura;
        alturaTotal += alturaEtiqueta;
    });

    return alturaTotal;
}

// Ajustar la posición del desplegable
function ajustarPosicionDesplegable(elementoDisparador, idDesplegable, accion, posicion) {
    const desplegableSuperior = document.getElementById('ContenidoFiltro' + (idDesplegable - 1));

    const boton = elementoDisparador.getElementById('BotonFiltro' + idDesplegable);

    if (accion === 'Bajar') {
        // Calcular el nuevo margen (bajar)
        const etiquetas = desplegableSuperior.querySelectorAll('label');
        const cuentaDeEtiquetas = etiquetas.length;
        if (cuentaDeEtiquetas > 0) {
            const margenActual = parseInt(getComputedStyle(boton).marginTop, 10);
            boton.style.marginTop = `${margenActual + posicion}px`;
        }
    } else {
        // Calcular el nuevo margen (subir)
        const etiquetas = desplegableSuperior.querySelectorAll('label');
        const cuentaDeEtiquetas = etiquetas.length;
        if (cuentaDeEtiquetas > 0) {
            const margenActual = parseInt(getComputedStyle(boton).marginTop, 10);
            boton.style.marginTop = `${margenActual - posicion}px`;
        }
    }
}

function mostrarDesplegable(idDesplegable) {
    const contenidoDesplegable = document.getElementById('ContenidoFiltro' + idDesplegable);
    contenidoDesplegable.style.display = 'block';
    const posicion = calcularAlturaDesplegable(idDesplegable);
    ajustarPosicionDesplegable(document, idDesplegable + 1, 'Bajar', posicion);
}

function ocultarDesplegable(idDesplegable) {
    const contenidoDesplegable = document.getElementById('ContenidoFiltro' + idDesplegable);
    const posicion = calcularAlturaDesplegable(idDesplegable);
    contenidoDesplegable.style.display = 'none';
    ajustarPosicionDesplegable(document, idDesplegable + 1, 'Subir', posicion);
}
