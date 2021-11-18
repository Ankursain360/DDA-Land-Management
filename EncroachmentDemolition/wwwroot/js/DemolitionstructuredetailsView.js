//$(document).ready(function () {
    //HttpGet(`/Demolitionstructuredetails/DetailsOfRepeater/?Id=${$("#hdnId").val() == null ? "" : $("#hdnId").val()}`, 'json', function (data) {
    //    debugger
    //    for (var i = 0; i < data.length; i++) {

    //        $("#tbl_posts #add #NoOfStructure").val(data[i].noOfStructure);
    //        $("#tbl_posts #add #NameOfStructure").val(data[i].nameOfStructure);
    //        $('#tbl_posts #add #nameOfStructure').trigger('change');
    //        if (i < data.length - 1) {
    //            var ConstructionStatus = $("#tbl_posts #add #NameOfStructure").children("option:selected").val();
    //            var content = jQuery('#tbl_posts #add tr'),
    //                size = jQuery('#tbl_posts >tbody >tr').length,
    //                element = null,
    //                element = content.clone();
    //            element.attr('id', 'rec-' + size);
    //            element.find('.delete-record').attr('data-id', size);
    //            element.appendTo('#tbl_posts_body');
    //            $('#tbl_posts_body #rec-' + size + ' #NameOfStructure').val(NameOfStructure);
    //            element.find('.sn').html(size);
    //            $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
    //            $("#tbl_posts #add .add").remove();
    //            $("#tbl_posts #tbl_posts_body .form-control").attr("readonly", true);
    //            element.find(".add-record").hide();
    //            element.find(".delete-record").show();
    //        }
    //    }
    //});

//});

$(document).ready(function () {
    $('#StructureId1').removeAttr('multiple');

    var id = parseInt($('#FixingDemolitionId').val());
    var encroachmentId = parseInt($('#EncroachmentId').val());
    var watchWardId = parseInt($('#WatchWardId').val());
    GetWatchWardDetails(watchWardId);
    GetEncroachmentDetails(encroachmentId);
    GetAnnexureADetails(id);

    FillDemolitionRptAtEdit();
    FillAreaRptAtEdit();
});

function GetWatchWardDetails(id) {
    HttpGet(`/Demolitionstructuredetails/WatchWardView/?Id=${id}`, 'html', function (response) {
        $('#WatchWardDetailsDiv').html("");
        $('#WatchWardDetailsDiv').html(response);
    });
};
function GetEncroachmentDetails(id) {
    HttpGet(`/Demolitionstructuredetails/EncroachmentRegisterView/?Id=${id}`, 'html', function (response) {
        $('#EncroachmentRegisterDetailsDiv').html("");
        $('#EncroachmentRegisterDetailsDiv').html(response);
    });
};

function GetAnnexureADetails(id) {
    HttpGet(`/Demolitionstructuredetails/AnnexureADetails/?Id=${id}`, 'html', function (response) {
        $('#AnnexureADetailsDiv').html("");
        $('#AnnexureADetailsDiv').html(response);
    });
};

function callSelect2() {
    $("select").select2({
        placeholder: "Select",
        allowClear: true
    });
}

