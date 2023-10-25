﻿
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
            divFecha.className = "contenidoCelda";
            divFecha.textContent = formatearFecha(datos[dato]);
            var fechaCelda = document.createElement("td");           
            fechaCelda.setAttribute('data-tooltip', divFecha.textContent);
            fechaCelda.appendChild(divFecha);

            var divPrecio = document.createElement("div");
            divPrecio.className = "contenidoCelda";
            divPrecio.textContent = "₡" + datos[dato].precio;
            divPrecio.classList.add("precio");
            var precioCelda = document.createElement("td");
            precioCelda.classList.add("precio");
            precioCelda.setAttribute('data-tooltip', datos[dato].precio);
            precioCelda.appendChild(divPrecio);


            var divCalificacion = document.createElement("div");
            divCalificacion.className = "contenidoCelda";
            divCalificacion.textContent = datos[dato].calificacion;
            var calificacionCelda = document.createElement("td");
            calificacionCelda.setAttribute('data-tooltip', datos[dato].calificacion);
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

// Paginar
function paginar(numeroPagina = productosVM.IndicePagina) {
    return paginador.paginar(resultados, numeroPagina);
}