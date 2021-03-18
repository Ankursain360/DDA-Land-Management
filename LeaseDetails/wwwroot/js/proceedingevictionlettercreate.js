$(document).ready(function () {

    GetDetails();

});

function GetDetails() {
    var param = GetSearchParam();
    HttpPost(`/ProceedingEvictionLetter/ViewLetter`, 'html', param, function (response) {
        $('#LetterData').html("");
        $('#LetterData').html(response);
    });
}

function GetSearchParam() {
    var model = {
        name: "test"
    }
    return model;
}

function GetLetterRefNo(id) {
    debugger;
    HttpGet(`/ProceedingEvictionLetter/GetLetterRefNo/?Id=${id}`, 'json', function (response) {
        $("#ZoneId").val('').trigger('change');
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").val('').trigger('change');
        $("#LocalityId").val('').trigger('change');
    });
};