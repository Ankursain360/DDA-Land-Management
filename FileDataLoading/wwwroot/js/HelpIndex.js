$(document).ready(function () {

    $('a#downloadLink').on('click', function (event) {
        debugger;
        event.preventDefault();
        const message = "You are being redirected to an external website.\nClick OK to continue or Cancel to stay.";
        const url = "https://www.microsoft.com/en-us/download/";
        if (confirm(message)) {
            window.open(url, '_blank');
        }
    });
    $('a#downloadAdobeLink').on('click', function (event) {
        debugger;
        event.preventDefault();
        const message = "You are being redirected to an external website.\nClick OK to continue or Cancel to stay.";
        const url = "https://www.adobe.com/";
        if (confirm(message)) {
            window.open(url, '_blank');
        }
    });
});