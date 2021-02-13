var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
    GetProposalplotdetails(currentPageNumber, currentPageSize, sortOrder);
});

function GetProposalplotdetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/paymentDetail/List`, 'html', param, function (response) {
        $('#divPaymentdetailTable').html("");
        $('#divPaymentdetailTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        demandListNo: $('#txtDemand').val(),
        bankName: $('#txtBank').val(),
        chequeNo: $('#txtCheque').val(),
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

    $('#txtDemand').val('');
    $('#txtBank').val(''),
        $('#txtCheque').val(''),
        //$('#txtDate').val(''),
        GetProposalplotdetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetProposalplotdetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
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
