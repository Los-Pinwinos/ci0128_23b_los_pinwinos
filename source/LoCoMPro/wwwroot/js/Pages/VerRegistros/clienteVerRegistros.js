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
    var cuerpoTabla = document.getElementById("cuerpoResultados");

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
    resultadosPaginados = paginar(numeroPagina);
    // Renderizar
    renderizarPaginacion();
    renderizarTabla(resultadosPaginados);
    window.scrollTo(0, 0);
}


// Renderizar paginado
function renderizarPaginacion() {
    // Obtener elementos
    var botonPaginaPrevia = document.getElementById("paginaPrevia");
    var botonSinPaginaPrevia = document.getElementById("sinPaginaPrevia");
    var botonPaginaSiguiente = document.getElementById("paginaSiguiente");
    var botonSinPaginaSiguiente = document.getElementById("sinPaginaSiguiente");
    var textoPaginaActual = document.getElementById("textoPaginacion");

    // Poner pagina actual
    textoPaginaActual.textContent = resultadosPaginados.IndicePagina;

    // Revisar si desplegar o no
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