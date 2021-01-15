var currentPageNumber = 1;
var currentPageSize = 5;
var currentSortOrderAscending = 1;
var currentSortOrderDescending = 2;


$(document).ready(function () {
    GetDistrict(currentPageNumber, currentPageSize);
});
$("#btnSearch").click(function () {
    GetDistrict(currentPageNumber, currentPageSize);
});
function Descending() {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');
    $('#txtCode').val('')
   
    if (value !== "0") {
        GetDistrictOrderby(currentPageNumber, currentPageSize, currentSortOrderDescending);
    }
    else {
        alert('Please select SortBy Value');
    }
};
function Ascending() {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    var value = $("#ddlSort").children("option:selected").val();
    $('#txtName').val('');
    $('#txtCode').val('')
   
    if (value !== "0") {
        
        GetDistrictOrderby(currentPageNumber, currentPageSize, currentSortOrderAscending);
    }
    else {
        alert('Please select SortBy Value');
    }
};

$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtCode').val('')
   
    GetDistrict(currentPageNumber, currentPageSize);
});

function GetDistrictOrderby(pageNumber, pageSize, order) {
    var param = GetSearchParamaOrderby(pageNumber, pageSize, order);
    HttpPost(`/district/List`, 'html', param, function (response) {
        $('#divDistrict').html("");
        $('#divDistrict').html(response);
    });
}

function GetDistrict(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/district/List`, 'html', param, function (response) {
        $('#divDistrict').html("");
        $('#divDistrict').html(response);
    });
}
function GetSearchParamaOrderby(pageNumber, pageSize, sortOrder) {
    var model = {
        name: $('#txtName').val(),
        code: $('#txtCode').val(),
       
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function GetSearchParam(pageNumber, pageSize) {
    var model = {
        name: $('#txtName').val(),
        code: $('#txtCode').val(),

        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

function onPaging(pageNo) {
    GetDistrict(parseInt(pageNo), parseInt(currentPageSize));
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDistrict(parseInt(currentPageNumber), parseInt(pageSize));
    currentPageSize = pageSize;
}
$('#myForm').validate({
    rules: {
     
        Name: {
            required: true
        }
    },

    messages: {
       
        Name: {
            required: DepartmentMessage //this is a function that returns custom messages
        }
    },
    highlight: function (element) {
        $(element).closest('.form-group').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
    },
    errorElement: 'span',
    errorClass: 'help-block',
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    },
    submitHandler: function (form) {
        // alert('Form validated and submitted ok.');
        return true;
    }
});
function DepartmentMessage() {
    var dropdown_val = $('#Name').val();
    if (dropdown_val == "") {
        return "Department Name is Mandatory Field";
    } else {
        return "";
    }
}