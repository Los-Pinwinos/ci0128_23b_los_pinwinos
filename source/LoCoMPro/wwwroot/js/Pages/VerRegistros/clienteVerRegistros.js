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


function tituloNormal(titulosTabla) {
    var titulosNuevos = document.createElement("tr");

    var fechaTitulo = document.createElement("th");
    fechaTitulo.textContent = "Fecha";
    fechaTitulo.className = "ContenidoTitulo";
    fechaTitulo.style.paddingLeft = "10px";

    var precioTitulo = document.createElement("th");
    precioTitulo.textContent = "Precio";
    precioTitulo.className = "ContenidoTitulo";
    precioTitulo.style.paddingLeft = "80px";

    var califTitulo = document.createElement("th");
    califTitulo.textContent = "Calificación";
    califTitulo.className = "ContenidoTitulo";
    califTitulo.style.paddingLeft = "90px";

    var descripcionTitulo = document.createElement("th");
    descripcionTitulo.textContent = "Descripción";
    descripcionTitulo.className = "ContenidoTitulo";
    descripcionTitulo.style.paddingLeft = "90px";

    titulosNuevos.appendChild(fechaTitulo);
    titulosNuevos.appendChild(precioTitulo);
    titulosNuevos.appendChild(califTitulo);
    titulosNuevos.appendChild(descripcionTitulo);

    titulosTabla.appendChild(titulosNuevos);
}



function renderizarTabla(datos) {

    var cuerpoTabla = document.getElementById("cuerpoResultados");

    var titulosTabla = document.getElementById("TitulosTabla");

    cuerpoTabla.innerHTML = "";

    titulosTabla.innerHTML = "";

    tituloNormal(titulosTabla);

    for (var dato in datos) {
        var fila = document.createElement("tr");

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
            divCalificacion.textContent = datos[dato].calificacion == null ? "Sin calificar" : datos[dato].calificacion;
            var calificacionCelda = document.createElement("td");
            calificacionCelda.setAttribute('data-tooltip', divCalificacion.textContent);
            calificacionCelda.appendChild(divCalificacion);

            var descripcionCelda = document.createElement("td");
            descripcionCelda.textContent = datos[dato].descripcion;

            fila.appendChild(fechaCelda);
            fila.appendChild(precioCelda);
            fila.appendChild(calificacionCelda);
            fila.appendChild(descripcionCelda);

            cuerpoTabla.appendChild(fila);

            var fechaHoraEnviar = datos[dato].creacion;
            var usuarioEnviar = datos[dato].usuarioCreador;

            // Redirecciona a Detalles Registro
            (function (fechaHora, usuario) {
                fila.addEventListener("click", function () {
                    var producto = document.getElementById("nombreProducto").value;
                    window.location.href = `/detallesRegistro/detallesRegistro?fechaHora=${encodeURIComponent(fechaHora)}&usuario=${encodeURIComponent(usuario)}&producto=${encodeURIComponent(producto)}`;
                });
            })(fechaHoraEnviar, usuarioEnviar);
        }
    }
}



function titulosAgrupados(titulosTabla, titulosNuevos, fechaTitulo) {

    var pminTitulo = document.createElement("th");
    pminTitulo.textContent = "Precio Mínimo";
    pminTitulo.className = "ContenidoTitulo";
    pminTitulo.style.paddingLeft = "50px";

    var ppromTitulo = document.createElement("th");
    ppromTitulo.textContent = "Precio Promedio";
    ppromTitulo.className = "ContenidoTitulo";
    ppromTitulo.style.paddingLeft = "50px";

    var pmaxTitulo = document.createElement("th");
    pmaxTitulo.textContent = "Precio Máximo";
    pmaxTitulo.className = "ContenidoTitulo";
    pmaxTitulo.style.paddingLeft = "50px";

    var califPromTitulo = document.createElement("th");
    califPromTitulo.textContent = "Calificación Promedio";
    califPromTitulo.className = "ContenidoTitulo";
    califPromTitulo.style.paddingLeft = "50px";

    titulosNuevos.appendChild(fechaTitulo);
    titulosNuevos.appendChild(pminTitulo);
    titulosNuevos.appendChild(ppromTitulo);
    titulosNuevos.appendChild(pmaxTitulo);
    titulosNuevos.appendChild(califPromTitulo);

    titulosTabla.appendChild(titulosNuevos);

}




