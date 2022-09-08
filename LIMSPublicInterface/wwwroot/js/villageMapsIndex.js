function villageDetails(){
    debugger;
    var VillageId = $("#VillageId").val();
    if (VillageId == 0 || VillageId == null) {
        alert("Please Select Village");
        window.location.reload();
        return false;
    }
    else {
        var id = $('#VillageId option:selected').val();
        HttpAsyncGet(`/VillageMaps/List/?Id=${id}`, 'html', function (response) {
            $('#maps').html("");
            $('#maps').html(response);
        });

        return true;
    }
};
