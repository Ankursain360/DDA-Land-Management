$("#btnGenerate").click(function () {
    debugger
    var result = ValidateForm();
    if (result) {
        var Id = $("#KhasraNo option:selected").val();
        $.get(`/ReportofLandTransferPropertyNoWise/GetDetails/?id=${Id}`, function (response) {
            $('#LoadView').html("");
            $('#LoadView').html(response);
        });
    }
});