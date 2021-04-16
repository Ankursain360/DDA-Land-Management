//notification dta
//debugger;
//$(document).ready(function () {
//    GetNotificationDetails();

//});
//debugger;
//function GetNotificationDetails() {
//    HttpGet(`/Newlandus4plot/NotificationView`, 'html', function (response) {
//        $('#divnotificationTable').html("");
//        $('#divnotificationTable').html(response);
//    });
//};
$("#NotificationId").change(function () {
    var id = $(this).val();
    debugger;
    if (id) {

        HttpGet(`/Newlandus4plot/NotificationView/?NotificationId=${id}`, 'html', function (response) {


            $('#divnotificationTable').html("");
            $('#divnotificationTable').html(response);

        });

    }
});

function onChange(id) {

    HttpGet(`/Newlandus4plot/GetKhasraList/?villageId=${id}`, 'json', function (response) {
        var html = '<option value=""> --Select--</option>';
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
        HttpGet(`/Newlandus4plot/GetKhasraAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#ABigha").val(response.bigha);
            $("#ABiswa").val(response.biswa);
            $("#ABiswanshi").val(response.biswanshi);
            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);
           
        });
        //HttpGet(`/Newlandus4plot/GetKhasra1AreaList/?khasraid=${kid}`, 'json', function (response) {
        //    debugger;
           

        //});

    }
});

$("#Bigha").keyup(function () {
    var Bigha_value = $(this).val();
    var Bigha1_value = $("#ABigha").val();
    if (Bigha_value > Bigha1_value) {
        alert("Enter the correct value");
        $(this).val('');
        return false;
    }

});
$("#Biswa").keyup(function () {
    var Biswa_value = $(this).val();
    var Biswa1_value = $("#ABiswa").val();
    if (Biswa_value > Biswa1_value) {
        alert("Enter the correct value");
        $(this).val('');
        return false;
    }

});

$("#Biswanshi").keyup(function () {
    var Biswanshi_value = $(this).val();
    var Biswanshi1_value = $("#ABiswanshi").val();
    if (Biswanshi_value > Biswanshi1_value) {
        alert("Area is should not be greater than actual area");
        $(this).val('');
        return false;
    }

});


