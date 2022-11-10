var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetProposalplotdetails(currentPageNumber, currentPageSize, sortOrder);
});


function GetExcel(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/UnderSection17Details/Undersection17List`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/UnderSection17Details/download';
        a.click();
    });
}

$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortOrder);
});

function GetProposalplotdetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/underSection17Details/List`, 'html', param, function (response) {
        $('#divUnderSection17Table').html("");
        $('#divUnderSection17Table').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        number: $('#txtNumber').val(),
        undersection6: $('#txtNotification6').val(),
     
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

$("#btnSearch").click(function () {
    GetProposalplotdetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    $('#txtNumber').val('');
    $('#txtNotification6').val(''),
        
        GetProposalplotdetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    debugger;
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetProposalplotdetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    debugger;
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetProposalplotdetails(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetProposalplotdetails(currentPageNumber, currentPageSize, sortOrder);
});

function onPaging(pageNo) {
    GetProposalplotdetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetProposalplotdetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}


