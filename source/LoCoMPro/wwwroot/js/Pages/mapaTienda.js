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

        // Especifica acciones de click
        this.map.on('click', this.manejoClickMapa.bind(this));
        this.botonBusqueda.addEventListener('click', this.manejoClickBusqueda.bind(this));
    }

    // Manejo de click en mapa
    manejoClickMapa(event) {
        // Obtiene la latitud y longitud del lugar que el usuario marcó
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
        fetch('https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode?f=pjson&featureTypes=&location=' + clickedLatLng.lng + ',' + clickedLatLng.lat)
            .then(response => response.json())
            .then(data => {
                // Coloca valores de Provincia, Cantón y Distrito en elementos HTML
                this.cajaProvincia.value = data.address.Region;
                this.cajaCanton.value = data.address.Subregion;
                this.cajaDistrito.value = data.address.City;
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }

    // Manejo de buscar en cajas de texto
    manejoClickBusqueda() {
        // Obtiene valores de HTML que el usuario escribió
        const provincia = this.cajaProvincia.value;
        const canton = this.cajaCanton.value;

        // Obtiene codificación de argcis
        fetch('https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/findAddressCandidates?singleLine=' + canton + ',' + provincia + '&f=pjson')
            .then(response => response.json())
            .then(data => {
                // Obtiene la longitud y latitud
                var longitud = data.candidates[0].location.x;
                var latitud = data.candidates[0].location.y;

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

                // Obtiene codificación inversa de argcis
                fetch('https://geocode.arcgis.com/arcgis/rest/services/World/GeocodeServer/reverseGeocode?f=pjson&featureTypes=&location=' + longitud + ',' + latitud)
                    .then(response => response.json())
                    .then(data => {
                        // Coloca valor de Distrito en el HTML
                        this.cajaDistrito.value = data.address.City;
                    })
                    .catch(error => {
                        console.error('Error:', error);
                    });
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }
}

// Crear un mapa
var mapa = new MapaTienda();