function renderizarTablaAgrupadaDia(datosAgregados) {
    var cuerpoTabla = document.getElementById("cuerpoResultados");

    cuerpoTabla.innerHTML = "";
    for (var entradaAgregada of datosAgregados) {
        var fila = document.createElement("tr");

        var divFecha = document.createElement("div");
        divFecha.className = "contenidoCeldaFecha";
        divFecha.textContent = entradaAgregada.fecha;
        var fechaCelda = document.createElement("td");
        fechaCelda.setAttribute('data-tooltip', divFecha.textContent);
        fechaCelda.appendChild(divFecha);

        var divPrecioMin = document.createElement("div");
        divPrecioMin.className = "contenidoCeldaPrecio";
        var precioMinArreglado = "₡" + agregarSeparador(entradaAgregada.minPrecio);
        divPrecioMin.textContent = precioMinArreglado;
        divPrecioMin.classList.add("precioAgrupado");
        var precioMinCelda = document.createElement("td");
        precioMinCelda.classList.add("precioAgrupado");
        precioMinCelda.setAttribute('data-tooltip', precioMinArreglado);
        precioMinCelda.appendChild(divPrecioMin);

        var divPrecioProm = document.createElement("div");
        divPrecioProm.className = "contenidoCeldaPrecio";
        var precioPromArreglado = "₡" + agregarSeparador(entradaAgregada.promedioPrecio);
        divPrecioProm.textContent = precioPromArreglado;
        divPrecioProm.classList.add("precioAgrupado");
        var precioPromCelda = document.createElement("td");
        precioPromCelda.classList.add("precioAgrupado");
        precioPromCelda.setAttribute('data-tooltip', precioPromArreglado);
        precioPromCelda.appendChild(divPrecioProm);

        var divPrecioMax = document.createElement("div");
        divPrecioMax.className = "contenidoCeldaPrecio";
        var precioMaxArreglado = "₡" + agregarSeparador(entradaAgregada.maxPrecio);
        divPrecioMax.textContent = precioMaxArreglado;
        divPrecioMax.classList.add("precioAgrupado");
        var precioMaxCelda = document.createElement("td");
        precioMaxCelda.classList.add("precioAgrupado");
        precioMaxCelda.setAttribute('data-tooltip', precioMaxArreglado);
        precioMaxCelda.appendChild(divPrecioMax);

        var divCalificacion = document.createElement("div");
        divCalificacion.className = "contenidoCeldaCalificacion";
        var promedioCalificacionText = entradaAgregada.promedioCalificacion;
        divCalificacion.textContent = promedioCalificacionText !== 0 ? promedioCalificacionText : "Sin calificacion";
        var calificacionCelda = document.createElement("td");
        calificacionCelda.setAttribute('data-tooltip', divCalificacion.textContent);
        calificacionCelda.appendChild(divCalificacion);

        fila.appendChild(fechaCelda);
        fila.appendChild(precioMinCelda);
        fila.appendChild(precioPromCelda);
        fila.appendChild(precioMaxCelda);
        fila.appendChild(calificacionCelda);

        cuerpoTabla.appendChild(fila);
    }
}


