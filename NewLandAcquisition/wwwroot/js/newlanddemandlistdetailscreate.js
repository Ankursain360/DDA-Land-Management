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

    HttpGet(`/NewLandDemandListDetails/GetKhasraList/?Id=${id}`, 'json', function (response) {
        $("#KhasraNoId").val('').trigger('change');
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#KhasraNoId").html(html);
    });
};


$('#ENMDocumentIFormFile').change(function () {
    var fileInput = document.getElementById('ENMDocumentIFormFile');
    var filePath = fileInput.value;
    const size = (ENMDocumentIFormFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Invalid file type');
        fileInput.value = '';
        return false;
    }
    if (size > 10535049) {
        alert("File must be of 10 MB or Lesser Than 10 MB");
        fileInput.value = '';
        return false;
    }

}