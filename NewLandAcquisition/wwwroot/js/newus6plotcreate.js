var currentPageNumber = 1;
var currentPageSize = 5;


$(document).ready(function () {
    var id = $("#NotificationId").val();
    $("#VillageId").val('');
    $("#KhasraId").val('');
    var param = {
        NotificationId: id,
        pageSize: parseInt(currentPageSize),
        pageNumber: parseInt(currentPageNumber)
    }

    if (id) {
        debugger;
        HttpPost(`/Newlandus6plot/NotificationView/`, 'html', param, function (response) {


            $('#divnotificationTable').html("");
            $('#divnotificationTable').html(response);

        });

    }
});
function GetDivision(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Newlandus6plot/NotificationView/`, 'html', param, function (response) {
        $('#divnotificationTable').html("");
        $('#divnotificationTable').html(response);

    });
}

function GetSearchParam(pageNumber, pageSize) {

    var model = {
        NotificationId: $('#NotificationId').val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }

    return model;
}






$("#NotificationId").change(function () {

    var id = $(this).val();

    var model = {
        NotificationId: id,
        pageSize: parseInt(currentPageSize),
        pageNumber: parseInt(currentPageNumber)
    }
    //alert(JSON.stringify(model));
    if (id) {
        HttpPost(`/Newlandus6plot/NotificationView/`, 'html', model, function (response) {
            $('#divnotificationTable').html("");
            $('#divnotificationTable').html(response);

        });

    }
});








function onChange(id) {

    HttpGet(`/Newlandus6plot/GetKhasraList/?villageId=${id}`, 'json', function (response) {
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
        HttpGet(`/Newlandus6plot/GetKhasraAreaList/?khasraid=${kid}`, 'json', function (response) {

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
    if (parseFloat(Bigha_value) > (Bigha1_value)) {
        alert("Used Area is not greater than Actual Area");
        $(this).val('');
        return false;
    }

});
$("#Biswa").keyup(function () {
    var Biswa_value = $(this).val();
    var Biswa1_value = $("#ABiswa").val();
    if (parseFloat(Biswa_value) > (Biswa1_value)) {
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