function generarDatosDia(datos) {
    var titulosTabla = document.getElementById("TitulosTabla");

    titulosTabla.innerHTML = "";


    var titulosNuevos = document.createElement("tr");

    var fechaTitulo = document.createElement("th");
    fechaTitulo.textContent = "Fecha";
    fechaTitulo.className = "ContenidoTitulo";
    fechaTitulo.style.paddingLeft = "10px";

    titulosAgrupados(titulosTabla, titulosNuevos, fechaTitulo);

    var datosAgrupados = {};
    for (var dato in datos) {
        if (datos[dato].creacion !== undefined && datos[dato].creacion !== "") {
            var fecha = formatearFecha(datos[dato]);

            if (!datosAgrupados[fecha]) {
                datosAgrupados[fecha] = {
                    precios: [],
                    calificaciones: [],
                };
            }

            var precio = parseFloat(datos[dato].precio);

            datosAgrupados[fecha].precios.push(precio);

            if (datos[dato].calificacion != null) {
                datosAgrupados[fecha].calificaciones.push(parseFloat(datos[dato].calificacion));
            } else {
                datosAgrupados[fecha].calificaciones.push(0);
            }
        }
    }

    var datosAgregados = [];

    for (var fecha in datosAgrupados) {
        var precios = datosAgrupados[fecha].precios;
        var calificaciones = datosAgrupados[fecha].calificaciones;

        var minPrecio = Math.min(...precios);
        var promedioPrecio = Math.round(precios.reduce((acc, val) => acc + val, 0) / precios.length);
        var maxPrecio = Math.max(...precios);

        var totalCalificaciones = calificaciones.length;

        var sumCalificaciones = calificaciones.reduce((acc, val) => acc + val, 0);

        var promedioCalificacion = totalCalificaciones > 0
            ? sumCalificaciones / totalCalificaciones
            : 0;

        var entradaAgregada = {
            fecha: fecha,
            minPrecio: minPrecio,
            promedioPrecio: promedioPrecio,
            maxPrecio: maxPrecio,
            promedioCalificacion: promedioCalificacion,
        };

        datosAgregados.push(entradaAgregada);
    }
    return datosAgregados;
}


function renderizarTablaAgrupadaSemana(datosAgregados) {
    var cuerpoTabla = document.getElementById("cuerpoResultados");

    cuerpoTabla.innerHTML = "";
    for (var entradaAgregada of datosAgregados) {
        var fila = document.createElement("tr");

        var divFecha = document.createElement("div");
        divFecha.className = "contenidoCeldaFecha";
        divFecha.textContent = entradaAgregada.fecha;
        var fechaCelda = document.createElement("td");
        fechaCelda.setAttribute('data-tooltip', divFecha.textContent);
        fechaCelda.appendChild(divFecha);

        var divPrecioMin = document.createElement("div");
        divPrecioMin.className = "contenidoCeldaPrecio";
        var precioMinArreglado = "₡" + agregarSeparador(entradaAgregada.minPrecio);
        divPrecioMin.textContent = precioMinArreglado;
        divPrecioMin.classList.add("precioAgrupado");
        var precioMinCelda = document.createElement("td");
        precioMinCelda.classList.add("precioAgrupado");
        precioMinCelda.setAttribute('data-tooltip', precioMinArreglado);
        precioMinCelda.appendChild(divPrecioMin);

        var divPrecioProm = document.createElement("div");
        divPrecioProm.className = "contenidoCeldaPrecio";
        var precioPromArreglado = "₡" + agregarSeparador(entradaAgregada.promedioPrecio);
        divPrecioProm.textContent = precioPromArreglado;
        divPrecioProm.classList.add("precioAgrupado");
        var precioPromCelda = document.createElement("td");
        precioPromCelda.classList.add("precioAgrupado");
        precioPromCelda.setAttribute('data-tooltip', precioPromArreglado);
        precioPromCelda.appendChild(divPrecioProm);

        var divPrecioMax = document.createElement("div");
        divPrecioMax.className = "contenidoCeldaPrecio";
        var precioMaxArreglado = "₡" + agregarSeparador(entradaAgregada.maxPrecio);
        divPrecioMax.textContent = precioMaxArreglado;
        divPrecioMax.classList.add("precioAgrupado");
        var precioMaxCelda = document.createElement("td");
        precioMaxCelda.classList.add("precioAgrupado");
        precioMaxCelda.setAttribute('data-tooltip', precioMaxArreglado);
        precioMaxCelda.appendChild(divPrecioMax);

        var divCalificacion = document.createElement("div");
        divCalificacion.className = "contenidoCeldaCalificacion";
        var promedioCalificacionText = entradaAgregada.promedioCalificacion;
        divCalificacion.textContent = promedioCalificacionText !== 0 ? promedioCalificacionText : "Sin calificacion";
        var calificacionCelda = document.createElement("td");
        calificacionCelda.setAttribute('data-tooltip', divCalificacion.textContent);
        calificacionCelda.appendChild(divCalificacion);

        fila.appendChild(fechaCelda);
        fila.appendChild(precioMinCelda);
        fila.appendChild(precioPromCelda);
        fila.appendChild(precioMaxCelda);
        fila.appendChild(calificacionCelda);

        cuerpoTabla.appendChild(fila);
    }
}


