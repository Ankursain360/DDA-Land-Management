var map;
var SerachResult = [];
var start_loc = [

    'Delhi',

];
var IIE_RECNO = [];
var SECT_LAYER = [];
var PLT_LAYER = [];
var BND_LAYER = [];
var LBL_LAYER = [];
var BRD_LAYER = [];
var RDS_LAYER = [];
var SRV_LAYER = [];
var VEG_LAYER = [];
var WTR_LAYER = [];
var MISC_LAYER = [];
var ELINE_LAYER = [];
var EPOLE_LAYER = [];
var FTP_LAYER = [];
var BRG_LAYER = [];
var RIVER_LAYER = [];
var RAILWAY_LAYER = [];
var MANHOLE_LAYER = [];
var LAMPPOST_LAYER = [];
var PLOT_NUMBER = [];
var PUBLIC_PURPOSE_LAYER = [];
var COMMON_LAYER = [];
var OTHERS_LAYER = [];
var WATER_HEAD_TANK = [];
var PARKING_LAYER = [];
var NALA_LAYER = [];
var POLICE_STATION_LAYER = [];
var FIRE_STATION_LAYER = [];
var GAS_LAYER = [];
var ENCROACHMENT_LAYER = [];
//Vacant Plots
var VACANTPLOT_LAYER = [];
var ALLOTEDPLOT_LAYER = [];
var UN_ALLOTEDPLOT_LAYER = [];
var UNDERCONSPLOT_LAYER = [];
var INPRODUCTIONPLOT_LAYER = [];
var NOSTATUSPLOT_LAYER = [];
var CLOSEDPLOT_LAYER = [];
var SICKPLOT_LAYER = [];
var NOTAVAILABLESPLOT_LAYER = [];
var Commercial_LAYER_for_Land = [];
var Commercial_LAND = [];
var Emergency_LAYER = [];
var Emergency_PLOTS = [];
var Land_Sale_Branch_LAYER = [];
var Land_Sale_Branch_PLOTS = [];
var Land_Administrative_Block_LAYER = [];
var Land_Administrative_Block_PLOTS = [];


var VACANT_PLOTS = [];
var ALLOTED_PLOTS = [];
var UN_ALLOTED_PLOTS = [];
var UNDERCONS_PLOTS = [];
var INPRODUCTION_PLOTS = [];
var NOTAVAILABLES_PLOT = [];
var SICK_PLOTS = [];
var CLOSED_PLOTS = [];
var NOSTATUS_PLOTS = [];
var zoomDistrict = [];
var zoomIIE = [];
var ZoomPlot = [];
//Copy of data

var VACANTPLOT_LAYER_COPY = [];
var ALLOTEDPLOT_LAYER_COPY = [];
var UNDERCONSPLOT_LAYER_COPY = [];
var INPRODUCTIONPLOT_LAYER_COPY = [];
var NOSTATUSPLOT_LAYER_COPY = [];
var NOTAVAILABLESPLOT_LAYER_COPY = [];

//
//Type of Plots data

var Industrial_LAYER = [];
var Commercial_LAYER = [];
var Residential_LAYER = [];
var Institutional_Banking_LAYER = [];
var CITY_NAME = [];
var Industrial_PLOTS = [];
var Commercial_PLOTS = [];
var Residential_PLOTS = [];
var Institutional_Banking_PLOTS = [];
// Maker 
var IIE_MARKERS = [];
var CATEGORY_MARKERS = [];
var DISTRICT_MARKER = [];
var AIRPORT_MARKERS = [];
var AIRPORT_MARKER = [];
function CoordMapType() { }

CoordMapType.prototype.tileSize = new google.maps.Size(256, 256);
CoordMapType.prototype.maxZoom = 25;

CoordMapType.prototype.getTile = function (coord, zoom, ownerDocument) {
    var div = ownerDocument.createElement('div');
    div.style.backgroundColor = '#E5E3DF';
    return div;
};

//Route
var source, destination;
var directionsDisplay;
var txtSource = $("#txtSource");
var txtDestination = $("#txtDestination");
var btnRoute = $("#btnRoute");
var directionsService = new google.maps.DirectionsService();
google.maps.event.addDomListener(btnRoute, 'click', function () {
    new google.maps.places.SearchBox(txtSource);
    new google.maps.places.SearchBox(txtDestination);
    directionsDisplay = new google.maps.DirectionsRenderer({ 'draggable': true });
});

var Categorydata = {

    "Accomodation & Food Services":
    {
        "IIE": [


            ['Sector -154', 77.46002, 28.45698, 15, 1258],
            ['Sector -155', 77.45579, 28.45234, 15, 1259],
            ['Sector -156', 77.45021, 28.45679, 15, 1261],
            ['Sector -157', 77.45551, 28.46118, 15, 1262],
            ['Sector -88', 77.424384, 28.531352, 15, 2255],

        ]


    }
}
var Marker = [
    //['Sector -154', 77.46002, 28.45698, 15, 1258, 40, 'NOPDF'],
    //['Sector -155', 77.45579, 28.45234, 15, 1259, 40, 'NOPDF'],
    //['Sector -156', 77.45021, 28.45679, 15, 1261, 40, 'NOPDF'],
    //['Sector -157', 77.45551, 28.46118, 15, 1262, 40, 'NOPDF'],
    //['Sector -88', 77.424384, 28.531352, 15, 2255, 40, 'NOPDF'],

]
//District Marker
var DistrictMarker = [

    //['CW-Kundli', 77.13180485, 28.86618415, 18, 4259, 40, 'images/districtimage/agra.png'],
    //['Kirti-Nagar', 77.139870, 28.642627, 18, 4263, 40, 'images/districtimage/agra.png'],
    //['NEPZ-Noida', 77.401710, 28.538572, 18, 4264, 40, 'images/districtimage/agra.png'],
    //['Okhla', 77.283315, 28.530570, 18, 4265, 40, 'images/districtimage/agra.png'],
    //['Okhla-IND', 77.283218, 28.517273, 18, 4266, 40, 'images/districtimage/agra.png'],
    //['Noida Phase-III', 77.593, 28.226, 9, 40, 'images/districtimage/agra.png'],



]

//Added by sachin 08/11/2017
var AIRPORT_MARKERS = [

    ['Indira Gandhi International Airport(IGI), Delhi', 77.0852373, 28.5602422, 9],
    ['ISBT KASHMIRI GATE', 77.2279084, 28.6686623, 9],
    ['New Delhi Railway station', 77.2152659, 28.6401166, 9],
    ['Noida Authority Office', 77.3184474, 28.5924664, 9]

]

//end Added by sachin 08/11/2017


var PDFMarker = [
    ['Tronica City', 77.26063, 28.785122, 15, 158, 41, 'VacantPlot/SurajpursiteC.PDF'],
]
var TextileMarkers = [
]
var ReservedMarker = [
]

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



function initialize() {
    var mapOptions = {
        zoom: 10,
        disableDefaultUI: true,
        mapTypeId: 'coordinate',
        // mapTypeId: google.maps.MapTypeId.ROADMAP,//SATELLITE
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
    showLegend();
    $('.statsDIv .close').click(function () { $('.statsDIv').hide(); });
    GetCurrentDetails("State", "4");
    showLegend();
    // setMarkers(this.IIEName);
    //setMarkers(map);
    //setDistrictMarker(map);
    //setAirportmarker(map);
    getStateboundary(4);
    getDistrictBoundary(4);
    //Added by sachin 4 july
    getAccordionData(4);
    //
}
//Added by sachin 08/09/2017
function Zoom_change(map) {
    var mapzoom = map.getZoom();
    if (mapzoom <= 8) {
        for (var pp = 0; pp < IIE_MARKERS.length; pp++) {
            IIE_MARKERS[pp].setMap(null);
        }
    }
}
//

//added by sachin 4 july
function getAccordionData(estate_recno) {

    ajaxReq('Home', 'GetZoneList', { "location": estate_recno }, function (resp) { BindDistrictName(resp) }, true);
}

function BindDistrictName(resp) {
    //// debugger;
    $("#accordion").empty();
    if (resp.status === true) {
        var DISTRICT = $.map(resp.data, function (el) { return el; })
        for (i = 0; i < DISTRICT.length; i++) {
            $("#accordion").append('<h3 title="Click Here to View the Industrial List!" style="font-size:12px; font-weight: 700;" id="d' + DISTRICT[i].District_Recno + '" onclick="showDistrict(this.id)">' + DISTRICT[i].District_Name + '</h3><div style="margin-left: 16px;"></div>');
            id_accord = 'd' + DISTRICT[i].District_Recno;
        }
    }
    else {
        alert('Dear User //n Something went wrong unable to get list of districts.please try again or contact to system administrator.')
    }
    //$("#accordion").accordion({
    //    active: 0,
    //    collapsible: false
    //});


    $("#accordion").ziehharmonika({
        headline: 'h3',
        prefix: false,
        highlander: true,
        collapsible: true,
        arrow: true,
        collapseIcons: {
            opened: '&ndash;',
            closed: '+'
        },
        collapseIconsAlign: 'right',
        scroll: true
    });


}

function showDistrict(maxima) {
    var newdis_id = maxima.replace('d', '');
    GetCurrentDetails("District", newdis_id);
    ajaxReq('Home', 'GetVillageList', { "ZoneId": newdis_id, "oldid": maxima }, function (resps) { BindIndustrialArea(resps, maxima) }, true);
}

function BindIndustrialArea(resps, maxima) {
    $("#Plotinfo").dialog(opt).dialog("close");
    //debugger;
    if (resps.status === true) {
        var indus = $.map(resps.data, function (el) { return el; })
        $("#" + maxima).next().empty();
        var data = '';
        if (parseInt(indus[0].Ind_area_recno) != 0) {
            data = '<div> <div class="list-type5"><ul>';

            for (i = 0; i < indus.length; i++) {
                if (parseInt(indus[i].iie_check) != 0) {
                    data += '<li id="i' + indus[i].Ind_area_recno + '" onclick="showIndustrial(this.id)" title="Click Here to View the Map!""><a href="#"  id="i' + indus[i].Ind_area_recno + '" onclick="showIndustrial(this.id)">' + indus[i].Ind_area_name + '</a> <span class="glyphicon glyphicon-eye-open pull-right"></span></li>';
                }
                else if (parseInt(indus[i].iie_URL) != 0) {

                    //disable non map IIE location  add by sachin 14/09/2017
                    data +=
                        //'<li id="i' + indus[i].Ind_area_recno + '" onclick="" title="Click Here to View the Map!""><a href="#"  id="i' + indus[i].Distance_from_Bus_Station + '" onclick="">' + indus[i].Ind_area_name + '</a> <span class="glyphicon glyphicon-eye-open pull-right"></span></li>';
                        '<li><a href="' + indus[i].URL + '" target="_blank"">' + indus[i].Ind_area_name + '</a> <span class="glyphicon glyphicon-eye-open pull-right"></span></li>';
                } else if (parseInt(indus[i].iie_URL) = 0) {
                    data += '<li id="i' + indus[i].URL + '" class="disabled" style="color:gray;cursor: not-allowed;" title="No Map Found""><a style="color:gray;cursor: not-allowed;"  id="i' + indus[i].Ind_area_recno + '">' + indus[i].Ind_area_name + '</a> <span class="glyphicon glyphicon-eye-open pull-right"></span></li>';
                } else {
                    data += '<li id="i' + indus[i].Ind_area_recno + '" class="disabled" style="color:gray;cursor: not-allowed;" title="No Map Found""><a style="color:gray;cursor: not-allowed;"  id="i' + indus[i].Ind_area_recno + '">' + indus[i].Ind_area_name + '</a> <span class="glyphicon glyphicon-eye-open pull-right"></span></li>';
                }
            }

            data += '</ul><div></div>';
        }
        else {
            data = '<div> <center><b><u></u></b></center><br/> No Data Found For this Phase!!';
        }
        $("#" + maxima).next().append(data);

        showDisBoundaries(indus[0].Polygon, indus[0].X, indus[0].Y);

    }
    else {
        $("#" + maxima).next().empty();
        $("#" + maxima).next().append('No Data Found For this District!!');
    }
}

function showIndustrial(ind_id) {
    var newind_id = ind_id.replace('i', '');
    GetCurrentDetails("IE", newind_id);
    ajaxReq('Home', 'GetVillageBoundary', { "Ind_Recno": newind_id }, function (resps) { BindIndBoundary(resps, newind_id) }, true);
}

function BindIndBoundary(resps, ind_id) {
    if (resps.status === true) {
        var indusb = $.map(resps.data, function (el) { return el; })
        var str = ind_id;
        if (indusb[0].Polygon !== null) {
            showIndBoundaries(indusb[0].Polygon, indusb[0].X, indusb[0].Y);
            ShowInddetailsfromList(ind_id, indusb[0].X, indusb[0].Y);
        } else {
            setaccordionMarkers(str);
        }
    }

}
function ShowInddetailsfromList(ind_id, xaixis, yaixis) {
    IIE_RECNO.push(ind_id);
    getPlotcreate(ind_id);
    getroadcreate(ind_id);
    getutilitycreate(ind_id);
    getPublicPurposecreate(ind_id);
    getCommanareacreate(ind_id);
    getOthercreate(ind_id);
    getWaterheadtankcreate(ind_id);
    getparkingcreate(ind_id);
    getNalacreate(ind_id);
    getPoloicestationcreate(ind_id);
    getfirestationcreate(ind_id);
    getlabelcreate(ind_id);
    getvegcreate(ind_id);
    getbndcreate(ind_id);
    getsectorcreate(ind_id);
    getElinecreate(ind_id);
    getEPolecreate(ind_id);
    getWatercreate(ind_id);
    getRivercreate(ind_id);
    getlamppostcreate(ind_id);
    getRailwaycreate(ind_id);
    getmanholecreate(ind_id);
    getgascreate(ind_id);
    getfootpathcreate(ind_id);
    getBridgecreate(ind_id);
    getencroachmentcreate(ind_id);
    //getestateinformation(ind_id);
    getestateinformation_forlist(ind_id);
    CITY_NAME.push(ind_id);
}

function showIndBoundaries(ploygn, xaixis, yaixis) {

    for (var x = 0; x < zoomDistrict.length; x++) {
        zoomDistrict[x].setMap(null);
    }
    var sl = createPolygon(getLatLongArr(ploygn));
    sl.setOptions({ strokeWeight: 5, strokeColor: '#F57002', fillOpacity: 0, clickable: !1 });
    zoomDistrict.push(sl);
    map.setZoom(15);
    map.panTo(new google.maps.LatLng(yaixis, xaixis));

}

function showDisBoundaries(ploygn, xaixis, yaixis) {

    for (var x = 0; x < zoomDistrict.length; x++) {
        zoomDistrict[x].setMap(null);
    }
    var sl = createPolygon(getLatLongArr(ploygn));
    sl.setOptions({ strokeWeight: 5, strokeColor: '#0242BD', fillOpacity: 0, clickable: !1 });
    zoomDistrict.push(sl);
    map.setZoom(12);
    map.panTo(new google.maps.LatLng(yaixis, xaixis));

}
//

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

function setFullMap(map) {
    for (i = 0; i < start_loc.length; i++) {

        // showPlots(start_loc[i][0]);
    }
}


//Added by sachin 08/11/2017
function setAirportmarker(map) {

    var marker, i;
    for (i = 0; i < AIRPORT_MARKERS.length; i++) {
        var beach = AIRPORT_MARKERS[i];
        /// if (beach[4] == DISTT) {
        var icon = "";
        switch (beach[0]) {

            case "Indira Gandhi International Airport(IGI), Delhi":
                icon = "images/plane.png";
                break;
            case "ISBT KASHMIRI GATE":
                icon = "images/isbt.png";

                break;
            case "New Delhi Railway station":
                icon = "images/Rail.png";

                break;
            case "Noida Authority Office":
                icon = "images/building.gif";

                break;

        }
        var mm = icon;
        marker = new google.maps.Marker({
            position: { lat: beach[2], lng: beach[1] },
            map: map,
            icon: mm,


            //  animation: google.maps.Animation.DROP,
            title: beach[0],
            //zIndex: beach[3]
        });
        marker.zaixs = beach[3],
            marker.x = beach[1],
            marker.y = beach[2],
            marker.IIEName = beach[0]
        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                map.setZoom(this.zaixs);
                map.panTo(new google.maps.LatLng(this.x, this.y));
            }
        })(marker, i));

        google.maps.event.addListener(marker, 'click', function () {
            map.setZoom(this.zaixs);
            map.panTo(new google.maps.LatLng(this.y, this.x));

        });

        AIRPORT_MARKER.push(marker);
    }
}
//end Added by sachin 08/11/2017

