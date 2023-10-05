// Funciones para encontrar similitud entre palabras
function calcularDistanciaLevenshtein(str1, str2) {
    const len1 = str1.length;
    const len2 = str2.length;

    // Crear matriz para la distancia de Levenshtein
    const dp = Array.from({ length: len1 + 1 }, () => Array(len2 + 1).fill(0));

    for (let i = 0; i <= len1; i++) {
        for (let j = 0; j <= len2; j++) {
            if (i === 0) {
                dp[i][j] = j; // Inicialización para la primera fila
            } else if (j === 0) {
                dp[i][j] = i; // Inicialización para la primera columna
            } else {
                // Calcular costo de operaciones (inserción, eliminación o sustitución)
                const costo = str1[i - 1] === str2[j - 1] ? 0 : 1;
                dp[i][j] = Math.min(
                    dp[i - 1][j] + 1, // Eliminación
                    dp[i][j - 1] + 1, // Inserción
                    dp[i - 1][j - 1] + costo // Sustitución
                );
            }
        }
    }

    return dp[len1][len2]; // Distancia de Levenshtein final
}
function construirMapaDistanciaLevenshtein(listaPalabras) {
    const mapaDistancia = new Map();

    // Mapear cada palabra a su forma en minúsculas para búsqueda eficiente
    for (const palabra of listaPalabras) {
        mapaDistancia.set(palabra.toLowerCase(), palabra);
    }

    return mapaDistancia;
}

// Función para encontrar el porcentaje de coincidencia
function encontrarPorcentajeCoincidencia(palabraDada, palabraOriginal) {
    palabraDada = palabraDada.toLowerCase();
    palabraOriginal = palabraOriginal.toLowerCase();

    // Calcular la distancia de Levenshtein entre las palabras
    const distancia = calcularDistanciaLevenshtein(palabraDada, palabraOriginal);
    const longitudMayor = Math.max(palabraDada.length, palabraOriginal.length);

    // Calcular el porcentaje de coincidencia
    const porcentajeCoincidencia = ((longitudMayor - distancia) / longitudMayor) * 100;

    return porcentajeCoincidencia.toFixed(2); // Redondear el porcentaje a 2 decimales
}

// Función para encontrar la palabra más similar
function encontrarPalabraMasSimilar(palabraDada, listaPalabras) {
    palabraDada = palabraDada.toLowerCase();

    // Construir un mapa de palabras en minúsculas a sus formas originales para una búsqueda rápida
    const mapaDistancia = construirMapaDistanciaLevenshtein(listaPalabras);

    // Comprobar coincidencias exactas
    if (mapaDistancia.has(palabraDada)) {
        const palabraExacta = mapaDistancia.get(palabraDada);
        const porcentajeExacto = 100.0;
        return { palabra: palabraExacta, porcentaje: porcentajeExacto };
    }

    let palabraMasSimilar = '';
    let porcentajeMasSimilar = 0;

    // Calcular el porcentaje de coincidencia para candidatos potenciales y mantener el más alto
    for (const palabra of listaPalabras) {
        const porcentaje = encontrarPorcentajeCoincidencia(palabraDada, palabra.toLowerCase());

        if (porcentaje > porcentajeMasSimilar) {
            palabraMasSimilar = palabra;
            porcentajeMasSimilar = porcentaje;
        }
    }

    return { palabra: palabraMasSimilar, porcentaje: parseFloat(porcentajeMasSimilar) };
}


// Clase Mapa para agregar una nueva tienda
class MapaTienda {
    // Coordenadas de San José, Costa Rica
    latitudInicial = 9.9281;
    longitudInicial = -84.0907;
    // Constructor
    constructor(latitud = this.latitudInicial, longitud = this.longitudInicial) {
        // Crea mapa
        this.map = L.map('map').setView([latitud, longitud], 8);

        // Agrega copyright
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(this.map);

        // Inicializa marcador
        this.marcador = null;

        // Asocia elementos HTML
        this.cajaProvincia = document.getElementById("Provincia");
        this.cajaCanton = document.getElementById("Canton");
        this.cajaLatitud = document.getElementById("CajaTextoLatitud");
        this.cajaLongitud = document.getElementById("CajaTextoLongitud");
        this.botonBusqueda = document.getElementById("BotonBusqueda");

        // Llamada inicial al manejo de clic en el mapa
        this.manejoClickMapa({ latlng: L.latLng(latitud, longitud) });

        // Especifica acciones de click
        this.map.on('click', this.manejoClickMapa.bind(this));
        this.botonBusqueda.addEventListener('click', this.manejoClickBusqueda.bind(this));
    }

    // Manejo de click en mapa
    async manejoClickMapa(event) {
        try {
            var clickedLatLng = event.latlng;

            // Coloca valores en elementos HTML
            this.cajaLatitud.value = clickedLatLng.lat;
            this.cajaLongitud.value = clickedLatLng.lng;

            // Revisa si hay marcador en el mapa
            if (this.marcador) {
                this.map.removeLayer(this.marcador);
            }

            // Crea un nuevo marcador
            this.marcador = L.marker(clickedLatLng).addTo(this.map);

            // Obtiene codificación inversa de argcis
            const respuesta = await fetch('https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode?f=pjson&featureTypes=&location='
                + clickedLatLng.lng + ',' + clickedLatLng.lat);
            const datos = await respuesta.json();

            // Encontrar provincia
            const provincia = encontrarPalabraMasSimilar(datos.address.Region, obtenerProvincias());
            if (provincia.porcentaje > 50) {
                this.cajaProvincia.value = provincia.palabra;

                const listaCantones = await obtenerCantones();

                // Encontrar canton
                const canton = encontrarPalabraMasSimilar(datos.address.Subregion, listaCantones);
                if (canton.porcentaje > 50) {
                    this.cajaCanton.value = canton.palabra;
                }
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }

    // Manejo de buscar en cajas de texto
    async manejoClickBusqueda() {
        try {
            // Obtiene valores de HTML que el usuario escribió
            const provincia = this.cajaProvincia.value;
            const canton = this.cajaCanton.value;

            // Obtiene codificación de argcis
            const respuesta = await fetch('https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates?singleLine='
                + canton + ',' + provincia + '&f=pjson');
            const datos = await respuesta.json();

            // Obtener longitud y latitud
            var longitud = datos.candidates[0].location.x;
            var latitud = datos.candidates[0].location.y;

            // Crea una coordenada
            var coordinates = L.latLng(latitud, longitud);

            // Revisa si el marcador existe
            if (this.marcador) {
                this.map.removeLayer(this.marcador);
            }

            // Actualiza el marcador
            this.marcador = L.marker(coordinates).addTo(this.map);

            // Coloca nueva vista (mueve el mapa)
            this.map.setView([latitud, longitud], 8);

            // Colocar valores en elementos HTML
            this.cajaLatitud.value = latitud;
            this.cajaLongitud.value = longitud;

            this.botonActualizar.click();
        } catch (error) {
            console.error('Error:', error);
        }
    }
}

// Función para inicializar el mapa al cargar el documento
document.addEventListener("DOMContentLoaded", function () {
    // Crear una instancia de la clase Mapa
    const mapa = new MapaTienda();
});
