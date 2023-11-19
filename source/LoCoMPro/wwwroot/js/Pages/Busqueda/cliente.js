var filtroModificadorActual = "";

function agregarSeparador(numero) {
    var textoNum = numero.toString();
    // Expresión regular para agregar un separador cada 3 dígitos
    textoNum = textoNum.replace(/\B(?=(\d{3})+(?!\d))/g, '.');
    return textoNum;
}

function formatearFecha(fecha) {
    const opciones = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return fecha.toLocaleDateString('es-ES', opciones);
}

function paginar(numeroPagina = productosVM.IndicePagina) {
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
    productosVM = paginar();

    renderizarPaginacion();
    renderizarTabla(productosVM);
}

// Configurar formato de precio para el filtro
function formatoPrecioFiltro() {
    // Crea un evento al escribir en la caja de texto de precio
    document.getElementById("precioMin").addEventListener("input", function (event) {
        // Reemplaza el valor de la caja de texto siguiendo esa regex
        event.target.value = event.target.value.replace(/[^\d,]|(,{2,})|(^,)|(^0+[0-9,]+)/g, "");
    });
    // Crea un evento al escribir en la caja de texto de precio
    document.getElementById("precioMax").addEventListener("input", function (event) {
        // Reemplaza el valor de la caja de texto siguiendo esa regex
        event.target.value = event.target.value.replace(/[^\d,]|(,{2,})|(^,)|(^0+[0-9,]+)/g, "");
    });
}