function setDistrictMarker(map) {

    var mm = "https://maps.google.com/intl/en_us/mapfiles/ms/micons/orange-dot.png";
    var marker, i;
    for (i = 0; i < DistrictMarker.length; i++) {
        var beach = DistrictMarker[i];

        var div = document.createElement('DIV');
        div.innerHTML = '<div id="Marker"><center><div class="MarkerCss">' + beach[0] + '</div><div class="arrow_box"></div></center></div>';

        marker = new RichMarker({
            map: map,
            position: new google.maps.LatLng(beach[2], beach[1]),
            draggable: false,
            flat: false,
            anchor: RichMarkerPosition.MIDDLE,
            content: div
        });
        marker.zaixs = beach[3],
            marker.x = beach[1],
            marker.y = beach[2],
            marker.IIEName = beach[4],
            marker.IIENamess = beach[0]
        // marker.PDF = beach[6],


        google.maps.event.addListener(marker, 'click', function () {



            IIE_RECNO.push(this.IIEName);
            map.setZoom(this.zaixs);
            getPlotcreate(this.IIEName);
            getroadcreate(this.IIEName);
            getutilitycreate(this.IIEName);
            getPublicPurposecreate(this.IIEName);
            getCommanareacreate(this.IIEName);
            getOthercreate(this.IIEName);
            getWaterheadtankcreate(this.IIEName);
            getparkingcreate(this.IIEName);
            getNalacreate(this.IIEName);
            getPoloicestationcreate(this.IIEName);
            getfirestationcreate(this.IIEName);
            getlabelcreate(this.IIEName);
            getvegcreate(this.IIEName);
            getbndcreate(this.IIEName);
            getsectorcreate(this.IIEName);
            getElinecreate(this.IIEName);
            getEPolecreate(this.IIEName);
            getWatercreate(this.IIEName);
            getRivercreate(this.IIEName);
            getlamppostcreate(this.IIEName);
            getRailwaycreate(this.IIEName);
            getmanholecreate(this.IIEName);
            getgascreate(this.IIEName);
            getfootpathcreate(this.IIEName);
            getBridgecreate(this.IIEName);
            getencroachmentcreate(this.IIEName);

            getestateinformation(this.IIEName);
            //  getAllIIE(this.IIEName);
            // ChangeIIEinformation(this.IIENamess);
            // $("#div_legend").dialog('open');
            map.panTo(new google.maps.LatLng(this.y, this.x));
            // zoomTo(this.zaixs);
            CITY_NAME.push(this.IIEName);
            //marker.zaixs = beach[3],
            //marker.x = beach[1],
            //marker.y = beach[2],
            // marker.IIEName = beach[4],
            //google.maps.event.addListener(marker, 'click', function () {
            //    for (var pp = 0; pp < IIE_MARKERS.length; pp++) {
            //        IIE_MARKERS[pp].setMap(null);
            //    }
            //    map.setZoom(this.zaixs);
            //    setPDFMarkers(this.IIEName);
            //    //setMarkers(this.IIEName);
            //    //GOVTMarker(this.IIEName);
            //    map.panTo(new google.maps.LatLng(this.y, this.x));
            //    zoomTo(this.zaixs);
        });
        DISTRICT_MARKER.push(marker);
    }
    function zoomTo(zoomFluid) {

        if (zoomFluid == 7) return 0;
        else {
            zoomFluid++;
            map.setZoom(zoomFluid);
            setTimeout("zoomTo()", 500);
        }
    }
}
function setPDFMarkers(STATE) {


    var infowindow = new google.maps.InfoWindow();
    //var mm = "https://maps.google.com/mapfiles/ms/icons/Yellow-dot.png";
    var mm = "https://maps.google.com/intl/en_us/mapfiles/ms/micons/yellow-dot.png";
    var marker, i;
    var CITY;
    for (i = 0; i < PDFMarker.length; i++) {
        var beach = PDFMarker[i];
        if (beach[5] == STATE) {

            var div = document.createElement('DIV');
            div.innerHTML = '<div id="IEMarker3"><center><div class="IEMarkerCss3" style="font-weight:bolder;">' + beach[0] + '</div><div class="IEarrow_box3"></div></center></div>';

            marker = new RichMarker({
                map: map,
                position: new google.maps.LatLng(beach[2], beach[1]),
                draggable: false,
                flat: false,
                anchor: RichMarkerPosition.MIDDLE,
                content: div
            });

            marker.zaixs = beach[3],
                marker.x = beach[1],
                marker.y = beach[2],
                marker.IIEName = beach[4],
                marker.IIENamess = beach[0]
            marker.PDF = beach[6],


                google.maps.event.addListener(marker, 'click', function () {


                    if (this.PDF !== 'NOPDF') {
                        //window.open(this.PDF, '_newtab');
                        getVacantPlotDetails(this.IIEName);
                    }
                    //IIE_RECNO.push(this.IIEName);
                    map.setZoom(this.zaixs);
                    getPlotcreate(this.IIEName);
                    getroadcreate(this.IIEName);
                    getutilitycreate(this.IIEName);
                    getPublicPurposecreate(this.IIEName);
                    getCommanareacreate(this.IIEName);
                    getOthercreate(this.IIEName);
                    getWaterheadtankcreate(this.IIEName);
                    getparkingcreate(this.IIEName);
                    getNalacreate(this.IIEName);
                    getPoloicestationcreate(this.IIEName);
                    getfirestationcreate(this.IIEName);
                    getlabelcreate(this.IIEName);
                    getvegcreate(this.IIEName);
                    getbndcreate(this.IIEName);
                    getsectorcreate(this.IIEName);
                    getElinecreate(this.IIEName);
                    getEPolecreate(this.IIEName);
                    getWatercreate(this.IIEName);
                    getRivercreate(this.IIEName);
                    getlamppostcreate(this.IIEName);
                    getRailwaycreate(this.IIEName);
                    getmanholecreate(this.IIEName);
                    getgascreate(this.IIEName);
                    getfootpathcreate(this.IIEName);
                    getBridgecreate(this.IIEName);
                    getencroachmentcreate(this.IIEName);
                    //    showPlots(this.IIEName);
                    getestateinformation(this.IIEName);
                    //  getAllIIE(this.IIEName);
                    // ChangeIIEinformation(this.IIENamess);
                    // $("#div_legend").dialog('open');
                    map.panTo(new google.maps.LatLng(this.y, this.x));
                    // zoomTo(this.zaixs);
                    CITY_NAME.push(this.IIEName);

                });


            IIE_MARKERS.push(marker);

        }
    }
    //  var zoomTo;
    //function zoomTo(zoomFluid) {

    //    if (zoomFluid == 7) return 0;
    //    else {
    //        zoomFluid++;
    //        map.setZoom(zoomFluid);
    //        setTimeout("zoomTo(zoomFluid)", 500);
    //    }
    //}
}
function setMarkers(DISTT) {

    GetCurrentDetails("District", DISTT);

    var image = {
        url: 'http://www.prodraw.net/favicon/temp/rxltlx5y_1.ico',

        size: new google.maps.Size(20, 32),
        // The origin for this image is (0, 0).
        origin: new google.maps.Point(0, 0),
        // The anchor for this image is the base of the flagpole at (0, 32).
        anchor: new google.maps.Point(0, 32)
    };
    // Shapes define the clickable region of the icon. The type defines an HTML
    // <area> element 'poly' which traces out a polygon as a series of X,Y points.
    // The final coordinate closes the poly by connecting to the first coordinate.
    var shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly',
        fillColor: '#3933FF'
    };
    var infowindow = new google.maps.InfoWindow();
    var mm = "https://maps.google.com/mapfiles/ms/icons/blue-dot.png";
    var marker, i;
    var CITY;
    for (i = 0; i < Marker.length; i++) {
        var beach = Marker[i];
        if (beach[5] == DISTT) {

            var div = document.createElement('DIV');
            div.innerHTML = '<div id="IEMarker"><center><div class="IEMarkerCss" style="font-weight:bolder;">' + beach[0] + '</div><div class="IEarrow_box"></div></center></div>';

            marker = new RichMarker({
                map: map,
                position: new google.maps.LatLng(beach[2], beach[1]),
                draggable: false,
                flat: false,
                anchor: RichMarkerPosition.MIDDLE,
                content: div
            });

            marker.zaixs = beach[3],
                marker.x = beach[1],
                marker.y = beach[2],
                marker.IIEName = beach[4],
                marker.IIENamess = beach[0]
            marker.PDF = beach[6],


                google.maps.event.addListener(marker, 'click', function () {


                    if (this.PDF !== 'NOPDF') {
                        // window.open(this.PDF, '_newtab');


                        getVacantPlotDetails(this.IIEName);
                    }
                    IIE_RECNO.push(this.IIEName);
                    map.setZoom(this.zaixs);
                    getPlotcreate(this.IIEName);
                    getroadcreate(this.IIEName);
                    getutilitycreate(this.IIEName);
                    getPublicPurposecreate(this.IIEName);
                    getCommanareacreate(this.IIEName);
                    getOthercreate(this.IIEName);
                    getWaterheadtankcreate(this.IIEName);
                    getparkingcreate(this.IIEName);
                    getNalacreate(this.IIEName);
                    getPoloicestationcreate(this.IIEName);
                    getfirestationcreate(this.IIEName);
                    getlabelcreate(this.IIEName);
                    getvegcreate(this.IIEName);
                    getbndcreate(this.IIEName);
                    getsectorcreate(this.IIEName);
                    getElinecreate(this.IIEName);
                    getEPolecreate(this.IIEName);
                    getWatercreate(this.IIEName);
                    getRivercreate(this.IIEName);
                    getlamppostcreate(this.IIEName);
                    getRailwaycreate(this.IIEName);
                    getmanholecreate(this.IIEName);
                    getgascreate(this.IIEName);
                    getfootpathcreate(this.IIEName);
                    getBridgecreate(this.IIEName);
                    getencroachmentcreate(this.IIEName);
                    //    showPlots(this.IIEName);
                    getestateinformation(this.IIEName);
                    //  getAllIIE(this.IIEName);
                    // ChangeIIEinformation(this.IIENamess);
                    // $("#div_legend").dialog('open');
                    map.panTo(new google.maps.LatLng(this.y, this.x));
                    // zoomTo(this.zaixs);
                    CITY_NAME.push(this.IIEName);

                });


            IIE_MARKERS.push(marker);

        }
    }
    //  var zoomTo;
    //function zoomTo(zoomFluid) {

    //    if (zoomFluid == 7) return 0;
    //    else {
    //        zoomFluid++;
    //        map.setZoom(zoomFluid);
    //        setTimeout("zoomTo(zoomFluid)", 500);
    //    }
    //}
}
function setaccordionMarkers(STATE) {
    // Adds markers to the map.

    // Marker sizes are expressed as a Size of X,Y where the origin of the image
    // (0,0) is located in the top left of the image.

    // Origins, anchor positions and coordinates of the marker increase in the X
    // direction to the right and in the Y direction down.
    var image = {
        url: 'http://www.prodraw.net/favicon/temp/rxltlx5y_1.ico',
        // This marker is 20 pixels wide by 32 pixels high.
        size: new google.maps.Size(20, 32),
        // The origin for this image is (0, 0).
        origin: new google.maps.Point(0, 0),
        // The anchor for this image is the base of the flagpole at (0, 32).
        anchor: new google.maps.Point(0, 32)
    };
    // Shapes define the clickable region of the icon. The type defines an HTML
    // <area> element 'poly' which traces out a polygon as a series of X,Y points.
    // The final coordinate closes the poly by connecting to the first coordinate.
    var shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly',
        fillColor: '#3933FF'
    };
    //map.setZoom(18);
    var infowindow = new google.maps.InfoWindow();
    //var mm = "https://maps.google.com/mapfiles/ms/icons/Yellow-dot.png";
    var mm = "https://maps.google.com/intl/en_us/mapfiles/ms/micons/yellow-dot.png";
    var marker, i;
    var CITY;
    for (i = 0; i < PDFMarker.length; i++) {
        var beach = PDFMarker[i];
        if (beach[4] == STATE) {
            var div = document.createElement('DIV');
            div.innerHTML = '<div id="IEMarker3"><center><div class="IEMarkerCss3" style="font-weight:bolder;">' + beach[0] + '</div><div class="IEarrow_box3"></div></center></div>';

            marker = new RichMarker({
                map: map,
                position: new google.maps.LatLng(beach[2], beach[1]),
                draggable: false,
                flat: false,
                anchor: RichMarkerPosition.MIDDLE,
                content: div
            });
            marker.zaixs = beach[3],
                marker.x = beach[1],
                marker.y = beach[2],
                marker.IIEName = beach[4],
                marker.IIENamess = beach[0]
            marker.PDF = beach[6],


                google.maps.event.addListener(marker, 'click', function () {


                    if (this.PDF !== 'NOPDF') {


                        getVacantPlotDetails(this.IIEName);

                    }
                    IIE_RECNO.push(this.IIEName);
                    map.setZoom(this.zaixs);
                    getPlotcreate(this.IIEName);
                    getroadcreate(this.IIEName);
                    getutilitycreate(this.IIEName);
                    getPublicPurposecreate(this.IIEName);
                    getCommanareacreate(this.IIEName);
                    getOthercreate(this.IIEName);
                    getWaterheadtankcreate(this.IIEName);
                    getparkingcreate(this.IIEName);
                    getNalacreate(this.IIEName);
                    getPoloicestationcreate(this.IIEName);
                    getfirestationcreate(this.IIEName);
                    getlabelcreate(this.IIEName);
                    getvegcreate(this.IIEName);
                    getbndcreate(this.IIEName);
                    getsectorcreate(this.IIEName);
                    getElinecreate(this.IIEName);
                    getEPolecreate(this.IIEName);
                    getWatercreate(this.IIEName);
                    getRivercreate(this.IIEName);
                    getlamppostcreate(this.IIEName);
                    getRailwaycreate(this.IIEName);
                    getmanholecreate(this.IIEName);
                    getgascreate(this.IIEName);
                    getfootpathcreate(this.IIEName);
                    getBridgecreate(this.IIEName);
                    getencroachmentcreate(this.IIEName);
                    map.panTo(new google.maps.LatLng(this.y, this.x));

                    CITY_NAME.push(this.IIEName);

                });

            map.panTo(new google.maps.LatLng(beach[2], beach[1]));
            map.setZoom(15);
            IIE_MARKERS.push(marker);
        }


    }

}
function ChangeIIEinformation(IIE) {
    if (IIE == "TRANSGANGA") {
        $("#LiQuery").removeClass('active');
        $("#LiLayers").removeClass('active');
        $("#LiLegends").removeClass('active');
        $("#LiIndustrialList").removeClass('active');
        $("#Liinformation").addClass("active");
        $('#home').removeClass('in active');
        $('#profile').removeClass('in active');
        $('#messages').removeClass('in active');

        $('#AMOUSI').css('display', 'none');
        $('#legends').addClass('in active');
        $('#TRANSGANGA').css('display', 'block');

    }
    else if (IIE == "AMOUSI") {
        $('#AMOUSI').css('display', 'block');//visible

        $("#LiQuery").removeClass('active');
        $("#LiLayers").removeClass('active');
        $("#LiLegends").removeClass('active');
        $("#LiIndustrialList").removeClass('active');
        $("#Liinformation").addClass("active");
        $('#home').removeClass('in active');
        $('#profile').removeClass('in active');
        $('#messages').removeClass('in active');


        $('#legends').addClass('in active');
        $('#TRANSGANGA').css('display', 'none');
    }

}

