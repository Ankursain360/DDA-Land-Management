
$("#survey_done").change(function () {

    var id = $(this).val();
    if (id == 1) {
        $("#tss_div").show();
    } else {
        $("#tss_div").hide();
    }
});



$("#ZoneId").change(function () {
    // alert(ZoneId);
    var ids = $(this).val();
    if (ids) {
        HttpGet(`/GramSabhaLand/AllVillagedataList/?zoneid=${ids}`, 'json', function (response) {
            var html = '<option value="">Select</option>';
            for (var i = 0; i < response.length; i++) {
                html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
            }
            $("#VillageId").select2('val', '')
            $("#VillageId").html(html);

            //  alert(JSON.stringify(response));
        });

    }
});
