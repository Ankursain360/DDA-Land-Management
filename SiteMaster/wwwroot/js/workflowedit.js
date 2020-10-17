$(document).ready(function () {

    GetTaskDetails();

});

function GetTaskDetails() {

    var id = $('#Id').val();
    HttpGet(`/WorkFlowTemplate/GetTaskDetails/?id=${id}`, 'json', function (response) {
        debugger;
        response = JSON.parse(response);
        console.log(response);
        var count = response.length;

        for (var i = 0; i < response.length; i++) {
            var param = GetSearchParam();
            HttpPost(`/WorkFlowTemplate/GetDetails`, 'html', param, function (response) {
                $('#LoadReportView').append(response);
            });
        }
        $("input[type='hidden'][name='ParameterSkipList[0]']").remove();
        FillLevels(response);

    });
}

function FillLevels(response) {
    var count = response.length;

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


        if (response[j].parameterSkip == true) {
            $("input[name='ParameterSkipList[" + j + "]']").prop("checked", true);
            $("input[name='ParameterSkipList[" + j + "]']").val("true");
        }
        else {
            $("input[name='ParameterSkipList[" + j + "]']").prop("checked", false);
            $("input[name='ParameterSkipList[" + j + "]']").val("false");
        }

        $("Select[name='ParameterNameList[" + j + "]']").val(response[j].parameterName);
        $("input[name='ParameterValueList[" + j + "]']").val(response[j].parameterValue);
        $("input[name='ParameterLevelList[" + j + "]']").val(response[j].parameterLevel);
        $("Select[name='ParameterActionList[" + j + "]']").val(response[j].parameterAction);


    }

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
            $(".ParameterNameListClass").html(html);
        }

    });
}

function GetLevelDetails() {


    if ($("#tbl_posts #add #parameterName").val() != '' && $("#tbl_posts #add #parameterValue").val() != '' && $("#tbl_posts #add #parameterLevel").val() != ''
        && $("#tbl_posts #add #dropdownlist").val() != '0' && $("#tbl_posts #add #ddlActionType").val() != '0'
    ) {

        var param = GetSearchParam();
        HttpPost(`/WorkFlowTemplate/GetDetails`, 'html', param, function (response) {
            $('#LoadReportView').append(response);

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

}

function GetSearchParam() {
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
        // jQuery("#recordDiv" + id).remove();
        //  $('#recordDiv' + id).empty();

        debugger;
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
            $(this).attr("data-id", "recordDiv" + i);
            i = i + 1;
        });
        return true;
    } else {
        return false;
    }
});

$(function () {
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

        if (parseInt(dropdown_val) < 1 || Name_val == "" || Description_val == "") {
            checkresult = false;
        }


        if (checkresult) {
            var param = GetListData();
            HttpPost(`/WorkFlowTemplate/Edit`, 'json', param, function (response) {
                window.location.href = '/WorkFlowTemplate/Index';
            });
        }
    });
});

function GetListData() {
    var id = $('#Id').val();
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
    var count = $('.myWebsiteTable').find('table').length;
    for (var i = 0; i < count; i++) {
        var parameterName = $("select[name='ParameterNameList[" + i + "]']").val();
        var parameterValue = $("select[name='ParameterValueList[" + i + "]']").val();
        var parameterLevel = $("input[name='ParameterLevelList[" + i + "]']").val();
        var parameterSkip = $("input[name='ParameterSkipList[" + i + "]']").val();
        var parameterAction = $("Select[name='ParameterActionList[" + i + "]']").val();

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
                parameterValue: parameterValue,
                parameterName: parameterName,
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
        HttpPost(`/WorkFlowTemplate/Create`, 'json', param, function (response) {
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
    var name = element.name;
    console.log(name);
    var fragment_arr = name.split("[").slice(-2)[1];
    console.log(fragment_arr);
    var fragment_arr = fragment_arr.split("]").slice(-2)[0];
    console.log(fragment_arr)
    var value = $("select[name='" + name + "']").val();
    console.log(value);

    HttpGet(`/WorkFlowTemplate/GetUserList/?value=${value}`, 'json', function (response) {
        var html = '<option value="0">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("select[name='ParameterNameList[" + fragment_arr + "]']").html(html);
        $("label[name='ParameterLabelNameList[" + fragment_arr + "]']").html(value + ' Name');
    });
}