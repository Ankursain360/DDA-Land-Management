

$(document).ready(function () {
if ($("#AppealByDept").val() == "Department") {
    $("#Departmentdiv").show();
} else {
    $("#Departmentdiv").hide();
    }
});


$("input[name='villageradio']").click(function () {
    var selected = $("input[type='radio'][name='villageradio']:checked");
    $("#AppealByDept").val(selected.val());

});


$(document).ready(function () {

    $("input[name='villageradio']").click(function () {
  
        debugger;
        if ($(this).val() == "Department") {
            if ($('#OtherDDA').is(':checked')) {
                $('#Departmentdiv').css('display', 'block');
            } else {
                $('#Departmentdiv').css('display', 'none');
            }
        } else {
            $('#Departmentdiv').css('display', 'none');
        }
       // if ($('#radio_button').is(':checked')) { alert("it's checked"); }
       
    });

});