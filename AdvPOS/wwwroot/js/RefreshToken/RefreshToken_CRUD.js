var Details = function (id) {
    var url = "/RefreshToken/Details?id=" + id;
    $('#titleBigModal').html("Refresh Token Details");
    loadBigModal(url);
};

var ViewUserDetails = function (Id) {
    var url = "/UserManagement/ViewUserDetails?Id=" + Id;
    $('#titleBigModal').html("User Details ");
    loadBigModal(url);
};

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
                url: "/RefreshToken/Delete?id=" + id,
                success: function (result) {
                    var message = "Refresh Token has been deleted successfully. Refresh Token ID: " + result.Id;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblRefreshToken').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};
