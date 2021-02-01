
var data = {
};

var colors = {
    "Plot Type": {
        "Industrial Plot": "#6C6C6C",
        "Commercial Estate": "#008C89",
        "Commercial Land": "#400040",
        "Residential Plot": "#D4CF17",
        "Institutional/Banking": "#0000cc",
        "Emergency Services": "#5252FF",
        "Land Sale Branch": "#A7855F",
        "Land Administrative Block": "#6A903E",
        "No Status": "#CCCC99"
    },
    "Completion Status": {
        "Available for Allotment": "#E222AB",
        "Not available for Allotment": "#FC033B",
        "Allotted": "#000080",
        "Free Hold": "#3498DB",
        "Sold In Last Auction": "#ECE92B",
        "Cancelled": "#FFA07A",
        "Green Area": "#55E82D",
        "Sick": "#927503",
        "No Status": "#CCCC99"
    },
    "blank": "#CCCC99"
};
var thematic_param = 'Completion Status';  //'Plot Type' "#A9A44C",;




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
        icon: 'images/marker.png',
        map: map,
        clickable: !1
    });
    return pt;
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

function createMarker(_path) {
    var tmpMarker = new google.maps.Marker({

        clickable: !1,
        map: map,
        path: _path
    });
    return tmpMarker;
}

function createStatePolygon(_path) {
    var tmpLine = new google.maps.PolylinepolyOptions({
        strokeColor: '#FF0000',
        fillColor: 'RED',
        fillOpacity: 0.7,
        strokeOpacity: 1,
        strokeWeight: 3,
        clickable: !1,
        map: map,
        path: _path
    });
    return tmpLine;
}

var maplayer = [];

function refreshLayer(city) {
 for (xl = 0; xl < start_loc.length; xl++) {

        var ds = start_loc[xl][0];

        if (ds === 'Uttar Pradesh') {
            clearAll(maplayer);
            maplayer = [];
        }
        showPlots(ds);
        showLegend();
    }
}

function showLegend() {
    var thmdiv = $('#mapstype').empty();
    var allotteddiv = $('#allottedStatus').empty();
    var thm_text;
    if ($('#chkCheckAllPlotStatus').attr('checked')) {
        thm_text = " Un-Allotted Plots";
        thematic_param = 'Completion Status';
        allotted_text = " Allotted Plots";
        thmdiv.append('<span style="margin-left:2px"><b>' + thm_text + ':</b><b style="font-size:18px;">|</b>');
        var count = 0;
        $.each(colors[thematic_param], function (key, val) {
            if (count <= 1) {
                thmdiv.append('<div class="foo" style="background-color:' + val + ';"></div>');
                thmdiv.append('<span class="flaticon-media26">' + key + '</span>');
            }
            count++;
        });
        thmdiv.append('</span>');
        count = 0;
        allotteddiv.append('<span style="margin-left:2px"><b>' + allotted_text + ':</b><b style="font-size:18px;">|</b></span>');
        $.each(colors[thematic_param], function (key, val) {
            if (count > 1) {
                allotteddiv.append('<div class="foo" style="background-color:' + val + ';"></div>');
                allotteddiv.append('<span class="flaticon-media26">' + key + '</span>');
            }
            count++;
        });
    }
    else {

        thm_text = "  Plot Type";
        thematic_param = 'Plot Type';

        allotteddiv.append('<span style="margin-left:2px"><b>' + thm_text + ':</b> <b style="font-size:18px;">|</b></span>');
        $.each(colors[thematic_param], function (key, val) {
            allotteddiv.append('<div class="foo" style="background-color:' + val + ';"></div>');
            allotteddiv.append('<span class="flaticon-media26">' + key + '</span>');
        });
    }


    //thmdiv.append('<span class="flaticon-media26" style="color:' + colors.blank + ';">No status</span>');    
}

function showPlots(city) {
    var data_city = city;
   
        ajaxReq('do_it.ashx', 'plotstatus', { "location": city }, function (resp) { fillStatus(resp, data_city, city) }, true);
   

    $('.footer .map_status').text(thematic_param == 'Completion Status' ? 'Map showing status of Plots' : 'Map showing type of Plots');
}

