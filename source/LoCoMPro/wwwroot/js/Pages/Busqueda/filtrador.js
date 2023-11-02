// Filtrador
class FiltradorDeBusqueda {
    constructor(provincias = null, cantones = null, tiendas = null, marcas = null, categorias = null) {
        this.usado = false; // Un booleano que indica si el filtrador se ha utilizado.
        this.provincias = provincias ? provincias : []; // Una lista de provincias para filtrar.
        this.cantones = cantones ? cantones : []; // Una lista de cantones para filtrar.
        this.tiendas = tiendas ? tiendas : []; // Una lista de tiendas para filtrar.
        this.marcas = marcas ? marcas : []; // Una lista de marcas para filtrar.
        this.categorias = categorias ? categorias : []; // Una lista de categorias para filtrar.
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

    // Un método para restablecer el estado de "usado" del filtrador.
    resetearUso() {
        this.usado = false;
    }
}
