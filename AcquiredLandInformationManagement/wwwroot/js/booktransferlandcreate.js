$(function () {
    $("input[name='grpLand']").click(function () {
        debugger
        if ($("#A").is(":checked")) {
            $('#StatusOfLand').val('Alloted');
        }
       else if ($("#V").is(":checked")) {
            $('#StatusOfLand').val('Vacant');

        } else if ($("#E").is(":checked")) {
            $('#StatusOfLand').val('Enchroched');
        } 
        else if ($("#G").is(":checked")) {
            $('#StatusOfLand').val('Green');
        } 
            
        else {
            $('#StatusOfLand').val('Green');
        }
    });
});

$("input[name='grpLand']").click(function () {
    var selected = $("input[type='radio'][name='grpLand']:checked");
    $("#StatusOfLand").val(selected.val());

});
