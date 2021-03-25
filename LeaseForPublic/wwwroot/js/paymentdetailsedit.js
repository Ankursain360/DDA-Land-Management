
var currentPageNumber = 1;
var currentPageSize = 5;
var sortOrder = 1;//default Ascending 

$(document).ready(function () {
      GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);
    // ClearFields();
});

$("#btnSearch").click(function () {
    GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);

});

$("#btnReset").click(function () {

    $('#txtNotificationN').val('');

    GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);
});


$("#btnAscending").click(function () {
    $("#btnDescending").removeClass("active");
    $("#btnAscending").addClass("active");
    sortOrder = 1;//for Ascending
    GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);
});



$("#btnDescending").click(function () {
    $("#btnAscending").removeClass("active");
    $("#btnDescending").addClass("active");
    sortOrder = 2;//for Descending
    GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);
});
$('#ddlSort').change(function () {
    GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);
});
function GetPremiumrate(pageNumber, pageSize, order) {
    var param = GetSearchParam(pageNumber, pageSize, order);
    
    HttpPost(`/LeasePaymentDetails/List`, 'html', param, function (response) {

        $('#divPossessionDetail').html("");
        $('#divPossessionDetail').html(response);

    });
}

function GetSearchParam(pageNumber, pageSize, sortOrder) {
    var model = {

        AllotmentId: $("#ddlrno").children("option:selected").val(),

        sortBy: $("#ddlSort").children("option:selected").val(),
        sortOrder: parseInt(sortOrder),
        pageSize: parseInt(pageSize),
        pageNumber: parseInt(pageNumber)
    }

    return model;
}

function onPaging(pageNo) {
    GetPremiumrate(parseInt(pageNo), parseInt(currentPageSize), sortOrder);
    currentPageNumber = pageNo;
}

function onChangePageSize(pageSize) {
    GetPremiumrate(parseInt(currentPageNumber), parseInt(pageSize), sortOrder);
    currentPageSize = pageSize;
}
$(function () {
    $("input[name='grpPaymentMode']").click(function () {

        if ($("#A").is(":checked")) {
            $('#PaymentMode').val('Challan');
        }


        else {
            $('#PaymentMode').val('Epayment');
        }
    });
});

$("input[name='grpPaymentMode']").click(function () {
    var selected = $("input[type='radio'][name='grpPaymentMode']:checked");
    $("#PaymentMode").val(selected.val());

});

$(function ClearFields() {

    if ((document.getElementById('btnSubmit').value == "Submit Details") || (document.getElementById('btnSubmit').value == "Create")) {
        $('#txtamount').val('');
        $('#txtchallan').val('');
        $('#txtpdate').val('');
        $('#ddlrno').val('');
        $('#ddlpt').val('');
    } else { }
    if (document.getElementById('hid2').value == "1") {
        $('#txtamount').val('');
        $('#txtchallan').val('');
        $('#txtpdate').val('');
        $('#ddlrno').val('');
        $('#ddlpt').val('');
    }
});

$("#ddlrno").change(function () {

    var kid = $(this).val();
    if (kid) {

        GetPremiumrate(currentPageNumber, currentPageSize, sortOrder);


    }
});


