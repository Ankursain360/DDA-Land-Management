
$(document).ready(function () {
    $(function () {
        $("#btnGenerate").click(function () {
            debugger;
            var url = '/ArchivedProperty/GetDetails';
           
            var departmentid = $('#DepartmentId option:selected').val();
            var zoneId = $('#ZoneId option:selected').val();
            var divisionId = $('#DivisionId option:selected').val();
            var id = $('#Id option:selected').val();
         
            $('#LoadReportView').empty();
            $('#LoadReportView').load(url, {
                department: departmentid, zone: zoneId, division: divisionId,
                primaryListNo: id
            }).hide().fadeIn(1000);;

        });
    });
})


$(function () {
    $(".linkdisabled").click(function () {
        return false;
    });
});
//Bind Zone Dropdown from Department
function GetZoneList(id) {
    HttpGet(`/ArchivedProperty/GetZoneList/?departmentId=${id}`, 'json', function (response) {
        var html = '<option value="">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
    });
};
//Bind Divison Dropdown from zone
function GetDivisionList(id) {

    HttpGet(`/ArchivedProperty/GetDivisionList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
    });
};
//Bind PrimaryNoList Dropdown from division
function GetPrimaryNoList(id) {
    debugger;
    
    HttpGet(`/ArchivedProperty/GetPrimaryNoList/?divisionId=${id}`, 'json', function (response) {
        var html = '<option value="">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].primaryListNo + '</option>';
        }
        $("#Id").html(html);
    });
};