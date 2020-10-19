var currentPageNumber = 1;
var currentPageSize = 10;

$(document).ready(function () {
    GetDistrict(currentPageNumber, currentPageSize);
});

function GetDistrict(pageNumber, pageSize) {
    var param = GetSearchParam(pageNumber, pageSize);
    HttpPost(`/district/List`, 'html', param, function (response) {
        $('#divDistrict').html("");
        $('#divDistrict').html(response);
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