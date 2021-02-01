function getWelfareindex(Charttype, Year, Index) {
    ajaxReq(
            'Handler.ashx',
            'getWelfareindex',
            { "Year": Year, "Index": Index },
            function (resp)
            { showWelfareIndex(resp, Charttype, Year, Index) },
            true
           );
}


function getWelfareindex3(Charttype3, Index3) {
    ajaxReq(
            'Handler.ashx',
            'getWelfareindex3',
            { "Index": Index3 },
            function (resp)
            { showWelfareIndex3(resp, Charttype3, Index3) },
            true
           );
}


function getWelfareindex4(Charttype4, Index4) {
    ajaxReq(
            'Handler.ashx',
            'getWelfareindex4',
            { "Index": Index4 },
            function (resp)
            { showWelfareIndex4(resp, Charttype4, Index4) },
            true
           );
}

function getWelfareindex1(Charttype1,Index1) {
    ajaxReq(
            'Handler.ashx',
            'getWelfareindex1',
            {"Index": Index1 },
            function (resp)
            { showWelfareIndex1(resp, Charttype1, Index1) },
            true
           );
}

function getWelfareindex2(Charttype2, Index2) {
    ajaxReq(
            'Handler.ashx',
            'getWelfareindex2',
            { "Index": Index2 },
            function (resp)
            { showWelfareIndex2(resp, Charttype2, Index2) },
            true
           );
}
$(document).ready(function () {
    $("#MenuRoot").click(function () {
        $("#Menu").hide();
        $("#MenuRoot").hide();
        $("#Menu1").hide();
    });
    $("#BackbtPlotStaus").click(function () {
        $("#Menu1").hide();
        $("#Menu").show();
    });

    $("#btnBackForZone").click(function () {
        $("#Menu").hide();
        $("#MenuRoot").hide();
        $("#Menu1").hide();
    });

    $("#btnBackForHousing").click(function () {
        $("#Menu1").show();
        $("#MenuRoot").hide();
        $("#Menu2").hide();
      


    });



})

var i = 1;
function showWelfareIndex(resp, Charttype, Year, Index) {
    if (resp.status === true) {

        if (Index === "All") {
            Index = "CWC"
        }
        var value = Year;
        
        var options = {
            theme: "theme1",
            exportEnabled: true,
            zoomEnabled: true,
            height: 348,
            title: {
                text: "Sector -"+Index,
                fontSize: 12,
                fontFamily: "verdana"
            },
            animationEnabled: true,
            bubble: {
                textStyle: {
                    fontSize: 30,
                    fontName: 'Times-Roman',
                    color: 'green',
                    bold: true,
                    italic: true
                }
            },
            legend: {
                horizontalAlign: "center", // "center" , "right"
                verticalAlign: "bottom",  // "top" , "bottom"
                fontSize: 15
            },


            data: [
        {
            click: function (e) {
                //var Location = $('#ddlLocationLand option:selected').val();
                //var RoleRecno = $('#hdnRoleRecno').val();
                //var Year = $('#ddlYearLand option:selected').val();
                //var Month;

                //if (Year != "All") {
                //    Month = $('#ddlMonthLand option:selected').val();
                //}
                //else {
                //    Month = "All";
                //}
                var Location = "MIG"
                var url = "DashboardGrid/DashboardDetails.aspx?loc=" + Location;
                window.open(url, '_blank');
                //window.location.href = "Process/AllotmentProcess.aspx?loc=" + Location + "&year=" + Year + "&Month=" + Month + "&Data=" + e.dataPoint.label;
            },
            indexLabel: "{y}",
            maximum: 100,
            valueFormatString: "00.00",
            indexLabelPlacement: "outside",
            indexLabelFontSize: 12,
            showInLegend: true,
            name: "",
            type: Charttype, //change it to line, area, column, pie, etc
            dataPoints: [
                { label: "MIG", name: "MIG", y: parseFloat(resp.data["MIG"]), z: 1 },
                { label: "LIG", name: "LIG", y: parseFloat(resp.data["LIG"]), z: 1 },
                { label: "NOC", name: "NOC", y: parseFloat(resp.data["NOC"]), z: 1 },
                { label: "EWS", name: "EWS", y: parseFloat(resp.data["EWS"]), z: 1 }
            ]
        }
            ],
            axisX: {
                labelFontSize: 10, 
                fontFamily: "Bold",
                labelAngle: 20,
                title: "Flat Category Detail",
            },
            axisY: {
                labelFontSize: 10,
                fontFamily: "Bold",
                labelAngle: 20,
                title: " In Number",
                interval: 100,

            }
        };

    
            $("#Wel_index_chart").CanvasJSChart(options);
        }
    
}



