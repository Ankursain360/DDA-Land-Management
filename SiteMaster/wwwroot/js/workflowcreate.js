// Repeator code 
$(document).ready(function () {
    $("#tbl_posts #tbl_posts_body .odd").remove();
    $("#tbl_posts #add .form-control").attr("multiple", false);
})

$(document).delegate('a.add-record', 'click', function (e) {
    var name = $("#tbl_posts #add #parameterName").val();
    var value = $("#tbl_posts #add #parameterValue").val();
    var level = $("#tbl_posts #add #parameterLevel").val();
    var isskip = $("#tbl_posts #add #parameterLevel").val();
    debugger
    if ($("#tbl_posts #add #parameterName").val() != '' && $("#tbl_posts #add #parameterValue").val() != '' && $("#tbl_posts #add #parameterLevel").val() != ''
        //&& $("#tbl_posts #add #drpPersonalGender").val() != '' && $("#tbl_posts #add #txtPersonalAddress").val() != ''
        //&& $("#tbl_posts #add #txtPersonalMobileNo").val() != '' && $("#tbl_posts #add #txtPersonalEmailid").val() != ''
    ) {
        //var GenderValue = $("#tbl_posts #add #drpPersonalGender").children("option:selected").val();
        //var father = $("#tbl_posts #add #txtPersonalFatherName").children("option:selected").val();
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
            $("#tbl_posts #add #parameterName").removeAttr("name")
            $("#tbl_posts #add #parameterName").attr("name", "ParameterNameList[" + i + "]");
            $("#tbl_posts #add #parameterValue").removeAttr("name")
            $("#tbl_posts #add #parameterValue").attr("name", "ParameterValueList[" + i + "]");
            $("#tbl_posts #add #parameterLevel").removeAttr("name")
            $("#tbl_posts #add #parameterLevel").attr("name", "ParameterLevelList[" + i + "]");
            $("#tbl_posts #add #paramenterSkip").removeAttr("name")
            $("#tbl_posts #add #paramenterSkip").attr("name", "ParameterSkipList[" + i + "]");

        };
        //$('#tbl_posts_body #rec-' + size + ' #drpPersonalGender').val(GenderValue);
        //$('#tbl_posts_body #rec-' + size + ' #txtPersonalFatherName').val(father);
        //$('#tbl_posts_body #rec-' + size + ' #drpPersonalGender').val(gender);
        element.find('.sn').html('Task ' + size);
        $("#tbl_posts #add .sn").text('Task ' + $('#tbl_posts >tbody >tr').length);
        $("#tbl_posts #add .add").remove();
        $("#tbl_posts #tbl_posts_body .form-control").attr("readonly", true);
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
            $("#tbl_posts #add #parameterName").removeAttr("name")
            $("#tbl_posts #add #parameterName").attr("name", "ParameterNameList[" + i + "]");
            $("#tbl_posts #add #parameterValue").removeAttr("name")
            $("#tbl_posts #add #parameterValue").attr("name", "ParameterValueList[" + i + "]");
            $("#tbl_posts #add #parameterLevel").removeAttr("name")
            $("#tbl_posts #add #parameterLevel").attr("name", "ParameterLevelList[" + i + "]");
            $("#tbl_posts #add #paramenterSkip").removeAttr("name")
            $("#tbl_posts #add #paramenterSkip").attr("name", "ParameterSkipList[" + i + "]");

        }

        //regnerate index number on table
        $('#tbl_posts_body tr').each(function (index) {
            var index = index + 1;
            $(this).find('span.sn').html('Task ' + index);
        });
        $("#tbl_posts #add .sn").text('Task ' + $('#tbl_posts >tbody >tr').length);
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

        var parameterName = $("input[name='ParameterNameList[" + i + "]']").val();
        var parameterValue = $("input[name='ParameterValueList[" + i + "]']").val();
        var parameterLevel = $("input[name='ParameterLevelList[" + i + "]']").val();
        var parameterSkip = $("input[name='ParameterSkipList[" + i + "]']").val();
        if ($("input[name='ParameterSkipList[" + i + "]']").is(":checked")) {
            parameterSkip = true;
        }
        else {
            parameterSkip = false;
        }
        if (parameterName == "" && parameterValue == "" && parameterLevel == "" && parameterSkip == "false") {

        }
        else {
            model = {
                parameterName: parameterName,
                parameterValue: parameterValue,
                parameterLevel: parameterLevel,
                parameterSkip: (parameterSkip)
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
        isActive: isActive,
        template: JSON.stringify(workflow)
    }
    console.log(data);
    //workflow.push(model);
    return data;
}

