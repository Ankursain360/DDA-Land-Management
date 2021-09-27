// Write your JavaScript code.
$(document).ready(function () {

    $('#mobileMenu').on('click', function () {
        $('#leftMenu').toggleClass('small');
        $('#mobileMenu').toggleClass('open');
        $('#rightSection').toggleClass('menu-close');
        $('footer').toggleClass('menu-close');
    });


    $("select").each(function () {
        if ($(this).hasClass("select2destroy") == false) {
            $(this).select2({
                //placeholder: "Select",
                //allowClear: true
            });
        }
    });
    function callSelect2() {
        $("select").each(function () {
            if ($(this).hasClass("select2destroy") == false) {
                $(this).select2({
                    //placeholder: "Select",
                    //allowClear: true
                });
            }
        });
    }

    $('.numbers').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    });
    $('.onlynumbers').keyup(function () {
        var $th = $(this);
        $th.val($th.val().replace(/[^0-9]/g, ''));
    });


    $(".ParameterActionListClass").attr("multiple", "");
    //$("select[id!='ddlPageSize']").select2({
    //    //placeholder: "--Select--",
    //    //allowClear: true
    //});
    if ($("form").is("[disabled]")) {
        if ($("form").attr('disabled') == 'disabled') {
            $('input').attr('disabled', 'disabled');
            $('select').attr('disabled', 'disabled');
            $('button').attr('disabled', 'disabled');
            $('textarea').attr('disabled', 'disabled');
        }
    }
    $('input,textarea').each(function () {
        if ($(this).is("[limit]")) {
            if ($(this).attr("limit") != undefined && $(this).attr("limit") != null) {
                $(this).MaxLength({ MaxLength: parseInt($(this).attr("limit")) });
            }
        }
    });
    var forbiddenWords = ['iframe', 'Video', 'Audio', 'Alert', '<', '>', '/>'];
    var Bool = "False";
    $('input,textarea').on('keyup', function (e) {
        forbiddenWords.forEach(function (val, index) {
            if (e.target.value.toUpperCase().indexOf(val.toUpperCase()) >= 0) {
                e.target.value = e.target.value.replace(new RegExp("(" + val + ")", 'gi'), '');
                Bool = "True";
            }
        }); if (Bool == "True") {
            Bool = "False";
            alert("Special character not allowed");
            return false;
        };
    });
    //$('input,textarea,select').on('keyup keypress change', function () {
    //    ValidateForm();
    //});
});

 


function numericDecimal(elementRef) {
    var keyCodeEntered = (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;
    if ((keyCodeEntered >= 48) && (keyCodeEntered <= 57)) {
        return true;
    }
    // '.' decimal point... 
    else if (keyCodeEntered == 46) {
        // Allow only 1 decimal point ('.')...  
        if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))
            return false;
        else
            return true;
    }
    return false;
}

//For Loader
$(window).on('load', function () {
    $("#loader-wrapper").css("display", "none");
});
$(window).on('beforeunload', function () {
    $("#loader-wrapper").css("display", "block");
});

//

function SuccessMessage(message) {
    toastr.success(message, 'Success', { timeOut: 3000, "progressBar": true });
}

function ErrorMessage(message) {
    toastr.error(message, 'Error', { timeOut: 3000, "progressBar": true });
}

function WarningMessage(message) {
    toastr.warning(message, 'Warning', { timeOut: 3000, "progressBar": true });
}

function InfoMessage(message) {
    toastr.info(message, 'Warning', { timeOut: 3000, "progressBar": true });
}

function DisplayLoader(show) {
    var styleValue = show === true ? "block" : "none";
    $("#loader-wrapper").css("display", styleValue);
}

function DisplayErrorMessages(response) {
    var messageList = "";
    if (response.status === 401) {
        ErrorMessage('401 :: Un-authorized Request.');
    } else if (response.status === 400) {
        if (response.responseJSON === undefined) {
            jsonObj = JSON.parse(response.responseText);
            for (i = 0; i <= jsonObj.error.length - 1; i++) {
                messageList += `<li>${jsonObj.error[i]}</li>`;
            }
        }
        else {
            for (i = 0; i <= response.responseJSON.error.length - 1; i++) {
                messageList += `<li>${response.responseJSON.error[i]}</li>`;
            }
        }

        var formattedMessage = `<ul>${messageList}</ul>`
        ErrorMessage(formattedMessage);
    } else {
        ErrorMessage(response.statusText);
    }
}

function HttpGet(url, dataType, callback) {
    DisplayLoader(true);
    $.ajax({
        cache: false,
        type: 'GET',
        // async: false,
        contentType: "application/json; charset=utf-8",
        dataType: dataType,
        url: url
    }).done(function (response) {
        DisplayLoader(false);
        callback(response);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        DisplayLoader(false);
        DisplayErrorMessages(jqXHR);
    });
}

function HttpPost(url, dataType, payload, callback) {
    DisplayLoader(true);
    $.ajax({
        cache: false,
        type: 'POST',
        async: false,
        data: JSON.stringify(payload),
        contentType: "application/json; charset=utf-8",
        dataType: dataType,
        url: url
    }).done(function (response) {
        DisplayLoader(false);
        callback(response);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        DisplayLoader(false);
        DisplayErrorMessages(jqXHR);
    });
}

function HttpPut(url, dataType, payload, callback) {
    DisplayLoader(true);
    $.ajax({
        cache: false,
        type: 'PUT',
        data: JSON.stringify(payload),
        contentType: "application/json; charset=utf-8",
        dataType: dataType,
        url: url
    }).done(function (response) {
        DisplayLoader(false);
        callback(response);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        DisplayLoader(false);
        DisplayErrorMessages(jqXHR);
    });
}

function HttpDelete(url, dataType, callback) {
    DisplayLoader(true);
    $.ajax({
        cache: false,
        type: 'DELETE',
        contentType: "application/json; charset=utf-8",
        dataType: dataType,
        url: url
    }).done(function (response) {
        DisplayLoader(false);
        callback(response);
    }).fail(function (jqXHR, textStatus, errorThrown) {
        DisplayLoader(false);
        DisplayErrorMessages(jqXHR);
    });
}
function preventBack() { window.history.forward(); }
setTimeout("preventBack()", 0);
window.onunload = function () { null };