function actualizarNoCalificaciones(estado) {
    var estrella0 = document.getElementById("no-rate");
    estrella0.checked = estado;
}

function calificarRegistro(calificacion, creacion, usuario) {
    actualizarNoCalificaciones(false);
    alert("voy al fetch");

    fetch(`/DetallesRegistro/DetallesRegistro?handler=Calificar&calificacionStr=${calificacion}&creacionStr=${creacion}&usuarioCreador=${usuario}`);
}