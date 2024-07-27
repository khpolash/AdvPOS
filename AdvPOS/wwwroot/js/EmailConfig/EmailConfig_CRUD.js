var DetailsEmailConfig = function(id) {
    var url = "/EmailConfig/Details?id=" + id;
    $('#titleBigModal').html("Email Config Details");
    loadBigModal(url);
};


var AddEditEmailConfig = function(id) {
    var url = "/EmailConfig/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleBigModal').html("Edit Email Config");
    } else {
        $('#titleBigModal').html("Add Email Config");
    }
    loadBigModal(url);
};

var SaveEmailConfig = function() {
    if (!$("#frmEmailConfig").valid()) {
        return;
    }

    var _frmEmailConfig = $("#frmEmailConfig").serialize();
    $.ajax({
        type: "POST",
        url: "/EmailConfig/AddEdit",
        data: _frmEmailConfig,
        success: function(result) {
            Swal.fire({
                title: result,
                icon: "success"
            }).then(function() {
                document.getElementById("btnCloseEmailConfig").click();
                $('#tblEmailConfig').DataTable().ajax.reload();
            });
        },
        error: function(errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var DeleteEmailConfig = function(id) {
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
                url: "/EmailConfig/Delete?id=" + id,
                success: function(result) {
                    var message = "Email Config has been deleted successfully. Email Config ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblEmailConfig').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};