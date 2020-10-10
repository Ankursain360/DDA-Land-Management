$("#btnGenerate").click(function () {
    debugger
    var Id = $("#KhasraNo option:selected").val();
    $.get(`/ReportofLandTransferPropertyNoWise/GetDetails/?id=${Id}`, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
});
