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

// Formatear la calificación para redondear, usar comas y tener solo un decimal
function formatearCalificacion(calificacion) {
    var redondeado = Math.floor(calificacion * 10) / 10;
    return redondeado.toLocaleString("es", { minimumFractionDigits: 1 });
}

// Paginar
function paginar(numeroPagina = aportesVM.IndicePagina) {
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
    if (paginaFinal < aportesVM.PaginasTotales) {
        const finalElipsis = document.createElement("span");
        // Agregar ... a la ultima pagina si fuera necesario

        if (paginaFinal !== aportesVM.PaginasTotales - 1) finalElipsis.textContent = " ... ";
        else finalElipsis.textContent = " ";
        paginacionContenedor.appendChild(finalElipsis);

        const linkUltimaPagina = document.createElement("span");
        linkUltimaPagina.textContent = aportesVM.PaginasTotales;
        linkUltimaPagina.classList.add("pagina-seleccionable");

        linkUltimaPagina.addEventListener("click", function () {
            pasarPagina(aportesVM.PaginasTotales);
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

    let paginaInicial = Math.max(1, aportesVM.IndicePagina - Math.floor(numeroDeLinksDePaginas / 2));
    let paginaFinal = Math.min(aportesVM.PaginasTotales, paginaInicial + numeroDeLinksDePaginas - 1);

    if (paginaFinal - paginaInicial + 1 < numeroDeLinksDePaginas) {
        paginaInicial = Math.max(1, paginaFinal - numeroDeLinksDePaginas + 1);
    }

    for (let pagina = paginaInicial; pagina <= paginaFinal; pagina++) {
        const paginaLink = document.createElement("span");
        paginaLink.textContent = pagina;

        if (pagina === aportesVM.IndicePagina) {
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

    if (aportesVM.TienePaginaPrevia) {
        botonPaginaPrevia.style.display = "block";
        botonSinPaginaPrevia.style.display = "none";
    } else {
        botonPaginaPrevia.style.display = "none";
        botonSinPaginaPrevia.style.display = "block";
    }

    if (aportesVM.TieneProximaPagina) {
        botonPaginaSiguiente.style.display = "block";
        botonSinPaginaSiguiente.style.display = "none";
    } else {
        botonPaginaSiguiente.style.display = "none";
        botonSinPaginaSiguiente.style.display = "block";
    }
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

        if (typeof datos[dato].producto !== 'undefined' && datos[dato].producto !== "") {
            var divFecha = document.createElement("div");
            divFecha.className = "contenidoCeldaFecha";
            divFecha.textContent = formatearFecha(new Date(datos[dato].fecha));
            var fechaCelda = document.createElement("td");
            var contenidoFecha = datos[dato].fecha[8] + datos[dato].fecha[9] + "/"
                + datos[dato].fecha[5] + datos[dato].fecha[6] + "/" + datos[dato].fecha[0] + datos[dato].fecha[1]
                + datos[dato].fecha[2] + datos[dato].fecha[3];
            fechaCelda.setAttribute('data-tooltip', contenidoFecha);
            fechaCelda.appendChild(divFecha);

            var divProducto = document.createElement("div");
            divProducto.className = "contenidoCeldaProducto";
            divProducto.textContent = datos[dato].producto;
            var productoCelda = document.createElement("td");
            productoCelda.setAttribute('data-tooltip', datos[dato].producto);
            productoCelda.appendChild(divProducto);

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

            var divCategoria = document.createElement("div");
            divCategoria.className = "contenidoCeldaCategoria";
            divCategoria.textContent = datos[dato].categoria;
            var categoriaCelda = document.createElement("td");
            categoriaCelda.setAttribute('data-tooltip', datos[dato].categoria);
            categoriaCelda.appendChild(divCategoria);

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

            var divCalificacion = document.createElement("div");
            divCalificacion.className = "contenidoCeldaCalificacion";
            divCalificacion.textContent = datos[dato].calificacion == 0 ? "Sin calificar" : formatearCalificacion(datos[dato].calificacion);
            var calificacionCelda = document.createElement("td");
            calificacionCelda.setAttribute('data-tooltip', divCalificacion.textContent);
            calificacionCelda.appendChild(divCalificacion);

            // Agregar celdas a fila
            row.appendChild(fechaCelda);
            row.appendChild(productoCelda);
            row.appendChild(precioCelda);
            row.appendChild(unidadCelda);
            row.appendChild(categoriaCelda);
            row.appendChild(tiendaCelda);
            row.appendChild(provinciaCelda);
            row.appendChild(cantonCelda);
            row.appendChild(calificacionCelda);
           
            // Agregar celdas cuerpo
            cuerpoTabla.appendChild(row);

            // Variables para redireccionar
            var fechaHoraEnviar = datos[dato].fecha;
            var producto = datos[dato].producto;

            // Redirecciona a Detalles Registro
            (function (fechaHora, producto) {
                row.addEventListener("click", function () {
                    var usuario = document.getElementById("NombreUsuario").value;
                    window.location.href = `/detallesRegistro/detallesRegistro?fechaHora=${encodeURIComponent(fechaHora)}&usuario=${encodeURIComponent(usuario)}&producto=${encodeURIComponent(producto)}`;
                });
            })(fechaHoraEnviar, producto);

        }
    }
}

// Pasar pagina
function pasarPagina(numeroPagina) {
    // Paginar
    aportesVM = paginar(numeroPagina);
    // Renderizar
    renderizarPaginacion();
    renderizarTabla(aportesVM);
    window.scrollTo(0, 0);
}