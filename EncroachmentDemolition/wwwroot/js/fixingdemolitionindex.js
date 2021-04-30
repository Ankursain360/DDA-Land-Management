var currentPageNumber = 1;
var currentPageSize = 5;

$(document).ready(function () {
    GetLandTransfer(currentPageNumber, currentPageSize);
});

$("#btnSearch1").click(function () {
    //  alert("Check");
    GetLandTransfer(currentPageNumber, currentPageSize);
});

$("#btnReset").click(function () {
    $('#txtLocality').val('');
    $('#txtKhasra').val('');
    $('#txtPoliceStation').val('');

    GetLandTransfer(currentPageNumber, currentPageSize);
});


function GetLandTransfer(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/AnnexureA/List`, 'html', param, function (response) {
        $('#divAnnexureALandTable').html("");
        $('#divAnnexureALandTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    
    var sorbyname = $('#Sortbyd').val();
    var sortdesc = $("#sortdesc").val();
 
    if (sorbyname) { } else {
        sorbyname = 'Locality';
    }


    var model = {

        khasra: $('#txtKhasra').val(),
        locality: $('#txtLocality').val(),
        policestation: $('#txtPoliceStation').val(),
      
        colname: sorbyname,
        orderby: sortdesc,

        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}






$("#Sortbyd").change(function () {

    GetLandTransfer(currentPageNumber, currentPageSize);

});

$("#ascId").click(function () {
    $("#descId").removeClass("active");
    $("#ascId").addClass("active");
    $("#sortdesc").val(2);
    GetLandTransfer(currentPageNumber, currentPageSize);
});
$("#descId").click(function () {
    $("#ascId").removeClass("active");
    $("#descId").addClass("active");
    $("#sortdesc").val(1);
    GetLandTransfer(currentPageNumber, currentPageSize);
});











function onPaging(pageNo) {
    GetLandTransfer(parseInt(pageNo), currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetLandTransfer(currentPageNumber, parseInt(pageSize));
    currentPageSize = pageSize;
}
