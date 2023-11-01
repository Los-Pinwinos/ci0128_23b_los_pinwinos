function actualizarNoCalificaciones(estado) {
    var estrella0 = document.getElementById("no-rate");
    estrella0.checked = estado;
}

function calificarRegistro(calificacion) {
    actualizarNoCalificaciones(false);

    alert(calificacion);

    fetch(`/DetallesRegistro/DetallesRegistro?handler=Calificar&calificacion=${calificacion}`);
}