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
var Polys = [];
var VILLAGEID_UNIVERSAL = [];
var RECTWITHKHASRANO_LAYER = [];
var WELL_LAYER = [];
var alljsondata = [];
$(document).ready(function () {
    HttpGet(`/GIS/GetZoneList`, 'json', function (response) {
        var html = '';
        for (var i = 0; i < response.length; i++) {
            var check = 0;
            html = html + '<div class="accordion-item"><h5 class="accordion-header" id="heading' + response[i].id + '"><button id="Z' + response[i].id + '" class="accordion-button collapsed py-2 bg-primary text-white" type="button" onclick="showZone(this.id)" data-bs-toggle="collapse" data-bs-target="#collapse' + response[i].id + '" aria-expanded="true" aria-controls="collapse' + response[i].id + '"><i class="ri-arrow-right-s-fill text-white"></i>' + response[i].name + '</button></h5><div id="collapse' + response[i].id + '" class="accordion-collapse collapse" aria-labelledby="heading' + response[i].id + '" data-bs-parent="#accordionData"><div class="accordion-body"><div class="list-group">';
            if (response[i].village.length > 0) {
                for (var j = 0; j < response[i].village.length; j++) {
                    if (response[i].village[j].isActive == 1) {
                        html = html + '<a href="javascript:void(0);" id="V' + response[i].village[j].id + '" class="list-group-item list-group-item-action" onclick="showVillage(this.id)"><i class="ri-eye-line"></i> ' + response[i].village[j].name + '</a>';
                        check++;
                    }
                }
            }
            if (check == 0) {
                html = html + '<a href="#" class="list-group-item list-group-item-action"><i class="ri-eye-line"></i> Village Not Found</a>';
            }
            html = html + '</div></div></div></div>';
        }
        $("#accordionData").html('');
        $("#accordionData").html(html);
    });
    callSelect2();
});

function CoordMapType() { }

CoordMapType.prototype.tileSize = new google.maps.Size(256, 256);
CoordMapType.prototype.maxZoom = 25;

CoordMapType.prototype.getTile = function (coord, zoom, ownerDocument) {
    var div = ownerDocument.createElement('div');
    div.style.width = this.tileSize.width + 'px';
    div.style.height = this.tileSize.height + 'px';
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

    };
    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
    map.mapTypes.set('coordinate', coordinateMapType);
    map.addListener('zoom_changed', function () {
        Zoom_change(map);
    });
    //Loads custom zoom controls
    var zoomDiv = document.createElement('div');
    var renderZoomControls = new ZoomControl(zoomDiv, this.map);
    zoomDiv.index = 1;
    var rightdiv = document.getElementById('rightsidebuttons');
    rightdiv.appendChild(zoomDiv);
    getStateboundary();

    $("select").each(function () {
        if ($(this).hasClass("select2destroy") == false) {
            $(this).select2();
        }
    });
    $('.numbers').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    });
    $('.onlynumbers').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9]/g, ''));
    });
}

function getStateboundary() {
    HttpGet(`/GIS/GetInitiallyStateDetails`, 'json', function (response) {
        setStateboundary(response);
    });
}

