

function onChange(id) {
   /* debugger*/
    HttpGet(`/NewSelfAssessmentForm/GetNewVillageList/?Districtid=${id}`, 'json', function (response) {
        var html = '<option value="">--Select--</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
      /*  $("#ColonyId").select2('val', '')*/
        //$("#VillageId").select2('val', '')
        $("#VillageId").empty().html(html);
    });
};
function onChangeVillage(id) {

    HttpGet(`/NewSelfAssessmentForm/GetColonyList/?VillageId=${id}`, 'json', function (response) {
        var html = '<option value="">--Select--</option>';
        for (var i = 0; i < response.length; i++) {
            html = html + '<option value=' + response[i].id + '>' + response[i].name + '</option>';
        }
        $("#ColonyId").html('')
        $("#ColonyId").html(html);
    });
};
$(document).delegate('a.add-record2', 'click', function (e) {
    debugger

    if ($("#tbl_posts2 #add2 #CarpetArea").val() != ''
        && $("#tbl_posts2 #add2 #FloorName").val() != '')
    {       
        e.preventDefault();
        var content = jQuery('#tbl_posts2 #add2 tr'),
            size = jQuery('#tbl_posts2 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record2').attr('data-id', size);
        element.appendTo('#tbl_posts2_body');

     /*   $('#tbl_posts2_body #rec-' + size + ' #FloorName').val(Gender);*/
        element.find('.sn2').html(size);
        $("#tbl_posts2 #add2 .sn2").text($('#tbl_posts2 >tbody >tr').length);
        $("#tbl_posts2 #add2 .add").remove();
      /*  $("#tbl_posts2 #add2 #AAttendance").select2('val', '');*/
        $("#tbl_posts2 #tbl_posts2_body .floating-label-field").attr("readonly", true);
        element.find(".add-record2").hide();
        element.find(".delete-record2").show();
        debugger

        $("#tbl_posts2 #add2 .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});


$(document).delegate('a.add-record3', 'click', function (e) {
    debugger

    if ($("#tbl_posts3 #add3 #NameOfTheSeller").val() != ''
        && $("#tbl_posts3 #add3 #NameOfThePayer").val() != '') {
        e.preventDefault();
        var content = jQuery('#tbl_posts3 #add3 tr'),
            size = jQuery('#tbl_posts3 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record3').attr('data-id', size);
        element.appendTo('#tbl_posts3_body');

        /*   $('#tbl_posts2_body #rec-' + size + ' #FloorName').val(Gender);*/
        element.find('.sn2').html(size);
        $("#tbl_posts3 #add3 .sn2").text($('#tbl_posts2 >tbody >tr').length);
        $("#tbl_posts3 #add3 .add").remove();
        /*  $("#tbl_posts2 #add2 #AAttendance").select2('val', '');*/
        $("#tbl_posts3 #tbl_posts3_body .floating-label-field").attr("readonly", true);
        element.find(".add-record3").hide();
        element.find(".delete-record3").show();
        debugger

        $("#tbl_posts2 #add3 .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});

$(document).delegate('a.add-record4', 'click', function (e) {
    debugger
    if ($("#tbl_posts4 #add4 #NameOfThePayerAts").val() != ''
        && $("#tbl_posts4 #add4 #NameOfTheSellerAts").val() != '') {
        e.preventDefault();
        var content = jQuery('#tbl_posts4 #add4 tr'),
            size = jQuery('#tbl_posts4 >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record4').attr('data-id', size);
        element.appendTo('#tbl_posts4_body');

        /*   $('#tbl_posts2_body #rec-' + size + ' #FloorName').val(Gender);*/
        element.find('.sn2').html(size);
        $("#tbl_posts4 #add4 .sn2").text($('#tbl_posts4 >tbody >tr').length);
        $("#tbl_posts4 #add4 .add").remove();
        /*  $("#tbl_posts2 #add2 #AAttendance").select2('val', '');*/
        $("#tbl_posts4 #tbl_posts4_body .floating-label-field").attr("readonly", true);
        element.find(".add-record4").hide();
        element.find(".delete-record4").show();
        debugger

        $("#tbl_posts4 #add4 .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});


$(document).delegate('a.delete-record3', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts3_body tr').each(function (index) {
            //alert(index);
            $(this).find('span .sn2').html(index + 1);
        });
        $("#tbl_posts3 #add3 .sn2").text($('#tbl_posts3 >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});


$(document).delegate('a.delete-record4', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_posts4_body tr').each(function (index) {
            //alert(index);
            $(this).find('span .sn2').html(index + 1);
        });
        $("#tbl_posts4 #add4 .sn2").text($('#tbl_posts4 >tbody >tr').length);
        return true;
    } else {
        return false;
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


