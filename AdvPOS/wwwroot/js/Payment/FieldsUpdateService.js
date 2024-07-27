var UpdateDueAndChangeAmount = function (TotalPaid) {
    var _PaidAmount = $("#PaidAmount").val();
    var _GrandTotal = $("#GrandTotal").val();

    if (_PaidAmount > _GrandTotal) {
        $('#DueAmount').val(0);
        $('#ChangedAmount').val((_PaidAmount - _GrandTotal).toFixed(2));
    }
    else {
        var result = parseFloat(_GrandTotal) - parseFloat(_PaidAmount);
        $('#DueAmount').val(result.toFixed(2));
        $('#ChangedAmount').val(0);
    }
}




/*
var UpdateAllNumericField = function () {
    UpdateSubTotalAndGrandTotal();
    UpdateCommonCharge();
    UpdateDiscount();
    UpdateTax();
    UpdatePaidAmount();
}

var UpdateSubTotalAndGrandTotal = function () {
    $('#SubTotal').val(sumTotalAmount().toFixed(2));
    $('#GrandTotal').val(sumTotalAmount().toFixed(2));
}

var UpdateCommonCharge = function () {
    var _GetInputFields = GetInputFields();
    var result = sumTotalAmount() + _GetInputFields.CommonCharge;
    $('#SubTotal').val(result.toFixed(2));
    $('#GrandTotal').val(result.toFixed(2));

    UpdateDiscount();
    UpdateTax();
    UpdatePaidAmount();
}

var UpdateDiscount = function () {
    var _GetInputFields = GetInputFields();
    var _sumTotalAmount = sumTotalAmount() + _GetInputFields.CommonCharge;
    var _DiscountPercentage = _GetInputFields.Discount / 100;
    var result = _sumTotalAmount - (_sumTotalAmount * _DiscountPercentage);

    $('#DiscountAmount').html((_sumTotalAmount * _DiscountPercentage).toFixed(2));
    $('#GrandTotal').val(result.toFixed(2));

    UpdatePaidAmount();
}

var UpdateTax = function () {
    var _GetInputFields = GetInputFields();

    var _sumTotalAmount = sumTotalAmount() + _GetInputFields.CommonCharge;
    var _DiscountPercentage = _GetInputFields.Discount / 100;
    _sumTotalAmount = _sumTotalAmount - (_sumTotalAmount * _DiscountPercentage);
    var _TaxPercentage = _GetInputFields.VAT / 100;
    var result = _sumTotalAmount + (_sumTotalAmount * _TaxPercentage);

    $('#TaxAmount').html((_sumTotalAmount * _TaxPercentage).toFixed(2));
    $('#GrandTotal').val(result.toFixed(2));

    UpdatePaidAmount();
}

var sumTotalAmount = function () {
    var _sumTotalAmount = 0;
    for (let i = 0; i < listPaymentDetail.length; i++) {
        _sumTotalAmount = parseFloat(_sumTotalAmount) + parseFloat(listPaymentDetail[i].TotalAmount);
    }
    $('#SumPaymentItem').text(_sumTotalAmount.toFixed(2));
    return _sumTotalAmount;
}

var UpdatePaidAmount = function () {
    var _GetInputFields = GetInputFields();

    if (_GetInputFields.PaidAmount > _GetInputFields.GrandTotal) {
        $('#DueAmount').val(0);
        $('#ChangedAmount').val((_GetInputFields.PaidAmount - _GetInputFields.GrandTotal).toFixed(2));
    }
    else {
        $('#DueAmount').val((_GetInputFields.GrandTotal - _GetInputFields.PaidAmount).toFixed(2));
        $('#ChangedAmount').val(0);
    }
}

var UpdateUnitPrice = function () {
    var _ItemId = $("#ItemId option:selected").text();
    var splitArray = _ItemId.split(":");

    var _UnitPrice = parseFloat(splitArray[1]);
    var _ItemVAT = parseFloat(splitArray[3]);

    $("#ItemVAT").val(_ItemVAT);
    $("#UnitPrice").val(_UnitPrice);
    $("#PaymentsDetailsId").val($("#ItemId").val());
    //onchangeUnitPrice();
}

*/