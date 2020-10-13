$(document).ready(function () {
   
    
    GetLevelDetails();

    BindDropdown();
});
$('#ddlOperationType').change(function () {
    BindDropdown();
});
function BindDropdown() {
    var value = $('#ddlOperationType option:selected').val();
    HttpGet(`/WorkFlowTemplate/GetUserList/?value=${value}`, 'json', function (response) {
        var html = '<option value="0">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        for (var i = 0; i < jQuery('#tbl_posts >tbody>tr').length; i++) {
            $("#dropdownlist").html(html);
        }

    });
}

function GetLevelDetails() {
    var param = GetSearchParam();
    HttpPost(`/WorkFlowTemplate/GetDetails`, 'html', param, function (response) {
        //   $('#LoadReportView').html("");
        $('#LoadReportView').append(response);
    });
}

function GetSearchParam() {
    var model = {
        name: "test"
    }
    return model;
}
