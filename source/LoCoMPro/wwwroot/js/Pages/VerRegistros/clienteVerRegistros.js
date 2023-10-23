
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
