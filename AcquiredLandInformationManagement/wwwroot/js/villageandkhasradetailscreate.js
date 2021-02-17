function onChange(id) {

    HttpGet(`/VillageAndKhasraDetails/GetKhasraList/?villageId=${id}`, 'json', function (response) {

        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

        $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};


$("#btnGenerate").click(function () {

    debugger;
    var param = GetSearchParam(currentPageNumber, currentPageSize);

    HttpPost(`/VillageAndKhasraDetails/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });

});
function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/VillageAndKhasraDetails/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });

}


function GetSearchParam(pageNumber, pageSize) {



    var VillageId = $('#Name option:selected').val();

    var test = [];

    var model = {


        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),

        village: parseInt(VillageId),

    }
    test.push(model);
    return model;
}


