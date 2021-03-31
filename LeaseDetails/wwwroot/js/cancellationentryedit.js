$(document).ready(function () {

    var sname = $("#AllotmentId").find(':selected').attr('data-id');

    var res = sname.split(",");
    // alert(res[0]);
    $("#Society_name").val(res[0]);
    $("#Purpose_name").val(res[1]);

});

function get_socity() {
    var sname = $("#AllotmentId").find(':selected').attr('data-id');
    var res = sname.split(",");
    // alert(res[0]);
    $("#Society_name").val(res[0]);
    $("#Purpose_name").val(res[1]);
}