// Filtrar
function filtrar() {
    var provincias = obtenerCheckboxesSeleccionadas("provincia");
    var cantones = obtenerCheckboxesSeleccionadas("canton");
    var tiendas = obtenerCheckboxesSeleccionadas("tienda");
    var marcas = obtenerCheckboxesSeleccionadas("marca");
    var categorias = obtenerCheckboxesSeleccionadas("categoria");
    var precioMin = document.getElementById("precioMin").value;
    var precioMax = document.getElementById("precioMax").value;
    var fechaMin = document.getElementById("fechaMin").value;
    var fechaMax = document.getElementById("fechaMax").value;

    // Configurar filtrador
    filtrador.setFiltroProvincias(provincias);
    filtrador.setFiltroCantones(cantones);
    filtrador.setFiltroTiendas(tiendas);
    filtrador.setFiltroMarcas(marcas);
    filtrador.setFiltroCategorias(categorias);
    filtrador.setPrecioMinimo(precioMin);
    filtrador.setPrecioMaximo(precioMax);
    filtrador.setFechaMinimo(fechaMin);
    filtrador.setFechaMaxima(fechaMax);

    resultados = filtrador.filtrar(resultados);

    productosVM = paginar(paginaDefault);

    renderizarFiltros();
    renderizarPaginacion();
    renderizarTabla(productosVM);
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
    if (paginaFinal < productosVM.PaginasTotales) {
        const finalElipsis = document.createElement("span");
        // Agregar ... a la ultima pagina si fuera necesario

        if (paginaFinal !== productosVM.PaginasTotales - 1) finalElipsis.textContent = " ... ";
        else finalElipsis.textContent = " ";
        paginacionContenedor.appendChild(finalElipsis);

        const linkUltimaPagina = document.createElement("span");
        linkUltimaPagina.textContent = productosVM.PaginasTotales;
        linkUltimaPagina.classList.add("pagina-seleccionable");

        linkUltimaPagina.addEventListener("click", function () {
            pasarPagina(productosVM.PaginasTotales);
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

    return { paginaInicial, paginaFinal };
}

function renderizarBotonesSiguienteAnterior() {
    var botonPaginaPrevia = document.getElementById("PaginaPrevia");
    var botonSinPaginaPrevia = document.getElementById("SinPaginaPrevia");
    var botonPaginaSiguiente = document.getElementById("PaginaSiguiente");
    var botonSinPaginaSiguiente = document.getElementById("SinPaginaSiguiente");

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

function obtenerCantidadCheckboxesSeleccionados(nombreDeFiltro) {
    return document.querySelectorAll('input[name="' + nombreDeFiltro + '"]:checked').length;
}

function renderizarFiltroCoindicionalmente(numeroDeFiltro, nombreDeFiltro, numeroDeFiltroModificador, nombreDeFiltroModificador) {
    var checkboxesSeleccionadasDeModificador = obtenerCheckboxesSeleccionadas(nombreDeFiltroModificador);
    var checkboxesSeleccionadasFiltro = obtenerCheckboxesSeleccionadasFiltros(document.getElementById("ContenidoFiltro" + numeroDeFiltro).querySelectorAll('input[type="checkbox"]'));

    var filtro = document.getElementById("ContenidoFiltro" + numeroDeFiltro);

    var checkBoxesAgregados = [];
    var nuevosCheckboxes = [];
    filtro.innerHTML = "";
    resultados.forEach(function (dato) {
        if (checkboxesSeleccionadasDeModificador.includes(dato[nombreDeFiltroModificador]) && !checkBoxesAgregados.includes(dato[nombreDeFiltro])) {
            checkBoxesAgregados.push(dato[nombreDeFiltro]);

            var checkbox = crearCheckbox(dato[nombreDeFiltro], nombreDeFiltro, checkboxesSeleccionadasFiltro);
            var etiqueta = crearEtiqueta(dato[nombreDeFiltro], checkbox);
            // Agregar un manejador de eventos "change" que invoca sincronizarFiltroConcreto
            checkbox.addEventListener("change", function () {
                sincronizarFiltroConcreto(numeroDeFiltroModificador, nombreDeFiltroModificador, numeroDeFiltro, nombreDeFiltro);
            });
            nuevosCheckboxes.push(etiqueta);
        }
    });

    agregarCheckboxesAFiltro(filtro, nuevosCheckboxes);
}

function sincronizarFiltroConcreto(numeroDeFiltro, nombreDeFiltro, numeroDeFiltroModificador, nombreDeFiltroModificador) {
    var checkboxesSeleccionadasDeModificador = obtenerCantidadCheckboxesSeleccionados(nombreDeFiltroModificador);
    var checkboxesSeleccionadasDeFiltro = obtenerCantidadCheckboxesSeleccionados(nombreDeFiltro);

    // Si ya no hay filtros modificadores, restaurar el estado de los otros filtros
    if (checkboxesSeleccionadasDeModificador == 0) {

        filtroModificadorActual = "";
        renderizarFiltroConcreto(numeroDeFiltro, nombreDeFiltro);
        sincronizarFiltros(numeroDeFiltro, nombreDeFiltro, numeroDeFiltroModificador, nombreDeFiltroModificador);

        // Si hay filtros modificacores y no hay filtros normales seleccionado, significa que hay que cambiar los filtros en base a los modificadores
    } else if (checkboxesSeleccionadasDeFiltro == 0) {

        filtroModificadorActual = nombreDeFiltroModificador;
        renderizarFiltroCoindicionalmente(numeroDeFiltro, nombreDeFiltro, numeroDeFiltroModificador, nombreDeFiltroModificador);

    // Si el modificador sigue con filtros y el modificador actual es el modificador que llama a esta funcion, hay que renderizar de nuevo condicionalmente el filtro
    } else if (checkboxesSeleccionadasDeModificador > 0 && filtroModificadorActual === nombreDeFiltroModificador) {

        renderizarFiltroCoindicionalmente(numeroDeFiltro, nombreDeFiltro, numeroDeFiltroModificador, nombreDeFiltroModificador);

    }
}

function sincronizarFiltros(numeroDeFiltro1, nombreDeFiltro1, numeroDeFiltro2, nombreDeFiltro2) {
    var checkboxesDeFiltro1 = document.querySelectorAll('input[name="' + nombreDeFiltro1 + '"]');
    var checkboxesDeFiltro2 = document.querySelectorAll('input[name="' + nombreDeFiltro2 + '"]');

    checkboxesDeFiltro1.forEach(function (casilla) {
        // Agregar un manejador de eventos "click" que invoca sincronizarFiltroConcreto
        casilla.addEventListener("click", function () {
            sincronizarFiltroConcreto(numeroDeFiltro2, nombreDeFiltro2, numeroDeFiltro1, nombreDeFiltro1);
        });
    });

    checkboxesDeFiltro2.forEach(function (casilla) {
        // Agregar un manejador de eventos "click" que invoca sincronizarFiltroConcreto
        casilla.addEventListener("click", function () {
            sincronizarFiltroConcreto(numeroDeFiltro1, nombreDeFiltro1, numeroDeFiltro2, nombreDeFiltro2);
        });
    });
}

function renderizarFiltros() {
    const filtroProvincia = { numeroDeFiltro: 1, nombreDeFiltro: "provincia" };
    const filtroCanton = { numeroDeFiltro: 2, nombreDeFiltro: "canton" };
    const filtroTienda = { numeroDeFiltro: 3, nombreDeFiltro: "tienda" };
    const filtroMarca = { numeroDeFiltro: 4, nombreDeFiltro: "marca" };
    const filtroCategoria = { numeroDeFiltro: 5, nombreDeFiltro: "categoria" };

    renderizarFiltroConcreto(filtroProvincia["numeroDeFiltro"], filtroProvincia["nombreDeFiltro"]);
    renderizarFiltroConcreto(filtroCanton["numeroDeFiltro"], filtroCanton["nombreDeFiltro"]);
    renderizarFiltroConcreto(filtroTienda["numeroDeFiltro"], filtroTienda["nombreDeFiltro"]);
    renderizarFiltroConcreto(filtroMarca["numeroDeFiltro"], filtroMarca["nombreDeFiltro"]);
    renderizarFiltroConcreto(filtroCategoria["numeroDeFiltro"], filtroCategoria["nombreDeFiltro"]);

    sincronizarFiltros(
        filtroProvincia["numeroDeFiltro"], filtroProvincia["nombreDeFiltro"],
        filtroCanton["numeroDeFiltro"], filtroCanton["nombreDeFiltro"]);
}

function obtenerCheckboxesSeleccionadasFiltros(casillasExistente) {
    const valoresSeleccionados = {};
    casillasExistente.forEach(function (casilla) {
        if (casilla.checked) {
            valoresSeleccionados[casilla.value] = true;
        }
    });
    return valoresSeleccionados;
}

function crearCheckbox(valor, nombreDeFiltro, valoresSeleccionados) {
    const casilla = document.createElement("input");
    casilla.type = "checkbox";
    casilla.name = nombreDeFiltro;
    casilla.value = valor;
    if (valoresSeleccionados[valor]) {
        casilla.checked = true;
    }
    return casilla;
}

function crearEtiqueta(valor, casilla) {
    const etiqueta = document.createElement("label");
    etiqueta.appendChild(casilla);
    etiqueta.appendChild(document.createTextNode(valor));
    return etiqueta;
}

function construirCheckboxesYEtiquetas(resultados, nombreDeFiltro, valoresSeleccionados) {
    const valoresUnicos = {};
    const checkboxesOrdenados = [];

    for (const resultado in resultados) {
        const valor = resultados[resultado][nombreDeFiltro];
        if (typeof valor !== 'undefined' && valor !== "" && !valoresUnicos[valor]) {
            valoresUnicos[valor] = true;
            const casilla = crearCheckbox(valor, nombreDeFiltro, valoresSeleccionados);
            const etiqueta = crearEtiqueta(valor, casilla);
            checkboxesOrdenados.push(etiqueta);
        }
    }

    return checkboxesOrdenados;
}

function ordenarCheckboxes(checkboxes) {
    checkboxes.sort(function (a, b) {
        const valueA = a.textContent;
        const valueB = b.textContent;
        return valueA.localeCompare(valueB);
    });
    return checkboxes;
}

function agregarCheckboxesAFiltro(filtro, checkboxes) {
    filtro.innerHTML = "";

    checkboxes.forEach(function (etiquetaCheckbox) {
        etiquetaCheckbox.style.marginTop = "30px";
        etiquetaCheckbox.style.marginBottom = "-8px";
        filtro.appendChild(etiquetaCheckbox);
    });

    if (filtro.childElementCount === 1) {
        filtro.innerHTML = "";
    }
}

function renderizarFiltroConcreto(numeroDeFiltro, nombreDeFiltro) {
    const filtros = document.getElementById("ContenidoFiltro" + numeroDeFiltro);
    const casillasExistente = filtros.querySelectorAll('input[type="checkbox"]');

    const valoresSeleccionados = obtenerCheckboxesSeleccionadasFiltros(casillasExistente);

    const checkboxes = construirCheckboxesYEtiquetas(resultados, nombreDeFiltro, valoresSeleccionados);

    const checkboxesOrdenados = ordenarCheckboxes(checkboxes);

    agregarCheckboxesAFiltro(filtros, checkboxesOrdenados);
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
    const row = document.createElement("tr");
    row.classList.add("result-row");

    if (typeof datos.nombre !== 'undefined' && datos.nombre !== "") {
        const nombreCelda = crearCeldaContenido(datos.nombre, "contenidoCeldaNombre");
        const categoriaCelda = crearCeldaContenido(datos.categoria, "contenidoCeldaCategoria");
        const marcaCelda = crearCeldaContenido(datos.marca, "contenidoCeldaMarca");
        const precioArreglado = "₡" + agregarSeparador(parseFloat(datos.precio));
        const precioCelda = crearCeldaContenido(precioArreglado, "contenidoCeldaPrecio");
        const unidadCelda = crearCeldaContenido(datos.unidad, "contenidoCeldaUnidad");
        const fechaCelda = crearCeldaContenido(formatearFecha(new Date(datos.fecha), "contenidoCeldaFecha"));
        const tiendaCelda = crearCeldaContenido(datos.tienda, "contenidoCeldaTienda");
        const provinciaCelda = crearCeldaContenido(datos.provincia, "contenidoCeldaProvincia");
        const cantonCelda = crearCeldaContenido(datos.canton, "contenidoCeldaCanton");

        row.appendChild(nombreCelda);
        row.appendChild(categoriaCelda);
        row.appendChild(marcaCelda);
        row.appendChild(precioCelda);
        row.appendChild(unidadCelda);
        row.appendChild(fechaCelda);
        row.appendChild(tiendaCelda);
        row.appendChild(provinciaCelda);
        row.appendChild(cantonCelda);

        cuerpoTabla.appendChild(row);
    }
}


function renderizarPinwinoTriste() {
    var filaDeContenedores = document.getElementById("FilaDeFiltrosYResultados");
    var tabla = document.getElementsByClassName("BusquedaIndice-tabla");
    var paginacion = document.getElementsByClassName("BusquedaIndice-contenedor-paginacion");

    // Quitar elementos innecesarios
    for (var i = 0; i < paginacion.length; i++) {
        paginacion[i].style.visibility = "hidden";
    }

    for (var i = 0; i < tabla.length; i++) {
        tabla[i].style.maxWidth = "25%";
        tabla[i].style.visibility = "hidden";
        tabla[i].style.overflowX = "unset";
    }

    var contenedorDiv = document.createElement("div");
    contenedorDiv.className = "BusquedaIndice-contenedor-gif";
    contenedorDiv.id = "ContenedorGif";

    var imagenGif = document.createElement("img");
    imagenGif.src = "/img/Pinwino_triste.gif";
    imagenGif.alt = "Pinwino triste";
    imagenGif.className = "BusquedaIndice-gif";
    imagenGif.id = "PinwinoTriste";

    var parrafo = document.createElement("p");
    parrafo.className = "BusquedaIndice-texto";
    parrafo.textContent = "No se encontraron resultados";

    contenedorDiv.appendChild(imagenGif);
    contenedorDiv.appendChild(parrafo);

    filaDeContenedores.appendChild(contenedorDiv);

    // Establecer el uso del filtrador para limpiar los filtros correctamente despues
    filtrador.usado = true;
}

function removerPinwinoTriste() {
    var tabla = document.getElementsByClassName("BusquedaIndice-tabla");
    var paginacion = document.getElementsByClassName("BusquedaIndice-contenedor-paginacion");
    var imagen = document.getElementById("ContenedorGif");

    if (imagen != null) {
        imagen.remove();

        // Restaurar elementos
        for (var i = 0; i < paginacion.length; i++) {
            paginacion[i].style.visibility = "visible";
        }

        for (var i = 0; i < tabla.length; i++) {
            tabla[i].style.maxWidth = "100%";
            tabla[i].style.visibility = "visible";
            tabla[i].style.overflowX = "auto";
        }
    }
}

function renderizarTabla(datos) {
    removerPinwinoTriste();

    const cuerpoTabla = document.getElementById("ResultadosDeBusqueda");

    // Limpiar el contenido existente
    cuerpoTabla.innerHTML = "";

    if (datos.length == 0) {

        renderizarPinwinoTriste();

    } else {

        datos.forEach(function (dato) {
            agregarFilaALaTabla(cuerpoTabla, dato);
        });

    }
}

// Redirecionar a ver registros

document.addEventListener("click", function () {
    const rows = document.querySelectorAll(".result-row");

    rows.forEach(row => {
        row.addEventListener("click", function () {
            const productoNombre = row.querySelector("td:nth-child(1)").textContent;   // Columna 1
            const categoriaNombre = row.querySelector("td:nth-child(2)").textContent;  // Columna 2
            const marcaNombre = row.querySelector("td:nth-child(3)").textContent;      // Columna 3
            const unidadNombre = row.querySelector("td:nth-child(5)").textContent;     // Columna 5
            const tiendaNombre = row.querySelector("td:nth-child(7)").textContent;     // Columna 7
            const provinciaNombre = row.querySelector("td:nth-child(8)").textContent;  // Columna 8
            const cantonNombre = row.querySelector("td:nth-child(9)").textContent;     // Columna 9

            window.location.href = `/VerRegistros/VerRegistros?productoNombre=${encodeURIComponent(productoNombre)}&categoriaNombre=${encodeURIComponent(categoriaNombre)}&marcaNombre=${encodeURIComponent(marcaNombre)}&unidadNombre=${encodeURIComponent(unidadNombre)}&tiendaNombre=${encodeURIComponent(tiendaNombre)}&provinciaNombre=${encodeURIComponent(provinciaNombre)}&cantonNombre=${encodeURIComponent(cantonNombre)}`;
        });
    });
});

function pasarPagina(numeroPagina) {
    productosVM = paginar(numeroPagina);

    renderizarPaginacion();
    renderizarTabla(productosVM);
    window.scrollTo(0, 0);
}

function limpiarCheckboxes(nombreDeCheckboxes) {
    var checkboxes = document.querySelectorAll('input[name="' + nombreDeCheckboxes + '"]:checked');

    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].checked = false;
    }
}

// Limpiar input según parámetro
function limpiarInput(primerValor, segundoValor) {
    var inputMinimo = document.getElementById(primerValor);
    var inputMaximo = document.getElementById(segundoValor);
    inputMinimo.value = "";
    inputMaximo.value = "";
}

function limpiarFiltros() {
    limpiarCheckboxes("provincia");
    limpiarCheckboxes("canton");
    limpiarCheckboxes("tienda");
    limpiarCheckboxes("marca");
    limpiarCheckboxes("categoria");
    limpiarInput("precioMin", "precioMax");
    limpiarInput("fechaMin", "fechaMax");

    if (filtrador.usado) {

        filtrador.resetearUso();

        // Restaurar resultados
        resultados = obtenerResultados();

        productosVM = paginar(paginaDefault);

        renderizarFiltros();
        renderizarPaginacion();
        renderizarTabla(productosVM);
    }
}

function obtenerCheckboxesSeleccionadas(nombreDeCheckboxes) {
    var valores = [];
    var checkboxes = document.querySelectorAll('input[name="' + nombreDeCheckboxes + '"]:checked');

    for (var i = 0; i < checkboxes.length; i++) {
        valores.push(checkboxes[i].value);
    }

    return valores;
}

function establecerFechaMaxima() {
    var fechaActual = new Date();
    var dia = fechaActual.getDate();
    // Enero se maneja como 0
    var mes = fechaActual.getMonth() + 1;
    var anno = fechaActual.getFullYear();
    if (dia < 10) {
        dia = '0' + dia
    }
    if (mes < 10) {
        mes = '0' + mes
    }
    fechaActual = anno + '-' + mes + '-' + dia;
    document.getElementById("fechaMin").setAttribute("max", fechaActual);
    document.getElementById("fechaMax").setAttribute("max", fechaActual);
}