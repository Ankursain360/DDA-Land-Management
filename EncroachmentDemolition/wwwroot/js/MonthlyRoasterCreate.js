$(document).ready(function () {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
    $("#Month").change(function () {
        var months = $("#Month option:selected").val();
        var years = $("#Year option:selected").val();
        var DepartmentId = $("#DepartmentId option:selected").val();
        var ZoneId = $("#ZoneId option:selected").val();
        var DivisionId = $("#DivisionId option:selected").val();
        var Locality = $("#Locality option:selected").val();

        $('#dialog').load("/MonthlyRoster/GetMonthlyDetails", { month: months, year: years, department: DepartmentId, zone: ZoneId, division: DivisionId, Locality: Locality });
    });
});

function onChangeDepartment(id) {

    HttpGet(`/MonthlyRoster/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {

        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").html('<option value="">Select</option>');
        $("#Locality").html('<option value="">Select</option>');
        $("#ZoneId").select2('val', '');
        $("#DivisionId").select2('val', '');
        $("#Locality").select2('val', '');
    });
};
function onChangeZone(id) {
    HttpGet(`/MonthlyRoster/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#Locality").html('<option value="">Select</option>');
        $("#DivisionId").select2('val', '');
        $("#Locality").select2('val', '');

    });
};
function onChangeDivision(id) {
    HttpGet(`/MonthlyRoster/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#Locality").html(html);
        $("#Locality").select2('val', '');
    });
};
function onSubmitForm() {
    var checkresult = true;
    var months = $("#Month option:selected").val();
    var years = $("#Year option:selected").val();
    var DepartmentId = $("#DepartmentId option:selected").val();
    var ZoneId = $("#ZoneId option:selected").val();
    var DivisionId = $("#DivisionId option:selected").val();
    var Locality = $("#Locality option:selected").val();
    var SecurityGuard = $("#SecurityGuard option:selected").val();
    if (months == "") {
        checkresult = false;
        $("#MonthMessage").show();
    } else if (checkresult) {
        $("#MonthMessage").hide();
    } if (SecurityGuard == "") {
        checkresult = false;
        $("#SecurityGuardMessage").show();
    } else if (checkresult) {
        $("#SecurityGuardMessage").hide();
    }

    if (years == "") {
        checkresult = false;
        $("#YearMessage").show();
    } else if (checkresult) {
        $("#YearMessage").hide();
    }

    if (DepartmentId == "") {
        checkresult = false;
        $("#DepartmentMessage").show();
    } else if (checkresult) {
        $("#DepartmentMessage").hide();
    }
    if (ZoneId == "") {
        checkresult = false;
        $("#ZoneMessage").show();
    } else if (checkresult) {
        $("#ZoneMessage").hide();
    }
    if (DivisionId == "") {
        checkresult = false;
        $("#DivisionMessage").show();
    } else if (checkresult) {
        $("#DivisionMessage").hide();
    }
    if (Locality == "") {
        checkresult = false;
        $("#LocalityMessage").show();
    } else if (checkresult) {
        $("#LocalityMessage").hide();
    }
    //var condition = 0;

    //$('table#RoasterTable tr').each(function () {
    //    var ddlValue = $(this).find('td').map(function () {
    //        return $(this).find('select').map(function () {
    //            if ($(this).prop("tagName") == "SELECT") {
    //                return JSON.stringify($(this).select2('val'));
    //            } else {
    //                return $(this).val();
    //            }
    //        }).get();
    //    }).get();
    //    if (condition > 0) {
    //        if (ddlValue == "[]") {
    //            checkresult = false;
    //            $(this).find("#PrimaryNoMessage").show();
    //        } else if (checkresult) {
    //            $(this).find("#PrimaryNoMessage").hide();
    //        }
    //    }
    //    condition++;
    //});
    debugger
    if (checkresult) {
        var param = GetListData();
        HttpPost(`/MonthlyRoster/Create`, 'json', param, function (response) {
            window.location.href = response;  //'/WorkFlowTemplate/Index';
            SuccessMessage('Data updated successfully.');
        });
    }
};
function GetListData() {
    var months = $("#Month option:selected").val();
    var years = $("#Year option:selected").val();
    var DepartmentId = $("#DepartmentId option:selected").val();
    var ZoneId = $("#ZoneId option:selected").val();
    var DivisionId = $("#DivisionId option:selected").val();
    var Locality = $("#Locality option:selected").val();
    var SecurityGuard = $("#UserProfileId option:selected").val();
    var tableData = [];
    var Model = [];
    var condition = 0;
    $('table#RoasterTable tr').each(function () {
        var tbl = $(this).find('td').map(function () {
            return $(this).find('input[type="text"],select').map(function () {
                if ($(this).prop("tagName") == "SELECT") {
                    return JSON.stringify($(this).select2('val'));
                } else {
                    return $(this).val();
                }
            }).get();
        }).get();
        Model = {
            Date: tbl[0],
            Days: tbl[1],
            PrimaryListNo: tbl[2]
        };
        if (condition > 0) {
            tableData.push(Model);
        }
        condition++;
    });
    var JsonData = JSON.stringify(tableData);
    console.log(JsonData);
    return {
        month: months,
        year: years,
        Id: 0,
        Department: DepartmentId,
        Zone: ZoneId,
        Division: DivisionId,
        Locality: Locality,
        securityGuard: SecurityGuard,
        Template: JsonData
    };
}
