// v1 - Regular JS
//(function () {
//    window.blazingTrails = {
//        map: {
//            initialize: function (routeMapComponent, hostElement) {
//                hostElement.map = L.map(hostElement).setView([51.700, -0.10], 3);

//                L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
//                    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>',
//                    maxZoom: 18,
//                    opacity: .75
//                }).addTo(hostElement.map);

//                hostElement.markers = [];
//                hostElement.lines = [];
//                hostElement.map.on('click', function (e) {
//                    var marker = L.marker(e.latlng);
//                    marker.addTo(hostElement.map);
//                    hostElement.markers.push(marker);
//                    var line = L.polyline(hostElement.markers.map(m => m.getLatLng()), { color: 'var(--brand)' }).addTo(hostElement.map);
//                    hostElement.lines.push(line);
//                    routeMapComponent.invokeMethodAsync('MarkerAdded', e.latlng.lat, e.latlng.lng);
//                });
//            },
//            deleteLastMarker: function (routeMapComponent, hostElement) {
//                if (hostElement.markers.length > 0) {
//                    var lastMarker = hostElement.markers[hostElement.markers.length - 1];
//                    var lastLine = hostElement.lines[hostElement.lines.length - 1];

//                    hostElement.map.removeLayer(lastMarker);
//                    lastLine.remove(hostElement.map);

//                    routeMapComponent.invokeMethodAsync('MarkerRemoved', lastMarker.getLatLng().lat, lastMarker.getLatLng().lng);

//                    hostElement.markers.pop();
//                    hostElement.lines.pop();
//                }
//            }
//        },
//    }
//})();

// v2 - JS Modules
export function initialize(routeMapComponent, hostElement, editingEnabled, existingWaypoints) {
    hostElement.map = L.map(hostElement).setView([51.700, -0.10], 3);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
        maxZoom: 18,
        opacity: .75
    }).addTo(hostElement.map);

    hostElement.markers = [];
    hostElement.lines = [];

    if (existingWaypoints && existingWaypoints.length > 0) {
        existingWaypoints.forEach(cord => {
            let marker = L.marker(cord);
            marker.addTo(hostElement.map);
            hostElement.markers.push(marker);
            let line = L.polyline(hostElement.markers.map(m => m.getLatLng()), { color: 'var(--brand)' }).addTo(hostElement.map);
            hostElement.lines.push(line);
        });
    }

    if (hostElement.markers.length > 0) {
        var markersGroup = new L.featureGroup(hostElement.markers);
        hostElement.map.fitBounds(markersGroup.getBounds().pad(1));
    }

    if (editingEnabled) {
        hostElement.map.on('click', function (e) {
            let marker = L.marker(e.latlng);
            marker.addTo(hostElement.map);
            hostElement.markers.push(marker);
            let line = L.polyline(hostElement.markers.map(m => m.getLatLng()), { color: 'var(--brand)' }).addTo(hostElement.map);
            hostElement.lines.push(line);
            routeMapComponent.invokeMethodAsync('WaypointAdded', e.latlng.lat, e.latlng.lng);
        });
    }
}

export function deleteLastWaypoint(routeMapComponent, hostElement) {
    if (hostElement.markers.length > 0) {
        var lastMarker = hostElement.markers[hostElement.markers.length - 1];
        var lastLine = hostElement.lines[hostElement.lines.length - 1];

        hostElement.map.removeLayer(lastMarker);
        lastLine.remove(hostElement.map);

        routeMapComponent.invokeMethodAsync('WaypointDeleted', lastMarker.getLatLng().lat, lastMarker.getLatLng().lng);

        hostElement.markers.pop();
        hostElement.lines.pop();
    }
}