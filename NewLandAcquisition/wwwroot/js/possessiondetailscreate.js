    function onChange(id) {
 
    HttpGet(`/NewlandPossesionDetails/GetKhasraList/?villageId=${id}`, 'json', function (response) {
      
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
        HttpGet(`/NewlandPossesionDetails/GetAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);
            // alert(JSON.stringify(response));
        });

    }
});


$("input[name='IsVacant']").click(function () {/* -----------Added by Renu  --------------- */
    if ($("#IsVacant").is(":checked"))
        $("#PossType").val("Vacant");
    else
        $("#PossType").val("");

    $('.chkAction:checkbox:checked').each(function () {
        var arrIds = $(this).attr("id");
        $("#PossType").val(arrIds);
    });
});

$("input[name='IsBuiltup']").click(function () {/* -----------Added by Renu  --------------- */
    if ($("#IsBuiltup").is(":checked"))
        $("#PossType").val("Built Up");
    else
        $("#PossType").val("");

    $('.chkAction:checkbox:checked').each(function () {
        var arrIds = $(this).attr("id");
        $("#PossType").val(arrIds);
    });
});

