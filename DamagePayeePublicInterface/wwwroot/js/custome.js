//$(document).ready(function () {
//    debugger;
//    function confirmRedirect(event) {

//        event.preventDefault();

//        const message = "You are being redirected to an external website.\nClick OK to continue or Cancel to stay.";
//        const url = "https://www.microsoft.com/en-us/download/";
//        if (confirm(message)) {
//            window.open(url, '_blank');
//        }

//        return false;
//    }
//});

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
    $('a#digitalindiaLink').on('click', function (event) {
        debugger;
        event.preventDefault();
        const message = "You are being redirected to an external website.\nClick OK to continue or Cancel to stay.";
        const url = "https://digitalindia.gov.in/";
        if (confirm(message)) {
            window.open(url, '_blank');
        }
    });
    $('a#data_Gov_Link').on('click', function (event) {
        debugger;
        event.preventDefault();
        const message = "You are being redirected to an external website.\nClick OK to continue or Cancel to stay.";
        const url = "https://data.gov.in/";
        if (confirm(message)) {
            window.open(url, '_blank');
        }
    });
    $('a#makeinindiaLink').on('click', function (event) {
        debugger;
        event.preventDefault();
        const message = "You are being redirected to an external website.\nClick OK to continue or Cancel to stay.";
        const url = "https://www.makeinindia.com/";
        if (confirm(message)) {
            window.open(url, '_blank');
        }
    });
    $('a#india_Gov_Link').on('click', function (event) {
        debugger;
        event.preventDefault();
        const message = "You are being redirected to an external website.\nClick OK to continue or Cancel to stay.";
        const url = "https://india.gov.in/";
        if (confirm(message)) {
            window.open(url, '_blank');
        }
    });
    $('a#my_gov_Link').on('click', function (event) {
        debugger;
        event.preventDefault();
        const message = "You are being redirected to an external website.\nClick OK to continue or Cancel to stay.";
        const url = "https://www.mygov.in/";
        if (confirm(message)) {
            window.open(url, '_blank');
        }
    });
});