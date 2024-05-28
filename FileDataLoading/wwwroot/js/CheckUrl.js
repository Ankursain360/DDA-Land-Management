function CheckUrl() {
    debugger;
    var URL = document.getElementById("url").value
    var a = document.createElement("a");
    if (URL != null) {
        if (confirm("You are being redirected to an external website. Please note that DDA LMIS Portal is not responsible for external websites content & privacy Policy.")) {
            a.target = '_blank';
            a.href = URL;
            a.click();
            return true;
        } else {
           
            return false;
        }
      
    }
    else {
       
        return false;
    }
}