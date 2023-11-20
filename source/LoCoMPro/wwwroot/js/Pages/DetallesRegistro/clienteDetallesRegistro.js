async function actualizarNoCalificaciones(estado) {
    var estrella0 = document.getElementById("no-rate");
    estrella0.checked = estado;
}

function actualizarUltimaCalificacion(calificacion) {
    var labelUltimaCalificacion = document.getElementById("UltimaCalificacion");
    labelUltimaCalificacion.hidden = false;
    labelUltimaCalificacion.innerHTML = "Ha calificado con: " + calificacion;
}

function actualizarPromedioCalificacion(promedio) {
    var labelCalificacion = document.getElementById("CalificacionTotal");
    labelCalificacion.innerHTML = promedio + " de 5,0";
}

function actualizarConteo(conteo) {
    var labelConteo = document.getElementById("CantidadCalificacionesTotal");
    labelConteo.innerHTML = "(" + conteo + ")";
}

async function calificarRegistro(calificacion) {
    actualizarNoCalificaciones(false);

    try {
        const response = await fetch(`/DetallesRegistro/DetallesRegistro?handler=Calificar&calificacion=${calificacion}`);

        if (response.ok) {
            var resultado = await response.json();
            const conteoNuevo = resultado.conteo;
            const calificacionNueva = resultado.calificacion;

            actualizarUltimaCalificacion(calificacion);
            actualizarPromedioCalificacion(calificacionNueva);
            actualizarConteo(conteoNuevo);
        } else {
            console.error("Error: ", response.status);
        }
    } catch (error) {
        console.error("Error: ", error);
    }
}
