var currentPageNumber = 1;
var currentPageSize = 5;

$(document).ready(function () {
    GetDesignation(currentPageNumber, currentPageSize);
});

function GetDesignation(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/designation/List`, 'html', param, function (response) {
        $('#divDesignationTable').html("");
        $('#divDesignationTable').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: "test",
        pageSize: pageSize,
        pageNumber: pageNumber
    }
    return model;
}

function onPaging(pageNo) {
    GetDesignation(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDesignation(currentPageNumber, pageSize);
    currentPageSize = pageSize;
}


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
