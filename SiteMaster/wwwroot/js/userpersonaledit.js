$("#updatePersonalBtn").click(function () {
    var param = GetListData();
    HttpPost(`/UserManagement/UpdatePersonalDetails`, 'json', param, function (response) {
        debugger;
    });

});

function GetListData() {
    var id = $('#Id').val();
    var name = $('#Name').val();
    var username = $('#UserName').val();
    var email = $('#Email').val();
    var phonenumber = $('#PhoneNumber').val();
    var model = {
        Id: parseInt(id),
        Name: name,
        UserName: username,
        Email: email,
        PhoneNumber: phonenumber
    }
    return model;
}
