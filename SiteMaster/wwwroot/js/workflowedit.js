
$(document).ready(function () {

    GetTaskDetails();
});

function GetTaskDetails() {

    var id = $('#Id').val();
    HttpGetAsync(`/WorkFlowTemplate/GetTaskDetails/?id=${id}`, 'json', function (response) {
        debugger;
        response = response //JSON.parse(response);
        var count = response.length;

        for (var i = 0; i < response.length; i++) {
            var param = GetSearchParam(response[i].parameterValue);
            HttpPostAsync(`/WorkFlowTemplate/GetDetails`, 'html', param, function (response) {
                $('#LoadReportView').append(response);
            });
        }
        $("input[type='hidden'][name='ParameterSkipList[0]']").remove();
        FillLevels(response);
    });
}

function FillLevels(response) {
    var count = response.length;
    var k = 0;
    $(".ParameterConditionalListClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterConditionalList[" + k + "]");
        k = k + 1;
    });

    var i = 0;
    $(".ParameterNameListClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterNameList[" + i + "]");
        i = i + 1;
    });

    var a = 0;
    $(".ParameterValueListClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterValueList[" + a + "]");
        $(this).removeAttr("onchange");
        $(this).attr("onchange", "callTypeDropdown(" + a + ")");
        a = a + 1;
    });

    var i = 0;
    $(".nameClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterLabelNameList[" + i + "]");
        i = i + 1;
    });

    var b = 0;
    $(".ParameterLevelListClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterLevelList[" + b + "]");
        $(this).val(b + 1);
        $(this).removeAttr("disabled", "disabled");
        $(this).attr("disabled", "disabled");
        b = b + 1;
    });

    var t = 0;
    $(".ParameterOrderListClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterOrderList[" + t + "]");
        $(this).val(t + 1);
        $(this).removeAttr("disabled", "disabled");
        $(this).attr("disabled", "disabled");
        t = t + 1;
    });

    var c = 0;
    $(".ParameterActionListClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterActionList[" + c + "]");
        c = c + 1;
    });
    $(".ParameterActionListClass").attr("multiple", "");

    var d = 0;
    $(".ParameterSkipListClass").each(function () {
        $(this).removeAttr("name").attr("name", "ParameterSkipList[" + d + "]");
        d = d + 1;
    });

    var f = 1;
    $(".sn").each(function () {
        $(this).html('Level ' + f);
        f = f + 1;
    });

    var i = 1;
    $(".delete-record").each(function () {
        $(this).removeAttr("data-id");
        $(this).attr("data-id", i);
        i = i + 1;
    });

    var i = 1;
    $(".recordDiv").each(function () {
        $(this).removeAttr("data-id");
        $(this).attr("id", "recordDiv" + i);
        i = i + 1;
    });

    for (var j = 0; j < response.length; j++) {
        debugger;

        if (response[j].parameterSkip == true) {
            $("input[name='ParameterSkipList[" + j + "]']").prop("checked", true);
            $("input[name='ParameterSkipList[" + j + "]']").val("true");
        }
        else {
            $("input[name='ParameterSkipList[" + j + "]']").prop("checked", false);
            $("input[name='ParameterSkipList[" + j + "]']").val("false");
        }

        $("Select[name='ParameterConditionalList[" + j + "]']").val(response[j].parameterConditional);
        $("Select[name='ParameterValueList[" + j + "]']").val(response[j].parameterValue);
        $("Select[name='ParameterValueList[" + j + "]']").trigger('change');
        $("Select[name='ParameterNameList[" + j + "]']").val(response[j].parameterName);
        $("input[name='ParameterLevelList[" + j + "]']").val(response[j].parameterLevel);
        $("input[name='ParameterOrderList[" + j + "]']").val(response[j].parameterOrder);
        $("Select[name='ParameterActionList[" + j + "]']").val(response[j].parameterAction);


    }

}

