$(document).ready(function () {
    $('#ddlOperationType').change(function () {
        debugger
        if ($(this).children("option:selected").val() == 'Role') {
            $('#divUser').hide();
        }
        else {
            $('#divUser').show();
            var roleId = $('#Role').val();
            if (roleId != '') {
                HttpGet(`/PageRole/GetUserList/?roleId=${roleId}`, 'json', function (response) {
                    var html = '<option value="">Select</option>';
                    for (var i = 0; i < response.length; i++) {
                        html = html + '<option value=' + response[i].id + '>' + response[i].displayName + '</option>';
                    }
                    $("#User").html(html);
                });
            }
        }
    });
    $('#Role').change(function () {
        var roleId = $('#Role').val();
        HttpGet(`/PageRole/GetUserList/?roleId=${roleId}`, 'json', function (response) {
            var html = '<option value="">Select</option>';
            for (var i = 0; i < response.length; i++) {
                html = html + '<option value=' + response[i].id + '>' + response[i].displayName + '</option>';
            }
            $("#ddlUser").html(html);
        });
    });
    $('#GetPageRoleRecord').click(function () {
        debugger
        $("#tbl_posts #tbl_posts_body").html('');
        var roleId = $('#Role').val();
        var operationType = $('#ddlOperationType').val();
        var moduleId = $('#ddlModuleId').val();
        var userId = $('#ddlUser').val();
        HttpGet(`/PageRole/GetPageRoleDetails/?roleId=${roleId}&operationType=${operationType}&moduleId=${moduleId}&userId=${userId}`, 'json', function (response) {
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
                if (i < response.length-1) {
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
                  
                    element.find('.sn').html(size);
                    $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
                    $("#tbl_posts #tbl_posts_body .form-control").attr("readonly", true);
                }
            }
        });
    });

});
$("#selectall_PageRole").change(function () {
    var status = this.checked;
    $('.checkbox_role').each(function () {
        this.checked = status;
    });
    if (status == true) {
        $(".isDisplay").val('1');
    } else {
        $(".isDisplay").val('0');
    }
});
$(document).on('change', "input.checkbox_role", function () {
    debugger
    if (this.checked == false) {
        $("#selectall_PageRole").prop("checked", false);
        $("#" + $(this).closest('table').attr('id') + " #" + $(this).closest('tbody').attr('id') + " #" + $(this).closest('tr').attr('id') + " #hdnRDisplay").val('0');
    }
    else {
        $("#" + $(this).closest('table').attr('id') + " #" + $(this).closest('tbody').attr('id') + " #" + $(this).closest('tr').attr('id') + " #hdnRDisplay").val('1');
    }
    if ($('.checkbox_role:checked').length == $('.checkbox_role').length) {
        $("#selectall_PageRole").prop("checked", true);
    }
});
// for Add writes
$("#selectall_add").change(function () {
    var status = this.checked;
    $('.checkbox_add').each(function () {
        this.checked = status;
    });
    if (status == true) {
        $(".isAdd").val('1');
    } else {
        $(".isAdd").val('0');
    }
});
$(document).on('change', "input.checkbox_add", function () {
    if (this.checked == false) {
        $("#selectall_add").prop("checked", false);
        $("#" + $(this).closest('table').attr('id') + " #" + $(this).closest('tbody').attr('id') + " #" + $(this).closest('tr').attr('id') + " #hdnRAdd").val('0');
    }
    else {
        $("#" + $(this).closest('table').attr('id') + " #" + $(this).closest('tbody').attr('id') + " #" + $(this).closest('tr').attr('id') + " #hdnRAdd").val('1');
    }
    if ($('.checkbox_add:checked').length == $('.checkbox_add').length) {
        $("#selectall_add").prop("checked", true);
    }
});
// for Edit Update writes
$("#selectall_editUpdate").change(function () {
    var status = this.checked;
    $('.checkbox_editUpdate').each(function () {
        this.checked = status;
    });
    if (status == true) {
        $(".isEdit").val('1');
    } else {
        $(".isEdit").val('0');
    }
});
$(document).on('change', "input.checkbox_editUpdate", function () {
    if (this.checked == false) {
        $("#selectall_editUpdate").prop("checked", false);
        $("#" + $(this).closest('table').attr('id') + " #" + $(this).closest('tbody').attr('id') + " #" + $(this).closest('tr').attr('id') + " #hdnREdit").val('0');
    }
    else {
        $("#" + $(this).closest('table').attr('id') + " #" + $(this).closest('tbody').attr('id') + " #" + $(this).closest('tr').attr('id') + " #hdnREdit").val('1');
    }
    if ($('.checkbox_editUpdate:checked').length == $('.checkbox_editUpdate').length) {
        $("#selectall_editUpdate").prop("checked", true);
    }
});
// for Delete writes
$("#selectall_delete").change(function () {
    var status = this.checked;
    $('.checkbox_delete').each(function () {
        this.checked = status;
    });
    if (status == true) {
        $(".isDelete").val('1');
    } else {
        $(".isDelete").val('0');
    }
});
$(document).on('change', "input.checkbox_delete", function () {
    if (this.checked == false) {
        $("#selectall_delete").prop("checked", false);
        $("#" + $(this).closest('table').attr('id') + " #" + $(this).closest('tbody').attr('id') + " #" + $(this).closest('tr').attr('id') + " #hdnRDelete").val('0');
    }
    else {
        $("#" + $(this).closest('table').attr('id') + " #" + $(this).closest('tbody').attr('id') + " #" + $(this).closest('tr').attr('id') + " #hdnRDelete").val('1');
    }
    if ($('.checkbox_delete:checked').length == $('.checkbox_delete').length) {
        $("#selectall_delete").prop("checked", false);
    }
});
// for View writes
$("#selectall_view").change(function () {
    var status = this.checked;
    $('.checkbox_view').each(function () {
        this.checked = status;
    });
    if (status == true) {
        $(".isView").val('1');
    } else {
        $(".isView").val('0');
    }
});
$(document).on('change', "input.checkbox_view", function () {
    if (this.checked == false) {
        $("#selectall_view").prop("checked", false);
        $("#" + $(this).closest('table').attr('id') + " #" + $(this).closest('tbody').attr('id') + " #" + $(this).closest('tr').attr('id') + " #hdnRView").val('0');
    }
    else {
        $("#" + $(this).closest('table').attr('id') + " #" + $(this).closest('tbody').attr('id') + " #" + $(this).closest('tr').attr('id') + " #hdnRView").val('1');
    }
    if ($('.checkbox_view:checked').length == $('.checkbox_view').length) {
        $("#selectall_view").prop("checked", false);
    }
});