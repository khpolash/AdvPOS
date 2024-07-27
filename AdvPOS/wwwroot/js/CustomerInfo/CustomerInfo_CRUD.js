var Details = function(id) {
    var url = "/CustomerInfo/Details?id=" + id;
    $('#titleBigModal').html("Customer Info Details");
    loadBigModal(url);
};

var TranHistory = function (id) {
    var url = "/CustomerInfo/CustomerTranHistory?id=" + id;
    $('#titleExtraBigModal').html("Customer Tran Summary");
    loadExtraBigModal(url);
};

var AddEdit = function(id) {
    var url = "/CustomerInfo/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleExtraBigModal').html("Edit Customer Info");
    } else {
        $('#titleExtraBigModal').html("Add Customer Info");
    }
    loadExtraBigModal(url);
};

var Save = function() {
    if (!$("#frmCustomerInfo").valid()) {
        return;
    }

    var _frmCustomerInfo = $("#frmCustomerInfo").serialize();
    $.ajax({
        type: "POST",
        url: "/CustomerInfo/AddEdit",
        data: _frmCustomerInfo,
        success: function(result) {
            Swal.fire({
                title: result.AlertMessage,
                icon: "success"
            }).then(function() {
                document.getElementById("btnClose").click();
                $('#tblCustomerInfo').DataTable().ajax.reload();
            });
        },
        error: function(errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var Delete = function(id) {
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
                url: "/CustomerInfo/Delete?id=" + id,
                success: function(result) {
                    var message = "Customer Info has been deleted successfully. Customer Info ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblCustomerInfo').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};