

function onChange(id) {
   /* debugger*/
    HttpGet(`/NewSelfAssessmentForm/GetNewVillageList/?Districtid=${id}`, 'json', function (response) {
        var html = '<option value="">--Select--</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
      /*  $("#ColonyId").select2('val', '')*/
        $("#VillageId").select2('val', '')
        $("#VillageId").html(html);
    });
};
function onChangeVillage(id) {

    HttpGet(`/NewSelfAssessmentForm/GetColonyList/?VillageId=${id}`, 'json', function (response) {
        var html = '<option value="">--Select--</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ColonyId").select2('val', '')
        $("#ColonyId").html(html);
    });
};
