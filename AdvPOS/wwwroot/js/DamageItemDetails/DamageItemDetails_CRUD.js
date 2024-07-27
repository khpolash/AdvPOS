var Details = function (id) {
    var url = "/DamageItemDetails/Details?id=" + id;
    $('#titleBigModal').html("Damage Item Deatils");
    loadBigModal(url);
};


var AddEdit = function (id) {
    var url = "/DamageItemDetails/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Damage Item");
    }
    else {
        $('#titleBigModal').html("Add Damage Item");
    }
    loadBigModal(url);
};

var Save = function () {
    if (!$("#frmDamageItemDeatils").valid()) {
        return;
    }

    var _frmDamageItemDeatils = $("#frmDamageItemDeatils").serialize();
    $.ajax({
        type: "POST",
        url: "/DamageItemDetails/AddEdit",
        data: _frmDamageItemDeatils,
        success: function (result) {
            Swal.fire({
                title: result,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $('#tblDamageItemDeatils').DataTable().ajax.reload();
            });
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var Delete = function (id) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "DELETE",
                url: "/DamageItemDetails/Delete?id=" + id,
                success: function (result) {
                    var message = "Damage Item has been deleted successfully. Damage Item ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblDamageItemDeatils').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
