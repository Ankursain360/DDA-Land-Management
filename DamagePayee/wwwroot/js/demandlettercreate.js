$(function () {

    $("#InterestAmount,#Penalty,#DamageCharges,#ReliefAmount").keyup(function () {
        var InterestAmount = $('#InterestAmount').val();
        if (InterestAmount) {

        } else {
            InterestAmount = 0;
        }
        var Penalty = $('#Penalty').val();


        var DamageCharges = $('#DamageCharges').val();
        var ReliefAmount = $('#ReliefAmount').val();
        if (Penalty && DamageCharges && ReliefAmount) {
            var total = parseFloat(InterestAmount) + parseFloat(Penalty) + parseFloat(DamageCharges) - parseFloat(ReliefAmount);
            $("#DepositDue").val(total);

        }
    });
});
//var Area = $('#Area').val();
//if (Area == "") {
//    checkresult = false;
//    $("#AreaMsg").show();
//} else {
//    $("#AreaMsg").hide();
//    checkresult = true;
//}
$(function () {
    /*debugger;*/
    $("#txtfileno").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/DemandsLetter/AutoComplete/',
                data: { "prefix": request.term },
                type: "POST",
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.fileNo, value: item.id };
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
            $("#txtfileno").val(ui.item.label);
            return false;
        },
        select: function (event, ui) {
            event.preventDefault();
            $("#txtfileno").val(ui.item.label);
            
            HttpGet(`/DemandsLetter/GetFileDetails?fileid=${parseInt(ui.item.value)}`, 'json', function (response) {
                //showDisBoundariesVillage(response[0].polygon, response[0].xcoordinate, response[0].ycoordinate, response[0].id);
                $("#PropertyNo").val(response.propertyNo);
                $("#Area").val(response.plotAreaSqYard);
                $("#Name").val(response.payeeName);
                $("#FatherName").val(response.fatherName);
                $("#Address").val(response.address);
            });
        },
        minLength: 1
    });
});
$("#btnCalculate").click(function () {
    //var IsValid = $("#frmUserPersonalInfo").valid(); 
    debugger;
    var checkresult = false;
    var PropertyTypeId = $('#PropertyTypeId option:selected').val();
    if (PropertyTypeId < 1) {
        $("#PropertyTypeIdMsg").show();
    } else {
        $("#PropertyTypeIdMsg").hide();
        checkresult = true;
    }
    var LocalityId = $('#LocalityId option:selected').val();
    if (LocalityId < 1) {
        $("#LocalityIdMsg").show();
    } else {
        $("#LocalityIdMsg").hide();
        checkresult = true;
    }
    var EncroachmentDate = $('#EncroachmentDate').val();
    if (EncroachmentDate == "") {
        $("#EncroachmentDateMsg").show();
    } else {
        $("#EncroachmentDateMsg").hide();
        checkresult = true;
    }

    var StartDate = $('#DemandPeriodFromDate').val();
    if (StartDate == "") {
        checkresult = false;
        $("#FromDateMsg").show();
    } else {
        $("#FromDateMsg").hide();
        checkresult = true;
    }
    var EndDate = $('#DemandPeriodToDate').val();
    if (EndDate == "") {
        checkresult = false;
        $("#ToDateMsg").show();
    } else {
        $("#ToDateMsg").hide();
        checkresult = true;
    }

    var Area = $('#Area').val();
    if (Area == "") {
        checkresult = false;
        $("#AreaMsg").show();
    } else {
        $("#AreaMsg").hide();
        checkresult = true;
    }

    if (LocalityId < 1 || PropertyTypeId < 1 || EncroachmentDate == "" || StartDate == "" || EndDate == "" || Area == "") {

        checkresult = false;
    }
    else {
        checkresult = true;
    }
    if (checkresult) {
        var param = GetSearchParam();
        HttpPost(`/DamageCalculator/DamageCalculate`, 'html', param, function (response) {
            $('#LoadView').html("");
            $('#LoadView').html(response);
        });
    }

});
function GetSearchParam() {
    var model = {
        PropertyTypeId: $("#PropertyTypeId").children("option:selected").val(),
        EncroachmentDate: $("#EncroachmentDate").val(),
        FromDate: $("#DemandPeriodFromDate").val(),
        ToDate: $("#DemandPeriodToDate").val(),
        LocalityId: $("#LocalityId").children("option:selected").val(),
        Area: $("#Area").val(),
    }
    debugger
    return model;
}