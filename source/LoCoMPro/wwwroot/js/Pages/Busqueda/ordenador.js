// Ordenador
class OrdenadorDeBusqueda {
    constructor(ordenado = null, sentido = null) {
        // Un booleano que indica si el ordenador se ha utilizado.
        this.usado = false;
        // La propiedad por la que se va a ordenar.
        this.ordenado = ordenado;
        // El sentido del ordenamiento (ascendente o descendente por defecto).
        this.sentido = sentido ? sentido : 'asc';
    }

    // Métodos para establecer la propiedad por la que se ordenará.
    setPropiedadOrdenada(ordenado) {
        this.ordenado = ordenado;
    }

    // Método para establecer el sentido del ordenamiento.
    setSentidoOrdenado(sentido) {
        this.sentido = sentido;
    }

    // Método para cambiar el sentido del ordenamiento entre ascendente y descendente.
    cambiarSentido() {
        this.sentido = (this.sentido == 'asc') ? 'desc' : 'asc';
    }

    // Método principal para ordenar una entrada de búsqueda.
    ordenar(entradaIQ) {
        this.usado = true;
        let resultadosIQ = entradaIQ;

        // Si se especifica la propiedad de ordenamiento y el sentido, se realiza el ordenamiento.
        if (this.ordenado && this.sentido) {
            resultadosIQ = this.ordenarSegunSentido(resultadosIQ);
        }
        return resultadosIQ;
    }

    // Método para ordenar según el sentido especificado (ascendente o descendente).
    ordenarSegunSentido(resultadosIQ) {
        if (this.sentido === 'asc') {
            resultadosIQ = this.ordenarAscendente(resultadosIQ);
        } else {
            resultadosIQ = this.ordenarDescendente(resultadosIQ);
        }
        return resultadosIQ;
    }

    // Métodos de ordenamiento para propiedades específicas.
    ordenarAscendente(resultadosIQ) {
        this.esconderFlechas();
        switch (this.ordenado) {
            case 'nombre':
                resultadosIQ = resultadosIQ.sort((a, b) => a.nombre.localeCompare(b.nombre));
                document.getElementById("DescendenteNombre").hidden = false;
                break;
            case 'categoria':
                resultadosIQ = resultadosIQ.sort((a, b) => a.categoria.localeCompare(b.categoria));
                document.getElementById("DescendenteCategoria").hidden = false;
                break;
            case 'marca':
                resultadosIQ = resultadosIQ.sort((a, b) => a.marca.localeCompare(b.marca));
                document.getElementById("DescendenteMarca").hidden = false;
                break;
            case 'precio':
                resultadosIQ = resultadosIQ.sort((a, b) => a.precio - b.precio);
                document.getElementById("DescendentePrecio").hidden = false;
                break;
            case 'unidad':
                resultadosIQ = resultadosIQ.sort((a, b) => a.unidad.localeCompare(b.unidad));
                document.getElementById("DescendenteUnidad").hidden = false;
                break;
            case 'fecha':
                resultadosIQ = resultadosIQ.sort((a, b) => new Date(a.fecha) - new Date(b.fecha));
                document.getElementById("DescendenteFecha").hidden = false;
                break;
            case 'tienda':
                resultadosIQ = resultadosIQ.sort((a, b) => a.tienda.localeCompare(b.tienda));
                document.getElementById("DescendenteTienda").hidden = false;
                break;
            case 'provincia':
                resultadosIQ = resultadosIQ.sort((a, b) => a.provincia.localeCompare(b.provincia));
                document.getElementById("DescendenteProvincia").hidden = false;
                break;
            case 'canton':
                resultadosIQ = resultadosIQ.sort((a, b) => a.canton.localeCompare(b.canton));
                document.getElementById("DescendenteCanton").hidden = false;
                break;
            default:
                break;
        }
        return resultadosIQ;
    }

    // Método de ordenamiento descendente para propiedades específicas.
    ordenarDescendente(resultadosIQ) {
        this.esconderFlechas();
        switch (this.ordenado) {
            case 'nombre':
                resultadosIQ = resultadosIQ.sort((a, b) => b.nombre.localeCompare(a.nombre));
                document.getElementById("AscendenteNombre").hidden = false;
                break;
            case 'categoria':
                resultadosIQ = resultadosIQ.sort((a, b) => b.categoria.localeCompare(a.categoria));
                document.getElementById("AscendenteCategoria").hidden = false;
                break;
            case 'marca':
                resultadosIQ = resultadosIQ.sort((a, b) => b.marca.localeCompare(a.marca));
                document.getElementById("AscendenteMarca").hidden = false;
                break;
            case 'precio':
                resultadosIQ = resultadosIQ.sort((a, b) => b.precio - a.precio);
                document.getElementById("AscendentePrecio").hidden = false;
                break;
            case 'unidad':
                resultadosIQ = resultadosIQ.sort((a, b) => b.unidad.localeCompare(a.unidad));
                document.getElementById("AscendenteUnidad").hidden = false;
                break;
            case 'fecha':
                resultadosIQ = resultadosIQ.sort((a, b) => new Date(b.fecha) - new Date(a.fecha));
                document.getElementById("AscendenteFecha").hidden = false;
                break;
            case 'tienda':
                resultadosIQ = resultadosIQ.sort((a, b) => b.tienda.localeCompare(a.tienda));
                document.getElementById("AscendenteTienda").hidden = false;
                break;
            case 'provincia':
                resultadosIQ = resultadosIQ.sort((a, b) => b.provincia.localeCompare(a.provincia));
                document.getElementById("AscendenteProvincia").hidden = false;
                break;
            case 'canton':
                resultadosIQ = resultadosIQ.sort((a, b) => b.canton.localeCompare(a.canton));
                document.getElementById("AscendenteCanton").hidden = false;
                break;
            default:
                break;
        }
        return resultadosIQ;
    }

    // Método para esconder todas las flechas
    esconderFlechas() {
        document.getElementById("DescendenteNombre").hidden = true;
        document.getElementById("AscendenteNombre").hidden = true;
        document.getElementById("DescendenteCategoria").hidden = true;
        document.getElementById("AscendenteCategoria").hidden = true;
        document.getElementById("DescendenteMarca").hidden = true;
        document.getElementById("AscendenteMarca").hidden = true;
        document.getElementById("DescendentePrecio").hidden = true;
        document.getElementById("AscendentePrecio").hidden = true;
        document.getElementById("DescendenteUnidad").hidden = true;
        document.getElementById("AscendenteUnidad").hidden = true;
        document.getElementById("DescendenteFecha").hidden = true;
        document.getElementById("AscendenteFecha").hidden = true;
        document.getElementById("DescendenteTienda").hidden = true;
        document.getElementById("AscendenteTienda").hidden = true;
        document.getElementById("DescendenteProvincia").hidden = true;
        document.getElementById("AscendenteProvincia").hidden = true;
        document.getElementById("DescendenteCanton").hidden = true;
        document.getElementById("AscendenteCanton").hidden = true;
    }

    // Un método para restablecer el estado de "usado" del ordenador.
    resetearUso() {
        this.usado = false;
        this.esconderFlechas();
    }
}