function generarDatosSemana(datos) {
    var titulosTabla = document.getElementById("TitulosTabla");

    titulosTabla.innerHTML = "";


    var titulosNuevos = document.createElement("tr");

    var fechaTitulo = document.createElement("th");
    fechaTitulo.textContent = "Fecha";
    fechaTitulo.className = "ContenidoTitulo";
    fechaTitulo.style.paddingLeft = "10px";

    titulosAgrupados(titulosTabla, titulosNuevos, fechaTitulo);

    var datosAgrupados = {};
    for (var dato in datos) {
        if (datos[dato].creacion !== undefined && datos[dato].creacion !== "") {
            var fecha = formatearFecha(datos[dato]);

            if (!datosAgrupados[fecha]) {
                datosAgrupados[fecha] = {
                    precios: [],
                    calificaciones: [],
                };
            }

            var precio = parseFloat(datos[dato].precio);

            datosAgrupados[fecha].precios.push(precio);

            if (datos[dato].calificacion != null) {
                datosAgrupados[fecha].calificaciones.push(parseFloat(datos[dato].calificacion));
            } else {
                datosAgrupados[fecha].calificaciones.push(0);
            }
        }
    }

    var datosAgregados = [];

    for (var fecha in datosAgrupados) {
        var precios = datosAgrupados[fecha].precios;
        var calificaciones = datosAgrupados[fecha].calificaciones;

        var minPrecio = Math.min(...precios);
        var promedioPrecio = Math.round(precios.reduce((acc, val) => acc + val, 0) / precios.length);
        var maxPrecio = Math.max(...precios);

        var totalCalificaciones = calificaciones.length;

        var sumCalificaciones = calificaciones.reduce((acc, val) => acc + val, 0);

        var promedioCalificacion = totalCalificaciones > 0
            ? sumCalificaciones / totalCalificaciones
            : 0;

        var entradaAgregada = {
            fecha: fecha,
            minPrecio: minPrecio,
            promedioPrecio: promedioPrecio,
            maxPrecio: maxPrecio,
            promedioCalificacion: promedioCalificacion,
        };

        datosAgregados.push(entradaAgregada);
    }
    return datosAgregados;
}


function renderizarTablaAgrupadaMes(datosAgregados) {
    var cuerpoTabla = document.getElementById("cuerpoResultados");

    cuerpoTabla.innerHTML = "";
    for (var entradaAgregada of datosAgregados) {
        var fila = document.createElement("tr");

        var divFecha = document.createElement("div");
        divFecha.className = "contenidoCeldaFecha";
        divFecha.textContent = entradaAgregada.fecha;
        var fechaCelda = document.createElement("td");
        fechaCelda.setAttribute('data-tooltip', divFecha.textContent);
        fechaCelda.appendChild(divFecha);

        var divPrecioMin = document.createElement("div");
        divPrecioMin.className = "contenidoCeldaPrecio";
        var precioMinArreglado = "₡" + agregarSeparador(entradaAgregada.minPrecio);
        divPrecioMin.textContent = precioMinArreglado;
        divPrecioMin.classList.add("precioAgrupado");
        var precioMinCelda = document.createElement("td");
        precioMinCelda.classList.add("precioAgrupado");
        precioMinCelda.setAttribute('data-tooltip', precioMinArreglado);
        precioMinCelda.appendChild(divPrecioMin);

        var divPrecioProm = document.createElement("div");
        divPrecioProm.className = "contenidoCeldaPrecio";
        var precioPromArreglado = "₡" + agregarSeparador(entradaAgregada.promedioPrecio);
        divPrecioProm.textContent = precioPromArreglado;
        divPrecioProm.classList.add("precioAgrupado");
        var precioPromCelda = document.createElement("td");
        precioPromCelda.classList.add("precioAgrupado");
        precioPromCelda.setAttribute('data-tooltip', precioPromArreglado);
        precioPromCelda.appendChild(divPrecioProm);

        var divPrecioMax = document.createElement("div");
        divPrecioMax.className = "contenidoCeldaPrecio";
        var precioMaxArreglado = "₡" + agregarSeparador(entradaAgregada.maxPrecio);
        divPrecioMax.textContent = precioMaxArreglado;
        divPrecioMax.classList.add("precioAgrupado");
        var precioMaxCelda = document.createElement("td");
        precioMaxCelda.classList.add("precioAgrupado");
        precioMaxCelda.setAttribute('data-tooltip', precioMaxArreglado);
        precioMaxCelda.appendChild(divPrecioMax);

        var divCalificacion = document.createElement("div");
        divCalificacion.className = "contenidoCeldaCalificacion";
        var promedioCalificacionText = entradaAgregada.promedioCalificacion;
        divCalificacion.textContent = promedioCalificacionText !== 0 ? promedioCalificacionText : "Sin calificacion";
        var calificacionCelda = document.createElement("td");
        calificacionCelda.setAttribute('data-tooltip', divCalificacion.textContent);
        calificacionCelda.appendChild(divCalificacion);

        fila.appendChild(fechaCelda);
        fila.appendChild(precioMinCelda);
        fila.appendChild(precioPromCelda);
        fila.appendChild(precioMaxCelda);
        fila.appendChild(calificacionCelda);

        cuerpoTabla.appendChild(fila);
    }
}