function setStateboundary(response) {
    var arr = $.map(response, function (el) { return el; })

    for (i = 0; i < arr.length; i++) {
        var ln = createPolygon(getLatLongArr(arr[i].polygon));
        ln.setOptions({ visibleZoom: 7, fillColor: arr[i].colorcode, hideZoom: 9, visible: true, map: map, strokeWeight: 1, strokeColor: '#47C4C8', fillOpacity: 0.3, clickable: !1 });
        var lp = new google.maps.LatLng(parseFloat(arr[i].ycoordinate), parseFloat(arr[i].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 0, hideZoom: 18, visible: true, map: map, cssName: 'nlLabelState', position: lp, text: arr[i].label });

        STATE_LAYER.push(_label);
        Polys.push(ln);
    }
}
/*Zone Boundary Start*/
function showZone(maxima) {
    var newdis_id = maxima.replace('Z', '');
    SetMapNull();
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
function showVillage(maxima) {
    var villageid = maxima.replace('V', '');
    HttpGet("/GIS/GetVillageDetails?VillageId=" + parseInt(villageid), 'json', function (response, villageid) {
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
    map.setZoom(15);
    map.panTo(new google.maps.LatLng(yaixis, xaixis));

    showvillagelayers(villageid);
}
function showvillagelayers(villageid) {

    HttpGet("/GIS/GetGisDataLayersDetails?VillageId=" + parseInt(villageid), 'json', function (response) {
        //sessionStorage.clear();
       // localStorage.setItem('alldata', JSON.stringify(response)); //data added to local storage
       // sessionStorage.setItem('alldata', JSON.stringify(response)); //data added to session storage
        alljsondata = [];
        alljsondata = JSON.stringify(response);
        var Abadi = response.filter((x) => x.gisLayerId === 1);    //abadi
        if (Abadi.length > 0 && Abadi[0].checkedStatus == 1)
            showDisBoundariesAbadi(Abadi);

        var Burji = response.filter((x) => x.gisLayerId === 2);//burji
        if (Burji.length > 0 && Burji[0].checkedStatus == 1)
            showDisBoundariesBurji(Burji);

        var Clean = response.filter((x) => x.gisLayerId === 3);//clean
        if (Clean.length > 0 && Clean[0].checkedStatus == 1)
            showDisBoundariesClean(Clean);

        var Dim = response.filter((x) => x.gisLayerId === 4);//Dimension
        if (Dim.length > 0 && Dim[0].checkedStatus == 1)
            showDisBoundariesDim(Dim);

        var Encroachment = response.filter((x) => x.gisLayerId === 5);//Encroachment
        if (Encroachment.length > 0 && Encroachment[0].checkedStatus == 1)
            showDisBoundariesEncroachment(Encroachment);

        var Grid = response.filter((x) => x.gisLayerId === 6);//Grid
        if (Grid.length > 0 && Grid[0].checkedStatus == 1)
            showDisBoundariesGrid(Grid);

        var Nala = response.filter((x) => x.gisLayerId === 7);//Nala
        if (Nala.length > 0 && Nala[0].checkedStatus == 1)
            showDisBoundariesNala(Nala);

        var TriJunction = response.filter((x) => x.gisLayerId === 8);//TriJunction
        if (TriJunction.length > 0 && TriJunction[0].checkedStatus == 1)
            showDisBoundariesTriJunction(TriJunction);

        var Zero = response.filter((x) => x.gisLayerId === 9);//Zero
        if (Zero.length > 0 && Zero[0].checkedStatus == 1)
            showDisBoundariesZero(Zero);

        var Nali = response.filter((x) => x.gisLayerId === 10);//Nali
        if (Nali.length > 0 && Nali[0].checkedStatus == 1)
            showDisBoundariesNali(Nali);

        var RailwayLine = response.filter((x) => x.gisLayerId === 11);//RailwayLine
        if (RailwayLine.length > 0 && RailwayLine[0].checkedStatus == 1)
            showDisBoundariesRailwayLine(RailwayLine);

        var FieldBoun = response.filter((x) => x.gisLayerId === 12);//FieldBoun
        if (FieldBoun.length > 0 && FieldBoun[0].checkedStatus == 1)
            showDisBoundariesFieldBoun(FieldBoun);

        var Killa = response.filter((x) => x.gisLayerId === 13);//Killa
        if (Killa.length > 0 && Killa[0].checkedStatus == 1)
            showDisBoundariesKilla(Killa);

        var Close = response.filter((x) => x.gisLayerId === 14);//Close
        if (Close.length > 0 && Close[0].checkedStatus == 1)
            showDisBoundariesClose(Close);

        var Saheda = response.filter((x) => x.gisLayerId === 15);//Saheda
        if (Saheda.length > 0 && Saheda[0].checkedStatus == 1)
            showDisBoundariesSaheda(Saheda);

        var KachaPakaLine = response.filter((x) => x.gisLayerId === 16);//KachaPakaLine
        if (KachaPakaLine.length > 0 && KachaPakaLine[0].checkedStatus == 1)
            showDisBoundariesKachaPakaLine(KachaPakaLine);

        var KhasraLine = response.filter((x) => x.gisLayerId === 17);//KhasraLine
        if (KhasraLine.length > 0 && KhasraLine[0].checkedStatus == 1)
            showDisBoundariesKhasraLine(KhasraLine);

        var Well = response.filter((x) => x.gisLayerId === 31);//Well
        if (Well.length > 0 && Well[0].checkedStatus == 1)
            showDisBoundariesWell(Well);

        var KhasraBoundary = response.filter((x) => x.gisLayerId === 18);//KhasraBoundary
        if (KhasraBoundary.length > 0 && KhasraBoundary[0].checkedStatus == 1)
            showDisBoundariesKhasraBoundary(KhasraBoundary);

        var Road = response.filter((x) => x.gisLayerId === 19);//Road
        if (Road.length > 0 && Road[0].checkedStatus == 1)
            showDisBoundariesRoad(Road);

        var Dashed = response.filter((x) => x.gisLayerId === 20);//Dashed
        if (Dashed.length > 0 && Dashed[0].checkedStatus == 1)
            showDisBoundariesDashed(Dashed);

        var Inner = response.filter((x) => x.gisLayerId === 21);//Inner
        if (Inner.length > 0 && Inner[0].checkedStatus == 1)
            showDisBoundariesInner(Inner);

        var VillageBoundary = response.filter((x) => x.VillageBoundary === 22);//VillageBoundary
        if (VillageBoundary.length > 0 && VillageBoundary[0].checkedStatus == 1)
            showDisBoundariesVillageBoundary(VillageBoundary);

        var Cleantext = response.filter((x) => x.gisLayerId === 23);//Cleantext
        if (Cleantext.length > 0 && Cleantext[0].checkedStatus == 1)
            showDisBoundariesCleantext(Cleantext);

        var Gosha = response.filter((x) => x.gisLayerId === 24);//Gosha
        if (Gosha.length > 0 && Gosha[0].checkedStatus == 1)
            showDisBoundariesGosha(Gosha);

        var Text = response.filter((x) => x.gisLayerId === 25);//Text
        if (Text.length > 0 && Text[0].checkedStatus == 1)
            showDisBoundariesText(Text);

        var DimentionText = response.filter((x) => x.gisLayerId === 26);//DimentionText
        if (DimentionText.length > 0 && DimentionText[0].checkedStatus == 1)
            showDisBoundariesDimentionText(DimentionText);

        var CloseText = response.filter((x) => x.gisLayerId === 27);//CloseText
        if (CloseText.length > 0 && CloseText[0].checkedStatus == 1)
            showDisBoundariesCloseText(CloseText);

        var VillageText = response.filter((x) => x.gisLayerId === 28);//VillageText
        if (VillageText.length > 0 && VillageText[0].checkedStatus == 1)
            showDisBoundariesVillageText(VillageText);

        var KhasraNo = response.filter((x) => x.gisLayerId === 29);//KhasraNo
        if (KhasraNo.length > 0 && KhasraNo[0].checkedStatus == 1)
            showDisBoundariesKhasraNo(KhasraNo);

        var RectWithKhasraNo = response.filter((x) => x.gisLayerId === 30);//RectWithKhasraNo
        if (RectWithKhasraNo.length > 0 && RectWithKhasraNo[0].checkedStatus == 1)
            showDisBoundariesRectWithKhasraNo(RectWithKhasraNo);
    });
    VILLAGEID_UNIVERSAL = [];
    VILLAGEID_UNIVERSAL.push(villageid);
}

function showVillageBoundaries(response) {
    var data = $.map(response, function (el) { return el; })
    for (i = 0; i < data.length; i++) {
        switch (data) {
            case (data[i].type == "Line"):
                var ln = createLine(getLatLongArr(data[i].polygon));
                ln.setOptions({ strokeWeight: 3, strokeColor: data[i].fillColor });
                ABADI_LAYER.push(ln);
                Polys.push(ln);

                break;

            case (data[i].type == "Point"):
                var lp = new google.maps.LatLng(parseFloat(data[i].ycoordinate), parseFloat(data[i].xcoordinate));
                var _label = new google.maps.Label({ visibleZoom: 18, hideZoom: 20, visible: true, map: map, cssName: 'nlLabelGosha', position: lp, text: data[i].label });
                GOSHA_LAYER.push(_label);
                Polys.push(_label);

                break;
        }


    }
}
function showDisBoundariesAbadi(response) {
    var abadi = $.map(response, function (el) { return el; })
    for (i = 0; i < abadi.length; i++) {
        var ln = createLine(getLatLongArr(abadi[i].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: abadi[i].fillColor });
        ABADI_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesBurji(response) {
    var burji = $.map(response, function (el) { return el; })
    if (burji[0].checkedStatus == 1) {
        for (e = 0; e < burji.length; e++) {
            var ln = createLine(getLatLongArr(burji[e].polygon));
            ln.setOptions({ strokeWeight: 3, strokeColor: burji[e].fillColor });
            BURJI_LAYER.push(ln);
            Polys.push(ln);
        }

    }
}
function showDisBoundariesClean(response) {
    var clean = $.map(response, function (el) { return el; })
    for (f = 0; f < clean.length; f++) {
        var ln = createLine(getLatLongArr(clean[f].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: clean[f].fillColor });
        CLEAN_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesDim(response) {
    var dim = $.map(response, function (el) { return el; })
    for (h = 0; h < DIM_LAYER.length; h++) {
        DIM_LAYER[h].setMap(null);
    }
    for (h = 0; h < dim.length; h++) {
        var poly = createPolygon(getLatLongArr(dim[h].polygon));
        poly.setOptions({ strokeWeight: 1, strokeColor: dim[h].fillColor, fillOpacity: 0, clickable: !0 });
        DIM_LAYER.push(poly);
        map.panTo(new google.maps.LatLng(dim[h].ycoordinate, dim[h].xcoordinate));
    }
}
function showDisBoundariesEncroachment(response) {
    var encroachment = $.map(response, function (el) { return el; })
    for (j = 0; j < encroachment.length; j++) {
        var ln = createLine(getLatLongArr(encroachment[j].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: encroachment[j].fillColor });
        ENCROACHMENT_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesGrid(response) {
    var grid = $.map(response, function (el) { return el; })
    for (k = 0; k < grid.length; k++) {
        var ln = createLine(getLatLongArr(grid[k].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: grid[k].fillColor });
        GRID_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesNala(response) {
    var nala = $.map(response, function (el) { return el; })
    for (m = 0; m < nala.length; m++) {
        var ln = createLine(getLatLongArr(nala[m].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: nala[m].fillColor });
        NALA_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesTriJunction(response) {
    var trijunction = $.map(response, function (el) { return el; })
    for (n = 0; n < trijunction.length; n++) {
        var ln = createLine(getLatLongArr(trijunction[n].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: trijunction[n].fillColor });
        TRIJUNCTION_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesZero(response) {
    var zero = $.map(response, function (el) { return el; })
    for (o = 0; o < zero.length; o++) {
        var ln = createLine(getLatLongArr(zero[o].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: zero[o].fillColor });
        ZERO_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesNali(response) {
    var nali = $.map(response, function (el) { return el; })
    for (p = 0; p < nali.length; p++) {
        var ln = createLine(getLatLongArr(nali[p].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: nali[p].fillColor });
        NALI_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesRailwayLine(response) {
    var railwayline = $.map(response, function (el) { return el; })
    for (q = 0; q < railwayline.length; q++) {
        var ln = createLine(getLatLongArr(railwayline[q].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: railwayline[q].fillColor });
        RAILWAYLINE_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesFieldBoun(response) {
    var fieldboun = $.map(response, function (el) { return el; })
    for (r = 0; r < fieldboun.length; r++) {
        var ln = createLine(getLatLongArr(fieldboun[r].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: fieldboun[r].fillColor });
        FIELDBOUN_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesKilla(response) {
    var killa = $.map(response, function (el) { return el; })
    for (s = 0; s < killa.length; s++) {
        var ln = createLine(getLatLongArr(killa[s].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: killa[s].fillColor });
        KILLA_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesClose(response) {
    var close = $.map(response, function (el) { return el; })
    for (t = 0; t < close.length; t++) {
        var ln = createLine(getLatLongArr(close[t].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: close[t].fillColor });
        CLOSE_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesSaheda(response) {
    var saheda = $.map(response, function (el) { return el; })
    for (u = 0; u < saheda.length; u++) {
        var ln = createLine(getLatLongArr(saheda[u].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: saheda[u].fillColor });
        SAHEDA_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesKachaPakaLine(response) {
    var kachapakaline = $.map(response, function (el) { return el; })
    for (v = 0; v < kachapakaline.length; v++) {
        var ln = createLine(getLatLongArr(kachapakaline[v].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: kachapakaline[v].fillColor });
        KACHAPAKALINE_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesKhasraLine(response) {
    var khasraline = $.map(response, function (el) { return el; })
    for (w = 0; w < khasraline.length; w++) {
        var ln = createLine(getLatLongArr(khasraline[w].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: khasraline[w].fillColor });
        KHASRALINE_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesKhasraBoundary(response) {
    var khasraboundary = $.map(response, function (el) { return el; })
    for (x = 0; x < khasraboundary.length; x++) {
        var ln = createLine(getLatLongArr(khasraboundary[x].polygon));
        ln.setOptions({ strokeWeight: 1, strokeColor: khasraboundary[x].fillColor });
        KHASRABOUNDARY_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesRoad(response) {
    var road = $.map(response, function (el) { return el; })
    for (y = 0; y < road.length; y++) {
        var ln = createLine(getLatLongArr(road[y].polygon));
        ln.setOptions({ strokeWeight: 1, strokeColor: road[y].fillColor });
        ROAD_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesDashed(response) {
    var dashed = $.map(response, function (el) { return el; })
    for (z = 0; z < dashed.length; z++) {
        var ln = createLine(getLatLongArr(dashed[z].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: dashed[z].fillColor });
        DASHED_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesInner(response) {
    var inner = $.map(response, function (el) { return el; })
    for (ab = 0; ab < inner.length; ab++) {
        var ln = createLine(getLatLongArr(inner[ab].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: inner[ab].fillColor });
        INNER_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesVillageBoundary(response) {
    var villageboundary = $.map(response, function (el) { return el; })
    for (ac = 0; ac < villageboundary.length; ac++) {
        var ln = createLine(getLatLongArr(villageboundary[ac].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: villageboundary[ac].fillColor });
        VILLAGEBOUNDARY_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesCleantext(response) {
    var cleantext = $.map(response, function (el) { return el; })
    for (ad = 0; ad < cleantext.length; ad++) {
        //var ln = createPoint(getLatLongArr(cleantext[g].polygon)[0]);
        //ln.setOptions({ strokeWeight: 3, strokeColor: '#AF4167 ' });
        var lp = new google.maps.LatLng(parseFloat(cleantext[ad].ycoordinate), parseFloat(cleantext[ad].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 60, visible: true, map: map, cssName: 'nlLabelCleantext', position: lp, text: cleantext[ad].label });
        CLEANTEXT_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesGosha(response) {
    var gosha = $.map(response, function (el) { return el; })
    for (ae = 0; ae < gosha.length; ae++) {
        var lp = new google.maps.LatLng(parseFloat(gosha[ae].ycoordinate), parseFloat(gosha[ae].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 26, visible: true, map: map, cssName: 'nlLabelGosha', position: lp, text: gosha[ae].label });
        GOSHA_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesText(response) {
    var textla = $.map(response, function (el) { return el; })
    for (af = 0; af < textla.length; af++) {
        var lp = new google.maps.LatLng(parseFloat(textla[af].ycoordinate), parseFloat(textla[af].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 18, visible: true, map: map, cssName: 'nlLabelKhasraText', position: lp, text: textla[af].label });
        TEXT_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesDimentionText(response) {
    var dimentiontext = $.map(response, function (el) { return el; })
    for (ag = 0; ag < dimentiontext.length; ag++) {
        var lp = new google.maps.LatLng(parseFloat(dimentiontext[ag].ycoordinate), parseFloat(dimentiontext[ag].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 60, visible: true, map: map, cssName: 'nlLabelDimentionText', position: lp, text: dimentiontext[ag].label });
        DIMENTIONTEXT_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesCloseText(response) {
    var closetext = $.map(response, function (el) { return el; })
    for (ah = 0; ah < closetext.length; ah++) {
        var lp = new google.maps.LatLng(parseFloat(closetext[ah].ycoordinate), parseFloat(closetext[ah].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 26, visible: true, map: map, cssName: 'nlLabelCloseText', position: lp, text: closetext[ah].label });
        CLOSETEXT_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesVillageText(response) {
    var villagetext = $.map(response, function (el) { return el; })
    for (ai = 0; ai < villagetext.length; ai++) {
        var lp = new google.maps.LatLng(parseFloat(villagetext[ai].ycoordinate), parseFloat(villagetext[ai].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 18, visible: true, map: map, cssName: 'nlLabelVillageText', position: lp, text: villagetext[ai].label });
        VILLAGETEXT_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesKhasraNo(response) {
    var khasrano = $.map(response, function (el) { return el; })
    for (aj = 0; aj < khasrano.length; aj++) {
        var lp = new google.maps.LatLng(parseFloat(khasrano[aj].ycoordinate), parseFloat(khasrano[aj].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 22, visible: true, map: map, cssName: 'nlLabelKhasraNo', position: lp, text: khasrano[aj].label, color: khasrano[aj].fillColor, clickable: !0 });
        _label.khasrano = khasrano[aj].label;
        _label.villageid = VILLAGEID_UNIVERSAL[0];
        google.maps.event.addListener(_label, 'click', function () {
            getInfo(this.villageid, this.khasrano);
        });
        KHASRANO_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesRectWithKhasraNo(response) {
    var rectkhasrano = $.map(response, function (el) { return el; })
    for (ak = 0; ak < rectkhasrano.length; ak++) {
        var lp = new google.maps.LatLng(parseFloat(rectkhasrano[ak].ycoordinate), parseFloat(rectkhasrano[ak].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 22, visible: true, map: map, cssName: 'nlLabelRectWithKhasraNo', position: lp, text: rectkhasrano[ak].label, color: rectkhasrano[ak].fillColor });

        RECTWITHKHASRANO_LAYER.push(_label);

        Polys.push(_label);
    }
}

function showDisBoundariesWell(response) {
    var well = $.map(response, function (el) { return el; })
    for (al = 0; al < well.length; al++) {
        var ln = createLine(getLatLongArr(well[al].polygon));
        ln.setOptions({ strokeWeight: 1, strokeColor: clean[al].fillColor });
        WELL_LAYER.push(ln);
        Polys.push(ln);
    }
}

/*Village Boundary End*/

function getInfo(villageid, khasrano) {

    HttpGet("/GIS/GetKhasraBasisOtherDetails?VillageId=" + parseInt(villageid) + "&KhasraNo=" + khasrano, 'json', function (response) {
        showKhasraBasisOtherDetails(response);
    });

    HttpGet("/GIS/GetKhasraBasisOtherDetailsForCourtCases?VillageId=" + parseInt(villageid) + "&KhasraNo=" + khasrano, 'json', function (response) {
        showKhasraBasisOtherDetailsForCourtCases(response);
    });
}

function showKhasraBasisOtherDetails(resp) {
    debugger;
    var tbl = $('#RouteDetailShow table').empty();
    tbl.empty();

    if (resp.length > 0) {

        $.each(resp[0], function (indx, itm) {
            if (itm != null && itm != "")
                tbl.append('<tr><td> <p class="m-0">' + itm.split(",")[0] + ' : <strong id="' + indx + '">' + itm.split(",")[1] + '</strong></p></td> </tr>');

        });

        //$('#tagVillageName').empty().append(resp[0].villageName);
        //$('#tagKhasra').empty().append(resp[0].khasraNo);
        //$('#tagArea').empty().append(resp[0].area);
        //$('#tagUs4').empty().append(resp[0].us4);
        //$('#tagUs6').empty().append(resp[0].us6);
        //$('#tagUs17').empty().append(resp[0].us17);
        //$('#tagUs22').empty().append(resp[0].us22);
        //$('#tagAward').empty().append(resp[0].award);
        //$('#tagPossessionDate').empty().append(resp[0].possessionDate);
        //$('#tagAllotmentDate').empty().append(resp[0].allotmentDate);
        //$('#tagTransferDepartment').empty().append(resp[0].transferDepartment);
        //$('#tagSchemeTransfer').empty().append(resp[0].schemeTransfer);
        //$('#tagRemarks').empty().append(resp[0].remarks);
        //$('#tagPartyName').empty().append(resp[0].partyName);
        //$('#tagDemandListNo').empty().append(resp[0].demandListNo);
        //$('#tagLBDate').empty().append(resp[0].lBNo);
        //$('#tagLACNo').empty().append(resp[0].lACNo);
        //$('#tagRFANo').empty().append(resp[0].rFANo);
        //$('#tagSLANo').empty().append(resp[0].sLPNo);
        //$('#tagCourt').empty().append(resp[0].court);
        //$('#tagPayableAmt').empty().append(resp[0].payableAmt);
        //$('#tagAppealableAmt').empty().append(resp[0].appealableAmt);
        $('#RouteDetailShow').show();
    }
    else {
        $('#RouteDetailShow').hide();
        InfoMessage('No Data Found');
    }

}

function showKhasraBasisOtherDetailsForCourtCases(resp) {
    debugger;
    var html = '';
    var tbl = '';
    if (resp.length > 0) {
        for (var i = 0; i < resp.length; i++) {
            html = html + '<div class="accordion-item"><h5 class="accordion-header" id="headingCourt' + i + '"><button id="ZCourt' + i + '" class="accordion-button collapsed py-2 bg-primary text-white" type="button" data-bs-toggle="collapse" data-bs-target="#collapseCourt' + i + '" aria-expanded="true" aria-controls="collapseCourt' + i + '"><i class="ri-arrow-right-s-fill text-white"></i> Case No : ' + resp[i].name + '</button></h5><div id="collapseCourt' + i + '" class="accordion-collapse collapse" aria-labelledby="headingCourt' + i + '" data-bs-parent="#accordionData"><div class="accordion-body"><div class="list-group"><table style="border-collapse: collapse; font-size:11px !important; margin-bottom: 0px;" class="table table-bordererd table-primary table-responsive"><tbody>';
            $.each(resp[i], function (indx, itm) {
                if (itm != null && itm != "" && indx != 'name')
                    html = html + '<tr><td> <p class="m-0">' + itm.split(",")[0] + ' : <strong id="' + indx + '">' + itm.split(",")[1] + '</strong></p></td> </tr>';

            });
            html = html + '</tbody></table></div></div></div></div>';
        }
        $("#CourtCaseData").html('');
        $("#CourtCaseData").html(html);
        $('#RouteDetailShow').show();
    }
    else {
        $('#RouteDetailShow').hide();
    }

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

$('#RouteDetail').on('click', function () {
    $('#RouteDetailShow').show();
});
$('#HideRouteDetail').on('click', function () {
    $('#RouteDetailShow').hide();
});
$(function () {
    $("#RouteDetailShow").draggable();
});

$(document).on('change', '#chkAllImpInfra', function (e) {   /*Select all Functionality added by renu */
    e.preventDefault();
   // var data = JSON.parse(localStorage.getItem('alldata'));  //get data from local storage
    var data = alljsondata;  //get data from local storage
    $(this).closest('table').find('td input[type="checkbox"]').prop('checked', $(this).prop('checked'));
    if (this.checked) {
        $('#chkAllImpInfra').attr("checked", "checked");
        $('#chkAllImpInfra').closest('table').find('td input[type="checkbox"]').prop('checked', true);

        var Abadi = data.filter((x) => x.gisLayerId === 1);    //abadi
        if (Abadi.length > 0)
            showDisBoundariesAbadi(Abadi);

        var Burji = data.filter((x) => x.gisLayerId === 2);//burji
        if (Burji.length > 0)
            showDisBoundariesBurji(Burji);

        var Clean = data.filter((x) => x.gisLayerId === 3);//clean
        if (Clean.length > 0)
            showDisBoundariesClean(Clean);

        var Dim = data.filter((x) => x.gisLayerId === 4);//Dimension
        if (Dim.length > 0)
            showDisBoundariesDim(Dim);

        var Encroachment = data.filter((x) => x.gisLayerId === 5);//Encroachment
        if (Encroachment.length > 0)
            showDisBoundariesEncroachment(Encroachment);

        var Grid = data.filter((x) => x.gisLayerId === 6);//Grid
        if (Grid.length > 0)
            showDisBoundariesGrid(Grid);

        var Nala = data.filter((x) => x.gisLayerId === 7);//Nala
        if (Nala.length > 0)
            showDisBoundariesNala(Nala);

        var TriJunction = data.filter((x) => x.gisLayerId === 8);//TriJunction
        if (TriJunction.length > 0)
            showDisBoundariesTriJunction(TriJunction);

        var Zero = data.filter((x) => x.gisLayerId === 9);//Zero
        if (Zero.length > 0)
            showDisBoundariesZero(Zero);

        var Nali = data.filter((x) => x.gisLayerId === 10);//Nali
        if (Nali.length > 0)
            showDisBoundariesNali(Nali);

        var RailwayLine = data.filter((x) => x.gisLayerId === 11);//RailwayLine
        if (RailwayLine.length > 0)
            showDisBoundariesRailwayLine(RailwayLine);

        var FieldBoun = data.filter((x) => x.gisLayerId === 12);//FieldBoun
        if (FieldBoun.length > 0)
            showDisBoundariesFieldBoun(FieldBoun);

        var Killa = data.filter((x) => x.gisLayerId === 13);//Killa
        if (Killa.length > 0)
            showDisBoundariesKilla(Killa);

        var Close = data.filter((x) => x.gisLayerId === 14);//Close
        if (Close.length > 0)
            showDisBoundariesClose(Close);

        var Saheda = data.filter((x) => x.gisLayerId === 15);//Saheda
        if (Saheda.length > 0)
            showDisBoundariesSaheda(Saheda);

        var KachaPakaLine = data.filter((x) => x.gisLayerId === 16);//KachaPakaLine
        if (KachaPakaLine.length > 0)
            showDisBoundariesKachaPakaLine(KachaPakaLine);

        var KhasraLine = data.filter((x) => x.gisLayerId === 17);//KhasraLine
        if (KhasraLine.length > 0)
            showDisBoundariesKhasraLine(KhasraLine);

        var KhasraBoundary = data.filter((x) => x.gisLayerId === 18);//KhasraBoundary
        if (KhasraBoundary.length > 0)
            showDisBoundariesKhasraBoundary(KhasraBoundary);

        var Road = data.filter((x) => x.gisLayerId === 19);//Road
        if (Road.length > 0)
            showDisBoundariesRoad(Road);

        var Dashed = data.filter((x) => x.gisLayerId === 20);//Dashed
        if (Dashed.length > 0)
            showDisBoundariesDashed(Dashed);

        var Inner = data.filter((x) => x.gisLayerId === 21);//Inner
        if (Inner.length > 0)
            showDisBoundariesInner(Inner);

        var VillageBoundary = data.filter((x) => x.VillageBoundary === 22);//VillageBoundary
        if (VillageBoundary.length > 0)
            showDisBoundariesVillageBoundary(VillageBoundary);

        var Cleantext = data.filter((x) => x.gisLayerId === 23);//Cleantext
        if (Cleantext.length > 0)
            showDisBoundariesCleantext(Cleantext);

        var Gosha = data.filter((x) => x.gisLayerId === 24);//Gosha
        if (Gosha.length > 0)
            showDisBoundariesGosha(Gosha);

        var Text = data.filter((x) => x.gisLayerId === 25);//Text
        if (Text.length > 0)
            showDisBoundariesText(Text);

        var DimentionText = data.filter((x) => x.gisLayerId === 26);//DimentionText
        if (DimentionText.length > 0)
            showDisBoundariesDimentionText(DimentionText);

        var CloseText = data.filter((x) => x.gisLayerId === 27);//CloseText
        if (CloseText.length > 0)
            showDisBoundariesCloseText(CloseText);

        var VillageText = data.filter((x) => x.gisLayerId === 28);//VillageText
        if (VillageText.length > 0)
            showDisBoundariesVillageText(VillageText);

        var KhasraNo = data.filter((x) => x.gisLayerId === 29);//KhasraNo
        if (KhasraNo.length > 0)
            showDisBoundariesKhasraNo(KhasraNo);

        var RectWithKhasraNo = data.filter((x) => x.gisLayerId === 30);//RectWithKhasraNo
        if (RectWithKhasraNo.length > 0)
            showDisBoundariesRectWithKhasraNo(RectWithKhasraNo);

        var Well = data.filter((x) => x.gisLayerId === 31);//Well
        if (Well.length > 0)
            showDisBoundariesWell(Well);
    }
    else {
        $('#chkAllImpInfra').closest('table').find('td input[type="checkbox"]').prop('checked', false);
        $('#chkAllImpInfra').closest('table').find('td input[type="checkbox"]').removeAttr('checked');

        var Abadi = data.filter((x) => x.gisLayerId === 1);    //abadi
        if (Abadi.length > 0) {
            for (h = 0; h < ABADI_LAYER.length; h++) {
                ABADI_LAYER[h].setMap(null);
            }
        }

        var Burji = data.filter((x) => x.gisLayerId === 2);//burji
        if (Burji.length > 0) {
            for (h = 0; h < BURJI_LAYER.length; h++) {
                BURJI_LAYER[h].setMap(null);
            }
        }

        var Clean = data.filter((x) => x.gisLayerId === 3);//clean
        if (Clean.length > 0) {
            for (h = 0; h < CLEAN_LAYER.length; h++) {
                CLEAN_LAYER[h].setMap(null);
            }
        }

        var Dim = data.filter((x) => x.gisLayerId === 4);//Dimension
        if (Dim.length > 0) {
            for (h = 0; h < DIM_LAYER.length; h++) {
                DIM_LAYER[h].setMap(null);
            }
        }

        var Encroachment = data.filter((x) => x.gisLayerId === 5);//Encroachment
        if (Encroachment.length > 0) {
            for (h = 0; h < ENCROACHMENT_LAYER.length; h++) {
                ENCROACHMENT_LAYER[h].setMap(null);
            }
        }

        var Grid = data.filter((x) => x.gisLayerId === 6);//Grid
        if (Grid.length > 0) {
            for (h = 0; h < GRID_LAYER.length; h++) {
                GRID_LAYER[h].setMap(null);
            }
        }

        var Nala = data.filter((x) => x.gisLayerId === 7);//Nala
        if (Nala.length > 0) {
            for (h = 0; h < NALA_LAYER.length; h++) {
                NALA_LAYER[h].setMap(null);
            }
        }

        var TriJunction = data.filter((x) => x.gisLayerId === 8);//TriJunction
        if (TriJunction.length > 0) {
            for (h = 0; h < TRIJUNCTION_LAYER.length; h++) {
                TRIJUNCTION_LAYER[h].setMap(null);
            }
        }

        var Zero = data.filter((x) => x.gisLayerId === 9);//Zero
        if (Zero.length > 0) {
            for (h = 0; h < ZERO_LAYER.length; h++) {
                ZERO_LAYER[h].setMap(null);
            }
        }

        var Nali = data.filter((x) => x.gisLayerId === 10);//Nali
        if (Nali.length > 0) {
            for (h = 0; h < NALI_LAYER.length; h++) {
                NALI_LAYER[h].setMap(null);
            }
        }

        var RailwayLine = data.filter((x) => x.gisLayerId === 11);//RailwayLine
        if (RailwayLine.length > 0) {
            for (h = 0; h < RAILWAYLINE_LAYER.length; h++) {
                RAILWAYLINE_LAYER[h].setMap(null);
            }
        }

        var FieldBoun = data.filter((x) => x.gisLayerId === 12);//FieldBoun
        if (FieldBoun.length > 0) {
            for (h = 0; h < FIELDBOUN_LAYER.length; h++) {
                FIELDBOUN_LAYER[h].setMap(null);
            }
        }

        var Killa = data.filter((x) => x.gisLayerId === 13);//Killa
        if (Killa.length > 0) {
            for (h = 0; h < KILLA_LAYER.length; h++) {
                KILLA_LAYER[h].setMap(null);
            }
        }

        var Close = data.filter((x) => x.gisLayerId === 14);//Close
        if (Close.length > 0) {
            for (h = 0; h < CLOSE_LAYER.length; h++) {
                CLOSE_LAYER[h].setMap(null);
            }
        }

        var Saheda = data.filter((x) => x.gisLayerId === 15);//Saheda
        if (Saheda.length > 0) {
            for (h = 0; h < SAHEDA_LAYER.length; h++) {
                SAHEDA_LAYER[h].setMap(null);
            }
        }

        var KachaPakaLine = data.filter((x) => x.gisLayerId === 16);//KachaPakaLine
        if (KachaPakaLine.length > 0) {
            for (h = 0; h < KACHAPAKALINE_LAYER.length; h++) {
                KACHAPAKALINE_LAYER[h].setMap(null);
            }
        }

        var KhasraLine = data.filter((x) => x.gisLayerId === 17);//KhasraLine
        if (KhasraLine.length > 0) {
            for (h = 0; h < KHASRALINE_LAYER.length; h++) {
                KHASRALINE_LAYER[h].setMap(null);
            }
        }

        var KhasraBoundary = data.filter((x) => x.gisLayerId === 18);//KhasraBoundary
        if (KhasraBoundary.length > 0) {
            for (h = 0; h < KHASRABOUNDARY_LAYER.length; h++) {
                KHASRABOUNDARY_LAYER[h].setMap(null);
            }
        }

        var Road = data.filter((x) => x.gisLayerId === 19);//Road
        if (Road.length > 0) {
            for (h = 0; h < ROAD_LAYER.length; h++) {
                ROAD_LAYER[h].setMap(null);
            }
        }

        var Dashed = data.filter((x) => x.gisLayerId === 20);//Dashed
        if (Dashed.length > 0) {
            for (h = 0; h < DASHED_LAYER.length; h++) {
                DASHED_LAYER[h].setMap(null);
            }
        }

        var Inner = data.filter((x) => x.gisLayerId === 21);//Inner
        if (Inner.length > 0) {
            for (h = 0; h < INNER_LAYER.length; h++) {
                INNER_LAYER[h].setMap(null);
            }
        }

        var VillageBoundary = data.filter((x) => x.VillageBoundary === 22);//VillageBoundary
        if (VillageBoundary.length > 0) {
            for (h = 0; h < VILLAGEBOUNDARY_LAYER.length; h++) {
                VILLAGEBOUNDARY_LAYER[h].setMap(null);
            }
        }

        var Cleantext = data.filter((x) => x.gisLayerId === 23);//Cleantext
        if (Cleantext.length > 0) {
            for (h = 0; h < CLEANTEXT_LAYER.length; h++) {
                CLEANTEXT_LAYER[h].setMap(null);
            }
        }

        var Gosha = data.filter((x) => x.gisLayerId === 24);//Gosha
        if (Gosha.length > 0) {
            for (h = 0; h < GOSHA_LAYER.length; h++) {
                GOSHA_LAYER[h].setMap(null);
            }
        }

        var Text = data.filter((x) => x.gisLayerId === 25);//Text
        if (Text.length > 0) {
            for (h = 0; h < TEXT_LAYER.length; h++) {
                TEXT_LAYER[h].setMap(null);
            }
        }

        var DimentionText = data.filter((x) => x.gisLayerId === 26);//DimentionText
        if (DimentionText.length > 0) {
            for (h = 0; h < DIMENTIONTEXT_LAYER.length; h++) {
                DIMENTIONTEXT_LAYER[h].setMap(null);
            }
        }

        var CloseText = data.filter((x) => x.gisLayerId === 27);//CloseText
        if (CloseText.length > 0) {
            for (h = 0; h < CLOSETEXT_LAYER.length; h++) {
                CLOSETEXT_LAYER[h].setMap(null);
            }
        }

        var VillageText = data.filter((x) => x.gisLayerId === 28);//VillageText
        if (VillageText.length > 0) {
            for (h = 0; h < VILLAGETEXT_LAYER.length; h++) {
                VILLAGETEXT_LAYER[h].setMap(null);
            }
        }

        var KhasraNo = data.filter((x) => x.gisLayerId === 29);//KhasraNo
        if (KhasraNo.length > 0) {
            for (h = 0; h < KHASRANO_LAYER.length; h++) {
                KHASRANO_LAYER[h].setMap(null);
            }
        }

        var RectWithKhasraNo = data.filter((x) => x.gisLayerId === 30);//RectWithKhasraNo
        if (RectWithKhasraNo.length > 0) {
            for (h = 0; h < RECTWITHKHASRANO_LAYER.length; h++) {
                RECTWITHKHASRANO_LAYER[h].setMap(null);
            }
        }

        var Well = data.filter((x) => x.gisLayerId === 31);//clean
        if (Well.length > 0) {
            for (h = 0; h < WELL_LAYER.length; h++) {
                WELL_LAYER[h].setMap(null);
            }
        }


    }
});

$('#navLayerstab').on('click', function (e) {    /*on layer click functionality added by renu */
    for (i = 0; i < VILLAGEID_UNIVERSAL.length; i++) {
        var villageid = VILLAGEID_UNIVERSAL[i];
        GetInfrastructureDetails(villageid);
    }
});

function GetInfrastructureDetails(villageid) {

    HttpGet(`/GIS/GetInfrastructureDetails?VillageId=${parseInt(villageid)}`, 'json', function (response) {
        var html = '';
        for (var i = 0; i < response.length; i++) {
            var check = 0;
            if (response[i].checkedStatus == 1)
                html = html + '<tr>  <td> <div class="form-check"><input class="form-check-input  checkUncheckInfra" type="checkbox" id="' + response[i].code + '" checked="checked"> <label class="form-check-label" for="chkAbadi"> ' + response[i].name + '</label> </div> </td><td class="text-right" align="right"><i class="ri-checkbox-blank-fill" style="color: ' + response[i].fillColor + '"></i></td> </tr>';
            else
                html = html + '<tr>  <td> <div class="form-check"><input class="form-check-input  checkUncheckInfra" type="checkbox" id="' + response[i].code + '"> <label class="form-check-label" for="chkAbadi"> ' + response[i].name + '</label> </div> </td><td class="text-right" align="right"><i class="ri-checkbox-blank-fill" style="color: ' + response[i].fillColor + '"></i></td> </tr>';
        }
        $("#infrastructureData").html('');
        $("#infrastructureData").html(html);
        $("#chkSelectAll").show();
    });
}

$('#infrastructureData').on('change', '.checkUncheckInfra', function (e) {  /*check uncheck Functionality added by renu */
    debugger;
    e.preventDefault();
    var id = $(this).attr("id");
    //var data = JSON.parse(localStorage.getItem('alldata'));  //get data from local storage
    var data = JSON.parse(alljsondata);
    if ($(this).is(":checked")) {

        var Abadi = data.filter((x) => x.gisLayerId === 1);    //abadi
        if (Abadi.length > 0 && Abadi[0].code == id)
            showDisBoundariesAbadi(Abadi);

        var Burji = data.filter((x) => x.gisLayerId === 2);//burji
        if (Burji.length > 0 && Burji[0].code == id)
            showDisBoundariesBurji(Burji);

        var Clean = data.filter((x) => x.gisLayerId === 3);//clean
        if (Clean.length > 0 && Clean[0].code == id)
            showDisBoundariesClean(Clean);

        var Dim = data.filter((x) => x.gisLayerId === 4);//Dimension
        if (Dim.length > 0 && Dim[0].code == id)
            showDisBoundariesDim(Dim);

        var Encroachment = data.filter((x) => x.gisLayerId === 5);//Encroachment
        if (Encroachment.length > 0 && Encroachment[0].code == id)
            showDisBoundariesEncroachment(Encroachment);

        var Grid = data.filter((x) => x.gisLayerId === 6);//Grid
        if (Grid.length > 0 && Grid[0].code == id)
            showDisBoundariesGrid(Grid);

        var Nala = data.filter((x) => x.gisLayerId === 7);//Nala
        if (Nala.length > 0 && Nala[0].code == id)
            showDisBoundariesNala(Nala);

        var TriJunction = data.filter((x) => x.gisLayerId === 8);//TriJunction
        if (TriJunction.length > 0 && TriJunction[0].code == id)
            showDisBoundariesTriJunction(TriJunction);

        var Zero = data.filter((x) => x.gisLayerId === 9);//Zero
        if (Zero.length > 0 && Zero[0].code == id)
            showDisBoundariesZero(Zero);

        var Nali = data.filter((x) => x.gisLayerId === 10);//Nali
        if (Nali.length > 0 && Nali[0].code == id)
            showDisBoundariesNali(Nali);

        var RailwayLine = data.filter((x) => x.gisLayerId === 11);//RailwayLine
        if (RailwayLine.length > 0 && RailwayLine[0].code == id)
            showDisBoundariesRailwayLine(RailwayLine);

        var FieldBoun = data.filter((x) => x.gisLayerId === 12);//FieldBoun
        if (FieldBoun.length > 0 && FieldBoun[0].code == id)
            showDisBoundariesFieldBoun(FieldBoun);

        var Killa = data.filter((x) => x.gisLayerId === 13);//Killa
        if (Killa.length > 0 && Killa[0].code == id)
            showDisBoundariesKilla(Killa);

        var Close = data.filter((x) => x.gisLayerId === 14);//Close
        if (Close.length > 0 && Close[0].code == id)
            showDisBoundariesClose(Close);

        var Saheda = data.filter((x) => x.gisLayerId === 15);//Saheda
        if (Saheda.length > 0 && Saheda[0].code == id)
            showDisBoundariesSaheda(Saheda);

        var KachaPakaLine = data.filter((x) => x.gisLayerId === 16);//KachaPakaLine
        if (KachaPakaLine.length > 0 && KachaPakaLine[0].code == id)
            showDisBoundariesKachaPakaLine(KachaPakaLine);

        var KhasraLine = data.filter((x) => x.gisLayerId === 17);//KhasraLine
        if (KhasraLine.length > 0 && KhasraLine[0].code == id)
            showDisBoundariesKhasraLine(KhasraLine);

        var KhasraBoundary = data.filter((x) => x.gisLayerId === 18);//KhasraBoundary
        if (KhasraBoundary.length > 0 && KhasraBoundary[0].code == id)
            showDisBoundariesKhasraBoundary(KhasraBoundary);

        var Road = data.filter((x) => x.gisLayerId === 19);//Road
        if (Road.length > 0 && Road[0].code == id)
            showDisBoundariesRoad(Road);

        var Dashed = data.filter((x) => x.gisLayerId === 20);//Dashed
        if (Dashed.length > 0 && Dashed[0].code == id)
            showDisBoundariesDashed(Dashed);

        var Inner = data.filter((x) => x.gisLayerId === 21);//Inner
        if (Inner.length > 0 && Inner[0].code == id)
            showDisBoundariesInner(Inner);

        var VillageBoundary = data.filter((x) => x.VillageBoundary === 22);//VillageBoundary
        if (VillageBoundary.length > 0 && VillageBoundary[0].code == id)
            showDisBoundariesVillageBoundary(VillageBoundary);

        var Cleantext = data.filter((x) => x.gisLayerId === 23);//Cleantext
        if (Cleantext.length > 0 && Cleantext[0].code == id)
            showDisBoundariesCleantext(Cleantext);

        var Gosha = data.filter((x) => x.gisLayerId === 24);//Gosha
        if (Gosha.length > 0 && Gosha[0].code == id)
            showDisBoundariesGosha(Gosha);

        var Text = data.filter((x) => x.gisLayerId === 25);//Text
        if (Text.length > 0 && Text[0].code == id)
            showDisBoundariesText(Text);

        var DimentionText = data.filter((x) => x.gisLayerId === 26);//DimentionText
        if (DimentionText.length > 0 && DimentionText[0].code == id)
            showDisBoundariesDimentionText(DimentionText);

        var CloseText = data.filter((x) => x.gisLayerId === 27);//CloseText
        if (CloseText.length > 0 && CloseText[0].code == id)
            showDisBoundariesCloseText(CloseText);

        var VillageText = data.filter((x) => x.gisLayerId === 28);//VillageText
        if (VillageText.length > 0 && VillageText[0].code == id)
            showDisBoundariesVillageText(VillageText);

        var KhasraNo = data.filter((x) => x.gisLayerId === 29);//KhasraNo
        if (KhasraNo.length > 0 && KhasraNo[0].code == id)
            showDisBoundariesKhasraNo(KhasraNo);

        var RectWithKhasraNo = data.filter((x) => x.gisLayerId === 30);//RectWithKhasraNo
        if (RectWithKhasraNo.length > 0 && RectWithKhasraNo[0].code == id)
            showDisBoundariesRectWithKhasraNo(RectWithKhasraNo);

        var Well = data.filter((x) => x.gisLayerId === 31);//Well
        if (Well.length > 0 && Well[0].code == id)
            showDisBoundariesWell(Well);

    }
    else {
        var Abadi = data.filter((x) => x.gisLayerId === 1);    //abadi
        if (Abadi.length > 0 && Abadi[0].code == id) {
            for (h = 0; h < ABADI_LAYER.length; h++) {
                ABADI_LAYER[h].setMap(null);
            }
        }

        var Burji = data.filter((x) => x.gisLayerId === 2);//burji
        if (Burji.length > 0 && Burji[0].code == id) {
            for (h = 0; h < BURJI_LAYER.length; h++) {
                BURJI_LAYER[h].setMap(null);
            }
        }

        var Clean = data.filter((x) => x.gisLayerId === 3);//clean
        if (Clean.length > 0 && Clean[0].code == id) {
            for (h = 0; h < CLEAN_LAYER.length; h++) {
                CLEAN_LAYER[h].setMap(null);
            }
        }

        var Dim = data.filter((x) => x.gisLayerId === 4);//Dimension
        if (Dim.length > 0 && Dim[0].code == id) {
            for (h = 0; h < DIM_LAYER.length; h++) {
                DIM_LAYER[h].setMap(null);
            }
        }

        var Encroachment = data.filter((x) => x.gisLayerId === 5);//Encroachment
        if (Encroachment.length > 0 && Encroachment[0].code == id) {
            for (h = 0; h < ENCROACHMENT_LAYER.length; h++) {
                ENCROACHMENT_LAYER[h].setMap(null);
            }
        }

        var Grid = data.filter((x) => x.gisLayerId === 6);//Grid
        if (Grid.length > 0 && Grid[0].code == id) {
            for (h = 0; h < GRID_LAYER.length; h++) {
                GRID_LAYER[h].setMap(null);
            }
        }

        var Nala = data.filter((x) => x.gisLayerId === 7);//Nala
        if (Nala.length > 0 && Nala[0].code == id) {
            for (h = 0; h < NALA_LAYER.length; h++) {
                NALA_LAYER[h].setMap(null);
            }
        }

        var TriJunction = data.filter((x) => x.gisLayerId === 8);//TriJunction
        if (TriJunction.length > 0 && TriJunction[0].code == id) {
            for (h = 0; h < TRIJUNCTION_LAYER.length; h++) {
                TRIJUNCTION_LAYER[h].setMap(null);
            }
        }

        var Zero = data.filter((x) => x.gisLayerId === 9);//Zero
        if (Zero.length > 0 && Zero[0].code == id) {
            for (h = 0; h < ZERO_LAYER.length; h++) {
                ZERO_LAYER[h].setMap(null);
            }
        }

        var Nali = data.filter((x) => x.gisLayerId === 10);//Nali
        if (Nali.length > 0 && Nali[0].code == id) {
            for (h = 0; h < NALI_LAYER.length; h++) {
                NALI_LAYER[h].setMap(null);
            }
        }

        var RailwayLine = data.filter((x) => x.gisLayerId === 11);//RailwayLine
        if (RailwayLine.length > 0 && RailwayLine[0].code == id) {
            for (h = 0; h < RAILWAYLINE_LAYER.length; h++) {
                RAILWAYLINE_LAYER[h].setMap(null);
            }
        }

        var FieldBoun = data.filter((x) => x.gisLayerId === 12);//FieldBoun
        if (FieldBoun.length > 0 && FieldBoun[0].code == id) {
            for (h = 0; h < FIELDBOUN_LAYER.length; h++) {
                FIELDBOUN_LAYER[h].setMap(null);
            }
        }

        var Killa = data.filter((x) => x.gisLayerId === 13);//Killa
        if (Killa.length > 0 && Killa[0].code == id) {
            for (h = 0; h < KILLA_LAYER.length; h++) {
                KILLA_LAYER[h].setMap(null);
            }
        }

        var Close = data.filter((x) => x.gisLayerId === 14);//Close
        if (Close.length > 0 && Close[0].code == id) {
            for (h = 0; h < CLOSE_LAYER.length; h++) {
                CLOSE_LAYER[h].setMap(null);
            }
        }

        var Saheda = data.filter((x) => x.gisLayerId === 15);//Saheda
        if (Saheda.length > 0 && Saheda[0].code == id) {
            for (h = 0; h < SAHEDA_LAYER.length; h++) {
                SAHEDA_LAYER[h].setMap(null);
            }
        }

        var KachaPakaLine = data.filter((x) => x.gisLayerId === 16);//KachaPakaLine
        if (KachaPakaLine.length > 0 && KachaPakaLine[0].code == id) {
            for (h = 0; h < KACHAPAKALINE_LAYER.length; h++) {
                KACHAPAKALINE_LAYER[h].setMap(null);
            }
        }

        var KhasraLine = data.filter((x) => x.gisLayerId === 17);//KhasraLine
        if (KhasraLine.length > 0 && KhasraLine[0].code == id) {
            for (h = 0; h < KHASRALINE_LAYER.length; h++) {
                KHASRALINE_LAYER[h].setMap(null);
            }
        }

        var KhasraBoundary = data.filter((x) => x.gisLayerId === 18);//KhasraBoundary
        if (KhasraBoundary.length > 0 && KhasraBoundary[0].code == id) {
            for (h = 0; h < KHASRABOUNDARY_LAYER.length; h++) {
                KHASRABOUNDARY_LAYER[h].setMap(null);
            }
        }

        var Road = data.filter((x) => x.gisLayerId === 19);//Road
        if (Road.length > 0 && Road[0].code == id) {
            for (h = 0; h < ROAD_LAYER.length; h++) {
                ROAD_LAYER[h].setMap(null);
            }
        }

        var Dashed = data.filter((x) => x.gisLayerId === 20);//Dashed
        if (Dashed.length > 0 && Dashed[0].code == id) {
            for (h = 0; h < DASHED_LAYER.length; h++) {
                DASHED_LAYER[h].setMap(null);
            }
        }

        var Inner = data.filter((x) => x.gisLayerId === 21);//Inner
        if (Inner.length > 0 && Inner[0].code == id) {
            for (h = 0; h < INNER_LAYER.length; h++) {
                INNER_LAYER[h].setMap(null);
            }
        }

        var VillageBoundary = data.filter((x) => x.VillageBoundary === 22);//VillageBoundary
        if (VillageBoundary.length > 0 && VillageBoundary[0].code == id) {
            for (h = 0; h < VILLAGEBOUNDARY_LAYER.length; h++) {
                VILLAGEBOUNDARY_LAYER[h].setMap(null);
            }
        }

        var Cleantext = data.filter((x) => x.gisLayerId === 23);//Cleantext
        if (Cleantext.length > 0 && Cleantext[0].code == id) {
            for (h = 0; h < CLEANTEXT_LAYER.length; h++) {
                CLEANTEXT_LAYER[h].setMap(null);
            }
        }

        var Gosha = data.filter((x) => x.gisLayerId === 24);//Gosha
        if (Gosha.length > 0 && Gosha[0].code == id) {
            for (h = 0; h < GOSHA_LAYER.length; h++) {
                GOSHA_LAYER[h].setMap(null);
            }
        }

        var Text = data.filter((x) => x.gisLayerId === 25);//Text
        if (Text.length > 0 && Text[0].code == id) {
            for (h = 0; h < TEXT_LAYER.length; h++) {
                TEXT_LAYER[h].setMap(null);
            }
        }

        var DimentionText = data.filter((x) => x.gisLayerId === 26);//DimentionText
        if (DimentionText.length > 0 && DimentionText[0].code == id) {
            for (h = 0; h < DIMENTIONTEXT_LAYER.length; h++) {
                DIMENTIONTEXT_LAYER[h].setMap(null);
            }
        }

        var CloseText = data.filter((x) => x.gisLayerId === 27);//CloseText
        if (CloseText.length > 0 && CloseText[0].code == id) {
            for (h = 0; h < CLOSETEXT_LAYER.length; h++) {
                CLOSETEXT_LAYER[h].setMap(null);
            }
        }

        var VillageText = data.filter((x) => x.gisLayerId === 28);//VillageText
        if (VillageText.length > 0 && VillageText[0].code == id) {
            for (h = 0; h < VILLAGETEXT_LAYER.length; h++) {
                VILLAGETEXT_LAYER[h].setMap(null);
            }
        }

        var KhasraNo = data.filter((x) => x.gisLayerId === 29);//KhasraNo
        if (KhasraNo.length > 0 && KhasraNo[0].code == id) {
            for (h = 0; h < KHASRANO_LAYER.length; h++) {
                KHASRANO_LAYER[h].setMap(null);
            }
        }

        var RectWithKhasraNo = data.filter((x) => x.gisLayerId === 30);//RectWithKhasraNo
        if (RectWithKhasraNo.length > 0 && RectWithKhasraNo[0].code == id) {
            for (h = 0; h < RECTWITHKHASRANO_LAYER.length; h++) {
                RECTWITHKHASRANO_LAYER[h].setMap(null);
            }
        }

        var Well = data.filter((x) => x.gisLayerId === 31);//Well
        if (Well.length > 0 && Well[0].code == id) {
            for (h = 0; h < WELL_LAYER.length; h++) {
                WELL_LAYER[h].setMap(null);
            }
        }

    }
});


function GetVillageList(id) {
    HttpGet(`/GIS/GetVillageList/?zoneId=${id}`, 'json', function (response) {
        $("#VillageId").val('0').trigger('change');
        var html = '<option value="0">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#VillageId").html(html);
        $("#KhasraId").val('0').trigger('change');
    });

    HttpGet(`/GIS/GetZoneDetails?ZoneId=${parseInt(id)}`, 'json', function (response) {
        showDisBoundaries(response[0].polygon, response[0].xcoordinate, response[0].ycoordinate);
    });
};

function GetKhasraList(id) {
    HttpGet(`/GIS/GetKhasraList/?villageId=${id}`, 'json', function (response) {
        var html = '<option value="0">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].label + '</option>';
        }
        $("#KhasraId").val('0').trigger('change');
        $("#KhasraId").html(html);
    });

    HttpGet("/GIS/GetVillageDetails?VillageId=" + parseInt(id), 'json', function (response) {
        if (response.length > 0)
            showDisBoundariesVillage(response[0].polygon, response[0].xcoordinate, response[0].ycoordinate, response[0].id);
    });
};

function ShowKhasraNo(id) {
    var khasrano = id;
    if (khasrano != '') {

        HttpGet("/GIS/GetKhasraNoPolygon?gisDataId=" + parseInt(khasrano), 'json', function (response) {
            var khasrano = $.map(response, function (el) { return el; })
            for (h = 0; h < KHASRANO_LAYER.length; h++) {
                KHASRANO_LAYER[h].setMap(null);
            }
            for (aj = 0; aj < khasrano.length; aj++) {
                var lp = new google.maps.LatLng(parseFloat(khasrano[aj].ycoordinate), parseFloat(khasrano[aj].xcoordinate));
                var measle = new google.maps.Marker({
                    position: lp,
                    map: map,
                    icon: {
                        url: "https://maps.gstatic.com/intl/en_us/mapfiles/markers2/measle.png",
                        size: new google.maps.Size(7, 7),
                        anchor: new google.maps.Point(4, 4)
                    }
                });
                var marker = new google.maps.Marker({
                    position: lp,
                    map: map,
                    icon: {
                        url: "http://maps.google.com/mapfiles/ms/icons/red-dot.png",
                        labelOrigin: new google.maps.Point(6, -4),
                        //size: new google.maps.Size(32, 32),
                        anchor: new google.maps.Point(16, 32)
                    },
                    label: {
                        text: khasrano[aj].label,
                        color: "#C70E20",
                        fontWeight: "bold"
                    }
                });
                marker.khasrano = khasrano[aj].label;
                marker.villageid = VILLAGEID_UNIVERSAL[0];
                google.maps.event.addListener(marker, 'click', function () {
                    getInfo(this.villageid, this.khasrano);
                });
                KHASRANO_LAYER.push(marker);
                KHASRANO_LAYER.push(measle);
                map.setZoom(17);
                Polys.push(marker);
                map.panTo(new google.maps.LatLng(parseFloat(khasrano[aj].ycoordinate), parseFloat(khasrano[aj].xcoordinate)));
            }
        });
    }
};
$(document).on('change', '#KhasraId', function (e) {
    e.preventDefault();
    var khasrano = $('#KhasraId option:selected').val();
    if (khasrano != '') {

        HttpGet("/GIS/GetKhasraNoPolygon?gisDataId=" + parseInt(khasrano), 'json', function (response) {
            var khasrano = $.map(response, function (el) { return el; })
            for (h = 0; h < KHASRANO_LAYER.length; h++) {
                KHASRANO_LAYER[h].setMap(null);
            }
            for (aj = 0; aj < khasrano.length; aj++) {
                var lp = new google.maps.LatLng(parseFloat(khasrano[aj].ycoordinate), parseFloat(khasrano[aj].xcoordinate));
                var measle = new google.maps.Marker({
                    position: lp,
                    map: map,
                    icon: {
                        url: "https://maps.gstatic.com/intl/en_us/mapfiles/markers2/measle.png",
                        size: new google.maps.Size(7, 7),
                        anchor: new google.maps.Point(4, 4)
                    }
                });
                var marker = new google.maps.Marker({
                    position: lp,
                    map: map,
                    icon: {
                        url: "http://maps.google.com/mapfiles/ms/icons/red-dot.png",
                        labelOrigin: new google.maps.Point(6, -4),
                        //size: new google.maps.Size(32, 32),
                        anchor: new google.maps.Point(16, 32)
                    },
                    label: {
                        text: khasrano[aj].label,
                        color: "#C70E20",
                        fontWeight: "bold"
                    }
                });
                marker.khasrano = khasrano[aj].label;
                marker.villageid = VILLAGEID_UNIVERSAL[0];
                google.maps.event.addListener(marker, 'click', function () {
                    getInfo(this.villageid, this.khasrano);
                });
                KHASRANO_LAYER.push(marker);
                KHASRANO_LAYER.push(measle);
                map.setZoom(17);
                Polys.push(marker);
                map.panTo(new google.maps.LatLng(parseFloat(khasrano[aj].ycoordinate), parseFloat(khasrano[aj].xcoordinate)));
            }
        });
    }
});

function SetMapNull() {
    for (h = 0; h < KHASRANO_LAYER.length; h++) {
        KHASRANO_LAYER[h].setMap(null);
    }
}

function callSelect2() {
    $("select").select2();
}
$('#Query1').on('click', function (e) {  

    e.preventDefault();
    callSelect2();
    
});