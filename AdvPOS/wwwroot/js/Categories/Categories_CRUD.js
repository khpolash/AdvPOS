var DetailCategories = function (id) {
    var url = "/Categories/Details?id=" + id;
    $('#titleMediumModal').html("Categories Details");
    loadMediumModal(url);
};

var AddEditCategories = function (id) {
    var url = "/Categories/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edit Categories");
    }
    else {
        $('#titleMediumModal').html("Add Categories");
    }
    loadMediumModal(url);
};

var SaveCategories = function () {
    if (!$("#frmCategories").valid()) {
        return;
    }

    var _frmCategories = $("#frmCategories").serialize();
    $.ajax({
        type: "POST",
        url: "/Categories/AddEdit",
        data: _frmCategories,
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
                        $('#tblCategories').DataTable().ajax.reload();
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

var DeleteCategories = function (id) {
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
                url: "/Categories/Delete?id=" + id,
                success: function (result) {
                    var message = "Categories has been deleted successfully. Categories ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblCategories').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
