$(document).ready(function () {
    $('#OperationType').change(function () {
        if ($('#OperationType').val() == 'Role') {
            $('#divUser').hide();
        }
        else {
            $('#divUser').show();
            var roleId = $('#Role').val();
            if (roleId!='') {
                $.ajax({
                    type: 'post',
                    url: '/PageRole/GetUserList',
                    data: { RoleId: roleId },
                    dataType: 'json',
                    success: function (data) {
                        var html = '<option value="">Select</option>';
                        for (var i = 0; i < data.length; i++) {
                            html = html + '<option value=' + data[i].id + '>' + data[i].displayName + '</option>';
                        }
                        $("#User").html(html);
                    }
                });
            }
        }
    });
    $('#Role').change(function () {
        var roleId = $('#Role').val();
        $.ajax({
            type: 'post',
            url: '/PageRole/GetUserList',
            data: { RoleId: roleId },
            dataType: 'json',
            success: function (data) {
                var html = '<option value="">Select</option>';
                for (var i = 0; i < data.length; i++) {
                    html = html + '<option value=' + data[i].id + '>' + data[i].displayName + '</option>';
                }
                $("#User").html(html);
            }
        });
    });









    $("#selectall_PageRole").change(function () {
        var status = this.checked;
        $('.checkbox_role').each(function () {
            this.checked = status;
        });
    });
    $('.checkbox_role').change(function () {

        if (this.checked == false) {
            $("#selectall_role")[0].checked = false;
        }
        if ($('.checkbox_role:checked').length == $('.checkbox_role').length) {
            $("#selectall_role")[0].checked = true;
        }
    });
    // for Add writes
    $("#selectall_add").change(function () {
        var status = this.checked;
        $('.checkbox_add').each(function () {
            this.checked = status;
        });
    });
    $('.checkbox_add').change(function () {

        if (this.checked == false) {
            $("#selectall_add")[0].checked = false;
        }
        if ($('.checkbox_add:checked').length == $('.checkbox_add').length) {
            $("#selectall_add")[0].checked = true;
        }
    });
    // for Edit Update writes
    $("#selectall_editUpdate").change(function () {
        var status = this.checked;
        $('.checkbox_editUpdate').each(function () {
            this.checked = status;
        });
    });
    $('.checkbox_editUpdate').change(function () {

        if (this.checked == false) {
            $("#selectall_editUpdate")[0].checked = false;
        }
        if ($('.checkbox_editUpdate:checked').length == $('.checkbox_editUpdate').length) {
            $("#selectall_editUpdate")[0].checked = true;
        }
    });
    // for Delete writes
    $("#selectall_delete").change(function () {
        var status = this.checked;
        $('.checkbox_delete').each(function () {
            this.checked = status;
        });
    });
    $('.checkbox_delete').change(function () {

        if (this.checked == false) {
            $("#selectall_delete")[0].checked = false;
        }
        if ($('.checkbox_delete:checked').length == $('.checkbox_delete').length) {
            $("#selectall_delete")[0].checked = true;
        }
    });
    // for View writes
    $("#selectall_view").change(function () {
        var status = this.checked;
        $('.checkbox_view').each(function () {
            this.checked = status;
        });
    });
    $('.checkbox_view').change(function () {
        if (this.checked == false) {
            $("#selectall_view")[0].checked = false;
        }
        if ($('.checkbox_view:checked').length == $('.checkbox_view').length) {
            $("#selectall_view")[0].checked = true;
        }
    });
});