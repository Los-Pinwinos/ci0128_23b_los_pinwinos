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
function paginar(numeroPagina = favoritosVM.IndicePagina) {
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

// Renderizar tabla
function renderizarTabla(datos) {
    // Obtener la tabla
    var cuerpoTabla = document.getElementById("CuerpoResultados");

    // Limpiar el contenido existente
    cuerpoTabla.innerHTML = "";

    // Iterar
    for (var dato in datos) {
        var row = document.createElement("tr");
        // result-row se utiliza para redirigir los datos de la fila selecionada a la página de búsqueda
        row.classList.add("result-row")

        if (datos[dato] != null && typeof datos[dato].nombreProducto !== 'undefined' && datos[dato].nombreProducto !== "") {
            var corazonDiv = document.createElement("div");
            corazonDiv.className = "contenidoCeldaCorazon";

            var iconoCorazon = document.createElement("i");
            iconoCorazon.classList.add('corazon', 'fas', 'fa-heart');
            iconoCorazon.classList.add('corazon-lleno');
            var idUnico = 'corazon_' + datos[dato].nombreProducto;
            iconoCorazon.id = idUnico;

            // Agregar evento de click
            iconoCorazon.addEventListener('click', function (evento) {
                var iconoClickeado = evento.target;
                var filaAEliminar = iconoClickeado.closest("tr");
                alternarCorazon(iconoClickeado.id, filaAEliminar);
            });

            var corazonCelda = document.createElement("td");
            corazonCelda.classList.add('no-hover');
            corazonDiv.appendChild(iconoCorazon);
            corazonCelda.appendChild(corazonDiv);

            var divProducto = document.createElement("div");
            divProducto.className = "contenidoCeldaProducto";
            divProducto.textContent = datos[dato].nombreProducto;
            var productoCelda = document.createElement("td");
            productoCelda.setAttribute('data-tooltip', datos[dato].nombreProducto);
            productoCelda.appendChild(divProducto);

            var divCategoria = document.createElement("div");
            divCategoria.className = "contenidoCeldaCategoria";
            divCategoria.textContent = datos[dato].nombreCategoria;
            var categoriaCelda = document.createElement("td");
            categoriaCelda.setAttribute('data-tooltip', datos[dato].nombreCategoria);
            categoriaCelda.appendChild(divCategoria);

            var divMarca = document.createElement("div");
            divMarca.className = "contenidoCeldaMarca";
            divMarca.textContent = datos[dato].nombreMarca;
            var marcaCelda = document.createElement("td");
            marcaCelda.setAttribute('data-tooltip', datos[dato].nombreMarca);
            marcaCelda.appendChild(divMarca);

            // Agregar celdas a fila
            row.appendChild(corazonCelda);
            row.appendChild(productoCelda);
            row.appendChild(categoriaCelda);
            row.appendChild(marcaCelda);
           
            // Agregar celdas cuerpo
            cuerpoTabla.appendChild(row);

            /*
            // Variables para redireccionar
            var producto = datos[dato].nombreProducto;

            // Redirecciona a Detalles Registro
            (function (producto) {
                row.addEventListener("click", function () {
                    window.location.href = `/Busqueda?&nombreProducto=${encodeURIComponent(producto)}`;
                });
            })(producto);*/

        }
    }
}

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

// Función para alternar la animación al hacer click
function alternarCorazon(idIcono, filaEliminar) {
    // Deshabilitar paginacion momentaneamente
    paginacionHabilitada = false;
    var iconoCorazon = document.getElementById(idIcono);
    if (iconoCorazon) {
        iconoCorazon.classList.remove('corazon-lleno');
    }

    if (filaEliminar) {
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
}

function renderizarPinwinoTriste() {
    var filaDeContenedores = document.getElementById("ContenedorPrincipal");
    var botonBusqueda = document.getElementById("BotonBusqueda");
    var tabla = document.getElementsByClassName("Favoritos-tabla");
    var paginacion = document.getElementsByClassName("Favoritos-contenedor-paginacion");

    botonBusqueda.remove();
    for (var i = 0; i < paginacion.length; i++) {
        paginacion[i].remove();
    }

    for (var i = 0; i < tabla.length; i++) {
        tabla[i].remove();
    }

    var contenedorDiv = document.createElement("div");
    contenedorDiv.className = "Favoritos-contenedor-gif";
    contenedorDiv.id = "ContenedorGif";

    var imagenGif = document.createElement("img");
    imagenGif.src = "/img/Pinwino_triste.gif";
    imagenGif.alt = "Pinwino triste";
    imagenGif.className = "Favoritos-gif";
    imagenGif.id = "PinwinoTriste";

    var parrafo = document.createElement("p");
    parrafo.className = "Favoritos-texto";
    parrafo.textContent = "No se han agregado favoritos";

    contenedorDiv.appendChild(imagenGif);
    contenedorDiv.appendChild(parrafo);

    filaDeContenedores.appendChild(contenedorDiv);
}

// Pasar pagina
function pasarPagina(numeroPagina) {
    if (paginacionHabilitada) {
        // Paginar
        favoritosVM = paginar(numeroPagina);
        // Renderizar
        renderizarPaginacion();
        renderizarTabla(favoritosVM);
        window.scrollTo(0, 0);
    }
}