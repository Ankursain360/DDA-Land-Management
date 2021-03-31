


$("#ApplicationId").change(function () {
    var id = $(this).val();

    if (id) {
      
        HttpGet(`/CalculationSheet/Receipt/?ApplicationId=${id}`, 'html', function (response) {

            
            $('#View').html("");
            $('#View').html(response);

        });

    }
});





$(function () {
    $("#btnPrint").click(function () {
        $('.jhide').hide();
        nWin($("#View").html(), $("#pagename").html());
        $('.jhide').show();
    });
});
function nWin(context, title) {
    var printWindow = window.open('', '');
    var doc = printWindow.document;
    var printContents = document.getElementById("View").innerHTML;
    doc.write(document.getElementById("View").innerHTML);
    //doc.write(printContents);
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
};
