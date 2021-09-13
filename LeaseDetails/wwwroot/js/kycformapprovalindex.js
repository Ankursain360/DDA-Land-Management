var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1; //default Ascending 

$(document).ready(function () {
    var StatusId = 0;
    GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
});

$("#btnSearch").click(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
});

$("#btnReset").click(function () {
    $('#txtId').val('');
    $('#txtName').val('');
    $('#txtFileNo').val('');
    $('#txtZone').val('');
    $('#txtLocality').val('');
    $('#txtPlot').val('');
    $('#ApprovalStatus').val('0').trigger('change');
    GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1; //for Ascending
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
});

$('#ddlSort').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
});

function GetDetails(pageNumber, pageSize, sortOrder, StatusId) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder, StatusId);
    HttpPost(`/KycFormApproval/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder, StatusId) {
    var model = {
        
        Id: $('#txtId').val(),
        property: $('#txtName').val(),
        Fileno: $('#txtFileNo').val(),
        zone: $('#txtZone').val(),
        locality: $('#txtLocality').val(),
        PlotNo: $('#txtPlot').val(),
        StatusId: parseInt(StatusId),

        approvalstatusId: parseInt($("#ApprovalStatus option:selected").val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder, StatusId);
        currentPageNumber = pageNo;
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortOrder, StatusId);
        currentPageNumber = pageNo;
    }
}

function onChangePageSize(pageSize) {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder, StatusId);
        currentPageSize = pageSize;
    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortOrder, StatusId);
        currentPageSize = pageSize;
    }
}

$("input[name='radioStatus']").click(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }

});

$('#ApprovalStatus').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
});



$('#txtId').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
});


$('#txtName').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
});

$('#txtFileNo').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
});

$('#txtZone').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
});

$('#txtLocality').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
}); 

$('#txtPlot').change(function () {
    if ($("#Pending").is(":checked")) {
        var StatusId = 0;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);

    }
    else if ($("#Approved").is(":checked")) {
        var StatusId = 1;
        GetDetails(currentPageNumber, currentPageSize, sortOrder, StatusId);
    }
}); 