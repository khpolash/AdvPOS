var Details = function (id) {
    var url = "/Branch/Details?id=" + id;
    $('#titleBigModal').html("Branch Details");
    loadBigModal(url);
};


var AddEdit = function (id) {
    var url = "/Branch/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleExtraBigModal').html("Edit Branch");
    }
    else {
        $('#titleExtraBigModal').html("Add Branch");
    }
    loadExtraBigModal(url);
};

var Save = function () {
    if (!$("#frmBranch").valid()) {
        return;
    }

    var _frmBranch = $("#frmBranch").serialize();
    $("#btnSave").val("Please Wait");
    $('#btnSave').attr('disabled', 'disabled');
    $.ajax({
        type: "POST",
        url: "/Branch/AddEdit",
        data: _frmBranch,
        success: function (result) {
            Swal.fire({
                title: result,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $("#btnSave").val("Save");
                $('#btnSave').removeAttr('disabled');
                $('#tblBranch').DataTable().ajax.reload();
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
                url: "/Branch/Delete?id=" + id,
                success: function (result) {
                    var message = "Branch has been deleted successfully. Branch ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblBranch').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
