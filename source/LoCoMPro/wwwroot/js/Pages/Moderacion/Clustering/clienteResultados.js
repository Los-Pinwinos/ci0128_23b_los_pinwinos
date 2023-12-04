// Paginar
function paginar(numeroPagina = resultadosPagina.IndicePagina) {
    return paginador.paginar(resultados, numeroPagina);
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
    if (paginaFinal < resultadosPagina.PaginasTotales) {
        const finalElipsis = document.createElement("span");
        // Agregar ... a la ultima pagina si fuera necesario

        if (paginaFinal !== resultadosPagina.PaginasTotales - 1) finalElipsis.textContent = " ... ";
        else finalElipsis.textContent = " ";
        paginacionContenedor.appendChild(finalElipsis);

        const linkUltimaPagina = document.createElement("span");
        linkUltimaPagina.textContent = resultadosPagina.PaginasTotales;
        linkUltimaPagina.classList.add("pagina-seleccionable");

        linkUltimaPagina.addEventListener("click", function () {
            pasarPagina(resultadosPagina.PaginasTotales);
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

    let paginaInicial = Math.max(1, resultadosPagina.IndicePagina - Math.floor(numeroDeLinksDePaginas / 2));
    let paginaFinal = Math.min(resultadosPagina.PaginasTotales, paginaInicial + numeroDeLinksDePaginas - 1);

    if (paginaFinal - paginaInicial + 1 < numeroDeLinksDePaginas) {
        paginaInicial = Math.max(1, paginaFinal - numeroDeLinksDePaginas + 1);
    }

    for (let pagina = paginaInicial; pagina <= paginaFinal; pagina++) {
        const paginaLink = document.createElement("span");
        paginaLink.textContent = pagina;

        if (pagina === resultadosPagina.IndicePagina) {
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

    if (resultadosPagina.TienePaginaPrevia) {
        botonPaginaPrevia.style.display = "block";
        botonSinPaginaPrevia.style.display = "none";
    } else {
        botonPaginaPrevia.style.display = "none";
        botonSinPaginaPrevia.style.display = "block";
    }

    if (resultadosPagina.TieneProximaPagina) {
        botonPaginaSiguiente.style.display = "block";
        botonSinPaginaSiguiente.style.display = "none";
    } else {
        botonPaginaSiguiente.style.display = "none";
        botonSinPaginaSiguiente.style.display = "block";
    }
}

function pasarPagina(numeroPagina) {
    if (paginacionHabilitada) {
        // Paginar
        resultadosPagina = paginar(numeroPagina);
        // Renderizar
        renderizarPaginacion();
        renderizarTabla(resultadosPagina);
        window.scrollTo(0, 0);
    }
}

function crearCeldaContenido(contenido, clase) {
    const divContenido = document.createElement("div");
    divContenido.className = clase;
    divContenido.textContent = contenido;

    const celda = document.createElement("td");
    celda.setAttribute('data-tooltip', contenido);
    celda.appendChild(divContenido);

    return celda;
}

var filasEliminar = [];
function crearCheckbox(actual) {
    var checkboxDiv = document.createElement("div");
    checkboxDiv.className = "contenidoCeldaCheckbox";
    var checkbox = document.createElement("input");
    checkbox.type = "checkbox";
    checkbox.id = 'checkbox_agrupado_' + resultadosPagina.IndicePagina + '_' + actual;

    checkbox.checked = (localStorage.getItem(checkbox.id) === 'true');

    checkbox.addEventListener('click', function (evento) {
        var checkboxCambiado = evento.target;
        var filaAEliminar = checkboxCambiado.closest("tr");
        if (checkboxCambiado.checked) {
            // Hay que agregarlo
            filasEliminar.push(filaAEliminar);
            localStorage.setItem(checkboxCambiado.id, 'true');
        } else {
            // Hay que borrarlo
            filasEliminar = filasEliminar.filter(fila => fila.id !== filaAEliminar.id);
            localStorage.removeItem(checkboxCambiado.id);
        }
    });

    var checkboxCelda = document.createElement("td");
    checkboxCelda.classList.add('no-hover');
    checkboxDiv.appendChild(checkbox);
    checkboxCelda.appendChild(checkboxDiv);

    return checkboxCelda;
}

function renderizarTabla(resultados) {
    var tabla = document.getElementById("CuerpoResultados");
    tabla.innerHTML = "";

    for (var actual in resultados) {
        if (resultados[actual] != null && typeof resultados[actual].nombreProducto !== 'undefined' && resultados[actual].nombreProducto !== "") {
            var fila = document.createElement("tr");
            fila.classList.add("result-row");


            var checkbox = crearCheckbox(actual);
            var nombre = crearCeldaContenido(resultados[actual].nombreProducto, "contenidoCeldaProducto");
            var categoria = crearCeldaContenido(resultados[actual].nombreCategoria, "contenidoCeldaCategoria");
            var marca = crearCeldaContenido(resultados[actual].nombreMarca, "contenidoCeldaMarca");
            var unidad = crearCeldaContenido(resultados[actual].unidad, "contenidoCeldaUnidad");

            fila.appendChild(checkbox);
            fila.appendChild(nombre);
            fila.appendChild(categoria);
            fila.appendChild(marca);
            fila.appendChild(unidad);
            tabla.appendChild(fila);
        }
    }
}
