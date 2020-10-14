$(document).ready(function () {


    var param = GetSearchParam();
    HttpPost(`/WorkFlowTemplate/GetDetails`, 'html', param, function (response) {
        //   $('#LoadReportView').html("");
        $('#LoadReportView').append(response);
    });

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
            $(".ParameterNameListClass").html(html);
        }

    });
}

function GetLevelDetails() {
    var name = $("#tbl_posts #add #ddlActionType").val();
    var value = $("#tbl_posts #add #parameterValue").val();
    var level = $("#tbl_posts #add #parameterLevel").val();
    var isskip = $("#tbl_posts #add #parameterLevel").val();
    //var numbers = [];
    //$(".ParameterNameListClass").each(function () {
    //    numbers.push($(this).children('option:selected').val());
    //});
    if ($("#tbl_posts #add #parameterName").val() != '' && $("#tbl_posts #add #parameterValue").val() != '' && $("#tbl_posts #add #parameterLevel").val() != ''
        && $("#tbl_posts #add #dropdownlistDesrtoy").val() != '0' && $("#tbl_posts #add #ddlActionType").val() != '0'
    ) {

        var param = GetSearchParam();
        HttpPost(`/WorkFlowTemplate/GetDetails`, 'html', param, function (response) {
            //   $('#LoadReportView').html("");
            $('#LoadReportView').append(response);

            var i = 0;
            // var collection = $(".ParameterNameListClass");
            $(".ParameterNameListClass").each(function () {
                // You can access `collection.length` here.
                // var size = collection.length;            
                // console.log(size);
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
            $(".ParameterLevelListClass").each(function () {
                $(this).removeAttr("name");
                $(this).attr("name", "ParameterLevelList[" + i + "]");
                $(this).val(i+1);
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
            $(".delete-recordDiv").each(function () {
                $(this).removeAttr("data-id");
                $(this).attr("id", "delete -recordDiv"+i);
                i = i + 1;
            });

        });
      
    }
    else {
        alert('Please fill record before add new record ');
    }
    //debugger;
    //var x = 0;
    //$(".ParameterNameListClass").each(function () {
    //    $(this).val(numbers[x]);
    //    x = x + 1;
    //});
}

function GetSearchParam() {
    //var particularDiv = document.getElementById('particularDiv');
    //var allTables = particularDiv.getElementsByTagName('table').length;
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
        jQuery('#delete -recordDiv' + id).remove();
      //  $('#delete -recordDiv' + id).empty();

        debugger;
        var i = 0;
        // var collection = $(".ParameterNameListClass");
        $(".ParameterNameListClass").each(function () {
            // You can access `collection.length` here.
            // var size = collection.length;            
            // console.log(size);
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
        $(".delete-recordDiv").each(function () {
            $(this).removeAttr("data-id");
            $(this).attr("data-id", "delete -recordDiv" + i);
            i = i + 1;
        });
        return true;
    } else {
        return false;
    }
});

$(function () {
    $("#btnCreate").click(function () {
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
            HttpPost(`/WorkFlowTemplate/Create`, 'json', param, function (response) {
                window.location.href = '/WorkFlowTemplate/Index';
            });
        }
        
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
    var count = $('.myWebsiteTable').find('table').length;
    for (var i = 0; i < count; i++) {
        var parameterName = $("select[name='ParameterNameList[" + i + "]']").val();
        var parameterValue = $("input[name='ParameterValueList[" + i + "]']").val();
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
    var dropdown_val = $('#ModuleId option:selected').val();
    if (parseInt(dropdown_val) < 1) {
        return "Module is Mandatory";
    } else {
        return false;
    }
} 

//For Textbox
function NameMessage() {
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