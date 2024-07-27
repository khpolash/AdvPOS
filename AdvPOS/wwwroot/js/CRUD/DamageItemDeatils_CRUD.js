var Details = function (id) {
    var url = "/DamageItemDetails/Details?id=" + id;
    $('#titleMediumModal').html("Damage Item Details");
    loadMediumModal(url);
};

var ViewItemHistory = function (ItemId) {
    var url = "/ItemsHistory/ViewItemHistory?ItemId=" + ItemId;
    $('#titleBigModal').html("Item History. Item ID: " + ItemId);
    loadBigModal(url);
};

var AddEdit = function (id) {
    var url = "/DamageItemDetails/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edit Damage Item Deatils");
    }
    else {
        $('#titleMediumModal').html("Add Damage Item Deatils");
    }
    loadMediumModal(url);
};

var Delete = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/DamageItemDeatils/Delete?id=" + id,
                success: function (result) {
                    var message = "Damage Item Deatils has been deleted successfully. Damage Item Deatils ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        text: 'Deleted!',
                        onAfterClose: () => {
                            location.reload();
                        }
                    });
                }
            });
        }
    });
};
