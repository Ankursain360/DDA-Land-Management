function onChange(id) {
   
    HttpGet(`/Village/GetZoneList/?DepartmentId=${id}`, 'json',  function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").select2('val', '')
        $("#ZoneId").html(html);
    });
};
function onChangeZone(id) {
   
    HttpGet(`/Village/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").select2('val', '')
        $("#DivisionId").html(html);
    });
};