function setSIDCMarkers(PDFMAP) {
    // Adds markers to the map.

    // Marker sizes are expressed as a Size of X,Y where the origin of the image
    // (0,0) is located in the top left of the image.

    var image = {
        url: 'https://www.prodraw.net/favicon/temp/rxltlx5y_1.ico',

        size: new google.maps.Size(20, 32),

        origin: new google.maps.Point(0, 0),

        anchor: new google.maps.Point(0, 32)
    };

    var shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly',
        fillColor: '#3933FF'
    };
    var infowindow = new google.maps.InfoWindow();
    var mm = "https://maps.google.com/intl/en_us/mapfiles/ms/micons/yellow-dot.png";
    var marker, i;
    for (i = 0; i < SIDCMarkers.length; i++) {
        var beach = SIDCMarkers[i];
        marker = new google.maps.Marker({
            position: { lat: beach[2], lng: beach[1] },
            map: map,
            icon: mm,

            // shape: shape,
            // animation: google.maps.Animation.DROP,
            title: beach[0],
            //zIndex: beach[3]
        });
        marker.zaixs = beach[3],
            marker.x = beach[1],
            marker.y = beach[2],
            marker.IIEName = beach[0]
        //google.maps.event.addListener(marker, 'click', (function (marker, i) {
        //    return function() {
        //        map.setZoom(beach[3]);
        //        map.panTo(new google.maps.LatLng(beach[2], beach[1]));
        //    }
        //})(marker, i));

        google.maps.event.addListener(marker, 'click', function () {

            //var latLngs = new google.maps.LatLng(this.y, this.x);
            //map.animateCamera(google.maps.animateCamera(latLng, this.zaixs));
            map.setZoom(this.zaixs);
            ChangeIIEinformation(this.IIEName);
            $("#div_legend").dialog('open');
            map.panTo(new google.maps.LatLng(this.y, this.x));
            zoomTo(this.zaixs);

        });

        // maplayer.push(marker);
        IIE_MARKERS.push(marker);

    }

    var minValue = 12, maxValue = 15;




    function zoomTo(zoomFluid) {
        //console.log(zoomFluid);
        if (zoomFluid == 7) return 0;
        else {
            zoomFluid++;
            map.setZoom(zoomFluid);
            setTimeout("zoomTo()", 500);
        }
    }
}
function setTextileMarkers(map) {
    // Adds markers to the map.

    // Marker sizes are expressed as a Size of X,Y where the origin of the image
    // (0,0) is located in the top left of the image.

    var image = {
        url: 'https://www.prodraw.net/favicon/temp/rxltlx5y_1.ico',

        size: new google.maps.Size(20, 32),

        origin: new google.maps.Point(0, 0),

        anchor: new google.maps.Point(0, 32)
    };

    var shape = {
        coords: [1, 1, 1, 20, 18, 20, 18, 1],
        type: 'poly',
        fillColor: '#3933FF'
    };
    var infowindow = new google.maps.InfoWindow();
    var mm = "https://maps.google.com/mapfiles/ms/icons/orange-dot.png";
    var marker, i;
    for (i = 0; i < TextileMarkers.length; i++) {
        var beach = TextileMarkers[i];
        marker = new google.maps.Marker({
            position: { lat: beach[2], lng: beach[1] },
            map: map,
            icon: mm,

            // shape: shape,
            // animation: google.maps.Animation.DROP,
            title: beach[0],
            //zIndex: beach[3]
        });
        marker.zaixs = beach[3],
            marker.x = beach[1],
            marker.y = beach[2],
            marker.IIEName = beach[0]
        //google.maps.event.addListener(marker, 'click', (function (marker, i) {
        //    return function() {
        //        map.setZoom(beach[3]);
        //        map.panTo(new google.maps.LatLng(beach[2], beach[1]));
        //    }
        //})(marker, i));

        google.maps.event.addListener(marker, 'click', function () {


            map.setZoom(this.zaixs);
            ChangeIIEinformation(this.IIEName);
            $("#div_legend").dialog('open');
            map.panTo(new google.maps.LatLng(this.y, this.x));
            zoomTo(this.zaixs);

        });

        // maplayer.push(marker);
        IIE_MARKERS.push(marker);

    }

    var minValue = 12, maxValue = 15;




    function zoomTo(zoomFluid) {
        //console.log(zoomFluid);
        if (zoomFluid == 7) return 0;
        else {
            zoomFluid++;
            map.setZoom(zoomFluid);
            setTimeout("zoomTo()", 500);
        }
    }
}
function setReservedMarker(map) {



    var marker, i;
    for (i = 0; i < ReservedMarker.length; i++) {
        var beach = ReservedMarker[i];

        var icon = "";
        switch (beach[0]) {

            case "Reserved For Women Entrepreneur":
                icon = "images/Reser.png";
                break;

        }
        var mm = icon;
        marker = new google.maps.Marker({
            position: { lat: beach[2], lng: beach[1] },
            map: map,
            icon: mm,


            //  animation: google.maps.Animation.DROP,
            title: beach[0],
            //zIndex: beach[3]
        });
        marker.zaixs = beach[3],
            marker.x = beach[1],
            marker.y = beach[2],
            marker.IIEName = beach[0]
        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                map.setZoom(this.zaixs);
                map.panTo(new google.maps.LatLng(this.x, this.y));
            }
        })(marker, i));

        google.maps.event.addListener(marker, 'click', function () {

            //var latLngs = new google.maps.LatLng(this.y, this.x);
            //map.animateCamera(google.maps.animateCamera(latLng, this.zaixs));
            map.setZoom(this.zaixs);
            //ChangeIIEinformation(this.IIEName);
            // $("#div_legend").dialog('open');
            map.panTo(new google.maps.LatLng(this.y, this.x));


        });

        // maplayer.push(marker);
        SIIDCUL_POLLUTION_MARKERS.push(marker);

    }




    function zoomTo(zoomFluid) {
        //console.log(zoomFluid);
        if (zoomFluid == 7) return 0;
        else {
            zoomFluid++;
            map.setZoom(zoomFluid);
            setTimeout("zoomTo()", 500);
        }
    }
}
function getVacantPlotDetails(city_name) {

    var city = city_name;

    ajaxReq('Home', 'VacantPlot', { "location": city }, function (resp) { setVacantPlotDetails(resp) }, true);
}
function setVacantPlotDetails(resp) {
    if (resp.status === true) {
        var tbl = $('.infomain table').empty();
        tbl.empty();
        tbl.append('<tr><th>Sr No.</th><th>Plot No</th><th>Plot Area(in sq.mt.)</th></tr>');
        //tbl.append('<tr><td >Plot No</td><td style="font-weight:bold;">' + plot + '</td></tr>');

        var arr = $.map(resp.data, function (el) { return el; })

        var j = 1;
        if (arr.length > 0) {
            for (i = 0; i < arr.length; i++) {
                if ((i % 2) == 0) {
                    tbl.append('<tr><td>' + (j) + '</td><td>' + arr[i] + '</td><td>' + arr[i + 1] + '</td></tr>');
                    j = j + 1;
                }
            }
            $(".infolower").empty();
            $(".infolower").css('visibility', 'visible');
        }
        else
            $(".infolower").css('visibility', 'hidden');

        //$.each(resp.data, function (indx, itm) {
        //    tbl.append('<tr><td>' + resp.data["Plot No"] + '</td><td>' + resp.data["Plot Area"] + '</td></tr>');
        //    //tbl.append('<tr><td>' + indx + '</td><td style="font-weight:bold;">' + itm + (indx == 'Plot Area' ? ' sq. mtr' : '') + '</td></tr>');
        //    if (indx == "Plot Status" && itm == "Vacant") {
        //        //  moreInfo();


        //        // var url = "http://118.185.3.27/UPSIDC_SmartCity/User/ApplicationFormForFirstTimeUser.aspx?PlotNo=" + plot + '&Area=' + area + '&Sector=' + SectorName + '&iierecno=' + city_name + '&Industrial_Area_Name=' + Industrial_Area_Name + '&District_Name=' + District_Name;
        //        //var url = "http://demo.onlineupsidc.com/ApplicationPayment.aspx?PlotNo=" + plot + '&Area=' + area + '&Sector=' + SectorName + '&iierecno=' + city_name + '&Industrial_Area_Name=' + Industrial_Area_Name + '&District_Name=' + District_Name;
        //        //  $(".infolower").append('<a id="btnApply" class="btn btn-danger" target="_blank" href="' + url + '">Apply Now</a>')
        //        //  $(".infolower").append('<a id="btnApply" target="_blank" href="#">Apply Now</a>')
        //    }

        //});

        $("#Div3").dialog(opt).dialog("open");
    }
    else {
        var tbl = $('.infomain table').empty();
        tbl.empty();
        tbl.append('<tr><th>Sr No.</th><th>Plot No</th><th>Plot Area(in sq.mt.)</th></tr>');
        tbl.append('<tr><td colspan="3"><p style="color:red;font-weight:bolder;">No Vacant Plots Data Found.</p></td></tr>');
        $(".infolower").empty();
        $(".infolower").css('visibility', 'visible');
        $("#Div3").dialog(opt).dialog("open");
    }
}
$(document).on('click', '#btnpollution', function (e) {
    e.preventDefault();
    map.setZoom(7);

    map.panTo(new google.maps.LatLng(26.846709, 80.946159));

    CloseDialogs();
    Pollution();

});
function Pollution() {
    for (i = 0; i < IIE_MARKERS.length; i++) {
        IIE_MARKERS[i].setMap(null);

    }
    for (i = 0; i < CATEGORY_MARKERS.length; i++) {
        CATEGORY_MARKERS[i].setMap(null);

    }
    setPollutionMarkers(map);
    // setSIDCPollutionMarkers(map);
    //setTextilePollutionMarkers(map);
};
var SIIDCUL_POLLUTION_MARKERS = [];
function setPollutionMarkers(map) {

    //var infowindow = new google.maps.InfoWindow();

    var marker, i;



    //   var infowindow = new google.maps.InfoWindow({
    //     content: contentString
    //});

    for (i = 0; i < Marker.length; i++) {
        var beach = Marker[i];
        var icon = "";
        switch (beach[0]) {

            case "TRANSGANGA":
                icon = "images/Blue.png";
                break;
            case "UNNAO2":
                icon = "images/Blue.png";
                break;


        }
        var mm = icon;
        marker = new google.maps.Marker({
            position: { lat: beach[2], lng: beach[1] },
            map: map,
            icon: mm,
            title: beach[0],

        });

        marker.zaixs = beach[3],
            marker.x = beach[1],
            marker.y = beach[2],
            marker.IIEName = beach[0]
        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {

                getPlotcreate(this.IIEName);
                getroadcreate(this.IIEName);
                getutilitycreate(this.IIEName);
                getPublicPurposecreate(this.IIEName);
                getCommanareacreate(this.IIEName);
                getOthercreate(this.IIEName);
                getWaterheadtankcreate(this.IIEName);
                getparkingcreate(this.IIEName);
                getNalacreate(this.IIEName);
                getPoloicestationcreate(this.IIEName);
                getfirestationcreate(this.IIEName);
                getlabelcreate(this.IIEName);
                getvegcreate(this.IIEName);
                getbndcreate(this.IIEName);
                getsectorcreate(this.IIEName);
                getElinecreate(this.IIEName);
                getEPolecreate(this.IIEName);
                getWatercreate(this.IIEName);
                getRivercreate(this.IIEName);
                getlamppostcreate(this.IIEName);
                getRailwaycreate(this.IIEName);
                getmanholecreate(this.IIEName);
                getgascreate(this.IIEName);
                getfootpathcreate(this.IIEName);
                getBridgecreate(this.IIEName);
                showPlots(this.IIEName);
                getencroachmentcreate(this.IIEName);
                map.setZoom(this.zaixs);
                ChangeIIEinformation(this.IIEName);
                //$("#div_legend").dialog('open');
                map.panTo(new google.maps.LatLng(this.y, this.x));
            }
        })(marker, i));


        marker.setMap(map);

        SIIDCUL_POLLUTION_MARKERS.push(marker);




    }

}
function setTextilePollutionMarkers(map) {

    //var infowindow = new google.maps.InfoWindow();

    var marker, i;



    //   var infowindow = new google.maps.InfoWindow({
    //     content: contentString
    //});

    for (i = 0; i < TextileMarkers.length; i++) {
        var beach = TextileMarkers[i];
        var icon = "";
        switch (beach[0]) {

            case "Textile Park,Sitarganj Phase II":
                icon = "images/split1.png";
                break;


        }
        var mm = icon;
        marker = new google.maps.Marker({
            position: { lat: beach[2], lng: beach[1] },
            map: map,
            icon: mm,
            title: beach[0],

        });

        marker.zaixs = beach[3],
            marker.x = beach[1],
            marker.y = beach[2],
            marker.IIE = beach[0]
        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                map.setZoom(this.zaixs);
                ChangeIIEinformation(this.IIE);
                $("#div_legend").dialog('open');
                map.panTo(new google.maps.LatLng(this.y, this.x));
            }
        })(marker, i));


        marker.setMap(map);

        SIIDCUL_POLLUTION_MARKERS.push(marker);




    }

}
function setSIDCPollutionMarkers(map) {



    var marker, i;
    for (i = 0; i < SIDCMarkers.length; i++) {
        var beach = SIDCMarkers[i];

        var icon = "";
        switch (beach[0]) {

            case "Bhimtal":
                icon = "images/split1.png";
                break;


        }
        var mm = icon;
        marker = new google.maps.Marker({
            position: { lat: beach[2], lng: beach[1] },
            map: map,
            icon: mm,


            //  animation: google.maps.Animation.DROP,
            title: beach[0],
            //zIndex: beach[3]
        });
        marker.zaixs = beach[3],
            marker.x = beach[1],
            marker.y = beach[2],
            marker.IIEName = beach[0]
        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                map.setZoom(this.zaixs);
                map.panTo(new google.maps.LatLng(this.x, this.y));
            }
        })(marker, i));

        google.maps.event.addListener(marker, 'click', function () {

            //var latLngs = new google.maps.LatLng(this.y, this.x);
            //map.animateCamera(google.maps.animateCamera(latLng, this.zaixs));
            map.setZoom(this.zaixs);
            ChangeIIEinformation(this.IIEName);
            $("#div_legend").dialog('open');
            map.panTo(new google.maps.LatLng(this.y, this.x));


        });

        // maplayer.push(marker);
        SIIDCUL_POLLUTION_MARKERS.push(marker);

    }




    function zoomTo(zoomFluid) {
        //console.log(zoomFluid);
        if (zoomFluid == 7) return 0;
        else {
            zoomFluid++;
            map.setZoom(zoomFluid);
            setTimeout("zoomTo()", 500);
        }
    }
}
function setReservedMarker(map) {



    var marker, i;
    for (i = 0; i < ReservedMarker.length; i++) {
        var beach = ReservedMarker[i];

        var icon = "";
        switch (beach[0]) {

            case "Reserved For Women Entrepreneur":
                icon = "images/split1.png";
                break;

        }
        var mm = icon;
        marker = new google.maps.Marker({
            position: { lat: beach[2], lng: beach[1] },
            map: map,
            icon: mm,


            //  animation: google.maps.Animation.DROP,
            title: beach[0],
            //zIndex: beach[3]
        });
        marker.zaixs = beach[3],
            marker.x = beach[1],
            marker.y = beach[2],
            marker.IIEName = beach[0]
        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                map.setZoom(this.zaixs);
                map.panTo(new google.maps.LatLng(this.x, this.y));
            }
        })(marker, i));

        google.maps.event.addListener(marker, 'click', function () {

            //var latLngs = new google.maps.LatLng(this.y, this.x);
            //map.animateCamera(google.maps.animateCamera(latLng, this.zaixs));
            map.setZoom(this.zaixs);
            //ChangeIIEinformation(this.IIEName);
            // $("#div_legend").dialog('open');
            map.panTo(new google.maps.LatLng(this.y, this.x));


        });

        // maplayer.push(marker);
        SIIDCUL_POLLUTION_MARKERS.push(marker);

    }




    function zoomTo(zoomFluid) {
        //console.log(zoomFluid);
        if (zoomFluid == 7) return 0;
        else {
            zoomFluid++;
            map.setZoom(zoomFluid);
            setTimeout("zoomTo()", 500);
        }
    }
}

function loadZoomControl(z) {
    $('div.ZoomControl').remove();
    $('div.RightTools').prepend('<div class="ZoomControl"><div class="sliderwrapper"><div title="Zoom-In" class="zoomIN tooltip" id="zIN"></div>' +
        '<div title="Zoom-Out" class="zoomOut tooltip" id="zOut"></div></div></div>');
    $('.ZoomControl').show();




    $('.logo').click(function () {
        window.location.href = 'Default.aspx';

    });

    function createPolygon(_path) {
        var tmpLine = new google.maps.Polygon({
            strokeColor: '#01FFF7',
            fillColor: '#D5DBDB',
            fillOpacity: 0.7,
            strokeWeight: 11,
            clickable: !1,
            map: map,
            path: _path
        });
        return tmpLine;
    }
    function clearAll(maplayer) {
        for (i = 0; i < maplayer.length; i++) {
            maplayer[i].setMap(null);
        }
        maplayer = [];
    }
    // End of Sector Searching 3/2/2016


    $('.DrpDiv p').click(function (e) {
        var target = $('.city .city_name');
        var ct = target.text();
        var p = $(e.target);
        var nt = p.text();
        var _city = p.attr("data-rel");
        if (ct != nt) {
            target.text(nt).attr("data-rel", _city);
            showPlots(nt);
            showStats(_city);
            showinitialdivs();
        }
    });

    $('.info .infolower').click(function (e) { $('.info').fadeOut(); });

    $(document).on('click', '#MapShowplots', function (e) {
        e.preventDefault();
        //  $("MapShowplots").click(function () {
        if (this.checked) {
            if (thematic_param === 'Plot Type') {
                thematic_param = 'Completion Status';

            }
            else {
                thematic_param = 'Plot Type';
            }
            refreshLayer();
        }
    });




    $('.legend_switch').click(function (e) {
        $(e.target).toggleClass('off');
        $('.div_legend').slideToggle();
    });

    //$('.info').draggable({ containment: "window", handle: ".infoHead" });  //infoup
}

function getAllDISTRICT(city_name) {

    var city = city_name;
    ajaxReq('Home', 'GetAllZoneList', { "location": city }, function (resp) { BindAllDISTRICT(resp) }, true);

}

function BindAllDISTRICT(resp) {
    $("#ddldistrict").empty();
    $("#ddlPhase").empty();
    if (resp.status === true) {

        var DISTRICT = $.map(resp.data, function (el) { return el; })

        $('#ddldistrict').append(new Option('--Select Zone--', ''));
        //$('#ddlPhase').append(new Option('--Select Phase--', ''));

        for (i = 0; i < DISTRICT.length; i++) {
            if (i % 5 === 0) {
                $("#ddldistrict").append(new Option(DISTRICT[i + 1], DISTRICT[i] + '/' + DISTRICT[i + 2] + '/' + DISTRICT[i + 3] + '/' + DISTRICT[i + 4]));
                // $("#ddlPhase").append(new Option(DISTRICT[i + 1], DISTRICT[i] + '/' + DISTRICT[i + 2] + '/' + DISTRICT[i + 3] + '/' + DISTRICT[i + 4]));
            }
        }
    }
}
$(document).on('change', '#ddldistrict', function (e) {
    e.preventDefault();
    var District = $('#ddldistrict option:selected').val();

    var DISTRICT_BND = District.split('/');
    getAllIIE(DISTRICT_BND[0]);
    DISTRICT_BND = DISTRICT_BND.concat(District);
    for (var x = 0; x < zoomDistrict.length; x++) {
        zoomDistrict[x].setMap(null);
    }
    var sl = createPolygon(getLatLongArr(DISTRICT_BND[1]));
    sl.setOptions({ strokeWeight: 5, strokeColor: '#0242BD', fillOpacity: 0, clickable: !1 });
    zoomDistrict.push(sl);
    map.setZoom(10);
    map.panTo(new google.maps.LatLng(DISTRICT_BND[3], DISTRICT_BND[2]));
});


function getAllIIECompanywise(city_name) {

    var city = city_name;
    ajaxReq('Home', 'GetVillageCompanywise', { "location": city }, function (resp) { BindAllIIECompanywise(resp) }, true);

}


function BindAllIIECompanywise(resp) {
    $("#ddlIIECompanywise").empty();
    if (resp.status === true) {
        var IIECompanywise = $.map(resp.data, function (el) { return el; })
        $('#ddlIIECompanywise').append(new Option('--Select Sector Area--', ''));
        for (i = 0; i < IIECompanywise.length; i++) {
            for (i = 0; i < IIECompanywise.length; i++) {
                if (i % 5 === 0) {

                    $("#ddlIIECompanywise").append(new Option(IIECompanywise[i + 1], IIECompanywise[i] + '/' + IIECompanywise[i + 2] + '/' + IIECompanywise[i + 3] + '/' + IIECompanywise[i + 4]));
                }
            }
        }
    }
    else {
        $('#ddlIIECompanywise').append(new Option('--No Data Found--', ''));
    }
}

$(document).on('change', '#ddlIIECompanywise', function (e) {
    e.preventDefault();
    var IIEE = $('#ddlIIECompanywise option:selected').text();
    var IIECompanywise = $('#ddlIIECompanywise option:selected').val();
    var IIECompanywise = IIECompanywise.split('/');
    var Company = IIECompanywise[0];
    $("#ddldistrict").empty();
    getCompanies(Company);

    //var IIECompanywise_BND = IIECompanywise.split('/');
    //IIECompanywise_BND = IIECompanywise_BND.concat(IIECompanywise);
    //All Layer Create function Date 18/jan/2017
    //getPlotcreate(IIEE);
    //getroadcreate(IIEE);
    //getutilitycreate(IIEE);
    //getlabelcreate(IIEE);
    //getvegcreate(IIEE);
    //getbndcreate(IIEE);
    //getsectorcreate(IIEE);
    //getElinecreate(IIEE);
    //getEPolecreate(IIEE);
    //getWatercreate(IIEE);
    //getRivercreate(IIEE);
    //getlamppostcreate(IIEE);
    //getRailwaycreate(IIEE);
    //getmanholecreate(IIEE);
    //for (var x = 0; x < zoomIIE.length; x++) {
    //    zoomIIE[x].setMap(null);
    //}
    //var sl = createPolygon(getLatLongArr(IIECompanywise_BND[1]));
    //sl.setOptions({ strokeWeight: 5, strokeColor: '#F0B27A', fillOpacity: 0, clickable: !1 });
    //zoomIIE.push(sl);
    //map.setZoom(15);
    //map.panTo(new google.maps.LatLng(IIECompanywise_BND[3], IIECompanywise_BND[2]));
});
$(document).on('click', '#btnCompanywise', function () {
    var plot = $('#ddlCompany option:selected').val();
    var IIEEE = $('#ddlIIECompanywise option:selected').val();
    var IA = IIEEE.split('/');
    var IIEE = IA[0];
    var PLOT_BND = plot.split('/');
    PLOT_BND = PLOT_BND.concat(plot);


    getPlotcreate(IIEE);
    getroadcreate(IIEE);
    getutilitycreate(IIEE);
    getPublicPurposecreate(IIEE);
    getCommanareacreate(IIEE);
    getOthercreate(IIEE);
    getWaterheadtankcreate(IIEE);
    getparkingcreate(IIEE);
    getNalacreate(IIEE);
    getPoloicestationcreate(IIEE);
    getfirestationcreate(IIEE);
    getlabelcreate(IIEE);
    getvegcreate(IIEE);
    getbndcreate(IIEE);
    getsectorcreate(IIEE);
    getElinecreate(IIEE);
    getEPolecreate(IIEE);
    getWatercreate(IIEE);
    getRivercreate(IIEE);
    getlamppostcreate(IIEE);
    getRailwaycreate(IIEE);
    getmanholecreate(IIEE);
    getfootpathcreate(IIEE);
    getBridgecreate(IIEE);
    getencroachmentcreate(IIEE);
    for (var x = 0; x < ZoomPlot.length; x++) {
        ZoomPlot[x].setMap(null);
    }
    var sl = createPolygon(getLatLongArr(PLOT_BND[1]));
    sl.setOptions({ strokeWeight: 5, strokeColor: '#4286f4', fillOpacity: 0, clickable: !1 });
    ZoomPlot.push(sl);
    map.setZoom(19);
    map.panTo(new google.maps.LatLng(PLOT_BND[3], PLOT_BND[2]));
});

