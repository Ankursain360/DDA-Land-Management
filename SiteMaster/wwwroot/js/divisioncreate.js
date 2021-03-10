$('#myForm').validate({
    rules: {

        DepartmentId: {
            required: true
        },
        Name: {
            required: true
        },
        Code: {
            required: true
        }
    },

    messages: {
        DepartmentId: {
            required: DepartmentIdMessage //this is a function that returns custom messages
        },
        Name: {
            required: NameMessage //this is a function that returns custom messages
        },
        Code: {
            required: CodeMessage //this is a function that returns custom messages
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

function NameMessage() {
    var dropdown_val = $('#Name').val();
    if (dropdown_val == "") {
        return "Zone Name is Mandatory";
    } else {
        return "";
    }
}

function DepartmentIdMessage() {
    var dropdown_val = $('#DepartmentId option:selected').val();
    if (dropdown_val == "") {
        return "Department is Mandatory";
    } else {
        return "";
    }
}

function CodeMessage() {
    var dropdown_val = $('#Code').val();
    if (dropdown_val == "") {
        return "Zone Code is Mandatory";
    } else {
        return "";
    }
}