﻿function onChange(id) {

    HttpGet(`/NewLandProposalPlotDetails/GetKhasraList/?villageId=${id}`, 'json', function (response) {
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
        HttpGet(`/NewLandProposalPlotDetails/GetKhasraAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);

        });

    }
});