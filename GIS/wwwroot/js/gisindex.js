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
var STATE_LAYER = [];
var ZERO_LAYER = [];
var NALI_LAYER = [];
var RAILWAYLINE_LAYER = [];
var FIELDBOUN_LAYER = [];
var KILLA_LAYER = [];
var CLOSE_LAYER = [];
var SAHEDA_LAYER = [];
var KACHAPAKALINE_LAYER = [];
var KHASRALINE_LAYER = [];
var KHASRABOUNDARY_LAYER = [];
var ROAD_LAYER = [];
var DASHED_LAYER = [];
var INNER_LAYER = [];
var VILLAGEBOUNDARY_LAYER = [];
var DIMENTIONTEXT_LAYER = [];
var CLOSETEXT_LAYER = [];
var VILLAGETEXT_LAYER = [];
var KHASRANO_LAYER = [];

$(document).ready(function () {
    console.log('f9');
    HttpGet(`/GIS/GetZoneList`, 'json', function (response) {
        var html = '';
        for (var i = 0; i < response.length; i++) {
            var check = 0;
            html = html + '<div class="accordion-item"><h5 class="accordion-header" id="heading' + response[i].id + '"><button id="Z' + response[i].id + '" class="accordion-button collapsed py-2 bg-primary text-white" type="button" onclick="showZone(this.id)" data-bs-toggle="collapse" data-bs-target="#collapse' + response[i].id + '" aria-expanded="true" aria-controls="collapse' + response[i].id + '"><i class="ri-arrow-right-s-fill text-white"></i>' + response[i].name + '</button></h5><div id="collapse' + response[i].id + '" class="accordion-collapse collapse" aria-labelledby="heading' + response[i].id + '" data-bs-parent="#accordionData"><div class="accordion-body"><div class="list-group">';
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
        //   mapTypeId: google.maps.MapTypeId.ROADMAP,//SATELLITE
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
        center: new google.maps.LatLng(28.6508954, 76.9201811),
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
    var rightdiv = document.getElementById('rightsidebuttons');
    rightdiv.appendChild(zoomDiv);
  // this.map.controls[google.maps.ControlPosition.BOTTOM_RIGHT].push(zoomDiv);
    //var fullscreen = new FullScreenControl(map, 'Full Screen', 'Exit Full Screen');
    //var rightActiondiv = document.getElementById('topRightActionButton');
    //rightActiondiv.appendChild(fullscreen);
    this.map.controls[google.maps.ControlPosition.TOP_RIGHT].push(FullScreenControl(map, 'Full Screen', 'Exit Full Screen'));
    //End of Code 
    // showLegend();
    //$('.statsDIv .close').click(function () { $('.statsDIv').hide(); });
    //GetCurrentDetails("State", "4");
    //  showLegend();
    getStateboundary();
}

function getStateboundary() {
    HttpGet(`/GIS/GetInitiallyStateDetails`, 'json', function (response) {
        setStateboundary(response);
    });
}

function setStateboundary(response) {
    //  if (response.status === true) {
    var arr = $.map(response, function (el) { return el; })

    for (i = 0; i < arr.length; i++) {
        var ln = createPolygon(getLatLongArr(arr[i].polygon));
        ln.setOptions({ visibleZoom: 7, fillColor: arr[i].colorcode, hideZoom: 9, visible: true, map: map, strokeWeight: 1, strokeColor: '#47C4C8', fillOpacity: 0.3, clickable: !1 });
        var lp = new google.maps.LatLng(parseFloat(arr[i].ycoordinate), parseFloat(arr[i].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 0, hideZoom: 18, visible: true, map: map, cssName: 'nlLabelState', position: lp, text: arr[i].label });

        STATE_LAYER.push(_label);
    }

    // }
}
/*Zone Boundary Start*/
function showZone(maxima) {
    var newdis_id = maxima.replace('Z', '');
    HttpGet(`/GIS/GetZoneDetails?ZoneId=${parseInt(newdis_id)}`, 'json', function (response) {
        showDisBoundaries(response[0].polygon, response[0].xcoordinate, response[0].ycoordinate);
    });
}
function showDisBoundaries(polygon, xaixis, yaixis) {

    for (var x = 0; x < zoomZone.length; x++) {
        zoomZone[x].setMap(null);
    }
    var sl = createPolygon(getLatLongArr(polygon));
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
    HttpGet(`/GIS/GetZeroDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesZero(response);
    });
    HttpGet(`/GIS/GetNaliDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesNali(response);
    });

    HttpGet(`/GIS/GetRailwayLineDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesRailwayLine(response);
    });

    HttpGet(`/GIS/GetFieldBounDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesFieldBoun(response);
    });
    HttpGet(`/GIS/GetKillaDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesKilla(response);
    });
    HttpGet(`/GIS/GetCloseDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesClose(response);
    });
    HttpGet(`/GIS/GetSahedaDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesSaheda(response);
    });
    HttpGet(`/GIS/GetKachaPakaLineDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesKachaPakaLine(response);
    });
    HttpGet(`/GIS/GetKhasraLineDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesKhasraLine(response);
    });

    HttpGet(`/GIS/GetKhasraBoundaryDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesKhasraBoundary(response);
    });

    HttpGet(`/GIS/GetRoadDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesRoad(response);
    });
    HttpGet(`/GIS/GetDashedDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesDashed(response);
    });
    HttpGet(`/GIS/GetInnerDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesInner(response);
    });
    HttpGet(`/GIS/GetVillageBoundaryDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesVillageBoundary(response);
    });
    HttpGet(`/GIS/GetDimTextDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesDimentionText(response);
    });
    HttpGet(`/GIS/GetCloseTextDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesCloseText(response);
    });
    HttpGet(`/GIS/GetVillageTextDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesVillageText(response);
    });
    HttpGet(`/GIS/GetKhasraNoDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        showDisBoundariesKhasraNo(response);
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
    for (k = 0; k < grid.length; k++) {
        var ln = createLine(getLatLongArr(grid[k].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#15EAFB' });
        GRID_LAYER.push(ln);
    }
}
function showDisBoundariesNala(response) {
    var nala = $.map(response, function (el) { return el; })
    for (m = 0; m < nala.length; m++) {
        var ln = createLine(getLatLongArr(nala[m].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#252A2A' });
        NALA_LAYER.push(ln);
    }
}
function showDisBoundariesTriJunction(response) {
    var trijunction = $.map(response, function (el) { return el; })
    for (n = 0; n < trijunction.length; n++) {
        var ln = createLine(getLatLongArr(trijunction[n].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#DC2A32 ' });
        TRIJUNCTION_LAYER.push(ln);
    }
}
function showDisBoundariesZero(response) {
    var zero = $.map(response, function (el) { return el; })
    for (o = 0; o < zero.length; o++) {
        var ln = createLine(getLatLongArr(zero[o].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#9FE2BF' });
        ZERO_LAYER.push(ln);
    }
}
function showDisBoundariesNali(response) {
    var nali = $.map(response, function (el) { return el; })
    for (p = 0; p < nali.length; p++) {
        var ln = createLine(getLatLongArr(nali[p].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#3C5D4C' });
        NALI_LAYER.push(ln);
    }
}
function showDisBoundariesRailwayLine(response) {
    var railwayline = $.map(response, function (el) { return el; })
    for (q = 0; q < railwayline.length; q++) {
        var ln = createLine(getLatLongArr(railwayline[q].polygon));
        ln.setOptions({ strokeWeight: 1, strokeColor: '#BA4A00' });
        RAILWAYLINE_LAYER.push(ln);
    }
}
function showDisBoundariesFieldBoun(response) {
    var fieldboun = $.map(response, function (el) { return el; })
    for (r = 0; r < fieldboun.length; r++) {
        var ln = createLine(getLatLongArr(fieldboun[r].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#E59866' });
        FIELDBOUN_LAYER.push(ln);
    }
}
function showDisBoundariesKilla(response) {
    var killa = $.map(response, function (el) { return el; })
    for (s = 0; s < killa.length; s++) {
        var ln = createLine(getLatLongArr(killa[s].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#D35400' });
        KILLA_LAYER.push(ln);
    }
}
function showDisBoundariesClose(response) {
    var close = $.map(response, function (el) { return el; })
    for (t = 0; t < close.length; t++) {
        var ln = createLine(getLatLongArr(close[t].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#A569BD' });
        CLOSE_LAYER.push(ln);
    }
}
function showDisBoundariesSaheda(response) {
    var saheda = $.map(response, function (el) { return el; })
    for (u = 0; u < saheda.length; u++) {
        var ln = createLine(getLatLongArr(saheda[u].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#85C1E9' });
        SAHEDA_LAYER.push(ln);
    }
}
function showDisBoundariesKachaPakaLine(response) {
    var kachapakaline = $.map(response, function (el) { return el; })
    for (v = 0; v < kachapakaline.length; v++) {
        var ln = createLine(getLatLongArr(kachapakaline[v].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#B9770E' });
        KACHAPAKALINE_LAYER.push(ln);
    }
}
function showDisBoundariesKhasraLine(response) {
    var khasraline = $.map(response, function (el) { return el; })
    for (w = 0; w < khasraline.length; w++) {
        var ln = createLine(getLatLongArr(khasraline[w].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#16A085' });
        KHASRALINE_LAYER.push(ln);
    }
}
function showDisBoundariesKhasraBoundary(response) {
    var khasraboundary = $.map(response, function (el) { return el; })
    for (x = 0; x < khasraboundary.length; x++) {
        var ln = createLine(getLatLongArr(khasraboundary[x].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#ABEBC6' });
        KHASRABOUNDARY_LAYER.push(ln);
    }
}
function showDisBoundariesRoad(response) {
    var road = $.map(response, function (el) { return el; })
    for (y = 0; y < road.length; y++) {
        var ln = createLine(getLatLongArr(road[y].polygon));
        ln.setOptions({ strokeWeight: 1, strokeColor: '#707B7C' });
        ROAD_LAYER.push(ln);
    }
}
function showDisBoundariesDashed(response) {
    var dashed = $.map(response, function (el) { return el; })
    for (z = 0; z < dashed.length; z++) {
        var ln = createLine(getLatLongArr(dashed[z].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#F7F9F9' });
        DASHED_LAYER.push(ln);
    }
}
function showDisBoundariesInner(response) {
    var inner = $.map(response, function (el) { return el; })
    for (ab = 0; ab < inner.length; ab++) {
        var ln = createLine(getLatLongArr(inner[ab].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#424949' });
        INNER_LAYER.push(ln);
    }
}
function showDisBoundariesVillageBoundary(response) {
    var villageboundary = $.map(response, function (el) { return el; })
    for (ac = 0; ac < villageboundary.length; ac++) {
        var ln = createLine(getLatLongArr(villageboundary[ac].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: '#7B7D7D' });
        VILLAGEBOUNDARY_LAYER.push(ln);
    }
}
function showDisBoundariesCleantext(response) {
    var cleantext = $.map(response, function (el) { return el; })
    for (ad = 0; ad < cleantext.length; ad++) {
        //var ln = createPoint(getLatLongArr(cleantext[g].polygon)[0]);
        //ln.setOptions({ strokeWeight: 3, strokeColor: '#AF4167 ' });
        var lp = new google.maps.LatLng(parseFloat(cleantext[ad].ycoordinate), parseFloat(cleantext[ad].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 20, hideZoom: 60, visible: true, map: map, cssName: 'nlLabelCleantext', position: lp, text: cleantext[ad].label });
        CLEANTEXT_LAYER.push(_label);
    }
}
function showDisBoundariesGosha(response) {
    var gosha = $.map(response, function (el) { return el; })
    for (ae = 0; ae < gosha.length; ae++) {
        var lp = new google.maps.LatLng(parseFloat(gosha[ae].ycoordinate), parseFloat(gosha[ae].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 18, hideZoom: 20, visible: true, map: map, cssName: 'nlLabelGosha', position: lp, text: gosha[ae].label });
        GOSHA_LAYER.push(_label);
    }
}
function showDisBoundariesText(response) {
    var textla = $.map(response, function (el) { return el; })
    for (af = 0; af < textla.length; af++) {
        var lp = new google.maps.LatLng(parseFloat(textla[af].ycoordinate), parseFloat(textla[af].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 18, visible: true, map: map, cssName: 'nlLabelKhasraText', position: lp, text: textla[af].label });
        TEXT_LAYER.push(_label);
    }
}
function showDisBoundariesDimentionText(response) {
    var dimentiontext = $.map(response, function (el) { return el; })
    for (ag = 0; ag < dimentiontext.length; ag++) {
        var lp = new google.maps.LatLng(parseFloat(dimentiontext[ag].ycoordinate), parseFloat(dimentiontext[ag].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 20, hideZoom: 60, visible: true, map: map, cssName: 'nlLabelDimentionText', position: lp, text: dimentiontext[ag].label });
        DIMENTIONTEXT_LAYER.push(_label);
    }
}
function showDisBoundariesCloseText(response) {
    var closetext = $.map(response, function (el) { return el; })
    for (ah = 0; ah < closetext.length; ah++) {
        var lp = new google.maps.LatLng(parseFloat(closetext[ah].ycoordinate), parseFloat(closetext[ah].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 18, hideZoom: 20, visible: true, map: map, cssName: 'nlLabelCloseText', position: lp, text: closetext[ah].label });
        CLOSETEXT_LAYER.push(_label);
    }
}
function showDisBoundariesVillageText(response) {
    var villagetext = $.map(response, function (el) { return el; })
    for (ai = 0; ai < villagetext.length; ai++) {
        var lp = new google.maps.LatLng(parseFloat(villagetext[ai].ycoordinate), parseFloat(villagetext[ai].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 18, visible: true, map: map, cssName: 'nlLabelVillageText', position: lp, text: villagetext[ai].label });
        VILLAGETEXT_LAYER.push(_label);
    }
}
function showDisBoundariesKhasraNo(response) {
    var khasrano = $.map(response, function (el) { return el; })
    for (aj = 0; aj < khasrano.length; aj++) {
        var lp = new google.maps.LatLng(parseFloat(khasrano[aj].ycoordinate), parseFloat(khasrano[aj].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 18, visible: true, map: map, cssName: 'nlLabelKhasraNo', position: lp, text: khasrano[aj].label });
        KHASRANO_LAYER.push(_label);
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