function GetLevelDetails() {


    if ($("#tbl_posts #add #parameterName").val() != '' && $("#tbl_posts #add #parameterValue").val() != '' && $("#tbl_posts #add #parameterLevel").val() != ''
        && $("#tbl_posts #add #dropdownlist").val() != '0' && $("#tbl_posts #add #ddlActionType").val() != '0'
    ) {

        var param = GetSearchParam();
        HttpPostAsync(`/WorkFlowTemplate/GetDetails`, 'html', param, function (response) {
            $('#LoadReportView').append(response);

            var k = 0;
            $(".ParameterConditionalListClass").each(function () {
                $(this).removeAttr("name");
                $(this).attr("name", "ParameterConditionalList[" + k + "]");
                k = k + 1;
            });

            var i = 0;
            $(".ParameterNameListClass").each(function () {
                $(this).removeAttr("name");
                $(this).attr("name", "ParameterNameList[" + i + "]");
                i = i + 1;
            });

            var i = 0;
            $(".ParameterValueListClass").each(function () {
                $(this).removeAttr("name");
                $(this).attr("name", "ParameterValueList[" + i + "]");
                $(this).removeAttr("onchange");
                $(this).attr("onchange", "callTypeDropdown(" + i + ")");
                i = i + 1;
            });

            var i = 0;
            $(".nameClass").each(function () {
                $(this).removeAttr("name");
                $(this).attr("name", "ParameterLabelNameList[" + i + "]");
                i = i + 1;
            });

            var i = 0;
            $(".ParameterLevelListClass").each(function () {
                $(this).removeAttr("name");
                $(this).attr("name", "ParameterLevelList[" + i + "]");
                $(this).val(i + 1);
                $(this).removeAttr("disabled", "disabled");
                $(this).attr("disabled", "disabled");
                i = i + 1;
            });

            var t = 0;
            $(".ParameterOrderListClass").each(function () {
                $(this).removeAttr("name");
                $(this).attr("name", "ParameterOrderList[" + t + "]");
                $(this).val(t + 1);
                $(this).removeAttr("disabled", "disabled");
                $(this).attr("disabled", "disabled");
                t = t + 1;
            });

            var i = 0;
            $(".ParameterActionListClass").each(function () {
                $(this).removeAttr("name");
                $(this).attr("name", "ParameterActionList[" + i + "]");
                i = i + 1;
            });

            var i = 0;
            $(".ParameterSkipListClass").each(function () {
                $(this).removeAttr("name");
                $(this).attr("name", "ParameterSkipList[" + i + "]");
                i = i + 1;
            });

            var i = 1;
            $(".sn").each(function () {
                $(this).html('Level ' + i);
                i = i + 1;
            });

            var i = 1;
            $(".delete-record").each(function () {
                $(this).removeAttr("data-id");
                $(this).attr("data-id", i);
                i = i + 1;
            });

            var i = 1;
            $(".recordDiv").each(function () {
                $(this).removeAttr("data-id");
                $(this).attr("id", "recordDiv" + i);
                i = i + 1;
            });

        });
    }
    else {
        alert('Please fill record before add new record ');
    }
    $("input[type='hidden'][name='ParameterSkipList[0]']").remove();
}

function GetSearchParam() {
    debugger;
    var count = $('.myWebsiteTable').find('table').length;
    var value = $('#ddlOperationType option:selected').val();
    var model = {
        size: count,
        opertaionId: value
    }
    return model;
}


$(document).delegate('a.add-record', 'click', function (e) {
    GetLevelDetails();
});

