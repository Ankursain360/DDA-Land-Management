function onChange(id) {
    debugger
    HttpGet(`/AwardPlotDetails/GetKhasraList/?VillageId=${id}`, 'json', function (response) {
        var html = '<option value=""></option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

        $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};