
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 
var filterdata = '';
var modelopen = false;
var userId = parseInt($('#hdnUserid').val());
var roleId = parseInt($('#hdnRoleid').val());
var deptId = parseInt($('#hdnDeptid').val());
var zoneId = parseInt($('#hdnZoneid').val());
$(document).ready(function (e) {
   
    GetDashboard();
    //var classname = $(".DemolitionCls").hasClass('active');
    //if (classname == false) {
    //    /*document.getElementsByClassName("DemolitionCls").style.color = "darkgreen";*/
    //    /*$(".DemolitionCls").removeAttr('color').attr('color', 'darkgreen');*/
    //    /*$('.DemolitionCls').css('color': 'darkgreen');*/
    //    $(".DemolitionCls").css("color","darkgreen");
    //}
    //else {
    //    $('.EncroachmentCls').css({ 'color': 'darkgreen' });
    //   /* document.getElementsByClassName("#EncroachmentCls").style.color = "darkgreen";*/
    //   /* $(".EncroachmentCls").removeAttr('color').attr('color', 'darkgreen');*/
    //}
});


function GetDashboard() {
    HttpGet(`/Demolitionstructuredetails/GetDashboard/?userId=${userId}&roleId=${roleId}&deptId=${deptId}&zoneId=${zoneId}`, 'html', function (response) {
        $('#divDashboard').html("");
        $('#divDashboard').html(response);
    });
};
function DemolitionDashboard() {
    
    HttpGet(`/Demolitionstructuredetails/GetDashboard/?userId=${userId}&roleId=${roleId}&deptId=${deptId}&zoneId=${zoneId}`, 'html', function (response) {
        $('#divDashboard').html("");
        $('#divDashboard').html(response);
        $('#divDemolitionDashboard').removeClass('active').addClass('active');
        $('#divEncroachmentDemolition').removeClass('active');              
    });
};

function EncroachmentDemolition() {
    
    HttpGet(`/Demolitionstructuredetails/GetEncroachmentRegistersDashboard/?userId=${userId}&roleId=${roleId}&deptId=${deptId}&zoneId=${zoneId}`, 'html', function (response) {
    $('#divDashboard').html("");
    $('#divDashboard').html(response);
    $('#divEncroachmentDemolition').removeClass('active').addClass('active');
    $('#divDemolitionDashboard').removeClass('active');   
    return true;
    });

};
function encroachmentregistrationshowdata(filter) {
    
    filterdata = filter;
    EncroachmentRegistrationPagewisedata(currentPageNumber, currentPageSize, sortOrder, filter)
}
function EncroachmentRegistrationPagewisedata(pageNumber, pageSize, sortOrder, filter) {
    
    var param = GetSearchParam(pageNumber, pageSize, sortOrder, filter)
    $('#hfiltertextofapplication').empty().text('(' + filter + ')');
    HttpPost(`/Demolitionstructuredetails/GetAllEncroachmentRagistrationDashboardListData`, 'html', param, function (response) {
        $('#divModelContent').empty().html(response);
        if (modelopen == false) {
            modelopen = true;
            $('#btnShowModel').click();//show model
        }

    });
}

function showdata(filter) {
    filterdata = filter;
    Pagewisedata(currentPageNumber, currentPageSize, sortOrder, filter)
}

function Pagewisedata(pageNumber, pageSize, sortOrder, filter) {
    
    var param = GetSearchParam(pageNumber, pageSize, sortOrder, filter)
    $('#hfiltertext').empty().text('(' + filter + ')');
    HttpPost(`/Demolitionstructuredetails/GetDashboardListData`, 'html', param, function (response) {
        $('#divModelContent').empty().html(response);
        if (modelopen == false) {
            modelopen = true;
            $('#btnShowModel').click();//show model
        }

    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder, filterdata) {
    var userId = parseInt($('#hdnUserid').val());
    var roleId = parseInt($('#hdnRoleid').val());
    var model = {
        filter: filterdata,
        userId: userId,
        roleId: roleId,
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }
    return model;
}

//Demolition Dashboard Download Funcationlity

$(document).on("click", "#btndownloadlink", function () {
/*$("#btndownloadlink").click(function () {  */
    if (filterdata == '' || filterdata == null) {
        alert("Please refresh the page and try again");
        window.location.reload();
    }
    else {
        Download(filterdata);
    }
});
function Download(filter) {
    
    let a = document.createElement('a');
    a.target = '_blank';
    a.href = `/Demolitionstructuredetails/DwnloadDashboard/?filter=${filter}`;
    a.click();
}
//Encroachment Dashboard Download Funcationlity

$(document).on("click", "#btndownloadlinkForEncroachmentRagistration", function () {
    /*$("#btndownloadlink").click(function () {  */
    if (filterdata == '' || filterdata == null) {
        alert("Please refresh the page and try again");
        window.location.reload();
    }
    else {
        DownloadForEncroachmentDashboard(filterdata);
    }
});
function DownloadForEncroachmentDashboard(filter) {
   
    let a = document.createElement('a');
    a.target = '_blank';
    a.href = `/Demolitionstructuredetails/DownloadEncroachmentDashboard/?filter=${filter}`;
    a.click();
}


function onPaging(pageNo) {    
    var DemolitionDashboard = $('#divDemolitionDashboard').hasClass('active');
    if (DemolitionDashboard == true) {
        Pagewisedata(parseInt(pageNo), parseInt(currentPageSize), sortOrder, filterdata);
    }
    else {
        EncroachmentRegistrationPagewisedata(parseInt(pageNo), parseInt(currentPageSize), sortOrder, filterdata);
    }
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {    
    var DemolitionDashboard = $('#divDemolitionDashboard').hasClass('active');
    if (DemolitionDashboard == true) {
        Pagewisedata(parseInt(currentPageNumber), parseInt(pageSize), sortOrder, filterdata);
    }
    else {
        EncroachmentRegistrationPagewisedata(parseInt(currentPageNumber), parseInt(pageSize), sortOrder, filterdata);
    }
    currentPageSize = pageSize;
}
function resetModel() {
    modelopen = false;
}