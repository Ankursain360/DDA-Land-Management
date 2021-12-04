$(document).ready(function () {
    $('#Gender').removeAttr('multiple');
    

});

$('#email').keyup(function () {
    $("#err-email").hide();
    var email = $('#email').val();

    var testEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (!testEmail.test(email)) {
        $("#err-email").show();
        return;
    } else {
        $("#err-email").hide();
    }

});



function fileCheck(obj) {
    var fileExtension = ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
    if ($.inArray($(obj).val().split('.').pop().toLowerCase(), fileExtension) == -1)
        alert("Only '.jpeg','.jpg', '.png', '.gif', '.bmp' formats are allowed.");
}

//    <script>

//        $(function () {
//        var dtToday = new Date();

//        var month = dtToday.getMonth() + 1;
//        var day = dtToday.getDate();
//        var year = dtToday.getFullYear();
//        if (month < 10)
//            month = '0' + month.toString();
//        if (day < 10)
//            day = '0' + day.toString();

//        var maxDate = year + '-' + month + '-' + day;
//        //alert(maxDate);
//        $('#txtDateofWill').attr('max', maxDate);
//        $('txtTakenOverDate').attr('max', maxDate);
//        $('txtDateRep').attr('max', maxDate);
//    });



//</script>

//@* For radio button event click *@

    $(function () {
        $("input[name='grpDamageAssesseeType']").click(function () {
            if ($("#rSubsequent").is(":checked")) {
                $("#DivForSubsequentPurchaser").show();
            } else {
                $("#DivForSubsequentPurchaser").hide();
            }
        });
    });

    $(function () {
        $("input[name='grpLitigation']").click(function () {
            if ($("#rbdYesLitigation").is(":checked")) {
                $("#DivForLitigationStatus").show();
            } else {
                $("#DivForLitigationStatus").hide();
            }
        });
    });

    $(function () {
        $("input[name='grpYESNO']").click(function () {
            var show = true;
            if ($("#rdbPayeeYes").is(":checked")) {
                $("#DivForPayeeNo").hide();
            } else {
                $("#DivOpenSubstitutionForm").find("input,select,textarea").each(function () {
                        if (($(this).is('[required]') || $(this).is('[data-val-required]')) && (!$(this).is(':hidden')) && (!$(this).is(':disabled'))) {
                            if (show == true) {
                                if ($(this).val() != null && $(this).val() != undefined && $(this).val() != '') {
                                }
                                else {
                                    show = false;
                                }
                            }
                        }
                    });
                if (show) {
                 
                        $("#DivForPayeeNo").show();
                    }
                    else {
                        alert('Please Fill All Mandatory Fields');
                        $("#DivForPayeeNo").hide();
                    }
            }
        });
    });

    $(function () {
        $("input[name='grpUseofpeoperty']").click(function () {
            if ($("#rdbResidential").is(":checked")) { //1st radio button
                $("#txtResidential").attr("disabled", "disabled");
                $("#txtCommercial").attr("disabled", "disabled");
                $("#txtResidential").removeAttr("disabled", "disabled");
                $("#txtCommercial").attr("disabled", "disabled");
                $("#txtCommercialmts").val('');
                $("#txtCommercial").val('');

            }
            else if ($("#rdbCommercial").is(":checked")) {
                $("#txtResidential").attr("disabled", "disabled");
                $("#txtCommercial").attr("disabled", "disabled");
                $("#txtResidential").attr("disabled", "disabled");
                $("#txtCommercial").removeAttr("disabled", "disabled");
                $("#txtResidentialmts").val('');
                $("#txtResidential").val('');
            }

            else if ($("#rdbMixed").is(":checked")) {
                $("#txtResidential").attr("disabled", "disabled");
                $("#txtCommercial").attr("disabled", "disabled");
                $("#txtResidential").removeAttr("disabled", "disabled");
                $("#txtCommercial").removeAttr("disabled", "disabled");
                $("#txtResidentialmts").val('');
                $("#txtResidential").val('');
                $("#txtCommercialmts").val('');
                $("#txtCommercial").val('');
            }

            else {
                $("#txtResidential").attr("disabled", "disabled");
                $("#txtCommercial").attr("disabled", "disabled");
                $("#txtResidentialmts").val('');
                $("#txtResidential").val('');
                $("#txtCommercialmts").val('');
                $("#txtCommercial").val('');
            }

        });
    });



   
        //@* convert yds to meters*@
    $("#txtPlotyds").change(function () {
        var plotyds = $("#txtPlotyds").val();
        var plotmeter = '';
        plotmeter = plotyds / 1.19599005;
        $("#txtPlotmts").val((plotmeter).toFixed(3));
    });

    $("#txtFlooryds").change(function () {
        var flooryds = $("#txtFlooryds").val();
        var floormeter = '';
        floormeter = flooryds / 1.19599005;
        $("#txtFloormts").val((floormeter).toFixed(3));
    });

    $("#txtResidential").change(function () {
        var yds = $("#txtResidential").val();
        var meter = '';
        meter = yds / 1.19599005;
        $("#txtResidentialmts").val((meter).toFixed(3));
       
    });

    $("#txtCommercial").change(function () {
        var yds = $("#txtCommercial").val();
        var meter = '';
        meter = yds / 1.19599005;
        $("#txtCommercialmts").val((meter).toFixed(3));
    });

