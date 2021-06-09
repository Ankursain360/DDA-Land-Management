//function onChange(id) {

//    HttpGet(`/NewLandEnhanceCompensation/GetKhasraList/?villageId=${id}`, 'json', function (response) {
//        var html = '<option value="0"></option>';
//        for (var i = 0; i < response.length; i++) {
//            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
//        }

//        $("#KhasraId").select2('val', '')
//        $("#KhasraId").html(html);
//    });
//};

//$("#KhasraId").change(function () {
//    var kid = $(this).val();
//    if (kid) {
//        HttpGet(`/NewLandEnhanceCompensation/GetAreaList/?khasraid=${kid}`, 'json', function (response) {

//            $("#Bigha").val(response.bigha);
//            $("#Biswa").val(response.biswa);
//            $("#Biswanshi").val(response.biswanshi);
//            // alert(JSON.stringify(response));
//        });

//    }
//});
function onChange(id) {

    HttpGet(`/Newlandus17plot/GetKhasraList/?villageId=${id}`, 'json', function (response) {
        var html = '<option value=""> select</option>';
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
        HttpGet(`/Newlandus17plot/GetKhasraAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);

        });

    }
});


$('#ENMDocumentIFormFile').change(function () {
    var fileInput = document.getElementById('ENMDocumentIFormFile');
    var filePath = fileInput.value;
    const size = (ENMDocumentIFormFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
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