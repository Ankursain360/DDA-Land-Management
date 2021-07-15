
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
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: dataType,
        url: url
    }).done(function (response) {       
        callback(response);
        DisplayLoader(false);
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
        async: true,
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

 
