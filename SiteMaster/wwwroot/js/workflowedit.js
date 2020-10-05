$(document).ready(function () {

    GetTaskDetails();

});

function GetTaskDetails() {
    debugger
    $("#tbl_posts #tbl_posts_body").html('');
    var roleId = $('#Role').val();
    var operationType = $('#ddlOperationType').val();
    var moduleId = $('#ddlModuleId').val();
    var userId = $('#ddlUser').val();
    HttpGet(`/PageRole/GetTaskDetails`, 'json', function (response) {
        debugger
        for (var i = 0; i < response.length; i++) {
            if (response[i].rDisplay == '1') {
                $("#tbl_posts #add #ChkDisplay").prop("checked", true);
                $("#tbl_posts #add #hdnRDisplay").val("1");
            }
            else {
                $("#tbl_posts #add #ChkDisplay").prop("checked", false);
                $("#tbl_posts #add #hdnRDisplay").val("0");
            }
            if (response[i].rAdd == '1') {
                $("#tbl_posts #add #ChkAdd").prop("checked", true);
                $("#tbl_posts #add #hdnRAdd").val("1");
            }
            else {
                $("#tbl_posts #add #ChkAdd").prop("checked", false);
                $("#tbl_posts #add #hdnRAdd").val("0");
            }
            if (response[i].rEdit == '1') {
                $("#tbl_posts #add #ChkEdit").prop("checked", true);
                $("#tbl_posts #add #hdnREdit").val("1");
            }
            else {
                $("#tbl_posts #add #ChkEdit").prop("checked", false);
                $("#tbl_posts #add #hdnREdit").val("0");
            }
            if (response[i].rView == '1') {
                $("#tbl_posts #add #ChkView").prop("checked", true);
                $("#tbl_posts #add #hdnRView").val("1");
            }
            else {
                $("#tbl_posts #add #ChkView").prop("checked", false);
                $("#tbl_posts #add #hdnRView").val("0");
            }
            if (response[i].rDelete == '1') {
                $("#tbl_posts #add #ChkDelete").prop("checked", true);
                $("#tbl_posts #add #hdnRDelete").val("1");
            }
            else {
                $("#tbl_posts #add #ChkDelete").prop("checked", false);
                $("#tbl_posts #add #hdnRDelete").val("0");
            }
            $("#tbl_posts #add #PageId").val(response[i].pageId);

            $("#tbl_posts #add #ModuleName").text(response[i].moduleName);
            $("#tbl_posts #add #PageName").text(response[i].pageName);

            $("#tbl_posts #add #PageId").removeAttr("name")
            $("#tbl_posts #add #PageId").attr("name", "PageIdList[" + i + "]");
            $("#tbl_posts #add #hdnRDisplay").removeAttr("name")
            $("#tbl_posts #add #hdnRDisplay").attr("name", "RDisplayList[" + i + "]");
            $("#tbl_posts #add #hdnRAdd").removeAttr("name")
            $("#tbl_posts #add #hdnRAdd").attr("name", "RAddList[" + i + "]");
            $("#tbl_posts #add #hdnREdit").removeAttr("name")
            $("#tbl_posts #add #hdnREdit").attr("name", "REditList[" + i + "]");
            $("#tbl_posts #add #hdnRView").removeAttr("name")
            $("#tbl_posts #add #hdnRView").attr("name", "RViewList[" + i + "]");
            $("#tbl_posts #add #hdnRDelete").removeAttr("name")
            $("#tbl_posts #add #hdnRDelete").attr("name", "RDeleteList[" + i + "]");
            if (i < response.length - 1) {
                var ChkDisplay = $("#tbl_posts #add #ChkDisplay").val();
                var ChkDelete = $("#tbl_posts #add #ChkDelete").val();
                var ChkView = $("#tbl_posts #add #ChkView").val();
                var ChkEdit = $("#tbl_posts #add #ChkEdit").val();
                var ChkAdd = $("#tbl_posts #add #ChkAdd").val();
                var ValueDisplay = $("#tbl_posts #add #hdnRDisplay").val();
                var ValueDelete = $("#tbl_posts #add #hdnRDelete").val();
                var ValueView = $("#tbl_posts #add #hdnRView").val();
                var ValueEdit = $("#tbl_posts #add #hdnREdit").val();
                var ValueAdd = $("#tbl_posts #add #hdnRAdd").val();

                var content = jQuery('#tbl_posts #add tr'),
                    size = jQuery('#tbl_posts >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.appendTo('#tbl_posts_body');
                //$('#tbl_posts_body #rec-' + size + ' #ChkDisplay').val(ChkDisplay);
                //$('#tbl_posts_body #rec-' + size + ' #ChkDelete').val(ChkDelete);
                //$('#tbl_posts_body #rec-' + size + ' #ChkView').val(ChkView);
                //$('#tbl_posts_body #rec-' + size + ' #ChkEdit').val(ChkEdit);
                //$('#tbl_posts_body #rec-' + size + ' #ChkAdd').val(ChkAdd);

                element.find('.sn').html(size);
                $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
                $("#tbl_posts #tbl_posts_body .form-control").attr("readonly", true);
            }
        }
    });
});
}