function agregarSeparador(numero) {
    var textoNum = numero.toString();
    textoNum = textoNum.replace('.', ',');
    // Expresión regular para agregar un separador cada 3 dígitos
    textoNum = textoNum.replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return textoNum;
}

function paginar(numeroPagina = registrosPag.IndicePagina) {
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
    if (paginaFinal < registrosPag.PaginasTotales) {
        const finalElipsis = document.createElement("span");
        // Agregar ... a la ultima pagina si fuera necesario

        if (paginaFinal !== registrosPag.PaginasTotales - 1) finalElipsis.textContent = " ... ";
        else finalElipsis.textContent = " ";
        paginacionContenedor.appendChild(finalElipsis);

        const linkUltimaPagina = document.createElement("span");
        linkUltimaPagina.textContent = registrosPag.PaginasTotales;
        linkUltimaPagina.classList.add("pagina-seleccionable");

        linkUltimaPagina.addEventListener("click", function () {
            pasarPagina(registrosPag.PaginasTotales);
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

    let paginaInicial = Math.max(1, registrosPag.IndicePagina - Math.floor(numeroDeLinksDePaginas / 2));
    let paginaFinal = Math.min(registrosPag.PaginasTotales, paginaInicial + numeroDeLinksDePaginas - 1);

    if (paginaFinal - paginaInicial + 1 < numeroDeLinksDePaginas) {
        paginaInicial = Math.max(1, paginaFinal - numeroDeLinksDePaginas + 1);
    }

    for (let pagina = paginaInicial; pagina <= paginaFinal; pagina++) {
        const paginaLink = document.createElement("span");
        paginaLink.textContent = pagina;

        if (pagina === registrosPag.IndicePagina) {
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

    if (registrosPag.TienePaginaPrevia) {
        botonPaginaPrevia.style.display = "block";
        botonSinPaginaPrevia.style.display = "none";
    } else {
        botonPaginaPrevia.style.display = "none";
        botonSinPaginaPrevia.style.display = "block";
    }

    if (registrosPag.TieneProximaPagina) {
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

// Variable con todas los registros que se desean eliminar
var filasEliminar = [];

function renderizarTabla(datos) {
    // Obtener la tabla
    var cuerpoTabla = document.getElementById("CuerpoResultados");

    // Limpiar el contenido existente
    cuerpoTabla.innerHTML = "";

    // Iterar
    for (var dato in datos) {
        if (datos[dato] != null && typeof datos[dato].producto !== 'undefined' && datos[dato].producto !== "") {
            var fila = document.createElement("tr");

            const valorPrecio = "₡" + agregarSeparador(parseFloat(datos[dato].precio));
            const valorMinimo = "₡" + agregarSeparador(parseFloat(datos[dato].minimo));
            const valorMaximo = "₡" + agregarSeparador(parseFloat(datos[dato].maximo));
            const valorDesviacion = "₡" + agregarSeparador(parseFloat(datos[dato].desviacionEstandar.toFixed(2)));
            const valorPromedio = "₡" + agregarSeparador(parseFloat(datos[dato].promedio.toFixed(2)));

            // Checkbox de la fila
            var checkboxDiv = document.createElement("div");
            checkboxDiv.className = "contenidoCeldaCheckbox";

            var checkbox = document.createElement("input");
            checkbox.type = "checkbox";
            checkbox.id = 'checkbox_' + dato;
            checkbox.addEventListener('click', function (evento) {
                var checkboxCambiado = evento.target;
                var filaAEliminar = checkboxCambiado.closest("tr");
                if (checkboxCambiado.checked) {
                    // Hay que agregarlo
                    filasEliminar.push(filaAEliminar);
                } else {
                    // Hay que borrarlo
                    filasEliminar = filasEliminar.filter(fila => fila !== filaAEliminar);
                }
            });

            var checkboxCelda = document.createElement("td");
            checkboxCelda.classList.add('no-hover');
            checkboxDiv.appendChild(checkbox);
            checkboxCelda.appendChild(checkboxDiv);

            // Contenido de las celdas
            var productoCelda = crearCeldaContenido(datos[dato].producto, "contenidoCeldaProducto");
            var precioCelda = crearCeldaContenido(valorPrecio, "contenidoCeldaPrecio");
            var minimoCelda = crearCeldaContenido(valorMinimo, "contenidoCeldaMinimo");
            var maximoCelda = crearCeldaContenido(valorMaximo, "contenidoCeldaMaximo");
            var desviacionCelda = crearCeldaContenido(valorDesviacion, "contenidoCeldaDesviacion");
            var promedioCelda = crearCeldaContenido(valorPromedio, "contenidoCeldaPromedio");
            var tiendaCelda = crearCeldaContenido(datos[dato].tienda, "contenidoCeldaTienda");
            var provinciaCelda = crearCeldaContenido(datos[dato].provincia, "contenidoCeldaPronvincia");
            var cantonCelda = crearCeldaContenido(datos[dato].canton, "contenidoCeldaCanton");

            // Se almacena el índice al que está asociado
            fila.dataset.fecha = datos[dato].fecha;
            fila.dataset.usuario = datos[dato].usuario;

            // Agregar celdas a fila
            fila.appendChild(checkboxCelda);
            fila.appendChild(productoCelda);
            fila.appendChild(precioCelda);
            fila.appendChild(minimoCelda);
            fila.appendChild(maximoCelda);
            fila.appendChild(desviacionCelda);
            fila.appendChild(promedioCelda);
            fila.appendChild(tiendaCelda);
            fila.appendChild(provinciaCelda);
            fila.appendChild(cantonCelda);

            // Agregar fila
            cuerpoTabla.appendChild(fila);
        }
    }
}

// Función que devuelve una lista eliminando el elemento seleccionado
function removerElementoDeResultados(datos, creacion, usuarioCreador) {
    lista = []
    for (var dato in datos) {
        if (typeof datos[dato].fecha !== 'undefined' && datos[dato].fecha !== "" &&
                datos[dato].usuario !== 'undefined' && datos[dato].usuario !== "" &&
                (datos[dato].fecha !== creacion || datos[dato].usuario !== usuarioCreador)) {
            lista.push(datos[dato]);
        }
    }
    return lista;
}

function confirmarAccion() {
    if (filasEliminar.length > 0) {
        if (confirm("¿Está seguro que desea eliminar los registros seleccionados?\n\n(Esta acción tendrá consecuencias)")) {
            eliminarRegistros();
        }
    } else {
        alert("Debe seleccionar los registros que desea eliminar");
    }
}

function eliminarRegistros() {
    var listaEliminar = [];

    // Deshabilitar paginacion momentáneamente
    paginacionHabilitada = false;

    for (var indice = 0; indice < filasEliminar.length; ++indice) {
        var fecha = filasEliminar[indice].dataset.fecha;
        var usuario = filasEliminar[indice].dataset.usuario;

        numeroPagina = registrosPag.IndicePagina;
        // Remover la fila
        filasEliminar[indice].remove();
        // Remover el outlier de los resultados
        resultados = removerElementoDeResultados(resultados, fecha, usuario);
        // Verificar si quedan outliers
        if (resultados.length > 0) {
            registrosPag = removerElementoDeResultados(registrosPag, fecha, usuario);
            // Paginar la página actual o la anterior si ya no quedan resultados en la actual
            registrosPag = registrosPag.length != 0 ? paginar(numeroPagina) : paginar(numeroPagina - 1);
            // Renderizar
            renderizarPaginacion();
            renderizarTabla(registrosPag);
        } else {
            // Terminó de revisar
            renderizarPinwinoFeliz();
        }

        listaEliminar.push({ fecha, usuario });
    }

    const listaEliminarStr = JSON.stringify(listaEliminar);
    fetch(`/Moderacion/OutliersPrecio?handler=EliminarRegistros&registrosStr=${listaEliminarStr}`);

    // Volver a habilitar paginacion
    paginacionHabilitada = true;

    // Se limpia
    filasEliminar = [];
}

function renderizarPinwinoFeliz() {
    // Borrar lo que hay
    var contenedorPrincipal = document.getElementById("ContenedorPrincipal");
    var tabla = document.getElementsByClassName("Tabla");
    var paginacion = document.getElementsByClassName("Contenedor-paginacion");
    var boton = document.getElementById("Eliminar");

    for (var i = 0; i < paginacion.length; i++) {
        paginacion[i].remove();
    }

    for (var i = 0; i < tabla.length; i++) {
        tabla[i].remove();
    }

    boton.remove();

    // Agregar lo nuevo
    var contenedorDiv = document.createElement("div");
    contenedorDiv.className = "Contenedor-gif";

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

    contenedorPrincipal.appendChild(contenedorDiv);
}

// Pasar pagina
function pasarPagina(numeroPagina) {
    if (paginacionHabilitada) {
        // Paginar
        registrosPag = paginar(numeroPagina);
        // Renderizar
        renderizarPaginacion();
        renderizarTabla(registrosPag);
        window.scrollTo(0, 0);
    }
}