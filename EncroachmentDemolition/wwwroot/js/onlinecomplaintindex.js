var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetComplaint(currentPageNumber, currentPageSize);
});

$("#btnSearch1").click(function () {
    //  alert("Check");
    GetComplaint(currentPageNumber, currentPageSize);
});


$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtContact').val('');
    $('#txtEmail').val('');
  
    $('#txtAddress').val('')

    GetComplaint(currentPageNumber, currentPageSize);
});



function GetComplaint(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Complaint/List`, 'html', param, function (response) {
        $('#divComplaint').html("");
        $('#divComplaint').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var sorbyname = $('#Sortbyd').val();
    var sortdesc = $("#sortdesc").val();
    if (sorbyname) { } else {
        sorbyname = 'Name';
    }
    var model = {
        name: $('#txtName').val(),
        contact: $('#txtContact').val(),
        email: $('#txtEmail').val(),
        address: $('#txtAddress').val(),
        colname: sorbyname,
        orderby: sortdesc,
      

        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}



$("#Sortbyd").change(function () {

    GetComplaint(currentPageNumber, currentPageSize);

});

$("#ascId").click(function () {
    $("#descId").removeClass("active");
    $("#ascId").addClass("active");
    $("#sortdesc").val(2);
    GetComplaint(currentPageNumber, currentPageSize);
});
$("#descId").click(function () {
    $("#ascId").removeClass("active");
    $("#descId").addClass("active");
    $("#sortdesc").val(1);
    GetComplaint(currentPageNumber, currentPageSize);
});


function onPaging(pageNo) {
    GetComplaint(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetComplaint(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}
