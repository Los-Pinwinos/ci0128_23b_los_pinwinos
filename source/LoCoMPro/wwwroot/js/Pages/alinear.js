function alinearContenedoresDeBarrasDeTexto(nombreDelContenedor, extra) {
    // Obtener todos los labels en los contenedores
    const labels = document.querySelectorAll(nombreDelContenedor + " label");

    // Encontrar la longitud mínima en los labels (Basandose en sus caracteres)
    let longitudMinima = Number.MAX_VALUE;
    labels.forEach((label) => {
        const longitudLabel = label.offsetWidth;
        if (longitudLabel < longitudMinima) {
            longitudMinima = longitudLabel;
        }
    });

    // Establecer el margen derecho de cada contenedor
    // Por cada label
    labels.forEach((label) => {
        // Obtener la longitud del label
        const longitudLabel = label.offsetWidth;
        // Obtener el contenedor padre
        const contenedor = label.closest(nombreDelContenedor);
        // Calcular el margen
        const nuevoMargen = longitudLabel - longitudMinima + extra;
        // Establecer el nuevo margen como un string
        contenedor.style.marginRight = `${nuevoMargen}px`;
    });
}