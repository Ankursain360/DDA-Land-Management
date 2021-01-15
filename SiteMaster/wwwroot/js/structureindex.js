var currentPageNumber = 1;
var currentPageSize = 5;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;

$(document).ready(function () {
    GetStructure(currentPageNumber, currentPageSize);
});

$("#btnSearch").click(function () {
    GetStructure(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {
   
    $('#txtName').val('');
  
    GetStructure(currentPageNumber, currentPageSize);
});


function GetStructure(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Structure/List`, 'html', param, function (response) {
        $('#divStructureTable').html("");
        $('#divStructureTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
       // name: "test",
        name: $('#txtName').val(),
       
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetStructure(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetStructure(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}

// ********** Sorting Code  **********


function GetStructureOrderBy(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/Structure/List`, 'html', param, function (response) {
        $('#divStructureTable').html("");
        $('#divStructureTable').html(response);
    });
}
    function Ascending() {
        $("#btnDescending").removeClass("active");
        $("#btnAscending").addClass("active");
        var value = $("#ddlSort").children("option:selected").val();
        if (value !== "0") {
            GetStructureOrderBy(currentPageNumber, currentPageSize, currentSortOrderAscending);
        }
        else {
            alert('Please select SortBy Value');
        }
    };

    function Descending() {
        $("#btnAscending").removeClass("active");
        $("#btnDescending").addClass("active");
        var value = $("#ddlSort").children("option:selected").val();
        if (value !== "0") {
            GetStructureOrderBy(currentPageNumber, currentPageSize, currentSortOrderDescending);
        }
        else {
            alert('Please select SortBy Value');
        }
    };



    function GetSearchParamaOrderby(pageNumber, pageSize, sortOrder) {
        var model = {
            name: $('#txtName').val(),
            sortBy: $("#ddlSort").children("option:selected").val(),
            sortOrder: parseInt(sortOrder),
            pageSize: parseInt(pageSize),
            pageNumber: parseInt(pageNumber)
        }
        return model;
    }

