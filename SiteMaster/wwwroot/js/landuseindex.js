var currentPageNumber = 1;
var currentPageSize = 10;
var currentsortOrderAsc = 1;
var currentsortOrderdesc = 2;

$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize);
});


function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
      HttpPost(`/LandUse/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });

    //if ($('table >tbody >tr').length <= 1) {
    //    GetDetails(1, $("#ddlPageSize option:selected").val());
    //}
}

function GetLandUseOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamOrderby(pageNumber, pageSize, order);
    HttpPost(`/LandUse/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}


function GetSearchParamOrderby(pageNumber, pageSize, order) {
    var model = {
        name: $('#txtName').val(),
        sortBy: $("ddlViewBy").childern("option:selected").val();
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    debugger
    return model;
}


$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {
    $("#txtName").val('');
    GetDetails(currentPageNumber, currentPageSize);
});

$("#btnAsc").click(function () {

    // var e = document.getElementById("ddlViewBy");
    //sortBy = e.value;
    var sortBy = $("ddlViewBy").childern("option:selected").val();
    $('#txtName').val('');
    //document.getElementById("ddlViewBy").value;
    //sortOrder = 'Asc';
    if (sortBy !== "0") {

        GetLandUseOrderby(currentpageNumber, currentpageSize, currentsortOrderAsc);
    }
    else { alert("Please select Sort by value."); }
    //GetDetails(currentPageNumber, currentPageSize);
});






function GetSearchParam(pageNumber, pageSize) {
    var model = {
          name: $('#txtName').val(),
          pageSize: pageSize,
        pageNumber: pageNumber
            }
   // debugger
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