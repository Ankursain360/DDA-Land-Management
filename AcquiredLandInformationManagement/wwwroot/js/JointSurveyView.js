

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

