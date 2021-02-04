function HttpGet(url, dataType, callback) {
    DisplayLoader(true);
    $.ajax({
        cache: false,
        type: 'GET',
        async: false,
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

function DisplayLoader(show) {
    var styleValue = show === true ? "block" : "none";
    $(".body-loading").css("display", styleValue);
}
