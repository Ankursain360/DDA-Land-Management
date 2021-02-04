var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 
var filedoc = 0;

$(function () {
   
    GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);
});

function GetFileDetails(pageNumber, pageSize, sortOrder,filedoc) {
    var param = GetSearchParam(pageNumber, pageSize,sortOrder, filedoc);
    HttpPost(`/IssueReturnFile/List`, 'html', param, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
}
$("#btnFind").click(function () {
    if ($("#File").is(":checked")) {
        var filedoc = 0;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);

    }
    else if ($("#Document").is(":checked")) {
        var filedoc = 1;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);
    }
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    if ($("#File").is(":checked")) {
        var filedoc = 0;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);

    }
    else if ($("#Document").is(":checked")) {
        var filedoc = 1;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);
    }
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    if ($("#File").is(":checked")) {
        var filedoc = 0;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);

    }
    else if ($("#Document").is(":checked")) {
        var filedoc = 1;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);
    }
});
$('#ddlSort').change(function () {
    if ($("#File").is(":checked")) {
        var filedoc = 0;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);

    }
    else if ($("#Document").is(":checked")) {
        var filedoc = 1;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);
    }
   
});
$("#btnReset").click(function () {
    $('#Id').val('0').trigger('change');

    if ($("#File").is(":checked")) {
        var filedoc = 0;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);

    }
    else if ($("#Document").is(":checked")) {
        var filedoc = 1;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);
    }
    
});
function GetSearchParam(pageNumber, pageSize, sortOrder, filedoc) {
    var model = {
        name: "test",
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        filedoc: parseInt(filedoc),
        pageSize: pageSize,
        pageNumber: pageNumber,
        FileNo: parseInt($('#Id option:selected').val())
    }
    return model;
}

function onPaging(pageNo) {
    pageNo = parseInt(pageNo);
    if ($("#File").is(":checked")) {
        var filedoc = 0;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);
        currentPageNumber = pageNo;

    }
    else if ($("#Document").is(":checked")) {
        var filedoc = 1;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);
        currentPageNumber = pageNo;
    }
}

function onChangePageSize(pageSize) {
    pageSize = parseInt(pageSize);
    if ($("#File").is(":checked")) {
        var filedoc = 0;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);
        currentPageSize = pageSize;
    }
    else if ($("#Document").is(":checked")) {
        var filedoc = 1;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);
        currentPageSize = pageSize;
    }
   
}


$("input[name='radioStatus']").click(function () {
    if ($("#File").is(":checked")) {
        var filedoc = 0;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);

    }
    else if ($("#Document").is(":checked")) {
        var filedoc = 1;
        GetFileDetails(currentPageNumber, currentPageSize, sortOrder, filedoc);
    }

});