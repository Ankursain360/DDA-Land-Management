function onChange(id) {

    HttpGet(`/Newlandus4plot/GetKhasraList/?villageId=${id}`, 'json', function (response) {
        var html = '<option></option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

        $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};