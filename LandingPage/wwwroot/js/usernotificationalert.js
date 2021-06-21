//$(document).ready(function () {

//});

function UpdateStatus(id, url) {
    debugger;
    var param = GetListData(id, url);
    HttpPost(`/UserNotification/UpdateUserNotification`, 'json', param, function (response) {

        if (response[0] == "false") {
            WarningMessage(response[1]);
        }
        else {
            window.location.href = response[1];
        }
    });
}

function GetListData(id, url) {
    var model = {
        Id: parseInt(id),
        url: url
    }
    return model;
}
