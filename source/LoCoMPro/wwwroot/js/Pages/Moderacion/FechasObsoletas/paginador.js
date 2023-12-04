// Paginador
class Paginador {
    constructor(configuracion) {
        this.usado = false; // Un booleano que indica si el paginador se ha utilizado.
        this.configuracion = configuracion; // La configuración para la paginación (presumiblemente el tamaño de página).
    }

    // Método para realizar la paginación de la entrada de búsqueda en una página específica.
    paginar(entradaIQ, pagina) {
        this.usado = true;
        const tamPagina = this.configuracion;
        return ListaPaginada.Crear(entradaIQ, pagina, tamPagina); // Se crea una instancia de ListaPaginada.
    }

    // Método para restablecer el estado de "usado" del paginador.
    resetearUso() {
        this.usado = false;
    }

    // Método para obtener la cantidad de resultados por página con la que se configuró el paginador
    resultadosPorPagina() {
        return this.configuracion
    }
}

// Lista paginada
class ListaPaginada extends Array {
    constructor(elementos, cantidad, indicePagina, tamPagina) {
        super(...elementos);
        this.Cantidad = cantidad; // La cantidad total de elementos en la lista.
        this.IndicePagina = indicePagina; // El número de página actual.
        this.PaginasTotales = Math.ceil(cantidad / tamPagina); // La cantidad total de páginas basada en el tamaño de página.
    }

    get TienePaginaPrevia() {
        return this.IndicePagina > 1; // Devuelve true si hay una página anterior.
    }

    get TieneProximaPagina() {
        return this.IndicePagina < this.PaginasTotales; // Devuelve true si hay una página siguiente.
    }

    // Método estático para crear una instancia de ListaPaginada de forma sincrónica.
    static Crear(fuente, indicePagina, tamPagina) {
        const cantidad = fuente.length;
        const elementos = fuente.slice((indicePagina - 1) * tamPagina, indicePagina * tamPagina);
        return new ListaPaginada(elementos, cantidad, indicePagina, tamPagina);
    }
}