function fillStatus(resp, data_city,city) {
    if (resp.status === true) {
        // var data_city = $.map(resp.data, function (el) { return el; })
        //  data_city[city_name]["Status"] = resp.data_city;
       var datacity = resp.data;
       // data[city]["Status"] = resp.data;
    }
   // showAllLayers(city, fullcity);
}

function getColor(plot_no, sector, city_name, Status) {
    var color = colors.blank;
    // plot_no = sector + '_' + plot_no;
    //plot_no = plot_no;
    try {
        // var thmval = "Vacant";
        var thmval = Status;
        color = colors[thematic_param][thmval];
        var x = 1;
    }
    catch (a) { }
    return color;
}
//added by sachin 5july
function getTypePlotColor(plot_no, Status) {
    var color = colors.blank;
    try {

        var thmval = Status;
        color = colors["Plot Type"][thmval] || 'blank';
        var x = 1;
    }
    catch (a) { }
    return color;
}
//

function showAllLayers(city, fullcity) {
   // $('.info').hide();
   
    
    data[city]["Status"] = data[city]["Status"] || {};

    if ($("#chkSector").prop('checked') == true) {
        var SECTR = data[city].SCTR;

        for (i = 0; i < SECTR.length; i++) {
            var ln = createPolygon(getLatLongArr(SECTR[i][4]));
            ln.setOptions({ strokeWeight: 5, strokeColor: '#971C66', fillOpacity: 0, clickable: !1 });
            maplayer.push(ln);
            //FOR LAYER'S ON/OFF
            SECT_LAYER.push(ln);
            //


            var lp = new google.maps.LatLng(parseFloat(SECTR[i][3]), parseFloat(SECTR[i][2]));
            var _label = new google.maps.Label({ visibleZoom: 13, hideZoom: 19, visible: true, map: map, cssName: 'sctrLbl', position: lp, text: SECTR[i][1] });
            //For Searching Added by sachin 3/2/2016
            //   var IIE = $('.city_name').text();


            // Sector.append('<option >' + SECTR[i][1] + ':</span>');
            //
            maplayer.push(_label);
        }
    }

    if (data[city]["LBLS"]) {
        var LBLS = data[city].LBLS;
        for (i = 0; i < LBLS.length; i++) {
            var lp = new google.maps.LatLng(parseFloat(LBLS[i][3]), parseFloat(LBLS[i][2]));
            var _label = new google.maps.Label({ visibleZoom: 19, hideZoom: 60, visible: true, map: map, cssName: 'nlLabel', position: lp, text: LBLS[i][1] });
            maplayer.push(_label);
        }
    }
    //Water Body
    if ($("#chkWaterBody").prop('checked') == true) {
        var RIVER = data[city].RIVER;
        for (i = 0; i < RIVER.length; i++) {
            var ln = createLine(getLatLongArr(RIVER[i]));
             ln.setOptions({ fillColor: '#0000ff', strokeColor: '#0000ff', fillOpacity: 1 });
           //ln.setOptions({strokeColor: '#0000ff', fillColor: '#0000ff',fillOpacity:0.8 });
            maplayer.push(ln);
            //FOR LAYER'S ON/OFF
            RIVER_LAYER.push(ln);
            //
        }
    }

    var STATE_BOND = data[city].STATE_BOND;
    for (i = 0; i < STATE_BOND.length; i++) {
        var ln = createPolygon(getLatLongArr(STATE_BOND[i][4]));
        ln.setOptions({ visibleZoom: 5, fillColor: '#FF8633', hideZoom: 9, visible: true, map: map, strokeWeight: 5, strokeColor: '#E74C3C', fillOpacity: 0, clickable: !1 });
        maplayer.push(ln);

        var lp = new google.maps.LatLng(parseFloat(STATE_BOND[i][3]), parseFloat(STATE_BOND[i][2]));
        var _label = new google.maps.Label({ visibleZoom: 5, hideZoom: 9, visible: true, map: map, cssName: 'sctrLblDIST', position: lp, text: STATE_BOND[i][1] });

    }
    if ($("#chkFootPath").prop('checked') == true) {
        var FTP = data[city].FTP;
        for (i = 0; i < FTP.length; i++) {
            var ln = createPolygon(getLatLongArr(FTP[i]));
            ln.setOptions({ fillColor: '#FFFFFF', strokeColor: '#6E6E6E', fillOpacity: 1 });
            maplayer.push(ln);
            //FOR LAYER'S ON/OFF
            FTP_LAYER.push(ln);
            //
        }
    }
    if ($("#chkBoundary").prop('checked') == true) {
        var BND = data[city].BND;
        for (i = 0; i < BND.length; i++) {
            var ln = createLine(getLatLongArr(BND[i]));
            ln.setOptions({fillcolor:' #000000', strokeWeight: 1, strokeColor: ' #000000' });
            maplayer.push(ln);
            BND_LAYER.push(ln);
        }
    }
    if ($("#chkRoad").prop('checked') == true) {
        var RDS = data[city].RDS;
        for (i = 0; i < RDS.length; i++) {
            var ln = createLine(getLatLongArr(RDS[i]));
            ln.setOptions({ strokeWeight: 4, strokeColor: '#FDFEFE' });
            maplayer.push(ln);
            RDS_LAYER.push(ln);
        }
    }
    if ($("#chkSurveyPoint").prop('checked') == true) {
        var SRV = data[city].SRV;
        for (i = 0; i < SRV.length; i++) {
            var pt = createPoint(getLatLongArr(SRV[i])[0]);
            maplayer.push(pt);
            SRV_LAYER.push(ln);
        }
    }
    if ($("#chkvegitation").prop('checked') == true) {
        var VEG = data[city].VEG;
        for (i = 0; i < VEG.length; i++) {
            var ln = createPolygon(getLatLongArr(VEG[i]));
            ln.setOptions({ fillColor: '#3AA03A', strokeColor: '#3AA03A', fillOpacity: 1 });
            maplayer.push(ln);
            VEG_LAYER.push(ln);
        }
    }
    //Water Connection
    if ($("#chkWaterSupplyLine").prop('checked') == true) {
        var WTR = data[city].WTR;
        for (i = 0; i < WTR.length; i++) {
            var ln = createPolygon(getLatLongArr(WTR[i]));
            ln.setOptions({strokeWeight: 3, strokeColor: '#8BBDEB' });
            maplayer.push(ln);
            WTR_LAYER.push(ln);
        }
    }
    if ($("#chkutitlity").prop('checked') == true) {
        var misc = data[city].MISC;
        for (i = 0; i < misc.length; i++) {
            var ln = createPolygon(getLatLongArr(misc[i]));
            //AD
            //-----------------------------
            // console.log(misc[i].toString());
            ln.id = misc[i].toString();
            //-----------------------------
            ln.setOptions({ fillColor: '#D35400', strokeColor: '#666666', fillOpacity: 0.8, clickable: !0 }); //        poly.setOptions({ fillColor: clr, clickable: !0 });


            //google.maps.event.addListener(ln, 'click', function () {
            //    console.log(this.id);
            //});

            maplayer.push(ln);
            MISC_LAYER.push(ln);
        }
    }

    if ($("#chkElectricityPole").prop('checked') == true) {
        var e_pole = data[city].EPOLE;
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
    if ($("#chkLampPost").prop('checked') == true) {
        var lamp_post = data[city].LAMPPOST;
        for (i = 0; i < lamp_post.length; i++) {
            var pt = createPoint(getLatLongArr(lamp_post[i])[0]);
            pt.setIcon('images/lamp.png');
            maplayer.push(pt);
            LAMPPOST_LAYER.push(pt);
        }
    }

    if ($("#chkManHoles").prop('checked') == true) {
        var manhole = data[city].MANHOLE;
        for (i = 0; i < manhole.length; i++) {
            var pt = createPoint(getLatLongArr(manhole[i])[0]);
            pt.setIcon('images/manhole.png');
            maplayer.push(pt);
            MANHOLE_LAYER.push(pt);
        }
    }
    if ($("#chkElectricityLine").prop('checked') == true) {
        var e_line = data[city].ELINE;
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
    if ($("#chkRailwayTrack").prop('checked') == true) {
        var RAilWay = data[city].RAILWAY;
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

    if ($("#chkBoundary").prop('checked') == true) {
        var BRD = data[city].BRD;
        for (i = 0; i < BRD.length; i++) {
            var ln = createLine(getLatLongArr(BRD[i]));
            ln.setOptions({ strokeWeight: 3, strokeColor: '#4679BD' });
            maplayer.push(ln);
            BRD_LAYER.push(ln);
        }
    }
    var plots = data[city].PLT;
    for (i = 0; i < plots.length; i++) {
        var poly = createPolygon(getLatLongArr(plots[i][5]));
        poly.id = plots[i][0];//
        poly.plotno = plots[i][1];
        poly.area = plots[i][2];
        poly.sector = plots[i][6] || 1;
        poly.city_name = fullcity;
        var clr = getColor(city, poly.plotno, poly.sector, fullcity);
        poly.setOptions({ fillColor: clr, clickable: !0 });

        google.maps.event.addListener(poly, 'mouseover', function () {
            this.setOptions({ strokeWeight: 2 });
        });

        google.maps.event.addListener(poly, 'mouseout', function () {
            this.setOptions({ strokeWeight: 1 });
        });
        google.maps.event.addListener(map, 'mousemove', function (event) {
            displayCoordinates(event.latLng);
        });

        google.maps.event.addListener(poly, 'click', function () {
            // alert(this.id);//
            // console.log(this.id);
            getInfo(this.plotno, this.area, this.sector, this.city_name);
        });
        
    //for Filter added on 13/5/2016 
        //For Vacant
        if (clr == '#CCD1D1') {
            VACANTPLOT_LAYER.push(poly);
            VACANT_PLOTS.push(plots[i]);
        }

            //for Industrial
        else if (clr == '#6C6C6C') {
            Industrial_LAYER.push(poly);
            Industrial_PLOTS.push(plots[i]);
        }
            //for Commercial
        else if (clr == '#008C89') {
            Commercial_LAYER.push(poly);
            Commercial_PLOTS.push(plots[i]);
        }
            //for Residential
        else if (clr == '#D4CF17') {
            Residential_LAYER.push(poly);
            Residential_PLOTS.push(plots[i]);
        }
            //for Institutional/Banking
        else if (clr == '#0000cc') {
            Institutional_Banking_LAYER.push(poly);
            Institutional_Banking_PLOTS.push(plots[i]);
        }
            //for Alloted
        else if (clr == '#F9E79F') {
            ALLOTEDPLOT_LAYER.push(poly);
            ALLOTED_PLOTS.push(plots[i]);
        }
            //for Under Construction
        else if (clr == '#3399ff') {
            UNDERCONSPLOT_LAYER.push(poly);
            UNDERCONS_PLOTS.push(plots[i]);
        }
            //for In Production
        else if (clr == '#BB8FCE') {
            INPRODUCTIONPLOT_LAYER.push(poly);
            INPRODUCTION_PLOTS.push(plots[i]);
        }
            //for No Status
        else if (clr == '#CCCC99') {
            NOSTATUSPLOT_LAYER.push(poly);
            NOSTATUS_PLOTS.push(plots[i]);
        }

        //
        
         maplayer.push(poly);

        if ($("#chkPlotNo").prop('checked') == true) {
            var lp = new google.maps.LatLng(parseFloat(plots[i][4]), parseFloat(plots[i][3]));
            var _label = new google.maps.Label({ visibleZoom: 17, hideZoom: 90, visible: true, map: map, cssName: 'nlLabel', position: lp, text: plots[i][1] });

            PLOT_NUMBER.push(_label);
            maplayer.push(_label);
            // PLT_LAYER.push(poly);
       }
        // PLT_LAYER.push(poly);
        
    }

    //map.setZoom(data[city].map.z);
    //map.panTo(new google.maps.LatLng(data[city].map.y, data[city].map.x));
}

function clearAll(maplayer) {
    for (i = 0; i < maplayer.length; i++) {
        maplayer[i].setMap(null);
    }
    maplayer = [];
}
function displayCoordinates(pnt) {
    var coordsLabel = document.getElementById("tdCursor");
    var lat = pnt.lat();
    lat = lat.toFixed(6);
    var lng = pnt.lng();
    lng = lng.toFixed(6);
    coordsLabel.innerHTML = lng + "," + lat;
}




$('#chkSector').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var SECTR = data[start_loc[x][0]].SCTR;

            for (i = 0; i < SECTR.length; i++) {
                var ln = createPolygon(getLatLongArr(SECTR[i][4]));
                ln.setOptions({ strokeWeight: 5, strokeColor: '#971C66', fillOpacity: 0, clickable: !1 });
                maplayer.push(ln);
                //FOR LAYER'S ON/OFF
                SECT_LAYER.push(ln);
                //
              //  SECT_LAYER.setMap(map);

                var lp = new google.maps.LatLng(parseFloat(SECTR[i][3]), parseFloat(SECTR[i][2]));
                var _label = new google.maps.Label({ visibleZoom: 13, hideZoom: 19, visible: true, map: map, cssName: 'sctrLbl', position: lp, text: SECTR[i][1] });
              
                //maplayer.push(_label);
                //maplayer.setMap(map);
            }
        }
    }
    else {
        for (i = 0; i < SECT_LAYER.length; i++) {
            SECT_LAYER[i].setMap(null);
        }

    }
});

