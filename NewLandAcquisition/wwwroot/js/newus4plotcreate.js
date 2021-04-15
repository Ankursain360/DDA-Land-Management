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