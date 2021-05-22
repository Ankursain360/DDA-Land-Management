var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
  
    $('.numbers').keypress(function (event) {
        if ((event.which != 45 || $(this).val().indexOf('-') != -1) &&
            (event.which != 46 || $(this).val().indexOf('.') != -1) &&
            ((event.which < 48 || event.which > 57) &&
                (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }

        var text = $(this).val();

        if ((text.indexOf('.') != -1) &&
            (text.substring(text.indexOf('.')).length > 3) &&
            (event.which != 0 && event.which != 8) &&
            ($(this)[0].selectionStart >= text.length - 3)) {
            event.preventDefault();
        }
    });
});

function GetDateRangeDetails(id) {
    debugger;
    HttpGet(`/DamageRateList/GetDateRangeList/?Id=${id}`, 'json', function (response) {
        $("#DateRangeId").val('').trigger('change');
        var html = '<option value="">---Select---</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DateRangeId").html(html);
    });
}

$("#btnSearch").click(function () {
    debugger;
    var checkresult = false;
    var PropertyTypeId = $('#PropertyTypeId option:selected').val();
    if (PropertyTypeId < 1) {
        $("#PropertyTypeIdMsg").show();
    } else {
        $("#PropertyTypeIdMsg").hide();
        checkresult = true;
    }
    var DateRangeId = $('#DateRangeId option:selected').val();
    if (DateRangeId < 1) {
        $("#DateRangeIdMsg").show();
    } else {
        $("#DateRangeIdMsg").hide();
        checkresult = true;
    }
    var LocalityId = $('#LocalityId option:selected').val();
    if (LocalityId < 1) {
        $("#LocalityIdMsg").show();
    } else {
        $("#LocalityIdMsg").hide();
        checkresult = true;
    }

    if (LocalityId < 1 || DateRangeId < 1 || PropertyTypeId < 1) {

        checkresult = false;
    }
    else {
        checkresult = true;
    }

    if (checkresult) {
        GetDetails(currentPageNumber, currentPageSize, sortOrder);
    }
    else {
        return WarningMessage('Please Fill all Mandatory Fields');
    }
});

function GetDetails(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    HttpPost(`/DamageRateList/List`, 'html', param, function (response) {
        $('#divTable').html("");
        $('#divTable').html(response);
    });
    $("#SaveNewRateDiv").show();
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        propertyid: parseInt($('#PropertyId option:selected').val()),
        daterangeid: parseInt($('#DateRangeId option:selected').val()),
        localityid: parseInt($('#LocalityId option:selected').val()),
        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}


$("#btnSaveNewRate").click(function () {
    debugger;
    var checkresult = false;
    var PropertyTypeId = $('#PropertyTypeId option:selected').val();
    if (PropertyTypeId < 1) {
        $("#PropertyTypeIdMsg").show();
    } else {
        $("#PropertyTypeIdMsg").hide();
        checkresult = true;
    }
    var DateRangeId = $('#DateRangeId option:selected').val();
    if (DateRangeId < 1) {
        $("#DateRangeIdMsg").show();
    } else {
        $("#DateRangeIdMsg").hide();
        checkresult = true;
    }
    var LocalityId = $('#LocalityId option:selected').val();
    if (LocalityId < 1) {
        $("#LocalityIdMsg").show();
    } else {
        $("#LocalityIdMsg").hide();
        checkresult = true;
    }
    var StartDate = $('#StartDate').val();
    if (StartDate == "") {
        $("#StartDateMsg").show();
    } else {
        $("#StartDateMsg").hide();
        checkresult = true;
    }
    var EndDate = $('#EndDate').val();
    if (EndDate == "") {
        checkresult = false;
        $("#EndDateMsg").show();
    } else {
        $("#EndDateMsg").hide();
        checkresult = true;
    }
    var Rate = $('#Rate').val();
    if (Rate == "") {
        checkresult = false;
        $("#RateMsg").show();
    } else {
        $("#RateMsg").hide();
        checkresult = true;
    }

    if (LocalityId < 1 || DateRangeId < 1 || PropertyTypeId < 1 || StartDate == "" || EndDate == "" || Rate == "") {

        checkresult = false;
    }
    else {
        checkresult = true;
    }

    if (checkresult) {
        SaveRateDetails();
    }
    else {
        return WarningMessage('Please Fill all Mandatory Fields');
    }
});

function SaveRateDetails() {
    var param = GetRateDetailsSearchParam();
    HttpPost(`/DamageRateList/Create`, 'json', param, function (response) {
        if (response != null) {
            if (response[0] == "false") {
                WarningMessage(response[1]);
            }
            else {
                Clear();
                GetDetails(currentPageNumber, currentPageSize, sortOrder);
                SuccessMessage(response[1]);
            }
        }
        else {
            WarningMessage('Unable to submit records');
        }
    });
}

function GetRateDetailsSearchParam() {
    var model = {
        propertyid: parseInt($('#PropertyId option:selected').val()),
        daterangeid: parseInt($('#DateRangeId option:selected').val()),
        localityid: parseInt($('#LocalityId option:selected').val()),
        StartDate: $('#StartDate').val(),
        EndDate: $('#EndDate').val(),
        Rate: parseFloat($('#Rate').val()),
        Id: parseInt($('#Id').val())

    }
    return model;
}

$("#btnReset").click(function () {
    Clear();
});

function Clear() {
    // $('#ddlModuleId').val(0).trigger('change');
    $('#StartDate').val('');
    $('#EndDate').val('');
    $('#Rate').val('');
    $('#Id').val('0');
    $("#btnSaveNewRate").val("Save New Rate");
}


function EditDetails(id, encroachmenttypeid, localityid, propertytypeid) {
    
    HttpGet("/DamageRateList/EditDetails?Id=" + id + "&EncroachmentTypeId=" + encroachmenttypeid + "&LocalityId=" + localityid + "&PropertypeId=" + propertytypeid, 'json', function (response) {
        if (response != null) {
            $("#btnSaveNewRate").val("Update");
            $("#Id").val(response.id);
            $("#StartDate").val(response.startDate.split('T')[0]);
            $("#EndDate").val(response.endDate.split('T')[0]);
            $("#Rate").val(response.rate);
        }
        else {
            $("#btnSaveNewRate").val("Save New Rate");
            $("#Id").val("0");
            $("#StartDate").val("");
            $("#EndDate").val("");
            $("#Rate").val("");
        }
    });
}