function showWelfareIndex1(resp, Charttype1, Index1) {
    //console.log(resp)
    if (resp.status === true) {

        if (Index1 === "All") {
            Index1 = "CWC"
        }
       
        var options = {
            theme: "theme1",
            exportEnabled: true,
            zoomEnabled: true,
            height: 348,
            title: {
                text: "Sector -" + Index1,
                fontSize: 12,
                fontFamily: "verdana"
            },
            animationEnabled: true,
            bubble: {
                textStyle: {
                    fontSize: 30,
                    fontName: 'Times-Roman',
                    color: 'green',
                    bold: true,
                    italic: true
                }
            },
            legend: {
                horizontalAlign: "center", // "center" , "right"
                verticalAlign: "bottom",  // "top" , "bottom"
                fontSize: 15
            },


            data: [
        {
            indexLabel: "{y}",
            maximum: 100,
            valueFormatString: "00.00",
            indexLabelPlacement: "outside",
            indexLabelFontSize: 12,
            showInLegend: true,
            name: "",
            type: Charttype1, //change it to line, area, column, pie, etc
            dataPoints: [
                { label: "Vacant", name: "Vacant", y: parseFloat(resp.data["Vacant"]), z: 1 },
                { label: "Cancel", name: "Cancel", y: parseFloat(resp.data["Cancel"]), z: 1 },
                { label: "Allotment", name: "Allotment", y: parseFloat(resp.data["Allotment"]), z: 1 },
                 { label: "Sold", name: "Sold", y: parseFloat(resp.data["Sold"]), z: 1 },
                { label: "Freehold", name: "Freehold", y: parseFloat(resp.data["Freehold"]), z: 1 }
            ]
        }
            ],
            axisX: {
                labelFontSize: 10,
                fontFamily: "Bold",
                labelAngle: 20,
                title: "Plot Category Type Detail",
            },
            axisY: {
                labelFontSize: 10,
                fontFamily: "Bold",
                labelAngle: 20,
                title: " In Number",
                interval: 100,

            }
        };


        $("#Plot_type_Chart").CanvasJSChart(options);
    }

}

