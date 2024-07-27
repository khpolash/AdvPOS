var ReturnLogDetails = function (id) {
    var url = "/ReturnLog/Details?id=" + id;
    $('#titleBigModal').html("ReturnLog Details");
    loadBigModal(url);
};

var GetSalesReturnItemDetails = function (RefId) {
    var url = "/Payment/GetSalesReturnItemDetails?id=" + RefId;
    $('#titleExtraBigModal').html("Payment Details");
    loadExtraBigModal(url);
};

var GetSalesRetrurn = function (id) {
    var url = "/ReturnLog/GetSalesRetrurn?id=" + id;
    $('#titleExtraBigModal').html("Payment Return");
    loadExtraBigModal(url);
    setTimeout(function () {
        $('#ReturnNote').focus();
    }, 300);
};

var SaveSalesRetrurn = function () {
    var _PaymentId = $("#PaymentId").val();
    var _ReturnNote = $("#ReturnNote").val();

    $("#btnConfirmReturn").val("Please Wait...");
    $('#btnConfirmReturn').attr('disabled', 'disabled');

    Swal.fire({
        title: 'Do you want to return this full payment?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/ReturnLog/SaveSalesRetrurn?id=" + _PaymentId + "&_ReturnNote= " + _ReturnNote,
                success: function (result) {
                    $('#tblPayments').DataTable().ajax.reload();
                    document.getElementById("btnClose").click();
                    $("#btnConfirmReturn").val("Confirm Return");
                    $('#btnConfirmReturn').removeAttr('disabled');
                    message = "Invoice has been return successfully. Invoice ID: " + result.InvoiceNo;
                    SwalSimpleAlert(message, "info");
                },
                error: function (errormessage) {
                    SwalSimpleAlert(errormessage.responseText, "warning");
                }
            });
        }
        else {
            $("#btnConfirmReturn").val("Confirm Return");
            $('#btnConfirmReturn').removeAttr('disabled');
        }
    });
};

var SingleItemSalesRetrurn = function (_PaymentId, _PaymentDetailId) {
    Swal.fire({
        title: 'Do you want to return this single item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/ReturnLog/SingleItemSalesRetrurn?PaymentId=" + _PaymentId + "&PaymentDetailId= " + _PaymentDetailId,
                success: function (result) {
                    $('#tblPayments').DataTable().ajax.reload();
                    document.getElementById("btnClose").click();
                    message = "Single item has been returned successfully. Item Name: " + result.ItemName;
                    SwalSimpleAlert(message, "info");
                },
                error: function (errormessage) {
                    SwalSimpleAlert(errormessage.responseText, "warning");
                }
            });
        }
    });
}
