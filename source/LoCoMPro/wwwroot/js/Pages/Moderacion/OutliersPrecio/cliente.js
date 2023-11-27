function agregarSeparador(numero) {
    var textoNum = numero.toString();
    // Expresión regular para agregar un separador cada 3 dígitos
    textoNum = textoNum.replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return textoNum;
}

function paginar(numeroPagina = registrosVM.IndicePagina) {
    return paginador.paginar(resultados, numeroPagina);
}

function renderizarPaginacion() {
    // Renderizar los botones de Siguiente Pagina y Pagina Anterior
    renderizarBotonesSiguienteAnterior();

    var paginacionContenedor = document.getElementById("NumerosPaginacion");

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

// Crea el contenido de una celda para una clase
function crearCeldaContenido(contenido, clase) {
    const divContenido = document.createElement("div");
    divContenido.className = clase;
    divContenido.textContent = contenido;

    const celda = document.createElement("td");
    celda.setAttribute('data-tooltip', contenido);
    celda.appendChild(divContenido);

    return celda;
}

function renderizarTabla(datos) {
    // Obtener la tabla
    var cuerpoTabla = document.getElementById("CuerpoResultados");

    // Limpiar el contenido existente
    cuerpoTabla.innerHTML = "";

    // Iterar
    for (var dato in datos) {
        var row = document.createElement("tr");

        if (datos[dato] != null && typeof datos[dato].producto !== 'undefined' && datos[dato].producto !== "") {
            /*
            // Checkbox
            var checkboxDiv = document.createElement("div");
            checkboxDiv.className = "contenidoCeldaCheckbox";

            var checkbox = document.createElement("input");
            checkbox.type = "checkbox";
            checkbox.id = 'checkbox_' + dato;

            checkbox.addEventListener('change', function (evento) {
                var checkboxCambiado = evento.target;
                var filaAEliminar = checkboxCambiado.closest("tr");
                eliminarRegistro(filaAEliminar);
            });

            var checkboxCelda = document.createElement("td");
            checkboxCelda.classList.add('no-hover');
            checkboxDiv.appendChild(checkbox);
            checkboxCelda.appendChild(checkboxDiv);
            */

            // Contenido de las celdas
            var productoCelda = crearCeldaContenido(datos[dato].producto, "contenidoCeldaProducto");
            var precioCelda = crearCeldaContenido(datos[dato].precio, "contenidoCeldaPrecio");
            var minimoCelda = crearCeldaContenido(datos[dato].minimo, "contenidoCeldaMinimo");
            var maximoCelda = crearCeldaContenido(datos[dato].maximo, "contenidoCeldaMaximo");
            var desviacionCelda = crearCeldaContenido(datos[dato].desviacionEstandar, "contenidoCeldaDesviacion");
            var promedioCelda = crearCeldaContenido(datos[dato].promedio, "contenidoCeldaPromedio");
            var tiendaCelda = crearCeldaContenido(datos[dato].minimo, "contenidoCeldaTienda");
            var provinciaCelda = crearCeldaContenido(datos[dato].provincia, "contenidoCeldaPronvincia");
            var cantonCelda = crearCeldaContenido(datos[dato].canton, "contenidoCeldaCanton");

            // Agregar celdas a fila
            row.appendChild(productoCelda);
            row.appendChild(precioCelda);
            row.appendChild(minimoCelda);
            row.appendChild(maximoCelda);
            row.appendChild(desviacionCelda);
            row.appendChild(tiendaCelda);
            row.appendChild(provinciaCelda);
            row.appendChild(cantonCelda);

            // Agregar fila
            cuerpoTabla.appendChild(row);
        }
    }
}

// TODO(Angie): cambiar
// Función que devuelve una lista eliminando el elemento seleccionado
function removerElementoDeResultados(datos, producto) {
    lista = []
    for (var dato in datos) {
        if (typeof datos[dato].nombreProducto !== 'undefined' && datos[dato].nombreProducto !== "") {
            if (datos[dato].nombreProducto != producto)
                lista.push(datos[dato]);
        }
    }
    return lista;
}

// TODO(Angie): cambiar
function eliminarRegistro(filaEliminar) {
    // TODO(Angie): cambiar

    // Deshabilitar paginacion momentaneamente
    paginacionHabilitada = false;

    // Obtener el producto asociado
    var producto = filaEliminar.querySelector('.contenidoCeldaProducto').textContent;
    // Agrega la clase para la transición suave y eliminación
    filaEliminar.classList.add('eliminacion-suave', 'eliminando');
    setTimeout(function () {
        numeroPagina = favoritosVM.IndicePagina;
        // Remover la fila
        filaEliminar.remove();
        // Remover el producto de los resultados
        resultados = removerElementoDeResultados(resultados, producto);
        // Verificar si quedan productos
        if (resultados.length > 0) {
            // Remover el producto de los resultados paginados
            favoritosVM = removerElementoDeResultados(favoritosVM, producto);
            // Paginar la pagina actual o la anterior si ya no quedan resultados en la actual
            favoritosVM = favoritosVM.length != 0 ? paginar(numeroPagina) : paginar(numeroPagina - 1);
            // Renderizar
            renderizarPaginacion();
            renderizarTabla(favoritosVM);
        } else {
            renderizarPinwinoTriste();
        }
        fetch(`/Cuenta/Favoritos?handler=RemoverDeFavoritos&nombreProducto=${producto}`);
        // Volver a habilitar paginacion
        paginacionHabilitada = true;
    }, 300);
}

function renderizarPinwinoFeliz() {
    var filaDeContenedores = document.getElementById("ContenedorPrincipal");
    var tabla = document.getElementsByClassName("Tabla");
    var paginacion = document.getElementsByClassName("Contenedor-paginacion");

    for (var i = 0; i < paginacion.length; i++) {
        paginacion[i].remove();
    }

    for (var i = 0; i < tabla.length; i++) {
        tabla[i].remove();
    }

    var contenedorDiv = document.createElement("div");
    contenedorDiv.className = "Contenedor-gif";
    contenedorDiv.id = "ContenedorGif";

    var imagenGif = document.createElement("img");
    imagenGif.src = "/img/Pinwino_feliz.gif";
    imagenGif.alt = "Pinwino feliz";
    imagenGif.className = "Gif";
    imagenGif.id = "PinwinoFeliz";

    var parrafo = document.createElement("p");
    parrafo.className = "Texto";
    parrafo.textContent = "No hay precios anómalos";

    contenedorDiv.appendChild(imagenGif);
    contenedorDiv.appendChild(parrafo);

    filaDeContenedores.appendChild(contenedorDiv);
}

// Pasar pagina
function pasarPagina(numeroPagina) {
    if (paginacionHabilitada) {
        // Paginar
        registrosVM = paginar(numeroPagina);
        // Renderizar
        renderizarPaginacion();
        renderizarTabla(registrosVM);
        window.scrollTo(0, 0);
    }
}