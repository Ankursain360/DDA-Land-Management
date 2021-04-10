var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetAcquiredLandVillage(currentPageNumber, currentPageSize, sortOrder);
});


function GetAcquiredLandVillage(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/SubstitutionMutationDetails/List`, 'html', param, function (response) {
        $('#divMutationTable').html("");
        $('#divMutationTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        
        locality: $('#txtLocality').val(),
        district: $('#txtDistrict').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

$("#btnSearch").click(function () {
    GetAcquiredLandVillage(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {

    
    $('#txtLocality').val(''),
        $('#txtDistrict').val(''),

    GetAcquiredLandVillage(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetAcquiredLandVillage(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetAcquiredLandVillage(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetAcquiredLandVillage(currentPageNumber, currentPageSize, sortOrder);
});

function onPaging(pageNo) {
    GetAcquiredLandVillage(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetAcquiredLandVillage(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}


$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetWatchandward(currentPageNumber, currentPageSize, StatusId);
    }

});
