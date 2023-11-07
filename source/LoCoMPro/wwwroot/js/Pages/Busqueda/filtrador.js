// Filtrador
class FiltradorDeBusqueda {
    constructor(provincias = null, cantones = null, tiendas = null, marcas = null, categorias = null, precioMin = null, precioMax = null, fechaMin = null, fechaMax = null) {
        this.usado = false; // Un booleano que indica si el filtrador se ha utilizado.
        this.provincias = provincias ? provincias : []; // Una lista de provincias para filtrar.
        this.cantones = cantones ? cantones : []; // Una lista de cantones para filtrar.
        this.tiendas = tiendas ? tiendas : []; // Una lista de tiendas para filtrar.
        this.marcas = marcas ? marcas : []; // Una lista de marcas para filtrar.
        this.categorias = categorias ? categorias : []; // Una lista de categorias para filtrar.
        this.precioMin = precioMin; // Valor mínimo para filtrar por precio
        this.precioMax = precioMax; // Valor máximo para filtrar por precio
        this.fechaMin = fechaMin; // Valor mínimo para filtrar por fecha
        this.fechaMax = fechaMax; // Valor máximo para filtrar por fecha
    }

    // Métodos para establecer los filtros de búsqueda para provincias, cantones, tiendas y marcas.
    setFiltroProvincias(provincias) {
        this.provincias = provincias;
    }

    setFiltroCantones(cantones) {
        this.cantones = cantones;
    }

    setFiltroTiendas(tiendas) {
        this.tiendas = tiendas;
    }

    setFiltroMarcas(marcas) {
        this.marcas = marcas;
    }

    setFiltroCategorias(categorias) {
        this.categorias = categorias;
    }

    setPrecioMinimo(precioMin) {
        this.precioMin = precioMin;
    }

    setPrecioMaximo(precioMax) {
        this.precioMax = precioMax;
    }

    setFechaMinimo(fechaMin) {
        this.fechaMin = fechaMin;
    }

    setFechaMaxima(fechaMax) {
        this.fechaMax = fechaMax;
    }

    // Método principal para aplicar los filtros a una entrada de búsqueda.
    filtrar(entradaIQ) {
        this.usado = true; // Se marca el filtrador como utilizado.
        let resultadosIQ = entradaIQ;

        // Se aplican los filtros en el orden especificado.
        resultadosIQ = this.filtrarProvincia(resultadosIQ);
        resultadosIQ = this.filtrarCanton(resultadosIQ);
        resultadosIQ = this.filtrarTienda(resultadosIQ);
        resultadosIQ = this.filtrarMarca(resultadosIQ);
        resultadosIQ = this.filtrarCategorias(resultadosIQ);
        resultadosIQ = this.filtrarPrecioMin(resultadosIQ);
        resultadosIQ = this.filtrarPrecioMax(resultadosIQ);
        resultadosIQ = this.filtrarFechaMin(resultadosIQ);
        resultadosIQ = this.filtrarFechaMax(resultadosIQ);

        // Se devuelve la lista de resultados filtrados.
        return resultadosIQ;
    }

    // Métodos de filtrado específicos para provincias, cantones, tiendas y marcas.
    filtrarProvincia(entradaIQ) {
        if (this.provincias.length > 0) {
            const filtro = this.provincias;
            entradaIQ = entradaIQ.filter(r => filtro.includes(r.provincia));
        }
        return entradaIQ;
    }

    filtrarCanton(entradaIQ) {
        if (this.cantones.length > 0) {
            const filtro = this.cantones;
            entradaIQ = entradaIQ.filter(r => filtro.includes(r.canton));
        }
        return entradaIQ;
    }

    filtrarTienda(entradaIQ) {
        if (this.tiendas.length > 0) {
            const filtro = this.tiendas;
            entradaIQ = entradaIQ.filter(r => filtro.includes(r.tienda));
        }
        return entradaIQ;
    }

    filtrarMarca(entradaIQ) {
        if (this.marcas.length > 0) {
            const filtro = this.marcas;
            entradaIQ = entradaIQ.filter(r => filtro.includes(r.marca));
        }
        return entradaIQ;
    }

    filtrarCategorias(entradaIQ) {
        if (this.categorias.length > 0) {
            const filtro = this.categorias;
            entradaIQ = entradaIQ.filter(r => filtro.includes(r.categoria));
        }
        return entradaIQ;
    }

    filtrarPrecioMin(entradaIQ) {
        if (this.precioMin !== "") {
            const filtro = parseFloat(this.precioMin, 10);
            entradaIQ = entradaIQ.filter(r => r.precio >= filtro);
        }
        return entradaIQ;
    }

    filtrarPrecioMax(entradaIQ) {
        if (this.precioMax !== "") {
            const filtro = parseFloat(this.precioMax, 10);
            entradaIQ = entradaIQ.filter(r => r.precio <= filtro);
        }
        return entradaIQ;
    }

    filtrarFechaMin(entradaIQ) {
        if (this.fechaMin !== "") {
            const filtro = this.convertirAFecha(this.fechaMin);
            entradaIQ = entradaIQ.filter(r => this.convertirAFecha(r.fecha) >= filtro);
        }
        return entradaIQ;
    }

    filtrarFechaMax(entradaIQ) {
        if (this.fechaMax !== "") {
            const filtro = this.convertirAFecha(this.fechaMax);
            entradaIQ = entradaIQ.filter(r => this.convertirAFecha(r.fecha) <= filtro);
        }
        return entradaIQ;
    }

    convertirAFecha(fecha) {
        const año = fecha.slice(0, 4);
        const mes = fecha.slice(5, 7);
        const dia = fecha.slice(8, 10);
        return new Date(año, mes - 1, dia, 0, 0, 0, 0);
    }

    // Un método para restablecer el estado de "usado" del filtrador.
    resetearUso() {
        this.usado = false;
    }
}
