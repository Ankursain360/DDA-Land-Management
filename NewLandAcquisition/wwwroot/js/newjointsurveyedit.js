﻿

$(document).ready(function () {
    
    FillAttendanceAtEdit();
    FillRepeatorAtEdit();
   
   $('#AAttendance').removeAttr('multiple');
      

    
});

function onChange(id) {

    HttpGet(`/NewLandJointSurvey/GetVillageList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#KhasraId").select2('val', '')
        $("#VillageId").select2('val', '')
        $("#VillageId").html(html);
    });
};
function onChangeZone(id) {

    HttpGet(`/NewLandJointSurvey/GetKhasraList/?VillageId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};

$("#KhasraId").change(function () {
    var kid = $(this).val();
    if (kid) {
        HttpGet(`/NewLandJointSurvey/GetAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);
            // alert(JSON.stringify(response));
        });

    }
});





//****************** code for saving tenant  details Rpt ************************

function FillAttendanceAtEdit() {


    HttpGet(`/NewLandJointSurvey/GetDetailsAttendance/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
       
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts2 #add2 #AName").val(data[i].name);
            $("#tbl_posts2 #add2 #ADesignation").val(data[i].designation);
            $("#tbl_posts2 #add2 #AAttendance").val(data[i].attendance);
            $('#tbl_posts2 #add2 #AAttendance').trigger('change');
            if (i < data.length - 1) {
                var Gender = $("#tbl_posts2 #add2 #AAttendance").children("option:selected").val();
                var content = jQuery('#tbl_posts2 #add2 tr'),
                    size = jQuery('#tbl_posts2 >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record2').attr('data-id', size);
                element.appendTo('#tbl_posts2_body');
                $('#tbl_posts2_body #rec-' + size + ' #AAttendance').val(Gender);
                element.find('.sn2').html(size);
                $("#tbl_posts2 #add2 .sn2").text($('#tbl_posts2 >tbody >tr').length);
                $("#tbl_posts2 #add2 .add").remove();
                //$("#tbl_posts2 #add2 #AAttendance").select2('val', '');
                $("#tbl_posts2 #tbl_posts2_body .floating-label-field").attr("readonly", true);
                element.find(".add-record2").hide();
                element.find(".delete-record2").show();
            }
        }
    });
}
$(document).ready(function () {
    $('#AAttendance').removeAttr('multiple');
    //$('#AAttendance').removeAttr('multiple');

});

$(document).delegate('a.add-record2', 'click', function (e) {
 

    if ($("#tbl_posts2 #add2 #AName").val() != ''
        && $("#tbl_posts2 #add2 #ADesignation").val() != ''
        && $("#tbl_posts2 #add2 #AAttendance").children("option:selected").val() != ''
        && $("#tbl_posts2 #add2 #AAttendance").children("option:selected").val() != undefined
    ) {
        var Gender = $("#tbl_posts2 #add2 #AAttendance").children("option:selected").val();
        e.preventDefault();
        var content = jQuery('#tbl_posts2 #add2 tr'),
            size = jQuery('#tbl_posts2 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record2').attr('data-id', size);
        element.appendTo('#tbl_posts2_body');

        $('#tbl_posts2_body #rec-' + size + ' #AAttendance').val(Gender);
        element.find('.sn2').html(size);
        $("#tbl_posts2 #add2 .sn2").text($('#tbl_posts2 >tbody >tr').length);
        $("#tbl_posts2 #add2 .add").remove();
        $("#tbl_posts2 #add2 #AAttendance").select2('val', '');
        $("#tbl_posts2 #tbl_posts2_body .floating-label-field").attr("readonly", true);
        element.find(".add-record2").hide();
        element.find(".delete-record2").show();
        

        $("#tbl_posts2 #add2 .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});
$(document).delegate('a.delete-record2', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts2_body tr').each(function (index) {
            //alert(index);
            $(this).find('span .sn2').html(index + 1);
        });
        $("#tbl_posts2 #add2 .sn2").text($('#tbl_posts2 >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});

function FillRepeatorAtEdit() {

    /* -----------Survey Report  --------------- */
    HttpGet(`/NewLandJointSurvey/GetDetailssurveyreport/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
       
        for (var i = 0; i < data.length; i++) {
           
            $("#tbl_posts #add #DocumentName").val(data[i].documentName);
          

            $("#tbl_posts #add #UploadFilePath").val(data[i].uploadFilePath);
           


            if (data[i].uploadFilePath != "" && data[i].uploadFilePath != null) {
                $("#tbl_posts #add #viewDocumentId").attr('href', '/NewLandJointSurvey/ViewSurveyReportFile/' + data[i].id)
                $("#tbl_posts #add #viewDocumentId").show();
            } else {
                $("#tbl_posts #add #viewDocumentId").hide();
            }

           
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

    if 
        ($("#tbl_posts #add #DocumentName").val() != ''

    ) {
       
        e.preventDefault();
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

function onChange(id) {

    HttpGet(`/NewLandJointSurvey/GetVillageList/?ZoneId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#KhasraId").select2('val', '')
        $("#VillageId").select2('val', '')
        $("#VillageId").html(html);
    });
};
function onChangeVillage(id) {

    HttpGet(`/NewLandJointSurvey/GetKhasraList/?VillageId=${id}`, 'json', function (response) {
        var html = '<option value="">Select</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#KhasraId").select2('val', '')
        $("#KhasraId").html(html);
    });
};

$("#KhasraId").change(function () {
    var kid = $(this).val();
    if (kid) {
        HttpGet(`/NewLandJointSurvey/GetAreaList/?khasraid=${kid}`, 'json', function (response) {

            $("#Bigha").val(response.bigha);
            $("#Biswa").val(response.biswa);
            $("#Biswanshi").val(response.biswanshi);
            // alert(JSON.stringify(response));
        });

    }
});