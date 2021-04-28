

var currentPageNumber = 1;
var currentPageSize = 5;


$(document).ready(function () {
    var id = $("#NotificationTypeId").val();
    
    var param = {
        TypeId: id,
        pageSize: parseInt(currentPageSize),
        pageNumber: parseInt(currentPageNumber)
    }

    if (id) {

        HttpPost(`/Newlandnotificationdetails/NotificationsView/`, 'html', param, function (response) {

            $('#divNotificationsTable').html("");
            $('#divNotificationsTable').html(response);

        });

    }
    var kid = $("#KhasraId").val();

    if (kid) {
        HttpGet(`/Newlandnotificationdetails/GetKhasraAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha1").val(response.bigha);
            $("#Biswa1").val(response.biswa);
            $("#Biswanshi1").val(response.biswanshi);



        });

    }
});

function GetNotifications(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);

    HttpPost(`/Newlandnotificationdetails/NotificationsView/`, 'html', param, function (response) {
        $('#divNotificationsTable').html("");
        $('#divNotificationsTable').html(response);

    });
}

function GetSearchParam(pageNumber, pageSize) {

    var model = {
        TypeId: $('#NotificationTypeId').val(),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }

    return model;
}



function onPaging(pageNo) {
    GetNotifications(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetNotifications(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}


$("#NotificationTypeId").change(function () {

    var id = $(this).val();

    var model = {
        TypeId: id,
        pageSize: parseInt(currentPageSize),
        pageNumber: parseInt(currentPageNumber)
    }
    
    if (id) {

        HttpPost(`/Newlandnotificationdetails/NotificationsView/`, 'html', model, function (response) {
            $('#divNotificationsTable').html("");
            $('#divNotificationsTable').html(response);

        });

    }
});







function onChange(id) {

    HttpGet(`/Newlandnotificationdetails/GetKhasraList/?villageId=${id}`, 'json', function (response) {
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
        HttpGet(`/Newlandnotificationdetails/GetKhasraAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha1").val(response.bigha);
            $("#Biswa1").val(response.biswa);
            $("#Biswanshi1").val(response.biswanshi);

            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);
           
        });

    }
});

$("#Bigha").keyup(function () {

    var Bigha_value = $(this).val();
    var Bigha1_value = $("#Bigha1").val();
    if (parseFloat(Bigha_value) > parseFloat(Bigha1_value)) {
        alert("Used Area should not be greater than Actual Area");
        $(this).val('');
        return false;
    }

});


$("#Biswa").keyup(function () {
    var Biswa_value = $(this).val();
    var Biswa1_value = $("#Biswa1").val();
    if (parseFloat(Biswa_value) > parseFloat(Biswa1_value)) {
        alert("Used Area should not be greater than Actual Area");
        $(this).val('');
        return false;
    }

});


$("#Biswanshi").keyup(function () {

    var Biswanshi_value = $(this).val();
    var Biswanshi1_value = $("#Biswanshi1").val();
    if (parseFloat(Biswanshi_value) > (Biswanshi1_value)) {
        alert("Used Area should not be greater than Actual Area");
        $(this).val('');
        return false;
    }

});


//function onPaging(pageNo) {
//    GetDivision(parseInt(pageNo), parseInt(currentPageSize));
//    currentPageNumber = pageNo;
//}

//function onChangePageSize(pageSize) {
//    GetDivision(parseInt(currentPageNumber), parseInt(pageSize));
//    currentPageSize = pageSize;
//}


