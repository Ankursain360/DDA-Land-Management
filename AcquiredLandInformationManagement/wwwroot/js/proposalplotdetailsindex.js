var currentPageNumber = 1;
var currentPageSize = 2;

$(document).ready(function () {
    GetProposalplotdetails(currentPageNumber, currentPageSize);
});

function GetProposalplotdetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/proposalPlotDetails/List`, 'html', param, function (response) {
        $('#divProposalplotdetailsTable').html("");
        $('#divProposalplotdetailsTable').html(response);
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
    GetProposalplotdetails(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetProposalplotdetails(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

