

$("input[name='IsVacant']").click(function () {/* -----------Added by Renu  --------------- */
    if ($("#IsVacant").is(":checked"))
        $("#SitePosition").val("Vacant");
    else
        $("#SitePosition").val("");

    $('.chkAction:checkbox:checked').each(function () {
        var arrIds = $(this).attr("id");
        $("#SitePosition").val(arrIds);
    });
});

$("input[name='IsBuiltup']").click(function () {/* -----------Added by Renu  --------------- */
    if ($("#IsBuiltup").is(":checked"))
        $("#SitePosition").val("Built Up");
    else
        $("#SitePosition").val("");

    $('.chkAction:checkbox:checked').each(function () {
        var arrIds = $(this).attr("id");
        $("#SitePosition").val(arrIds);
    });
});


$("input[name='IsPartialBuiltup']").click(function () {/* -----------Added by Renu  --------------- */
    if ($("#IsVacant").is(":checked"))
        $("#SitePosition").val("Partial Builtup");
    else
        $("#SitePosition").val("");

    $('.chkAction:checkbox:checked').each(function () {
        var arrIds = $(this).attr("id");
        $("#SitePosition").val(arrIds);
    });
});

$("input[name='IsBoundary']").click(function () {/* -----------Added by Renu  --------------- */
    if ($("#IsBuiltup").is(":checked"))
        $("#SitePosition").val("Boundary");
    else
        $("#SitePosition").val("");

    $('.chkAction:checkbox:checked').each(function () {
        var arrIds = $(this).attr("id");
        $("#SitePosition").val(arrIds);
    });
});

$("#VillageId").change(function () {
    // alert(ZoneId);
    var ids = $(this).val();
    if (ids) {
        HttpGet(`/JointSurvey/AllKhasraList/?villageid=${ids}`, 'json', function (response) {
            var html = '<option value="">Select</option>';
            for (var i = 0; i < response.length; i++) {
                html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
            }
            $("#KhasraId").select2('val', '')
            $("#KhasraId").html(html);

            //  alert(JSON.stringify(response));
        });

    }
});