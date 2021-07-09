////$(function () {
////    //$("input[name='grpLand']").click(function () {
       
////        if ($("#A").is(":checked")) {
////            $('#StatusOfLand').val('Alloted');
////        }
////       else if ($("#V").is(":checked")) {
////            $('#StatusOfLand').val('Vacant');

////        } else if ($("#E").is(":checked")) {
////            $('#StatusOfLand').val('Buildup/Encroached');
////        } 
////        else if ($("#G").is(":checked")) {
////            $('#StatusOfLand').val('Green');
////        } 
            
////        else {
////            $('#StatusOfLand').val('Green');
////        }
////    //});
////});
function onChange(id) {
 
    HttpGet(`/bookTransferLand/GetKhasraList/?LocalityId=${id}`, 'json', function (response) {
       
        var html = '<option val="">-- select-- </option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

        // $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};

$("input[name='grpLand']").click(function () {
    var selected = $("input[type='radio'][name='grpLand']:checked");
    $("#StatusOfLand").val(selected.val());

});


$('#GOINotificationDocumentIFormFile').change(function () {
    var fileInput = document.getElementById('GOINotificationDocumentIFormFile');
    var filePath = fileInput.value;
    const size = (GOINotificationDocumentIFormFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});
$('#OrderDocumentIFormFile').change(function () {
    var fileInput = document.getElementById('OrderDocumentIFormFile');
    var filePath = fileInput.value;
    const size = (OrderDocumentIFormFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});
$('#PossessionDocumentIFormFile').change(function () {
    var fileInput = document.getElementById('PossessionDocumentIFormFile');
    var filePath = fileInput.value;
    const size = (PossessionDocumentIFormFile.files[0].size);
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