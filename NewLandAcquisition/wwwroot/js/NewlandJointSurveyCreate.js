function onChange(id) {
   // alert(id);

    HttpGet(`/NewLandJointSurvey/GetVillageList/?ZoneId=${id}`, 'json', function (response) {
      
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
       
      
        $("#vvv").html(html); 
    });
};

function onChange_village(id) {
     alert(id);
    HttpGet(`/NewLandJointSurvey/GetKhasraList/?VillageId=${id}`, 'json', function (response) {
        var html = '<option value="0"></option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        alert(html);
      //  $("#KhasraId").select2('val', '')
        $("#kkk").html(html);
    });
    
};
/*
$("#VillageId").change(function ()  {

    HttpGet(`/NewLandJointSurvey/GetKhasraList/?VillageId=${id}`, 'json', function (response) {
        var html = '<option value="0"></option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

        $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
});*/
$("#KhasraId").change(function () {
    var kid = $(this).val();
    if (kid) {
        HttpGet(`/NewLandJointSurvey/GetAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);
            // alert(JSON.stringify(response));
        });

    }
});