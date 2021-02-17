$(function () {
    $("input[name='grpLand']").click(function () {
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
//$(function () {
//    $("input[name='grpNature']").click(function () {
//        if ($("#N2").is(":checked")) {
//            $('#Nature').val('0');
//        } else {
//            $('#Nature').val('1');
//        }
//    });
//});
$("input[name='grpLand']").click(function () {
    var selected = $("input[type='radio'][name='grpLand']:checked");
    $("#StatusOfLand").val(selected.val());

});
//$("input[name='grpNature']").click(function () {
//    var selected = $("input[type='radio'][name='grpNature']:checked");
//    $("#Nature").val(selected.val());

//});