$(document).delegate('a.delete-record', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        debugger;
        var id = jQuery(this).attr('data-id');
        //   var targetDiv = jQuery(this).attr('targetDiv');
        $('#recordDiv' + id).remove();
      //  $("input[data-id='recordDiv1" + id + "'").remove();
        // jQuery("#recordDiv" + id).remove();
        //  $('#recordDiv' + id).empty();

        debugger;
        var k = 0;
        $(".ParameterConditionalListClass").each(function () {
            $(this).removeAttr("name");
            $(this).attr("name", "ParameterConditionalList[" + k + "]");
            k = k + 1;
        });
        var i = 0;
        $(".ParameterNameListClass").each(function () {
            $(this).removeAttr("name");
            $(this).attr("name", "ParameterNameList[" + i + "]");
            i = i + 1;
        });
        var i = 0;
        $(".ParameterValueListClass").each(function () {
            $(this).removeAttr("name");
            $(this).attr("name", "ParameterValueList[" + i + "]");
            $(this).removeAttr("onchange");
            $(this).attr("onchange", "callTypeDropdown(" + i + ")");
            i = i + 1;
        });

        var i = 0;
        $(".nameClass").each(function () {
            $(this).removeAttr("name");
            $(this).attr("name", "ParameterLabelNameList[" + i + "]");
            i = i + 1;
        });

        var i = 0;
        $(".ParameterLevelListClass").each(function () {
            $(this).removeAttr("name");
            $(this).attr("name", "ParameterLevelList[" + i + "]");
            $(this).val(i + 1);
            $(this).removeAttr("disabled", "disabled");
            $(this).attr("disabled", "disabled");
            i = i + 1;
        });

        var t = 0;
        $(".ParameterOrderListClass").each(function () {
            $(this).removeAttr("name");
            $(this).attr("name", "ParameterOrderList[" + t + "]");
            $(this).val(t + 1);
            $(this).removeAttr("disabled", "disabled");
            $(this).attr("disabled", "disabled");
            t = t + 1;
        });

        var i = 0;
        $(".ParameterActionListClass").each(function () {
            $(this).removeAttr("name");
            $(this).attr("name", "ParameterActionList[" + i + "]");
            i = i + 1;
        });

        var i = 0;
        $(".ParameterSkipListClass").each(function () {
            $(this).removeAttr("name");
            $(this).attr("name", "ParameterSkipList[" + i + "]");
            i = i + 1;
        });

        var i = 1;
        $(".sn").each(function () {
            $(this).html('Level ' + i);
            i = i + 1;
        });

        var i = 1;
        $(".delete-record").each(function () {
            $(this).removeAttr("data-id");
            $(this).attr("data-id", i);
            i = i + 1;
        });

        var i = 1;
        $(".recordDiv").each(function () {
            $(this).removeAttr("id");
            $(this).attr("id", "recordDiv" + i);
            i = i + 1;
        });
        return true;
    } else {
        return false;
    }
});


$("#btnCreate").click(function () {
    debugger;
    var checkresult = false;
    var dropdown_val = $('#ModuleId option:selected').val();
    if (parseInt(dropdown_val) < 1) {
        checkresult = false;
        $("#ModuleIdMessage").show();
    } else {
        checkresult = true;
    }

    var Name_val = $('#Name').val();
    if (Name_val == "") {
        checkresult = false;
        $("#NameMessage").show();
    } else {
        checkresult = true;
    }

    var Description_val = $('#Description').val();
    if (Description_val == "") {
        checkresult = false;
        $("#DescriptionMessage").show();
    } else {
        checkresult = true;
    }

    var SlaTime_val = $('#Slatime').val();
    if (SlaTime_val == "") {
        checkresult = false;
        $("#SlatimeMessage").show();
    } else {
        checkresult = true;
        $("#SlatimeMessage").hide();
    }

    var EffectiveDate_val = $('#EffectiveDate').val();
    if (EffectiveDate_val == "") {
        checkresult = false;
        $("#EffectiveDateMessage").show();
    } else {
        checkresult = true;
        $("#EffectiveDateMessage").hide();
    }

    if (parseInt(dropdown_val) < 1 || Name_val == "" || Description_val == "" || SlaTime_val == "" || EffectiveDate_val == "") {
        checkresult = false;
        return WarningMessage('Please Fill all Mandatory Fields');
    }


    var remove_ApprovedItem = $('#ApprovedCode').val();
    var remove_ForwardItem = $('#ForwardCode').val();
    var count = $('.myWebsiteTable').find('table').length;
    for (var i = 0; i < count; i++) {
        var parameterSkip = false;
        if ($("input[name='ParameterSkipList[" + i + "]']").is(":checked")) {
            parameterSkip = true;
        }
        else {
            parameterSkip = false;
        }
        var parameterActionArrayCode = [];
        var parameterActionArrayId = [];
        var parameterAction = $("Select[name='ParameterActionList[" + i + "]']").val();
        for (var r = 0; r < parameterAction.length; r++) {
            parameterActionArrayCode.push(parameterAction[r].split('|')[1]);
            parameterActionArrayId.push(parameterAction[r].split('|')[0]);
        }
        if (i < count - 1) {
            if (jQuery.inArray(remove_ForwardItem, parameterActionArrayCode) == -1)
                return InfoMessage('Submit Action is mandatroy at each level apart from last level.');
        }
        if (i == count - 1) {
            if (jQuery.inArray(remove_ApprovedItem, parameterActionArrayCode) == -1)
                return InfoMessage('Approved action is missing at last level. Kindly include Approved Action at Last level.');

            if (parameterSkip == true)
                return InfoMessage('Last level cannot be skipped, Please unselect skip at last level');
        }
    }

    if (checkresult) {
        var param = GetListData();
        if (param) {
            HttpPostAsync(`/WorkFlowTemplate/Edit`, 'json', param, function (response) {
                if (response != null) {
                    if (response[0] == "false") {
                        WarningMessage(response[1]);
                    }
                    else {
                        window.location.href = response[0];
                        SuccessMessage(response[1]);
                    }
                }
                else {
                    WarningMessage('Unable to update records');
                }
            });
        }
        else {
            WarningMessage('Please Fill all details Level wise');
        }
    }
});


