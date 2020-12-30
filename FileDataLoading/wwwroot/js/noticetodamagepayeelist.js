



        $("#btnCreate").click(function () {
        //getPDF();
        var printWindow = window.open('', '');
        var doc = printWindow.document;
        var printContents = document.getElementById("MainContentForm").innerHTML;
        // doc.write(document.getElementById("divPrintRef").innerHTML);  // _Layout page div id (Mandatory)
        doc.write(printContents);
        doc.close();
        function show() {
            if (doc.readyState === "complete") {
                printWindow.focus();
                printWindow.print();
                printWindow.close();
            } else {
                setTimeout(show, 100);
            }
        };
        show();
    });







    function convert_HTML_To_PDF() {
        var doc = new jsPDF();



        var elementHTML = $('#contnet').html();
        var specialElementHandlers = {
            '#elementH': function (element, renderer) {
                return true;
            }
        };
        doc.fromHTML(elementHTML, 15, 15, {
            'width': 170,
            'elementHandlers': specialElementHandlers
        });

        // Save the PDF
        doc.save('sample-document.pdf');
    }