function showWelfareIndex2(resp, Charttype2, Index2) {
    //console.log(resp)
    if (resp.status === true) {

        if (Index2 === "All") {
            Index2 = "CWC"
        }
        var options = {
            theme: "theme1",
            exportEnabled: true,
            zoomEnabled: true,
            height: 348,
            title: {
                text: "Sector -" + Index2,
                fontSize: 12,
                fontFamily: "verdana"
            },
            animationEnabled: true,
            bubble: {
                textStyle: {
                    fontSize: 30,
                    fontName: 'Times-Roman',
                    color: 'green',
                    bold: true,
                    italic: true
                }
            },
            legend: {
                horizontalAlign: "center", // "center" , "right"
                verticalAlign: "bottom",  // "top" , "bottom"
                fontSize: 15
            },


            data: [
        {
            click: function (e) {
                //i++
                //if (i % 2 == 0) {
                //    $("#PlotType").show();
                //}
                //else if (i % 2 == 1) {
                //    $("#PlotType").hide();
                //}
                $("#MenuRoot").show();
                $("#Menu2").show();

            },
            indexLabel: "{y}",
            maximum: 100,
            valueFormatString: "00.00",
            indexLabelPlacement: "outside",
            indexLabelFontSize: 12,
            showInLegend: true,
            name: "",
            type: Charttype2, //change it to line, area, column, pie, etc
            dataPoints: [
                { label: "Commercial", name: "Commercial", y: parseFloat(resp.data["Commercial"]), z: 1 },
                { label: "Institution", name: "Institution", y: parseFloat(resp.data["Institution"]), z: 1 },
                { label: "Imm", name: "Imm", y: parseFloat(resp.data["Imm"]), z: 1 },
                { label: "Housing", name: "Housing", y: parseFloat(resp.data["Housing"]), z: 1 },
                { label: "Land Sale Branch", name: "Land Sale Branch", y: parseFloat(resp.data["Land Sale Branch"]), z: 1 },
                { label: "Lease Administrative Branch", name: "Lease Administrative Branch", y: parseFloat(resp.data["Lease Administrative Branch"]), z: 1 }
            ]
        }
            ],
            axisX: {
                labelFontSize: 10,
                fontFamily: "Bold",
                labelAngle: 20,
                title: "Plot Status Detail",
            },
            axisY: {
                labelFontSize: 10,
                fontFamily: "Bold",
                labelAngle: 20,
                title: " In Number",
                interval: 100,

            }
        };


        $("#Plot_Status").CanvasJSChart(options);
    }

}

function showWelfareIndex3(resp, Charttype3, Index3) {
    //console.log(resp)
    if (resp.status === true) {

        if (Index3 === "All") {
            Index3 = "CWC"
        }

        var options = {
            theme: "theme1",
            exportEnabled: true,
            zoomEnabled: true,
            height: 348,
            title: {
                text: " Rohini Zone",
                fontSize: 12,
                fontFamily: "verdana"
            },
            animationEnabled: true,
            bubble: {
                textStyle: {
                    fontSize: 30,
                    fontName: 'Times-Roman',
                    color: 'green',
                    bold: true,
                    italic: true
                }
            },
            legend: {
                horizontalAlign: "center", // "center" , "right"
                verticalAlign: "bottom",  // "top" , "bottom"
                fontSize: 15
            },


            data: [
        {
            click: function (e) {
                //i++
                //if (i % 2 == 0) {
                //    $("#PlotType").show();
                //}
                //else if (i % 2 == 1) {
                //    $("#PlotType").hide();
                //}
                $("#MenuRoot").show();
                $("#Menu").show();

            },
            indexLabel: "{y}",
            maximum: 100,
            valueFormatString: "00.00",
            indexLabelPlacement: "outside",
            indexLabelFontSize: 12,
            showInLegend: true,
            name: "",
            type: Charttype3, //change it to line, area, column, pie, etc
            dataPoints: [
                { label: "Alloted Area", name: "Alloted Area", y: parseFloat(resp.data["Alloted Area"]), z: 1 },
                { label: "Vacant Area", name: "Vacant Area", y: parseFloat(resp.data["Vacant Area"]), z: 1 },
                 { label: "Land Area", name: "Land Area", y: parseFloat(resp.data["Land Area"]), z: 1 }
            ]
        }
            ],
            axisX: {
                labelFontSize: 10,
                fontFamily: "Bold",
                labelAngle: 20,
                title: "DDA Land Information",
            },
            axisY: {
                labelFontSize: 10,
                fontFamily: "Bold",
                labelAngle: 20,
                title: " In Acres",
                interval:  100000,

            }
        };


        $("#DdaLandDetails").CanvasJSChart(options);
    }

}

