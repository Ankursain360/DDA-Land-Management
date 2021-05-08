var currentPageNumber = 1;
var currentPageSize = 5;
var sortby = 1;//default Ascending 
$(document).ready(function () {


    var param = GetSearchParam(currentPageNumber, currentPageSize, sortby);

    HttpPost(`/newlandvillagekhasrareport/GetDetails`, 'html', param, function (response) {

        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
  
})

$("#btnGenerate").click(function () {

    var sort = $('#ddlSort option:selected').val();
    var vil = $('#NewLandvillageId option:selected').val();
    var name = $('#Name option:selected').val();
    if (vil == null) {
        alert("Please select village")

    } else
        if (name == null) {
            alert("Please select khasra no")
        } else
            if (sort == null) {
                alert("Please select valid sort by option");
                $('#ddlSort option:selected').val('Village');
            }

            else {
                GetDetails(currentPageNumber, currentPageSize, sortby);
            }

});


function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
  
    HttpPost(`/newlandvillagekhasrareport/GetDetails`, 'html', param, function (response) {
        $('#LoadReportView').html("");
        $('#LoadReportView').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {

    var name = $('#Name option:selected').val();
    var villageid = $('#NewLandvillageId option:selected').val();

    var model = {
        name: "report",
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber),
        name: parseInt(name),
        villageId: parseInt(villageid),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
    }
    return model;
}
$("#btnAscending").click(function () {

    sortby = 1;//for Ascending
    var sort = $('#ddlSort option:selected').val();

    var sort = $('#ddlSort option:selected').val();
    var vil = $('#NewLandvillageId option:selected').val();
    var name = $('#Name option:selected').val();
    if (vil == null) {
        alert("Please select village")

    } else
        if (name == null) {
            alert("Please select khasra no")
        } else
            if (sort == null) {
                alert("Please select valid sort by option");
                $('#ddlSort option:selected').val('Village');
            }

            else {
                $("#btnDescending").removeClass("active");
                $("#btnAscending").addClass("active");
                GetDetails(currentPageNumber, currentPageSize, sortby);
            }

});


$("#btnDescending").click(function () {

    sortby = 2;//for Descending

    var sort = $('#ddlSort option:selected').val();
    var vil = $('#NewLandvillageId option:selected').val();
    var name = $('#Name option:selected').val();
    if (vil == null) {
        alert("Please select village")

    } else
        if (name == null) {
            alert("Please select khasra no")
        } else
            if (sort == null) {
                alert("Please select valid sort by option");
                $('#ddlSort option:selected').val('Village');
            }

            else {
                $("#btnAscending").removeClass("active");
                $("#btnDescending").addClass("active");
                GetDetails(currentPageNumber, currentPageSize, sortby);
            }
});

$("#btnReset").click(function () {

    var sort = $('#ddlSort option:selected').val();
    var vil = $('#NewLandvillageId option:selected').val();
    var name = $('#Name option:selected').val();

    if (sort == null) {
        alert("Please select valid sort by option");
        $('#ddlSort option:selected').val('Village');
    }
    else
        if (vil == null) {
            // alert("Please select village")
            $('#NewLandvillageId').val('0').trigger('change');
            $('#Name').val('0').trigger('change');
            GetDetails(currentPageNumber, currentPageSize, sortby);

        } else
            if (name == null) {
                $('#NewLandvillageId').val('0').trigger('change');
                $('#Name').val('0').trigger('change');
                GetDetails(currentPageNumber, currentPageSize, sortby);
                //alert("Please select khasra no")
            }

            else {
                $('#NewLandvillageId').val('0').trigger('change');
                $('#Name').val('0').trigger('change');
                GetDetails(currentPageNumber, currentPageSize, sortby);
            }
});


$('#ddlSort').change(function () {


    var sort = $('#ddlSort option:selected').val();
    var vil = $('#NewLandvillageId option:selected').val();
    var name = $('#Name option:selected').val();
    if (vil == null) {
        alert("Please select village")

    } else
        if (name == null) {
            alert("Please select khasra no")
        } else
            if (sort == null) {
                alert("Please select valid sort by option");
                $('#ddlSort option:selected').val('Village');
            }

            else {
                GetDetails(currentPageNumber, currentPageSize, sortby);
            }

});

function onPaging(pageNo) {
    var sort = $('#ddlSort option:selected').val();
    var vil = $('#NewLandvillageId option:selected').val();
    var name = $('#Name option:selected').val();
    if (vil == null) {
        alert("Please select village")

    } else
        if (name == null) {
            alert("Please select khasra no")
        } else
            if (sort == null) {
                alert("Please select valid sort by option");
                $('#ddlSort option:selected').val('Village');
            }

            else {
                GetDetails(parseInt(pageNo), parseInt(currentPageSize), sortby);
                currentPageNumber = pageNo;
            }

}

function onChangePageSize(pageSize) {
    var sort = $('#ddlSort option:selected').val();
    var vil = $('#NewLandvillageId option:selected').val();
    var name = $('#Name option:selected').val();
    if (vil == null) {
        alert("Please select village")

    } else
        if (name == null) {
            alert("Please select khasra no")
        } else
            if (sort == null) {
                alert("Please select valid sort by option");
                $('#ddlSort option:selected').val('Village');
            }

            else {
                GetDetails(parseInt(currentPageNumber), parseInt(pageSize), sortby);
                currentPageSize = pageSize;
            }

}


function onChange(id) {

    HttpGet(`/newlandvillagekhasrareport/GetAllKhasraList/?NewLandvillageId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }

        $("#Name").val('0').trigger('change');
        $("#Name").html(html);
    });
};