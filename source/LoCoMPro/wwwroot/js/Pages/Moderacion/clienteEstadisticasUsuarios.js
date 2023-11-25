// Formatear la calificación para redondear, usar comas y tener solo un decimal
function formatearCalificacion(calificacion) {
    var redondeado = Math.floor(calificacion * 10) / 10;
    return redondeado.toLocaleString("es", { minimumFractionDigits: 1 });
}


function renderizarTabla(datos) {

    var cuerpoTabla = document.getElementById("cuerpoResultados");

    cuerpoTabla.innerHTML = "";

    for (var dato in datos) {
        var fila = document.createElement("tr");
        if (typeof datos[dato].creacion !== 'undefined' && datos[dato].creacion !== "") {

            var divUsuario = document.createElement("div");
            divUsuario.className = "contenidoCeldaUsuario";
            divUsuario.textContent = formatearFecha(datos[dato]);
            var usuarioCelda = document.createElement("td");
            usuarioCelda.setAttribute('data-tooltip', divUsuario.textContent);
            usuarioCelda.appendChild(divUsuario);

            var divCalificacion = document.createElement("div");
            divCalificacion.className = "contenidoCeldaNumero";
            divCalificacion.textContent = datos[dato].calificacion == 0 ? "Sin calificar" : formatearCalificacion(datos[dato].calificacion);
            divCalificacion.style.marginLeft = "60px";
            var calificacionCelda = document.createElement("td");
            calificacionCelda.setAttribute('data-tooltip', divCalificacion.textContent);
            calificacionCelda.appendChild(divCalificacion);

            var divContribuciones = document.createElement("div");
            divContribuciones.className = "contenidoCeldaNumero";
            divContribuciones.textContent = formatearFecha(datos[dato]);
            var contribucionesCelda = document.createElement("td");
            contribucionesCelda.setAttribute('data-tooltip', divContribuciones.textContent);
            contribucionesCelda.appendChild(divContribuciones);

            var divRealizados = document.createElement("div");
            divRealizados.className = "contenidoCeldaNumero";
            divRealizados.textContent = formatearFecha(datos[dato]);
            var realizadosCelda = document.createElement("td");
            realizadosCelda.setAttribute('data-tooltip', divRealizados.textContent);
            realizadosCelda.appendChild(divRealizados);

            var divAceptados = document.createElement("div");
            divAceptados.className = "contenidoCeldaNumero";
            divAceptados.textContent = formatearFecha(datos[dato]);
            var aceptadosCelda = document.createElement("td");
            aceptadosCelda.setAttribute('data-tooltip', divAceptados.textContent);
            aceptadosCelda.appendChild(divAceptados);


            fila.appendChild(usuarioCelda);
            fila.appendChild(calificacionCelda);
            fila.appendChild(contribucionesCelda);
            fila.appendChild(realizadosCelda);
            fila.appendChild(aceptadosCelda);

            cuerpoTabla.appendChild(fila);
        }
    }
}





function pasarPagina(numeroPagina) {
    resultadosPaginados = paginar(numeroPagina);
    renderizarPaginacion();
    renderizarTabla(resultadosPaginados);
    
    window.scrollTo(0, 0);
}


function renderizarPaginacion() {
    // Renderizar los botones de Siguiente Pagina y Pagina Anterior
    renderizarBotonesSiguienteAnterior();

    var paginacionContenedor = document.getElementById("textoPaginacion");

    // Renderizar los numeros de en medio mostrados en la barra de paginacion
    var limites = renderizarNumerosPaginaIntermedios(paginacionContenedor);
    renderizarPrimerNumeroPagina(paginacionContenedor, limites.paginaInicial);
    renderizarUltimoNumeroPagina(paginacionContenedor, limites.paginaFinal);
}

function renderizarUltimoNumeroPagina(paginacionContenedor, paginaFinal) {
    if (paginaFinal < resultadosPaginados.PaginasTotales) {
        const finalElipsis = document.createElement("span");
        // Agregar ... a la ultima pagina si fuera necesario

        if (paginaFinal !== resultadosPaginados.PaginasTotales - 1) finalElipsis.textContent = " ... ";
        else finalElipsis.textContent = " ";
        paginacionContenedor.appendChild(finalElipsis);

        const linkUltimaPagina = document.createElement("span");
        linkUltimaPagina.textContent = resultadosPaginados.PaginasTotales;
        linkUltimaPagina.classList.add("pagina-seleccionable");

        linkUltimaPagina.addEventListener("click", function () {
            pasarPagina(resultadosPaginados.PaginasTotales);
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

    let paginaInicial = Math.max(1, resultadosPaginados.IndicePagina - Math.floor(numeroDeLinksDePaginas / 2));
    let paginaFinal = Math.min(resultadosPaginados.PaginasTotales, paginaInicial + numeroDeLinksDePaginas - 1);

    if (paginaFinal - paginaInicial + 1 < numeroDeLinksDePaginas) {
        paginaInicial = Math.max(1, paginaFinal - numeroDeLinksDePaginas + 1);
    }

    for (let pagina = paginaInicial; pagina <= paginaFinal; pagina++) {
        const paginaLink = document.createElement("span");
        paginaLink.textContent = pagina;

        if (pagina === resultadosPaginados.IndicePagina) {
            paginaLink.classList.add("pagina");
        } else {
            paginaLink.classList.add("pagina-seleccionable");
            paginaLink.addEventListener("click", function () {
                pasarPagina(pagina, presionado);
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
    var botonPaginaPrevia = document.getElementById("paginaPrevia");
    var botonSinPaginaPrevia = document.getElementById("sinPaginaPrevia");
    var botonPaginaSiguiente = document.getElementById("paginaSiguiente");
    var botonSinPaginaSiguiente = document.getElementById("sinPaginaSiguiente");

    if (resultadosPaginados.TienePaginaPrevia) {
        botonPaginaPrevia.style.display = "block";
        botonSinPaginaPrevia.style.display = "none";
    } else {
        botonPaginaPrevia.style.display = "none";
        botonSinPaginaPrevia.style.display = "block";
    }

    if (resultadosPaginados.TieneProximaPagina) {
        botonPaginaSiguiente.style.display = "block";
        botonSinPaginaSiguiente.style.display = "none";
    } else {
        botonPaginaSiguiente.style.display = "none";
        botonSinPaginaSiguiente.style.display = "block";
    }
}

// Paginar
function paginar(numeroPagina = resultadosPaginados.IndicePagina) {
    return paginador.paginar(resultados, numeroPagina);
}