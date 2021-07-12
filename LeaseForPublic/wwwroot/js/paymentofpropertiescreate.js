$("#payble1").change(function () {
    var id = $(this).val();

    if (id == 1) {
        $("#showyes").show();
        $("#shoshow").show();
        $("#payble2").select2();
        $("#tbl_posts").hide();
    } else {
        $("#tbl_posts").hide();
        $("#shoshow").hide();
        $("#showyes").hide();
    }
});

$("#payble2").change(function () {
    var id = $(this).val();

    if (id == 1) {

        $("#tbl_posts").hide();
    } else {
        $("#tbl_posts").show();

    }
});
