// Formatear fecha
function formatearFecha(fecha) {
    const opciones = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return fecha.toLocaleDateString('es-ES', opciones);
}

// Paginar
function paginar(numeroPagina = productosVM.IndicePagina) {
    return paginador.paginar(resultados, numeroPagina);
}

// Ordenar
function ordenar(propiedadOrdenado) {
    // Configurar ordenador
    if (propiedadOrdenado === ordenador.ordenado) {
        ordenador.cambiarSentido();
    } else {
        ordenador.setPropiedadOrdenada(propiedadOrdenado);
        ordenador.setSentidoOrdenado('asc');
    }
    // Ordenar
    resultados = ordenador.ordenar(resultados);
    productosVM = paginar();
    // Renderizar
    renderizarPaginacion();
    renderizarTabla(productosVM);
}

// Filtrar
function filtrar() {
    // Obtener datos
    var provincias = obtenerValoresSeleccionados("provincia");
    var cantones = obtenerValoresSeleccionados("canton");
    var tiendas = obtenerValoresSeleccionados("tienda");
    var marcas = obtenerValoresSeleccionados("marca");
    // Configurar filtrador
    filtrador.setFiltroProvincias(provincias);
    filtrador.setFiltroCantones(cantones);
    filtrador.setFiltroTiendas(tiendas);
    filtrador.setFiltroMarcas(marcas);
    // Filtrar
    resultados = filtrador.filtrar(resultados);
    productosVM = paginar(paginaDefault);
    // Renderizar
    renderizarFiltros();
    renderizarPaginacion();
    renderizarTabla(productosVM);
}

// Renderizar paginado
function renderizarPaginacion() {
    // Obtener elementos
    var botonPaginaPrevia = document.getElementById("PaginaPrevia");
    var botonSinPaginaPrevia = document.getElementById("SinPaginaPrevia");
    var botonPaginaSiguiente = document.getElementById("PaginaSiguiente");
    var botonSinPaginaSiguiente = document.getElementById("SinPaginaSiguiente");
    var textoPaginaActual = document.getElementById("TextoPaginacion");

    // Poner pagina actual
    textoPaginaActual.textContent = productosVM.IndicePagina;

    // Revisar si desplegar o no
    if (productosVM.TienePaginaPrevia) {
        botonPaginaPrevia.style.display = "block";
        botonSinPaginaPrevia.style.display = "none";
    } else {
        botonPaginaPrevia.style.display = "none";
        botonSinPaginaPrevia.style.display = "block";
    }

    if (productosVM.TieneProximaPagina) {
        botonPaginaSiguiente.style.display = "block";
        botonSinPaginaSiguiente.style.display = "none";
    } else {
        botonPaginaSiguiente.style.display = "none";
        botonSinPaginaSiguiente.style.display = "block";
    }
}

// Renderizar filtros
function renderizarFiltros() {
    // Renderizar
    renderizarFiltroConcreto(1, "provincia");
    renderizarFiltroConcreto(2, "canton");
    renderizarFiltroConcreto(3, "tienda");
    renderizarFiltroConcreto(4, "marca");
}

