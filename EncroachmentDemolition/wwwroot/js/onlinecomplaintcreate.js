
    $("#slong").on("blur", function (e) {
        $(".err").hide();
    });
    $("#Photolatlong").on("change", function (e) {
        $(".err").hide();
        $("#slat").val('');
        $("#slong").val('');
        $('#slat').attr('readonly', false);
        $('#slong').attr('readonly', false);

        EXIF.getData(e.target.files[0], function () {
            //console.log(EXIF.pretty(this));
            var result = EXIF.pretty(this);
            var allMetaData = EXIF.getAllTags(this);
            console.log(allMetaData.GPSLatitude);
           // alert(allMetaData.GPSLatitude[0]);
            if (allMetaData.GPSLatitude) { 
             
            var lat = allMetaData.GPSLatitude[0].numerator + "." + allMetaData.GPSLatitude[1].numerator + "." + allMetaData.GPSLatitude[2].numerator;
            var long = allMetaData.GPSLongitude[0].numerator + "." + allMetaData.GPSLongitude[1].numerator + "." +allMetaData.GPSLongitude[2].numerator;

            $("#slat").val(lat);
                $("#slong").val(long);
                $('#slat').attr('readonly', true);
                $('#slong').attr('readonly', true);

            } else {
             
                $(".err").show();

          }
        });
    });

$(document).ready(function () {

    $("input[name='DDA']").click(function () {
        var selected = $("input[type='radio'][name='DDA']:checked");
        $("#DDAid").val(selected.val());

    });

});
