$(document).ready(function () {
    debugger;
    var value = "Personal"
    var param = GetSearchParam(value);
    HttpPost(`/UserManagement/GetDetails`, 'html', param, function (response) {
        $('#divLoadData').html("");
        $('#divLoadData').html(response);
    });



    HttpGet(`/UserManagement/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        debugger;
        $("#ZoneId").val('').trigger('change');
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
    });

});

function GetSearchParam(value) {
    var id = $('#Id').val();
    var model = {
        id: parseInt(id),
        value: value
    }
    return model;
}

$("#ProfileBtn").click(function () {
    debugger;
    var value = "Profile"
    var param = GetSearchParam(value);
    HttpPost(`/UserManagement/GetDetails`, 'html', param, function (response) {
        $('#divLoadData').html("");
        $('#divLoadData').html(response);
    });

});

$("#PersonalBtn").click(function () {
    debugger;
    var value = "Personal"
    var param = GetSearchParam(value);
    HttpPost(`/UserManagement/GetDetails`, 'html', param, function (response) {
        $('#divLoadData').html("");
        $('#divLoadData').html(response);
    });

});

$("#updatePersonalBtn").click(function () {
        var param = GetListData();
    HttpPost(`/UserManagement/UpdatePersonalDetails`, 'json', param, function (response) {
        debugger;
        });

});

function GetListData() {

}