function getAllIIE(District) {


    ajaxReq('Home', 'IIE', { "location": District }, function (resp) { BindAllIIE(resp) }, true);

}
function BindAllIIE(resp) {
    $("#ddlIIE").empty();
    $("#ddlIIE1").empty();
    $("#ddlSector").empty();
    $("#ddlPlot").empty();
    if (resp.status === true) {
        var IIE = $.map(resp.data, function (el) { return el; })
        $('#ddlIIE').append(new Option('--Select Locality--', ''));
        // $('#ddlIIE1').append(new Option('--Select Industrial Area--', ''));
        for (i = 0; i < IIE.length; i++) {

            for (i = 0; i < IIE.length; i++) {
                if (i % 5 === 0) {
                    $("#ddlIIE").append(new Option(IIE[i + 1], IIE[i] + '/' + IIE[i + 2] + '/' + IIE[i + 3] + '/' + IIE[i + 4]));
                    //  $("#ddlIIE1").append(new Option(IIE[i + 1], IIE[i] + '/' + IIE[i + 2] + '/' + IIE[i + 3] + '/' + IIE[i + 4]));

                }
            }
        }
    }
    else {
        $('#ddlIIE').append(new Option('--No Data Found--', ''));
        //  $('#ddlIIE1').append(new Option('--No Data Found--', ''));

    }

}
$(document).on('change', '#ddlIIE', function (e) {
    e.preventDefault();
    // var IIEE = $('#ddlIIE option:selected').text();
    var IIE = $('#ddlIIE option:selected').val();
    var IA = IIE.split('/');
    var IIEE = IA[0];
    var IIE_BND = IIE.split('/');
    getAllPLOT(IIEE)
    // getAllSector(IIE_BND[0]);
    IIE_BND = IIE_BND.concat(IIE);
    //All Layer Create function Date 18/jan/2017
    IIE_RECNO.push(IIEE);
    getPlotcreate(IIEE);
    getroadcreate(IIEE);
    getutilitycreate(IIEE);
    getPublicPurposecreate(IIEE);
    getCommanareacreate(IIEE);
    getOthercreate(IIEE);
    getWaterheadtankcreate(IIEE);
    getparkingcreate(IIEE);
    getNalacreate(IIEE);
    getPoloicestationcreate(IIEE);
    getfirestationcreate(IIEE);
    getlabelcreate(IIEE);
    getvegcreate(IIEE);
    getbndcreate(IIEE);
    getsectorcreate(IIEE);
    getElinecreate(IIEE);
    getEPolecreate(IIEE);
    getWatercreate(IIEE);
    getRivercreate(IIEE);
    getlamppostcreate(IIEE);
    getRailwaycreate(IIEE);
    getmanholecreate(IIEE);
    getfootpathcreate(IIEE);
    getBridgecreate(IIEE);
    getencroachmentcreate(IIEE);
    getPlotcreate(IIEE);

    for (var x = 0; x < zoomIIE.length; x++) {
        zoomIIE[x].setMap(null);
    }
    var sl = createPolygon(getLatLongArr(IIE_BND[1]));
    sl.setOptions({ strokeWeight: 5, strokeColor: '#F0B27A', fillOpacity: 0, clickable: !1 });
    zoomIIE.push(sl);
    map.setZoom(15);
    map.panTo(new google.maps.LatLng(IIE_BND[3], IIE_BND[2]));
});

function getAllSector(IIE) {

    // var city = city_name;
    ajaxReq('Home', 'ALLSECTOR', { "location": IIE }, function (resp) { BindAllSECTOR(resp) }, true);

}
function BindAllSECTOR(resp) {
    $("#ddlSector").empty();
    $("#ddlPlot").empty();
    if (resp.status === true) {
        var Sector = $.map(resp.data, function (el) { return el; })

        // $('#ddlSector').append(new Option('--Select Sector--', ''));

        for (i = 0; i < Sector.length; i++) {
            if (i % 5 === 0) {
                $("#ddlSector").append(new Option(Sector[i + 1], Sector[i] + '/' + Sector[i + 2] + '/' + Sector[i + 3] + '/' + Sector[i + 4]));
            }
        }
        $('#ddlSector').change();
    }
    else {
        alert('Dear User,\n this IIE plot details are not mapped with GIS system.');
        $('#ddlSector').append(new Option('--No Data Found--', ''));

    }
}



//$(document).on('change', '#ddlSector', function (e) {
//    e.preventDefault();

//    var Sector = $('#ddlSector option:selected').val();
//    var IIE_Sector = Sector.split('/');
//    getAllPLOT(IIE_Sector[0]);
//});
function getAllPLOT(IIE) {


    ajaxReq('Home', 'ALLPLOT', { "location": IIE }, function (resp) { BindAllPLOT(resp) }, true);

}
function BindAllPLOT(resp) {
    $("#ddlPlot").empty();
    if (resp.status === true) {
        var ALLplot = $.map(resp.data, function (el) { return el; })

        $('#ddlPlot').append(new Option('--Select Plot--', ''));

        for (i = 0; i < ALLplot.length; i++) {
            if (i % 5 === 0) {
                $("#ddlPlot").append(new Option(ALLplot[i + 1], ALLplot[i] + '/' + ALLplot[i + 2] + '/' + ALLplot[i + 3] + '/' + ALLplot[i + 4]));
            }
        }

    }
}


$(document).on('change', '#ddlPlot', function (e) {
    e.preventDefault();
    var Plot = $('#ddlPlot option:selected').val();
    var PLOT_BND = Plot.split('/');
    PLOT_BND = PLOT_BND.concat(Plot);
    for (var x = 0; x < ZoomPlot.length; x++) {
        ZoomPlot[x].setMap(null);
    }
    var sl = createPolygon(getLatLongArr(PLOT_BND[1]));
    sl.setOptions({ strokeWeight: 5, strokeColor: '#007DC5', fillOpacity: 0, clickable: !1 });
    ZoomPlot.push(sl);
    map.setZoom(19);
    map.panTo(new google.maps.LatLng(PLOT_BND[3], PLOT_BND[2]));

});


function getBridgecreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getBridgecreate', { "location": city }, function (resp) { setBridgecreate(resp) }, true);
}

function setBridgecreate(resp) {
    ddlPlot
    if (resp.status === true) {
        if ($("#chkBridge").prop('checked') == true) {
            var BRG = $.map(resp.data, function (el) { return el; })


            for (i = 0; i < BRG.length; i++) {
                var ln = createPolygon(getLatLongArr(BRG[i]));
                ln.setOptions({ fillColor: '#85A6D3', strokeColor: '#6E6E6E', fillOpacity: 1 });
                maplayer.push(ln);
                //FOR LAYER'S ON/OFF
                BRG_LAYER.push(ln);

            }
        }
    }
}
$(document).on('change', '#chkBridge', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getBridgecreate(beach);

        }
    }
    else {
        for (i = 0; i < BRG_LAYER.length; i++) {
            BRG_LAYER[i].setMap(null);
        }

    }
});

function getfootpathcreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getfootpathcreate', { "location": city }, function (resp) { setftpcreate(resp) }, true);
}

function setftpcreate(resp) {
    if (resp.status === true) {
        if ($("#chkFootPath").prop('checked') == true) {
            var FTP = $.map(resp.data, function (el) { return el; })


            for (i = 0; i < FTP.length; i++) {
                var ln = createPolygon(getLatLongArr(FTP[i]));
                ln.setOptions({ fillColor: '#FFFFFF', strokeColor: '#6E6E6E', fillOpacity: 1 });
                maplayer.push(ln);
                //FOR LAYER'S ON/OFF
                FTP_LAYER.push(ln);

            }
        }
    }
}
$(document).on('change', '#chkFootPath', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getfootpathcreate(beach);

        }
    }
    else {
        for (i = 0; i < FTP_LAYER.length; i++) {
            FTP_LAYER[i].setMap(null);
        }

    }
});

function getgascreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getgascreate', { "location": city }, function (resp) { setgascreate(resp) }, true);
}
function setgascreate(resp) {
    if (resp.status === true) {
        if ($("#chkgas").prop('checked') == true) {
            var gas = $.map(resp.data, function (el) { return el; })


            //var manhole = data[city].MANHOLE;
            for (i = 0; i < gas.length; i++) {

                var pt = createPoint(getLatLongArr(gas[i])[0]);
                pt.setIcon('images/manhole.png');
                maplayer.push(pt);
                GAS_LAYER.push(pt);
            }
        }
    }
}
$(document).on('change', '#chkgas', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getgascreate(beach);

        }
    }
    else {
        for (i = 0; i < GAS_LAYER.length; i++) {
            GAS_LAYER[i].setMap(null);
        }

    }
});
function getmanholecreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getmanholecreate', { "location": city }, function (resp) { setmanholecreate(resp) }, true);
}

function setmanholecreate(resp) {
    if (resp.status === true) {
        if ($("#chkManHoles").prop('checked') == true) {
            var manhole = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < manhole.length; i++) {
                var ln = createPolygon(getLatLongArr(manhole[i]));
                ln.setOptions({ strokeWeight: 2, strokeColor: '#5B5BA2', fillOpacity: 0, clickable: !1 });
                maplayer.push(ln);
                MANHOLE_LAYER.push(ln);
                //FOR LAYER'S ON/OFF

                //var lp = new google.maps.LatLng(parseFloat(manhole[i][2]), parseFloat(manhole[i][1]));
                //var _label = new google.maps.Label({ visibleZoom: 13, hideZoom: 19, visible: true, map: map, cssName: 'sctrLbl', position: lp, text: SECTR[i][0] });

                //maplayer.push(_label);
            }
        }
    }
}
//function setmanholecreate(resp) {
//    if (resp.status === true) {
//        if ($("#chkManHoles").prop('checked') == true) {
//            var manhole = $.map(resp.data, function (el) { return el; })



//            for (i = 0; i < manhole.length; i++) {

//                var pt = createPoint(getLatLongArr(manhole[i])[0]);
//                pt.setIcon('images/manhole.png');
//                maplayer.push(pt);
//                MANHOLE_LAYER.push(pt);
//            }
//        }
//    }
//}
$(document).on('change', '#chkManHoles', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getmanholecreate(beach);

        }
    }
    else {
        for (i = 0; i < MANHOLE_LAYER.length; i++) {
            MANHOLE_LAYER[i].setMap(null);
        }

    }
});
function getRailwaycreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getRailwaycreate', { "location": city }, function (resp) { setRailwaycreate(resp) }, true);
}
function setRailwaycreate(resp) {
    if (resp.status === true) {
        if ($("#chkRailwayTrack").prop('checked') == true) {
            var RAilWay = $.map(resp.data, function (el) { return el; })

            // var RAilWay = data[city].RAILWAY;
            for (i = 0; i < RAilWay.length; i++) {
                var ln = createLine(getLatLongArr(RAilWay[i]));

                var lineSymbol = {
                    path: 'M 0,-1 0,1',
                    strokeOpacity: 1,
                    strokeColor: '#000000',
                    scale: 4
                };

                ln.setOptions({
                    strokeOpacity: 0,
                    icons: [{
                        icon: lineSymbol,
                        offset: '0',
                        repeat: '20px'
                    }]
                });
                maplayer.push(ln);
                RAILWAY_LAYER.push(ln);
            }
        }
    }
}
$(document).on('change', '#chkRailwayTrack', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getRailwaycreate(beach);

        }
    }
    else {
        for (i = 0; i < RAILWAY_LAYER.length; i++) {
            RAILWAY_LAYER[i].setMap(null);
        }

    }
});
function getlamppostcreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getlamppostcreate', { "location": city }, function (resp) { setlamppostcreate(resp) }, true);
}
function setlamppostcreate(resp) {
    if (resp.status === true) {
        if ($("#chkLampPost").prop('checked') == true) {
            var lamp_post = $.map(resp.data, function (el) { return el; })


            //var lamp_post = data[city].LAMPPOST;
            for (i = 0; i < lamp_post.length; i++) {
                var pt = createPoint(getLatLongArr(lamp_post[i])[0]);
                pt.setIcon('images/lamp.png');
                maplayer.push(pt);
                LAMPPOST_LAYER.push(pt);
            }
        }
    }
}
$(document).on('change', '#chkLampPost', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getlamppostcreate(beach);

        }
    }
    else {
        for (i = 0; i < LAMPPOST_LAYER.length; i++) {
            LAMPPOST_LAYER[i].setMap(null);
        }

    }
});
function getRivercreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getRivercreate', { "location": city }, function (resp) { setRivercreate(resp) }, true);
}
function setRivercreate(resp) {
    if (resp.status === true) {
        if ($("#chkWaterBody").prop('checked') == true) {
            var RIVER = $.map(resp.data, function (el) { return el; })


            // var RIVER = data[city].RIVER;
            for (i = 0; i < RIVER.length; i++) {
                var ln = createLine(getLatLongArr(RIVER[i]));
                //   ln.setOptions({ fillColor: '#B2D1EA', strokeColor: '#B2D1EA', fillOpacity: 1 });
                ln.setOptions({ strokeWeight: 3, strokeColor: '#B2D1EA' });
                maplayer.push(ln);
                //FOR LAYER'S ON/OFF
                RIVER_LAYER.push(ln);

                Polys.push(ln);
            }
        }
    }
}
$(document).on('change', '#chkWaterBody', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getRivercreate(beach);

        }
    }
    else {
        for (i = 0; i < RIVER_LAYER.length; i++) {
            RIVER_LAYER[i].setMap(null);
        }

    }
});
function getWatercreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getWatercreate', { "location": city }, function (resp) { setWatercreate(resp) }, true);
}
function setWatercreate(resp) {
    if (resp.status === true) {
        if ($("#chkWaterSupplyLine").prop('checked') == true) {
            var WTR = $.map(resp.data, function (el) { return el; })


            for (i = 0; i < WTR.length; i++) {
                var ln = createPolygon(getLatLongArr(WTR[i]));
                ln.setOptions({ strokeWeight: 3, strokeColor: '#8BBDEB' });
                maplayer.push(ln);
                WTR_LAYER.push(ln);
            }
        }
    }
}
$(document).on('change', '#chkWaterSupplyLine', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getWatercreate(beach);

        }
    }
    else {
        for (i = 0; i < WTR_LAYER.length; i++) {
            WTR_LAYER[i].setMap(null);
        }

    }
});
$(document).on('click', '#btnClear', function (e) {
    e.preventDefault();

    //ClearAllResults();

    //map.setZoom(7);

    //map.panTo(new google.maps.LatLng(26.846709, 80.946159));
    GetCurrentDetails("State", "4");
    window.location.href = 'default.aspx';
});
function ClearAllResults() {
    directionsDisplay.setMap(null);
    for (i = 0; i < SerachResult.length; i++) {
        SerachResult[i].setMap(null);
    }
    for (var x = 0; x < zoomIIE.length; x++) {
        zoomIIE[x].setMap(null);
    }
    for (var x = 0; x < zoomDistrict.length; x++) {
        zoomDistrict[x].setMap(null);
    }
    for (i = 0; i < SIIDCUL_POLLUTION_MARKERS.length; i++) {
        SIIDCUL_POLLUTION_MARKERS[i].setMap(null);
    }
    for (i = 0; i < CATEGORY_MARKERS.length; i++) {
        CATEGORY_MARKERS[i].setMap(null);
    }
    for (i = 0; i < IIE_MARKERS.length; i++) {
        IIE_MARKERS[i].setMap(null);
    }
    setDistrictMarker(map);
    setAirportmarker(map);
    //setSIDCMarkers(map);
    //setTextileMarkers(map);
    //setReservedMarker(map);
}
function getEPolecreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getEPolecreate', { "location": city }, function (resp) { setEPolecreate(resp) }, true);
}

