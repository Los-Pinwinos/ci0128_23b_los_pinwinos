function agregarSeparador(numero) {
    var textoNum = numero.toString();
    // Expresión regular para agregar un separador cada 3 dígitos
    textoNum = textoNum.replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return textoNum;
}

// Paginar
function paginar(numeroPagina = favoritosVM.IndicePagina) {
    return paginador.paginar(resultados, numeroPagina);
}

function ordenar(propiedadOrdenado) {
    // Configurar ordenador
    if (propiedadOrdenado === ordenador.ordenado) {
        ordenador.cambiarSentido();
    } else {
        ordenador.setPropiedadOrdenada(propiedadOrdenado);
        ordenador.setSentidoOrdenado('asc');
    }
    resultados = ordenador.ordenar(resultados);
    console.log(resultados);

    favoritosVM = paginar();

    renderizarPaginacion();
    renderizarTabla(favoritosVM);
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
    if (paginaFinal < favoritosVM.PaginasTotales) {
        const finalElipsis = document.createElement("span");
        // Agregar ... a la ultima pagina si fuera necesario

        if (paginaFinal !== favoritosVM.PaginasTotales - 1) finalElipsis.textContent = " ... ";
        else finalElipsis.textContent = " ";
        paginacionContenedor.appendChild(finalElipsis);

        const linkUltimaPagina = document.createElement("span");
        linkUltimaPagina.textContent = favoritosVM.PaginasTotales;
        linkUltimaPagina.classList.add("pagina-seleccionable");

        linkUltimaPagina.addEventListener("click", function () {
            pasarPagina(favoritosVM.PaginasTotales);
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

    let paginaInicial = Math.max(1, favoritosVM.IndicePagina - Math.floor(numeroDeLinksDePaginas / 2));
    let paginaFinal = Math.min(favoritosVM.PaginasTotales, paginaInicial + numeroDeLinksDePaginas - 1);

    if (paginaFinal - paginaInicial + 1 < numeroDeLinksDePaginas) {
        paginaInicial = Math.max(1, paginaFinal - numeroDeLinksDePaginas + 1);
    }

    for (let pagina = paginaInicial; pagina <= paginaFinal; pagina++) {
        const paginaLink = document.createElement("span");
        paginaLink.textContent = pagina;

        if (pagina === favoritosVM.IndicePagina) {
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

    if (favoritosVM.TienePaginaPrevia) {
        botonPaginaPrevia.style.display = "block";
        botonSinPaginaPrevia.style.display = "none";
    } else {
        botonPaginaPrevia.style.display = "none";
        botonSinPaginaPrevia.style.display = "block";
    }

    if (favoritosVM.TieneProximaPagina) {
        botonPaginaSiguiente.style.display = "block";
        botonSinPaginaSiguiente.style.display = "none";
    } else {
        botonPaginaSiguiente.style.display = "none";
        botonSinPaginaSiguiente.style.display = "block";
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

function agregarFilaALaTabla(cuerpoTabla, datos) {
    const row = document.createElement("tr");
    row.classList.add("result-row");

    if (typeof datos.nombreTienda !== 'undefined' && datos.nombreTienda !== "") {
        const tiendaCelda = crearCeldaContenido(datos.nombreTienda, "contenidoCeldaTienda");
        const provinciaCelda = crearCeldaContenido(datos.nombreProvincia, "contenidoCeldaProvincia");
        const cantonCelda = crearCeldaContenido(datos.nombreCanton, "contenidoCeldaCanton");

        const cantidadArreglada = `${datos.cantidadEncontrada} (${parseFloat(datos.porcentajeEncontrado.toFixed(2))}%)`;
        const cantidadCelda = crearCeldaContenido(cantidadArreglada, "contenidoCeldaCantidad");
        const precioArreglado = "₡" + agregarSeparador(parseFloat(datos.precioTotal.toFixed(2)));
        const precioCelda = crearCeldaContenido(precioArreglado, "contenidoCeldaPrecio");
        const distanciaArreglada = `${datos.distanciaTotal.toFixed(2).replace('.', ',')}km`;
        const distanciaCelda = crearCeldaContenido(distanciaArreglada, "contenidoCeldaDistancia");

        row.appendChild(tiendaCelda);
        row.appendChild(provinciaCelda);
        row.appendChild(cantonCelda);
        row.appendChild(cantidadCelda);
        row.appendChild(precioCelda);
        row.appendChild(distanciaCelda);

        cuerpoTabla.appendChild(row);

        // Redirecciona a ver los productos de la tienda
        (function (tienda, provincia, canton, distrito, precio, distancia) {
            row.addEventListener("click", function () {
                window.location.href = `/Busqueda/Favoritos/VerProductos?&nombreTienda=${encodeURIComponent(tienda)}\&nombreProvincia=${encodeURIComponent(provincia)}&nombreCanton=${encodeURIComponent(canton)}&nombreDistrito=${encodeURIComponent(distrito)}&precioTotal=${encodeURIComponent(precio)}&distanciaTotal=${encodeURIComponent(distancia)}`;
            });
        })(datos.nombreTienda, datos.nombreProvincia, datos.nombreCanton, datos.nombreDistrito,
            agregarSeparador(parseFloat(datos.precioTotal.toFixed(2))), datos.distanciaTotal.toFixed(2).replace('.', ','));
    }
}

// Renderizar tabla
function renderizarTabla(datos) {
    // Obtener la tabla
    var cuerpoTabla = document.getElementById("CuerpoResultados");

    // Limpiar el contenido existente
    cuerpoTabla.innerHTML = "";

    datos.forEach(function (dato) {
        agregarFilaALaTabla(cuerpoTabla, dato);
    });
}

// Pasar pagina
function pasarPagina(numeroPagina) {
    // Paginar
    favoritosVM = paginar(numeroPagina);
    // Renderizar
    renderizarPaginacion();
    renderizarTabla(favoritosVM);
    window.scrollTo(0, 0);
}