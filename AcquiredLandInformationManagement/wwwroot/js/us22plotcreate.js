function onChange(id) {

    HttpGet(`/UnderSection22PlotDetails/GetKhasraList/?villageId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};