function setEPolecreate(resp) {
    if (resp.status === true) {
        if ($("#chkElectricityPole").prop('checked') == true) {
            var e_pole = $.map(resp.data, function (el) { return el; })

            // var e_pole = data[city].EPOLE;
            for (i = 0; i < e_pole.length; i++) {
                var pt = createPoint(getLatLongArr(e_pole[i])[0]);
                var imgg = {
                    url: 'images/pole.png',
                    size: new google.maps.Size(12, 12),
                    origin: new google.maps.Point(0, 0),
                    anchor: new google.maps.Point(6, 4),
                    scaledSize: new google.maps.Size(12, 12)
                };
                pt.setIcon(imgg);
                maplayer.push(pt);
                EPOLE_LAYER.push(pt);
            }
        }
    }
}
$(document).on('change', '#chkElectricityPole', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getEPolecreate(beach);

        }
    }
    else {
        for (i = 0; i < EPOLE_LAYER.length; i++) {
            EPOLE_LAYER[i].setMap(null);
        }

    }
});
function getElinecreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getElinecreate', { "location": city }, function (resp) { setElinecreate(resp) }, true);
}
function setElinecreate(resp) {
    if (resp.status === true) {
        if ($("#chkElectricityLine").prop('checked') == true) {
            var e_line = $.map(resp.data, function (el) { return el; })
            for (i = 0; i < e_line.length; i++) {
                var ln = createLine(getLatLongArr(e_line[i]));

                var lineSymbol = {
                    path: 'M 0,-2 0,2',
                    strokeOpacity: 1,
                    scale: 2
                };

                ln.setOptions({
                    strokeOpacity: 0,
                    icons: [{
                        icon: lineSymbol,
                        offset: '0',
                        repeat: '20px'
                    }]
                });
                maplayer.push(ln);
                ELINE_LAYER.push(ln);
            }
        }
    }
}
$(document).on('change', '#chkElectricityLine', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getElinecreate(beach);

        }
    }
    else {
        for (i = 0; i < ELINE_LAYER.length; i++) {
            ELINE_LAYER[i].setMap(null);
        }

    }
});
function getsectorcreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getsectorcreate', { "location": city }, function (resp) { setsectorcreate(resp) }, true);
}
function setsectorcreate(resp) {
    if (resp.status === true) {
        if ($("#chkSector").prop('checked') == true) {
            var SECTR = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < SECTR.length; i++) {
                var ln = createPolygon(getLatLongArr(SECTR[i][3]));
                ln.setOptions({ strokeWeight: 2, strokeColor: '#393939', fillOpacity: 0, clickable: !1 });
                maplayer.push(ln);
                SECT_LAYER.push(ln);
                //FOR LAYER'S ON/OFF

                var lp = new google.maps.LatLng(parseFloat(SECTR[i][2]), parseFloat(SECTR[i][1]));
                var _label = new google.maps.Label({ visibleZoom: 13, hideZoom: 19, visible: true, map: map, cssName: 'sctrLbl', position: lp, text: SECTR[i][0] });

                maplayer.push(_label);
            }
        }
    }
}
$(document).on('change', '#chkSector', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getsectorcreate(beach);

        }
    }
    else {
        for (i = 0; i < SECT_LAYER.length; i++) {
            SECT_LAYER[i].setMap(null);
        }

    }
});
function getbndcreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getbndcreate', { "location": city }, function (resp) { setbndcreate(resp) }, true);
}
function setbndcreate(resp) {
    if (resp.status === true) {
        if ($("#chkBoundary").prop('checked') == true) {
            var BND = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < BND.length; i++) {
                var ln = createLine(getLatLongArr(BND[i]));
                ln.setOptions({ fillcolor: ' #000000', strokeWeight: 1, strokeColor: ' #000000' });
                maplayer.push(ln);
                BND_LAYER.push(ln);
            }
        }
    }
}

$(document).on('change', '#chkBoundary', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getbndcreate(beach);

        }
    }
    else {
        for (i = 0; i < BND_LAYER.length; i++) {
            BND_LAYER[i].setMap(null);
        }

    }
});
function getvegcreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getvegcreate', { "location": city }, function (resp) { setvegcreate(resp) }, true);
}

function setvegcreate(resp) {
    if (resp.status === true) {
        if ($("#chkvegitation").prop('checked') == true) {
            var VEG = $.map(resp.data, function (el) { return el; })
            for (i = 0; i < VEG.length; i++) {
                var ln = createPolygon(getLatLongArr(VEG[i]));
                ln.setOptions({ fillColor: '#55E82D', strokeColor: '#3AA03A', fillOpacity: 1 });
                maplayer.push(ln);
                VEG_LAYER.push(ln);

                Polys.push(ln);
            }
        }
    }
}
$(document).on('change', '#chkvegitation', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getvegcreate(beach);

        }
    }
    else {
        for (i = 0; i < VEG_LAYER.length; i++) {
            VEG_LAYER[i].setMap(null);
        }

    }
});
function getlabelcreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getlabelcreate', { "location": city }, function (resp) { setlabelcreate(resp) }, true);
}

function setlabelcreate(resp) {
    if (resp.status === true) {
        var LBLS = $.map(resp.data, function (el) { return el; })


        for (i = 0; i < LBLS.length; i++) {
            var lp = new google.maps.LatLng(parseFloat(LBLS[i][2]), parseFloat(LBLS[i][1]));
            var _label = new google.maps.Label({ visibleZoom: 16, hideZoom: 60, visible: true, map: map, cssName: 'nlLabel', position: lp, text: LBLS[i][0] });
            maplayer.push(_label);
        }
    }
}

function getfirestationcreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getfirestationcreate', { "location": city }, function (resp) { setfirestation(resp) }, true);
}

function setfirestation(resp) {
    if (resp.status === true) {
        if ($("#chkfire").prop('checked') == true) {
            var misc = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < misc.length; i++) {
                var ln = createPolygon(getLatLongArr(misc[i]));
                ln.id = misc[i].toString();
                ln.setOptions({ fillColor: '#cc0000', strokeColor: '#666666', fillOpacity: 0.8, clickable: !0 });
                maplayer.push(ln);
                FIRE_STATION_LAYER.push(ln);

                Polys.push(ln);
            }
        }
    }
}

$(document).on('change', '#chkfire', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getfirestationcreate(beach);

        }
    }
    else {
        for (i = 0; i < FIRE_STATION_LAYER.length; i++) {
            FIRE_STATION_LAYER[i].setMap(null);
        }

    }
});
function getencroachmentcreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getencroachmentcreate', { "location": city }, function (resp) { setencroachment(resp) }, true);
}

function setencroachment(resp) {
    if (resp.status === true) {
        if ($("#checkEncroachment").prop('checked') == true) {
            var misc = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < misc.length; i++) {
                var ln = createPolygon(getLatLongArr(misc[i]));
                ln.id = misc[i].toString();
                ln.setOptions({ fillColor: '#999966', strokeColor: '#999966', fillOpacity: 0.8, clickable: !0 });
                maplayer.push(ln);
                ENCROACHMENT_LAYER.push(ln);
            }
        }
    }
}

$(document).on('change', '#checkEncroachment', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getencroachmentcreate(beach);

        }
    }
    else {
        for (i = 0; i < ENCROACHMENT_LAYER.length; i++) {
            ENCROACHMENT_LAYER[i].setMap(null);
        }

    }
});
function getPoloicestationcreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getPoloicestationcreate', { "location": city }, function (resp) { setPolicestation(resp) }, true);
}

function setPolicestation(resp) {
    if (resp.status === true) {
        if ($("#chkpolice").prop('checked') == true) {
            var misc = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < misc.length; i++) {
                var ln = createPolygon(getLatLongArr(misc[i]));
                ln.id = misc[i].toString();
                ln.setOptions({ fillColor: '#F76085', strokeColor: '#666666', fillOpacity: 0.8, clickable: !0 });
                maplayer.push(ln);
                POLICE_STATION_LAYER.push(ln);

                Polys.push(ln);
            }
        }
    }
}

$(document).on('change', '#chkpolice', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getPoloicestationcreate(beach);

        }
    }
    else {
        for (i = 0; i < POLICE_STATION_LAYER.length; i++) {
            POLICE_STATION_LAYER[i].setMap(null);
        }

    }
});
function getNalacreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getNalacreate', { "location": city }, function (resp) { setNala(resp) }, true);
}

function setNala(resp) {
    if (resp.status === true) {
        if ($("#chknala").prop('checked') == true) {
            var misc = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < misc.length; i++) {
                var ln = createPolygon(getLatLongArr(misc[i]));
                ln.id = misc[i].toString();
                ln.setOptions({ fillColor: '#05f2ff', strokeColor: '#666666', fillOpacity: 0.8, clickable: !0 });
                maplayer.push(ln);
                NALA_LAYER.push(ln);

                Polys.push(ln);
            }
        }
    }
}

$(document).on('change', '#chknala', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getNalacreate(beach);

        }
    }
    else {
        for (i = 0; i < NALA_LAYER.length; i++) {
            NALA_LAYER[i].setMap(null);
        }

    }
});
function getparkingcreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getparkingcreate', { "location": city }, function (resp) { setparking(resp) }, true);
}

function setparking(resp) {
    if (resp.status === true) {
        if ($("#chkparking").prop('checked') == true) {
            var misc = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < misc.length; i++) {
                var ln = createPolygon(getLatLongArr(misc[i]));
                ln.id = misc[i].toString();
                ln.setOptions({ fillColor: '#99ffcc', strokeColor: '#666666', fillOpacity: 0.8, clickable: !0 });
                maplayer.push(ln);
                PARKING_LAYER.push(ln);

                Polys.push(ln);
            }
        }
    }
}

$(document).on('change', '#chkparking', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getparkingcreate(beach);

        }
    }
    else {
        for (i = 0; i < PARKING_LAYER.length; i++) {
            PARKING_LAYER[i].setMap(null);
        }

    }
});

function getWaterheadtankcreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getWaterheadtankcreate', { "location": city }, function (resp) { setWaterheadtank(resp) }, true);
}

function setWaterheadtank(resp) {
    if (resp.status === true) {
        if ($("#chkwatertank").prop('checked') == true) {
            var misc = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < misc.length; i++) {
                var ln = createPolygon(getLatLongArr(misc[i]));
                ln.id = misc[i].toString();
                ln.setOptions({ fillColor: '#b3b3ff', strokeColor: '#666666', fillOpacity: 0.8, clickable: !0 });
                maplayer.push(ln);
                WATER_HEAD_TANK.push(ln);

                Polys.push(ln);
            }
        }
    }
}

$(document).on('change', '#chkwatertank', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getWaterheadtankcreate(beach);

        }
    }
    else {
        for (i = 0; i < WATER_HEAD_TANK.length; i++) {
            WATER_HEAD_TANK[i].setMap(null);
        }

    }
});
function getOthercreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getOthercreate', { "location": city }, function (resp) { setOtherarea(resp) }, true);
}

function setOtherarea(resp) {
    if (resp.status === true) {
        if ($("#chkother").prop('checked') == true) {
            var misc = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < misc.length; i++) {
                var ln = createPolygon(getLatLongArr(misc[i]));
                ln.id = misc[i].toString();
                ln.setOptions({ fillColor: '#ffc34d', strokeColor: '#666666', fillOpacity: 0.8, clickable: !0 });
                maplayer.push(ln);
                OTHERS_LAYER.push(ln);

                Polys.push(ln);
            }
        }
    }
}

$(document).on('change', '#chkother', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getOthercreate(beach);

        }
    }
    else {
        for (i = 0; i < OTHERS_LAYER.length; i++) {
            OTHERS_LAYER[i].setMap(null);
        }

    }
});
function getCommanareacreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getCommanareacreate', { "location": city }, function (resp) { setCommanarea(resp) }, true);
}

function setCommanarea(resp) {
    if (resp.status === true) {
        if ($("#chkcomman").prop('checked') == true) {
            var misc = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < misc.length; i++) {
                var ln = createPolygon(getLatLongArr(misc[i]));
                ln.id = misc[i].toString();
                ln.setOptions({ fillColor: '#cc00ff', strokeColor: '#666666', fillOpacity: 0.8, clickable: !0 });
                maplayer.push(ln);
                COMMON_LAYER.push(ln);

                Polys.push(ln);
            }
        }
    }
}

$(document).on('change', '#chkcomman', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getCommanareacreate(beach);

        }
    }
    else {
        for (i = 0; i < COMMON_LAYER.length; i++) {
            COMMON_LAYER[i].setMap(null);
        }

    }
});
function getPublicPurposecreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getPublicPurposecreate', { "location": city }, function (resp) { setPublicPurpose(resp) }, true);
}

function setPublicPurpose(resp) {
    if (resp.status === true) {
        if ($("#chkpublicpurpose").prop('checked') == true) {
            var misc = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < misc.length; i++) {
                var ln = createPolygon(getLatLongArr(misc[i]));
                ln.id = misc[i].toString();
                ln.setOptions({ fillColor: '#b3b300"', strokeColor: '#666666', fillOpacity: 0.8, clickable: !0 });
                maplayer.push(ln);
                PUBLIC_PURPOSE_LAYER.push(ln);

                Polys.push(ln);
            }
        }
    }
}

$(document).on('change', '#chkpublicpurpose', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getPublicPurposecreate(beach);

        }
    }
    else {
        for (i = 0; i < PUBLIC_PURPOSE_LAYER.length; i++) {
            PUBLIC_PURPOSE_LAYER[i].setMap(null);
        }

    }
});

function getutilitycreate(city_name) {

    var city = city_name;
    ajaxReq('Home', 'getutilitycreate', { "location": city }, function (resp) { setutility(resp) }, true);
}

function setutility(resp) {
    if (resp.status === true) {
        if ($("#chkutitlity").prop('checked') == true) {
            var misc = $.map(resp.data, function (el) { return el; })

            for (i = 0; i < misc.length; i++) {
                var ln = createPolygon(getLatLongArr(misc[i]));
                ln.id = misc[i].toString();
                ln.setOptions({ fillColor: '#AF7AC5', strokeColor: '#666666', fillOpacity: 0.8, clickable: !0 });
                maplayer.push(ln);
                MISC_LAYER.push(ln);
                Polys.push(ln);
            }
        }
    }
}

$(document).on('change', '#chkutitlity', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getutilitycreate(beach);

        }
    }
    else {
        for (i = 0; i < MISC_LAYER.length; i++) {
            MISC_LAYER[i].setMap(null);
        }

    }
});
function getroadcreate(city_name) {

    var city = city_name;

    ajaxReq('Home', 'getroadcreate', { "location": city }, function (resp) { setroad(resp) }, true);
}
function setroad(resp) {
    if (resp.status === true) {
        if ($("#chkRoad").prop('checked') == true) {
            var RDS = $.map(resp.data, function (el) { return el; })
            for (i = 0; i < RDS.length; i++) {
                var ln = createPolygon(getLatLongArr(RDS[i]));
                ln.setOptions({ fillColor: '#a0a2a5', strokeColor: '#a0a2a5', fillOpacity: 1 });
                maplayer.push(ln);
                RDS_LAYER.push(ln);

            }
        }
    }
}
$(document).on('change', '#chkRoad', function (e) {
    e.preventDefault();
    if (this.checked) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getroadcreate(beach);

        }
    }
    else {
        for (i = 0; i < RDS_LAYER.length; i++) {
            RDS_LAYER[i].setMap(null);
        }

    }
});





function getPlotcreate(city_name) {
    GetCurrentDetails("IE", city_name);
    var city = city_name;
    ajaxReq('Home', 'getPlotcreate', { "location": city }, function (resp) { setPlotcreate(resp, city_name) }, true);
}

var Polys = [];

function setPlotcreate(resp, city_name) {
    if (resp.status === true) {
        //debugger;
        var plots = $.map(resp.data, function (el) { return el; })
        var clr;
        var typeClr;

        $('#OpacityRange').val(0.5);
        $('#demo').html(50);

        for (i = 0; i < plots.length; i++) {
            var poly = createPolygon(getLatLongArr(plots[i][4]));
            poly.plotno = plots[i][0];
            poly.area = plots[i][1];
            poly.sector = plots[i][5] || 1;
            poly.Status = plots[i][6];
            poly.Ind_area_name = plots[i][7];
            poly.Districtname = plots[i][8];
            poly.plottype = plots[i][9];
            poly.SectorName = plots[i][10];
            poly.iierecno = city_name;

            if ($('#chkCheckAllPlotStatus').attr('checked')) {
                clr = getColor(poly.plotno, poly.sector, city_name, poly.Status);
                poly.setOptions({ fillColor: clr, clickable: !0, fillOpacity: 0.5 });
            }
            else {
                typeClr = getTypePlotColor(poly.plotno, poly.sector, city_name, poly.plottype);
                poly.setOptions({ fillColor: typeClr, clickable: !0 });
            }

            Polys.push(poly);

            google.maps.event.addListener(poly, 'mouseover', function () {
                this.setOptions({ strokeWeight: 4 });
            });
            google.maps.event.addListener(poly, 'mouseout', function () {
                this.setOptions({ strokeWeight: 1 });
            });
            google.maps.event.addListener(map, 'mousemove', function (event) {
                displayCoordinates(event.latLng);
            });
            google.maps.event.addListener(poly, 'mousemove', function (event) {
                displayCoordinates(event.latLng);
            });

            if (clr != '#CCCC99') {
                google.maps.event.addListener(poly, 'click', function () {
                    getInfo(this.plotno, this.area, this.sector, this.iierecno, this.SectorName, this.Ind_area_name, this.Districtname);
                });
            }

            if ($('#chkCheckAllPlotStatus').attr('checked')) {
                //for Filter added on 13/5/2016 
                //For Available for AllotmenT
                if (clr == '#E222AB') {
                    UN_ALLOTEDPLOT_LAYER.push(poly);
                    UN_ALLOTED_PLOTS.push(plots[i]);
                }
                //nOT Available for Allotmen
                else if (clr == '#FC033') {
                    NOTAVAILABLESPLOT_LAYER.push(poly);
                    NOTAVAILABLES_PLOT.push(plots[i]);

                }
                //for Vacant
                else if (clr == '#000080') {
                    VACANTPLOT_LAYER.push(poly);
                    VACANT_PLOTS.push(plots[i]);

                }
                //for Under Construction// freehold #3498DB
                else if (clr == '#3498DB') {
                    UNDERCONSPLOT_LAYER.push(poly);
                    UNDERCONS_PLOTS.push(plots[i]);
                }
                //for Under Production // Sold In Last Auction
                else if (clr == '#ECE92B') {
                    INPRODUCTIONPLOT_LAYER.push(poly);
                    INPRODUCTION_PLOTS.push(plots[i]);
                }
                //for Production Stop// Cancel
                else if (clr == '#FFA07A') {
                    CLOSEDPLOT_LAYER.push(poly);
                    CLOSED_PLOTS.push(plots[i]);
                }
                //for Sick
                else if (clr == '#927503') {
                    SICKPLOT_LAYER.push(poly);
                    SICK_PLOTS.push(plots[i]);
                }
                //for No Status
                else if (clr == '#CCCC99') {
                    NOSTATUSPLOT_LAYER.push(poly);
                    NOSTATUS_PLOTS.push(plots[i]);
                }
                //
            }
            else {
                //for land use filter added by sachin bhatt 5july2017
                //Industrial
                if (typeClr == '#6C6C6C') {
                    Industrial_LAYER.push(poly);
                    Industrial_PLOTS.push(plots[i]);
                }
                //for Commercial//  Commercial Estate
                else if (typeClr == '#008C89') {
                    Commercial_LAYER.push(poly);
                    Commercial_PLOTS.push(plots[i]);
                }
                else if (typeClr == '#400040') {
                    Commercial_LAYER_for_Land.push(poly);
                    Commercial_LAND.push(plots[i]);
                }
                //for Residential
                else if (typeClr == '#D4CF17') {
                    Residential_LAYER.push(poly);
                    Commercial_PLOTS.push(plots[i]);
                }
                //for In Institutional_Banking_
                else if (typeClr == '#0000cc') {
                    Institutional_Banking_LAYER.push(poly);
                    Institutional_Banking_PLOTS.push(plots[i]);
                }
                //for Emergency Services
                else if (typeClr == '#5252FF') {
                    Emergency_LAYER.push(poly);
                    Emergency_PLOTS.push(plots[i]);
                }
                else if (typeClr == '#A7855F') {
                    Land_Sale_Branch_LAYER.push(poly);
                    Land_Sale_Branch_PLOTS.push(plots[i]);
                }
                else if (typeClr == '#6A903E') {
                    Land_Administrative_Block_LAYER.push(poly);
                    Land_Administrative_Block_PLOTS.push(plots[i]);
                }
                //for No Status
                else if (typeClr == '#CCCC99') {
                    NOSTATUSPLOT_LAYER.push(poly);
                    NOSTATUS_PLOTS.push(plots[i]);
                }
                //
                //
            }

            maplayer.push(poly);
            //  if ($("#chkPlotNo").prop('checked') == true) {
            var lp = new google.maps.LatLng(parseFloat(plots[i][3]), parseFloat(plots[i][2]));

            if (poly.area >= 10000) {
                var _label = new google.maps.Label({
                    visibleZoom: 12, hideZoom: 90, visible: true, map: map, cssName: 'nlLabel', position: lp, text: plots[i][0]
                });
            }
            else if (poly.area >= 3000 && poly.area < 10000) {
                var _label = new google.maps.Label({
                    visibleZoom: 13, hideZoom: 90, visible: true, map: map, cssName: 'nlLabel', position: lp, text: plots[i][0]
                });
            }
            else if (poly.area >= 1000 && poly.area < 3000) {
                var _label = new google.maps.Label({
                    visibleZoom: 16, hideZoom: 90, visible: true, map: map, cssName: 'nlLabel', position: lp, text: plots[i][0]
                });
            }
            else if (poly.area >= 300 && poly.area < 1000) {
                var _label = new google.maps.Label({
                    visibleZoom: 16, hideZoom: 90, visible: true, map: map, cssName: 'nlLabel', position: lp, text: plots[i][0]
                });
            }
            else {
                var _label = new google.maps.Label({
                    visibleZoom: 16, hideZoom: 90, visible: true, map: map, cssName: 'nlLabel', position: lp, text: plots[i][0]
                });
            }

            PLOT_NUMBER.push(_label);
            maplayer.push(_label);
            // }
        }
    }
}

