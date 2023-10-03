
// Obtener contenedor de filtros
const contenedorFiltros = document.getElementById("ContenedorFiltros");

// Obtener dropdowns
const dropdowns = contenedorFiltros.querySelectorAll(".BusquedaIndice-dropdown-content");
// Obtener el contador de dropdowns
const cuentaDeDropdowns = dropdowns.length;
// Tamanno de label en px
const tamannoDeLabel = 35;

// Ajustar posicion de dropdown
function adjustDropdownPosition(triggerElement, dropdownId, action) {
    // Dropdown de arriba
    const dropDownArriba = document.getElementById('ContenidoFiltro' + (dropdownId - 1));

    // Obtener elementos
    const boton = triggerElement.getElementById('BotonFiltro' + dropdownId);
    const siguienteDropdown = triggerElement.getElementById('ContenidoFiltro' + dropdownId);
    // Obtener altura maxima

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
        // Calculatar el nuevo margen (subir)
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