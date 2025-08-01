﻿
var currentPageNumber = 1;
var currentPageSize = 5;


$(document).ready(function () {
    var id = $("#AwardMasterId").val();
    $("#VillageId").val('');
    $("#KhasraId").val('');
    var param = {
        NotificationId: id,
        pageSize: parseInt(currentPageSize),
        pageNumber: parseInt(currentPageNumber)
    }

    if (id) {
      
        HttpPost(`/AwardPlotDetails/NotificationView/`, 'html', param, function (response) {


            $('#divnotificationTable').html("");
            $('#divnotificationTable').html(response);

        });

    }
});
function GetDivision(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/AwardPlotDetails/NotificationView/`, 'html', param, function (response) {
        $('#divnotificationTable').html("");
        $('#divnotificationTable').html(response);

    });
}

function GetSearchParam(pageNumber, pageSize) {

    var model = {
        AwardMasterId: $('#AwardMasterId').val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }

    return model;
}






$("#AwardMasterId").change(function () {

    var id = $(this).val();

    var model = {
        AwardMasterId: id,
        pageSize: parseInt(currentPageSize),
        pageNumber: parseInt(currentPageNumber)
    }
    //alert(JSON.stringify(model));
    if (id) {
        HttpPost(`/AwardPlotDetails/NotificationView/`, 'html', model, function (response) {
            $('#divnotificationTable').html("");
            $('#divnotificationTable').html(response);

        });

    }
});








function onChange(id) {

    HttpGet(`/AwardPlotDetails/GetKhasraList/?villageId=${id}`, 'json', function (response) {
        var html = '<option value=""> select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

        // $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};
$("#KhasraId").change(function () {
    var kid = $(this).val();
  
    if (kid) {
        HttpGet(`/AwardPlotDetails/GetKhasraAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#ABigha").val(response.bigha);
            $("#ABiswa").val(response.biswa);
            $("#ABiswanshi").val(response.biswanshi);

            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);
            // alert(JSON.stringify(response));
        });

    }
});

$("#Bigha").keyup(function () {

    var Bigha_value = $(this).val();
    var Bigha1_value = $("#ABigha").val();
    if (parseFloat(Bigha_value) > parseFloat(Bigha1_value)) {
        alert("Used Area is not greater than Actual Area");
        $(this).val('');
        return false;
    }

});
$("#Biswa").keyup(function () {
    var Biswa_value = $(this).val();
    var Biswa1_value = $("#ABiswa").val();
    if (parseFloat(Biswa_value) > parseFloat(Biswa1_value)) {
        alert("Used Area is not greater than Actual Area");
        $(this).val('');
        return false;
    }

});

$("#Biswanshi").keyup(function () {

    var Biswanshi_value = $(this).val();
    var Biswanshi1_value = $("#ABiswanshi").val();
    if (parseFloat(Biswanshi_value) > (Biswanshi1_value)) {
        alert("Used Area is not greater than Actual Area");
        $(this).val('');
        return false;
    }

});



function onPaging(pageNo) {
    GetDivision(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDivision(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}


