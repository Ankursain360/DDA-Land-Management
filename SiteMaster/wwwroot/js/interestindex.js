
$(document).ready(function () {
    GetInterest();
});
$("#btnSearch").click(function () {
    GetInterest();
});

$("#btnReset").click(function () {
    $('#txtProperty').val('');
    GetInterest();
});

function GetInterest() {
    var param = GetSearchParam();
    HttpPost(`/Interest/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });


}

function GetSearchParam() {
    var model = {
        name: "interest",
        property: $('#txtProperty').val()
    };
    return model;
}

