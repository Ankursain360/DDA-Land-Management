$(document).ready(function () {
    GetRate();
});
$("#btnSearch").click(function () {
    GetRate();
});

$("#btnReset").click(function () {
    $('#txtProperty').val('');
    GetRate();
}); 

function GetRate() {
    var param = GetSearchParam();
    HttpPost(`/Rate/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });

   
}

function GetSearchParam() {
    var model = {
        name: "rate",
        property: $('#txtProperty').val()
    };
    return model;
}

