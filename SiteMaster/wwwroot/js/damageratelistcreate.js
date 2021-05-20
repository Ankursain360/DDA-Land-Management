var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {

});

function GetDateRangeDetails(id) {
    debugger;
    HttpGet(`/DamageRateList/GetDateRangeList/?Id=${id}`, 'json', function (response) {
        $("#DateRangeId").val('').trigger('change');
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DateRangeId").html(html);
    });
}

$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/DamageRateList/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
    $("#SaveNewRateDiv").show();
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        propertyid: parseInt($('#PropertyId option:selected').val()),
        daterangeid: parseInt($('#DateRangeId option:selected').val()),
        localityid: parseInt($('#LocalityId option:selected').val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}