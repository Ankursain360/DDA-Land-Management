$(function () {
    $("input[name='grpType']").click(function () {
        if ($("#1").is(":checked")) {
            $('#Type').val('1');
        } else {
            $('#Type').val('0');
        }
    });
});
(function () {
    $("input[name='grpN']").click(function () {
        if ($("#N1").is(":checked")) {
            $('#Nature').val('1');
        } else {
            $('#Nature').val('0');
        }
    });
});
$(function () {
    $("input[name='grpNature']").click(function () {
        if ($("#N2").is(":checked")) {
            $('#Nature').val('0');
        }
        else {
            $('#Nature').val('1');
        }
    });
});
$("input[name='grpType']").click(function () {
    var selected = $("input[type='radio'][name='grpType']:checked");
    $("#Type").val(selected.val());

});
$("input[name='grpN']").click(function () {
    var selected = $("input[type='radio'][name='grpN']:checked");
    $("#Nature").val(selected.val());

});
$("input[name='grpNature']").click(function () {
    var selected = $("input[type='radio'][name='grpNature']:checked");
    $("#Nature").val(selected.val());

});


$('#DocumentIFormFile').change(function () {
    var fileInput = document.getElementById('DocumentIFormFile');
    var filePath = fileInput.value;
    const size = (DocumentIFormFile.files[0].size);
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