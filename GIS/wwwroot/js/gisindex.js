


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
var CALVERT_LAYER = [];
var MICK_LAYER = [];
var LINE_LAYER = [];
var DDA_VACANT_LAYER = [];
var highlighted_khasra_LAYER = [];


$(document).ready(function () {

    $(document).ajaxStart(function () {
        var styleValue = "block";
        $("#loader-wrapper").css("display", styleValue);
    });

    $(document).ajaxComplete(function () {
        var styleValue = "none";
        $("#loader-wrapper").css("display", styleValue);
    });

    HttpGet(`/GIS/GetZoneList`, 'json', function (response) {
        var html = '';
        for (var i = 0; i < response.length; i++) {
            var check = 0;
            html = html + '<div class="accordion-item"><h5 class="accordion-header" id="heading' + response[i].id + '"><button id="Z' + response[i].id + '" class="accordion-button collapsed py-2 bg-primary text-white" type="button" onclick="showZone(this.id)" data-bs-toggle="collapse" data-bs-target="#collapse' + response[i].id + '" aria-expanded="true" aria-controls="collapse' + response[i].id + '"><i class="ri-arrow-right-s-fill text-white"></i>' + response[i].name + '</button></h5><div id="collapse' + response[i].id + '" class="accordion-collapse collapse" aria-labelledby="heading' + response[i].id + '" ><div class="accordion-body"><div class="list-group">';
            if (response[i].village.length > 0) {
                for (var j = 0; j < response[i].village.length; j++) {
                    if (response[i].village[j].isActive == 1) {
                        // html = html + '<a href="javascript:void(0);" id="V' + response[i].village[j].id + '" class="list-group-item list-group-item-action" onclick="showVillage(this.id)"><i class="ri-eye-line"></i> ' + response[i].village[j].name + '</a>';
                        html = html + '<div class="form-check" style="border-bottom: 1px solid rgba(0, 0, 0, .125);"><input class="form-check-input"  type="checkbox" id="V' + response[i].village[j].id + '" onchange="showVillage(this.id)"> <label class="form-check-label" for="V' + response[i].village[j].id + '">' + response[i].village[j].name + '</label> <span id="z' + response[i].village[j].id + '" class="ri-zoom-in-line" style="display:none" onclick="ZoomtoVillage(' + response[i].village[j].ycoordinate + ',' + response[i].village[j].xcoordinate + ',\'' + response[i].village[j].name + '\');" title="Click here Zoom to ' + response[i].village[j].name + '"></span></div> ';
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
        //mapTypeId: 'OSM',
        scaleControl: true,
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
                'visibility': 'off'
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

    google.maps.event.addListener(map, 'mousemove', function (event) {
        displayCoordinates(event.latLng);
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
        var lns = createDashedLine(getLatLongArr(arr[i].polygon));
        ln.setOptions({ visibleZoom: 7, fillColor: arr[i].colorcode, hideZoom: 9, visible: true, map: map, strokeWeight: 0, strokeColor: '#47C4C8', fillOpacity: 0.3, clickable: !1 });
        var lp = new google.maps.LatLng(parseFloat(arr[i].ycoordinate), parseFloat(arr[i].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 0, hideZoom: 18, visible: true, map: map, cssName: 'nlLabelState', position: lp, text: arr[i].label });
        STATE_LAYER.push(lns);
        STATE_LAYER.push(ln);
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
    google.maps.event.addListener(map, 'mousemove', function (event) {
        displayCoordinates(event.latLng);
    });
    //google.maps.event.addListener(poly, 'mousemove', function (event) {
    //    displayCoordinates(event.latLng);
    //});
}
/*Zone Boundary End*/

/*Village Boundary Start*/
function showVillage(maxima) {
    if ($('#' + maxima).is(':checked')) {
        var villageid = maxima.replace('V', '');
        HttpGet("/GIS/GetVillageDetails?VillageId=" + parseInt(villageid), 'json', function (response, villageid) {
            //Show Village Name
            $('#spanVillageName').empty().append(response[0].name.toUpperCase());
            $('#spanVillage').empty().append('Village : ' + response[0].name.toUpperCase())
            $('#aVillageName').show();
            //Show Zoom to Icon
            $('#z' + response[0].id).show();
            //
            showDisBoundariesVillage(response[0].polygon, response[0].xcoordinate, response[0].ycoordinate, response[0].id);
        });
    }
    else {
        var villageid = maxima.replace('V', '');
        //hide zoom to icon
        $('#z' + villageid).hide();
        //
        //Remove all layers of perticular village
        RemoveAllVillageLayer(villageid);
        VILLAGEID_UNIVERSAL.splice($.inArray(villageid, VILLAGEID_UNIVERSAL), 1);

    }
}
function ZoomtoVillage(yaixis, xaixis, villagename) {
    $('#spanVillageName').empty().append(villagename.toUpperCase());
    $('#spanVillage').empty().append('Village : ' + villagename.toUpperCase())
    $('#aVillageName').show();
    map.setZoom(15);
    map.panTo(new google.maps.LatLng(yaixis, xaixis));
}
function showDisBoundariesVillage(ploygn, xaixis, yaixis, villageid) {

    //for (var x = 0; x < zoomvillage.length; x++) {
    //    zoomvillage[x].setMap(null);
    //}
    var sl = createPolygon(getLatLongArr(ploygn));
    sl.setOptions({ strokeWeight: 5, strokeColor: '#FF5733', fillOpacity: 0, clickable: !1 });
    zoomvillage.push({ "villageid": villageid, "layer": sl });
    map.setZoom(15);
    map.panTo(new google.maps.LatLng(yaixis, xaixis));

    showvillagelayers(villageid);
    google.maps.event.addListener(map, 'mousemove', function (event) {
        displayCoordinates(event.latLng);
    });
}
function showvillagelayers(villageid) {

    HttpGet("/GIS/GetGisDataLayersDetails?VillageId=" + parseInt(villageid), 'json', function (response, villageid) {
        //sessionStorage.clear();
        // localStorage.setItem('alldata', JSON.stringify(response)); //data added to local storage
        // sessionStorage.setItem('alldata', JSON.stringify(response)); //data added to session storage
        DisplayLoader(true);
        alljsondata = [];
        alljsondata = JSON.stringify(response);
        var Abadi = response.filter((x) => x.gisLayerId === 1);    //abadi
        if (Abadi.length > 0 && Abadi[0].checkedStatus == 1)
            showDisBoundariesAbadi(Abadi, villageid);

        var Burji = response.filter((x) => x.gisLayerId === 2);//burji
        if (Burji.length > 0 && Burji[0].checkedStatus == 1)
            showDisBoundariesBurji(Burji, villageid);

        var Clean = response.filter((x) => x.gisLayerId === 3);//clean
        if (Clean.length > 0 && Clean[0].checkedStatus == 1)
            showDisBoundariesClean(Clean, villageid);

        var Dim = response.filter((x) => x.gisLayerId === 4);//Dimension
        if (Dim.length > 0 && Dim[0].checkedStatus == 1)
            showDisBoundariesDim(Dim, villageid);

        var Encroachment = response.filter((x) => x.gisLayerId === 5);//Encroachment
        if (Encroachment.length > 0 && Encroachment[0].checkedStatus == 1)
            showDisBoundariesEncroachment(Encroachment, villageid);

        var Grid = response.filter((x) => x.gisLayerId === 6);//Grid
        if (Grid.length > 0 && Grid[0].checkedStatus == 1)
            showDisBoundariesGrid(Grid, villageid);

        var Nala = response.filter((x) => x.gisLayerId === 7);//Nala
        if (Nala.length > 0 && Nala[0].checkedStatus == 1)
            showDisBoundariesNala(Nala, villageid);

        var TriJunction = response.filter((x) => x.gisLayerId === 8);//TriJunction
        if (TriJunction.length > 0 && TriJunction[0].checkedStatus == 1)
            showDisBoundariesTriJunction(TriJunction, villageid);

        var Zero = response.filter((x) => x.gisLayerId === 9);//Zero
        if (Zero.length > 0 && Zero[0].checkedStatus == 1)
            showDisBoundariesZero(Zero, villageid);

        var Nali = response.filter((x) => x.gisLayerId === 10);//Nali
        if (Nali.length > 0 && Nali[0].checkedStatus == 1)
            showDisBoundariesNali(Nali, villageid);

        var RailwayLine = response.filter((x) => x.gisLayerId === 11);//RailwayLine
        if (RailwayLine.length > 0 && RailwayLine[0].checkedStatus == 1)
            showDisBoundariesRailwayLine(RailwayLine, villageid);

        var FieldBoun = response.filter((x) => x.gisLayerId === 12);//FieldBoun
        if (FieldBoun.length > 0 && FieldBoun[0].checkedStatus == 1)
            showDisBoundariesFieldBoun(FieldBoun, villageid);

        var Killa = response.filter((x) => x.gisLayerId === 13);//Killa
        if (Killa.length > 0 && Killa[0].checkedStatus == 1)
            showDisBoundariesKilla(Killa, villageid);

        var Close = response.filter((x) => x.gisLayerId === 14);//Close
        if (Close.length > 0 && Close[0].checkedStatus == 1)
            showDisBoundariesClose(Close, villageid);

        var Saheda = response.filter((x) => x.gisLayerId === 15);//Saheda
        if (Saheda.length > 0 && Saheda[0].checkedStatus == 1)
            showDisBoundariesSaheda(Saheda, villageid);

        var KachaPakaLine = response.filter((x) => x.gisLayerId === 16);//KachaPakaLine
        if (KachaPakaLine.length > 0 && KachaPakaLine[0].checkedStatus == 1)
            showDisBoundariesKachaPakaLine(KachaPakaLine, villageid);

        var KhasraLine = response.filter((x) => x.gisLayerId === 17);//KhasraLine
        if (KhasraLine.length > 0 && KhasraLine[0].checkedStatus == 1)
            showDisBoundariesKhasraLine(KhasraLine, villageid);

        var Well = response.filter((x) => x.gisLayerId === 31);//Well
        if (Well.length > 0 && Well[0].checkedStatus == 1)
            showDisBoundariesWell(Well, villageid);

        var KhasraBoundary = response.filter((x) => x.gisLayerId === 18);//KhasraBoundary
        if (KhasraBoundary.length > 0 && KhasraBoundary[0].checkedStatus == 1)
            showDisBoundariesKhasraBoundary(KhasraBoundary, villageid);

        var Road = response.filter((x) => x.gisLayerId === 19);//Road
        if (Road.length > 0 && Road[0].checkedStatus == 1)
            showDisBoundariesRoad(Road, villageid);

        var Dashed = response.filter((x) => x.gisLayerId === 20);//Dashed
        if (Dashed.length > 0 && Dashed[0].checkedStatus == 1)
            showDisBoundariesDashed(Dashed, villageid);

        var Inner = response.filter((x) => x.gisLayerId === 21);//Inner
        if (Inner.length > 0 && Inner[0].checkedStatus == 1)
            showDisBoundariesInner(Inner, villageid);

        var VillageBoundary = response.filter((x) => x.VillageBoundary === 22);//VillageBoundary
        if (VillageBoundary.length > 0 && VillageBoundary[0].checkedStatus == 1)
            showDisBoundariesVillageBoundary(VillageBoundary);

        var Cleantext = response.filter((x) => x.gisLayerId === 23);//Cleantext
        if (Cleantext.length > 0 && Cleantext[0].checkedStatus == 1)
            showDisBoundariesCleantext(Cleantext, villageid);

        var Gosha = response.filter((x) => x.gisLayerId === 24);//Gosha
        if (Gosha.length > 0 && Gosha[0].checkedStatus == 1)
            showDisBoundariesGosha(Gosha, villageid);

        var Text = response.filter((x) => x.gisLayerId === 25);//Text
        if (Text.length > 0 && Text[0].checkedStatus == 1)
            showDisBoundariesText(Text, villageid);

        var DimentionText = response.filter((x) => x.gisLayerId === 26);//DimentionText
        if (DimentionText.length > 0 && DimentionText[0].checkedStatus == 1)
            showDisBoundariesDimentionText(DimentionText, villageid);

        var CloseText = response.filter((x) => x.gisLayerId === 27);//CloseText
        if (CloseText.length > 0 && CloseText[0].checkedStatus == 1)
            showDisBoundariesCloseText(CloseText, villageid);

        var VillageText = response.filter((x) => x.gisLayerId === 28);//VillageText
        if (VillageText.length > 0 && VillageText[0].checkedStatus == 1)
            showDisBoundariesVillageText(VillageText, villageid);

        var KhasraNo = response.filter((x) => x.gisLayerId === 29);//KhasraNo
        if (KhasraNo.length > 0 && KhasraNo[0].checkedStatus == 1)
            showDisBoundariesKhasraNo(KhasraNo, villageid);

        var RectWithKhasraNo = response.filter((x) => x.gisLayerId === 30);//RectWithKhasraNo
        if (RectWithKhasraNo.length > 0 && RectWithKhasraNo[0].checkedStatus == 1)
            showDisBoundariesRectWithKhasraNo(RectWithKhasraNo, villageid);

        var Calvert = response.filter((x) => x.gisLayerId === 34);//Calvert
        if (Calvert.length > 0 && Calvert[0].checkedStatus == 1)
            showDisBoundariesCalvert(Calvert, villageid);

        var Mick = response.filter((x) => x.gisLayerId === 33);//Mick
        if (Mick.length > 0 && Mick[0].checkedStatus == 1)
            showDisBoundariesMick(Mick, villageid);

        var Line = response.filter((x) => x.gisLayerId === 35);//Line
        if (Line.length > 0 && Line[0].checkedStatus == 1)
            showDisBoundariesLine(Line, villageid);

        var VLand = response.filter((x) => x.gisLayerId === 36);//DDA Vacant Land
        if (VLand.length > 0 && VLand[0].checkedStatus == 1)
            showDDAVacantLandBoundaries(VLand, villageid);
    });
    VILLAGEID_UNIVERSAL = [];
    VILLAGEID_UNIVERSAL.push(villageid);
    DisplayLoader(false);
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
        ABADI_LAYER.push({ "villageid": abadi[i].villageId, "layer": ln });
        //ABADI_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesBurji(response) {
    var burji = $.map(response, function (el) { return el; })
    if (burji[0].checkedStatus == 1) {
        for (e = 0; e < burji.length; e++) {
            var ln = createLine(getLatLongArr(burji[e].polygon));
            ln.setOptions({ strokeWeight: 3, strokeColor: burji[e].fillColor });
            BURJI_LAYER.push({ "villageid": burji[e].villageId, "layer": ln });
            // BURJI_LAYER.push(ln);
            Polys.push(ln);
        }

    }
}
function showDisBoundariesClean(response, villageid) {
    var clean = $.map(response, function (el) { return el; })
    for (f = 0; f < clean.length; f++) {
        var ln = createLine(getLatLongArr(clean[f].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: clean[f].fillColor });
        CLEAN_LAYER.push({ "villageid": clean[f].villageId, "layer": ln });
        //CLEAN_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesDim(response, villageid) {
    var dim = $.map(response, function (el) { return el; })

    $.each(DIM_LAYER, function (index, value) {
        value.layer.setMap(null);
    });
    for (h = 0; h < dim.length; h++) {
        var poly = createPolygon(getLatLongArr(dim[h].polygon));
        poly.setOptions({ strokeWeight: 1, strokeColor: dim[h].fillColor, fillOpacity: 0, clickable: !0 });
        DIM_LAYER.push({ "villageid": dim[h].villageId, "layer": poly });
        //DIM_LAYER.push(poly);
        // map.panTo(new google.maps.LatLng(dim[h].ycoordinate, dim[h].xcoordinate));
    }
}
function showDisBoundariesEncroachment(response, villageid) {
    var encroachment = $.map(response, function (el) { return el; })
    for (j = 0; j < encroachment.length; j++) {
        var ln = createLine(getLatLongArr(encroachment[j].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: encroachment[j].fillColor });
        ENCROACHMENT_LAYER.push({ "villageid": encroachment[j].villageId, "layer": ln });
        //ENCROACHMENT_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesGrid(response, villageid) {
    var grid = $.map(response, function (el) { return el; })
    for (k = 0; k < grid.length; k++) {
        var ln = createLine(getLatLongArr(grid[k].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: grid[k].fillColor });
        GRID_LAYER.push({ "villageid": grid[k].villageId, "layer": ln });
        // GRID_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesNala(response, villageid) {
    var nala = $.map(response, function (el) { return el; })
    for (m = 0; m < nala.length; m++) {
        var ln = createLine(getLatLongArr(nala[m].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: nala[m].fillColor });
        NALA_LAYER.push({ "villageid": nala[m].villageId, "layer": ln });
        // NALA_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesTriJunction(response, villageid) {
    var trijunction = $.map(response, function (el) { return el; })
    for (n = 0; n < trijunction.length; n++) {
        var ln = createLine(getLatLongArr(trijunction[n].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: trijunction[n].fillColor });
        TRIJUNCTION_LAYER.push({ "villageid": trijunction[n].villageId, "layer": ln });
        //TRIJUNCTION_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesZero(response, villageid) {
    var zero = $.map(response, function (el) { return el; })
    for (o = 0; o < zero.length; o++) {
        var ln = createLine(getLatLongArr(zero[o].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: zero[o].fillColor });
        ZERO_LAYER.push({ "villageid": zero[o].villageId, "layer": ln });
        //ZERO_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesNali(response, villageid) {
    var nali = $.map(response, function (el) { return el; })
    for (p = 0; p < nali.length; p++) {
        var ln = createLine(getLatLongArr(nali[p].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: nali[p].fillColor });
        NALI_LAYER.push({ "villageid": nali[p].villageId, "layer": ln });
        // NALI_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesRailwayLine(response, villageid) {
    var railwayline = $.map(response, function (el) { return el; })
    for (q = 0; q < railwayline.length; q++) {
        var ln = createLine(getLatLongArr(railwayline[q].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: railwayline[q].fillColor });
        RAILWAYLINE_LAYER.push({ "villageid": railwayline[q].villageId, "layer": ln });
        //RAILWAYLINE_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesFieldBoun(response, villageid) {
    var fieldboun = $.map(response, function (el) { return el; })
    for (r = 0; r < fieldboun.length; r++) {
        var ln = createLine(getLatLongArr(fieldboun[r].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: fieldboun[r].fillColor });
        FIELDBOUN_LAYER.push({ "villageid": fieldboun[r].villageId, "layer": ln });
        // FIELDBOUN_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesKilla(response, villageid) {
    var killa = $.map(response, function (el) { return el; })
    for (s = 0; s < killa.length; s++) {
        var ln = createLine(getLatLongArr(killa[s].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: killa[s].fillColor });
        KILLA_LAYER.push({ "villageid": killa[s].villageId, "layer": ln });
        //KILLA_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesClose(response, villageid) {
    var close = $.map(response, function (el) { return el; })
    for (t = 0; t < close.length; t++) {
        var ln = createLine(getLatLongArr(close[t].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: close[t].fillColor });
        CLOSE_LAYER.push({ "villageid": close[t].villageId, "layer": ln });
        //CLOSE_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesSaheda(response, villageid) {
    var saheda = $.map(response, function (el) { return el; })
    for (u = 0; u < saheda.length; u++) {
        var ln = createLine(getLatLongArr(saheda[u].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: saheda[u].fillColor });
        SAHEDA_LAYER.push({ "villageid": saheda[u].villageId, "layer": ln });
        // SAHEDA_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesKachaPakaLine(response, villageid) {
    var kachapakaline = $.map(response, function (el) { return el; })
    for (v = 0; v < kachapakaline.length; v++) {
        var ln = createLine(getLatLongArr(kachapakaline[v].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: kachapakaline[v].fillColor });
        KACHAPAKALINE_LAYER.push({ "villageid": kachapakaline[v].villageId, "layer": ln });
        //KACHAPAKALINE_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesKhasraLine(response, villageid) {
    var khasraline = $.map(response, function (el) { return el; })
    for (w = 0; w < khasraline.length; w++) {
        var ln = createLine(getLatLongArr(khasraline[w].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: khasraline[w].fillColor });
        KHASRALINE_LAYER.push({ "villageid": khasraline[w].villageId, "layer": ln });
        //KHASRALINE_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesKhasraBoundary(response, villageid) {
    var khasraboundary = $.map(response, function (el) { return el; })
    for (x = 0; x < khasraboundary.length; x++) {
        var ln = createLine(getLatLongArr(khasraboundary[x].polygon));
        ln.setOptions({ strokeWeight: 1, strokeColor: khasraboundary[x].fillColor });
        KHASRABOUNDARY_LAYER.push({ "villageid": khasraboundary[x].villageId, "layer": ln });
        // KHASRABOUNDARY_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesCalvert(response, villageid) {
    var Calvert = $.map(response, function (el) { return el; })
    for (x = 0; x < Calvert.length; x++) {
        var ln = createLine(getLatLongArr(Calvert[x].polygon));
        ln.setOptions({ strokeWeight: 1, strokeColor: Calvert[x].fillColor });
        CALVERT_LAYER.push({ "villageid": Calvert[x].villageId, "layer": ln });
        //CALVERT_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesMick(response, villageid) {
    var mick = $.map(response, function (el) { return el; })
    for (x = 0; x < mick.length; x++) {
        var ln = createLine(getLatLongArr(mick[x].polygon));
        ln.setOptions({ strokeWeight: 1, strokeColor: mick[x].fillColor });
        MICK_LAYER.push({ "villageid": mick[x].villageId, "layer": ln });
        //MICK_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesLine(response, villageid) {
    var line = $.map(response, function (el) { return el; })
    for (x = 0; x < line.length; x++) {
        var ln = createLine(getLatLongArr(line[x].polygon));
        ln.setOptions({ strokeWeight: 1, strokeColor: line[x].fillColor });
        LINE_LAYER.push({ "villageid": line[x].villageId, "layer": ln });
        //LINE_LAYER.push(ln);
        Polys.push(ln);
    }
}
//Added by sachin for DDA Vacant Land Data 18-07-2022
function showDDAVacantLandBoundaries(response, villageid) {
    var poly = $.map(response, function (el) { return el; });

    for (i = 0; i < poly.length; i++) {
        var ln = createPolygon(getLatLongArr(poly[i].polygon));
        // var lns = createDashedLine(getLatLongArr(poly[i].polygon));
        ln.setOptions({ visibleZoom: 10, fillColor: poly[i].fillColor, hideZoom: 9, visible: true, map: map, strokeWeight: 1, strokeColor: poly[i].strokeColor, fillOpacity: 0.6, clickable: !1 });
        var lp = new google.maps.LatLng(parseFloat(poly[i].labelYcoordinate), parseFloat(poly[i].labelXcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 13, hideZoom: 21, visible: true, map: map, cssName: 'nlLabelState', position: lp, text: poly[i].label });
        DDA_VACANT_LAYER.push({ "villageid": poly[i].villageId, "layer": ln });
        DDA_VACANT_LAYER.push({ "villageid": poly[i].villageId, "layer": _label });
        Polys.push(ln);
    }

}

function showDisBoundariesRoad(response, villageid) {
    var road = $.map(response, function (el) { return el; })
    for (y = 0; y < road.length; y++) {
        var ln = createLine(getLatLongArr(road[y].polygon));
        ln.setOptions({ strokeWeight: 1, strokeColor: road[y].fillColor });
        ROAD_LAYER.push({ "villageid": road[y].villageId, "layer": ln });
        //ROAD_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesDashed(response, villageid) {
    var dashed = $.map(response, function (el) { return el; })
    for (z = 0; z < dashed.length; z++) {
        var ln = createLine(getLatLongArr(dashed[z].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: dashed[z].fillColor });
        DASHED_LAYER.push({ "villageid": dashed[z].villageId, "layer": ln });
        // DASHED_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesInner(response) {
    var inner = $.map(response, function (el) { return el; })
    for (ab = 0; ab < inner.length; ab++) {
        var ln = createLine(getLatLongArr(inner[ab].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: inner[ab].fillColor });
        INNER_LAYER.push({ "villageid": inner[ab].villageId, "layer": ln });
        //INNER_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesVillageBoundary(response, villageid) {
    var villageboundary = $.map(response, function (el) { return el; })
    for (ac = 0; ac < villageboundary.length; ac++) {
        var ln = createLine(getLatLongArr(villageboundary[ac].polygon));
        ln.setOptions({ strokeWeight: 3, strokeColor: villageboundary[ac].fillColor });
        VILLAGEBOUNDARY_LAYER.push({ "villageid": villageboundary[ac].villageId, "layer": ln });
        //VILLAGEBOUNDARY_LAYER.push(ln);
        Polys.push(ln);
    }
}
function showDisBoundariesCleantext(response, villageid) {
    var cleantext = $.map(response, function (el) { return el; })
    for (ad = 0; ad < cleantext.length; ad++) {
        //var ln = createPoint(getLatLongArr(cleantext[g].polygon)[0]);
        //ln.setOptions({ strokeWeight: 3, strokeColor: '#AF4167 ' });
        var lp = new google.maps.LatLng(parseFloat(cleantext[ad].ycoordinate), parseFloat(cleantext[ad].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 60, visible: true, map: map, cssName: 'nlLabelCleantext', position: lp, text: cleantext[ad].label });
        CLEANTEXT_LAYER.push({ "villageid": cleantext[ad].villageId, "layer": _label });
        //CLEANTEXT_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesGosha(response, villageid) {
    var gosha = $.map(response, function (el) { return el; })
    for (ae = 0; ae < gosha.length; ae++) {
        var lp = new google.maps.LatLng(parseFloat(gosha[ae].ycoordinate), parseFloat(gosha[ae].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 26, visible: true, map: map, cssName: 'nlLabelGosha', position: lp, text: gosha[ae].label });
        GOSHA_LAYER.push({ "villageid": gosha[ae].villageId, "layer": _label });
        //GOSHA_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesText(response, villageid) {
    var textla = $.map(response, function (el) { return el; })
    for (af = 0; af < textla.length; af++) {
        var lp = new google.maps.LatLng(parseFloat(textla[af].ycoordinate), parseFloat(textla[af].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 18, visible: true, map: map, cssName: 'nlLabelKhasraText', position: lp, text: textla[af].label });
        TEXT_LAYER.push({ "villageid": textla[af].villageId, "layer": _label });
        //TEXT_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesDimentionText(response, villageid) {
    var dimentiontext = $.map(response, function (el) { return el; })
    for (ag = 0; ag < dimentiontext.length; ag++) {
        var lp = new google.maps.LatLng(parseFloat(dimentiontext[ag].ycoordinate), parseFloat(dimentiontext[ag].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 60, visible: true, map: map, cssName: 'nlLabelDimentionText', position: lp, text: dimentiontext[ag].label });
        DIMENTIONTEXT_LAYER.push({ "villageid": dimentiontext[ag].villageId, "layer": _label });
        // DIMENTIONTEXT_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesCloseText(response, villageid) {
    var closetext = $.map(response, function (el) { return el; })
    for (ah = 0; ah < closetext.length; ah++) {
        var lp = new google.maps.LatLng(parseFloat(closetext[ah].ycoordinate), parseFloat(closetext[ah].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 26, visible: true, map: map, cssName: 'nlLabelCloseText', position: lp, text: closetext[ah].label });
        CLOSETEXT_LAYER.push({ "villageid": closetext[ah].villageId, "layer": _label });
        //CLOSETEXT_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesVillageText(response, villageid) {
    var villagetext = $.map(response, function (el) { return el; })
    for (ai = 0; ai < villagetext.length; ai++) {
        var lp = new google.maps.LatLng(parseFloat(villagetext[ai].ycoordinate), parseFloat(villagetext[ai].xcoordinate));
        var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 18, visible: true, map: map, cssName: 'nlLabelVillageText', position: lp, text: villagetext[ai].label });
        VILLAGETEXT_LAYER.push({ "villageid": villagetext[ai].villageId, "layer": _label });
        // VILLAGETEXT_LAYER.push(_label);

        Polys.push(_label);
    }
}
function showDisBoundariesKhasraNo(response, villageid) {
    var khasrano = $.map(response, function (el) { return el; })
    for (aj = 0; aj < khasrano.length; aj++) {
        var lp = new google.maps.LatLng(parseFloat(khasrano[aj].ycoordinate), parseFloat(khasrano[aj].xcoordinate));

        var mapLabel = new MapLabel({
            text: khasrano[aj].label,
            position: lp,//new google.maps.LatLng(parseFloat(khasrano[aj].ycoordinate), parseFloat(khasrano[aj].xcoordinate)),
            map: map,
            fontSize: 11,
            align: 'center',
            fontColor: khasrano[aj].fillColor,
            minZoom: 17,
            maxZoom: 22,
            clickable: 1
        });
        mapLabel.set('position', new google.maps.LatLng(parseFloat(khasrano[aj].ycoordinate), parseFloat(khasrano[aj].xcoordinate)));

        var khasradata = khasrano[aj].label.split('//');
        if (khasradata.length > 1) {
            mapLabel.khasrano = khasradata[1];
            mapLabel.Rectno = khasradata[0];
        }
        else {

            mapLabel.khasrano = khasradata[0];
            mapLabel.Rectno = "";
        }
        mapLabel.villageid = khasrano[aj].villageId;

        google.maps.event.addListener(mapLabel, 'click', function () {
            getInfo(this.villageid, this.khasrano, this.Rectno);
        });

        KHASRANO_LAYER.push({ "villageid": khasrano[aj].villageId, "layer": mapLabel });
        Polys.push(mapLabel);
    }
}
function showDisBoundariesRectWithKhasraNo(response, villageid) {
    var rectkhasrano = $.map(response, function (el) { return el; })
    for (ak = 0; ak < rectkhasrano.length; ak++) {
        var lp = new google.maps.LatLng(parseFloat(rectkhasrano[ak].ycoordinate), parseFloat(rectkhasrano[ak].xcoordinate));


        var mapLabel = new MapLabel({
            text: rectkhasrano[ak].label,
            position: lp,
            map: map,
            fontSize: 11,
            align: 'center',
            fontColor: rectkhasrano[ak].fillColor,
            minZoom: 17,
            maxZoom: 22,
            clickable: 1
        });
        mapLabel.set('position', new google.maps.LatLng(parseFloat(rectkhasrano[ak].ycoordinate), parseFloat(rectkhasrano[ak].xcoordinate)));
        var khasradata = rectkhasrano[ak].label.split('//');
        if (khasradata.length > 1) {
            mapLabel.khasrano = rectkhasrano[ak].label;
            //mapLabel.khasrano = khasradata[1];
            mapLabel.Rectno = khasradata[0];

        }
        else {

            //mapLabel.khasrano = khasradata[0];
            mapLabel.khasrano = rectkhasrano[ak].label;
            mapLabel.Rectno = "0";

        }
        mapLabel.id = rectkhasrano[ak].id;
        mapLabel.villageid = rectkhasrano[ak].villageId;

        google.maps.event.addListener(mapLabel, 'dblclick', function () {
            // Change the color of label when clicks on it
            this.set('fontSize', 22);
            this.set('fontColor', 'red');
            this.set('strokeWeight', 3);
            this.set('strokeColor', '#ffffff');
            this.set('align', 'center');
            this.set('fontWeight', 'bolder');
            clearhighlightedkhasra(this);

            getInfo(this.villageid, this.khasrano, this.Rectno, this.id);
        });

        RECTWITHKHASRANO_LAYER.push({ "villageid": rectkhasrano[ak].villageId, "layer": mapLabel });

        Polys.push(mapLabel);
    }
}
function clearhighlightedkhasra(_label) {

    $.each(highlighted_khasra_LAYER, function (index, value) {
        if (_label != value) {
            value.set('fontFamily', 'sans-serif');
            value.set('fontSize', 12);
            value.set('fontColor', '#AC8373');
            value.set('strokeWeight', 4);
            value.set('strokeColor', '#ffffff');
            value.set('align', 'center');
            value.set('zIndex', 1e3);
        }
        highlighted_khasra_LAYER.splice(index, 1);
    });
    highlighted_khasra_LAYER.push(_label);

}

function showDisBoundariesWell(response, villageid) {
    var well = $.map(response, function (el) { return el; })
    for (al = 0; al < well.length; al++) {
        var ln = createLine(getLatLongArr(well[al].polygon));
        ln.setOptions({ strokeWeight: 1, strokeColor: well[al].fillColor });
        WELL_LAYER.push({ "villageid": well[al].villageId, "layer": ln });
        // WELL_LAYER.push(ln);
        Polys.push(ln);
    }
}

/*Village Boundary End*/

function getInfo(villageid, khasrano, RectNo, khasraid) {

    HttpGet("/GIS/GetKhasraBasisOtherDetails?VillageId=" + parseInt(villageid) + "&KhasraNo=" + khasrano + "&RectNo=" + RectNo, 'json', function (response) {
        showKhasraBasisOtherDetails(response, khasraid, khasrano);
    });

    HttpGet("/GIS/GetKhasraBasisOtherDetailsForCourtCases?VillageId=" + parseInt(villageid) + "&KhasraNo=" + khasrano + "&RectNo=" + RectNo, 'json', function (response) {
        showKhasraBasisOtherDetailsForCourtCases(response);
    });
}

function showKhasraBasisOtherDetails(resp, khasraid, khasrano) {
    debugger;
    var tbl = $('#RouteDetailShow #tblVillageData').empty();
    tbl.empty();

    if (resp.length > 0) {
        $('#hdnkhasraid').empty().val(khasraid);
        $('#txtKhasrano').empty().val(khasrano);
        $.each(resp[0], function (indx, itm) {
            if (itm != null && itm != "")
                tbl.append('<tr><td> <p class="m-0">' + itm.split(",")[0] + ' : <strong id="' + indx + '">' + itm.split(",")[1] + '</strong></p></td> </tr>');

        });
        $('#RouteDetailShow').show();
    }
    else {
        $('#RouteDetailShow').hide();
        InfoMessage('No Data Found');
    }

}

/*edit khasra no*/
function editKhasra() {
    $('#btnedit').click();
}
/*update khasra no*/
$(".btnupdate").click(function () {
    var khasraid = $('#hdnkhasraid').val();
    var khasrano = $('#txtKhasrano').val();
    if (khasraid != "") {
        if (khasrano != "") {
            HttpGet("/GIS/UpdatekhasraNo?khasraid=" + parseInt(khasraid) + "&KhasraNo=" + khasrano, 'json', function (response) {

                alert(response.responseMessage);
                $('#txtKhasrano').val('');
                $('#btnclose').click();

            });
        }
        else {
            alert("Please enter khasra no.");
        }
    }
    else {
        alert("Unable to process your request.Kindly refresh this page and try again.");
    }

});

function showKhasraBasisOtherDetailsForCourtCases(resp) {
    debugger;
    var html = '';
    var tbl = '';
    $("#CourtCaseData").html('');
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
        $("#CourtCaseData").html('');
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
    var data = JSON.parse(alljsondata);  //get data from local storage
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

        var Calvert = data.filter((x) => x.gisLayerId === 34);//Calvert
        if (Calvert.length > 0)
            showDisBoundariesCalvert(Calvert);

        var Mick = data.filter((x) => x.gisLayerId === 33);//Mick
        if (Mick.length > 0)
            showDisBoundariesMick(Mick);

        var Line = data.filter((x) => x.gisLayerId === 35);//Line
        if (Line.length > 0)
            showDisBoundariesLine(Line);

        var Vland = data.filter((x) => x.gisLayerId === 36);//DDA Vacant Land
        if (Vland.length > 0)
            showDDAVacantLandBoundaries(Vland);
    }
    else {
        $('#chkAllImpInfra').closest('table').find('td input[type="checkbox"]').prop('checked', false);
        $('#chkAllImpInfra').closest('table').find('td input[type="checkbox"]').removeAttr('checked');

        var Abadi = data.filter((x) => x.gisLayerId === 1);    //abadi
        if (Abadi.length > 0) {
            $.each(ABADI_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Burji = data.filter((x) => x.gisLayerId === 2);//burji
        if (Burji.length > 0) {
            $.each(BURJI_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Clean = data.filter((x) => x.gisLayerId === 3);//clean
        if (Clean.length > 0) {
            $.each(CLEAN_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Dim = data.filter((x) => x.gisLayerId === 4);//Dimension
        if (Dim.length > 0) {
            $.each(DIM_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Encroachment = data.filter((x) => x.gisLayerId === 5);//Encroachment
        if (Encroachment.length > 0) {
            $.each(ENCROACHMENT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Grid = data.filter((x) => x.gisLayerId === 6);//Grid
        if (Grid.length > 0) {
            $.each(GRID_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Nala = data.filter((x) => x.gisLayerId === 7);//Nala
        if (Nala.length > 0) {
            $.each(NALA_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var TriJunction = data.filter((x) => x.gisLayerId === 8);//TriJunction
        if (TriJunction.length > 0) {
            $.each(TRIJUNCTION_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Zero = data.filter((x) => x.gisLayerId === 9);//Zero
        if (Zero.length > 0) {
            $.each(ZERO_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Nali = data.filter((x) => x.gisLayerId === 10);//Nali
        if (Nali.length > 0) {
            $.each(NALI_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var RailwayLine = data.filter((x) => x.gisLayerId === 11);//RailwayLine
        if (RailwayLine.length > 0) {
            $.each(RAILWAYLINE_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var FieldBoun = data.filter((x) => x.gisLayerId === 12);//FieldBoun
        if (FieldBoun.length > 0) {
            $.each(FIELDBOUN_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Killa = data.filter((x) => x.gisLayerId === 13);//Killa
        if (Killa.length > 0) {
            $.each(KILLA_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Close = data.filter((x) => x.gisLayerId === 14);//Close
        if (Close.length > 0) {
            $.each(CLOSE_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Saheda = data.filter((x) => x.gisLayerId === 15);//Saheda
        if (Saheda.length > 0) {
            $.each(SAHEDA_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var KachaPakaLine = data.filter((x) => x.gisLayerId === 16);//KachaPakaLine
        if (KachaPakaLine.length > 0) {
            $.each(KACHAPAKALINE_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var KhasraLine = data.filter((x) => x.gisLayerId === 17);//KhasraLine
        if (KhasraLine.length > 0) {
            $.each(KHASRALINE_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var KhasraBoundary = data.filter((x) => x.gisLayerId === 18);//KhasraBoundary
        if (KhasraBoundary.length > 0) {
            $.each(KHASRABOUNDARY_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Road = data.filter((x) => x.gisLayerId === 19);//Road
        if (Road.length > 0) {
            $.each(ROAD_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Dashed = data.filter((x) => x.gisLayerId === 20);//Dashed
        if (Dashed.length > 0) {
            $.each(DASHED_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Inner = data.filter((x) => x.gisLayerId === 21);//Inner
        if (Inner.length > 0) {
            $.each(INNER_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var VillageBoundary = data.filter((x) => x.VillageBoundary === 22);//VillageBoundary
        if (VillageBoundary.length > 0) {
            $.each(VillageBoundary, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Cleantext = data.filter((x) => x.gisLayerId === 23);//Cleantext
        if (Cleantext.length > 0) {
            $.each(CLEANTEXT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Gosha = data.filter((x) => x.gisLayerId === 24);//Gosha
        if (Gosha.length > 0) {
            $.each(GOSHA_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Text = data.filter((x) => x.gisLayerId === 25);//Text
        if (Text.length > 0) {
            $.each(TEXT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var DimentionText = data.filter((x) => x.gisLayerId === 26);//DimentionText
        if (DimentionText.length > 0) {
            $.each(DIMENTIONTEXT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var CloseText = data.filter((x) => x.gisLayerId === 27);//CloseText
        if (CloseText.length > 0) {
            $.each(CLOSETEXT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var VillageText = data.filter((x) => x.gisLayerId === 28);//VillageText
        if (VillageText.length > 0) {
            $.each(VILLAGETEXT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var KhasraNo = data.filter((x) => x.gisLayerId === 29);//KhasraNo
        if (KhasraNo.length > 0) {
            $.each(KHASRANO_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var RectWithKhasraNo = data.filter((x) => x.gisLayerId === 30);//RectWithKhasraNo
        if (RectWithKhasraNo.length > 0) {
            $.each(RECTWITHKHASRANO_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Well = data.filter((x) => x.gisLayerId === 31);//clean
        if (Well.length > 0) {
            $.each(WELL_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Calvert = data.filter((x) => x.gisLayerId === 34);//Calvert
        if (Calvert.length > 0) {
            $.each(CALVERT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Mick = data.filter((x) => x.gisLayerId === 33);//Mick
        if (Mick.length > 0) {
            $.each(MICK_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Line = data.filter((x) => x.gisLayerId === 35);//Line
        if (Line.length > 0) {
            $.each(LINE_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var VLand = data.filter((x) => x.gisLayerId === 36);//Line
        if (VLand.length > 0) {
            $.each(DDA_VACANT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }


    }
});

$('#navLayerstab').on('click', function (e) {    /*on layer click functionality added by renu */
    if (VILLAGEID_UNIVERSAL.length == 0) {
        $("#chkSelectAll").hide();
        $("#infrastructureData").html('<b>Please select at least one village from village list</b>');
    }
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

        var Calvert = data.filter((x) => x.gisLayerId === 34);//Calvert
        if (Calvert.length > 0 && Calvert[0].code == id)
            showDisBoundariesCalvert(Calvert);

        var Mick = data.filter((x) => x.gisLayerId === 33);//Mick
        if (Mick.length > 0 && Mick[0].code == id)
            showDisBoundariesMick(Mick);

        var Line = data.filter((x) => x.gisLayerId === 35);//Line
        if (Line.length > 0 && Line[0].code == id)
            showDisBoundariesLine(Line);

        var Vland = data.filter((x) => x.gisLayerId === 36);//DDA Vacant Land
        if (Vland.length > 0 && Vland[0].code == id)
            showDDAVacantLandBoundaries(Vland);

    }
    else {
        var Abadi = data.filter((x) => x.gisLayerId === 1);    //abadi
        if (Abadi.length > 0 && Abadi[0].code == id) {
            $.each(ABADI_LAYER, function (index, value) {
                value.layer.setMap(null);
            });

        }

        var Burji = data.filter((x) => x.gisLayerId === 2);//burji
        if (Burji.length > 0 && Burji[0].code == id) {
            $.each(BURJI_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Clean = data.filter((x) => x.gisLayerId === 3);//clean
        if (Clean.length > 0 && Clean[0].code == id) {
            $.each(CLEAN_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Dim = data.filter((x) => x.gisLayerId === 4);//Dimension
        if (Dim.length > 0 && Dim[0].code == id) {
            $.each(DIM_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Encroachment = data.filter((x) => x.gisLayerId === 5);//Encroachment
        if (Encroachment.length > 0 && Encroachment[0].code == id) {
            $.each(ENCROACHMENT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Grid = data.filter((x) => x.gisLayerId === 6);//Grid
        if (Grid.length > 0 && Grid[0].code == id) {
            $.each(GRID_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Nala = data.filter((x) => x.gisLayerId === 7);//Nala
        if (Nala.length > 0 && Nala[0].code == id) {
            $.each(NALA_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var TriJunction = data.filter((x) => x.gisLayerId === 8);//TriJunction
        if (TriJunction.length > 0 && TriJunction[0].code == id) {
            $.each(TRIJUNCTION_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Zero = data.filter((x) => x.gisLayerId === 9);//Zero
        if (Zero.length > 0 && Zero[0].code == id) {
            $.each(ZERO_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Nali = data.filter((x) => x.gisLayerId === 10);//Nali
        if (Nali.length > 0 && Nali[0].code == id) {
            $.each(NALI_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var RailwayLine = data.filter((x) => x.gisLayerId === 11);//RailwayLine
        if (RailwayLine.length > 0 && RailwayLine[0].code == id) {
            $.each(RAILWAYLINE_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var FieldBoun = data.filter((x) => x.gisLayerId === 12);//FieldBoun
        if (FieldBoun.length > 0 && FieldBoun[0].code == id) {
            $.each(FIELDBOUN_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Killa = data.filter((x) => x.gisLayerId === 13);//Killa
        if (Killa.length > 0 && Killa[0].code == id) {
            $.each(KILLA_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Close = data.filter((x) => x.gisLayerId === 14);//Close
        if (Close.length > 0 && Close[0].code == id) {
            $.each(CLOSE_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Saheda = data.filter((x) => x.gisLayerId === 15);//Saheda
        if (Saheda.length > 0 && Saheda[0].code == id) {
            $.each(SAHEDA_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var KachaPakaLine = data.filter((x) => x.gisLayerId === 16);//KachaPakaLine
        if (KachaPakaLine.length > 0 && KachaPakaLine[0].code == id) {
            $.each(KACHAPAKALINE_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var KhasraLine = data.filter((x) => x.gisLayerId === 17);//KhasraLine
        if (KhasraLine.length > 0 && KhasraLine[0].code == id) {
            $.each(KHASRALINE_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var KhasraBoundary = data.filter((x) => x.gisLayerId === 18);//KhasraBoundary
        if (KhasraBoundary.length > 0 && KhasraBoundary[0].code == id) {
            $.each(KHASRABOUNDARY_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Road = data.filter((x) => x.gisLayerId === 19);//Road
        if (Road.length > 0 && Road[0].code == id) {
            $.each(ROAD_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Dashed = data.filter((x) => x.gisLayerId === 20);//Dashed
        if (Dashed.length > 0 && Dashed[0].code == id) {
            $.each(DASHED_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Inner = data.filter((x) => x.gisLayerId === 21);//Inner
        if (Inner.length > 0 && Inner[0].code == id) {
            $.each(INNER_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var VillageBoundary = data.filter((x) => x.VillageBoundary === 22);//VillageBoundary
        if (VillageBoundary.length > 0 && VillageBoundary[0].code == id) {
            $.each(VILLAGEBOUNDARY_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Cleantext = data.filter((x) => x.gisLayerId === 23);//Cleantext
        if (Cleantext.length > 0 && Cleantext[0].code == id) {
            $.each(CLEANTEXT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Gosha = data.filter((x) => x.gisLayerId === 24);//Gosha
        if (Gosha.length > 0 && Gosha[0].code == id) {
            $.each(GOSHA_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Text = data.filter((x) => x.gisLayerId === 25);//Text
        if (Text.length > 0 && Text[0].code == id) {
            $.each(TEXT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var DimentionText = data.filter((x) => x.gisLayerId === 26);//DimentionText
        if (DimentionText.length > 0 && DimentionText[0].code == id) {
            $.each(DIMENTIONTEXT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var CloseText = data.filter((x) => x.gisLayerId === 27);//CloseText
        if (CloseText.length > 0 && CloseText[0].code == id) {
            $.each(CLOSETEXT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var VillageText = data.filter((x) => x.gisLayerId === 28);//VillageText
        if (VillageText.length > 0 && VillageText[0].code == id) {
            $.each(VILLAGETEXT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var KhasraNo = data.filter((x) => x.gisLayerId === 29);//KhasraNo
        if (KhasraNo.length > 0 && KhasraNo[0].code == id) {
            $.each(KHASRANO_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var RectWithKhasraNo = data.filter((x) => x.gisLayerId === 30);//RectWithKhasraNo
        if (RectWithKhasraNo.length > 0 && RectWithKhasraNo[0].code == id) {
            $.each(RECTWITHKHASRANO_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Well = data.filter((x) => x.gisLayerId === 31);//Well
        if (Well.length > 0 && Well[0].code == id) {
            $.each(WELL_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Calvert = data.filter((x) => x.gisLayerId === 34);//Calvert
        if (Calvert.length > 0 && Calvert[0].code == id) {
            $.each(CALVERT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Mick = data.filter((x) => x.gisLayerId === 33);//Mick
        if (Mick.length > 0 && Mick[0].code == id) {
            $.each(MICK_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Line = data.filter((x) => x.gisLayerId === 35);//Line
        if (Line.length > 0 && Line[0].code == id) {
            $.each(LINE_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
        }

        var Vland = data.filter((x) => x.gisLayerId === 36);//Line
        if (Vland.length > 0 && Vland[0].code == id) {
            $.each(DDA_VACANT_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
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
            $.each(KHASRANO_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
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
                var khasradata = khasrano[aj].label.split("//");
                if (khasradata.length > 1) {
                    //marker.khasrano = khasradata[1];
                    marker.khasrano = khasrano[aj].label;
                    marker.Rectno = khasradata[0] | 0;
                }
                else {

                    //marker.khasrano = khasradata[0];
                    marker.khasrano = khasrano[aj].label;
                    marker.Rectno = "0";
                }
                marker.villageid = khasrano[aj].villageId;
                google.maps.event.addListener(marker, 'click', function () {
                    getInfo(this.villageid, this.khasrano, this.Rectno);
                });
                KHASRANO_LAYER.push({ "villageid": khasrano[aj].villageId, "layer": marker });
                KHASRANO_LAYER.push({ "villageid": khasrano[aj].villageId, "layer": measle });
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
            $.each(KHASRANO_LAYER, function (index, value) {
                value.layer.setMap(null);
            });
            //for (h = 0; h < KHASRANO_LAYER.length; h++) {
            //    KHASRANO_LAYER[h].setMap(null);
            //}
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
                var khasradata = khasrano[aj].label.split("//");
                if (khasradata.length > 1) {
                    marker.khasrano = khasradata[1];
                    marker.Rectno = khasradata[0];
                }
                else {

                    marker.khasrano = khasradata[0];
                    marker.Rectno = "";
                }

                marker.villageid = khasrano[aj].villageId;
                google.maps.event.addListener(marker, 'click', function () {
                    getInfo(this.villageid, this.khasrano, this.Rectno);
                });
                KHASRANO_LAYER.push({ "villageid": khasrano[aj].villageId, "layer": marker });
                KHASRANO_LAYER.push({ "villageid": khasrano[aj].villageId, "layer": measle });
                //KHASRANO_LAYER.push(marker);
                //KHASRANO_LAYER.push(measle);
                map.setZoom(17);
                Polys.push(marker);
                map.panTo(new google.maps.LatLng(parseFloat(khasrano[aj].ycoordinate), parseFloat(khasrano[aj].xcoordinate)));
            }
        });
    }
});

function SetMapNull() {
    $.each(KHASRANO_LAYER, function (index, value) {
        value.layer.setMap(null);
    });
}

function callSelect2() {
    $("select").select2();
}
$('#Query1').on('click', function (e) {

    e.preventDefault();
    callSelect2();

});
function RemoveAllVillageLayer(villageids) {
    var villageid = parseInt(villageids);
    let villagelayer = zoomvillage.filter((x) => x.villageid === villageid);
    $.each(villagelayer, function (index, value) {
        value.layer.setMap(null);
    });
    let ABADIlayer = ABADI_LAYER.filter((x) => x.villageid === villageid);
    $.each(ABADIlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let Burjilayer = BURJI_LAYER.filter((x) => x.villageid === villageid);
    $.each(Burjilayer, function (index, value) {
        value.layer.setMap(null);
    });
    let cleanlayer = CLEAN_LAYER.filter((x) => x.villageid === villageid);
    $.each(cleanlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let dimlayer = DIM_LAYER.filter((x) => x.villageid === villageid);
    $.each(dimlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let encroachlayer = ENCROACHMENT_LAYER.filter((x) => x.villageid === villageid);
    $.each(encroachlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let gridlayer = GRID_LAYER.filter((x) => x.villageid === villageid);
    $.each(gridlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let nalalayer = NALA_LAYER.filter((x) => x.villageid === villageid);
    $.each(nalalayer, function (index, value) {
        value.layer.setMap(null);
    });
    let trijunctionlayer = TRIJUNCTION_LAYER.filter((x) => x.villageid === villageid);
    $.each(trijunctionlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let zerolayer = ZERO_LAYER.filter((x) => x.villageid === villageid);
    $.each(zerolayer, function (index, value) {
        value.layer.setMap(null);
    });
    let nalilayer = NALI_LAYER.filter((x) => x.villageid === villageid);
    $.each(nalilayer, function (index, value) {
        value.layer.setMap(null);
    });
    let railwaylayer = RAILWAYLINE_LAYER.filter((x) => x.villageid === villageid);
    $.each(railwaylayer, function (index, value) {
        value.layer.setMap(null);
    });
    let filelayer = FIELDBOUN_LAYER.filter((x) => x.villageid === villageid);
    $.each(filelayer, function (index, value) {
        value.layer.setMap(null);
    });
    let killalayer = KILLA_LAYER.filter((x) => x.villageid === villageid);
    $.each(killalayer, function (index, value) {
        value.layer.setMap(null);
    });
    let closelayer = CLOSE_LAYER.filter((x) => x.villageid === villageid);
    $.each(closelayer, function (index, value) {
        value.layer.setMap(null);
    });
    let sahedalayer = SAHEDA_LAYER.filter((x) => x.villageid === villageid);
    $.each(sahedalayer, function (index, value) {
        value.layer.setMap(null);
    });
    let kachapakallayer = KACHAPAKALINE_LAYER.filter((x) => x.villageid === villageid);
    $.each(kachapakallayer, function (index, value) {
        value.layer.setMap(null);
    });
    let khasralinelayer = KHASRALINE_LAYER.filter((x) => x.villageid === villageid);
    $.each(khasralinelayer, function (index, value) {
        value.layer.setMap(null);
    });
    let khasraboundaryllayer = KHASRABOUNDARY_LAYER.filter((x) => x.villageid === villageid);
    $.each(khasraboundaryllayer, function (index, value) {
        value.layer.setMap(null);
    });
    let calvertllayer = CALVERT_LAYER.filter((x) => x.villageid === villageid);
    $.each(calvertllayer, function (index, value) {
        value.layer.setMap(null);
    });
    let micklayer = MICK_LAYER.filter((x) => x.villageid === villageid);
    $.each(micklayer, function (index, value) {
        value.layer.setMap(null);
    });

    let linelayer = LINE_LAYER.filter((x) => x.villageid === villageid);
    $.each(linelayer, function (index, value) {
        value.layer.setMap(null);
    });
    let roadlayer = ROAD_LAYER.filter((x) => x.villageid === villageid);
    $.each(roadlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let dashedlayer = DASHED_LAYER.filter((x) => x.villageid === villageid);
    $.each(dashedlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let innerlayer = INNER_LAYER.filter((x) => x.villageid === villageid);
    $.each(innerlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let villageboundarylayer = VILLAGEBOUNDARY_LAYER.filter((x) => x.villageid === villageid);
    $.each(villageboundarylayer, function (index, value) {
        value.layer.setMap(null);
    });
    let cleantextlayer = CLEANTEXT_LAYER.filter((x) => x.villageid === villageid);
    $.each(cleantextlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let goshalayer = GOSHA_LAYER.filter((x) => x.villageid === villageid);
    $.each(goshalayer, function (index, value) {
        value.layer.setMap(null);
    });

    let textlayer = TEXT_LAYER.filter((x) => x.villageid === villageid);
    $.each(textlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let dimentiontextlayer = DIMENTIONTEXT_LAYER.filter((x) => x.villageid === villageid);
    $.each(dimentiontextlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let closetextlayer = CLOSETEXT_LAYER.filter((x) => x.villageid === villageid);
    $.each(closetextlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let villagetextlayer = VILLAGETEXT_LAYER.filter((x) => x.villageid === villageid);
    $.each(villagetextlayer, function (index, value) {
        value.layer.setMap(null);
    });
    let khasranolayer = KHASRANO_LAYER.filter((x) => x.villageid === villageid);
    $.each(khasranolayer, function (index, value) {
        value.layer.setMap(null);
    });
    let rectkhasranolayer = RECTWITHKHASRANO_LAYER.filter((x) => x.villageid === villageid);
    $.each(rectkhasranolayer, function (index, value) {
        value.layer.setMap(null);
    });
    let welllayer = WELL_LAYER.filter((x) => x.villageid === villageid);
    $.each(welllayer, function (index, value) {
        value.layer.setMap(null);
    });
    let ddavacantlandlayer = DDA_VACANT_LAYER.filter((x) => x.villageid === villageid);
    $.each(ddavacantlandlayer, function (index, value) {
        value.layer.setMap(null);
    });

}