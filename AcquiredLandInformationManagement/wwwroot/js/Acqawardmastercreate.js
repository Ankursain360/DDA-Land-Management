$(function () {
    $("input[name='grpType']").click(function () {
        if ($("#1").is(":checked")) {
            $('#Type').val('1');
        } else {
            $('#Type').val('0');
        }
    });
});
$(function () {
    $("input[name='grpNature']").click(function () {
        if ($("#N2").is(":checked")) {
            $('#Nature').val('0');
        } else {
            $('#Nature').val('1');
        }
    });
});
$("input[name='grpType']").click(function () {
    var selected = $("input[type='radio'][name='grpType']:checked");
    $("#Type").val(selected.val());

});
$("input[name='grpNature']").click(function () {
    var selected = $("input[type='radio'][name='grpNature']:checked");
    $("#Nature").val(selected.val());

});