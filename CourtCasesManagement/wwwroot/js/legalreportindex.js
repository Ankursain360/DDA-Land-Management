
//Bind dependent Dropdown 



function GetLocalityList(id) {
    debugger;

    HttpGet(`/LegalReport/GetLocalityList/?zoneId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });
};

function GetCourtCaseNoList(id) {
    HttpGet(`/LegalReport/GetCourtCaseNoList/?filenoId=${id}`, 'json', function (response) {
        var html = '<option value="0">All</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].courtCaseNo + '</option>';
        }
        $("#Id").html(html);
    });
};