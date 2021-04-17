$(document).ready(function () {
   

});

$("input[name='grpCourtInvolves']").click(function () {
    var selected = $("input[type='radio'][name='grpCourtInvolves']:checked");
    $("#CourtInvolves").val(selected.val());
});
$("input[name='grpBalanceInterestCase']").click(function () {
    var selected = $("input[type='radio'][name='grpBalanceInterestCase']:checked");
    $("#BalanceInterestCase").val(selected.val());
});
$("input[name='grpPayableApealable']").click(function () {
    var selected = $("input[type='radio'][name='grpPayableApealable']:checked");
    $("#PayableAppealable").val(selected.val());
});
function GetKhasraList(id) {
  
    HttpGet(`/DemandListDetails/GetKhasraList/?Id=${id}`, 'json', function (response) {
        $("#KhasraNoId").val('').trigger('change');
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#KhasraNoId").html(html);
    });
};