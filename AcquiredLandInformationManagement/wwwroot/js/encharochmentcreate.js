$(function () {
    $("input[name='grpDamageArea']").click(function () {
        if ($("#Dyes").is(":checked")) {
            $('#DamageArea').val('yes');
        } else {
            $('#DamageArea').val('No');
        }
    });
});
//$(function () {
//    $("input[name='grpNature']").click(function () {
//        if ($("#N2").is(":checked")) {
//            $('#Nature').val('0');
//        } else {
//            $('#Nature').val('1');
//        }
//    });
//});
$("input[name='grpDamageArea']").click(function () {
    var selected = $("input[type='radio'][name='grpDamageArea']:checked");
    $("#DamageArea").val(selected.val());

});
//$("input[name='grpNature']").click(function () {
//    var selected = $("input[type='radio'][name='grpNature']:checked");
//    $("#Nature").val(selected.val());

//});