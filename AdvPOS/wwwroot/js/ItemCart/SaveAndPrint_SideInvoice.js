var SaveAndPrintPaymentSI = function () {
    if (listItemCart.length < 1) {
        FieldValidationAlert("#tblItemCart", "Please add at least one item.", "info");
        return;
    }
    if (!FieldValidation('#AmountSI')) {
        FieldValidationAlert("#AmountSI", "Payment Amount is Required", "info");
        return;
    }

    $("#btnSaveAndPrintSI").val("Creating Invoice...");
    $("#btnSaveAndPrintSI").attr("disabled", "disabled");

    $("#btnSaveAndPrintSI").LoadingOverlay("show", {
        background: "rgba(165, 190, 100, 0.5)",
    });
    $("#btnSaveAndPrintSI").LoadingOverlay("show");
    var _data = GetSaveAndPrintPaymentSIData();
    $.ajax({
        ContentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        url: "/ItemCart/CreateDraftItemCart",
        data: _data,
        success: function (result) {
            $("#btnSaveAndPrintSI").LoadingOverlay("hide", true);
            $("#btnSaveAndPrintSI").val("Checkout");
            $("#btnSaveAndPrintSI").removeAttr("disabled");

            ClearAllCartItem();
            location.href = "/Payment/PrintPaymentInvoice?_PaymentId=" + result.Id + "&IsSaveAndPrint=" + true;
        },
        error: function (errormessage) {
            $("#btnSaveAndPrintSI").val("Checkout");
            $("#btnSaveAndPrintSI").removeAttr("disabled");
            SwalSimpleAlert(errormessage.responseText, "warning");
        },
    });
};

var GetDefaultAmount500 = function () {
    $("#AmountSI").val(500);
    UpdateChangeAmountSI();
}
var GetDefaultAmount1000 = function () {
    $("#AmountSI").val(1000);
    UpdateChangeAmountSI();
}
var GetDefaultAmount2000 = function () {
    $("#AmountSI").val(2000);
    UpdateChangeAmountSI();
}
var GetDefaultAmount5000 = function () {
    $("#AmountSI").val(5000);
    UpdateChangeAmountSI();
}

var GetDefaultAmountFullPaid = function () {
    var _ItemChartGrandTotal = $("#ItemChartGrandTotal").text();
    const _ItemChartGrandTotalVal = _ItemChartGrandTotal.split(":");
    $("#AmountSI").val(_ItemChartGrandTotalVal[1]);
    UpdateChangeAmountSI();
}

var UpdateChangeAmountSI = function () {
    var _ItemChartGrandTotal = $("#ItemChartGrandTotal").text();
    const _ItemChartGrandTotalVal = _ItemChartGrandTotal.split(":");
    var _AmountSI = $("#AmountSI").val();
    var result = parseFloat(_AmountSI) - parseFloat(_ItemChartGrandTotalVal[1]);
    $("#ChangeAmountSI").val(result.toFixed(2));
}

var GetSaveAndPrintPaymentSIData = function () {
    var _CustomerId = $("#CustomerId").val();
    var _ItemChartSubTotal = $("#ItemChartSubTotal").text();
    const _ItemChartSubTotalVal = _ItemChartSubTotal.split(":");

    var _ItemChartGrandTotal = $("#ItemChartGrandTotal").text();
    const _ItemChartGrandTotalVal = _ItemChartGrandTotal.split(":");

    var _AmountSI = $("#AmountSI").val();
    var _ChangeAmountSI = $("#ChangeAmountSI").val();

    var ItemCartSideInvoiceViewModel = {};
    ItemCartSideInvoiceViewModel.CustomerId = _CustomerId;
    ItemCartSideInvoiceViewModel.SubTotal = _ItemChartSubTotalVal[1];
    ItemCartSideInvoiceViewModel.GrandTotal = _ItemChartGrandTotalVal[1];
    ItemCartSideInvoiceViewModel.PaidAmount = _AmountSI;
    ItemCartSideInvoiceViewModel.ChangedAmount = _ChangeAmountSI;
    ItemCartSideInvoiceViewModel.listPaymentDetail = listItemCart;
    ItemCartSideInvoiceViewModel.IsSaveAndPrint = true;

    return ItemCartSideInvoiceViewModel;
}