var zoomZone = [];
var map;
var zoomZone = [];
var zoomvillage = [];
var ZoomPlot = [];
var ABADI_LAYER = [];
var BURJI_LAYER = [];
var CLEAN_LAYER = [];
var CLEANTEXT_LAYER = [];
var DIM_LAYER = [];
var ENCROACHMENT_LAYER = [];
var GOSHA_LAYER = [];
var GRID_LAYER = [];
var NALA_LAYER = [];
var TEXT_LAYER = [];
var TRIJUNCTION_LAYER = [];


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
                        html = html + '<a href="javascript:void(0);" id="V' + response[i].village[j].id + '" class="list-group-item list-group-item-action" onclick="showVillage(this.id, ' + response[i].id + ')"><i class="ri-eye-line"></i> ' + response[i].village[j].name + '</a>';
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
        mapTypeId: 'coordinate',
        //mapTypeId: google.maps.MapTypeId.ROADMAP,//SATELLITE
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
    //map.mapTypes.set(google.maps.MapTypeId.ROADMAP);
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


/*Zone Boundary Start*/
function showZone(maxima) {
    var newdis_id = maxima.replace('Z', '');
    HttpGet(`/GIS/GetZoneDetails?ZoneId=${parseInt(newdis_id)}`, 'json', function (response) {
        showDisBoundaries(response[0].polygon, response[0].xcoordinate, response[0].ycoordinate);
    });
}
function showDisBoundaries(ploygn, xaixis, yaixis) {

    for (var x = 0; x < zoomZone.length; x++) {
        zoomZone[x].setMap(null);
    }
    var sl = createPolygon(getLatLongArr(ploygn));
    sl.setOptions({ strokeWeight: 5, strokeColor: '#0242BD', fillOpacity: 0, clickable: !1 });
    zoomZone.push(sl);
    map.setZoom(10);
    map.panTo(new google.maps.LatLng(yaixis, xaixis));

}
/*Zone Boundary End*/

