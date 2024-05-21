function onChange(id) {

    HttpGet(`/UserManagement/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        $("#ZoneId").val('').trigger('change');
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
    });

    HttpGet(`/UserManagement/GetBranchList/?DepartmentId=${id}`, 'json', function (response) {
        $("#BranchId").val('').trigger('change');
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#BranchId").html(html);
    });
};

$("#defaultpass").click(function () {
    var val = $("#defaultpassword_text").val();
   // alert(vall);
    //val 1=off,2=on
    if ($("#defaultpass").is(':checked')) {
        $("#defaultpassword_text").val(2);
        $("#Password").attr("disabled", "disabled");
        $("#ConfirmPassword").attr("disabled", "disabled");
    } else {
        $("#defaultpassword_text").val(1);
        $("#Password").removeAttr("disabled");
        $("#ConfirmPassword").removeAttr("disabled");
        $("#Password").focus();
        $("#ConfirmPassword").focus();

    }

});

function CheckPasswordValidation() {
    debugger;
    
    let regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@.#$!%*?&])[A-Za-z\d@.#$!%*?&]{8,15}$/;
    var password = document.getElementById("Password").value
    if (regex.test(password))
    {
        return true; 
    }
    else
    {
        alert('Passwords must contain at least eight characters, including uppercase, lowercase letters,numbers and special character (@#$!%*?&)');
        return false;
    }
}