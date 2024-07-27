var Details = function (id) {
    var url = "/PaymentStatus/Details?id=" + id;
    $('#titleBigModal').html("Payment Status Details");
    loadBigModal(url);
};


var AddEdit = function (id) {
    var url = "/PaymentStatus/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Payment Status");
    }
    else {
        $('#titleBigModal').html("Add Payment Status");
    }
    loadBigModal(url);
};

var Save = function () {
    if (!$("#frmPaymentStatus").valid()) {
        return;
    }

    var _frmPaymentStatus = $("#frmPaymentStatus").serialize();
    $.ajax({
        type: "POST",
        url: "/PaymentStatus/AddEdit",
        data: _frmPaymentStatus,
        success: function (result) {
            Swal.fire({
                title: result,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $('#tblPaymentStatus').DataTable().ajax.reload();
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
                url: "/PaymentStatus/Delete?id=" + id,
                success: function (result) {
                    var message = "Payment Status has been deleted successfully. Payment Status ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblPaymentStatus').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
