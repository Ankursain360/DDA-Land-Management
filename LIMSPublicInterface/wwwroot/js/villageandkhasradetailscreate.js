var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {





})

$("#btnGenerate").click(function () {

    debugger;
    var param = GetSearchParam(currentPageNumber, currentPageSize);
    alert(JSON.stringify(param));
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


$("#btnReset").click(function () {

    $('#Name').val('0').trigger('change');


    GetDetails(currentPageNumber, currentPageSize);


});


function GetSearchParam(pageNumber, pageSize) {



    var VillageId = $('#KhasraId option:selected').val();

    var test = [];

    var model = {


        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),

        Khasraid: parseInt(VillageId),

    }
    test.push(model);
    return model;
}
function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}


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
