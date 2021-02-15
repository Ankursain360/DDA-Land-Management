$("input[name='jarairadio']").click(function () {
    var selected = $("input[type='radio'][name='jarairadio']:checked");
    $("#JaraiSakani").val(selected.val());

});
$("input[name='villageradio']").click(function () {
    var selected = $("input[type='radio'][name='villageradio']:checked");
    $("#Language").val(selected.val());

});