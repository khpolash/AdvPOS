var Details = function (id) {
    var url = "/Payment/Details?id=" + id;
    $('#titleExtraBigModal').html("Payment Details");
    loadExtraBigModal(url);
};

var ViewCustomerDetails = function (id) {
    var url = "/CustomerInfo/Details?id=" + id;
    $('#titleBigModal').html("Customer Info Details");
    loadBigModal(url);
};

var POSReport = function (id) {
    var url = "/Payment/POSReport?id=" + id;
    $('#titleSmallModal').html("POS Report");
    loadCommonModal(url);
};

var PrintPOSReport = function (id) {
    location.href = "/Payment/PrintPOSReport?id=" + id;
};

var PrintPaymentInvoice = function (PaymentId) {
    location.href = "/Payment/PrintPaymentInvoice?_PaymentId=" + PaymentId;
};

var AddEdit = function (id) {
    var url = "/Payment/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleExtraBigModal').html("Edit Payment");
        //GetPaymentSummary(id);
    } else {
        $('#titleExtraBigModal').html("Add Payment");
    }
    localStorage.removeItem('PaymentId');
    localStorage.removeItem('CurrentURL');
    loadExtraBigModal(url);
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
                type: "DELETE",
                url: "/Payment/Delete?id=" + id,
                success: function (result) {
                    var message;
                    if (result.Category == 1) {
                        $('#tblPayments').DataTable().ajax.reload();
                        message = "Invoice has been deleted successfully. Invoice ID: " + result.InvoiceNo;
                    }
                    else if (result.Category == 2) {
                        $('#tblPaymentDraft').DataTable().ajax.reload();
                        message = "Draft Invoice has been deleted successfully. Draft InvoiceID: " + result.InvoiceNo;
                    }
                    else if (result.Category == 3) {
                        $('#tblQuoteInvoice').DataTable().ajax.reload();
                        message = "Quote has been deleted successfully. Quote ID: " + result.QuoteNo;
                    }

                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {                           
                        }
                    });
                }
            });
        }
    });
};

var AddNewCustomer = function () {
    activaTab('divAddNewCustomer');
};

$(document).ready(function () {
    $('.CustomerId').select2({
        ajax: {
            type: "GET",
            url: '/CustomerInfo/GetAllCustomerForDDL',
            dataType: 'json'
        },
    });
});


var GetPriceModel = function () {
    var _PriceModel = $("#PriceModel").val();
    $.ajax({
        type: "GET",
        url: "/Payment/GetPriceModel?Id=" + _PriceModel,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data === null) return;
            var $dropdown = $('#ItemId');
            $('#ItemId').html('').select2({ data: [{ id: '', text: '' }] });
            $.each(data, function () {
                $dropdown.append($("<option />").val(this.Id).text(this.Name));
            });
        },
        error: function (response) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });

};


var GetItemByItemBarcode = function (_ItemBarcode) {
    if (_ItemBarcode == null || _ItemBarcode == '') return;

    $.ajax({
        type: "GET",
        url: "/Payment/GetItemByItemBarcode?ItemBarcode=" + _ItemBarcode,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data === null) {
                Swal.fire({
                    title: "Item not found for Barcode: " + _ItemBarcode,
                    icon: 'info',
                    onAfterClose: () => {
                        setTimeout(function () {
                            $('#ItemBarcode').focus();
                        }, 300);
                    }
                });
                return;
            }

            $("#ItemId").val(data.Id);
            $('#ItemId').append(data.Id).trigger('change');
            UpdateUnitPrice();
            $('#Quantity').focus();
        },
        error: function (response) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });

};

var GetPaymentSummaryData = function () {
    $.ajax({
        type: "GET",
        url: "/Payment/GetPaymentSummaryData",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data === null) return;

            $("#ReportSubTotal").text(data.SubTotal.toFixed(2));
            $("#ReportDiscountAmount").text(data.DiscountAmount.toFixed(2));
            $("#ReportVATAmount").text(data.VATAmount.toFixed(2));
            $("#ReportGrandTotal").text(data.GrandTotal.toFixed(2));

            $("#ReportDueAmount").text(data.DueAmount.toFixed(2));
            $("#ReportPaidAmount").text(data.PaidAmount.toFixed(2));

            $("#ReportChangedAmount").text(data.ChangedAmount.toFixed(2));
            $("#ReportGrandTotalCashflow").text(data.GrandTotal.toFixed(2));
        },
        error: function (response) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
};

var GetCustomerHistory = function (CustomerId) {
    $.ajax({
        type: "GET",
        url: "/Payment/GetCustomerHistory?CustomerId=" + CustomerId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data === null) {
                $("textarea#CustomerNote").val('');
                return;
            }
            $("textarea#CustomerNote").val(data.CustomerNote);
            $("#PrevousBalance").val(data.PrevousBalance.toFixed(2));
        },
        error: function (response) {
            SwalSimpleAlert(response.responseText, "warning");
        }
    });
};

var GetPaymentSummary = function (Id) {
    $.ajax({
        type: "GET",
        url: "/Payment/GetPaymentSummary?Id=" + Id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            UpdatePaymentFields(result);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

var UpdatePaymentFields = function (PaymentCRUDViewModel) {
    $("#PaidAmount").val(PaymentCRUDViewModel.PaidAmount);
    $('#SumPaymentHistory').text(PaymentCRUDViewModel.PaidAmount);

    $("#ChangedAmount").val(PaymentCRUDViewModel.ChangedAmount);
    $("#SubTotal").val(PaymentCRUDViewModel.SubTotal);

    $("#GrandTotal").val(PaymentCRUDViewModel.GrandTotal.toFixed(2));
    $("#DueAmount").val(PaymentCRUDViewModel.DueAmount);


    $("#DiscountAmount").val(PaymentCRUDViewModel.DiscountAmount);
    $("#VATAmount").val(PaymentCRUDViewModel.VATAmount);

    var _SumPaymentItem = PaymentCRUDViewModel.GrandTotal - PaymentCRUDViewModel.CommonCharge;
    $("#SumPaymentItem").text(_SumPaymentItem);
}