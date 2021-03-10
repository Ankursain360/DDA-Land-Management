$('#myForm').validate({
    rules: {

        Name: {
            required: true
        }
    },

    messages: {
        Name: {
            required: NameMessage //this is a function that returns custom messages
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
        return "Classification of Land Name is Mandatory";
    } else {
        return "";
    }
}