var currentPageNumber = 1;
var currentPageSize = 5;

$(document).ready(function () {
    
        var e = document.getElementById("RecentLayer");
        var RecentLayer = e.options[e.selectedIndex].value;
        var e = document.getElementById("Operator");
        var Operator = e.options[e.selectedIndex].value;
        var e = document.getElementById("RecentLayer2");
        var RecentLayer2 = e.options[e.selectedIndex].value;

        var query = RecentLayer + "  " + Operator + "  "+ RecentLayer2;
    $("input[name='Query']").val(query);



    $(".contactinatevalue").change(function () {
        debugger;
        var e = document.getElementById("RecentLayer");
        var RecentLayer = e.options[e.selectedIndex].value;
        var e = document.getElementById("Operator");
        var Operator = e.options[e.selectedIndex].value;
        var e = document.getElementById("RecentLayer2");
        var RecentLayer2 = e.options[e.selectedIndex].value;

        var query = RecentLayer + "  " + Operator + "  " + RecentLayer2;
        $("input[name='Query']").val(query);
    });
})
function Submit() {
    debugger;
    var FirstnameId = $("#FirstName").children("option:selected").val()
    var SecondnameId = $("#SecondName").children("option:selected").val()
    /*var RecentLayer = $("#RecentLayer").children("option:selected").val()*/
    var DataType = $("#DataType").val()
    //var Operator = $("#Operator").children("option:selected").val()
    //var RecentLayer2 = $("#RecentLayer2").children("option:selected").val()
    var Datatype2 = $("#Datatype2").val()
    /*var Operator = $("#Operator").children("option:selected").val()*/

    if (FirstnameId == "") {

        alert("Select Base Layer(T0) is mandatory");
    }
    else if (SecondnameId == "") {

        alert("Select Base Layer(T1) is mandatory");
    }
    
    else if (DataType == "") {

        alert("DataType is mandatory");
    }
   
    else if (Datatype2 == "") {

        alert("DataType is mandatory");
    }
    else {
        var modal = {
            id: FirstnameId,
            pageSize: parseInt(currentPageSize),
            pageNumber: parseInt(currentPageNumber)
        }
        HttpPost(`/DDADecisionSupportSystem/ViewComparingImage/`, 'html', modal, function (response) {


            $('#divComparingImageTable').html("");
            $('#divComparingImageTable').html(response);

        });
    }

    //var url = "/DDADecisionSupportSystem/getdata?Id=" + Id;
    //window.location.href = url; 
    
    //$.ajax({
    //    url: 'ViewComparingImage',
    //    type: 'GET',
    //    data: { id: Id },
    //    success: function (data) {
    //        console.log(data);
    //    }
    //});
    
}