$('#chkWaterBody ').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var RIVER = data[start_loc[x][0]].RIVER;
            for (i = 0; i < RIVER.length; i++) {
                var ln = createPolygon(getLatLongArr(RIVER[i]));
               ln.setOptions({strokeColor: '#0000ff', fillColor: '#0000ff',fillOpacity:0.8 });
               // maplayer.push(ln);
                RIVER_LAYER.push(ln);
             //   WTR_LAYER.setMap(map);
            }
        }
    }

    else {
        for (i = 0; i < RIVER_LAYER.length; i++) {
            RIVER_LAYER[i].setMap(null);
        }

    }
});

$('#chkWaterSupplyLine').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var WTR = data[start_loc[x][0]].WTR;
            for (i = 0; i < WTR.length; i++) {
                var ln = createLine(getLatLongArr(WTR[i]));
                ln.setOptions({ fillColor: '#8BBDEB', strokeColor: '#8BBDEB', fillOpacity: 0.8});
               // maplayer.push(ln);
                //FOR LAYER'S ON/OFF
                WTR_LAYER.push(ln);
                //RIVER_LAYER.setMap(map);
                //
            }
        }
    }

    else {
        for (i = 0; i < WTR_LAYER.length; i++) {
            WTR_LAYER[i].setMap(null);
        }

    }
});

