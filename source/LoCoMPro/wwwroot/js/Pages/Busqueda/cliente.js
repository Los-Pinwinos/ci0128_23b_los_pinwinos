function agregarSeparador(numero) {
    var textoNum = numero.toString();
    // Expresión regular para agregar un separador cada 3 dígitos
    textoNum = textoNum.replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return textoNum;
}

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

// Configurar formato de precio para el filtro
function formatoPrecioFiltro() {
    // Crea un evento al escribir en la caja de texto de precio
    document.getElementById("precioMin").addEventListener("input", function (event) {
        // Reemplaza el valor de la caja de texto siguiendo esa regex
        event.target.value = event.target.value.replace(/[^\d,]|(,{2,})|(^,)|(^0+[0-9,]+)/g, "");
    });
    // Crea un evento al escribir en la caja de texto de precio
    document.getElementById("precioMax").addEventListener("input", function (event) {
        // Reemplaza el valor de la caja de texto siguiendo esa regex
        event.target.value = event.target.value.replace(/[^\d,]|(,{2,})|(^,)|(^0+[0-9,]+)/g, "");
    });
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

function renderizarPaginacion() {
    // Renderizar los botones de Siguiente Pagina y Pagina Anterior
    renderizarBotonesSiguienteAnterior();

    var paginacionContenedor = document.getElementById("TextoPaginacion");

    // Renderizar los numeros de en medio mostrados en la barra de paginacion
    var limites = renderizarNumerosPaginaIntermedios(paginacionContenedor);
    renderizarPrimerNumeroPagina(paginacionContenedor, limites.paginaInicial);
    renderizarUltimoNumeroPagina(paginacionContenedor, limites.paginaFinal);
}

function renderizarUltimoNumeroPagina(paginacionContenedor, paginaFinal) {
    if (paginaFinal < productosVM.PaginasTotales) {
        const finalElipsis = document.createElement("span");
        // Agregar ... a la ultima pagina si fuera necesario

        if (paginaFinal !== productosVM.PaginasTotales - 1) finalElipsis.textContent = " ... ";
        else finalElipsis.textContent = " ";
        paginacionContenedor.appendChild(finalElipsis);

        const linkUltimaPagina = document.createElement("span");
        linkUltimaPagina.textContent = productosVM.PaginasTotales;
        linkUltimaPagina.classList.add("pagina-seleccionable");

        linkUltimaPagina.addEventListener("click", function () {
            pasarPagina(productosVM.PaginasTotales);
        });
        paginacionContenedor.appendChild(linkUltimaPagina);
    }
}

function renderizarPrimerNumeroPagina(paginacionContenedor, paginaInicial) {
    if (paginaInicial > 1) {
        const inicioElipsis = document.createElement("span");

        // Agregar ... a la primera pagina si fuera necesario
        if (paginaInicial !== 2) inicioElipsis.textContent = " ... ";
        else inicioElipsis.textContent = " ";

        paginacionContenedor.insertBefore(inicioElipsis, paginacionContenedor.firstChild);

        const linkPrimeraPagina = document.createElement("span");
        linkPrimeraPagina.textContent = "1";
        linkPrimeraPagina.classList.add("pagina-seleccionable");

        linkPrimeraPagina.addEventListener("click", function () {
            pasarPagina(1);
        });
        paginacionContenedor.insertBefore(linkPrimeraPagina, inicioElipsis);
    }
}

function renderizarNumerosPaginaIntermedios(paginacionContenedor) {

    paginacionContenedor.innerHTML = "";

    const numeroDeLinksDePaginas = 5;

    let paginaInicial = Math.max(1, productosVM.IndicePagina - Math.floor(numeroDeLinksDePaginas / 2));
    let paginaFinal = Math.min(productosVM.PaginasTotales, paginaInicial + numeroDeLinksDePaginas - 1);

    if (paginaFinal - paginaInicial + 1 < numeroDeLinksDePaginas) {
        paginaInicial = Math.max(1, paginaFinal - numeroDeLinksDePaginas + 1);
    }

    for (let pagina = paginaInicial; pagina <= paginaFinal; pagina++) {
        const paginaLink = document.createElement("span");
        paginaLink.textContent = pagina;

        if (pagina === productosVM.IndicePagina) {
            paginaLink.classList.add("pagina");
        } else {
            paginaLink.classList.add("pagina-seleccionable");
            paginaLink.addEventListener("click", function () {
                pasarPagina(pagina);
            });
        }

        paginacionContenedor.appendChild(paginaLink);
        if (pagina !== paginaFinal) {
            var espacio = document.createElement("span");
            espacio.textContent = " ";
            paginacionContenedor.appendChild(espacio);
        }
    }

    return { paginaInicial, paginaFinal };
}

function renderizarBotonesSiguienteAnterior() {
    var botonPaginaPrevia = document.getElementById("PaginaPrevia");
    var botonSinPaginaPrevia = document.getElementById("SinPaginaPrevia");
    var botonPaginaSiguiente = document.getElementById("PaginaSiguiente");
    var botonSinPaginaSiguiente = document.getElementById("SinPaginaSiguiente");

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
        checkboxLabel.style.marginTop = "30px";
        checkboxLabel.style.marginBottom = "-8px";

        // Append the modified checkboxLabel to your element (filtros)
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
            divCategoria.className = "contenidoCeldaCategoria";
            divCategoria.textContent = datos[dato].categoria;
            var categoriaCelda = document.createElement("td");
            categoriaCelda.setAttribute('data-tooltip', datos[dato].categoria);
            categoriaCelda.appendChild(divCategoria);

            var divMarca = document.createElement("div");
            divMarca.className = "contenidoCeldaMarca";
            divMarca.textContent = datos[dato].marca;
            var marcaCelda = document.createElement("td");
            marcaCelda.setAttribute('data-tooltip', datos[dato].marca);
            marcaCelda.appendChild(divMarca);

            var divPrecio = document.createElement("div");
            divPrecio.className = "contenidoCeldaPrecio";
            var precioArreglado = "₡" + agregarSeparador(parseFloat(datos[dato].precio));
            divPrecio.textContent = precioArreglado;
            var precioCelda = document.createElement("td");
            precioCelda.setAttribute('data-tooltip', precioArreglado);
            precioCelda.appendChild(divPrecio);

            var divUnidad = document.createElement("div");
            divUnidad.className = "contenidoCeldaUnidad";
            divUnidad.textContent = datos[dato].unidad;
            var unidadCelda = document.createElement("td");
            unidadCelda.setAttribute('data-tooltip', datos[dato].unidad);
            unidadCelda.appendChild(divUnidad);

            var divFecha = document.createElement("div");
            divFecha.className = "contenidoCeldaFecha";
            divFecha.textContent = formatearFecha(new Date(datos[dato].fecha));
            var fechaCelda = document.createElement("td");
            var contenidoFecha = datos[dato].fecha[8] + datos[dato].fecha[9] + "/"
                + datos[dato].fecha[5] + datos[dato].fecha[6] + "/" + datos[dato].fecha[0] + datos[dato].fecha[1]
                + datos[dato].fecha[2] + datos[dato].fecha[3];
            fechaCelda.setAttribute('data-tooltip', contenidoFecha);
            fechaCelda.appendChild(divFecha);

            var divTienda = document.createElement("div");
            divTienda.className = "contenidoCeldaTienda";
            divTienda.textContent = datos[dato].tienda;
            var tiendaCelda = document.createElement("td");
            tiendaCelda.setAttribute('data-tooltip', datos[dato].tienda);
            tiendaCelda.appendChild(divTienda);

            var divProvincia = document.createElement("div");
            divProvincia.className = "contenidoCeldaProvincia";
            divProvincia.textContent = datos[dato].provincia;
            var provinciaCelda = document.createElement("td");
            provinciaCelda.setAttribute('data-tooltip', datos[dato].provincia);
            provinciaCelda.appendChild(divProvincia);

            var divCanton = document.createElement("div");
            divCanton.className = "contenidoCeldaCanton";
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

document.addEventListener("click", function () {
    const rows = document.querySelectorAll(".result-row");

    rows.forEach(row => {
        row.addEventListener("click", function () {
            const productName = row.querySelector("td:nth-child(1)").textContent;      // Columna 1
            const categoriaNombre = row.querySelector("td:nth-child(2)").textContent;  // Columna 2
            const marcaNombre = row.querySelector("td:nth-child(3)").textContent;      // Columna 3
            const unidadNombre = row.querySelector("td:nth-child(5)").textContent;     // Columna 5
            const tiendaNombre = row.querySelector("td:nth-child(7)").textContent;     // Columna 7
            const provinciaNombre = row.querySelector("td:nth-child(8)").textContent;  // Columna 8
            const cantonNombre = row.querySelector("td:nth-child(9)").textContent;     // Columna 9

            window.location.href = `/VerRegistros/VerRegistros?productName=${encodeURIComponent(productName)}&categoriaNombre=${encodeURIComponent(categoriaNombre)}&marcaNombre=${encodeURIComponent(marcaNombre)}&unidadNombre=${encodeURIComponent(unidadNombre)}&tiendaNombre=${encodeURIComponent(tiendaNombre)}&provinciaNombre=${encodeURIComponent(provinciaNombre)}&cantonNombre=${encodeURIComponent(cantonNombre)}`;
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

// Limpiar input de precio
function limpiarInputPrecio() {
    var inputMinimo = document.getElementById("precioMin");
    var inputMaximo = document.getElementById("precioMax");
    inputMinimo.value = "";
    inputMaximo.value = "";
}

// Limpiar filtross
function limpiarFiltros() {
    // Limpiar checkboxes
    limpiarCheckboxes("provincia");
    limpiarCheckboxes("canton");
    limpiarCheckboxes("tienda");
    limpiarCheckboxes("marca");
    limpiarInputPrecio();

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