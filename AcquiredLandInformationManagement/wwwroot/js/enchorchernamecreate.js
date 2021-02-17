var currentPageNumber = 1;
var currentPageSize = 10;
var sortby = 1;
// var freeholdstatus = 0;
var name = 0;
var address = 0;
var fileno = 0;
var recstate = 0;

$(document).ready(function () {
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
});

function GetDataStorage(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    //  HttpPost(`/SearchByParticular/List`, 'html', param, function (response) {
    HttpPost(`/EncroachmentDetails/GetEnchorcherNameDetails`, 'html', param, function (response) {
        $('#divEncrocherNameDetails').html("");
        $('#divEncrocherNameDetails').html(response);
    });

}
$("#btnAddEName").click(function () {
    //   debugger
    name = $('#NameE').val(),
    fileno  = $('#txtFileNo').val(),
    address  = $('#AddressE').val(),
    recstate = parseInt(($('#KhasraId option:selected').val()))
    GetDataStorage(currentPageNumber, currentPageSize, sortby);
});


function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {
        name: name ,
        fileno: fileno ,
        address: address,
        Recstate: recstate
      
    }
    debugger
    return model;
}

function onPaging(pageNo) {
    GetDataStorage(parseInt(pageNo), parseInt(currentPageSize), sortby);
    currentPageNumber = pageNo;
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetDataStorage(parseInt(currentPageNumber), parseInt(pageSize), sortby);
    currentPageSize = pageSize;
}
$(function () {
    var coll = document.getElementsByClassName("collapsible");
    var i;

    for (i = 0; i < coll.length; i++) {
        coll[i].addEventListener("click", function () {
            this.classList.toggle("active");
            var content = this.nextElementSibling;
            if (content.style.maxHeight) {
                content.style.maxHeight = null;
            } else {
                content.style.maxHeight = content.scrollHeight + "px";
            }
        });
    }
});








