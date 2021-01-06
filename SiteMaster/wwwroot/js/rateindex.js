
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;


$(document).ready(function () {
    GetRate();
});
$("#btnSearch").click(function () {
    GetRate();
});

$("#btnReset").click(function () {
    $('#txtProperty').val('');
    GetRate();
}); 

function GetRate() {
    var param = GetSearchParam();
    HttpPost(`/Rate/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });

   
}

function GetSearchParam() {
    var model = {
        name: "rate",
        property: $('#txtProperty').val()
    };
    return model;
}


// ********** Sorting Code  **********


function GetRateOrderBy(order) {
    var param = GetSearchParamaOrderby(order);
    HttpPost(`/Rate/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
}

function Ascending() {
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');

    if (value !== "0") {
        GetRateOrderBy(currentSortOrderAscending);
    }
    else {
        alert('Please select SortBy Value');
    }
};

function Descending() {
    var value = $("#ddlSort").children("option:selected").val();
   
    if (value !== "0") {
        GetRateOrderBy(currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};



function GetSearchParamaOrderby(sortOrder) {
    var model = {
       
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
       
    }
    return model;
}