$('#chkFootPath').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var FTP = data[start_loc[x][0]].FTP;
            for (i = 0; i < FTP.length; i++) {
                var ln = createPolygon(getLatLongArr(FTP[i]));
                ln.setOptions({ fillColor: '#FFFFFF', strokeColor: '#6E6E6E', fillOpacity: 1 });
               // maplayer.push(ln);
                //FOR LAYER'S ON/OFF
                FTP_LAYER.push(ln);
                //
               // FTP_LAYER.setMap(map);
            }
        }
    }

    else {
        for (i = 0; i < FTP_LAYER.length; i++) {
            FTP_LAYER[i].setMap(null);
        }

    }
});

$('#chkutitlity').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var misc = data[start_loc[x][0]].MISC;
            for (i = 0; i < misc.length; i++) {
                var ln = createPolygon(getLatLongArr(misc[i]));
                //AD
                //-----------------------------
                // console.log(misc[i].toString());
                ln.id = misc[i].toString();
                //-----------------------------
                ln.setOptions({ fillColor: '#D35400', strokeColor: '#666666', fillOpacity: 0.8, clickable: !0 }); //        poly.setOptions({ fillColor: clr, clickable: !0 });


                //google.maps.event.addListener(ln, 'click', function () {
                //    console.log(this.id);
                //});

               // maplayer.push(ln);
                MISC_LAYER.push(ln);
               // MISC_LAYER.setMap(map);
                //maplayer.setMap(ln);
            }
        }
    }

    else {
        for (i = 0; i < MISC_LAYER.length; i++) {
            MISC_LAYER[i].setMap(null);
        }

    }
});

