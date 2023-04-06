var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 
var ImagesId = '';
var modelopen = false;
$(document).ready(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnSearch").click(function () {
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
$('#ddlSort').change(function () {
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});
$("#btnReset").click(function () {

    $('#DepartmentId').val('0').trigger('change');
    $('#ZoneId').val('0').trigger('change');
    $('#DivisionId').val('0').trigger('change');
    GetDetails(currentPageNumber, currentPageSize, sortOrder);
});

function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParamAppDetails(pageNumber, pageSize, sortOrder);
    HttpPost(`/VacantLandAppdetails/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
} 

//function GetSearchParam(pageNumber, pageSize, sortOrder) {
//    var model = {
//        name: "test",
//        departmentId: parseInt($('#DepartmentId').val()),
//        zoneId: parseInt($('#ZoneId').val()),
//        divisionId: parseInt($('#DivisionId').val()),
//        inventoriedId: parseInt($('#InventoriedInId').val()),
//        classificationOfLandId: parseInt($('#ClassificationOfLandId').val()),
//        plannedUnplannedLand: $('#PlannedUnplannedLand').val(),
//        sortBy: $("#ddlSort").children("option:selected").val(),
//        sortOrder: parseInt(sortOrder),
//        pageSize: pageSize,
//        pageNumber: pageNumber
//    }
//    return model;
//}
function GetSearchParamAppDetails(pageNumber, pageSize, sortOrder, ImagesId) {
    var model = {
        departmentId: parseInt($("#DepartmentId").children("option:selected").val()),
        zoneId: parseInt($("#ZoneId").children("option:selected").val()),
        divisionId: parseInt($("#DivisionId").children("option:selected").val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: pageSize,
        Id : ImagesId,
        pageNumber: pageNumber
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

function GetZoneList(id) {
    HttpGet(`/VacantLandAppdetails/GetZoneList/?departmentId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").val('0').trigger('change');
        $("#ZoneId").html(html);
        /* $("#DivisionId").val('0').trigger('change');*/
    });
};

//Bind Divison and Locality Dropdown from Department
function GetDivisionList(id) {
    debugger;
    HttpGet(`/VacantLandAppdetails/GetDivisionList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").val('0').trigger('change');
        $("#DivisionId").html(html);
    });
};
function GetPrimaryList(id) {
    debugger;
    var param = GetSearchParamAppDetails(currentPageNumber, currentPageSize, sortOrder, id);
    HttpPost(`/VacantLandAppdetails/GetPrimaryList`, 'json', param, function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].DivisionId + '>' + response[i].primaryListNo + '</option>';
        }
        $("#PrimaryListId").val('0').trigger('change');
        $("#PrimaryListId").html(html);
       
    });
}

function ShowImages(id) {
    debugger;
    HttpGet(`/VacantLandAppdetails/fetchAppUploadImagesFile/?id=${id}`, 'html', function (response) {
        $('#divModelContent').empty().html(response);
        if (modelopen == false) {
            modelopen = true;
            $('#btnShowimagesModel').click();//show model
        }
    });
};
function resetModel() {
    modelopen = false;
}
