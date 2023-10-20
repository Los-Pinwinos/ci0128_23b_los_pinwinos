// Ordenador
class OrdenadorDeBusqueda {
    constructor(ordenado = null, sentido = null) {
        this.usado = false; // Un booleano que indica si el ordenador se ha utilizado.
        this.ordenado = ordenado; // La propiedad por la que se va a ordenar.
        this.sentido = sentido ? sentido : 'asc'; // El sentido del ordenamiento (ascendente o descendente por defecto).
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
        switch (this.ordenado) {
            case 'precio':
                resultadosIQ = resultadosIQ.sort((a, b) => a.precio - b.precio);
                break;
            case 'provincia':
                resultadosIQ = resultadosIQ.sort((a, b) => a.provincia.localeCompare(b.provincia));
                break;
            case 'canton':
                resultadosIQ = resultadosIQ.sort((a, b) => a.canton.localeCompare(b.canton));
                break;
            default:
                break;
        }
        return resultadosIQ;
    }

    // Método de ordenamiento descendente para propiedades específicas.
    ordenarDescendente(resultadosIQ) {
        switch (this.ordenado) {
            case 'precio':
                resultadosIQ = resultadosIQ.sort((a, b) => b.precio - a.precio);
                break;
            case 'provincia':
                resultadosIQ = resultadosIQ.sort((a, b) => b.provincia.localeCompare(a.provincia));
                break;
            case 'canton':
                resultadosIQ = resultadosIQ.sort((a, b) => b.canton.localeCompare(a.canton));
                break;
            default:
                break;
        }
        return resultadosIQ;
    }

    // Un método para restablecer el estado de "usado" del ordenador.
    resetearUso() {
        this.usado = false;
    }
}