$(document).on('change', '#chkCheckAllPlotStatus', function (e) {
    e.preventDefault();

    $(this).closest('table').find('td input[type="checkbox"]').prop('checked', $(this).prop('checked'));
    if (this.checked) {
        $('#chkCheckAllPlotStatus').attr("checked", "checked");;
        $('#chlAllLandUser').prop('checked', false);
        $('#chlAllLandUser').removeAttr('checked');
        $('#chlAllLandUser').closest('table').find('td input[type="checkbox"]').prop('checked', false);
        $('#chlAllLandUser').closest('table').find('td input[type="checkbox"]').removeAttr('checked');
        showLegend();
        clearLandUse();
        var iierecno = $.unique(IIE_RECNO);

        for (i = 0; i < iierecno.length; i++) {
            getPlotcreate(iierecno[i]);
        }

    }

    else {

        for (i = 0; i < UN_ALLOTEDPLOT_LAYER.length; i++) {
            UN_ALLOTEDPLOT_LAYER[i].setMap(null);
        }

        for (i = 0; i < CLOSEDPLOT_LAYER.length; i++) {
            CLOSEDPLOT_LAYER[i].setMap(null);
        }

        for (i = 0; i < SICKPLOT_LAYER.length; i++) {
            SICKPLOT_LAYER[i].setMap(null);
        }

        for (i = 0; i < VACANTPLOT_LAYER.length; i++) {
            VACANTPLOT_LAYER[i].setMap(null);
        }


        for (i = 0; i < ALLOTEDPLOT_LAYER.length; i++) {
            ALLOTEDPLOT_LAYER[i].setMap(null);
        }


        for (i = 0; i < UNDERCONSPLOT_LAYER.length; i++) {
            UNDERCONSPLOT_LAYER[i].setMap(null);
        }


        for (i = 0; i < INPRODUCTIONPLOT_LAYER.length; i++) {
            INPRODUCTIONPLOT_LAYER[i].setMap(null);
        }


        for (i = 0; i < NOSTATUSPLOT_LAYER.length; i++) {
            NOSTATUSPLOT_LAYER[i].setMap(null);
        }
    }
});

$(document).on('change', '#chkUnalloted', function (e) {
    e.preventDefault();
    if (this.checked) {

        //for (i = 0; i < CITY_NAME.length; i++) {
        //    var beach = CITY_NAME[i];
        //    getPlotcreate(beach);
        for (i = 0; i < UN_ALLOTEDPLOT_LAYER.length; i++) {
            UN_ALLOTEDPLOT_LAYER[i].setMap(map);

        }
    }
    else {
        for (i = 0; i < UN_ALLOTEDPLOT_LAYER.length; i++) {
            UN_ALLOTEDPLOT_LAYER[i].setMap(null);
        }

    }
});
$(document).on('change', '#chkVacant', function (e) {
    e.preventDefault();
    if (this.checked) {
        for (i = 0; i < VACANTPLOT_LAYER.length; i++) {
            VACANTPLOT_LAYER[i].setMap(map);

        }
    }
    else {
        for (i = 0; i < VACANTPLOT_LAYER.length; i++) {
            VACANTPLOT_LAYER[i].setMap(null);
        }

    }
});
$(document).on('change', '#chkUnderConstruction', function (e) {
    e.preventDefault();
    if (this.checked) {
        for (i = 0; i < UNDERCONSPLOT_LAYER.length; i++) {
            UNDERCONSPLOT_LAYER[i].setMap(map);
        }
    }
    else {
        for (i = 0; i < UNDERCONSPLOT_LAYER.length; i++) {
            UNDERCONSPLOT_LAYER[i].setMap(null);
        }

    }
});
$(document).on('change', '#chkInProduction', function (e) {
    e.preventDefault();
    if (this.checked) {
        for (i = 0; i < INPRODUCTIONPLOT_LAYER.length; i++) {
            INPRODUCTIONPLOT_LAYER[i].setMap(map);
        }
    }
    else {
        for (i = 0; i < INPRODUCTIONPLOT_LAYER.length; i++) {
            INPRODUCTIONPLOT_LAYER[i].setMap(null);
        }

    }
});
$(document).on('change', '#chkclosed', function (e) {
    e.preventDefault();
    if (this.checked) {
        for (i = 0; i < CLOSEDPLOT_LAYER.length; i++) {
            CLOSEDPLOT_LAYER[i].setMap(map);
        }
    }
    else {
        for (i = 0; i < CLOSEDPLOT_LAYER.length; i++) {
            CLOSEDPLOT_LAYER[i].setMap(null);
        }

    }
});
$(document).on('change', '#chksick', function (e) {
    e.preventDefault();
    if (this.checked) {
        for (i = 0; i < SICKPLOT_LAYER.length; i++) {
            SICKPLOT_LAYER[i].setMap(map);
        }
    }
    else {
        for (i = 0; i < SICKPLOT_LAYER.length; i++) {
            SICKPLOT_LAYER[i].setMap(null);
        }

    }
});
$(document).on('change', '#chkNoStatus', function (e) {
    e.preventDefault();
    if (this.checked) {
        for (i = 0; i < NOSTATUSPLOT_LAYER.length; i++) {
            NOSTATUSPLOT_LAYER[i].setMap(map);

        }
    }
    else {
        for (i = 0; i < NOSTATUSPLOT_LAYER.length; i++) {
            NOSTATUSPLOT_LAYER[i].setMap(null);
        }

    }
});


//Changes by sachin 5 july

$(document).on('change', '#chlAllLandUser', function (e) {
    e.preventDefault();

    $(this).closest('table').find('td input[type="checkbox"]').prop('checked', $(this).prop('checked'));
    if (this.checked) {
        clearplotStatus()
        $('#chkCheckAllPlotStatus').prop('checked', false);
        $('#chkCheckAllPlotStatus').removeAttr('checked');
        $('#chkCheckAllPlotStatus').closest('table').find('td input[type="checkbox"]').prop('checked', false);
        $('#chkCheckAllPlotStatus').closest('table').find('td input[type="checkbox"]').removeAttr('checked');
        showLegend();
        var iierecno = $.unique(IIE_RECNO);

        for (i = 0; i < UN_ALLOTEDPLOT_LAYER.length; i++) {
            UN_ALLOTEDPLOT_LAYER[i].setMap(null);
        }

        for (i = 0; i < CLOSEDPLOT_LAYER.length; i++) {
            CLOSEDPLOT_LAYER[i].setMap(null);
        }

        for (i = 0; i < SICKPLOT_LAYER.length; i++) {
            SICKPLOT_LAYER[i].setMap(null);
        }

        for (i = 0; i < VACANTPLOT_LAYER.length; i++) {
            VACANTPLOT_LAYER[i].setMap(null);
        }


        for (i = 0; i < ALLOTEDPLOT_LAYER.length; i++) {
            ALLOTEDPLOT_LAYER[i].setMap(null);
        }


        for (i = 0; i < UNDERCONSPLOT_LAYER.length; i++) {
            UNDERCONSPLOT_LAYER[i].setMap(null);
        }


        for (i = 0; i < INPRODUCTIONPLOT_LAYER.length; i++) {
            INPRODUCTIONPLOT_LAYER[i].setMap(null);
        }


        for (i = 0; i < NOSTATUSPLOT_LAYER.length; i++) {
            NOSTATUSPLOT_LAYER[i].setMap(null);
        }




        for (i = 0; i < iierecno.length; i++) {
            getPlotcreate(iierecno[i]);
        }

        //for (i = 0; i < Industrial_LAYER.length; i++) {

        //    Industrial_LAYER[i].setMap(map);
        //}

        //for (i = 0; i < Commercial_LAYER.length; i++) {

        //    Commercial_LAYER[i].setMap(map);

        //}

        //for (i = 0; i < Residential_LAYER.length; i++) {

        //    Residential_LAYER[i].setMap(map);
        //}

        //for (i = 0; i < Institutional_Banking_LAYER.length; i++) {

        //    Institutional_Banking_LAYER[i].setMap(map);
        //}
    }

    else {

        for (i = 0; i < Industrial_LAYER.length; i++) {
            Industrial_LAYER[i].setMap(null);
        }


        for (i = 0; i < Commercial_LAYER.length; i++) {
            Commercial_LAYER[i].setMap(null);
        }


        for (i = 0; i < Residential_LAYER.length; i++) {
            Residential_LAYER[i].setMap(null);
        }


        for (i = 0; i < Institutional_Banking_LAYER.length; i++) {
            Institutional_Banking_LAYER[i].setMap(null);
        }

    }
});
$(document).on('change', '#chkIndustrialPlot', function (e) {
    e.preventDefault();
    if (this.checked) {
        for (i = 0; i < Industrial_LAYER.length; i++) {
            Industrial_LAYER[i].setMap(map);

        }
    }
    else {
        for (i = 0; i < Industrial_LAYER.length; i++) {
            Industrial_LAYER[i].setMap(null);
        }

    }
});
$(document).on('change', '#chkCommercialPlot', function (e) {
    e.preventDefault();
    if (this.checked) {
        for (i = 0; i < Commercial_LAYER.length; i++) {
            Commercial_LAYER[i].setMap(map);

        }
    }
    else {
        for (i = 0; i < Commercial_LAYER.length; i++) {
            Commercial_LAYER[i].setMap(null);
        }

    }
});
$(document).on('change', '#chkResidentialplot', function (e) {
    e.preventDefault();
    if (this.checked) {
        for (i = 0; i < Residential_LAYER.length; i++) {
            Residential_LAYER[i].setMap(map);

        }
    }
    else {
        for (i = 0; i < Residential_LAYER.length; i++) {
            Residential_LAYER[i].setMap(null);
        }

    }
});
$(document).on('change', '#chkInstitutionalplot', function (e) {
    e.preventDefault();
    if (this.checked) {
        for (i = 0; i < Institutional_Banking_LAYER.length; i++) {
            INPRODUCTIONPLOT_LAYER[i].setMap(map);
        }
    }
    else {
        for (i = 0; i < Institutional_Banking_LAYER.length; i++) {
            Institutional_Banking_LAYER[i].setMap(null);
        }

    }
});
$(document).on('change', '#chklandUseNostatus', function (e) {
    e.preventDefault();
    if (this.checked) {
        for (i = 0; i < NOSTATUSPLOT_LAYER.length; i++) {
            NOSTATUSPLOT_LAYER[i].setMap(map);

        }
    }
    else {
        for (i = 0; i < NOSTATUSPLOT_LAYER.length; i++) {
            NOSTATUSPLOT_LAYER[i].setMap(null);
        }

    }
});
function clearplotStatus() {
    for (i = 0; i < VACANTPLOT_LAYER.length; i++) {
        VACANTPLOT_LAYER[i].setMap(null);
    }
    for (i = 0; i < UN_ALLOTEDPLOT_LAYER.length; i++) {
        UN_ALLOTEDPLOT_LAYER[i].setMap(null);
    }
    for (i = 0; i < UNDERCONSPLOT_LAYER.length; i++) {
        UNDERCONSPLOT_LAYER[i].setMap(null);
    }
    for (i = 0; i < INPRODUCTIONPLOT_LAYER.length; i++) {
        INPRODUCTIONPLOT_LAYER[i].setMap(null);
    }
}
function clearLandUse() {
    for (i = 0; i < Industrial_LAYER.length; i++) {
        Industrial_LAYER[i].setMap(null);
    }
    for (i = 0; i < Commercial_LAYER.length; i++) {
        Commercial_LAYER[i].setMap(null);
    }
    for (i = 0; i < Residential_LAYER.length; i++) {
        Residential_LAYER[i].setMap(null);
    }
    for (i = 0; i < Institutional_Banking_LAYER.length; i++) {
        Institutional_Banking_LAYER[i].setMap(null);
    }
    for (i = 0; i < Commercial_LAYER_for_Land.length; i++) {
        Commercial_LAYER_for_Land[i].setMap(null);
    }
    for (i = 0; i < Emergency_LAYER.length; i++) {
        Emergency_LAYER[i].setMap(null);
    }
    for (i = 0; i < Land_Sale_Branch_LAYER.length; i++) {
        Land_Sale_Branch_LAYER[i].setMap(null);
    }
    for (i = 0; i < Land_Administrative_Block_LAYER.length; i++) {
        Land_Administrative_Block_LAYER[i].setMap(null);
    }
}
//
function getDistrictBoundary(city_name) {

    var state = city_name;

    ajaxReq('Home', 'getDistrictboundary', { "location": state }, function (resp) { setDistrictboundary(resp) }, true);

}

function setDistrictboundary(resp) {
    if (resp.status === true) {
        var arr = $.map(resp.data, function (el) { return el; })

        for (i = 0; i < arr.length; i++) {
            var ln = createPolygon(getLatLongArr(arr[i][3]));
            ln.setOptions({ visibleZoom: 10, fillColor: '#FF8633', hideZoom: 9, visible: true, map: map, strokeWeight: 2, strokeColor: '#686868', fillOpacity: 0, clickable: !1 });
            maplayer.push(ln);

            var lp = new google.maps.LatLng(parseFloat(arr[i][2]), parseFloat(arr[i][1]));
            var _label = new google.maps.Label({ visibleZoom: 10, hideZoom: 5, visible: true, map: map, cssName: 'sctrLblDIST', position: lp, text: arr[i][0] });
        }

    }
}
function getStateboundary(city_name) {
    //debugger;

    var city = city_name;

    ajaxReq('Home', 'getStateboundary', { "location": city }, function (resp) { setStateboundary(resp) }, true);

}

function setStateboundary(resp) {
    if (resp.status === true) {
        //debugger;
        var arr = $.map(resp.data, function (el) { return el; })

        for (i = 0; i < arr.length; i++) {
            var ln = createPolygon(getLatLongArr(arr[i][0]));
            ln.setOptions({ visibleZoom: 5, fillColor: '#FF8633', hideZoom: 9, visible: true, map: map, strokeWeight: 4, strokeColor: '#E74C3C', fillOpacity: 0.1, clickable: !1 });
            maplayer.push(ln);

            // var lp = new google.maps.LatLng(parseFloat(arr[i][2]), parseFloat(arr[i][1]));
            //var _label = new google.maps.Label({ visibleZoom: 5, hideZoom: 9, visible: true, map: map, cssName: 'sctrLblDIST', position: lp, text: arr[i][0] });
        }

    }
}

