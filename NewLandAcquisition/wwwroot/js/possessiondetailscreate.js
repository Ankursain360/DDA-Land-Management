    function onChange(id) {
 
    HttpGet(`/NewlandPossesionDetails/GetKhasraList/?villageId=${id}`, 'json', function (response) {
      
        var html = '<option value=""> --Select--</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

        $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};
$("#KhasraId").change(function () {
    var kid = $(this).val();
    if (kid) {
        HttpGet(`/NewlandPossesionDetails/GetAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);
            // alert(JSON.stringify(response));
        });

    }
});


$("input[name='IsVacant']").click(function () {/* -----------Added by Renu  --------------- */
    if ($("#IsVacant").is(":checked"))
        $("#PossType").val("Vacant");
    else
        $("#PossType").val("");

    $('.chkAction:checkbox:checked').each(function () {
        var arrIds = $(this).attr("id");
        $("#PossType").val(arrIds);
    });
});

$("input[name='IsBuiltup']").click(function () {/* -----------Added by Renu  --------------- */
    if ($("#IsBuiltup").is(":checked"))
        $("#PossType").val("Built Up");
    else
        $("#PossType").val("");

    $('.chkAction:checkbox:checked').each(function () {
        var arrIds = $(this).attr("id");
        $("#PossType").val(arrIds);
    });
});
$('#DocumentIFormFile').change(function () {
    debugger;
    var fileInput = document.getElementById('DocumentIFormFile');
    var filePath = fileInput.value;
    const size = (DocumentIFormFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
    debugger;

    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Invalid file type');
        fileInput.value = '';
        return false;
    }
    if (size > 10535049) {
        alert("File must be of 10 MB or Lesser Than 10 MB");
        fileInput.value = '';
        return false;
    }

}

function funHideAnd(value) {
    if (value == "No") {
        $("#Show_1").show();
        $("#Show_2").show();

    }

    else {
        $("#Show_1").hide();
        $("#Show_2").hide();
    }


}