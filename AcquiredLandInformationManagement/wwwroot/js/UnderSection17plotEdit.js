﻿var currentPageNumber = 1;
var currentPageSize = 5;


$(document).ready(function () {
    //
    var id = $("#UnderSection17Id").val();

   
    var param = {
        NotificationId: id,
        pageSize: parseInt(currentPageSize),
        pageNumber: parseInt(currentPageNumber)
    }

    if (id) {

        HttpPost(`/Undersection17plotdetail/Notification4View/`, 'html', param, function (response) {


            $('#divnotification17Table').html("");
            $('#divnotification17Table').html(response);

        });

    }

    //
    var kid = $("#KhasraId").val();

    if (kid) {
        HttpGet(`/Undersection17plotdetail/GetAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha1").val(response.bigha);
            $("#Biswa1").val(response.biswa);
            $("#Biswanshi1").val(response.biswanshi);


        });

    }

});
function GetUS17(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Undersection17plotdetail/Notification4View/`, 'html', param, function (response) {
        $('#divnotification17Table').html("");
        $('#divnotification17Table').html(response);

    });
}

function GetSearchParam(pageNumber, pageSize) {

    var model = {
        NotificationId: $('#UnderSection17Id').val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }

    return model;
}






$("#UnderSection17Id").change(function () {

    var id = $(this).val();

    var model = {
        NotificationId: id,
        pageSize: parseInt(currentPageSize),
        pageNumber: parseInt(currentPageNumber)
    }
    //alert(JSON.stringify(model));
    if (id) {
        HttpPost(`/Undersection17plotdetail/Notification4View/`, 'html', model, function (response) {
            $('#divnotification17Table').html("");
            $('#divnotification17Table').html(response);

        });

    }
});








function onChange(id) {

    HttpGet(`/Undersection17plotdetail/GetKhasraList/?villageId=${id}`, 'json', function (response) {
        var html = '<option value=""> select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

         $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};
$("#KhasraId").change(function () {
    var kid = $(this).val();
    if (kid) {
        HttpGet(`/Undersection17plotdetail/GetAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha1").val(response.bigha);
            $("#Biswa1").val(response.biswa);
            $("#Biswanshi1").val(response.biswanshi);

            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);
            // alert(JSON.stringify(response));
        });

    }
});

$("#Bigha").keyup(function () {
    var Bigha_value = $(this).val();
    var Bigha1_value = $("#Bigha1").val();
    if (parseFloat(Bigha_value) > (Bigha1_value)) {
        alert("Area is not greater than actual area");
        $(this).val('');
        return false;
    }

});
$("#Biswa").keyup(function () {
    var Biswa_value = $(this).val();
    var Biswa1_value = $("#Biswa1").val();
    if (parseFloat(Biswa_value > Biswa1_value)) {
        alert("Area is not greater than actual area");
        $(this).val('');
        return false;
    }

});

$("#Biswanshi").keyup(function () {
    var Biswanshi_value = $(this).val();
    var Biswanshi1_value = $("#Biswanshi1").val();
    if (parseFloat(Biswanshi_value) > (Biswanshi1_value)) {
        alert("Area is not greater than actual area");
        $(this).val('');
        return false;
    }

});



function onPaging(pageNo) {
    GetUS17(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetUS17(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}
