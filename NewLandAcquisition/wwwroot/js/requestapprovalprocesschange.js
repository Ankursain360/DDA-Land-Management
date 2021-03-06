


$(document).ready(function () {

    var id = parseInt($('#Id').val());
    GetOtherDetails(id);
    GetHistoryDetails(id);
    GetReqIdHistory(id);
    HttpGet(`/RequestApprovalProcess/GetApprovalDropdownList`, 'html', function (response) {

        response = JSON.parse(response);
        $('#ApprovalStatus option').each(function () {
            if (response.length > 0) {
                for (var i = 0; i < response.length; i++) {
                    if (response[i] == $(this).val()) {
                        $(this).show().trigger('change');
                    }
                    else {
                        $(this).remove().trigger('change');
                    }
                }
            }
        });
    });

});
function setRouteParameter() {
    var newhref = $("#addActivityPrice").attr('href') + '/' + g_id;
    $("#addActivityPrice").attr("href", newhref);
}

function GetReqIdHistory(id) {
    HttpGet(`/RequestApprovalProcess/RequestIdHistory/?Id=${id}`, 'json', function (data) {

      //  debugger
        try {

            if ((data[0].id) != 0) {
                var x = data[0].id;
                
                if (((data[0].ReqId) == 0)  || ((data[0].createdBy) == 0))  {


                    $("viewlink").attr("asp-route-id", x);
                    var newhref = $("#viewlink").attr('href') + '/' + x;
                    $("#viewlink").attr("href", newhref);
                    document.getElementById("viewlink").text = "Click me to open annexure 2 view link!";

                   
                }
                
            else
                {
                    $("editlink").attr("asp-route-id", x);
                    var newhref = $("#editlink").attr('href') + '/' + x;
                    $("#editlink").attr("href", newhref);

                    document.getElementById("editlink").text = "Click me to open annexure 2 edit link!";
                }
            }
            else
            {
                document.getElementById("createlink").text = "Click me to open annexure 2 create link!";
              
            }
        } catch
        { document.getElementById("createlink").text = "Click me to open annexure 2 create link!"; }

        // $("#tbl_posts #add #EName").val(Data[0].Id);
        // $("#tbl_posts #add #EAddress").val(data[i].address);

        //$('#RequestIdHistoryDiv').val("");
        //$('#RequestIdHistoryDiv').val(response);
    });
};

function GetOtherDetails(id) {
    HttpGet(`/RequestApprovalProcess/RequestView/?Id=${id}`, 'html', function (response) {
        $('#RequestDiv').html("");

        $('#RequestDiv').html(response);
    });
};

function GetHistoryDetails(id) {
    HttpGet(`/RequestApprovalProcess/HistoryDetails/?Id=${id}`, 'html', function (response) {
        $('#divHistoryDetails').html("");
        $('#divHistoryDetails').html(response);
    });
};

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$("#collapse").click(function () {
    debugger;
    $('#collapseApprroval').collapse("toggle").promise().done(function () {
        $("select").select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});

$("#collapse").click(function () {
    $("#collapseHistoryApprroval").collapse("toggle").promise().done(function () {
        $('#select').select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});

$('#myForm').validate({
    rules: {
        ApprovalStatusId: {
            required: true
        },
        ApprovalRemarks: {
            required: true
        }
    },

    messages: {
        ApprovalStatusId: {
            required: ApprovalStatusIdMessage //this is a function that returns custom messages
        },
        ApprovalRemarks: {
            required: ApprovalRemarksMessage //this is a function that returns custom messages
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
        // alert('Form validated and submitted ok.');
        return true;
    }
});

function ApprovalRemarksMessage() {
    var dropdown_val = $('#ApprovalRemarks').val();
    if (dropdown_val == "") {
        return "Approval Remarks is Mandatory";
    } else {
        return "";
    }
};

function ApprovalStatusIdMessage() {
    var dropdown_val = $('#ApprovalStatus').val();
    if (dropdown_val == "") {
        return "Approval Status is Mandatory";
    } else {
        return "";
    }
};
