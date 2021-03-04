$(function () {
    $('#drp1').change(function () {
        var id = $("#drp1").val();
        if (id == 1) {
            $("#txt1").show();

        }
        else {
            $("#txt1").hide();
        }
    });
});

$(function () {
    $('#drp2').change(function () {
        var id = $("#drp2").val();
        if (id == 1) {
            $("#txt2").show();

        }
        else {
            $("#txt2").hide();
        }
    });
});


$(function () {
    $('#drp3').change(function () {
        var id = $("#drp3").val();
        if (id == 1) {
            $("#txt3").show();

        }
        else {
            $("#txt3").hide();
        }
    });
});

$(function drp4change() {

    $('#drp4').change(function () {
        var id = $("#drp4").val();
        if (id == 1) {
            $("#txt4").show();

        }
        else {
            $("#txt4").hide();
        }
    });
});
$(function drpHasLandchange() {

    $('#drpHasLand').change(function () {
        var id = $("#drpHasLand").val();
        if (id == 0) {
            $("#txtDateLand").hide();
            $("#txtLand").hide();
            $("#Sn7Filep").hide();
        }
        else {
            $("#txtDateLand").show();
            $("#txtLand").show();
            $("#Sn7Filep").show();
        }
    });
});




