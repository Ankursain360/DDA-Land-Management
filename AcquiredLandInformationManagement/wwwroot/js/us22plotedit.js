var currentPageNumber = 1;
var currentPageSize = 5;


$(document).ready(function () {
    //
    var id = $("#UnderSection22Id").val();

    //$("#VillageId").val('');
    //$("#KhasraId").val('');
    var param = {
        NotificationId: id,
        pageSize: parseInt(currentPageSize),
        pageNumber: parseInt(currentPageNumber)
    }

    if (id) {

        HttpPost(`/UnderSection22PlotDetails/Notification4View/`, 'html', param, function (response) {


            $('#divnotification22Table').html("");
            $('#divnotification22Table').html(response);

        });

    }

    //
    var kid = $("#KhasraId").val();

    if (kid) {
        HttpGet(`/UnderSection22PlotDetails/GetAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha1").val(response.bigha);
            $("#Biswa1").val(response.biswa);
            $("#Biswanshi1").val(response.biswanshi);


            // alert(JSON.stringify(response));
        });

    }

});
function GetDivision(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/UnderSection22PlotDetails/Notification4View/`, 'html', param, function (response) {
        $('#divnotification22Table').html("");
        $('#divnotification22Table').html(response);

    });
}

function GetSearchParam(pageNumber, pageSize) {

    var model = {
        NotificationId: $('#UnderSection22Id').val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }

    return model;
}






$("#UnderSection22Id").change(function () {

    var id = $(this).val();

    var model = {
        NotificationId: id,
        pageSize: parseInt(currentPageSize),
        pageNumber: parseInt(currentPageNumber)
    }
    //alert(JSON.stringify(model));
    if (id) {
        HttpPost(`/UnderSection22PlotDetails/Notification4View/`, 'html', model, function (response) {
            $('#divnotification22Table').html("");
            $('#divnotification22Table').html(response);

        });

    }
});








function onChange(id) {

    HttpGet(`/UnderSection22PlotDetails/GetKhasraList/?villageId=${id}`, 'json', function (response) {
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
        HttpGet(`/UnderSection22PlotDetails/GetAreaList/?khasraid=${kid}`, 'json', function (response) {

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
    if (Bigha_value > Bigha1_value) {
        alert("Enter the correct value");
        $(this).val('');
        return false;
    }

});
$("#Biswa").keyup(function () {
    var Biswa_value = $(this).val();
    var Biswa1_value = $("#Biswa1").val();
    if (Biswa_value > Biswa1_value) {
        alert("Enter the correct value");
        $(this).val('');
        return false;
    }

});

$("#Biswanshi").keyup(function () {
    var Biswanshi_value = $(this).val();
    var Biswanshi1_value = $("#Biswanshi1").val();
    if (Biswanshi_value > Biswanshi1_value) {
        alert("Area is not greater than actual area");
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
