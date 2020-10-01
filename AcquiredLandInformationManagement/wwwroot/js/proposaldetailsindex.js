var currentPageNumber = 1;
var currentPageSize = 2;

$(document).ready(function () {
    GetProposaldetails(currentPageNumber, currentPageSize);
});

function GetProposaldetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/proposalDetails/List`, 'html', param, function (response) {
        $('#divProposaldetailsTable').html("");
        $('#divProposaldetailsTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    GetProposaldetails(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetProposaldetails(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