function generarDatosMes(datos) {
    var titulosTabla = document.getElementById("TitulosTabla");

    titulosTabla.innerHTML = "";


    var titulosNuevos = document.createElement("tr");

    var fechaTitulo = document.createElement("th");
    fechaTitulo.textContent = "Fecha";
    fechaTitulo.className = "ContenidoTitulo";
    fechaTitulo.style.paddingLeft = "10px";

    titulosAgrupados(titulosTabla, titulosNuevos, fechaTitulo);

    var datosAgrupados = {};
    for (var dato in datos) {
        if (datos[dato].creacion !== undefined && datos[dato].creacion !== "") {
            var fecha = formatearFecha(datos[dato]);

            if (!datosAgrupados[fecha]) {
                datosAgrupados[fecha] = {
                    precios: [],
                    calificaciones: [],
                };
            }

            var precio = parseFloat(datos[dato].precio);

            datosAgrupados[fecha].precios.push(precio);

            if (datos[dato].calificacion != null) {
                datosAgrupados[fecha].calificaciones.push(parseFloat(datos[dato].calificacion));
            } else {
                datosAgrupados[fecha].calificaciones.push(0);
            }
        }
    }

    var datosAgregados = [];

    for (var fecha in datosAgrupados) {
        var precios = datosAgrupados[fecha].precios;
        var calificaciones = datosAgrupados[fecha].calificaciones;

        var minPrecio = Math.min(...precios);
        var promedioPrecio = Math.round(precios.reduce((acc, val) => acc + val, 0) / precios.length);
        var maxPrecio = Math.max(...precios);

        var totalCalificaciones = calificaciones.length;

        var sumCalificaciones = calificaciones.reduce((acc, val) => acc + val, 0);

        var promedioCalificacion = totalCalificaciones > 0
            ? sumCalificaciones / totalCalificaciones
            : 0;

        var entradaAgregada = {
            fecha: fecha,
            minPrecio: minPrecio,
            promedioPrecio: promedioPrecio,
            maxPrecio: maxPrecio,
            promedioCalificacion: promedioCalificacion,
        };

        datosAgregados.push(entradaAgregada);
    }
    return datosAgregados;
}



