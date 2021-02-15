
$("input[name='grpDamageAssesseeType']").click(function () {
    if ($("#builtup").is(":checked")) {
        $("#Type").val('B');
       
     
    }
    else if ($("#Partialbuiltup").is(":checked")) {
        $("#Type").val('P');

       
    }

    else if ($("#vacant").is(":checked")) {
        $("#Type").val('V');

        
    }

    
    else {
        $("#Type").val('W');
       
        
    }
});