function GetListData() {
    var id = $('#Id').val();
    var moduleId = $('#ModuleId option:selected').val();
    var name = $('#Name').val();
    var description = $('#Description').val();
    var slatime = $('#Slatime').val();
    var effectivedate = $('#EffectiveDate').val();
    var usertype = $('#ddlOperationType option:selected').val();

    var remove_RevertItem = $('#RevertCode').val();
    var remove_ApprovedItem = $('#ApprovedCode').val();
    var remove_ForwardItem = $('#ForwardCode').val();

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
    var count = $('.myWebsiteTable').find('table').length;
    for (var i = 0; i < count; i++) {
        var parameterName = [];
        var parameterConditional = $("select[name='ParameterConditionalList[" + i + "]']").val();
        var parameterValue = $("select[name='ParameterValueList[" + i + "]']").val();
        if (parameterValue == "Role")
            parameterName.push($("select[name='ParameterNameList[" + i + "]']").val());
        else
            parameterName = ($("select[name='ParameterNameList[" + i + "]']").val());
        var parameterLevel = $("input[name='ParameterLevelList[" + i + "]']").val();
        var parameterOrder = $("input[name='ParameterOrderList[" + i + "]']").val();
        var parameterSkip = $("input[name='ParameterSkipList[" + i + "]']").val();
        var parameterAction = $("Select[name='ParameterActionList[" + i + "]']").val();


        var parameterActionArrayCode = [];
        var parameterActionArrayId = [];

        for (var r = 0; r < parameterAction.length; r++) {
            parameterActionArrayCode.push(parameterAction[r].split('|')[1]);
            parameterActionArrayId.push(parameterAction[r].split('|')[0]);
        }

        if (parameterValue == "Role" && (parameterName == null || parameterName == "")) {
            return false;
        }
        else if (parameterValue == "User" && parameterName.length == 0) {
            return false;
        }

        if (i == 0) {
            for (var w = 0; w < parameterActionArrayCode.length; w++) {
                if (parameterActionArrayCode[w] == remove_RevertItem) {
                    parameterActionArrayCode.splice(w, 1);
                    parameterActionArrayId.splice(w, 1);
                }
            }
            //  InfoMessage('Revert  not allowed at first level');
        }
        if (i < count - 1) {
            for (var q = 0; q < parameterActionArrayCode.length; q++) {
                if (parameterActionArrayCode[q] == remove_ApprovedItem) {
                    parameterActionArrayCode.splice(q, 1);
                    parameterActionArrayId.splice(q, 1);
                }
            }
            //  InfoMessage('Approved  not allowed at any level');
        }
        if (i == count - 1) {
            for (var v = 0; v < parameterActionArrayCode.length; v++) {
                if (parameterActionArrayCode[v] == remove_ForwardItem) {
                    parameterActionArrayCode.splice(v, 1);
                    parameterActionArrayId.splice(v, 1);
                }
            }
            //  InfoMessage('Forward not allowed at Last level');
        }
        parameterAction = parameterActionArrayId;
        if (parameterAction.length == 0) {
            return false;
        }

        if ($("input[name='ParameterSkipList[" + i + "]']").is(":checked")) {
            parameterSkip = true;
        }
        else {
            parameterSkip = false;
        }

        model = {
            parameterConditional: parameterConditional,
            parameterValue: parameterValue,
            parameterName: parameterName,
            parameterLevel: parameterLevel,
            parameterOrder: parameterOrder,
            parameterSkip: (parameterSkip),
            parameterAction: parameterAction
        }
        workflow.push(model);
    };

    if ($.isEmptyObject(workflow)) {

    }
    else {
        data = {
            Id: parseInt(id),
            moduleId: parseInt(moduleId),
            slatime: parseInt(slatime),
            effectivedate: effectivedate,
            name: name,
            description: description,
            usertype: usertype,
            isActive: isActive,
            template: JSON.stringify(workflow)
        }
    }
    return data;
}



