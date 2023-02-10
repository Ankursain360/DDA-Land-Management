var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 


$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
   
});

function GetExcel(pageNumber, pageSize, order) {
    debugger;
    var param = GetSearchParam(pageNumber, pageSize, order);
    if (param.village == "" && param.title == "") {
       
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/LandAcquisitionAwards/DownloadallLandAcquisitionAwards';
            a.click();
       

    }
    else {
    HttpPost(`/LandAcquisitionAwards/DownloadLandAcquisitionAwards`, 'html', param, function (response) {
            var a = document.createElement("a");
            a.target = '_blank';
            a.href = '/LandAcquisitionAwards/Download';
            a.click();
    });
    }
}

$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#txtVillage').val('');
    $('#txtAwards').val('');
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDetails(currentPageNumber, currentPageSize, sortOrder); 
});

function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/LandAcquisitionAwards/List`, 'html', param, function (response) {
        $('#divLandAcquisitionAwards').html("");
        $('#divLandAcquisitionAwards').html(response);
    });


}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        village: $('#txtVillage').val(),
        title: $('#txtAwards').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}


function onPaging(pageNo) {
    GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}


