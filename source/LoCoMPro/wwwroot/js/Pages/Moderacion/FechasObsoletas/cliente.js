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
function paginar(numeroPagina = outliersVM.IndicePagina) {
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
    if (paginaFinal < outliersVM.PaginasTotales) {
        const finalElipsis = document.createElement("span");
        // Agregar ... a la ultima pagina si fuera necesario

        if (paginaFinal !== outliersVM.PaginasTotales - 1) finalElipsis.textContent = " ... ";
        else finalElipsis.textContent = " ";
        paginacionContenedor.appendChild(finalElipsis);

        const linkUltimaPagina = document.createElement("span");
        linkUltimaPagina.textContent = outliersVM.PaginasTotales;
        linkUltimaPagina.classList.add("pagina-seleccionable");

        linkUltimaPagina.addEventListener("click", function () {
            pasarPagina(outliersVM.PaginasTotales);
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

    let paginaInicial = Math.max(1, outliersVM.IndicePagina - Math.floor(numeroDeLinksDePaginas / 2));
    let paginaFinal = Math.min(outliersVM.PaginasTotales, paginaInicial + numeroDeLinksDePaginas - 1);

    if (paginaFinal - paginaInicial + 1 < numeroDeLinksDePaginas) {
        paginaInicial = Math.max(1, paginaFinal - numeroDeLinksDePaginas + 1);
    }

    for (let pagina = paginaInicial; pagina <= paginaFinal; pagina++) {
        const paginaLink = document.createElement("span");
        paginaLink.textContent = pagina;

        if (pagina === outliersVM.IndicePagina) {
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

    if (outliersVM.TienePaginaPrevia) {
        botonPaginaPrevia.style.display = "block";
        botonSinPaginaPrevia.style.display = "none";
    } else {
        botonPaginaPrevia.style.display = "none";
        botonSinPaginaPrevia.style.display = "block";
    }

    if (outliersVM.TieneProximaPagina) {
        botonPaginaSiguiente.style.display = "block";
        botonSinPaginaSiguiente.style.display = "none";
    } else {
        botonPaginaSiguiente.style.display = "none";
        botonSinPaginaSiguiente.style.display = "block";
    }
}

// Renderizar tabla
function renderizarTabla(datos, numeroPagina) {
    // Obtener la tabla
    var cuerpoTabla = document.getElementById("CuerpoResultados");

    // Limpiar el contenido existente
    cuerpoTabla.innerHTML = "";

    // Iterar
    for (var dato = 0; dato < datos.length; dato++) {
        var row = document.createElement("tr");
        // result-row se utiliza para redirigir los datos de la fila selecionada a la página de búsqueda
        row.classList.add("result-row");
        let indiceSeleccion = (paginador.resultadosPorPagina() * (numeroPagina - 1)) + dato;

        if (datos[dato] != null && typeof datos[dato].producto !== 'undefined' && datos[dato].producto !== "") {
            
            var checkboxDiv = document.createElement("div");
            checkboxDiv.className = "contenidoCeldaCheckbox";

            var checkbox = document.createElement("input");
            checkbox.type = "checkbox";
            checkbox.value = "";
            checkbox.checked = seleccion[indiceSeleccion];
            checkbox.id = 'checkbox_' + datos[dato].producto + '_' + datos[dato].tienda + '_' + datos[dato].provincia + '_' + datos[dato].canton;
            checkbox.style.cursor = "pointer";

            checkbox.addEventListener('click', function (event) {
                seleccion[indiceSeleccion] = !seleccion[indiceSeleccion];
            });

            var checkboxCelda = document.createElement("td");
            checkboxDiv.appendChild(checkbox);
            checkboxCelda.appendChild(checkboxDiv);

            var divProducto = document.createElement("div");
            divProducto.className = "contenidoCeldaProducto";
            divProducto.textContent = datos[dato].producto;
            var productoCelda = document.createElement("td");
            productoCelda.classList.add("tener_Tooltip");
            productoCelda.setAttribute('data-tooltip', datos[dato].producto);
            productoCelda.appendChild(divProducto);

            var divTienda = document.createElement("div");
            divTienda.className = "contenidoCeldaTienda";
            divTienda.textContent = datos[dato].tienda;
            var tiendaCelda = document.createElement("td");
            tiendaCelda.classList.add("tener_Tooltip");
            tiendaCelda.setAttribute('data-tooltip', datos[dato].tienda);
            tiendaCelda.appendChild(divTienda);

            var divProvincia = document.createElement("div");
            divProvincia.className = "contenidoCeldaProvincia";
            divProvincia.textContent = datos[dato].provincia;
            var provinciaCelda = document.createElement("td");
            provinciaCelda.classList.add("tener_Tooltip");
            provinciaCelda.setAttribute('data-tooltip', datos[dato].provincia);
            provinciaCelda.appendChild(divProvincia);

            var divCanton = document.createElement("div");
            divCanton.className = "contenidoCeldaCanton";
            divCanton.textContent = datos[dato].canton;
            var cantonCelda = document.createElement("td");
            cantonCelda.classList.add("tener_Tooltip");
            cantonCelda.setAttribute('data-tooltip', datos[dato].canton);
            cantonCelda.appendChild(divCanton);

            var divCantidadRegistros = document.createElement("div");
            divCantidadRegistros.className = "contenidoCeldaCantidadRegistros";
            divCantidadRegistros.textContent = agregarSeparador(parseFloat(datos[dato].cantidadRegistros));
            var cantidadRegistrosCelda = document.createElement("td");
            cantidadRegistrosCelda.appendChild(divCantidadRegistros);

            var divFechaCorte = document.createElement("div");
            divFechaCorte.className = "contenidoCeldaFechaCorte";
            divFechaCorte.textContent = formatearFecha(new Date(datos[dato].fechaCorte));
            var fechaCorteCelda = document.createElement("td");
            fechaCorteCelda.appendChild(divFechaCorte);

            // Agregar celdas a fila
            row.appendChild(checkboxCelda);
            row.appendChild(productoCelda);
            row.appendChild(tiendaCelda);
            row.appendChild(provinciaCelda);
            row.appendChild(cantonCelda);
            row.appendChild(cantidadRegistrosCelda);
            row.appendChild(fechaCorteCelda);
           
            // Agregar celdas cuerpo
            cuerpoTabla.appendChild(row);
        }
    }
}

function confirmarEliminacion() {
    // Obtiene la cantidad de resultados seleccionada
    const cantidadSeleccionada = seleccion.filter(value => value === true).length;

    // Si el usuario seleccionó al menos uno
    if (cantidadSeleccionada > 0) {
        if (confirm("¿Está seguro que desea eliminar los resultados seleccionados?\n\n(Esta acción tendrá consecuencias)")) {
            eliminarRegistros();
        }
    } else {
        alert("Debe seleccionar los resultados que desea eliminar");
    }
}

function eliminarRegistros() {
    var listaEliminar = [];

    for (let indice = seleccion.length - 1; indice >= 0; --indice) {
        if (seleccion[indice]) {
            listaEliminar.push(resultados[indice]);
            resultados.splice(indice, 1);
            seleccion.splice(indice, 1);
        }
    }

    if (seleccion.length > 0) {
        outliersVM = paginar(paginaDefault);
        renderizarPaginacion();
        renderizarTabla(outliersVM, 1);
    } else {
        renderizarPinwinoFeliz();
    }

    const listaEliminarStr = JSON.stringify(listaEliminar);
    fetch(`/Moderacion/FechasObsoletas?handler=EliminarRegistros&registrosStr=${listaEliminarStr}`);
}

function renderizarPinwinoFeliz() {
    // Borrar lo que hay
    var tabla = document.getElementById("ContenedorTabla");
    var paginacion = document.getElementById("ContenedorPaginacion");
    var boton = document.getElementById("ContenedorBoton");
    tabla.remove();
    paginacion.remove();
    boton.remove();

    // Agregar lo nuevo
    var contenedorPrincipal = document.getElementById("ContenedorPrincipal");
    var gifDiv = document.createElement("div");
    gifDiv.className = "FechasObsoletas-contenedor-gif";

    var gif = document.createElement("img");
    gif.src = "/img/Pinwino_feliz.gif";
    gif.alt = "Pinwino feliz";
    gif.className = "FechasObsoletas-gif";
    gif.id = "PinwinoFeliz";

    var texto = document.createElement("p");
    texto.className = "FechasObsoletas-texto";
    texto.textContent = "No existen registros obsoletos";

    gifDiv.appendChild(gif);
    gifDiv.appendChild(texto);
    contenedorPrincipal.appendChild(gifDiv);
}

// Pasar pagina
function pasarPagina(numeroPagina) {
    if (paginacionHabilitada) {
        // Paginar
        outliersVM = paginar(numeroPagina);
        // Renderizar
        renderizarPaginacion();
        renderizarTabla(outliersVM, numeroPagina);
        window.scrollTo(0, 0);
    }
}