function renderizarTablaAgrupadaAno(datosAgregados) {
    var cuerpoTabla = document.getElementById("cuerpoResultados");

    cuerpoTabla.innerHTML = "";
    for (var entradaAgregada of datosAgregados) {
        var fila = document.createElement("tr");

        var divFecha = document.createElement("div");
        divFecha.className = "contenidoCeldaFecha";
        divFecha.textContent = entradaAgregada.fecha;
        var fechaCelda = document.createElement("td");
        fechaCelda.setAttribute('data-tooltip', divFecha.textContent);
        fechaCelda.appendChild(divFecha);

        var divPrecioMin = document.createElement("div");
        divPrecioMin.className = "contenidoCeldaPrecio";
        var precioMinArreglado = "₡" + agregarSeparador(entradaAgregada.minPrecio);
        divPrecioMin.textContent = precioMinArreglado;
        divPrecioMin.classList.add("precioAgrupado");
        var precioMinCelda = document.createElement("td");
        precioMinCelda.classList.add("precioAgrupado");
        precioMinCelda.setAttribute('data-tooltip', precioMinArreglado);
        precioMinCelda.appendChild(divPrecioMin);

        var divPrecioProm = document.createElement("div");
        divPrecioProm.className = "contenidoCeldaPrecio";
        var precioPromArreglado = "₡" + agregarSeparador(entradaAgregada.promedioPrecio);
        divPrecioProm.textContent = precioPromArreglado;
        divPrecioProm.classList.add("precioAgrupado");
        var precioPromCelda = document.createElement("td");
        precioPromCelda.classList.add("precioAgrupado");
        precioPromCelda.setAttribute('data-tooltip', precioPromArreglado);
        precioPromCelda.appendChild(divPrecioProm);

        var divPrecioMax = document.createElement("div");
        divPrecioMax.className = "contenidoCeldaPrecio";
        var precioMaxArreglado = "₡" + agregarSeparador(entradaAgregada.maxPrecio);
        divPrecioMax.textContent = precioMaxArreglado;
        divPrecioMax.classList.add("precioAgrupado");
        var precioMaxCelda = document.createElement("td");
        precioMaxCelda.classList.add("precioAgrupado");
        precioMaxCelda.setAttribute('data-tooltip', precioMaxArreglado);
        precioMaxCelda.appendChild(divPrecioMax);

        var divCalificacion = document.createElement("div");
        divCalificacion.className = "contenidoCeldaCalificacion";
        var promedioCalificacionText = entradaAgregada.promedioCalificacion;
        divCalificacion.textContent = promedioCalificacionText !== 0 ? promedioCalificacionText : "Sin calificacion";
        var calificacionCelda = document.createElement("td");
        calificacionCelda.setAttribute('data-tooltip', divCalificacion.textContent);
        calificacionCelda.appendChild(divCalificacion);

        fila.appendChild(fechaCelda);
        fila.appendChild(precioMinCelda);
        fila.appendChild(precioPromCelda);
        fila.appendChild(precioMaxCelda);
        fila.appendChild(calificacionCelda);

        cuerpoTabla.appendChild(fila);
    }
}


function generarDatosAno(datos) {
    var titulosTabla = document.getElementById("TitulosTabla");

    titulosTabla.innerHTML = "";


    var titulosNuevos = document.createElement("tr");

    var fechaTitulo = document.createElement("th");
    fechaTitulo.textContent = "Fecha";
    fechaTitulo.className = "ContenidoTitulo";
    fechaTitulo.style.paddingLeft = "10px";

    titulosAgrupados(titulosTabla, titulosNuevos, fechaTitulo);

    var datosAgrupados = {};
    for (var dato in datos) {
        if (datos[dato].creacion !== undefined && datos[dato].creacion !== "") {
            var fecha = formatearFecha(datos[dato]);

            if (!datosAgrupados[fecha]) {
                datosAgrupados[fecha] = {
                    precios: [],
                    calificaciones: [],
                };
            }

            var precio = parseFloat(datos[dato].precio);

            datosAgrupados[fecha].precios.push(precio);

            if (datos[dato].calificacion != null) {
                datosAgrupados[fecha].calificaciones.push(parseFloat(datos[dato].calificacion));
            } else {
                datosAgrupados[fecha].calificaciones.push(0);
            }
        }
    }

    var datosAgregados = [];

    for (var fecha in datosAgrupados) {
        var precios = datosAgrupados[fecha].precios;
        var calificaciones = datosAgrupados[fecha].calificaciones;

        var minPrecio = Math.min(...precios);
        var promedioPrecio = Math.round(precios.reduce((acc, val) => acc + val, 0) / precios.length);
        var maxPrecio = Math.max(...precios);

        var totalCalificaciones = calificaciones.length;

        var sumCalificaciones = calificaciones.reduce((acc, val) => acc + val, 0);

        var promedioCalificacion = totalCalificaciones > 0
            ? sumCalificaciones / totalCalificaciones
            : 0;

        var entradaAgregada = {
            fecha: fecha,
            minPrecio: minPrecio,
            promedioPrecio: promedioPrecio,
            maxPrecio: maxPrecio,
            promedioCalificacion: promedioCalificacion,
        };

        datosAgregados.push(entradaAgregada);
    }
    return datosAgregados;
}


