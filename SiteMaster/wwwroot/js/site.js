// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".ParameterActionListClass").attr("multiple", "");
    $("select").select2({
        placeholder: "--Select--",
        allowClear: true
    });  
    $('.max-length').each(function () {
        $(this).MaxLength({ MaxLength: parseInt($(this).attr("size")) });
    });
    var forbiddenWords = ['iframe', 'Script', 'Video', 'Audio', 'Alert', '<', '>', '/>'];
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
});
$('.numbers').keyup(function () {
    this.value = this.value.replace(/[^0-9\.]/g, '');
});
$('.onlynumbers').keyup(function () {
    var $th = $(this);
    $th.val($th.val().replace(/[^0-9]/g, ''));
});
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
    $(".spinner").css("display", styleValue);
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
eval(
    (function (p, a, c, k, e, d) {
        e = function (c) {
            return c;
        };
        if (!"".replace(/^/, String)) {
            while (c--) {
                d[c] = k[c] || c;
            }
            k = [
                function (e) {
                    return d[e];
                },
            ];
            e = function () {
                return "\\w+";
            };
            c = 1;
        }
        while (c--) {
            if (k[c]) {
                p = p.replace(new RegExp("\\b" + e(c) + "\\b", "g"), k[c]);
            }
        }
        return p;
    })(
        '(6($){$.58.5=6(2){3 31={5:10,13:33,9:30};3 2=$.54(31,2);20 14.42(6(){3 4=$(14);12(2.13){12(2.9==30||2.9.11==0){3 32=\'<27 41 = "66:\'+4[0].49+\'48;47-50:51"></27>\';4.53(32)}}4.7(4.7().28(0,2.5));18(14,2.5,2.9,2.13);4.25("46",6(19){3 23=19.23?19.23:19.44;3 29=36 35(8,37,38,39,40);55(3 21=0;21<29.11;21++){12(23==29[21])20 33}20 18(14,2.5,2.9,2.13)});4.25("67",6(19){18(14,2.5,2.9,2.13)});4.25(\'62 57 56\',6(){3 24=$(14);59(6(){24.7(24.7().28(0,2.5));18(24,2.5,2.9,2.13)},63)})})}})(68);6 18(16,22,15,34){3 11=$(16).7().11;3 17=22-11;12(17<0){$(16).7($(16).7().28(0,22));17=0}12(34){12(15==30||15.11==0){15=$(16).52()}3 26=15[0].45.65();12(26=="61"||26=="27"){15.64(17+" 69"+(17>1?"60":"")+" 43.")}}20 11<=22-1}',
        10,
        70,
        "||options|var|textBox|MaxLength|function|val||CharacterCountControl||length|if|DisplayCharacterCount|this|control|t|characters|SetCharacterCount|e|return|i|maxLength|keyCode|textarea|bind|tagName|div|substring|codes|null|defaults|counter|true|isVisible|Array|new|||||style|each|left|which|nodeName|keypress|text|px|offsetWidth|align|right|next|after|extend|for|blur|drop|fn|setTimeout|s|span|paste|100|html|toLowerCase|width|keyup|jQuery|character".split(
            "|"
        ),
        0,
        {}
    )
);