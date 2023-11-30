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
    const tienda = document.getElementById("Tienda").value;
    const provincia = document.getElementById("Provincia").value;
    const canton = document.getElementById("Canton").value;

    const row = document.createElement("tr");
    row.classList.add("result-row");

    if (typeof datos.producto !== 'undefined' && datos.producto !== "") {
        const productoCelda = crearCeldaContenido(datos.producto, "contenidoCeldaProducto");
        const precioArreglado = "₡" + agregarSeparador(parseFloat(datos.precio.toFixed(2)));
        const precioCelda = crearCeldaContenido(precioArreglado, "contenidoCeldaPrecio");

        const unidadCelda = crearCeldaContenido(datos.unidad, "contenidoCeldaUnidad");
        const categoriaCelda = crearCeldaContenido(datos.categoria, "contenidoCeldaCategoria");
        const marcaCelda = crearCeldaContenido(datos.marca, "contenidoCeldaMarca");


        row.appendChild(productoCelda);
        row.appendChild(precioCelda);
        row.appendChild(unidadCelda);
        row.appendChild(categoriaCelda);
        row.appendChild(marcaCelda);

        cuerpoTabla.appendChild(row);

        // Redirecciona a ver los productos de la tienda
        (function (productoNombre, categoriaNombre, marcaNombre, unidadNombre, tiendaNombre, provinciaNombre, cantonNombre) {
            row.addEventListener("click", function () {
                window.location.href = `/VerRegistros/VerRegistros?productoNombre=${encodeURIComponent(productoNombre)}&categoriaNombre=${encodeURIComponent(categoriaNombre)}&marcaNombre=${encodeURIComponent(marcaNombre)}&unidadNombre=${encodeURIComponent(unidadNombre)}&tiendaNombre=${encodeURIComponent(tiendaNombre)}&provinciaNombre=${encodeURIComponent(provinciaNombre)}&cantonNombre=${encodeURIComponent(cantonNombre)}`;
            });
        })(datos.producto, datos.categoria, datos.marca, datos.unidad, tienda, provincia, canton);
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