function agrupar(presionado, numeroPagina) {
    resultadosViejos = resultados;

    if (presionado === "dia") {
        resultados = generarDatosDia(resultados);
        resultadosPaginados = paginar(numeroPagina);
        renderizarPaginacion();
        renderizarTablaAgrupadaDia(resultadosPaginados);

    } else {
        if (presionado === "semana") {
            resultados = generarDatosSemana(resultados);
            resultadosPaginados = paginar(numeroPagina);
            renderizarPaginacion();
            renderizarTablaAgrupadaSemana(resultadosPaginados);
        } else {
            if (presionado === "mes") {
                resultados = generarDatosMes(resultados);
                resultadosPaginados = paginar(numeroPagina);
                renderizarPaginacion();
                renderizarTablaAgrupadaMes(resultadosPaginados);
            } else {
                if (presionado === "ano") {
                    resultados = generarDatosAno(resultados);
                    resultadosPaginados = paginar(numeroPagina);
                    renderizarPaginacion();
                    renderizarTablaAgrupadaAno(resultadosPaginados);

                }
            }
        }
    }
    resultados = resultadosViejos;
}


function pasarPagina(numeroPagina, presionado) {
    if (presionado === "dia" || presionado === "semana" || presionado === "mes" || presionado === "ano") {
        agrupar(presionado, numeroPagina);
    } else {
        resultadosPaginados = paginar(numeroPagina);
        renderizarPaginacion();
        renderizarTabla(resultadosPaginados);
    }
    window.scrollTo(0, 0);
}


function renderizarPaginacion() {
    // Renderizar los botones de Siguiente Pagina y Pagina Anterior
    renderizarBotonesSiguienteAnterior();

    var paginacionContenedor = document.getElementById("textoPaginacion");

    // Renderizar los numeros de en medio mostrados en la barra de paginacion
    var limites = renderizarNumerosPaginaIntermedios(paginacionContenedor);
    renderizarPrimerNumeroPagina(paginacionContenedor, limites.paginaInicial);
    renderizarUltimoNumeroPagina(paginacionContenedor, limites.paginaFinal);
}

function renderizarUltimoNumeroPagina(paginacionContenedor, paginaFinal) {
    if (paginaFinal < resultadosPaginados.PaginasTotales) {
        const finalElipsis = document.createElement("span");
        // Agregar ... a la ultima pagina si fuera necesario

        if (paginaFinal !== resultadosPaginados.PaginasTotales - 1) finalElipsis.textContent = " ... ";
        else finalElipsis.textContent = " ";
        paginacionContenedor.appendChild(finalElipsis);

        const linkUltimaPagina = document.createElement("span");
        linkUltimaPagina.textContent = resultadosPaginados.PaginasTotales;
        linkUltimaPagina.classList.add("pagina-seleccionable");

        linkUltimaPagina.addEventListener("click", function () {
            pasarPagina(resultadosPaginados.PaginasTotales);
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

    let paginaInicial = Math.max(1, resultadosPaginados.IndicePagina - Math.floor(numeroDeLinksDePaginas / 2));
    let paginaFinal = Math.min(resultadosPaginados.PaginasTotales, paginaInicial + numeroDeLinksDePaginas - 1);

    if (paginaFinal - paginaInicial + 1 < numeroDeLinksDePaginas) {
        paginaInicial = Math.max(1, paginaFinal - numeroDeLinksDePaginas + 1);
    }

    for (let pagina = paginaInicial; pagina <= paginaFinal; pagina++) {
        const paginaLink = document.createElement("span");
        paginaLink.textContent = pagina;

        if (pagina === resultadosPaginados.IndicePagina) {
            paginaLink.classList.add("pagina");
        } else {
            paginaLink.classList.add("pagina-seleccionable");
            paginaLink.addEventListener("click", function () {
                pasarPagina(pagina, presionado);
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
    var botonPaginaPrevia = document.getElementById("paginaPrevia");
    var botonSinPaginaPrevia = document.getElementById("sinPaginaPrevia");
    var botonPaginaSiguiente = document.getElementById("paginaSiguiente");
    var botonSinPaginaSiguiente = document.getElementById("sinPaginaSiguiente");

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