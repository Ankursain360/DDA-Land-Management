var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
   
    var param = GetSearchParam(currentPageNumber, currentPageSize);

    HttpPost(`/DMSUserRight/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
   

})

$("#btnGenerate").click(function () {
  
    debugger;
    var param = GetSearchParam(currentPageNumber, currentPageSize);

    HttpPost(`/DMSUserRight/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });

});
function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/DMSUserRight/GetDetails`, 'html', param, function (response) {
            $('#LoadReportView').html("");
            $('#LoadReportView').html(response);
        });
    
}


function GetSearchParam(pageNumber, pageSize) {



    var departmentId = $('#DepartmentId option:selected').val();

    var test = [];

    var model = {
      

        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
     
        department: parseInt(departmentId),
 
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

$("#btnSaveBottom").click(function () {
    var arrRight = [];
    var y = 0;
    $('.chkView:checkbox:checked').each(function () {
        var v = $(this).val();
      
        if ($('#download-' + v).prop("checked") == true) {
            var dtrue = 1;
        } else {
            var dtrue = 0;
        }
        if ($('#view-' + v).prop("checked") == true) {
            var vtrue = 1;
        } else {
            var vtrue = 0;
        }
        
        if(v != y) {
            var model = {
                userId: v,
                Viewright: vtrue,
                Downloadright: dtrue
            }
            arrRight.push(model);
             y = v;
        }
    });
  // console.log(arrRight);
    
    
    HttpPost('/DMSUserRight/AddUpdateDmsRight', 'json', arrRight, function (response) {
        SuccessMessage(response);
     // arrPermission = [];
    });
});




