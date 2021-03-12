function onChange(id) {

    HttpGet(`/Newlandus22plot/GetKhasraList/?villageId=${id}`, 'json', function (response) {
        var html = '<option value="">--Select--</option>';
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
        HttpGet(`/Newlandus22plot/GetKhasraAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);

        });

    }
});




function onNotificationChange(id) {

    HttpGet(`/Newlandus22plot/GetAllUS4Plot/?notificationId=${id}`, 'json', function (response) {
        var html = '<option></option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option  value=' + response[i].id + '>' + response[i].notification.name + '</option>';
        }

        $("#Us4Id").select2('val', '')
        $("#Us4Id").html(html);
    });
    HttpGet(`/Newlandus22plot/GetAllUS6Plot/?notificationId=${id}`, 'json', function (response) {
        var html = '<option></option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].notification.name + '</option>';
        }

        $("#Us6Id").select2('val', '')
        $("#Us6Id").html(html);
    });
    HttpGet(`/Newlandus22plot/GetAllUS17Plot/?notificationId=${id}`, 'json', function (response) {
        var html = '<option></option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].notification.name + '</option>';
        }

        $("#Us17Id").select2('val', '')
        $("#Us17Id").html(html);
    });
};