function villageDetails(){
    debugger;
    var VillageId = $("#VillageId").val();
    if (VillageId == 0 || VillageId == null) {
        alert("Please Select Village");
        window.location.reload();
        return false;
    }
    else {

        return true;
    }
};