// Renderizar filtros de provincias
function renderizarFiltroConcreto(numeroDeFiltro, nombreDeFiltro) {
    var filtros = document.getElementById("ContenidoFiltro" + numeroDeFiltro);
    var valoresSeleccionados = {};
    var valoresUnicos = {};
    var casillasExistente = filtros.querySelectorAll('input[type="checkbox"]');
    var checkboxesOrdenados = [];

    // Obtener valores seleccionados
    casillasExistente.forEach(function (casilla) {
        if (casilla.checked) {
            valoresSeleccionados[casilla.value] = true;
        }
    });

    for (var resultado in resultados) {
        // Obtener valor
        var valor = resultados[resultado][nombreDeFiltro];
        // Ver si es válido
        if (typeof valor !== 'undefined' && valor !== "" && !valoresUnicos[valor]) {
            // Asignar como valor único
            valoresUnicos[valor] = true;
            // Crear checkbox
            var casilla = document.createElement("input");
            casilla.type = "checkbox";
            casilla.name = nombreDeFiltro;
            casilla.value = resultados[resultado][nombreDeFiltro];
            // Poner como marcada
            if (valoresSeleccionados[casilla.value]) {
                casilla.checked = true;
            }
            // Crear label
            var etiqueta = document.createElement("label");
            etiqueta.appendChild(casilla);
            etiqueta.appendChild(document.createTextNode(resultados[resultado][nombreDeFiltro]));
            // Agregar a la lista para ordenar
            checkboxesOrdenados.push(etiqueta);
        }
    }

    // Ordenar los checkboxes alfabéticamente
    checkboxesOrdenados.sort(function (a, b) {
        var valueA = a.textContent;
        var valueB = b.textContent;
        return valueA.localeCompare(valueB);
    });

    // Limpiar contenido
    filtros.innerHTML = "";

    // Agregar los checkboxes ordenados de nuevo
    checkboxesOrdenados.forEach(function (checkboxLabel) {
        filtros.appendChild(checkboxLabel);
    });

    // Verificar si es solo uno
    if (filtros.childElementCount === 1) filtros.innerHTML = "";
}


// Renderizar tabla
function renderizarTabla(datos) {
    // Obtener la tabla
    var cuerpoTabla = document.getElementById("CuerpoResultados");

    // Limpiar el contenido existente
    cuerpoTabla.innerHTML = "";

    // Iterar
    for (var dato in datos) {
        var row = document.createElement("tr");

        if (typeof datos[dato].nombre !== 'undefined' && datos[dato].nombre !== "") {
            var nombreCell = document.createElement("td");
            nombreCell.textContent = datos[dato].nombre;

            var marcaCell = document.createElement("td");
            marcaCell.textContent = datos[dato].marca;

            var precioCell = document.createElement("td");
            precioCell.textContent = "₡" + datos[dato].precio;

            var fechaCell = document.createElement("td");
            fechaCell.textContent = formatearFecha(new Date(datos[dato].fecha));

            var tiendaCell = document.createElement("td");
            tiendaCell.textContent = datos[dato].tienda;

            var provinciaCell = document.createElement("td");
            provinciaCell.textContent = datos[dato].provincia;

            var cantonCell = document.createElement("td");
            cantonCell.textContent = datos[dato].canton;

            // Agregar celdas a fila
            row.appendChild(nombreCell);
            row.appendChild(marcaCell);
            row.appendChild(precioCell);
            row.appendChild(fechaCell);
            row.appendChild(tiendaCell);
            row.appendChild(provinciaCell);
            row.appendChild(cantonCell);

            // Agregar celdas cuerpo
            cuerpoTabla.appendChild(row);
        }
    }
}

// Pasar pagina
function pasarPagina(numeroPagina) {
    // Paginar
    productosVM = paginar(numeroPagina);
    // Renderizar
    renderizarPaginacion();
    renderizarTabla(productosVM);
    window.scrollTo(0, 0);
}

// Limpiar check boxes
function limpiarCheckboxes(nombreCampo) {
    var checkboxes = document.querySelectorAll('input[name="' + nombreCampo + '"]:checked');

    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].checked = false;
    }
}

// Limpiar filtros
function limpiarFiltros() {
    // Limpiar checkboxes
    limpiarCheckboxes("provincia");
    limpiarCheckboxes("canton");
    limpiarCheckboxes("tienda");
    limpiarCheckboxes("marca");
    if (filtrador.usado) {
        // Restaurar uso
        filtrador.resetearUso();
        // Restaurar resultados
        resultados = obtenerResultados();
        // Paginar
        productosVM = paginar(paginaDefault);
        // Renderizar
        renderizarFiltros();
        renderizarPaginacion();
        renderizarTabla(productosVM);
    }
}

// Obtener valores de checkboxes
function obtenerValoresSeleccionados(nombreCampo) {
    var valores = [];
    var checkboxes = document.querySelectorAll('input[name="' + nombreCampo + '"]:checked');

    for (var i = 0; i < checkboxes.length; i++) {
        valores.push(checkboxes[i].value);
    }

    return valores;
}