$('#chkvegitation').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var VEG = data[start_loc[x][0]].VEG;
            for (i = 0; i < VEG.length; i++) {
                var ln = createPolygon(getLatLongArr(VEG[i]));
                ln.setOptions({ fillColor: '#3AA03A', strokeColor: '#3AA03A', fillOpacity: 1 });
               // maplayer.push(ln);
                VEG_LAYER.push(ln);
               // VEG_LAYER.setMap(map);
            }
        }
    }

    else {
        for (i = 0; i < VEG_LAYER.length; i++) {
            VEG_LAYER[i].setMap(null);
        }

    }
});
$('#chkBoundary').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var BND = data[start_loc[x][0]].BND;
            for (i = 0; i < BND.length; i++) {
                var ln = createLine(getLatLongArr(BND[i]));
                ln.setOptions({ strokeWeight: 1, strokeColor: ' #000000' });
              //  maplayer.push(ln);
                BND_LAYER.push(ln);
               // BND_LAYER.setMap(map);
            }
        }
    }

    else {
        for (i = 0; i < BND_LAYER.length; i++) {
            BND_LAYER[i].setMap(null);
        }

    }
});

//$('#chkRoad').on('change', function () {
 //   if (this.checked) // if changed state is "CHECKED"
  //  {
//        for (x = 0; x < start_loc.length; x++) {

           

