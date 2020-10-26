function onChangeDepartment(id) {
    HttpGet(`/LandTransfer/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").html('<option value="">Select</option>');
        $("#LocalityId").html('<option value="">Select</option>');
        $("#ZoneId").select2('val','');
        $("#DivisionId").select2('val','');
        $("#LocalityId").select2('val','');
    });
};
function onChangeZone(id) {
    HttpGet(`/LandTransfer/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#LocalityId").html('<option value="">Select</option>');
        $("#DivisionId").select2('val', '');
        $("#LocalityId").select2('val', '');

    });
};
function onChangeDivision(id) {
    HttpGet(`/LandTransfer/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
        $("#LocalityId").select2('val', '');
    });
};
function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif|\.pdf|\.xls|\.xlsx|\.docx|\.doc)$/i;
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
$("#KhasraNo").change(function () {
    var khasraNo = $("#KhasraNo").val();
    $.get(`/LandTransfer/GetHistoryDetails/?KhasraNo=${khasraNo}`, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
});
$(document).ready(function () {
    $("input").each(function () {
        if ($(this).val() == '0') {
            $(this).val('');
        }
    });
    $(".numbers").keyup(function () {
        var buildupArea = $("input[name='BuildupArea']").val();
        var VacantArea = $("input[name='VacantArea']").val();
        $("input[name='TotalArea']").val(parseFloat(buildupArea == '' ? '0' : buildupArea) + parseFloat(VacantArea == '' ? '0' : VacantArea));
    });
    $('#CopyofOrder').change(function () {
        var fileInput = document.getElementById('CopyofOrder');
        var filePath = fileInput.value;
        const size = (CopyofOrder.files[0].size);
        fileValidation(filePath, fileInput, size);
    });
    var khasraNo = $("#KhasraNo").val();
    $.get(`/LandTransfer/GetHistoryDetails/?KhasraNo=${khasraNo}`, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
});