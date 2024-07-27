var Details = function (id) {
    var url = "/VatPercentage/Details?id=" + id;
    $('#titleBigModal').html("Vat Percentage Details");
    loadBigModal(url);
};


var AddEdit = function (id) {
    var url = "/VatPercentage/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Vat Percentage");
    }
    else {
        $('#titleBigModal').html("Add Vat Percentage");
    }
    loadBigModal(url);
};

var Save = function () {
    if (!$("#frmVatPercentage").valid()) {
        return;
    }

    var _frmVatPercentage = $("#frmVatPercentage").serialize();
    $.ajax({
        type: "POST",
        url: "/VatPercentage/AddEdit",
        data: _frmVatPercentage,
        success: function (result) {
            Swal.fire({
                title: result,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $('#tblVatPercentage').DataTable().ajax.reload();
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
                type: "POST",
                url: "/VatPercentage/Delete?id=" + id,
                success: function (result) {
                    var message = "Vat Percentage has been deleted successfully. Vat Percentage ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblVatPercentage').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
