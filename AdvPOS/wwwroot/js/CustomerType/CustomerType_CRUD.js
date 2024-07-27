var Details = function (id) {
    var url = "/CustomerType/Details?id=" + id;
    $('#titleBigModal').html("Customer Type Details");
    loadBigModal(url);
};


var AddEdit = function (id) {
    var url = "/CustomerType/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Customer Type");
    }
    else {
        $('#titleBigModal').html("Add Customer Type");
    }
    loadBigModal(url);
};

var Save = function () {
    if (!$("#frmCustomerType").valid()) {
        return;
    }

    var _frmCustomerType = $("#frmCustomerType").serialize();
    $.ajax({
        type: "POST",
        url: "/CustomerType/AddEdit",
        data: _frmCustomerType,
        success: function (result) {
            Swal.fire({
                title: result,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $('#tblCustomerType').DataTable().ajax.reload();
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
                url: "/CustomerType/Delete?id=" + id,
                success: function (result) {
                    var message = "CustomerType has been deleted successfully. Customer Type ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblCustomerType').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
