
    $(function () {
        var value = $('#ddproperty option:selected').val();
        if (value == "Lease") {
        $('#divLeaseproperty').show();
            $('#divLicenseproperty').hide();
        }
        else {
        $('#divLeaseproperty').hide();
            $('#divLicenseproperty').show();
        }

        var value2 = $('#ddleasetype option:selected').val();
        if (value2 == "License") {
            $('#divTenure').show();
           
        }
        else {
            $('#divTenure').hide();
           
        }
        
    });
    $('#ddproperty').change(function () {
        var value = $('#ddproperty option:selected').val();
        if (value == "Lease") {
        $('#divLeaseproperty').show();
            $('#divLicenseproperty').hide();
        }
        else {
        $('#divLeaseproperty').hide();
            $('#divLicenseproperty').show();
        }
    });


$('#ddleasetype').change(function () {
    var value2 = $('#ddleasetype option:selected').val();
    if (value2 == "License") {
        $('#divTenure').show();

    }
    else {
        $('#divTenure').hide();

    }
});