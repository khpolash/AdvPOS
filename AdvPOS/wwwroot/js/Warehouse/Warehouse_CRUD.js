var DetailWarehouse = function (id) {
    var url = "/Warehouse/Details?id=" + id;
    $('#titleBigModal').html("Warehouse Details");
    loadBigModal(url);
};


var AddEditWarehouse = function (id) {
    var url = "/Warehouse/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Warehouse");
    }
    else {
        $('#titleBigModal').html("Add Warehouse");
    }
    loadBigModal(url);
};

var SaveWarehouse = function () {
    if (!$("#frmWarehouse").valid()) {
        return;
    }

    var _frmWarehouse = $("#frmWarehouse").serialize();
    $.ajax({
        type: "POST",
        url: "/Warehouse/AddEdit",
        data: _frmWarehouse,
        success: function (result) {
            if (result.IsSuccess) {
                Swal.fire({
                    title: result.AlertMessage,
                    icon: "success"
                }).then(function () {
                    document.getElementById("btnClose").click();
                    if (result.CurrentURL == "/Items/Index") {
                        $('#tblItem').DataTable().ajax.reload();
                    }
                    else {
                        $('#tblWarehouse').DataTable().ajax.reload();
                    }
                });
            }
            else {
                Swal.fire({
                    title: result.AlertMessage,
                    icon: "warning"
                }).then(function () {
                    setTimeout(function () {
                        $('#Name').focus();
                    }, 400);
                });
            }
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var DeleteWarehouse = function (id) {
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
                url: "/Warehouse/Delete?id=" + id,
                success: function (result) {
                    var message = "Warehouse has been deleted successfully. Warehouse ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblWarehouse').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};