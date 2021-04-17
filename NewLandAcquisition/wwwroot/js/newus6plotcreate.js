$(document).ready(function () {
    var id = $("#NotificationId").val();
    $("#VillageId").val('');
    $("#KhasraId").val('');
    if (id) {

        HttpGet(`/Newlandus6plot/NotificationView/?NotificationId=${id}`, 'html', function (response) {


            $('#divnotification6Table').html("");
            $('#divnotification6Table').html(response);

        });

    }
});
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

    HttpGet(`/Newlandus6plot/GetKhasraList/?villageId=${id}`, 'json', function (response) {
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
        HttpGet(`/Newlandus6plot/GetKhasraAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#ABigha").val(response.bigha);
            $("#ABiswa").val(response.biswa);
            $("#ABiswanshi").val(response.biswanshi);
            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);
           
        });

    }
});