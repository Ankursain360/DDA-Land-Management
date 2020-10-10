// Repeator code 
$(document).ready(function () {
    $("#tbl_posts #tbl_posts_body .odd").remove();
    $("#tbl_posts #add .form-control").attr("multiple", false);

    BindDropdown();
});

$(document).delegate('a.add-record', 'click', function (e) {
    var name = $("#tbl_posts #add #ddlActionType").val();
    var value = $("#tbl_posts #add #parameterValue").val();
    var level = $("#tbl_posts #add #parameterLevel").val();
    var isskip = $("#tbl_posts #add #parameterLevel").val();
    debugger
    if ($("#tbl_posts #add #parameterName").val() != '' && $("#tbl_posts #add #parameterValue").val() != '' && $("#tbl_posts #add #parameterLevel").val() != ''
        && $("#tbl_posts #add #dropdownlist").val() != '0' && $("#tbl_posts #add #ddlActionType").val() != '0'
        //&& $("#tbl_posts #add #txtPersonalMobileNo").val() != '' && $("#tbl_posts #add #txtPersonalEmailid").val() != ''
    ) {
        var actionValue = $("#tbl_posts #add #ddlActionType").children("option:selected").val();
        var dropdownValue = $("#tbl_posts #add #dropdownlist").children("option:selected").val();
        //var gender = $("#tbl_posts #add #drpPersonalGender").children("option:selected").val();
        debugger
        e.preventDefault();
        var content = jQuery('#tbl_posts #add tr'),
            size = jQuery('#tbl_posts >tbody>tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_posts_body');
        debugger;
        for (var i = 0; i < jQuery('#tbl_posts >tbody>tr').length; i++) {
            $("#tbl_posts #add #dropdownlist").removeAttr("name")
            $("#tbl_posts #add #dropdownlist").attr("name", "ParameterNameList[" + i + "]");
            $("#tbl_posts #add #parameterValue").removeAttr("name")
            $("#tbl_posts #add #parameterValue").attr("name", "ParameterValueList[" + i + "]");
            $("#tbl_posts #add #parameterLevel").removeAttr("name")
            $("#tbl_posts #add #parameterLevel").attr("name", "ParameterLevelList[" + i + "]");
            $("#tbl_posts #add #paramenterSkip").removeAttr("name")
            $("#tbl_posts #add #paramenterSkip").attr("name", "ParameterSkipList[" + i + "]");
            $("#tbl_posts #add #ddlActionType").removeAttr("name")
            $("#tbl_posts #add #ddlActionType").attr("name", "ParameterActionList[" + i + "]");

        };
        $('#tbl_posts_body #rec-' + size + ' #ddlActionType').val(actionValue);
        $('#tbl_posts_body #rec-' + size + ' #dropdownlist').val(dropdownValue);
        //$('#tbl_posts_body #rec-' + size + ' #drpPersonalGender').val(gender);
        element.find('.sn').html('Level ' + size);
        $("#tbl_posts #add .sn").text('Level ' + $('#tbl_posts >tbody >tr').length);
        $("#tbl_posts #add .add").remove();
     //   $("#tbl_posts #tbl_posts_body .form-control").attr("readonly", true);
        //element.find(".add-record").hide();
        element.find(".delete-record").show();
        debugger
        $("#tbl_posts #add .form-control").val('');
        $("#tbl_posts #add  .checkbox").prop("checked", false);
    }
    else {
        alert('Please fill record before add new record ');
    }
});
$(document).delegate('a.delete-record', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();

        debugger;
        for (var i = 0; i < jQuery('#tbl_posts >tbody>tr').length; i++) {
            $("#tbl_posts #add #dropdownlist").removeAttr("name")
            $("#tbl_posts #add #dropdownlist").attr("name", "ParameterNameList[" + i + "]");
            $("#tbl_posts #add #parameterValue").removeAttr("name")
            $("#tbl_posts #add #parameterValue").attr("name", "ParameterValueList[" + i + "]");
            $("#tbl_posts #add #parameterLevel").removeAttr("name")
            $("#tbl_posts #add #parameterLevel").attr("name", "ParameterLevelList[" + i + "]");
            $("#tbl_posts #add #paramenterSkip").removeAttr("name")
            $("#tbl_posts #add #paramenterSkip").attr("name", "ParameterSkipList[" + i + "]");
            $("#tbl_posts #add #ddlActionType").removeAttr("name")
            $("#tbl_posts #add #ddlActionType").attr("name", "ParameterActionList[" + i + "]");

        }

        //regnerate index number on table
        $('#tbl_posts_body tr').each(function (index) {
            var index = index + 1;
            $(this).find('span.sn').html('Level ' + index);
        });
        $("#tbl_posts #add .sn").text('Level ' + $('#tbl_posts >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});


$(function () {
    $("#btnCreate").click(function () {
        debugger;
        var param = GetListData();
        HttpPost(`/WorkFlowTemplate/Create`, 'json', param, function (response) {
            window.location.href = '/WorkFlowTemplate/Index';
        });
    });
});

function GetListData() {
    var id = 0;
    var moduleId = $('#ModuleId option:selected').val();
    var name = $('#Name').val();
    var description = $('#Description').val();
    var usertype = $('#ddlOperationType option:selected').val();
    var isActive;
    if ($("#IsActiveData").is(":checked")) {
        isActive = 1;
    }
    else {
        isActive = 0;
    }
    var workflow = [];
    var model = {};
    var data = {};
    debugger;
    for (var i = 0; i < jQuery('#tbl_posts >tbody>tr').length; i++) {
        var parameterName = $("Select[name='ParameterNameList[" + i + "]']").children("option:selected").val();
        var parameterValue = $("input[name='ParameterValueList[" + i + "]']").val();
        var parameterLevel = $("input[name='ParameterLevelList[" + i + "]']").val();
        var parameterSkip = $("input[name='ParameterSkipList[" + i + "]']").val();
        var parameterAction = $("Select[name='ParameterActionList[" + i + "]']").children("option:selected").val();
        if ($("input[name='ParameterSkipList[" + i + "]']").is(":checked")) {
            parameterSkip = true;
        }
        else {
            parameterSkip = false;
        }
        if ((parameterName == "") && (parameterValue == "") && (parameterLevel == "") && (parameterName == "0") && (parameterAction == "0")) {

        }
        else {
            model = {
                parameterName: parameterName,
                parameterValue: parameterValue,
                parameterLevel: parameterLevel,
                parameterSkip: (parameterSkip),
                parameterAction: parameterAction
            }
            workflow.push(model);
        }
        
    };
    console.log(workflow);
    data = {
        Id: parseInt(id),
        moduleId: parseInt(moduleId),
        name: name,
        description: description,
        usertype: usertype,
        isActive: isActive,
        template: JSON.stringify(workflow)
    }
    console.log(data);
    return data;
}

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