//changes by sachin 22 july 2017 for district list
function getestateinformation_forlist(city_name) {
    // debugger;
    var city = city_name;
    ajaxReq('Home', 'getestateinformation', { "location": city }, function (resp) { showestateinformation_forlist(resp, city_name) }, true);

}
function showestateinformation_forlist(resp, city_name) {
    // debugger;
    if (resp.status === true) {
        var IIEINFOS = $.map(resp.data, function (el) { return el; })



        $('#iieInfo_name').empty();
        $('#iieInfo_TotalArea').empty();
        var tbl = $('#iieInfo_LandUse').empty();
        $('#iieInfo_Rate').empty();

        $('#iieInfo_name').append(IIEINFOS[0].Ind_area_name);
        $('#iieInfo_TotalArea').append(IIEINFOS[0].Area);
        $('#iieInfo_Rate').append(IIEINFOS[0].IND_RATE);
        var tr = '<tr>';
        tr += '<td style="border: 1px solid lightgrey; border-collapse: collapse; text-align: center; font-size: 11px;"><div style="width: 40px; color: #4679bd; font-weight: bold; margin-top: 2px;">S.no.</div></td>';
        tr += '<td style="border: 1px solid lightgrey; border-collapse: collapse; padding-left: 10px; font-size: 11px;"><div style="width: 100px; color: #4679bd; font-weight: bold; margin-top: 2px;">Land Use</div></td>';
        tr += '<td style="border: 1px solid lightgrey; border-collapse: collapse; text-align: center; font-size: 11px;"><div style="width: 100px; color: #4679bd; font-weight: bold; margin-top: 2px;">Area(In Hact.) Earmarked</div></td></tr>';
        tbl.append(tr);
        //tbl.append('<tr>'+''+'<div style="width: 100px; color: #4679bd; font-weight: bold; margin-top: 2px;">Land Use</div></td>'+' '+'<div style="width: 100px; color: #4679bd; font-weight: bold; margin-top: 2px;">Area(In Acres) Earmarked</div></td></tr>')
        for (var VALS = 0; VALS < IIEINFOS.length; VALS++) {
            drawRow(IIEINFOS[VALS], tbl, VALS);
        }
        tbl.append('<tr><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + '' + '</div></td><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;color: #4679bd;">Total</div></td><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + IIEINFOS[0].Total_area + '</div></td></tr>');
        getestateotherinformation(city_name);
        $("#diviieinfor").show();
        $("#DivNodata").hide();
    }
    else {
        //No Information
        $("#LiQuery").removeClass('active');
        $("#LiLayers").removeClass('active');
        $("#LiLegends").removeClass('active');
        $("#LiIndustrialList").addClass('active');
        $("#Liinformation").removeClass("active");
        $('#home').removeClass('in active');
        $('#profile').removeClass('in active');
        $('#messages').removeClass('in active');
        $('#settings').addClass('in active');
        $('#legends').removeClass('in active');
        $("#DivNodata").show();
        $("#diviieinfor").hide();
    }
}

//end of changes 22july2017


//changes by sachin 5 july
function getestateinformation(city_name) {
    //debugger;
    var city = city_name;
    ajaxReq('Home', 'getestateinformation', { "location": city }, function (resp) { showestateinformation(resp, city_name) }, true);

}
function showestateinformation(resp, city_name) {
    // debugger;
    if (resp.status === true) {
        var IIEINFOS = $.map(resp.data, function (el) { return el; })

        $("#LiQuery").removeClass('active');
        $("#LiLayers").removeClass('active');
        $("#LiLegends").removeClass('active');
        $("#LiIndustrialList").removeClass('active');
        $("#Liinformation").addClass("active");
        $('#home').removeClass('in active');
        $('#profile').removeClass('in active');
        $('#messages').removeClass('in active');
        $('#settings').removeClass('in active');
        $('#legends').addClass('in active');

        $('#iieInfo_name').empty();
        $('#iieInfo_TotalArea').empty();
        var tbl = $('#iieInfo_LandUse').empty();
        $('#iieInfo_Rate').empty();

        $('#iieInfo_name').append(IIEINFOS[0].Ind_area_name);
        $('#iieInfo_TotalArea').append(IIEINFOS[0].Area);
        $('#iieInfo_Rate').append(IIEINFOS[0].IND_RATE);
        var tr = '<tr>';
        tr += '<td style="border: 1px solid lightgrey; border-collapse: collapse; text-align: center; font-size: 10px;"><div style="width: 30px; color: #4679bd; font-weight: bold; margin-top: 2px;">S.no.</div></td>';
        tr += '<td style="border: 1px solid lightgrey; border-collapse: collapse; padding-left: 10px; font-size: 10px;"><div style="width: 80px; color: #4679bd; font-weight: bold; margin-top: 2px;">Land Use</div></td>';
        tr += '<td style="border: 1px solid lightgrey; border-collapse: collapse; text-align: center; font-size: 10px;"><div style="width: 80px; color: #4679bd; font-weight: bold; margin-top: 2px;">Area(In sq Mtr)</div></td></tr>';
        tbl.append(tr);
        //tbl.append('<tr>'+''+'<div style="width: 100px; color: #4679bd; font-weight: bold; margin-top: 2px;">Land Use</div></td>'+' '+'<div style="width: 100px; color: #4679bd; font-weight: bold; margin-top: 2px;">Area(In Acres) Earmarked</div></td></tr>')
        for (var VALS = 0; VALS < IIEINFOS.length; VALS++) {
            drawRow(IIEINFOS[VALS], tbl, VALS);
        }
        tbl.append('<tr><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + '' + '</div></td><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;color: #4679bd;">Total</div></td><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + IIEINFOS[0].Total_area + '</div></td></tr>');
        getestateotherinformation(city_name);
        $("#diviieinfor").show();
        $("#DivNodata").hide();
    }
    else {
        //No Information
        $("#LiQuery").removeClass('active');
        $("#LiLayers").removeClass('active');
        $("#LiLegends").removeClass('active');
        $("#LiIndustrialList").addClass('active');
        $("#Liinformation").removeClass("active");
        $('#home').removeClass('in active');
        $('#profile').removeClass('in active');
        $('#messages').removeClass('in active');
        $('#settings').addClass('in active');
        $('#legends').removeClass('in active');
        $("#DivNodata").show();
        $("#diviieinfor").hide();
    }
}
//function drawRow(rowData, tbl, VALS) {
//     //debugger;
//    tbl.append('<tr><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + (parseInt(VALS) + 1) + '</div></td><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + rowData.Land_Name + '</div></td><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + rowData.Land_area + '</div></td></tr>');
//}
function drawRow(rowData, tbl, VALS) {
    // debugger;
    tbl.append('<tr><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + (parseInt(VALS) + 1) + '</div></td><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + rowData.LAND_NAME + '</div></td><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + rowData.LAND_AREA + '</div></td></tr>');
}
//function drawRow2(rowData, tbl, VALS) {
//    //debugger;
//    tbl.append('<tr><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + (parseInt(VALS) + 1) + '</div></td><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + rowData.Land_Name + '</div></td><td class="RightSideSecondTD"><div style="width: 98%; margin-top: 2px;">' + rowData.Land_area + '</div></td></tr>');
//}

function getestateotherinformation(city_name) {
    // debugger;
    var city = city_name;
    ajaxReq('Home', 'getestateotherinformation', { "location": city }, function (resp) { showestateotherinformation(resp, city_name) }, true);

}
function showestateotherinformation(resp, city_name) {
    //debugger;
    if (resp.status === true) {
        var otherdata = $.map(resp.data, function (el) { return el; })
        $("#iieInfo_Location").empty();
        $("#iieInfo_allottedplots").empty();
        $("#iieInfo_majorUnits").empty();
        $("#iieInfo_products").empty();
        for (var locs = 0; locs < otherdata.length; locs++) {
            if (otherdata[locs].TYPE === 0) {
                $("#iieInfo_Location").append('<li>' + otherdata[locs].VALUE + '</li>')
            }
            else if (otherdata[locs].TYPE === 1) {
                $("#iieInfo_allottedplots").append('<li>' + otherdata[locs].VALUE + '</li>')
            }
            else if (otherdata[locs].TYPE === 2) {
                $("#iieInfo_majorUnits").append('<li>' + otherdata[locs].VALUE + '</li>')
            }
            else if (otherdata[locs].TYPE === 3) {
                $("#iieInfo_products").append('<li>' + otherdata[locs].VALUE + '</li>')
            }
        }
    }
}
//

//getRegionInfo(this.plotno, this.sector)
//function getRegionInfo(plot, sector) {
//    var city = city_name;
//    ajaxReq('Home', 'Regioninfo', { "plot": plot, "Sector": sector }, function (resp) { showRegionInfo(resp, plot, sector) }, true);
//    $('.statsDIv').hide();
//}
//function showRegionInfo(resp, plot, sector) {
//    // debugger;
//    if (resp.status === true) {
//        //var tbl = $('.infomain table').empty();
//        //var sector = SectorName;
//        //$('#spanIIE').empty().append(Industrial_Area_Name);
//        //tbl.empty();

//        //tbl.append('<tr><td >Plot No</td><td style="font-weight:bold;">' + plot + '</td></tr>');
//        //$.each(resp.data, function (indx, itm) {
//            //tbl.append('<tr><td>' + indx + '</td><td style="font-weight:bold;">' + itm + (indx == 'Plot Area' ? ' sq. mtr' : '') + '</td></tr>');
//            //if (indx == "Plot Status" && itm == "Available for Allotment") {
//                //  moreInfo();

//                //$(".infolower").empty();
//                //$(".infolower").css('visibility', 'visible');
//                // var url = "http://118.185.3.27/UPSIDC_SmartCity/User/ApplicationFormForFirstTimeUser.aspx?PlotNo=" + plot + '&Area=' + area + '&Sector=' + SectorName + '&iierecno=' + city_name + '&Industrial_Area_Name=' + Industrial_Area_Name + '&District_Name=' + District_Name;
//                // var url = "http://onlineupsidc.com/ApplicationPayment.aspx?PlotNo=" + plot + '&Area=' + area + '&Sector=' + SectorName + '&iierecno=' + city_name + '&Industrial_Area_Name=' + Industrial_Area_Name + '&District_Name=' + District_Name;
//                var url = "http://localhost:17225/User/ApplicationForm.aspx?PlotNo=" + plot + '&Sector=' + sector;
//                //$(".infolower").append('<a id="btnApply" class="btn btn-danger" target="_blank" href="' + url + '">Apply Now</a>')

//            //}
//            //else
//               // $(".infolower").css('visibility', 'hidden');
//        //});

//        $("#Plotinfo").dialog(opt).dialog("open");
//    }
//}
function getInfo(plot, area, sector, city_name, SectorName, Industrial_Area_Name, District_Name) {
    //debugger;
    var city = city_name;
    ajaxReq('Home', 'plotinfo', { "plot": plot, "Sector": sector }, function (resp) { showPlotInfo(resp, plot, area, sector, city_name, SectorName, Industrial_Area_Name, District_Name) }, true);
    $('.statsDIv').hide();
}

function showPlotInfo(resp, plot, area, sector, city_name, SectorName, Industrial_Area_Name, District_Name) {
    debugger;
    if (resp.status === true) {
        //
        var IIEINFO = $.map(resp.data, function (el) { return el; })
        // var tbl = $('.infolower1').empty();
        //var tbl = $('.infolower2').empty();
        //var tbl = $('.infolower3').empty();
        // var tbl = $('#spanIIE').empty();
        // var tbl = $('#iieInfo_plot').empty();
        //$('#spanIIE').append(Industrial_Area_Name);
        $(".infolower1").empty();
        $('#spanPlotNo').empty().append(plot);
        $('#spanPlotArea').empty().append(area);
        $('#spanSectorName').empty().append(SectorName);
        $('#spanDistrictName').empty().append(District_Name);
        $('#spanAllotteeName').empty().append(IIEINFO[0].Allotee_Name);
        $('#spanPlotStatus').empty().append(IIEINFO[0].Plot_Status);
        //for (var VALS = 0; VALS < IIEINFO.length; VALS++) {
        //    drawRow(IIEINFO[VALS], tbl, VALS);
        //}
        if (IIEINFO[0].Plot_Status == "Available for Allotment") {
            var url = "http://118.185.3.27/ddanewgis/user/Applicationform.aspx";
            $(".infolower1").empty().append('<td style="margin-left:100px;">Apply for Booking</td><td><a href=" ' + url + '" target="_blank"><b>Click here</b></a>')
        }
        //
        var tbl = $('.infomain table').empty();
        var sector = SectorName;

        tbl.empty();

        tbl.append('<tr><td >Plot No</td><td style="font-weight:bold;">' + plot + '</td></tr>');

        $.each(resp.data, function (indx, itm) {
            tbl.append('<tr><td>' + indx + '</td><td style="font-weight:bold;">' + itm + (indx == 'Plot Area' ? ' sq. mtr' : '') + '</td></tr>');
            if (indx == "Plot Status" && itm == "Available for Allotment") {
                //  moreInfo();

                //$(".infolower").empty();
                //$(".infolower").css('visibility', 'visible');

                // var url = "http://118.185.3.27/UPSIDC_SmartCity/User/ApplicationFormForFirstTimeUser.aspx?PlotNo=" + plot + '&Area=' + area + '&Sector=' + SectorName + '&iierecno=' + city_name + '&Industrial_Area_Name=' + Industrial_Area_Name + '&District_Name=' + District_Name;
                // var url = "http://onlineupsidc.com/ApplicationPayment.aspx?PlotNo=" + plot + '&Area=' + area + '&Sector=' + SectorName + '&iierecno=' + city_name + '&Industrial_Area_Name=' + Industrial_Area_Name + '&District_Name=' + District_Name;
                // var url = "http://118.185.3.27/ApplicationForm/User/IndustriesApplicationForm.aspx?PlotNo=" + plot + '&Area=' + area + '&Sector=' + sector;
                //$(".infolower").append('<tr><td >Plot Area</td><td style="font-weight:bold;">' + area + '</td></tr>')

            }
            else
                $(".infolower").css('visibility', 'visible');
        });

        $("#Plotinfo").dialog(opt).dialog("open");
    }
}

//function getInfo( Industrial_Area_Name) {

//    //debugger;
//    var IIE = Industrial_Area_Name;
//    ajaxReq('Home', 'plotinfo', { "IIE": Industrial_Area_Name }, function (resp) { showPlotInfo(resp, Industrial_Area_Name) }, true);
//    $('.statsDIv').hide();
//}

//function showPlotInfo(resp, Industrial_Area_Name) {
//    //debugger;

//    if (resp.status === true) {
//        var IIEINFO = $.map(resp.data, function (el) { return el; })
//        var tbl = $('.infolower1').empty();
//        var tbl = $('.infolower2').empty();
//        var tbl = $('.infolower3').empty();
//        var tbl = $('#spanIIE').empty();
//        var tbl = $('#iieInfo_plot').empty();
//        $('#spanIIE').append(IIEINFO[1].Ind_area_name);
//        for (var VALS = 0; VALS < IIEINFO.length; VALS++) {
//            drawRow(IIEINFO[VALS], tbl, VALS);
//        }
//        $("#Plotinfo").show();
//        //var url = "http://118.185.3.27/cwcgisnew/User/ApplicationForm.aspx?Industrial_Area_Name=" + Industrial_Area_Name;
//        //var urls = "http://118.185.3.27/cwcgisnew/User/TariffDetails.aspx?Industrial_Area_Name=" + Industrial_Area_Name;
//        var url = "/cwcgisnew/User/ApplicationForm.aspx?Industrial_Area_Name=" + Industrial_Area_Name;
//        var urls = "/cwcgisnew/User/TariffDetails.aspx?Industrial_Area_Name=" + Industrial_Area_Name;
//        var urlImg = "/cwcgisnew/User/ImageGallery.aspx?Industrial_Area_Name=" + Industrial_Area_Name;
//        $(".infolower1").append('<td style="margin-left:100px;">Apply for Booking</td><td><a href=" ' + url + '" target="_blank"><b>Click here</b></a>')
//        $(".infolower2").append('<td>Tariff Details</td><td><a href=" ' + urls + '" target="_blank"><b>Click here</b></a>')
//        $(".infolower3").append('<td>Images</td><td><a href=" ' + urlImg + '" target="_blank"><b>Click here</b></a>')

//        $("#Plotinfo").dialog(opt).dialog("open");
//        } 
//       // $("#Plotinfo").dialog(opt).dialog("open"); 
//}
function moreInfo() {

    var _lis = '<li>High Class Road: 30m wide and above</li><li>Electricity & LED Street lights</li><li>Water Supply</li><li>Statutory Services</li><li>DataCom Services</li><li>Fire & Police Stations</li>';
    $('.info .infomore ul').empty().append(_lis);
}
var opt = {
    resizable: false,
    autoOpen: false,
    show: 'blind',
    hide: 'blind',
    width: 330,
    top: 100,
    position: { my: "right center", at: "right center", of: window }
};

function getCompanies(city_name) {

    var city = city_name;
    ajaxReq('Home', 'CompaniesList', { "location": city }, function (resp) { BindCompanies(resp, city) }, true);

}

function BindCompanies(resp, city) {
    if (resp.status === true) {
        var CompanyName = $.map(resp.data, function (el) { return el; })
        $('#ddlCompany').append(new Option('--Select Company--', ''));
        for (i = 0; i < CompanyName.length; i++) {

            for (i = 0; i < CompanyName.length; i++) {
                if (i % 5 === 0) {
                    $("#ddlCompany").append(new Option(CompanyName[i], CompanyName[i + 1] + '/' + CompanyName[i + 2] + '/' + CompanyName[i + 3] + '/' + CompanyName[i + 4]));
                }
            }
        }
    }
    else {
        $('#ddlCompany').empty();
        $('#ddlCompany').append(new Option('--No Allotment Data Found--', ''));
    }

}

function getAllCategory() {
    ajaxReq('Home', 'IIECategory', {}, function (resp) { BindAllCategory(resp) }, true);

}
function BindAllCategory(resp) {
    $("#ddlCategory").empty();
    if (resp.status === true) {
        var IIECategory = $.map(resp.data, function (el) { return el; })
        $('#ddlCategory').append(new Option('--Select Category--', ''));

        for (i = 0; i < IIECategory.length; i++) {
            if (i % 2 === 0) {
                $("#ddlCategory").append(new Option(IIECategory[i + 1], IIECategory[i] + '/' + IIECategory[i + 2] + '/' + IIECategory[i + 3] + '/' + IIECategory[i + 4]));
            }
        }
    }
}

$(document).on('click', '#btnCategorywise', function (e) {
    e.preventDefault();

    for (i = 0; i < IIE_MARKERS.length; i++) {
        IIE_MARKERS[i].setMap(null);

    }

    for (i = 0; i < SIIDCUL_POLLUTION_MARKERS.length; i++) {
        SIIDCUL_POLLUTION_MARKERS[i].setMap(null);
    }

    map.setZoom(7);

    map.panTo(new google.maps.LatLng(26.846709, 80.946159));


    var Plotarea = $('#txtPlotArea').val();
    var category = $('#ddlCategory option:selected').text();


    if (category == "--Select Category--") {
        alert("Please select type of Industry").fadeOut("slow");
    }

    else if (Plotarea == "0" || Plotarea == "") {
        alert("Please Enter Plot Area").fadeOut("slow");
    }

    else {

        getPlotAvailabliltyInfo(Plotarea, category);
    }

});

function getPlotAvailabliltyInfo(Plotarea, category) {

    ajaxReq('Home', 'getPlotAvailabliltyInfo', { "plotArea": Plotarea, "category": category }, function (resp) { showPlotAvailablilty(resp, Plotarea) }, true);

}
function showPlotAvailablilty(resp, Plotarea) {
    if (resp.status === true) {
        // debugger;
        var tbl = $('#sa').empty();
        var cat = $('#ddlCategory option:selected').text();
        tbl.empty();

        tbl.append('<thead class="ui-widget-header"><tr><td>IIE Name</td><td>Available Plots Count</td><td>Land Rate</td></tr> </thead> <tbody class="ui-widget-content">');

        for (i = 0; i < CATEGORY_MARKERS.length; i++) {
            CATEGORY_MARKERS[i].setMap(null);
        }
        for (var i = 0; i < resp.data.length; i++) {
            var getIIEArray = [];
            $("#sa").append("<tr><td ><div class='DivCategory1'><b>" + resp.data[i]["Ind_area_name"] + "</b></div></td><td align='center'><div class='DivCategory2';>" + resp.data[i]["VacantPlots_Count"] + "</div></td><td><div class='DivCategory3'>" + resp.data[i]["Land_Rate"] + "</div></td></tr>");
            for (var x = 0; x < Categorydata[cat]["IIE"].length; x++) {

                var find = Categorydata[cat]["IIE"][x];
                if (find[0] === resp.data[i]["Ind_area_name"]) {
                    getIIEArray.push(find);
                    break;
                }
            }
            if (getIIEArray.length > 0) {
                var mm = "images/pinBlue.png";
                var marker, i;

                var beach = getIIEArray[0];
                marker = new google.maps.Marker({
                    position: { lat: beach[2], lng: beach[1] },
                    map: map,
                    icon: mm,
                    title: beach[0]

                });
                marker.zaixs = beach[3],
                    marker.x = beach[1],
                    marker.y = beach[2],
                    marker.IIEName = beach[4],
                    marker.Area = Plotarea,
                    marker.category = cat,
                    google.maps.event.addListener(marker, 'click', function () {
                        createIIELayes(marker);
                    });
                CATEGORY_MARKERS.push(marker);
            }
        }

        $("#sa").append('</tbody>');
        $("#DivCategoryResult").dialog(opt).dialog("open");

        //myFunctions();

    }
    else {
        var tbl = $('#sa').empty();
        tbl.empty();
        tbl.append('<thead class="ui-widget-header"><tr><td>IIE Name</td><td>Vancant Plots Count</td><td style="font-weight:bold;">Land Rate</td></tr> </thead> <tbody class="ui-widget-content">');
        tbl.append('<tr><td colspan="3">Due to Network issue system is unable to fetch the data or No Data Found</td></tr></tbody>');
    }
}
function createIIELayes(marker) {
    // debugger;
    map.setZoom(marker.zaixs);
    map.panTo(new google.maps.LatLng(marker.y, marker.x));
    getPlotcreate(marker.IIEName);
    getroadcreate(marker.IIEName);
    getutilitycreate(marker.IIEName);
    getPublicPurposecreate(marker.IIEName);
    getCommanareacreate(marker.IIEName);
    getOthercreate(marker.IIEName);
    getWaterheadtankcreate(marker.IIEName);
    getparkingcreate(marker.IIEName);
    getNalacreate(marker.IIEName);
    getPoloicestationcreate(marker.IIEName);
    getfirestationcreate(marker.IIEName);
    getlabelcreate(marker.IIEName);
    getvegcreate(marker.IIEName);
    getbndcreate(marker.IIEName);
    getsectorcreate(marker.IIEName);
    getElinecreate(marker.IIEName);
    getEPolecreate(marker.IIEName);
    getWatercreate(marker.IIEName);
    getRivercreate(marker.IIEName);
    getlamppostcreate(marker.IIEName);
    getRailwaycreate(marker.IIEName);
    getmanholecreate(marker.IIEName);
    getfootpathcreate(marker.IIEName);
    getBridgecreate(marker.IIEName);
    showPlots(marker.IIEName);
    //CloseDialogs();  
    if (marker.Area > 0) {
        // debugger;
        PushVacantPlots(marker.Area);
    }
    else {
        alert("Please Enter Plot Area").fadeOut("slow");
    }
}
var CloseDialogs = function () {
    $(".ui-dialog-content").dialog("close");
};
$(document).on('keypress', '#txtPlotArea', function (e) {
    //$("#txtPlotArea").keypress(function (e) {
    //if the letter is not digit then display error and don't type anything
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        //display error message
        alert("Digits Only").fadeOut("slow");
        $('#txtPlotArea').val("");
    }
});



function PushVacantPlots(Area) {
    // debugger;
    for (i = 0; i < SerachResult.length; i++) {
        SerachResult[i].setMap(null);
    }
    for (i = 0; i <= VACANT_PLOTS.length; i++) {
        var layer = VACANT_PLOTS[i];
        if (VACANT_PLOTS.length > 0) {
            if (VACANT_PLOTS[i].length >= 1) {
                layer.area = VACANT_PLOTS[i][1];
                var Areas = layer.area;
                if (parseInt(Areas) >= parseInt(Area)) {
                    var poly = createPolygon(getLatLongArr(layer[4]));
                    //{ fillColor: '#fff198', strokeColor: '#666666', fillOpacity: 0.8, clickable: !0 }
                    poly.setOptions({ strokeWeight: 6, strokeColor: '#3333cc', fillOpacity: 0 });
                    SerachResult.push(poly);
                }
            }
        }
    }
}


$(document).on('click', '#btnSearchRoute', function (e) {
    e.preventDefault();
    $("#DivRoute").dialog(opt).dialog("open");
});

var CloseDialogs = function () {
    $(".ui-dialog-content").dialog("close");
};

function GetCurrentDetails(Type, Id) {
    ajaxReq('Home', 'GetCurrentDetails', { "Type": Type, "Id": Id }, function (resp) { ShowCurrentDetails(resp, Type) }, true);
}

function ShowCurrentDetails(resp, Type) {
    if (resp.status === true) {

        var Details = $.map(resp.data, function (el) { return el; })

        if (Type === "State") {
            $("#lblArea").text("State : ");
            $("#lblType").text("No of District:9");
            //$("#lblAvailablePlots").text("No. of plots Available for Allotment : ");
            //$("#lblAvailableTotalArea").text("Available plots Area : ");
            //hide column
            $("#tdAvailableplots").hide();
            $("#tdAvailableplotsArea").hide();
        }
        //else if (Type === "District") {
        //    $("#lblArea").text("District : ");
        //    $("#lblType").text("No. of Industrial Area : ");
        //    //hide column
        //    $("#tdAvailableplots").hide();
        //    $("#tdAvailableplotsArea").hide();
        //}
        //else {
        //    $("#lblArea").text("Region office : ");
        //    $("#lblType").text("No. of Plots : ");
        //    //hide column
        //    $("#tdAvailableplots").show();
        //    $("#tdAvailableplotsArea").show();
        //}

        $("#lblTotalArea").text("Total Area  :366704 Acre ");

        //$("#lblAreaName").empty().append(Details[0].Name);
        //$("#lblNoOfValue").empty().append(Details[0].TotalNo);
        //$("#lblAreaValue").empty().append(Details[0].TotalArea);
        //$("#lblAvailablePlotsValue").empty().append(Details[0].plotcount);
        //$("#lblAvailableTotalAreaValue").empty().append(Details[0].totalplotArea);
        //$("#lblAvailableTotalAreaValue").append(" Acres");
        //$("#lblAreaValue").append(" Acres");
    }
    else {
        $("#lblArea").text("State : ");
        $("#lblAreaName").text("Delhi");

        $("#lblType").text("No. of Districts : ");
    }
}


function ShowCurrentDetails() {





    $("#lblArea").text("State :Delhi ");
    $("#lblType").text("No of Zones:18");

    //hide column
    $("#tdAvailableplots").hide();
    $("#tdAvailableplotsArea").hide();



    $("#lblTotalArea").text("Total Area  :366704 Acres ");


}




$(document).on('click', '#Searchindustrye', function (e) {
    e.preventDefault();
    getAllDISTRICT(4);
    getAllIIECompanywise(4);
    getAllCategory();
});

function ajaxReq(handler, reqType, inData, callbackFun, asyc) {
    if (asyc) asyc = true; else asyc = false;
    $.ajax({
        url: handler + "?request=" + reqType,
        type: "post",
        async: asyc,
        data: inData,
        error: function () {
            // alert("Oops! Something went wrong.");
        }
    }).done(callbackFun);
}

$(document).on('change', '#chkAllImpInfra', function (e) {

    e.preventDefault();

    $(this).closest('table').find('td input[type="checkbox"]').prop('checked', $(this).prop('checked'));
    if (this.checked) {
        $('#chkAllImpInfra').attr("checked", "checked");
        $('#chkAllImpInfra').closest('table').find('td input[type="checkbox"]').prop('checked', true);


    }
    else {
        $('#chkAllImpInfra').closest('table').find('td input[type="checkbox"]').prop('checked', false);
        $('#chkAllImpInfra').closest('table').find('td input[type="checkbox"]').removeAttr('checked');

    }


    //road
    if ($("#chkRoad").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getroadcreate(beach);

        }
    }
    else {
        for (i = 0; i < RDS_LAYER.length; i++) {
            RDS_LAYER[i].setMap(null);
        }

    }

    //Water Connection

    if ($("#chkWaterSupplyLine").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getWatercreate(beach);

        }
    }
    else {
        for (i = 0; i < WTR_LAYER.length; i++) {
            WTR_LAYER[i].setMap(null);
        }

    }
    //Electric Line

    if ($("#chkElectricityLine").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getElinecreate(beach);

        }
    }
    else {
        for (i = 0; i < ELINE_LAYER.length; i++) {
            ELINE_LAYER[i].setMap(null);
        }

    }

    //RailwayTrack
    if ($("#chkRailwayTrack").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getRailwaycreate(beach);

        }
    }
    else {
        for (i = 0; i < RAILWAY_LAYER.length; i++) {
            RAILWAY_LAYER[i].setMap(null);
        }

    }
    //Electric pole
    if ($("#chkElectricityPole").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getEPolecreate(beach);

        }
    }
    else {
        for (i = 0; i < EPOLE_LAYER.length; i++) {
            EPOLE_LAYER[i].setMap(null);
        }

    }

    //Man Hole

    if ($("#chkManHoles").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getmanholecreate(beach);

        }
    }
    else {
        for (i = 0; i < MANHOLE_LAYER.length; i++) {
            MANHOLE_LAYER[i].setMap(null);
        }

    }
    //Gas
    if ($("#chkgas").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getgascreate(beach);

        }
    }
    else {
        for (i = 0; i < GAS_LAYER.length; i++) {
            GAS_LAYER[i].setMap(null);
        }

    }

    //Utitlity

    if ($("#chkutitlity").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getutilitycreate(beach);

        }
    }
    else {
        for (i = 0; i < MISC_LAYER.length; i++) {
            MISC_LAYER[i].setMap(null);
        }

    }

});


