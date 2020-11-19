

//For dropdown change event
$(document).ready(function () {

    var id = parseInt($('#PrimaryListNo option:selected').val());
    GetOtherDetails(id);
});


function GetOtherDetails(id) {
    HttpGet(`/WatchWard/InventoryView/?Id=${id}`, 'html', function (response) {
        $('#InventoryOtherDiv').html("");
        $('#InventoryOtherDiv').html(response);
    });
};
$(function () {
    var dtToday = new Date();

    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();

    var maxDate = year + '-' + month + '-' + day;
    //alert(maxDate);
    $('#txtDate').attr('max', maxDate);
    $('#txtDateRep').attr('max', maxDate);
});



$('#Photo').change(function () {
    var fileInput = document.getElementById('Photo');
    var filePath = fileInput.value;
    const size = (Photo.files[0].size);
    fileValidation(filePath, fileInput, size);
});

function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Invalid file type');
        fileInput.value = '';
        return false;
    }
    else if (size > 10535049) {
        alert("File must be of 10 MB or Lesser Than 10 MB");
        fileInput.value = '';
        return false;
    }
}

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$("#collapse").click(function () {
    debugger;
    $('#collapseExample').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    })
});