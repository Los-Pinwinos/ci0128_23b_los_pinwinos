async function actualizarNoCalificaciones(estado) {
    var estrella0 = document.getElementById("no-rate");
    estrella0.checked = estado;
}

async function calificarRegistro(calificacion) {
    actualizarNoCalificaciones(false);

    try {
        const response = await fetch(`/DetallesRegistro/DetallesRegistro?handler=Calificar&calificacion=${calificacion}`);

        if (response.ok) {
            var labelUltimaCalificacion = document.getElementById("UltimaCalificacion");
            labelUltimaCalificacion.hidden = false;
            labelUltimaCalificacion.innerHTML = "Su última calificación fue: " + calificacion;
        } else {
            console.error("Error: ", response.status);
        }
    } catch (error) {
        console.error("Error: ", error);
    }
}
