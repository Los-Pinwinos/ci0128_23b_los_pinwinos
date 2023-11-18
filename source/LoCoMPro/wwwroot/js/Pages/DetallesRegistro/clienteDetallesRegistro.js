async function actualizarNoCalificaciones(estado) {
    var estrella0 = document.getElementById("no-rate");
    estrella0.checked = estado;
}

function actualizarUltimaCalificacion(calificacion) {
    var labelUltimaCalificacion = document.getElementById("UltimaCalificacion");
    labelUltimaCalificacion.hidden = false;
    labelUltimaCalificacion.innerHTML = "Ha calificado con: " + calificacion;

    var boton = document.getElementById("BotonActualizar");
    boton.click();
}

async function calificarRegistro(calificacion) {
    actualizarNoCalificaciones(false);

    try {
        const response = await fetch(`/DetallesRegistro/DetallesRegistro?handler=Calificar&calificacion=${calificacion}`);

        if (response.ok) {
            actualizarUltimaCalificacion(calificacion);
        } else {
            console.error("Error: ", response.status);
        }
    } catch (error) {
        console.error("Error: ", error);
    }
}
