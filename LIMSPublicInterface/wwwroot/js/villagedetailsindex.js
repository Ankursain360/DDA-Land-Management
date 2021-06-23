var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {


    var param = GetSearchParam(currentPageNumber, currentPageSize);

    HttpPost(`/Villagealldetails/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });


});

$("#btnGenerate").click(function () {
  
  
    debugger;
    var UnderSection4Id = $("#Name").val();
    var element = $("#Name").find('option:selected');
    var myTag = element.attr("mytag");
    if (myTag) {
        $('#village_tag').html("Village Name - " + myTag);
    } else {
        $('#village_tag').html("Village Name - ");
        location.reload();
    }
    if (UnderSection4Id) {
    var param = GetSearchParam(currentPageNumber, currentPageSize);

    HttpPost(`/Villagealldetails/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
    } else {
        alert("Please Select Village Name");
    }
});
function GetDetails(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/Villagealldetails/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
        });
    
}


$("#btnReset").click(function () {
    $('#village_tag').html('Village Name - All');

    $('#Name').val('0').trigger('change');

    GetDetails(currentPageNumber, currentPageSize);


});


function GetSearchParam(pageNumber, pageSize) {



    var VillageId = $('#Name option:selected').val();

    var test = [];

    var model = {
      

        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
     
        village: parseInt(VillageId),
 
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
   console.log(arrRight);
    
    
    HttpPost('/DMSUserRight/AddUpdateDmsRight', 'json', arrRight, function (response) {
        SuccessMessage(response);
     // arrPermission = [];
    });
});




