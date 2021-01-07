
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;


$(document).ready(function () {
    GetInterest();
});
$("#btnSearch").click(function () {
    GetInterest();
});

$("#btnReset").click(function () {
    $('#txtProperty').val('');
    GetInterest();
});

function GetInterest() {
    var param = GetSearchParam();
    HttpPost(`/Interest/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });


}

function GetSearchParam() {
    var model = {
        name: "interest",
        property: $('#txtProperty').val()
    };
    return model;
}




// ********** Sorting Code  **********


function GetInterestOrderBy(order) {
    var param = GetSearchParamaOrderby(order);
    HttpPost(`/Interest/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });

}

function Ascending() {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
   
    if (value !== "0") {
        GetInterestOrderBy(currentSortOrderAscending);
    }
    else {
        alert('Please select SortBy Value');
    }
};

function Descending() {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();

    if (value !== "0") {
        GetInterestOrderBy(currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};



function GetSearchParamaOrderby(sortOrder) {
    var model = {
        name: "interest",
        property: $('#txtProperty').val(),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),

    }
    return model;
}


