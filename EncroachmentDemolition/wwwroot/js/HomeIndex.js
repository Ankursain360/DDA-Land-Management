
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 
var filterdata = '';
var modelopen = false;
$(document).ready(function () {
    var userId = parseInt($('#hdnUserid').val());
    var roleId = parseInt($('#hdnRoleid').val());
    GetDashboard(userId, roleId);
});


function GetDashboard(userId, roleId) {
    HttpGet(`/Demolitionstructuredetails/GetDashboard/?userId=${userId}&roleId=${roleId}`, 'html', function (response) {
        $('#divDashboard').html("");
        $('#divDashboard').html(response);
    });
};

function showdata(filter) {
    filterdata = filter;
    Pagewisedata(currentPageNumber, currentPageSize, sortOrder, filter)
}
function Pagewisedata(pageNumber, pageSize, sortOrder, filter) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder, filter)
    $('#hfiltertext').empty().text('(' + filter + ')');
    HttpPost(`/Demolitionstructuredetails/GetDashboardListData`, 'html', param, function (response) {
        $('#divModelContent').empty().html(response);
        if (modelopen == false) {
            modelopen = true;
            $('#btnShowModel').click();//show model
        }

    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder, filterdata) {
    var userId = parseInt($('#hdnUserid').val());
    var roleId = parseInt($('#hdnRoleid').val());
    var model = {
        filter: filterdata,
        userId: userId,
        roleId: roleId,
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    Pagewisedata(parseInt(pageNo), parseInt(currentPageSize), sortOrder, filterdata);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    Pagewisedata(parseInt(currentPageNumber), parseInt(pageSize), sortOrder, filterdata);
    currentPageSize = pageSize;
}
function resetModel() {
    modelopen = false;
}