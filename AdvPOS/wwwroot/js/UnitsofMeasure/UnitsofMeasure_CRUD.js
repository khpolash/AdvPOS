var DetailUnitsofMeasure = function (id) {
    var url = "/UnitsofMeasure/Details?id=" + id;
    $('#titleBigModal').html("Unit of Measure Details");
    loadBigModal(url);
};


var AddEditUnitsofMeasure = function (id) {
    var url = "/UnitsofMeasure/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Unit of Measure");
    }
    else {
        $('#titleBigModal').html("Add Unit of Measure");
    }
    loadBigModal(url);
};

var SaveUnitsofMeasure = function () {
    if (!$("#frmUnitsofMeasure").valid()) {
        return;
    }

    var _frmUnitsofMeasure = $("#frmUnitsofMeasure").serialize();
    $.ajax({
        type: "POST",
        url: "/UnitsofMeasure/AddEdit",
        data: _frmUnitsofMeasure,
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
                        $('#tblUnitsofMeasure').DataTable().ajax.reload();
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

var DeleteUnitsofMeasure = function (id) {
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
                url: "/UnitsofMeasure/Delete?id=" + id,
                success: function (result) {
                    var message = "Unit of Measure has been deleted successfully. Unit of Measure ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblUnitsofMeasure').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
