function actualizarNoCalificaciones(estado) {
    var estrella0 = document.getElementById("no-rate");
    estrella0.checked = estado;
}

/*function calificarRegistro(calificacion, creacion, usuario) {
    actualizarNoCalificaciones(false);
    alert("voy al fetch");

    fetch(`/DetallesRegistro/DetallesRegistro?handler=Calificar&calificacionStr=${calificacion}&creacionStr=${creacion}&usuarioCreador=${usuario}`);
}*/

/*
function calificarRegistro(calificacion) {
    actualizarNoCalificaciones(false);

    const usuario = document.getElementById("usuario");
    const creacion = document.getElementById("creacionRegistroStr");
    alert("fetcheando " + usuario.value + " " + creacion.value);

    fetch(`/DetallesRegistro/DetallesRegistro?handler=Calificar&calificacionStr=${calificacion}`);
}*/

const usuarioInput = document.getElementById("usuario");
const creacion = document.getElementById("creacionRegistroStr");

function calificarRegistro(calificacion) {

    // TODO(Angie): Borrar
    alert("fetcheando " + usuario.value + " " + creacion.value);

    fetch(`/DetallesRegistro/DetallesRegistro?handler=Calificar&calificacionStr=${calificacion}&creacionStr=${creacion.value}&usuarioCreador=${usuario.value}`);
}

