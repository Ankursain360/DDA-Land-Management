$(document).ready(function () {

    GetTaskDetails();

});

function GetTaskDetails() {

    var id = $('#Id').val();
    HttpGetAsync(`/WorkFlowTemplate/GetTaskDetails/?id=${id}`, 'json', function (response) {
        debugger;
        response = JSON.parse(response);
        console.log(response);
        var count = response.length;

        for (var i = 0; i < response.length; i++) {
            var param = GetSearchParam();
            HttpPostAsync(`/WorkFlowTemplate/GetDetails`, 'html', param, function (response) {
                $('#LoadReportView').append(response);
            });
        }

        FillLevels(response);

    });
}

function FillLevels(response) {
    var count = response.length;
    var k = 0;
    $(".ParameterConditionalListClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterConditionalList[" + k + "]");
        $(this).attr("readonly", "readonly");
        k = k + 1;
    });


    var i = 0;
    $(".ParameterNameListClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterNameList[" + i + "]");
        $(this).attr("readonly", "readonly");
        i = i + 1;
    });

    var a = 0;
    $(".ParameterValueListClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterValueList[" + a + "]");
        $(this).removeAttr("onchange");
        $(this).attr("onchange", "callTypeDropdown(" + a + ")");
        $(this).attr("readonly", "readonly");
        a = a + 1;
    });

    var i = 0;
    $(".nameClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterLabelNameList[" + i + "]");
        $(this).attr("readonly", "readonly");
        i = i + 1;
    });

    var b = 0;
    $(".ParameterLevelListClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterLevelList[" + b + "]");
        $(this).val(b + 1);
        $(this).attr("readonly", "readonly");
        b = b + 1;
    });

    var c = 0;
    $(".ParameterActionListClass").each(function () {
        $(this).removeAttr("name");
        $(this).attr("name", "ParameterActionList[" + c + "]");
        $(this).attr("readonly", "readonly");
        c = c + 1;
    });
    $(".ParameterActionListClass").attr("multiple", "");

    var d = 0;
    $(".ParameterSkipListClass").each(function () {
        $(this).removeAttr("name").attr("name", "ParameterSkipList[" + d + "]");
        $(this).attr("readonly", "readonly");
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