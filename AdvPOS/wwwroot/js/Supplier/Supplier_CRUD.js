var DetailSupplier = function (id) {
    var url = "/Supplier/Details?id=" + id;
    $('#titleBigModal').html("Supplier Details");
    loadBigModal(url);
};


var AddEditSupplier = function (id) {
    var url = "/Supplier/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Supplier");
    }
    else {
        $('#titleBigModal').html("Add Supplier");
    }
    loadBigModal(url);
};

var SaveSupplier = function () {
    if (!$("#frmSupplier").valid()) {
        return;
    }

    var _frmSupplier = $("#frmSupplier").serialize();
    $.ajax({
        type: "POST",
        url: "/Supplier/AddEdit",
        data: _frmSupplier,
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
                        $('#tblSupplier').DataTable().ajax.reload();
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

var DeleteSupplier = function (id) {
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
                url: "/Supplier/Delete?id=" + id,
                success: function (result) {
                    var message = "Supplier has been deleted successfully. Supplier ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblSupplier').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
