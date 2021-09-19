//common function
//On Zoom Change Set or remove layers
function Zoom_change(map) {
    var mapzoom = map.getZoom();
    if (mapzoom >= 13) {
        // Remove State Layer
        for (h = 0; h < STATE_LAYER.length; h++) {
            STATE_LAYER[h].setMap(null);
        }
        //Remove Zoom Zone Boundary
        $.each(zoomZone, function (index, value) {
            value.setMap(null);
        });
        // Reset Layers
        zoomZone = [];
        STATE_LAYER = [];
    }
    else {
        //Set State Boundary
        if (STATE_LAYER.length == 0) {
            getStateboundary();
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

$(function () {
    var availableTags = [
        "ActionScript",
        "AppleScript",
        "Asp",
        "BASIC",
        "C",
        "C++",
        "Clojure",
        "COBOL",
        "ColdFusion",
        "Erlang",
        "Fortran",
        "Groovy",
        "Haskell",
        "Java",
        "JavaScript",
        "Lisp",
        "Perl",
        "PHP",
        "Python",
        "Ruby",
        "Scala",
        "Scheme"
    ];
    $("#txtSearch").autocomplete({
        source: availableTags
    });
});

$(function () {
    $("#txtSearch").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/GIS/AutoComplete/',
                data: { "prefix": request.term },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.name, value: item.id };
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        focus: function (event, ui) {
            $("#txtSearch").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            event.preventDefault();
            $("#txtSearch").val(ui.item.label);
            //Show Village Name
            $('#spanVillageName').empty().append(ui.item.label.toUpperCase());
            $('#spanVillage').empty().append('Village : ' + ui.item.label.toUpperCase())
            $('#aVillageName').show();
            HttpGet(`/GIS/GetVillageDetails?VillageId=${parseInt(ui.item.value)}`, 'json', function (response) {
                showDisBoundariesVillage(response[0].polygon, response[0].xcoordinate, response[0].ycoordinate, response[0].id);
            });
        },
        minLength: 1
    });
});

function printMaps() {
    var body = $('body');
    var mapContainer = $('#map-canvas');
    var mapContainerParent = mapContainer.parent();
    var printContainer = $('<div>');

    printContainer
        .addClass('print-container')
        //.css('position', 'fixed')
        .height(mapContainer.height())
        .append(mapContainer)
        .prependTo(body);

    var content = body
        .children()
        .not('script')
        .not(printContainer)
        .detach();

    // Patch for some Bootstrap 3.3.x `@*@media*@ print` styles. :|
    var patchedStyle = $('<style>')
        .attr('media', 'print')
        .text('img { max-width: none !important; }' +
            'a[href]:after { content: ""; }')
        .appendTo('head');

    window.print();

    body.prepend(content);
    mapContainerParent.prepend(mapContainer);

    printContainer.remove();
    patchedStyle.remove();
}

$(document).on('change', '#OpacityRange', function () {
    var Value = $(this).val() * 100;
    Value = Value.toFixed(0);
    $('#demo').html(Value);

    var Opacity = ($('#demo').html() / 100);

    for (var i = 0; i < Polys.length; i++) {
        var Poly = Polys[i];
        Poly.setOptions({ fillOpacity: Opacity });
    }
});


function displayCoordinates(pnt) {
    var coordsLabel = document.getElementById("tdCursor");
    var lat = pnt.lat();
    lat = lat.toFixed(6);
    var lng = pnt.lng();
    lng = lng.toFixed(6);
    coordsLabel.innerHTML = lng + "," + lat;
}

var lineSymbol = {
    path: 'M 0,-1 0,1',
    strokeOpacity: 1,
    scale: 2,
    strokeColor:'#C2C0BF'
};

function createDashedLine(_path) {
    var tmpLines = new google.maps.Polyline({
        strokeOpacity: 0,
        icons: [{
            icon: lineSymbol,
            offset: '0',
            repeat: '20px'
        }],
        map: map,
        path: _path
    });
    return tmpLines;
}
