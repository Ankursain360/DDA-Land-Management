var zoomZone = [];
var map;
var zoomZone = [];
var zoomvillage = [];
var ZoomPlot = [];
$(document).ready(function () {
    console.log('f9');
    HttpGet(`/GIS/GetZoneList`, 'json', function (response) {
        var html = '';
        for (var i = 0; i < response.length; i++) {
            var check = 0;
            html = html + '<div class="accordion-item"><h5 class="accordion-header" id="heading' + response[i].id + '"><button id="Z' + response[i].id + '" class="accordion-button collapsed" type="button" onclick="showZone(this.id)" data-bs-toggle="collapse" data-bs-target="#collapse' + response[i].id + '" aria-expanded="true" aria-controls="collapse' + response[i].id + '"><i class="ri-arrow-right-s-fill"></i>' + response[i].name + '</button></h5><div id="collapse' + response[i].id + '" class="accordion-collapse collapse" aria-labelledby="heading' + response[i].id + '" data-bs-parent="#accordionData"><div class="accordion-body"><div class="list-group">';
            if (response[i].village.length > 0) {
                for (var j = 0; j < response[i].village.length; j++) {
                    if (response[i].village[j].isActive == 1) {
                        html = html + '<a href="#" id="V' + response[i].village[j].id + '" class="list-group-item list-group-item-action"><i class="ri-eye-line"></i> ' + response[i].village[j].name + '</a>';
                        check++;
                    }
                }
            }
            if (check == 0) {
                html = html + '<a href="#" class="list-group-item list-group-item-action"><i class="ri-eye-line"></i> Village Not Found</a>';
            }
            html = html + '</div></div></div></div>';
            // zoomZone.push(response);
        }
        $("#accordionData").html('');
        $("#accordionData").html(html);
    });
    //initialize();
});

function CoordMapType() { }

CoordMapType.prototype.tileSize = new google.maps.Size(256, 256);
CoordMapType.prototype.maxZoom = 25;

CoordMapType.prototype.getTile = function (coord, zoom, ownerDocument) {
    var div = ownerDocument.createElement('div');
    // div.innerHTML = coord;
    div.style.width = this.tileSize.width + 'px';
    div.style.height = this.tileSize.height + 'px';
    //div.style.fontSize = '10';
    //div.style.borderStyle = 'solid';
    //div.style.borderWidth = '1px';
    //div.style.borderColor = '#AAAAAA';
    div.style.backgroundColor = '#E5E3DF';
    return div;
};

CoordMapType.prototype.name = 'Tile #s';
CoordMapType.prototype.alt = 'Tile Coordinate Map Type';
var coordinateMapType = new CoordMapType();


google.maps.event.addDomListener(window, 'load', initialize);
function initialize() {
    var mapOptions = {
        zoom: 10,
        disableDefaultUI: true,
        //  mapTypeId: 'coordinate',
        mapTypeId: google.maps.MapTypeId.ROADMAP,//SATELLITE
        scaleControl: false,
        disableDefaultUI: true,
        zoomControl: false,
        rotateControl: true,
        zoomControlOptions: {
            position: google.maps.ControlPosition.LEFT_CENTER
        },

        styles: [{
            'featureType': 'all',
            'elementType': 'labels',
            'stylers': [{
                'visibility': 'on'
            }]

        }],

        fullscreenControl: false,
        center: new google.maps.LatLng(28.66667, 77.21667),
        //scaleControlOptions: { position: google.maps.ControlPosition.RIGHT_CENTER }

    };
    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
    map.mapTypes.set('coordinate', coordinateMapType);
    map.mapTypes.set(google.maps.MapTypeId.ROADMAP);
    //Added by Sachin 08/09/2017
    map.addListener('zoom_changed', function () {
        Zoom_change(map);
    });
    //Loads custom zoom controls
    var zoomDiv = document.createElement('div');
    var renderZoomControls = new ZoomControl(zoomDiv, this.map);
    zoomDiv.index = 1;
    this.map.controls[google.maps.ControlPosition.LEFT_CENTER].push(zoomDiv);
    this.map.controls[google.maps.ControlPosition.TOP_RIGHT].push(FullScreenControl(map, 'Full Screen', 'Exit Full Screen'));
    //End of Code 
    // showLegend();
    //$('.statsDIv .close').click(function () { $('.statsDIv').hide(); });
    //GetCurrentDetails("State", "4");
    //  showLegend();
}
function showZone(maxima) {
    var newdis_id = maxima.replace('Z', '');
    HttpGet(`/GIS/GetZoneDetails?ZoneId=${parseInt(newdis_id)}`, 'json', function (response) {
        showDisBoundaries(response.Polygon, response.xCoordinate, response.yCoordinate);
    });
}
function showDisBoundaries(ploygn, xaixis, yaixis) {

    for (var x = 0; x < zoomZone.length; x++) {
        zoomZone[x].setMap(null);
    }
    var sl = createPolygon(getLatLongArr(ploygn));
    sl.setOptions({ strokeWeight: 5, strokeColor: '#0242BD', fillOpacity: 0, clickable: !1 });
    zoomZone.push(sl);
    map.setZoom(12);
    map.panTo(new google.maps.LatLng(yaixis, xaixis));

}


//common function

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














$('#TransparencyRange, #MapOption').on('click', function () {
    if ($(this).hasClass('show')) {
        $('.actionBtn').removeClass('show');
    } else {
        $('.actionBtn').removeClass('show');
        $(this).toggleClass('show');
    }

});

$('#LeftNavTab a').on('click', function (e) {
    e.preventDefault()
    $(this).tab('show')
});

$('.MenuToggle').on('click', function (e) {
    $('#leftSection').toggleClass('goleft');
});
