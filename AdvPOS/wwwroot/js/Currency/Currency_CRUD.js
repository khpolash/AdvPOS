var Details = function (id) {
    var url = "/Currency/Details?id=" + id;
    $('#titleBigModal').html("Currency Details");
    loadBigModal(url);
};


var AddEdit = function (id) {
    var url = "/Currency/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Currency");
    }
    else {
        $('#titleBigModal').html("Add Currency");
    }
    loadBigModal(url);
};

var Save = function () {
    if (!$("#frmCurrency").valid()) {
        return;
    }

    var _frmCurrency = $("#frmCurrency").serialize();
    $.ajax({
        type: "POST",
        url: "/Currency/AddEdit",
        data: _frmCurrency,
        success: function (result) {
            Swal.fire({
                title: result,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $('#tblCurrency').DataTable().ajax.reload();
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
                url: "/Currency/Delete?id=" + id,
                success: function (result) {
                    var message = "Currency has been deleted successfully. Currency ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblCurrency').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
