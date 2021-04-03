


$("#btnGenerate").click(function () {

  //  var id = $(this).val();
    var id = $("#ddlRefNo").children("option:selected").val();
    var allotid = id;
   // var daten = document.getElementById("txtDate").value;
    var refn = document.getElementById("txtRefernceNumber").value;
   // sessionStorage.NewDate = daten;
    sessionStorage.RefN = refn;
    sessionStorage.Vid = allotid;
    if (id) {

        HttpGet(`/LetterofAllotment/Receipt/?ApplicationId=${id}`, 'html', function (response) {
            debugger

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
