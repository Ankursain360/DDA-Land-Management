$(document).ready(function () {

       
});
$("input[name='grpCaseType']").click(function () {
    var selected = $("input[type='radio'][name='grpCaseType']:checked");
    $("#CaseType").val(selected.val());

});
$("input[name='grpInfavour']").click(function () {
    var selected = $("input[type='radio'][name='grpInfavour']:checked");
    $("#InFavour").val(selected.val());

});