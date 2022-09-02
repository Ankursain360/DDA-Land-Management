var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 

$(document).ready(function () {
    GetProposaldetails(currentPageNumber, currentPageSize, sortOrder);
})

$("#btnGenerate").click(function () {
    debugger;
    var VillageId = $("#VillageId").val();  
    if (VillageId == 0 || VillageId == null) {       
        alert("Please Select Village");
        window.location.reload();    
    }
    else {        
        GetDetails(parseInt(currentPageNumber), parseInt(currentPageSize));
        //Download(parseInt(currentPageNumber), parseInt(currentPageSize));
    }

});

$("#btndownload").click(function () {
    debugger;
    var VillageId = $("#VillageId").val();
    if (VillageId == 0 || VillageId == null) {
        alert("Please Select Village");
        window.location.reload();
    } 
    else {         
    Download(parseInt(currentPageNumber), parseInt(currentPageSize));
    }
});
function GetDetails(pageNumber, pageSize) {
    debugger;
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/VillageDetailsReport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });

}

function Download(pageNumber, pageSize) {
    debugger;
    var param = GetSearchParam(pageNumber, pageSize);
    let a = document.createElement('a');
    a.target = '_blank';
    a.href = "../VillageDetailsReport/Download/" + param.VillageId ;
    a.click();
}


$("#btnReset").click(function () {
    document.getElementById("VillageId").selectedIndex = ""; 
    location.reload(); 
});


function GetSearchParam(pageNumber, pageSize) { 
    debugger;

    var VillageId = $('#VillageId option:selected').val();

    var test = [];

    var model = {


        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),

        VillageId: parseInt(VillageId),

    }
    test.push(model);
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

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetProposaldetails(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetProposaldetails(currentPageNumber, currentPageSize, sortOrder);
});
