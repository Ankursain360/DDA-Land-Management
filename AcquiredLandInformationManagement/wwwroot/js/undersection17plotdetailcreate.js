function onChange(id) {

    HttpGet(`/ProposalPlotDetails/GetKhasraList/?villageId=${id}`, 'json', function (response) {
        var html = '<option value="0">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

        $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};