$("#collapse").click(function () {
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
$("#collapse").click(function () {
    $("#collapseWatchWardApprroval").collapse("toggle").promise().done(function () {
        $('#select').select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});

$("#collapse").click(function () {
    $("#collapseAnnexureA").collapse("toggle").promise().done(function () {
        $('#select').select2({
            placeholder: "Select",
            allowClear: true
        });
    });
});
function onChangeDepartment1(id) {
    HttpGet(`/Demolitionstructuredetails/GetZoneList/?DepartmentId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ZoneId").html(html);
        $("#DivisionId").html('<option value="">Select</option>');
        $("#LocalityId").html('<option value="">Select</option>');
    });
};
function onChangeZone1(id) {
    HttpGet(`/Demolitionstructuredetails/GetDivisionList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#DivisionId").html(html);
        $("#LocalityId").html('<option value="">Select</option>');
    });
};
function onChangeDivision1(id) {
    HttpGet(`/Demolitionstructuredetails/GetLocalityList/?DivisionId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#LocalityId").html(html);
    });
};

//$(document).delegate('a.add-record', 'click', function (e) {
//    debugger
//    if($("#tbl_posts #add #NameOfStructure").children("option:selected").val() != '' && $("#tbl_posts #add #NameOfStructure").children("option:selected").val() != undefined && $("#tbl_posts #add #NoOfStructure").val() != ''

//    ) {
//        e.preventDefault();
//        var content = jQuery('#tbl_posts #add tr'),
//            size = jQuery('#tbl_posts >tbody >tr').length,
//            element = null,
//            element = content.clone();
//        element.attr('id', 'rec-' + size);
//        element.find('.delete-record').attr('data-id', size);
//        element.appendTo('#tbl_posts_body');
//        element.find('.sn').html(size);
//        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
//        $("#tbl_posts #add .add").remove();
//        $("#tbl_posts #tbl_posts_body .form-control").attr("readonly", true);
//        element.find(".add-record").hide();
//        element.find(".delete-record").show();
//        debugger
//        $("#tbl_posts #add .form-control").val('');
//    }
//    else {
//        alert('Please fill record before add new record ');
//    }
//});
//$(document).delegate('a.delete-record', 'click', function (e) {
//    e.preventDefault();
//    var didConfirm = confirm("Are you sure You want to delete");
//    if (didConfirm == true) {
//        var id = jQuery(this).attr('data-id');
//        var targetDiv = jQuery(this).attr('targetDiv');
//        jQuery('#rec-' + id).remove();
//        //regnerate index number on table
//        $('#tbl_posts_body tr').each(function (index) {
//            //alert(index);
//            $(this).find('span.sn').html(index + 1);
//        });
//        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
//        return true;
//    } else {
//        return false;
//    }
//});

$('#DemolitionReportFile1').change(function () {
    var fileInput = document.getElementById('DemolitionReportFile');
    var filePath = fileInput.value;
    const size = (DemolitionReportFile.files[0].size);
    fileValidation(filePath, fileInput, size);
});


function fileValidation(filePath, fileInput, size) {
    var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.pdf)$/i;
    if (!allowedExtensions.exec(filePath)) {
        alert('Invalid file type');
        fileInput.value = '';
        return false;
    }
    if (size > 10535049) {
        alert("File must be of 10 MB or Lesser Than 10 MB");
        fileInput.value = '';
        return false;
    }

}





//****************** code for saving Demolishedstructurerpt Rpt ************************


function FillDemolitionRptAtEdit() {


    HttpGet(`/Demolitionstructuredetails/GetDetailsDemolitionRpt/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger;
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts #add #Date1").val(data[i].date);
            //$("#tbl_posts #add #StructureId1").val(data[i].Structname.split('/').slice(1)); 
            $("#tbl_posts #add #StructureId1").val(data[i].structureId); 
            $("#tbl_posts #add #StructureId1").trigger('change');
            $("#tbl_posts #add #NoOfStructureDemolished").val(data[i].noOfStructureDemolished); 
            $("#tbl_posts #add #NoOfStructureRemaining").val(data[i].noOfStructureRemaining); 
         
            if (i < data.length - 1) {
                var content = jQuery('#tbl_posts #add tr'),
                    size = jQuery('#tbl_posts >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_posts_body');

                element.find('.sn').html(size);
                $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
                $("#tbl_posts #add .add").remove();
                $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
                element.find(".add-record").hide();
                element.find(".delete-record").show();
            }
        }
    });


}

$(document).delegate('a.add-record', 'click', function (e) {
    debugger

    if ($("#tbl_posts #add #Date1").val() != ''
        && $("#tbl_posts #add #StructureId1").children("option:selected").val() != undefined) {
        var struct = $("#tbl_posts #add #StructureId1").children("option:selected").val();
        e.preventDefault();
        var content = jQuery('#tbl_posts #add tr'),
            size = jQuery('#tbl_posts >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_posts_body');
        // $('#tbl_posts_body #rec-' + size + ' #Gender').val(Gender);
        $('#tbl_posts_body #rec-' + size + ' #StructureId1').val(struct);
        element.find('.sn').html(size);
        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
        $("#tbl_posts #add .add").remove();
        $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
        element.find(".add-record").hide();
        element.find(".delete-record").show();
        debugger

        $("#tbl_posts #add .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});


$(document).delegate('a.delete-record', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts_body tr').each(function (index) {
            //alert(index);
            $(this).find('span.sn').html(index + 1);
        });
        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});






//****************** code for saving Areareclaimedrpt Rpt ************************


function FillAreaRptAtEdit() {


    HttpGet(`/Demolitionstructuredetails/GetDetailsAreaRpt/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
        debugger
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts1 #add1 #Date2").val(data[i].date);
            $("#tbl_posts1 #add1 #Area1").val(data[i].areaReclaimed);
            $("#tbl_posts1 #add1 #AreaToBeReclaimed").val(data[i].areaToBeReclaimed);
           

            if (i < data.length - 1) {
                var content = jQuery('#tbl_posts1 #add1 tr'),
                    size = jQuery('#tbl_posts1 >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec1-' + size);
                element.find('.delete-record1').attr('data-id', size);
                element.appendTo('#tbl_posts1_body');

                element.find('.sn1').html(size);
                $("#tbl_posts1 #add1 .sn1").text($('#tbl_posts1 >tbody >tr').length);
                $("#tbl_posts1 #add1 .add").remove();
                $("#tbl_posts1 #tbl_posts1_body .floating-label-field").attr("readonly", true);
                element.find(".add-record1").hide();
                element.find(".delete-record1").show();
            }
        }
    });
}



$(document).delegate('a.add-record1', 'click', function (e) {
    debugger

    if ($("#tbl_posts1 #add1 #Date2").val() != ''
        && $("#tbl_posts1 #add1 #Area1").val() != '') {

        e.preventDefault();
        var content = jQuery('#tbl_posts1 #add1 tr'),
            size = jQuery('#tbl_posts1 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec1-' + size);
        element.find('.delete-record1').attr('data-id', size);
        element.appendTo('#tbl_posts1_body');
        // $('#tbl_posts_body #rec-' + size + ' #Gender').val(Gender);

        element.find('.sn1').html(size);
        $("#tbl_posts1 #add1 .sn1").text($('#tbl_posts1 >tbody >tr').length);
        $("#tbl_posts1 #add1 .add").remove();
        $("#tbl_posts1 #tbl_posts1_body .floating-label-field").attr("readonly", true);
        element.find(".add-record1").hide();
        element.find(".delete-record1").show();
        debugger

        $("#tbl_posts1 #add1 .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});

$(document).delegate('a.delete-record1', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec1-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts1_body tr').each(function (index) {
            //alert(index);
            $(this).find('span.sn1').html(index + 1);
        });
        $("#tbl_posts1 #add1 .sn1").text($('#tbl_posts1 >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});





$('.checkExtension').on('change', function (e) {
  

    debugger;
    var flag = false;
    var result = $(this).val();
    var file = result;
    if (file != null) {

        var multi = file.split(".");
        if (multi.length > 2) {

            alert("Please upload proper file with single dot in filename");
            $(this).val('');
            return;
        }
      
        var extension = file.substr((file.lastIndexOf('.') + 1));
        extension = 'pdf';
        switch (extension) {

            case 'pdf':
                flag = true;
                $('#error').empty();
                break;
            case 'PDF':
                flag = true;
                $('#error').empty();
                break;
            default:
                alert("You can upload only pdf extension file Only")
                $(this).val('');
                flag = false;
        }
      
        alert("test1");
        if (flag == true) {

            var FileID = $(this).attr('id');

            var size = ValidateFileSize(FileID, $(this));
          
            if (size > 5) {
                alert("You Can Upload file Size Up to 5 MB.");
                $(this).val('');
            }
            else {
                alert("test11");
                filecontrol = $(this);
                
                var myformData = new FormData();
                myformData.append('file', $(this)[0].files[0]);
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "/Demolitionstructuredetails/CheckFile",
                    contentType: false,
                    processData: false,
                    data: myformData,
                    success: function (response) {

                        showResult(response, filecontrol)

                    },
                    failure: function (response) {
                        //alert(response.d);
                        return false;
                    }
                });
                function showResult(response, filecontrol) {
                    debugger;
                    if (response == false) {
                        alert("Please select vaild pdf file.");
                        filecontrol.val('');
                    }
                    else {
                        return true;
                    }
                }

            }
        }
    }


});



function ValidateFileSize(fileid, file) {
    try {
        var fileSize = 0;
        if (navigator.userAgent.match(/msie/i)) {
            var obaxo = new ActiveXObject("Scripting.FileSystemObject");
            var filePath = file[0].value;
            var objFile = obaxo.getFile(filePath);
            var fileSize = objFile.size;
            fileSize = fileSize / 1048576;
        }
        else {
            fileSize = file[0].files[0].size
            fileSize = fileSize / 1048576;
        }

        return fileSize;
    }
    catch (e) {
        alert("Error is :" + e);
    }
}