//            var RDS = data[start_loc[x][0]].RDS;
//            for (i = 0; i < RDS.length; i++) {
//                var ln = createLine(getLatLongArr(RDS[i]));
//                ln.setOptions({ strokeWeight: 4, strokeColor: '#FDFEFE' });
              
//                RDS_LAYER.push(ln);
               
//            }
//        }
//    }

//    else {
//        for (i = 0; i < RDS_LAYER.length; i++) {
//            RDS_LAYER[i].setMap(null);
//        }

//    }
//});

$('#chkSurveyPoint').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var SRV = data[start_loc[x][0]].SRV;
            for (i = 0; i < SRV.length; i++) {
                var pt = createPoint(getLatLongArr(SRV[i])[0]);
               // maplayer.push(pt);
                SRV_LAYER.push(pt);
               // SRV_LAYER.setMap(map);
            }
        }
    }

    else {
        for (i = 0; i < SRV_LAYER.length; i++) {
            SRV_LAYER[i].setMap(null);
        }

    }
});
$('#chkLampPost').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {
            
            // showPlots(start_loc[i][0]);

            var lamp_post = data[start_loc[x][0]].LAMPPOST;
            for (i = 0; i < lamp_post.length; i++) {
                var pt = createPoint(getLatLongArr(lamp_post[i])[0]);
                pt.setIcon('images/lamp.png');
               // maplayer.push(pt);
                LAMPPOST_LAYER.push(pt);
               // LAMPPOST_LAYER.setMap(map);
            }
        }
    }

    else {
        for (i = 0; i < LAMPPOST_LAYER.length; i++) {
            LAMPPOST_LAYER[i].setMap(null);
        }

    }
});

$('#chkManHoles').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var manhole = data[start_loc[x][0]].MANHOLE;
            for (i = 0; i < manhole.length; i++) {
                var pt = createPoint(getLatLongArr(manhole[i])[0]);
                pt.setIcon('images/manhole.png');
               // maplayer.push(pt);
                MANHOLE_LAYER.push(pt);
               // MANHOLE_LAYER.setMap(map);
            }
        }
    }

    else {
        for (i = 0; i < MANHOLE_LAYER.length; i++) {
            MANHOLE_LAYER[i].setMap(null);
        }

    }
});

