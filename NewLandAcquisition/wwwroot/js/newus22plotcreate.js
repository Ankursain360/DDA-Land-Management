function onChange(id) {

    HttpGet(`/Newlandus22plot/GetKhasraList/?villageId=${id}`, 'json', function (response) {
        var html = '<option></option>';
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


$("#NotificationId").change(function () {
    var id = $(this).val();
    if (id) {
        HttpGet(`/Newlandus22plot/FetchUS6Plot/?notificationId=${id}`, 'json', function (response) {

           // $("#Us6Id").val(response.bigha);
            //$("#Biswa").val(response.biswa);
            //$("#Biswanshi").val(response.biswanshi);
            $('#Us6Id').val(response.us6Id).trigger('change');
        });

    }
});