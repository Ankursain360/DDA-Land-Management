var currentPageNumber = 1;
var currentPageSize = 2;

$(document).ready(function () {
    GetModule(currentPageNumber, currentPageSize);
});

function GetModule(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/module/List`, 'html', param, function (response) {
        $('#divModuleTable').html("");
        $('#divModuleTable').html(response);
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
    GetModule(pageNo, currentPageSize);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetModule(currentPageNumber, pageSize);
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
