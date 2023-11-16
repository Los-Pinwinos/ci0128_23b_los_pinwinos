// Función para calcular distancia de Levenshtein
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

// Función para construir un mapa de distancia Levenshtein
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

// Clase Mapa
class Mapa {
    // Coordenadas de Costa Rica
    latitudInicial = 9.9281;
    longitudInicial = -84.0907;

    constructor(latitud = this.latitudInicial, longitud = this.longitudInicial) {
        // Crear un mapa Leaflet
        this.map = L.map('map').setView([latitud, longitud], 8);

        // Agregar capa de mosaico de OpenStreetMap
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        }).addTo(this.map);

        // Inicializar marcador
        this.marcador = null;

        // Obtener elementos HTML
        this.cajaProvincia = document.getElementById("CajaDeSeleccionProvincia");
        this.cajaCanton = document.getElementById("CajaDeSeleccionCanton");
        this.cajaLatitud = document.getElementById("CajaTextoLatitud");
        this.cajaLongitud = document.getElementById("CajaTextoLongitud");
        this.botonBusqueda = document.getElementById("BotonBusqueda");

        // Provincias, cantones disponibles
        this.provinciasDisponibles = obtenerProvinciasDisponibles();
        this.cantonesDisponibles = [];

        // Llamada inicial al manejo de click en el mapa
        this.manejoClickMapa({ latlng: L.latLng(latitud, longitud) });

        // Escuchar eventos de clic en el mapa y en el botón de búsqueda
        this.map.on('click', this.manejoClickMapa.bind(this));
        this.botonBusqueda.addEventListener('click', this.manejoClickBusqueda.bind(this));
    }

    // Manejo de clic en el mapa
    async manejoClickMapa(event) {
        var clickedLatLng = event.latlng;

        // Colocar valores en elementos HTML
        this.cajaLatitud.value = clickedLatLng.lat;
        this.cajaLongitud.value = clickedLatLng.lng;

        // Verificar si hay un marcador en el mapa y eliminarlo
        if (this.marcador) {
            this.map.removeLayer(this.marcador);
        }

        // Crear un nuevo marcador en la ubicación clickeada
        this.marcador = L.marker(clickedLatLng).addTo(this.map);

        try {
            // Obtener información de ubicación inversa a través de la API de ArcGIS
            const response = await fetch('https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode?f=pjson&featureTypes=&location=' + clickedLatLng.lng + ',' + clickedLatLng.lat);
            if (!response.ok) {
                throw new Error(`Error HTTP! Estado: ${response.status}`);
            }
            const data = await response.json();

            // Encontrar la provincia más similar a la región proporcionada por ArcGIS
            const provincia = encontrarPalabraMasSimilar(data.address.Region, this.provinciasDisponibles);

            // Actualizar el valor en la caja de provincia si hay una coincidencia suficientemente alta
            if (provincia.porcentaje > 50 && this.cajaProvincia.value != provincia.palabra) {
                this.cajaProvincia.value = provincia.palabra;
                // Esperar a que se actualicen los cantones
                await actualizarCantones();
            }

            // Encontrar el cantón más similar a la subregión proporcionada por ArcGIS
            const canton = encontrarPalabraMasSimilar(data.address.Subregion, obtenerCantonesDisponibles());

            // Actualizar el valor en la caja de cantón si hay una coincidencia suficientemente alta
            if (canton.porcentaje > 50 && this.cajaCanton.value != canton.palabra) {
                this.cajaCanton.value = canton.palabra;
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }

    // Manejo de búsqueda en cajas de texto
    manejoClickBusqueda() {
        // Obtener valores de las cajas de provincia y cantón
        const provincia = this.cajaProvincia.value;
        const canton = this.cajaCanton.value;

        // Obtener la codificación de dirección a través de la API de ArcGIS
        fetch('https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates?singleLine=' + canton + ',' + provincia + '&f=pjson')
            .then(response => response.json())
            .then(data => {
                // Obtener longitud y latitud de la primera candidata
                var longitud = data.candidates[0].location.x;
                var latitud = data.candidates[0].location.y;

                // Crear una coordenada
                var coordinates = L.latLng(latitud, longitud);

                // Verificar si existe un marcador en el mapa y eliminarlo
                if (this.marcador) {
                    this.map.removeLayer(this.marcador);
                }

                // Actualizar el marcador en la nueva ubicación
                this.marcador = L.marker(coordinates).addTo(this.map);

                // Establecer la vista del mapa en la nueva ubicación
                this.map.setView([latitud, longitud], 8);

                // Colocar los nuevos valores en las cajas de latitud y longitud
                this.cajaLatitud.value = latitud;
                this.cajaLongitud.value = longitud;
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }
}

// Función para inicializar el mapa al cargar el documento
document.addEventListener("DOMContentLoaded", function () {
    var mapa;
    // Verficar si se puede localizar al usuario
    if (longitud == 0 && latitud == 0)
        // Crear una instancia de la clase Mapa
        mapa = new Mapa();
    else
        mapa = new Mapa(latitud, longitud);
});