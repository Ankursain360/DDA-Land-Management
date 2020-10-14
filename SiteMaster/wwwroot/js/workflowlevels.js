//$(document).ready(function () {

//    BindDropdown();
//});

//function BindDropdown() {
//    debugger;
//    var value = $('#OperationId').val();
//    HttpGet(`/WorkFlowTemplate/GetUserList/?value=${value}`, 'json', function (response) {
//        var html = '<option value="0">---Select---</option>';
//        for (var i = 0; i < response.length; i++) {
//            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
//        }
//        for (var i = 0; i < jQuery('#tbl_posts >tbody>tr').length; i++) {
//            $(".ParameterNameListClass").html(html);
//        }

//    });
//}