/*Village Boundary Start*/
function showVillage(maxima, zoneId) {
    var villageid = maxima.replace('V', '');
    HttpGet("/GIS/GetVillageDetails?VillageId=" + parseInt(villageid) + "&ZoneId=" + zoneId, 'json', function (response, villageid) {
        showDisBoundariesVillage(response[0].polygon, response[0].xcoordinate, response[0].ycoordinate, response[0].id);
    });
}
function showDisBoundariesVillage(ploygn, xaixis, yaixis, villageid) {

    for (var x = 0; x < zoomvillage.length; x++) {
        zoomvillage[x].setMap(null);
    }
    var sl = createPolygon(getLatLongArr(ploygn));
    sl.setOptions({ strokeWeight: 5, strokeColor: '#FF5733', fillOpacity: 0, clickable: !1 });
    zoomvillage.push(sl);
    map.setZoom(14);
    map.panTo(new google.maps.LatLng(yaixis, xaixis));
    showvillagelayers(villageid);
}
function showvillagelayers(villageid) {

    HttpGet(`/GIS/GetAbadiDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesAbadi(response);
    });

    HttpGet(`/GIS/GetBurjiDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesBurji(response);
    });
    HttpGet(`/GIS/GetCleanDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesClean(response);
    });

    HttpGet(`/GIS/GetCleantextDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesCleantext(response);
    });

    HttpGet(`/GIS/GetDimDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesDim(response);
    });

    HttpGet(`/GIS/GetEncroachmentDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesEncroachment(response);
    });

    HttpGet(`/GIS/GetGoshaDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesGosha(response);
    });

    HttpGet(`/GIS/GetGridDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesGrid(response);
    });
    HttpGet(`/GIS/GetNalaDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesNala(response);
    });
    HttpGet(`/GIS/GetTextDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesText(response);
    });
    HttpGet(`/GIS/GetTriJunctionDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesTriJunction(response);
    });
}

function showDisBoundariesAbadi(response) {
    var abadi = $.map(response, function (el) { return el; })
    for (i = 0; i < abadi.length; i++) {
        var ln = createLine(getLatLongArr(abadi[i].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#900C3F' });
        ABADI_LAYER.push(ln);
    }
}
function showDisBoundariesBurji(response) {
    var burji = $.map(response, function (el) { return el; })
    for (e = 0; e < burji.length; e++) {
        var ln = createLine(getLatLongArr(burji[e].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#83FF33' });
        BURJI_LAYER.push(ln);
    }
}
function showDisBoundariesClean(response) {
    var clean = $.map(response, function (el) { return el; })
    for (f = 0; f < clean.length; f++) {
        var ln = createLine(getLatLongArr(clean[f].polygon));
        ln.setOptions({ strokeWeight: 1, strokeColor: '#C6C03A' });
        CLEAN_LAYER.push(ln);
    }
}
function showDisBoundariesDim(response) {
    var dim = $.map(response, function (el) { return el; })
    for (h = 0; h < dim.length; h++) {
        var ln = createLine(getLatLongArr(dim[h].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#41AF78' });
        DIM_LAYER.push(ln);
    }
}
function showDisBoundariesEncroachment(response) {
    var encroachment = $.map(response, function (el) { return el; })
    for (j = 0; j < encroachment.length; j++) {
        var ln = createLine(getLatLongArr(encroachment[j].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#AB3975' });
        ENCROACHMENT_LAYER.push(ln);
    }
}
function showDisBoundariesGrid(response) {
    var grid = $.map(response, function (el) { return el; })
    for (j = 0; j < grid.length; j++) {
        var ln = createLine(getLatLongArr(grid[j].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#15EAFB' });
        GRID_LAYER.push(ln);
    }
}
function showDisBoundariesNala(response) {
    var nala = $.map(response, function (el) { return el; })
    for (j = 0; j < nala.length; j++) {
        var ln = createLine(getLatLongArr(nala[j].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#252A2A' });
        NALA_LAYER.push(ln);
    }
}
function showDisBoundariesCleantext(response) {
    var cleantext = $.map(response, function (el) { return el; })
    for (g = 0; g < cleantext.length; g++) {
        //var ln = createPoint(getLatLongArr(cleantext[g].polygon)[0]);
        //ln.setOptions({ strokeWeight: 3, strokeColor: '#AF4167 ' });
        var lp = new google.maps.LatLng(parseFloat(cleantext[g].ycoordinate), parseFloat(cleantext[g].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 20, hideZoom: 60, visible: true, map: map, cssName: 'nlLabelCleantext', position: lp, text: cleantext[g].label });
        CLEANTEXT_LAYER.push(_label);
    }
}
function showDisBoundariesGosha(response) {
    var gosha = $.map(response, function (el) { return el; })
    for (g = 0; g < gosha.length; g++) {
        var lp = new google.maps.LatLng(parseFloat(gosha[g].ycoordinate), parseFloat(gosha[g].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 18, hideZoom: 20, visible: true, map: map, cssName: 'nlLabelGosha', position: lp, text: gosha[g].label }); 9
        GOSHA_LAYER.push(_label);
    }
}
function showDisBoundariesText(response) {
    var textla = $.map(response, function (el) { return el; })
    for (g = 0; g < textla.length; g++) {
        var lp = new google.maps.LatLng(parseFloat(textla[g].ycoordinate), parseFloat(textla[g].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 18, visible: true, map: map, cssName: 'nlLabelKhasraText', position: lp, text: textla[g].label });
        TEXT_LAYER.push(_label);
    }
}
function showDisBoundariesTriJunction(response) {
    var trijunction = $.map(response, function (el) { return el; })
    for (j = 0; j < trijunction.length; j++) {
        var ln = createLine(getLatLongArr(trijunction[j].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#DC2A32 ' });
        TRIJUNCTION_LAYER.push(ln);
    }
}
/*Village Boundary End*/

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
