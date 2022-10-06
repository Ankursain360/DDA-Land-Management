

$(document).ready(function () {
    $('#OwnershipStatus').removeAttr('multiple');
    var id = parseInt($('#RequestId').val());
    GetRequestData(id);
    FillKhasraAtEdit();
});


// CODE FOR SAVING VALUE OF RADIO BUTTON
$("input[name='unit']").click(function () {
    var selected = $("input[type='radio'][name='unit']:checked");
    $("#AreaUnit").val(selected.val());

});


//****************** code for saving khasra details Rpt ************************


function FillKhasraAtEdit() {
    debugger;

    HttpGet(`/Newlandannexure1/GetDetailsKhasra/?Id=${$("#Id").val() == null ? "" : $("#Id").val()}`, 'json', function (data) {
   
        for (var i = 0; i < data.length; i++) {
            $("#tbl_posts #add #KhasraNo").val(data[i].khasraNo);
            $("#tbl_posts #add #Bigha").val(data[i].bigha);
            $("#tbl_posts #add #Biswa").val(data[i].biswa);
            $("#tbl_posts #add #Biswanshi").val(data[i].biswanshi);
            $("#tbl_posts #add #OwnershipStatus").val(data[i].ownershipStatus);
            $('#tbl_posts #add #OwnershipStatus').trigger('change');
            $("#tbl_posts #add #OwnerName").val(data[i].ownerName);

            $("#tbl_posts #add #Address").val(data[i].address);
           var status = $("#tbl_posts #add #OwnershipStatus").children("option:selected").val();
           
            if (i < data.length - 1) {
                var content = jQuery('#tbl_posts #add tr'),
                    size = jQuery('#tbl_posts >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_posts_body');
                $('#tbl_posts_body #rec-' + size + ' #OwnershipStatus').val(status);
               
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

    debugger;
    if ($("#tbl_posts #add #OwnershipStatus").children("option:selected").val() != ''
        && $("#tbl_posts #add #OwnershipStatus").children("option:selected").val() != undefined
        && $("#tbl_posts #add #KhasraNo").val() != ''
        && $("#tbl_posts #add #Bigha").val() != ''
        && $("#tbl_posts #add #Biswa").val() != ''
        && $("#tbl_posts #add #Biswanshi").val() != ''

    ) 
    {
        var status = $("#tbl_posts #add #OwnershipStatus").children("option:selected").val();
        e.preventDefault();
        var content = jQuery('#tbl_posts #add tr'),
            size = jQuery('#tbl_posts >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_posts_body');
        $('#tbl_posts_body #rec-' + size + ' #OwnershipStatus').val(status);

        element.find('.sn').html(size);
        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
        $("#tbl_posts #add .add").remove();
        $("#tbl_posts #add #OwnershipStatus").select2('val', '');
        $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);

        element.find(".add-record").hide();
        element.find(".delete-record").show();
       

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




function GetRequestData(id) {
   // var id = parseInt($('#Id').val());
    HttpGet(`/Newlandannexure1/RequestView/?Id=${id}`, 'html', function (response) {
        $('#RequestView').html("");
        $('#RequestView').html(response);
    });



    $(function () {
        debugger;
        $("input[name='grpunit']").click(function () {
            if ($("#rdbHA").is(":checked")) {
                $('#AreaUnit').val('HA');
            }
            else if ($("#rdbAcre").is(":checked")) {
                $('#AreaUnit').val('Acre');
            }
            else 
                $('#AreaUnit').val('Sq Meters');
           
        });
    });

   
};
function empty() {
    debugger;
    if ($("#rdbHA").is(":checked")) {
        var HA = $('#AreaUnit').val('HA');
    }
    else if ($("#rdbAcre").is(":checked")) {
        var ACRE = $('#AreaUnit').val('Acre');
    }
    else if ($("#rdbSqm").is(":checked")) {
           var SQM = $('#AreaUnit').val('Sq Meters');
    }
    
        if (HA == undefined && ACRE == undefined && SQM == undefined) {
        alert("AreaUnit is mandatory");
        return false;
        }
   else {
           return true;
        }
    
};


//function KhasraDetailsEmpty() {
//    debugger;
//    var Khasra = $('#KhasraNo').val();
//    var bigha = $('#Bigha').val();
//    var biswa = $('#Biswa').val();
//    var biswanshi = $('#Biswanshi').val();
//    var ownershipStatus = $('#OwnershipStatus option:selected').val();
//    if (Khasra == "" || bigha == "" || biswa == "" || biswanshi == "" || ownershipStatus == "") {
//        alert("KhasraDetails is mandatory");
//        return false;
//    }
//    else {
//        return true;
//    }

//};
//function KhasraDetailsEmpty() {
//    debugger;
//    var AllTxtBoxes = new Array;

//    AllTxtBoxes = document.getElementsByTagName("tbody")[1];
//    for (var i = 0; i <= AllTxtBoxes.length; i++) {
//        var Khasra = $('#KhasraNo').val().AllTxtBoxes;
//        var bigha = $('#Bigha').val().AllTxtBoxes;
//        var biswa = $('#Biswa').val().AllTxtBoxes;
//        var biswanshi = $('#Biswanshi').val().AllTxtBoxes;
//        var ownershipStatus = $('#OwnershipStatus option:selected').val().AllTxtBoxes;
//        if (AllTxtBoxes[i].type == undefined) {
//            alert("KhasraDetails is mandatory");
//            return false;
//        }

//        else {
//            return true;
//        }
    
//    }
//}

function KhasraDetailsEmpty() {

    debugger;
    if ($("#tbl_posts #add #OwnershipStatus").children("option:selected").val() != ''
        && $("#tbl_posts #add #OwnershipStatus").children("option:selected").val() != undefined
        && $("#tbl_posts #add #KhasraNo").val() != ''
        && $("#tbl_posts #add #Bigha").val() != ''
        && $("#tbl_posts #add #Biswa").val() != ''
        && $("#tbl_posts #add #Biswanshi").val() != ''

    ) {
        return true;
    }
    else {
        alert("Khasra Details is mandatory");
            return false;
    }
}