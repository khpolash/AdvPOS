var Details = function (id) {
    var url = "/PaymentType/Details?id=" + id;
    $('#titleBigModal').html("Payment Type Details");
    loadBigModal(url);
};


var AddEdit = function (id) {
    var url = "/PaymentType/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Payment Type");
    }
    else {
        $('#titleBigModal').html("Add Payment Type");
    }
    loadBigModal(url);
};

var Save = function () {
    if (!$("#frmPaymentType").valid()) {
        return;
    }

    var _frmPaymentType = $("#frmPaymentType").serialize();
    $.ajax({
        type: "POST",
        url: "/PaymentType/AddEdit",
        data: _frmPaymentType,
        success: function (result) {
            Swal.fire({
                title: result,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $('#tblPaymentType').DataTable().ajax.reload();
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
                url: "/PaymentType/Delete?id=" + id,
                success: function (result) {
                    var message = "Payment Type has been deleted successfully. Payment Type ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblPaymentType').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
