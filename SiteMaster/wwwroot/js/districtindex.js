var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
  
    GetDistrict(currentPageNumber, currentPageSize, sortOrder);
});

$("#btnSearch").click(function () {
    GetDistrict(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetDistrict(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetDistrict(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnReset").click(function () {
    $('#txtName').val('');
    $('#txtCode').val('')
    GetDistrict(currentPageNumber, currentPageSize, sortOrder);
   
});


function GetDistrict(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/district/List`, 'html', param, function (response) {
        $('#divDistrict').html("");
        $('#divDistrict').html(response);
    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
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

function onPaging(pageNo) {
    GetDistrict(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDistrict(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
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