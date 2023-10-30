function agregarSeparador(numero) {
    var textoNum = numero.toString();
    // Expresión regular para agregar un separador cada 3 dígitos
    textoNum = textoNum.replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return textoNum;
}

// Formatear fecha
function formatearFecha(datos) {
    var contenidoFecha = datos.creacion[8] + datos.creacion[9] + "/"
        + datos.creacion[5] + datos.creacion[6] + "/" + datos.creacion[0] + datos.creacion[1]
        + datos.creacion[2] + datos.creacion[3];
    return contenidoFecha
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

        if (typeof datos[dato].creacion !== 'undefined' && datos[dato].creacion !== "") {

            var divFecha = document.createElement("div");
            divFecha.className = "contenidoCeldaFecha";
            divFecha.textContent = formatearFecha(datos[dato]);
            var fechaCelda = document.createElement("td");           
            fechaCelda.setAttribute('data-tooltip', divFecha.textContent);
            fechaCelda.appendChild(divFecha);

            var divPrecio = document.createElement("div");
            divPrecio.className = "contenidoCeldaPrecio";
            var precioArreglado = "₡" + agregarSeparador(parseFloat(datos[dato].precio));
            divPrecio.textContent = precioArreglado;
            divPrecio.classList.add("precio");
            var precioCelda = document.createElement("td");
            precioCelda.classList.add("precio");
            precioCelda.setAttribute('data-tooltip', precioArreglado);
            precioCelda.appendChild(divPrecio);

            var divCalificacion = document.createElement("div");
            divCalificacion.className = "contenidoCeldaCalificacion";
            divCalificacion.textContent = datos[dato].calificacion == null? "Sin calificar": datos[dato].calificacion;
            var calificacionCelda = document.createElement("td");
            calificacionCelda.setAttribute('data-tooltip', divCalificacion.textContent);
            calificacionCelda.appendChild(divCalificacion);

            var descripcionCelda = document.createElement("td");
            descripcionCelda.textContent = datos[dato].descripcion;
            
            // Agregar celdas a fila
            row.appendChild(fechaCelda);
            row.appendChild(precioCelda);
            row.appendChild(calificacionCelda);
            row.appendChild(descripcionCelda);

            // Agregar celdas cuerpo
            cuerpoTabla.appendChild(row);

            // Variables para redireccionar
            var fechaHoraEnviar = datos[dato].creacion;
            var usuarioEnviar = datos[dato].usuarioCreador;

            // Redirecciona a Detalles Registro
            (function(fechaHora, usuario) {
                row.addEventListener("click", function () {
                    var producto = document.getElementById("nombreProducto").value;
                    window.location.href = `/detallesRegistro/detallesRegistro?fechaHora=${encodeURIComponent(fechaHora)}&usuario=${encodeURIComponent(usuario)}&producto=${encodeURIComponent(producto)}`;
                });
            })(fechaHoraEnviar, usuarioEnviar);
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


function renderizarPaginacion() {
    // Renderizar los botones de Siguiente Pagina y Pagina Anterior
    renderizarBotonesSiguienteAnterior();

    var paginacionContenedor = document.getElementById("TextoPaginacion");

    // Renderizar los numeros de en medio mostrados en la barra de paginacion
    renderizarNumerosPaginaIntermedios(paginacionContenedor);
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

// Paginar
function paginar(numeroPagina = productosVM.IndicePagina) {
    return paginador.paginar(resultados, numeroPagina);
}