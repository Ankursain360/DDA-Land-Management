$(document).ready(function () {
    debugger
    var param = GetSearchParam();
    HttpPost(`/UserManagement/LoadPersonalDetails`, 'html', param, function (response) {
        $('#divLoadData').html("");
        $('#divLoadData').html(response);
    });

   
});

$("#btnPersonalInfo").click(function (event) {
    event.preventDefault();
    var param = GetSearchParam();
    HttpPost(`/UserManagement/LoadPersonalDetails`, 'html', param, function (response) {
        $('#divLoadData').html("");
        $('#divLoadData').html(response);
    });
});

$("#btnProfileInfo").click(function (event) {
    event.preventDefault();
    var param = GetSearchParam();
    HttpPost(`/UserManagement/LoadProfileDetails`, 'html', param, function (response) {
        $('#divLoadData').html("");
        $('#divLoadData').html(response);
    });
});

function GetSearchParam() {
    var id = $('#Id').val();
    var model = {
        id: parseInt(id)
    }
    return model;
}

function onChange(id) {

    HttpGet(`/UserManagement/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        $("#ZoneId").val('').trigger('change');
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
    });
};