$('#chkElectricityPole').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var e_pole = data[start_loc[x][0]].EPOLE;
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
                //maplayer.push(pt);
                EPOLE_LAYER.push(pt);
                //EPOLE_LAYER.setMap(map);
            }
        }
    }

    else {
        for (i = 0; i < EPOLE_LAYER.length; i++) {
            EPOLE_LAYER[i].setMap(null);
        }

    }
});

$('#chkElectricityLine').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);
            
            var e_line = data[start_loc[x][0]].ELINE;
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
               // ELINE_LAYER.setMap(map);
            }
        }
    }

    else {
        for (i = 0; i < ELINE_LAYER.length; i++) {
            ELINE_LAYER[i].setMap(null);
        }

    }
});
$('#chkRailwayTrack').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var RAilWay = data[start_loc[x][0]].RAILWAY;
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
               // maplayer.push(ln);
                RAILWAY_LAYER.push(ln);
                //RAILWAY_LAYER.setMap(map);
            }
        }
    }

    else {
        for (i = 0; i < RAILWAY_LAYER.length; i++) {
            RAILWAY_LAYER[i].setMap(null);
        }

    }
});



$('#chkCheckAllLayers').click(function (e) {
    $(this).closest('table').find('td input:checkbox').prop('checked', this.checked);
});

$('#chkCheckAllLayers').on('change', function () {
    if (this.checked) {       
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var SECTR = data[start_loc[x][0]].SCTR;

            for (i = 0; i < SECTR.length; i++) {
                var ln = createPolygon(getLatLongArr(SECTR[i][4]));
                ln.setOptions({ strokeWeight: 5, strokeColor: '#971C66', fillOpacity: 0, clickable: !1 });
                maplayer.push(ln);
                //FOR LAYER'S ON/OFF
                SECT_LAYER.push(ln);
                //
                //  SECT_LAYER.setMap(map);
                var lp = new google.maps.LatLng(parseFloat(SECTR[i][3]), parseFloat(SECTR[i][2]));
                var _label = new google.maps.Label({ visibleZoom: 13, hideZoom: 19, visible: true, map: map, cssName: 'sctrLbl', position: lp, text: SECTR[i][1] });
            }
                   
            var plots = data[start_loc[x][0]].PLT;

            for (i = 0; i < plots.length; i++) {

                //  SECT_LAYER.setMap(map);

                var lp = new google.maps.LatLng(parseFloat(plots[i][4]), parseFloat(plots[i][3]));
                var _label = new google.maps.Label({ visibleZoom: 15, hideZoom: 30, visible: true, map: map, cssName: 'nlLabel', position: lp, text: plots[i][1] });

                PLOT_NUMBER.push(_label);
                maplayer.push(_label);
                //maplayer.setMap(map);
            }
            var WTR = data[start_loc[x][0]].WTR;
            for (i = 0; i < WTR.length; i++) {
                var ln = createPolygon(getLatLongArr(WTR[i]));
                ln.setOptions({ fillColor: '#0000ff', strokeColor: '#0000ff', fillOpacity: 1 });
                // maplayer.push(ln);
                WTR_LAYER.push(ln);
                //   WTR_LAYER.setMap(map);
            }

        }
    }
    else {
        for (i = 0; i < SECT_LAYER.length; i++) {
            SECT_LAYER[i].setMap(null);
        }

        for (i = 0; i < PLOT_NUMBER.length; i++) {
            PLOT_NUMBER[i].setMap(null);
        }

        for (i = 0; i < WTR_LAYER.length; i++) {
            WTR_LAYER[i].setMap(null);
        }
    }
});

$('#chkCheckAllPlotStatus').click(function (e) {
    $(this).closest('table').find('td input:checkbox').prop('checked', this.checked);
});

