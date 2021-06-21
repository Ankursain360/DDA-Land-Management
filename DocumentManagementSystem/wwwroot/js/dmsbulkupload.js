$(document).ready(function () {
    $('#FileUpload').change(function () {
        alert("test");
        var filedata = "";
        var fileInput = document.getElementById('FileUpload');
        var filePath = fileInput.value;
        const size = (FileUpload.files[0].size);
        fileValidation(filePath, fileInput, size);
        debugger;
        var fileinputs = $('#FileUpload');
        var reader = new FileReader();
        reader.readAsDataURL(fileinputs.files[0]);

        reader.onload = function () {
            filedata = (reader.result);//base64encoded string
        };
        reader.onerror = function (error) {
            console.log('Error: ', error);
        };
    });
});


$("#ZoneId").change(function () {
    // alert(ZoneId);
    var ids = $(this).val();
    if (ids) {
        HttpGet(`/DMSFileUpload/AllVillagedataList/?zoneid=${ids}`, 'json', function (response) {
            var html = '<option value="">Select</option>';
            for (var i = 0; i < response.length; i++) {
                html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
            }
            $("#VillageId").select2('val', '')
            $("#VillageId").html(html);

            //  alert(JSON.stringify(response));
        });

    }
});


$("#btnSaveBulkUploadInfo").click(function () {
    debugger;
    var param = GetListData();
    var validateForm = $("#frmInfo").valid();
    if (validateForm) {
        HttpPost(`/DMSFileUpload/SaveBulkUploadDetails`, 'json', param, function (response) {
            SuccessMessage('Saved Data Successfully successfully.');
        });
    }

});

function GetListData() {
    debugger;
    var filedata = "";
    var fileUpload = $("#FileUpload").get(0);
    var files = fileUpload.files;
    var fileInput = $('#FileUpload');
    var filePath = fileInput.value;

    var reader = new FileReader();
    reader.readAsDataURL(fileInput.files[0]);

    reader.onload = function () {
        filedata= (reader.result);//base64encoded string
    };
    reader.onerror = function (error) {
        console.log('Error: ', error);
    };
    var model = {
        DepartmentId: parseInt($('#DepartmentId').val()),
        PdfLocationPath: ($('#PdfLocationPath').val()),
        FileUpload: (filedata)
    }
    return model;
}

$('#FileUpload').change(function () {
    var filedata = "";
    var fileInput = document.getElementById('FileUpload');
    var filePath = fileInput.value;
    const size = (FileUpload.files[0].size);
    fileValidation(filePath, fileInput, size);
    debugger;
    var fileinputs = $('#FileUpload');
    var reader = new FileReader();
    reader.readAsDataURL(fileinputs.files[0]);

    reader.onload = function () {
        filedata = (reader.result);//base64encoded string
    };
    reader.onerror = function (error) {
        console.log('Error: ', error);
    };
});

$('body').on('change', '#FileUpload', function () {
    debugger;
    var fileInput = document.getElementById('#FileUpload');

    var reader = new FileReader();
    reader.readAsDataURL(fileInput.files[0]);

    reader.onload = function () {
        filedata = (reader.result);//base64encoded string
    };
    reader.onerror = function (error) {
        console.log('Error: ', error);
    };

})