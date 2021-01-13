

var currentPageNumber = 1;
var currentPageSize = 10;
$(document).ready(function () {
    debugger;
    var date = new Date();
    var fromDate ;
    var toDate ;
    var id = $('#txtid').val();
    GetHistory(currentPageNumber, currentPageSize, fromDate, toDate, id);

});

    $("#btnFilter").click(function () {
        debugger;
        var result = ValidateForm();
        var fromDate = $('#txtFromDate').val();
        var toDate = $('#txtToDate').val();
        var id = $('#txtid').val();
    
       
            GetHistory(currentPageNumber, currentPageSize, fromDate, toDate,id);
        
    });


function GetHistory(pageNumber, pageSize, fromDate, toDate,Id) {
    var param = GetSearchParam(pageNumber, pageSize, fromDate, toDate,Id);
    debugger;
    HttpPost(`/CurrentStatusOfHandedOverTakenOverLand/HistoryDetails`, 'html', param, function (response) {
        $('#LoadView').html("");
        $('#LoadView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, fromDate, toDate,Id) {
    debugger;
    var model = {
        name: "test",
        PageSize: pageSize,
        PageNumber: pageNumber,
        fromDate: fromDate==undefined?null:fromDate,
        toDate: toDate == undefined ?null:toDate,
        landtransferId:parseInt(Id)
      
    }
    return model;
}

function onPaging(pageNo) {
    debugger;
    pageNo = parseInt(pageNo);
  
    var FromDate = $('#txtFromDate').val();
    var ToDate = $('#txtToDate').val();
    var id = $('#txtid').val();

  
    GetHistory(currentPageNumber, currentPageSize, FromDate, ToDate,id);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    debugger;
    pageSize = parseInt(pageSize);
    var FromDate = $('#txtFromDate').val();
    var ToDate = $('#txtToDate').val();
    var id = $('#txtid').val();
  
    GetHistory(currentPageNumber, currentPageSize, FromDate, ToDate,id);
    currentPageSize = pageSize;
}

