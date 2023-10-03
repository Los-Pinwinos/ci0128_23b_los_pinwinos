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
        this.cajaDistrito = document.getElementById("Distrito");
        this.cajaLatitud = document.getElementById("CajaTextoLatitud");
        this.cajaLongitud = document.getElementById("CajaTextoLongitud");
        this.botonBusqueda = document.getElementById("BotonBusqueda");
        this.botonSiguiente = document.getElementById("Siguiente");
        this.botonSiguienteEvento = document.getElementById("SigEvento");

        // Llamada inicial al manejo de clic en el mapa
        this.manejoClickMapa({ latlng: L.latLng(latitud, longitud) });

        // Especifica acciones de click
        this.map.on('click', this.manejoClickMapa.bind(this));
        this.botonBusqueda.addEventListener('click', this.manejoClickBusqueda.bind(this));
        this.botonSiguiente.addEventListener('click', this.manejoClickBusquedaDistrito.bind(this));
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
            if (provincia.porcentaje > 50) {
                this.cajaProvincia.value = provincia.palabra;

                const listaCantones = await obtenerCantones();

                // Encontrar canton
                if (canton.porcentaje > 50) {
                    this.cajaCanton.value = canton.palabra;

                    const listaDistritos = await obtenerDistritos();

                    // Encontrar distrito
                    if (distrito.porcentaje > 50) {
                        this.cajaDistrito.value = distrito.palabra;
                    }
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

            // Coloca valores en elementos HTML
            this.cajaLatitud.value = latitud;
            this.cajaLongitud.value = longitud;

            const respuestaDistrito = await fetch('https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode?f=pjson&featureTypes=&location='
                + longitud + ',' + latitud);
            const datosDistrito = await respuestaDistrito.json();


            // Colocar valores en elementos HTML
            const listaDistritos = await obtenerDistritos();
            if (distrito.porcentaje > 50) {
                this.cajaDistrito.value = distrito.palabra;
            }
        } catch (error) {
            console.error('Error:', error);
        }
    }

    // Manejo de buscar en cajas de texto
    async manejoClickBusquedaDistrito() {
        try {
            // Obtener valores de HTML
            const provincia = this.cajaProvincia.value;
            const canton = this.cajaCanton.value;

            // Obtener codificación de argcis
            const respuesta = await fetch('https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates?singleLine='
                + canton + ',' + provincia + '&f=pjson');
            const datos = await respuesta.json();

            // Obtener longitud y latitud
            var longitud = datos.candidates[0].location.x;
            var latitud = datos.candidates[0].location.y;

            // Colocar valores en elementos HTML
            this.cajaLatitud.value = latitud;
            this.cajaLongitud.value = longitud;

            const respuestaDistrito = await fetch('https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode?f=pjson&featureTypes=&location='
                + longitud + ',' + latitud);
            const datosDistrito = await respuestaDistrito.json();

            // Colocar valores en elementos HTML
            const listaDistritos = await obtenerDistritos();
            if (distrito.porcentaje > 50) {
                this.cajaDistrito.value = distrito.palabra;
            }
        } catch (error) {
            console.error('Error:', error);
        }
        this.botonSiguienteEvento.click();
    }
}

// Función para inicializar el mapa al cargar el documento
document.addEventListener("DOMContentLoaded", function () {
    // Crear una instancia de la clase Mapa
    const mapa = new MapaTienda();
});