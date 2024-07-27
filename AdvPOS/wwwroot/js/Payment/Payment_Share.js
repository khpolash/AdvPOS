
$(document).ready(function () {
    var _divReceiverEmail = document.getElementById("divReceiverEmail");
    _divReceiverEmail.style.display = "none";
});

$("body").on("click", "#chkTypeReceiverEmail", function () {
    var _divReceiverEmailId = document.getElementById("divReceiverEmailId");
    var _divReceiverEmail = document.getElementById("divReceiverEmail");

    if ($('#chkTypeReceiverEmail').is(":checked")) {
        _divReceiverEmailId.style.display = "none";
        _divReceiverEmail.style.display = "block";
        $('#ReceiverEmail').focus();
    }
    else {
        _divReceiverEmailId.style.display = "block";
        _divReceiverEmail.style.display = "none";
        $('#ReceiverEmailId').focus();
    }
});

var SendMailPaymentInvoice = function (Id) {
    if (!$("#frmShare").valid()) {
        return;
    }

    $("#btnSend").val("Sending...");
    $('#btnSend').attr('disabled', 'disabled');
    var _PreparedfrmShareObj = PreparedfrmShareObj();

    $("#btnSend").LoadingOverlay("show", {
        background: "rgba(165, 190, 100, 0.5)"
    });
    $("#btnSend").LoadingOverlay("show");

    $.ajax({
        type: "POST",
        url: "/PaymentShare/SendMailPaymentInvoice",
        data: _PreparedfrmShareObj,
        processData: false,
        contentType: false,
        success: function (result) {
            $("#btnSend").LoadingOverlay("hide", true);

            Swal.fire({
                title: result.AlertMessage,
                icon: "success"
            }).then(function () {
                $("#btnSend").val("Send");
                $('#btnSend').removeAttr('disabled');

                document.getElementById("btnClose").click();
            });
        },
        error: function (errormessage) {
            $("#btnSend").val("Send");
            $('#btnSend').removeAttr('disabled');           
            errormessage = '<p align="left">'+ errormessage.responseText +'</p>';
            Swal.fire({ title: errormessage.responseText, icon: 'warning' });
        }
    });
}

var PreparedfrmShareObj = function () {
    var _FormData = new FormData()
    _FormData.append('InvoiceId', $("#InvoiceId").val())
    _FormData.append('IsHideCompanyInfo', $("#IsHideCompanyInfo").val())
    _FormData.append('InvoiceDocType', $("#InvoiceDocType").val())

    _FormData.append('SenderEmailId', $("#SenderEmailId").val())
    _FormData.append('Subject', $("#Subject").val())
    var _Body = $('#Body').val().replace(/(\r\n|\n|\r)/gm, '<br />');
    _FormData.append('Body', _Body)

    if ($('#chkTypeReceiverEmail').is(":checked")) {
        _FormData.append('ReceiverEmail', $("#ReceiverEmail").val())
    }
    else {
        _FormData.append('ReceiverEmail', $("#ReceiverEmailId option:selected").text())
    }
    return _FormData;
}


var EmailConfig = function () {
    var _SenderEmailId = $("#SenderEmailId").val();
    $('#titleEmailConfigModal').html("Edit Email Config");

    var url = "/EmailConfig/AddEdit?id=" + _SenderEmailId;
    $("#bodyEmailConfig").load(url, function () {
        $("#EmailConfigModal").modal("show");
    });
}

var closeEmailConfig = function () {
    $('#EmailConfigModal').modal('hide');
}