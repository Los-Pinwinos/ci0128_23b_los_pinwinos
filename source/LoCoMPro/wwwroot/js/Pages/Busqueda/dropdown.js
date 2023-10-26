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

// Ajustar posicion de dropdown
function adjustDropdownPosition(triggerElement, dropdownId, action) {
    // Dropdown de arriba
    const dropDownArriba = document.getElementById('ContenidoFiltro' + (dropdownId - 1));

    // Obtener elementos
    const boton = triggerElement.getElementById('BotonFiltro' + dropdownId);

    // Determinar accion
    if (action === 'Bajar') {
        // Calcular el nuevo margen (bajar)
        const labels = dropDownArriba.querySelectorAll('label');
        const cuentaDeLabels = labels.length;
        if (cuentaDeLabels > 0) {
            const margenActual = parseInt(getComputedStyle(boton).marginTop, 10);
            boton.style.marginTop = `${margenActual + cuentaDeLabels * tamannoDeLabel}px`;
        }
    } else {
        // Calcular el nuevo margen (subir)
        const labels = dropDownArriba.querySelectorAll('label');
        const cuentaDeLabels = labels.length;
        if (cuentaDeLabels > 0) {
            const margenActual = parseInt(getComputedStyle(boton).marginTop, 10);
            boton.style.marginTop = `${margenActual - cuentaDeLabels * tamannoDeLabel}px`;
        }
    }
}

// Mostrar dropdown
function showdropdown(dropdownId) {
    const dropdownContent = document.getElementById('ContenidoFiltro' + dropdownId);
    dropdownContent.style.display = 'block';
    adjustDropdownPosition(document, dropdownId + 1, 'Bajar');

}

// Esconder dropdown
function hidedropdown(dropdownId) {
    const dropdownContent = document.getElementById('ContenidoFiltro' + dropdownId);
    dropdownContent.style.display = 'none';
    adjustDropdownPosition(document, dropdownId + 1, 'Subir');
}