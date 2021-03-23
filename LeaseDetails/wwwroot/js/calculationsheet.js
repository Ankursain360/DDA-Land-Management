


//$("#ApplicationId").change(function () {
//    var id = $(this).val();
  
//    if (id) {
//        HttpGet(`/CalculationSheet/GetApplicationAreaDetails/?ApplicationId=${id}`, 'json', function (response) {

//            $("#AllotedArea").val(response.allotedArea);
//            //$("#BuildingArea").val(response.buildingArea);
//            $("#PlayGroundArea").val(response.playGroundArea);

//        });

//    }
//});

//function GetReport(pageNumber, pageSize, sortOrder) {
//    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
//    HttpPost(`/ReportofLandTransferDepartmentWise/List`, 'html', param, function (response) {
//        $('#LoadReportView').html("");
//        $('#LoadReportView').html(response);
//    });
//}

$("#ApplicationId").change(function () {
    var id = $(this).val();

    if (id) {
        HttpGet(`/CalculationSheet/Receipt/?ApplicationId=${id}`, 'html', function (response) {

           
            $('#View').html("");
            $('#View').html(response);

        });

    }
});