$(document).on('change', '#chkallAddiInfra', function (e) {

    e.preventDefault();

    $(this).closest('table').find('td input[type="checkbox"]').prop('checked', $(this).prop('checked'));
    if (this.checked) {
        $('#chkallAddiInfra').attr("checked", "checked");
        $('#chkallAddiInfra').closest('table').find('td input[type="checkbox"]').prop('checked', true);


    }
    else {
        $('#chkallAddiInfra').closest('table').find('td input[type="checkbox"]').prop('checked', false);
        $('#chkallAddiInfra').closest('table').find('td input[type="checkbox"]').removeAttr('checked');

    }


    //Bridge
    if ($("#chkBridge").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getBridgecreate(beach);

        }
    }
    else {
        for (i = 0; i < BRG_LAYER.length; i++) {
            BRG_LAYER[i].setMap(null);
        }

    }

    //Vegitation

    if ($("#chkvegitation").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getvegcreate(beach);

        }
    }
    else {
        for (i = 0; i < VEG_LAYER.length; i++) {
            VEG_LAYER[i].setMap(null);
        }

    }
    //Foot Path

    if ($("#chkFootPath").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getfootpathcreate(beach);

        }
    }
    else {
        for (i = 0; i < FTP_LAYER.length; i++) {
            FTP_LAYER[i].setMap(null);
        }

    }

    //LampPost
    if ($("#chkLampPost").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getlamppostcreate(beach);

        }
    }
    else {
        for (i = 0; i < LAMPPOST_LAYER.length; i++) {
            LAMPPOST_LAYER[i].setMap(null);
        }

    }
    //Comman Area
    if ($("#chkcomman").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getCommanareacreate(beach);

        }
    }
    else {
        for (i = 0; i < COMMON_LAYER.length; i++) {
            COMMON_LAYER[i].setMap(null);
        }

    }

    //Public Purpose

    if ($("#chkpublicpurpose").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getPublicPurposecreate(beach);

        }
    }
    else {
        for (i = 0; i < PUBLIC_PURPOSE_LAYER.length; i++) {
            PUBLIC_PURPOSE_LAYER[i].setMap(null);
        }

    }

});


$(document).on('change', '#chkAllMiscel', function (e) {

    e.preventDefault();

    $(this).closest('table').find('td input[type="checkbox"]').prop('checked', $(this).prop('checked'));
    if (this.checked) {
        $('#chkAllMiscel').attr("checked", "checked");
        $('#chkAllMiscel').closest('table').find('td input[type="checkbox"]').prop('checked', true);


    }
    else {
        $('#chkAllMiscel').closest('table').find('td input[type="checkbox"]').prop('checked', false);
        $('#chkAllMiscel').closest('table').find('td input[type="checkbox"]').removeAttr('checked');

    }


    //Boundary
    if ($("#chkBoundary").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getbndcreate(beach);

        }
    }
    else {
        for (i = 0; i < BND_LAYER.length; i++) {
            BND_LAYER[i].setMap(null);
        }

    }

    //sector

    if ($("#chkSector").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getsectorcreate(beach);

        }
    }
    else {
        for (i = 0; i < SECT_LAYER.length; i++) {
            SECT_LAYER[i].setMap(null);
        }

    }
    //Other

    if ($("#chkother").prop('checked') == true) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getOthercreate(beach);

        }
    }
    else {
        for (i = 0; i < OTHERS_LAYER.length; i++) {
            OTHERS_LAYER[i].setMap(null);
        }

    }

    //Water Tank
    if ($("#chkwatertank").prop('checked') == true) {

        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getWaterheadtankcreate(beach);

        }
    }
    else {
        for (i = 0; i < WATER_HEAD_TANK.length; i++) {
            WATER_HEAD_TANK[i].setMap(null);
        }

    }
    //Parking
    if ($("#chkparking").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getparkingcreate(beach);

        }
    }
    else {
        for (i = 0; i < PARKING_LAYER.length; i++) {
            PARKING_LAYER[i].setMap(null);
        }

    }

    //Water Body

    if ($("#chkWaterBody").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getRivercreate(beach);

        }
    }
    else {
        for (i = 0; i < RIVER_LAYER.length; i++) {
            RIVER_LAYER[i].setMap(null);
        }

    }

    //NALA

    if ($("#chknala").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getNalacreate(beach);

        }
    }
    else {
        for (i = 0; i < NALA_LAYER.length; i++) {
            NALA_LAYER[i].setMap(null);
        }

    }

    //Police Station

    if ($("#chkpolice").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getPoloicestationcreate(beach);

        }
    }
    else {
        for (i = 0; i < POLICE_STATION_LAYER.length; i++) {
            POLICE_STATION_LAYER[i].setMap(null);
        }

    }

    //Fire

    if ($("#chkfire").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getfirestationcreate(beach);

        }
    }
    else {
        for (i = 0; i < FIRE_STATION_LAYER.length; i++) {
            FIRE_STATION_LAYER[i].setMap(null);
        }

    }

    //Encroachment
    if ($("#checkEncroachment").prop('checked') == true) {
        for (i = 0; i < CITY_NAME.length; i++) {
            var beach = CITY_NAME[i];
            getencroachmentcreate(beach);

        }
    }
    else {
        for (i = 0; i < ENCROACHMENT_LAYER.length; i++) {
            ENCROACHMENT_LAYER[i].setMap(null);
        }

    }
});

var availableTags = [
];

function FetchData() {
    ajaxReq('Home', 'IIE_Auto', {}, function (resp) { showautocomp(resp) }, false);
}
function showautocomp(resp) {
    if (resp.status == true) {
        var dd = $.map(resp.data, function (el) { return el; })
        availableTags = [];
        // debugger;
        for (var xx = 0; xx < dd.length; xx++) {
            var availableTag = [];
            availableTag.id = dd[xx].Recno;
            availableTag.name = dd[xx].Name;
            availableTags.push(availableTag);
        }
    }
}
$(function () {
    FetchData();
    $("#tags").autocomplete({
        source: function (request, response) {
            response($.map(availableTags, function (value, key) {
                if (value.name.toUpperCase().indexOf(request.term.toUpperCase()) > -1) {
                    return {
                        label: value.name,
                        value: value.id
                    }
                }
            }));

        },
        select: function (event, ui) {
            event.preventDefault();
            $("#tags").val(ui.item.label);
            showAutoIndustrial(ui.item.value);

        }
    });

});

function showAutoIndustrial(ind_id) {
    GetCurrentDetails("IE", ind_id);
    ajaxReq('Home', 'IndBoundary', { "Ind_Recno": ind_id }, function (resps) { BindIndBoundary(resps, ind_id) }, true);
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

//harshit
//Added on 07/11/2017
$(document).on('click', '#basemape', function (e) {
    e.preventDefault();
    getAllDISTRICT(4);
});

//$(document).on('change', '#ddlPhase', function (e) {
//    e.preventDefault();
//    var District = $('#ddlPhase option:selected').val();

//    var DISTRICT_BND = District.split('/');
//    getAllIIE(DISTRICT_BND[0]);
//    DISTRICT_BND = DISTRICT_BND.concat(District);
//    for (var x = 0; x < zoomDistrict.length; x++) {
//        zoomDistrict[x].setMap(null);
//    }
//    var sl = createPolygon(getLatLongArr(DISTRICT_BND[1]));
//    sl.setOptions({ strokeWeight: 5, strokeColor: '#0242BD', fillOpacity: 0, clickable: !1 });
//    zoomDistrict.push(sl);
//    map.setZoom(10);
//    map.panTo(new google.maps.LatLng(DISTRICT_BND[3], DISTRICT_BND[2]));
//});

$(document).on('click', '#btnShow', function (e) {
    e.preventDefault();
    var Vphase = $('#ddlPhase option:selected').val();
    var Vsector = $('#ddlSectorName option:selected').val();
    if (Vphase !== "" && Vsector !== "") {
        Vphase = $('#ddlPhase option:selected').text();
        Vsector = $('#ddlSectorName option:selected').text();
        var win = window.open('PlotStatusReports.aspx?phase=' + Vphase + '&sector=' + Vsector, '_blank');
        if (win) {
            //Browser has allowed it to be opened
            win.focus();
        } else {
            //Browser has blocked it
            alert('Please allow popups for this website');
        }
    }
    else {
        alert("please select correct details.");
    }
});
