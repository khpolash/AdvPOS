var AddPaymentDetail = function () {
    if (!FieldValidation('#ItemId')) {
        FieldValidationAlert('#ItemId', 'Payment Item is Required.', "warning");
        return;
    }
    var _Quantity = $("#Quantity").val();
    if (_Quantity === "" || _Quantity === null || parseFloat(_Quantity) < 1) {
        FieldValidationAlert('#Quantity', 'Quantity is Required', "warning");
        return;
    }

    //Check Stock
    var _ItemIdText = $("#ItemId option:selected").text();
    var splitArray = _ItemIdText.split(":");
    if (parseFloat(splitArray[5]) < parseFloat($("#Quantity").val())) {
        FieldValidationAlert('#Quantity', 'Stock limit crosses the selected quantity, please check the quantity.', "warning");
        return;
    }

    $("#btnPaymentsDetails").val("Please Wait");
    $("#btnPaymentsDetails").attr('disabled', 'disabled');


    var _UnitPrice = $("#UnitPrice option:selected").text();
    var splitUnitPrice = _UnitPrice.split(":");
    _UnitPrice = splitUnitPrice[1];


    var splitArrayForName = _ItemIdText.split(":");
    var PaymentsDetailItem = {};
    PaymentsDetailItem.ItemId = $("#ItemId").val();
    PaymentsDetailItem.PaymentId = $("#Id").val();
    PaymentsDetailItem.ItemName = splitArrayForName[0];
    PaymentsDetailItem.Quantity = $("#Quantity").val();
    PaymentsDetailItem.UnitPrice = parseFloat(_UnitPrice);
    PaymentsDetailItem.ItemVAT = $("#ItemVAT").val();
    PaymentsDetailItem.ItemDiscount = $("#ItemDiscount").val();
    PaymentsDetailItem.TotalAmount = $("#TotalAmount").val();

    var _PaymentCRUDViewModel = PreparedFormObj();
    PaymentsDetailItem.PaymentCRUDViewModel = _PaymentCRUDViewModel;
    var _Category = $("#Category").val();
    PaymentsDetailItem.PaymentCRUDViewModel.Category = _Category;

    $.ajax({
        type: "POST",
        url: "/Payment/AddPaymentDetail",
        data: PaymentsDetailItem,
        dataType: "json",
        success: function (result) {
            UpdatePaymentFields(result.PaymentCRUDViewModel);
            AddHTMLTableRow(result);
            $("#btnPaymentsDetails").val("Add Item");
            $('#btnPaymentsDetails').removeAttr('disabled');
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}


function UpdatePaymentDetail(button) {
    var row = $(button).closest("TR");
    var _PaymentDetailId = $("TD", row).eq(0).html();
    var _ItemName = $("#ItemName" + _PaymentDetailId).val();

    var _IsVat = $("#IsVat").val();
    var _ItemVAT;
    if (_IsVat == "Yes") {
        _ItemVAT = $("TD", row).eq(4).html();
        $("#ItemVAT").val(_ItemVAT);
    }
    else {
        _ItemVAT = 0;
    }

    var _Quantity = parseFloat($("#Quantity" + _PaymentDetailId).val());
    var _UnitPrice = parseFloat($("#UnitPrice" + _PaymentDetailId).val());
    var _ItemDiscount = parseFloat($("#ItemDiscount" + _PaymentDetailId).val());


    $("#btnUpdatePaymentsDetail" + _PaymentDetailId).val("Please Wait");
    $("#btnUpdatePaymentsDetail" + _PaymentDetailId).attr('disabled', 'disabled');

    var PaymentDetailCRUDViewModel = {};
    PaymentDetailCRUDViewModel.Id = _PaymentDetailId;
    PaymentDetailCRUDViewModel.ItemName = _ItemName;
    PaymentDetailCRUDViewModel.Quantity = _Quantity;
    PaymentDetailCRUDViewModel.UnitPrice = _UnitPrice;
    PaymentDetailCRUDViewModel.ItemVAT = _ItemVAT;
    PaymentDetailCRUDViewModel.ItemDiscount = _ItemDiscount;
    PaymentDetailCRUDViewModel.TotalAmount = _Quantity * _UnitPrice;

    var _PaymentCRUDViewModel = PreparedFormObj();
    PaymentDetailCRUDViewModel.PaymentCRUDViewModel = _PaymentCRUDViewModel;
    var _Category = $("#Category").val();
    PaymentDetailCRUDViewModel.PaymentCRUDViewModel.Category = _Category;

    $.ajax({
        type: "POST",
        url: "/Payment/UpdatePaymentDetail",
        data: PaymentDetailCRUDViewModel,
        dataType: "json",
        success: function (result) {
            toastr.success("Update item successfully. Item ID: " + result.Id, 'Success');
            $("#TotalAmount" + result.Id).text(result.TotalAmount);
            $("#btnUpdatePaymentsDetail" + _PaymentDetailId).val("Update");
            $('#btnUpdatePaymentsDetail' + _PaymentDetailId).removeAttr('disabled');

            UpdatePaymentFields(result.PaymentCRUDViewModel);
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}


var DeletePaymentDetail = function (PaymentDetailCRUDViewModel) {
    var _PaymentCRUDViewModel = PreparedFormObj();
    PaymentDetailCRUDViewModel.PaymentCRUDViewModel = _PaymentCRUDViewModel;

    $.ajax({
        type: "DELETE",
        url: "/Payment/DeletePaymentDetail",
        data: PaymentDetailCRUDViewModel,
        dataType: "json",
        success: function (result) {
            toastr.success("Payment details item has been deleted successfully. ID: " + result.Id, 'Success');
            UpdatePaymentFields(result);
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}