$('#chkCheckAllPlotStatus').on('change', function () {
    if (this.checked) {
        
        for (i = 0; i < VACANTPLOT_LAYER.length; i++) {

            VACANTPLOT_LAYER[i].setMap(map);
        }
        

        for (i = 0; i < ALLOTEDPLOT_LAYER.length; i++) {

            ALLOTEDPLOT_LAYER[i].setMap(map);

        }
        
        for (i = 0; i < UNDERCONSPLOT_LAYER.length; i++) {

            UNDERCONSPLOT_LAYER[i].setMap(map);
        }        

        for (i = 0; i < INPRODUCTIONPLOT_LAYER.length; i++) {

            INPRODUCTIONPLOT_LAYER[i].setMap(map);
        }        

        for (i = 0; i < NOSTATUSPLOT_LAYER.length; i++) {

            NOSTATUSPLOT_LAYER[i].setMap(map);
        }
    }
    
    else {
      
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

$('#chkVacant').on('change', function () {

    if (this.checked) {

        for (i = 0; i < VACANTPLOT_LAYER.length; i++) {

            VACANTPLOT_LAYER[i].setMap(map);
        }

    }
    else
    {
        
        for (i = 0; i < VACANTPLOT_LAYER.length; i++) {
            VACANTPLOT_LAYER[i].setMap(null);
        }
    }
});

$('#chkAlloted').on('change', function () {
    if (this.checked) {

        for (i = 0; i < ALLOTEDPLOT_LAYER.length; i++) {
            ALLOTEDPLOT_LAYER[i].setMap(map);
        }

    }
    else {
       
        for (i = 0; i < ALLOTEDPLOT_LAYER.length; i++) {
            ALLOTEDPLOT_LAYER[i].setMap(null);
        }
    }
});

$('#chkUnderConstruction').on('change', function () {
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

$('#chkInProduction').on('change', function () {

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

$('#chkNoStatus').on('change', function () {
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

$('#chkCheckAllPlotType').click(function (e) {
    $(this).closest('table').find('td input:checkbox').prop('checked', this.checked);
});

$('#chkCheckAllPlotType').on('change', function () {

    if (this.checked) {

        for (i = 0; i < Industrial_LAYER.length; i++) {

            Industrial_LAYER[i].setMap(map);
        }


        for (i = 0; i < Commercial_LAYER.length; i++) {

            Commercial_LAYER[i].setMap(map);

        }

        for (i = 0; i < Residential_LAYER.length; i++) {

            Residential_LAYER[i].setMap(map);
        }

        for (i = 0; i < Institutional_Banking_LAYER.length; i++) {

            Institutional_Banking_LAYER[i].setMap(map);
        }

        for (i = 0; i < NOSTATUSPLOT_LAYER.length; i++) {

            NOSTATUSPLOT_LAYER[i].setMap(map);
        }
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

        
        for (i = 0; i < NOSTATUSPLOT_LAYER.length; i++) {
            NOSTATUSPLOT_LAYER[i].setMap(null);
        }
    }





});

$('#chkIndustrialPlot').on('change', function () {

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

$('#chkCommercialPlot').on('change', function () {
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

$('#chkResidentialPlot').on('change', function () {
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

$('#chkInstitutional').on('change', function () {
    if (this.checked) {

        for (i = 0; i < Institutional_Banking_LAYER.length; i++) {

            Institutional_Banking_LAYER[i].setMap(map);
        }

    }
    else {

        for (i = 0; i < Institutional_Banking_LAYER.length; i++) {
            Institutional_Banking_LAYER[i].setMap(null);
        }
    }
});
$('#chkPlotNo').on('change', function () {
    if (this.checked) // if changed state is "CHECKED"
    {
        for (x = 0; x < start_loc.length; x++) {

            // showPlots(start_loc[i][0]);

            var plots = data[start_loc[x][0]].PLT;

            for (i = 0; i < plots.length; i++) {

                //  SECT_LAYER.setMap(map);

                var lp = new google.maps.LatLng(parseFloat(plots[i][4]), parseFloat(plots[i][3]));
                var _label = new google.maps.Label({ visibleZoom: 15, hideZoom: 30, visible: true, map: map, cssName: 'nlLabel', position: lp, text: plots[i][1] });

                PLOT_NUMBER.push(_label);
                maplayer.push(_label);
                //maplayer.setMap(map);
            }
        }
    }
    else {
        for (i = 0; i < PLOT_NUMBER.length; i++) {
            PLOT_NUMBER[i].setMap(null);
        }

    }
});