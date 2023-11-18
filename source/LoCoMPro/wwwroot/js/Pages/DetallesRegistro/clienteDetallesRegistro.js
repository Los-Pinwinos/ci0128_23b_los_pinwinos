async function actualizarNoCalificaciones(estado) {
    var estrella0 = document.getElementById("no-rate");
    estrella0.checked = estado;
}

async function calificarRegistro(calificacion) {
    actualizarNoCalificaciones(false);

    try {
        const response = await fetch(`/DetallesRegistro/DetallesRegistro?handler=Calificar&calificacion=${calificacion}`);

        if (response.ok) {
            actualizarCalificacion(calificacion);
        } else {
            console.error("Error: ", response.status);
        }
    } catch (error) {
        console.error("Error: ", error);
    }
}
