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
        // result-row se utiliza para redirigir los datos de la fila selecionada a la página de VerRegistros
        row.classList.add("result-row")

        if (typeof datos[dato].nombre !== 'undefined' && datos[dato].nombre !== "") {
            var divNombre = document.createElement("div");
            divNombre.className = "contenidoCeldaNombre";
            divNombre.textContent = datos[dato].nombre;
            var nombreCelda = document.createElement("td");
            nombreCelda.setAttribute('data-tooltip', datos[dato].nombre);
            nombreCelda.appendChild(divNombre);

            var nombreCell = document.createElement("td");
            nombreCell.textContent = datos[dato].nombre;

            var divCategoria = document.createElement("div");
            divCategoria.className = "contenidoCelda";
            divCategoria.textContent = datos[dato].categoria;
            var categoriaCelda = document.createElement("td");
            categoriaCelda.setAttribute('data-tooltip', datos[dato].categoria);
            categoriaCelda.appendChild(divCategoria);

            var divMarca = document.createElement("div");
            divMarca.className = "contenidoCelda";
            divMarca.textContent = datos[dato].marca;
            var marcaCelda = document.createElement("td");
            marcaCelda.setAttribute('data-tooltip', datos[dato].marca);
            marcaCelda.appendChild(divMarca);

            var divPrecio = document.createElement("div");
            divPrecio.className = "contenidoCelda";
            divPrecio.textContent = "₡" + datos[dato].precio;
            var precioCelda = document.createElement("td");
            precioCelda.setAttribute('data-tooltip', datos[dato].precio);
            precioCelda.appendChild(divPrecio);

            var divUnidad = document.createElement("div");
            divUnidad.className = "contenidoCelda";
            divUnidad.textContent = datos[dato].unidad;
            var unidadCelda = document.createElement("td");
            unidadCelda.setAttribute('data-tooltip', datos[dato].unidad);
            unidadCelda.appendChild(divUnidad);

            var divFecha = document.createElement("div");
            divFecha.className = "contenidoCelda";
            divFecha.textContent = formatearFecha(new Date(datos[dato].fecha));
            var fechaCelda = document.createElement("td");
            var contenidoFecha = datos[dato].fecha[8] + datos[dato].fecha[9] + "/"
                + datos[dato].fecha[5] + datos[dato].fecha[6] + "/" + datos[dato].fecha[0] + datos[dato].fecha[1]
                + datos[dato].fecha[2] + datos[dato].fecha[3];
            fechaCelda.setAttribute('data-tooltip', contenidoFecha);
            fechaCelda.appendChild(divFecha);

            var divTienda = document.createElement("div");
            divTienda.className = "contenidoCelda";
            divTienda.textContent = datos[dato].tienda;
            var tiendaCelda = document.createElement("td");
            tiendaCelda.setAttribute('data-tooltip', datos[dato].tienda);
            tiendaCelda.appendChild(divTienda);

            var divProvincia = document.createElement("div");
            divProvincia.className = "contenidoCelda";
            divProvincia.textContent = datos[dato].provincia;
            var provinciaCelda = document.createElement("td");
            provinciaCelda.setAttribute('data-tooltip', datos[dato].provincia);
            provinciaCelda.appendChild(divProvincia);

            var divCanton = document.createElement("div");
            divCanton.className = "contenidoCelda";
            divCanton.textContent = datos[dato].canton;
            var cantonCelda = document.createElement("td");
            cantonCelda.setAttribute('data-tooltip', datos[dato].canton);
            cantonCelda.appendChild(divCanton);

            // Agregar celdas a fila
            row.appendChild(nombreCelda);
            row.appendChild(categoriaCelda);
            row.appendChild(marcaCelda);
            row.appendChild(precioCelda);
            row.appendChild(unidadCelda);
            row.appendChild(fechaCelda);
            row.appendChild(tiendaCelda);
            row.appendChild(provinciaCelda);
            row.appendChild(cantonCelda);

            // Agregar celdas cuerpo
            cuerpoTabla.appendChild(row);
        }
    }
}


// Redirecionar a ver registros
document.addEventListener("DOMContentLoaded", function () {
    // Indica que, a la hora de seleccionar una columna, esta redirigirá la búsqueda a la página de ver registros
    // Para el producto indicado.
    const rows = document.querySelectorAll(".result-row");

    rows.forEach(row => {
    row.addEventListener("click", function () {
        const productName = row.querySelector("td:first-child").textContent;
        window.location.href = `/VerRegistros/VerRegistros?productName=${encodeURIComponent(productName)}`;
    });
    });
});

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