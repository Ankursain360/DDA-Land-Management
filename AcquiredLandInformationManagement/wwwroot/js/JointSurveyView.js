
//$("input[name='grpDamageAssesseeType']").click(function () {
//    if ($("#builtup").is(":checked")) {
//        $("#Type").val('B');    
//    }
//    else if ($("#Partialbuiltup").is(":checked")) {
//        $("#Type").val('P');
//    }
//    else if ($("#vacant").is(":checked")) {
//        $("#Type").val('V');
//    }
//    else {
//        $("#Type").val('W');
//    }
//});


$('#CheckBoxList').on('change', 'input[type=checkbox]', function () {

    var id = $(this).val(); // this gives me null
    if (id != null) {
        alert(id)
    }

});