// CODE FOR SAVING VALUE OF RADIO BUTTON
$("input[name='grpDamageAssesseeType']").click(function () {
    var selected = $("input[type='radio'][name='grpDamageAssesseeType']:checked");
    $("#TypeOfDamageAssessee").val(selected.val());

});

$("input[name='grpUseofpeoperty']").click(function () {
    var selected = $("input[type='radio'][name='grpUseofpeoperty']:checked");
    $("#UseOfProperty").val(selected.val());

});

$("input[name='grpLitigation']").click(function () {
    var selected = $("input[type='radio'][name='grpLitigation']:checked");
    $("#LitigationStatus").val(selected.val());

});

$("input[name='grpCaseperson']").click(function () {
    var selected = $("input[type='radio'][name='grpCaseperson']:checked");
    $("#PetitionerRespondent").val(selected.val());

});

$("input[name='grpYESNO']").click(function () {
    var selected = $("input[type='radio'][name='grpYESNO']:checked");
    $("#IsDdadamagePayee").val(selected.val());

});

$("input[name='Bill']").click(function () {
    var selected = $("input[type='radio'][name='Bill']:checked");
    $("#IsDocumentFor").val(selected.val());

});

        //@*Repeator code  *@
    //$(document).ready(function () {
    //        $("#tbl_posts #tbl_posts_body .odd").remove();
    //    $("#tbl_posts #add .form-control").attr("multiple", false);
    //})

   

//****************** code for personal info Rpt ************************

