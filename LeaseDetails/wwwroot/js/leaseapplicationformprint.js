$(document).ready(function () {

    /* -----------Start Call Repeator Added by Renu  --------------- */
    //FillDocumentRepeator();
    PrintData();
});
function closePrintWindow() {

    window.close();
}

function PrintData() {
    debugger;
    var printWindow = this.window;
    var doc = printWindow.document;
    var printContents = document.getElementById("LetterData").innerHTML;
    doc.write(document.getElementById("divPrintRef").innerHTML);  // _Layout page div id (Mandatory)
   
    function show() {
        if (doc.readyState === "complete") {
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        } else {
            doc.write(printContents);
            doc.close();
            setTimeout(show, 100);
        }
    };
    show();
    window.onafterprint = closePrintWindow;
}

function FillDocumentRepeator() {/* -----------Added by Renu  --------------- */

    HttpGet(`/LeaseApplicationForm/GetDocumentChecklistDetails/`, 'json', function (data) {
        for (var i = 0; i < data.length; i++) {

            $("#tbl_posts #add #DocumentChecklistId").val(data[i].id);
            $("#tbl_posts #add #ServiceId").val(data[i].serviceTypeId);
            $("#tbl_posts #add #DocumentName").val(data[i].name);
            $("#tbl_posts #add #Mandatory").val((data[i].isMandatory) == 1 ? "Yes" : "No");
            $("#tbl_posts #add #IsMandatory").val(data[i].isMandatory);
            $("#tbl_posts #add #FileUploadedPath").val('');
            if ((data[i].isMandatory) == 1 ? false : true)
                $('#tbl_posts #add #FileUploaded').removeAttr('required');
            if (i < data.length - 1) {
                var content = jQuery('#tbl_posts #add tr'),
                    size = jQuery('#tbl_posts >tbody >tr').length,
                    element = null,
                    element = content.clone();
                element.attr('id', 'rec-' + size);
                element.find('.delete-record').attr('data-id', size);
                element.appendTo('#tbl_posts_body');
                $("#tbl_posts #add .add").remove();
            }
            //if ((data[i].isMandatory) == 1 ? false : true)
            //    jQuery('#DivDocument >tbody >tr').removeAttr('required');
        }
    });
}
