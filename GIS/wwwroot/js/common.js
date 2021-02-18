//common function

function Zoom_change(map) {
    var mapzoom = map.getZoom();
    if (mapzoom <= 8) {
        for (var pp = 0; pp < zoomZone.length; pp++) {
            zoomZone[pp].setMap(null);
        }
    }
}
function createPolygon(_path) {
    var tmpLine = new google.maps.Polygon({
        strokeColor: '#666666',
        fillColor: '#FF8800',
        fillOpacity: 0.7,
        strokeOpacity: 1,
        strokeWeight: 1,
        clickable: !1,
        map: map,
        path: _path
    });
    return tmpLine;
}

function createLine(_path) {
    var tmpLine = new google.maps.Polyline({
        strokeColor: '#4679BD',
        strokeWeight: 1,
        clickable: !1,
        map: map,
        path: _path
    });
    return tmpLine;
}

function createPoint(pos) {
    var pt = new google.maps.Marker({
        position: pos,
       // icon: 'images/marker.png',
        map: map,
        clickable: !1
    });
    return pt;
}



function getLatLongArr(pgString) {
    var latLngArr = [];
    pgString = pgString.substring(pgString.lastIndexOf('(') + 1, pgString.indexOf(')'));
    var longLatArr = pgString.split(',');
    for (var i = 0; i < longLatArr.length; i++) {
        var LongLatsingle = longLatArr[i].split(' ');
        latLngArr.push(new google.maps.LatLng(parseFloat(LongLatsingle[1]), parseFloat(LongLatsingle[0])));
    }
    return latLngArr;
}

//Map option click events
$(document).on('click', '#mapsatellite', function (e) {

    map.setOptions({
        styles: [{
            'featureType': 'all',
            'elementType': 'labels',
            'stylers': [{
                'visibility': 'off'
            }]
        }]
    });


    map.setMapTypeId(google.maps.MapTypeId.SATELLITE);
});
$(document).on('click', '#maproad', function (e) {

    map.setOptions({
        styles: [{
            'featureType': 'all',
            'elementType': 'labels',
            'stylers': [{
                'visibility': 'on'
            }]
        }]
    });

    map.setMapTypeId(google.maps.MapTypeId.ROADMAP);
});
$(document).on('click', '#maphybrid', function (e) {

    map.setOptions({
        styles: [{
            'featureType': 'all',
            'elementType': 'labels',
            'stylers': [{
                'visibility': 'on'
            }]
        }]
    });
    map.setMapTypeId(google.maps.MapTypeId.TERRAIN);
});