function showWelfareIndex4(resp, Charttype4, Index4) {
    //console.log(resp)
    if (resp.status === true) {

        if (Index4 === "All") {
            Index4 = "CWC"
        }

        var options = {
            theme: "theme1",
            exportEnabled: true,
            zoomEnabled: true,
            height: 348,
            title: {
                text: "Sector -" + Index4,
                fontSize: 12,
                fontFamily: "verdana"
            },
            animationEnabled: true,
            bubble: {
                textStyle: {
                    fontSize: 30,
                    fontName: 'Times-Roman',
                    color: 'green',
                    bold: true,
                    italic: true
                }
            },
            legend: {
                horizontalAlign: "center", // "center" , "right"
                verticalAlign: "bottom",  // "top" , "bottom"
                fontSize: 15
            },


            data: [
        {
            click: function (e) {
                //i++
                //if (i % 2 == 0) {
                //    $("#PlotType").show();
                //}
                //else if (i % 2 == 1) {
                //    $("#PlotType").hide();
                //}
                $("#MenuRoot").show();
                $("#Menu").hide();
                $("#Menu1").show();

            },
            
            indexLabel: "{y}",
            maximum: 100,
            valueFormatString: "00.00",
            indexLabelPlacement: "outside",
            indexLabelFontSize: 12,
            showInLegend: true,
            name: "",
            type: Charttype4, //change it to line, area, column, pie, etc
            dataPoints: [
                 { label: "Rohini Zone (H)", name: "Total_Land_Area", y: parseFloat(resp.data["Rohini Zone (H)"]), z: 1 },
                 { label: "Rohini Zone (M)", name: "Rohini Zone (M)", y: parseFloat(resp.data["Rohini Zone (M)"]), z: 1 },
                 { label: "Zone k-I", name: "Zone k-I", y: parseFloat(resp.data["Zone k-I"]), z: 1 },
                 { label: "Zone k-II", name: "Zone k-II", y: parseFloat(resp.data["Zone k-II"]), z: 1 },
                 { label: "Zone P-I", name: "Zone P-I", y: parseFloat(resp.data["Zone P-I"]), z: 1 },
                 { label: "Zone A", name: "Zone A", y: parseFloat(resp.data["Zone A"]), z: 1 },
                 { label: "Zone B", name: "Zone B", y: parseFloat(resp.data["Zone B"]), z: 1 },
                 { label: "Zone C", name: "Zone C", y: parseFloat(resp.data["Zone C"]), z: 1 },
                 { label: "Zone D", name: "Zone D", y: parseFloat(resp.data["Zone D"]), z: 1 },
                 { label: "Zone E", name: "Zone E", y: parseFloat(resp.data["Zone E"]), z: 1 },
                 { label: "Zone F", name: "Zone F", y: parseFloat(resp.data["Zone F"]), z: 1 },
                 { label: "Zone G", name: "Zone G", y: parseFloat(resp.data["Zone G"]), z: 1 },
                 { label: "Zone H", name: "Zone H", y: parseFloat(resp.data["Zone H"]), z: 1 },
                 { label: "Zone L", name: "Zone L", y: parseFloat(resp.data["Zone L"]), z: 1 },
                 { label: "Zone O", name: "Zone O", y: parseFloat(resp.data["Zone O"]), z: 1 },
                 { label: "Zone UC& J", name: "Zone UC& J", y: parseFloat(resp.data["Zone UC& J"]), z: 1 }

            ]
        }
            ],
            axisX: {
                labelFontSize: 10,
                fontFamily: "Bold",
                labelAngle: 20,
                title: "DDA Land Zone Information",
            },
            axisY: {
                labelFontSize: 10,
                fontFamily: "Bold",
                labelAngle: 20,
                title: " In Acres",
                interval: 100,

            }
        };


        $("#PlotZonewise").CanvasJSChart(options);
    }

}


function ajaxReq(handler, reqType, data, callbackFun, asyc) {
    if (asyc) asyc = true; else asyc = false;
    $.ajax({
        url: handler + "?request=" + reqType,
        type: "post",
        async: asyc,
        data: data,
        error: function () {
            alert("Oops! Something went wrong.");
        }
    }).done(callbackFun);
}