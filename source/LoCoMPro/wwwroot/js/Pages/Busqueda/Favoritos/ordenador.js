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
            case 'cantidad':
                resultadosIQ = resultadosIQ.sort((a, b) => a.cantidadEncontrada - b.cantidadEncontrada);
                document.getElementById("DescendenteCantidad").hidden = false;
                break;
            case 'precio':
                resultadosIQ = resultadosIQ.sort((a, b) => a.precioTotal - b.precioTotal);
                document.getElementById("DescendentePrecio").hidden = false;
                break;
            case 'distancia':
                resultadosIQ = resultadosIQ.sort((a, b) => a.distanciaTotal - b.distanciaTotal);
                document.getElementById("DescendenteDistancia").hidden = false;
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
            case 'cantidad':
                resultadosIQ = resultadosIQ.sort((a, b) => b.cantidadEncontrada - a.cantidadEncontrada);
                document.getElementById("AscendenteCantidad").hidden = false;
                break;
            case 'precio':
                resultadosIQ = resultadosIQ.sort((a, b) => b.precioTotal - a.precioTotal);
                document.getElementById("AscendentePrecio").hidden = false;
                break;
            case 'distancia':
                resultadosIQ = resultadosIQ.sort((a, b) => b.distanciaTotal - a.distanciaTotal);
                document.getElementById("AscendenteDistancia").hidden = false;
                break;
            default:
                break;
        }
        return resultadosIQ;
    }

    // Método para esconder todas las flechas
    esconderFlechas() {
        document.getElementById("AscendenteCantidad").hidden = true;
        document.getElementById("DescendenteCantidad").hidden = true;
        document.getElementById("AscendentePrecio").hidden = true;
        document.getElementById("DescendentePrecio").hidden = true;
        document.getElementById("AscendenteDistancia").hidden = true;
        document.getElementById("DescendenteDistancia").hidden = true;
    }

    // Un método para restablecer el estado de "usado" del ordenador.
    resetearUso() {
        this.usado = false;
        this.esconderFlechas();
    }
}