$('#myForm').validate({
    rules: {
        ModuleId: {
            required: true
        },
        Name: {
            required: true
        },
        Description: {
            required: true
        }
    },

    messages: {
        ModuleId: {
            required: ModuleIdMessage //this is a function that returns custom messages
        },
        Name: {
            required: NameMessage //this is a function that returns custom messages
        },
        Description: {
            required: DescriptionMessage //this is a function that returns custom messages
        }
    },
    highlight: function (element) {
        $(element).closest('.form-group').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
    },
    errorElement: 'span',
    errorClass: 'help-block',
    errorPlacement: function (error, element) {
        if (element.parent('.input-group').length) {
            error.insertAfter(element.parent());
        } else {
            error.insertAfter(element);
        }
    },
    submitHandler: function (form) {
        debugger;
        var param = GetListData();
        HttpPostAsync(`/WorkFlowTemplate/Create`, 'json', param, function (response) {
            window.location.href = '/WorkFlowTemplate/Index';
        });
        return true;
    }
});

//For Drop down
function ModuleIdMessage() {
    debugger;
    var dropdown_val = $('#ModuleId option:selected').val();
    if (dropdown_val < 1) {
        return "Module is Mandatory";
    } else {
        return false;
    }
}

//For Textbox
function NameMessage() {
    debugger;
    var dropdown_val = $('#Name').val();
    if (dropdown_val == "") {
        return "Process Name is Mandatory";
    } else {
        return "";
    }
}

function DescriptionMessage() {
    var dropdown_val = $('#Description').val();
    if (dropdown_val == "") {
        return "Proccess Description is Mandatory";
    } else {
        return "";
    }
}

function callTypeDropdown(element) {
    debugger;
    var name = element;
    var fragment_arr = name;
    var value = $("select[name='ParameterValueList[" + name + "]']").val();

    HttpGetAsync(`/WorkFlowTemplate/GetUserList/?value=${value}`, 'json', function (response) {
        debugger;
        var html = "";
        if (value == "Role") {
            html = '<option selected="selected" disabled="disabled" value="0">--Select-- </option>';
        }
        for (var i = 0; i < response.length; i++) {
            //if (value == "Role") {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
            //}
            //else {
            //    html = html + '<option value=' + response[i].user.id + '>' + response[i].user.name + '</option>';
            //}
        }

        $("select[name='ParameterNameList[" + fragment_arr + "]']").val(null).trigger('change');
        if (value == "Role") {
            $("select[name='ParameterNameList[" + fragment_arr + "]']").removeAttr("multiple", "multiple");
        }
        else {
            $("select[name='ParameterNameList[" + fragment_arr + "]']").attr("multiple", "");
        }
        $("select[name='ParameterNameList[" + fragment_arr + "]']").html(html);
        $("label[name='ParameterLabelNameList[" + fragment_arr + "]']").html(value + ' Name');

        $("select[name='ParameterNameList[" + fragment_arr + "]']").select2({
        });
    });
    // callSelect2();
}

function callSelect2() {
    $("select").select2({
    });
}