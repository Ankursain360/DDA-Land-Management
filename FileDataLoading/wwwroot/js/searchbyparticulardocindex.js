var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;
var freeholdstatus = 0;
var fileNumber = "";
var fileN = 0;

$(document).ready(function () {
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
});

function GetDataStorage(pageNumber, pageSize, order) {
    debugger
    var test = "";
    var ftest = "";
    fileNumber = document.getElementById('FileNo').value;
    fileN = document.getElementById('FileName').value;
    if (fileNumber != test) {
        //  $('#FileNo').val = "0";
        // fileNumber = "0";
    }
    else { fileNumber = "0"; }
    if (fileN == ftest) {
        //  $('#FileName').val = "0";
        fileN = "0";
    }
    var param = GetSearchParam(pageNumber, pageSize, order);
    //  HttpPost(`/SearchByParticular/List`, 'html', param, function (response) {
    HttpPost(`/SearchByParameterDoc/GetDetails`, 'html', param, function (response) {

        $('#LoadView').html("");
        $('#LoadView').html(response);
    });

}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    debugger
    var model = {
        name: freeholdstatus,
        DeptId: parseInt(($('#DeptId option:selected').val())),
        LocalityId: parseInt(($('#LocalityId option:selected').val())),
        AlmirahId: parseInt(($('#AlmirahId option:selected').val())),
        RowId: parseInt(($('#RowId option:selected').val())),
        BundleId: parseInt(($('#BundleId option:selected').val())),
        ColId: parseInt(($('#ColId option:selected').val())),
        RRNo: $("#RecordRoomId").val(),
        FileNo: fileNumber,//$("#FileNo").val(),
        FileName: fileN,//$("#FileName").val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    //  debugger
    return model;
}
$('#ddlColName').change(function () {
    $("#txtsearchtxt").val('');
});
$("#btnGenerate").click(function () {
    //   debugger
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
});

$("#btnReset").click(function () {
    $("#FileNo").val('');
    $("#FileName").val('');
    $("#txtRoomNo").val('');
    $('#DeptId').val('0').trigger('change');
    $('#LocalityId').val('0').trigger('change');
    $('#AlmirahId').val('0').trigger('change');
    $('#RowId').val('0').trigger('change');
    $('#BundleId').val('0').trigger('change');
    $('#ColId').val('0').trigger('change');
    $('#RecordRoomId').val('0').trigger('change');

    GetDataStorage(currentPageNumber, currentPageSize, sortby);
});
function Descending() {
    $("#btnDescending").addClass("active");
    $("#btnAscending").removeClass("active");

    sortby = 2;     //for Descending
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
};
function Ascending() {
    $("#btnAscending").addClass("active");
    $("#btnDescending").removeClass("active");

    sortby = 1;    //for Asc
    GetDataStorage(currentPageNumber, currentPageSize, sortby);

};
function FreeHoldYes() {
    $("#ryes").checked = true;


    freeholdstatus = 1;    //for Asc
    GetDataStorage(currentPageNumber, currentPageSize, sortby);

};
function FreeHoldNo() {

    freeholdstatus = 0;    //for Asc
    GetDataStorage(currentPageNumber, currentPageSize, sortby);

};

function GetDataStorageOrderby(pageNumber, pageSize, order) {
    FileNumber = document.getElementById('#FileNo').value;
    FileN = document.getElementById('#FileName').value;
    if (FileNumber === '') {
        $('#FileNo').val = "0";
        FileNumber = "0";
    }
    if (FileN === '') {
        $('#FileName').val = "0";
        FileN = "0";
    }
    var param = GetSearchParamOrderby(pageNumber, pageSize, order);
    //    HttpPost(`/SearchByParticular/List`, 'html', param, function (response) {
    HttpPost(`/SearchByParameterDoc/GetDetails`, 'html', param, function (response) {

        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
    function GetSearchParamOrderby(pageNumber, pageSize, sortOrder) {
        debugger

        var model = {

            name: freeholdstatus,
            // searchCol: $('#ddlColName').children("option:selected").val(),
            //  searchText: $("#txtsearchtxt").val(),
            DeptId: parseInt(($('#DeptId option:selected').val())),
            LocalityId: parseInt(($('#LocalityId option:selected').val())),
            AlmirahId: parseInt(($('#AlmirahId option:selected').val())),
            RowId: parseInt(($('#RowId option:selected').val())),
            BundleId: parseInt(($('#BundleId option:selected').val())),
            ColId: parseInt(($('#ColId option:selected').val())),
            RRNo: $("#RecordRoomId").val(),
            FileNo: FileNumber.val(),
            FileName: FileN.val(),
            sortBy: $("#ddlSort").children("option:selected").val(),
            sortOrder: parseInt(sortOrder),
            pageSize: parseInt(pageSize),
            pageNumber: parseInt(pageNumber)
        }
        return model;
    }



    function onPaging(pageNo) {
        GetDataStorage(parseInt(pageNo), parseInt(currentPageSize), sortby);
        currentPageNumber = pageNo;
    }

    function onChangePageSize(pageSize) {
        GetDataStorage(parseInt(currentPageNumber), parseInt(pageSize), sortby);
        currentPageSize = pageSize;
    }


}






