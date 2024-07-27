$(document).ready(function () {
    var _divQuoteNo = document.getElementById("divQuoteNo");
    var _QuoteNoRef = $("#QuoteNoRef").val();
    if (_QuoteNoRef == -3) {
        $("#Category").val("3").change();
    }
    else {
        _divQuoteNo.style.display = "none";
    }
});

$('#Category').change(function () {
    var _divPaymentStatus = document.getElementById("divPaymentStatus");
    var _divInvoiceNo = document.getElementById("divInvoiceNo");
    var _divQuoteNo = document.getElementById("divQuoteNo");
    var _divPaymentDetail = document.getElementById("divPaymentDetail");

    var _DocumentType = $('#Category').val();
    if (_DocumentType == 1) {
        _divPaymentStatus.style.display = "block";
        _divInvoiceNo.style.display = "block";
        _divQuoteNo.style.display = "none";
        _divPaymentDetail.style.display = "block";
    }
    else {
        _divPaymentStatus.style.display = "none";
        _divInvoiceNo.style.display = "none";
        _divQuoteNo.style.display = "block";
        _divPaymentDetail.style.display = "none";
    }

});
