var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;

$(document).ready(function () {
    GetMorLands(currentPageNumber, currentPageSize, sortOrder);
});

function GetExcel(pageNumber, pageSize, order) {
    debugger;
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/MorLands/MorlandList`, 'html', param, function (response) {
        var a = document.createElement("a");
        a.target = '_blank';
        a.href = '/MorLands/download';
        a.click();
    });
}

$("#btndownload").click(function () {
    GetExcel(currentPageNumber, currentPageSize, sortOrder);
});

function GetMorLands(pageNumber, pageSize, sortOrder) {
    var param = GetSearchParam(pageNumber, pageSize, sortOrder);
    HttpPost(`/morLands/List`, 'html', param, function (response) {
        $('#divMorlandTable').html("");
        $('#divMorlandTable').html(response);
    });

}
function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        propertyname: $('#txtProperty').val(),
        sitedesc: $('#txtDescription').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    //    debugger
    return model;
}

$("#btnSearch").click(function () {
    GetMorLands(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending 
    GetMorLands(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetMorLands(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetMorLands(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtProperty').val('');
    $('#txtDescription').val('');
    GetMorLands(currentPageNumber, currentPageSize, sortOrder);
});

function onPaging(pageNo) {
    GetMorLands(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetMorLands(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}

$('#ddlSort').change(function () {
    GetMorLands(currentPageNumber, currentPageSize, sortOrder);
});
//function GetMorLands(pageNumber, pageSize) {
//    var param = GetSearchParam(pageNumber, pageSize);
//    HttpPost(`/morLands/List`, 'html', param, function (response) {
//        $('#divMorlandTable').html("");
//        $('#divMorlandTable').html(response);
//    });
//}

//function GetSearchParam(pageNumber, pageSize) {
//    var model = {
//        name: "test",
//        pageSize: pageSize,
//        pageNumber: pageNumber
//    }
//    return model;
//}

//function onPaging(pageNo) {
//    GetMorLands(pageNo, currentPageSize);
//    currentPageNumber = pageNo;
//}

//function onChangePageSize(pageSize) {
//    GetMorLands(currentPageNumber, pageSize);
//    currentPageSize = pageSize;
//}


//    function DeleteData(id) {

//        var Param = id;

//        $.ajax({
//            url: "/Designation/DeleteConfirmed",
//            data: {id: Param },
//            traditional: true,
//            dataType: "text",
//            contentType: "application/text; charset=utf-8",
//            success: function (result) {
//                if (result) {
//                    $('#myModaldelete').modal('show');
//                } else {
//                    alert("Error occurs on the Database level!");
//                }
//            },
//            error: function () {
//                alert("An error has occured!!!");
//            }
//        });
//}

//    $(function () {
//        $('[name=deletepopup]').click(function () {

//            $('#myModal').modal('show');
//            var href = $(this).attr('href');
//            var myStringArray = href.split('=');
//            var Findid = myStringArray[1];
//            var s = $('#popupid').attr('href', href);
//            return false;
//        });
//        if (window.location.search == "?result=DeleteSuccess") {
//            $('#myModaldelete').modal('show');
//            $.gritter.add({
//                title: 'Record Deleted',
//                text: 'Record Deleted Successfully !!!',
//                class_name: 'with-icon exclamation-circle warning'
//            });
//        }
//    });