$(document).delegate('a.add-record', 'click', function (e) {
    debugger

    if ($("#tbl_posts #add #Gender").children("option:selected").val() != ''
        && $("#tbl_posts #add #Gender").children("option:selected").val() != undefined
        && $("#tbl_posts #add #payeeName").val() != ''
        && $("#tbl_posts #add #payeeFatherName").val() != ''
        && $("#tbl_posts #add #MobileNo").val() != ''
        && $("#tbl_posts #add #EmailId").val() != ''

    ) {
        var Gender = $("#tbl_posts #add #Gender").children("option:selected").val();
        e.preventDefault();
        var content = jQuery('#tbl_posts #add tr'),
            size = jQuery('#tbl_posts >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-record').attr('data-id', size);
        element.appendTo('#tbl_posts_body');
        $('#tbl_posts_body #rec-' + size + ' #Gender').val(Gender);

        element.find('.sn').html(size);
        $("#tbl_posts #add .sn").text($('#tbl_posts >tbody >tr').length);
        $("#tbl_posts #add .add").remove();
        $("#tbl_posts #tbl_posts_body .floating-label-field").attr("readonly", true);
        element.find(".add-record").hide();
        element.find(".delete-record").show();
        debugger

        $("#tbl_posts #add .floating-label-field").val('');
        $("#tbl_posts #add .field-validation-valid").html('');
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



//****************** code for Allotte type Rpt ************************

$(document).delegate('a.add-recordDamageAssessee', 'click', function (e) {
    debugger

    if ($("#tbl_DamageAssessee #addDamageAssessee #txtDamageAssesseeName").val() != ''
        && $("#tbl_DamageAssessee #addDamageAssessee #txtDamageAssesseeFather").val() != ''
        && $("#tbl_DamageAssessee #addDamageAssessee #txtDateofWill").val() != ''
        && $("#tbl_DamageAssessee #addDamageAssessee #ATSGPA").val() != ''
       
    ) {
      e.preventDefault();
        var content = jQuery('#tbl_DamageAssessee #addDamageAssessee tr'),
            size = jQuery('#tbl_DamageAssessee >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-recordDamageAssessee').attr('data-id', size);
        element.appendTo('#tbl_DamageAssessee_body');
         element.find('.sn1').html(size);
        $("#tbl_DamageAssessee #addDamageAssessee .sn1").text($('#tbl_DamageAssessee >tbody >tr').length);
        $("#tbl_DamageAssessee #addDamageAssessee .add").remove();
        $("#tbl_DamageAssessee #tbl_DamageAssessee_body .floating-label-field").attr("readonly", true);
        element.find(".add-recordDamageAssessee").hide();
        element.find(".delete-recordDamageAssessee").show();
        debugger
        /*$("#tbl_posts #add .form-control").val('');*/
        $("#tbl_DamageAssessee #addDamageAssessee .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});
$(document).delegate('a.delete-recordDamageAssessee', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_DamageAssessee_body tr').each(function (index) {
            //alert(index);
            $(this).find('span.sn1').html(index + 1);
        });
        $("#tbl_DamageAssessee #addDamageAssessee .sn1").text($('#tbl_DamageAssessee >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});



//****************** code for Payment History ************************

$(document).delegate('a.add-recordPayment', 'click', function (e) {
    debugger

    if ($("#tbl_Payment #addPayment #txtPersonalName").val() != ''
        && $("#tbl_Payment #addPayment #txtPersonalFatherName").val() != ''
        && $("#tbl_Payment #addPayment #txtPersonalMobileNo").val() != ''
        && $("#tbl_Payment #addPayment #txtPersonalEmailid").val() != ''
        && $("#tbl_Payment #addPayment #Reciept").val() != '')
    {
        e.preventDefault();
        var content = jQuery('#tbl_Payment #addPayment tr'),
            size = jQuery('#tbl_Payment >tbody >tr').length,
            element = null,
            element = content.clone();
        element.attr('id', 'rec-' + size);
        element.find('.delete-recordPayment').attr('data-id', size);
        element.appendTo('#tbl_Payment_body');
        element.find('.sn2').html(size);
        $("#tbl_Payment #addPayment .sn2").text($('#tbl_Payment >tbody >tr').length);
        $("#tbl_Payment #addPayment .add").remove();
        $("#tbl_Payment #tbl_Payment_body .floating-label-field").attr("readonly", true);
        element.find(".add-recordPayment ").hide();
        element.find(".delete-recordPayment").show();
        debugger
        
        $("#tbl_Payment #addPayment .floating-label-field").val('');
    }
    else {
        alert('Please fill record before add new record ');
    }
});
$(document).delegate('a.delete-recordPayment', 'click', function (e) {
    e.preventDefault();
    var didConfirm = confirm("Are you sure You want to delete");
    if (didConfirm == true) {
        var id = jQuery(this).attr('data-id');
        var targetDiv = jQuery(this).attr('targetDiv');
        jQuery('#rec-' + id).remove();
        //regnerate index number on table
        $('#tbl_Payment_body tr').each(function (index) {
            //alert(index);
            $(this).find('span.sn2').html(index + 1);
        });
        $("#tbl_Payment #addPayment .sn2").text($('#tbl_Payment >tbody >tr').length);
        return true;
    } else {
        return false;
    }
});

//********************* Length validation****************************
var minLength = 0;
var maxLength = 45;


$(function () {
    $('.Namecheck').keyup(function () {
        debugger;
    var char = $(this).val();
    var charLength = $(this).val().length;
    //if (charLength < minLength) {
    //    $('span').text('Length is short, minimum ' + minLength + ' required.');
    //} else 
    if (charLength > maxLength) {
        $('.msg').text('Length is not valid, maximum ' + maxLength + ' allowed.');
        $(this).val(char.substring(0, maxLength));
    }
    else {
      
    }
});
});










function chkvalidAadhar(input) {
    var value = $(input).val();
    
        var re = /^([2-9]{1}[0-9]{3}[0-9]{4}[0-9]{4})$/;
        var is_valid = re.test(value);
        if (!is_valid) {
            $(input).val('');
            $(input).focus();
            // alert("Please enter valid Pan card number");
            $(input).nextAll('span:first').empty().html('Please enter valid Aadhar card number');
        }
    }


function chkvalid(input) {
    var value = $(input).val();
    var re = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
    var is_valid = re.test(value);
    if (!is_valid) {
        $(input).val('');
        $(input).focus();

        $(input).nextAll('span:first').empty().html('Please enter valid Pan card number');
    } 
      
}

function chkvalidmobile(input) {
    var value = $(input).val();
    var re = /^([0-9]{10})$/;
    var is_valid = re.test(value);
    if (!is_valid) {
        $(input).val('');
        $(input).focus();
        // alert("Please enter valid Pan card number");
        $(input).nextAll('span:first').empty().html('Please enter valid mobile number');
    }
}


function chkvalidemail(input) {
    var value = $(input).val();
    var re = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    var is_valid = re.test(value);
    if (!is_valid) {
        $(input).val('');
        $(input).focus();
        // alert("Please enter valid Pan card number");
        $(input).nextAll('span:first').empty().html('Please enter valid email id');
    }
}



$('#DocumentIFormFile').change(function () {
    var fileInput = document.getElementById('DocumentIFormFile');
    var filePath = fileInput.value;
    const size = (DocumentIFormFile.files[0].size);
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

//$("form").submit(function () {
//    if ('#payeeName' == ""
//        && 'payeeFatherName' == ""
//        && 'Gender' == ""
//        && 'Address' == ""
//        && ('MobileNo' == "" || 'MobileNo' == null)
//        && 'EmailId' == ""
//        && 'AadharNo' == ""
//        && 'Photograph' == ""
//        && 'SignatureFile' == ""
//    ) {
        
//        alert('Please fill all mandatory feilds of Personal Information of Damage Assessee repeater');
//        return false;
//    }
//    else
//    {
//        return true;
//    }
    
//});



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


        if (flag == true) {

            var FileID = $(this).attr('id');

            var size = ValidateFileSize(FileID, $(this));

            if (size > 5) {
                alert("You Can Upload file Size Up to 5 MB.");
                $(this).val('');
            }
            else {

                filecontrol = $(this);
                var myformData = new FormData();
                myformData.append('file', $(this)[0].files[0]);
                $.ajax({
                    async: false,
                    type: "POST",
                    url: "../DamagePayeeRegister/CheckFile",
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



