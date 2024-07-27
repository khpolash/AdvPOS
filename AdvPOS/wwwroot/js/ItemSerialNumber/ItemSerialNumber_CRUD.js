var CountItemSerialNumber = function (id) {
    var result = null;
    $.ajax({
        type: "GET",
        url: "/ItemSerialNumber/CountItemSerialNumber?PaymentDetailId=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            if (data === null) return;
            result = data;
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });

    return result;
}

function Add(_ItemSerialNumberModel) {
    var result = null;
    $.ajax({
        type: "POST",
        url: "/ItemSerialNumber/Add",
        data: _ItemSerialNumberModel,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data === null) return;
            result = data;
            toastr.success("Item Serial Number added successfully. Serial Number: " + data.SerialNumber, 'Success');
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });

    return result;
}



var AddItemSerialNumber = function (button) {
    var row = $(button).closest("TR");
    var _PaymentDetailId = $("TD", row).eq(0).html();

    var _Quantity = $("#Quantity" + _PaymentDetailId).val();
    var _CountItemSerialNumber = CountItemSerialNumber(_PaymentDetailId);

    if (parseFloat(_Quantity) == parseFloat(_CountItemSerialNumber)) {
        SwalSimpleAlert("Item Serial Number Alredy Added", "info");
        return;
    }

    AddSerialNumberPopup(_PaymentDetailId);
}


var AddSerialNumberPopup = function (_PaymentDetailId) {
    Swal.fire({
        title: 'Please Enter Serial Number for Item',
        input: 'text',
        //inputValue: _inputValue,
        inputAttributes: {
            autocapitalize: 'off'
        },
        showCancelButton: true,
        confirmButtonText: 'Add',
        showLoaderOnConfirm: true,
        preConfirm: (result) => {
            if (result == '') {
                Swal.showValidationMessage(
                    'Empty not allowed.' + result
                )
            }
            else {
                var _ItemName = $("#ItemName" + _PaymentDetailId).val();
                var _IMENO = _ItemName + ' (Serial No.' + result + ')' + ' ';
                $("#ItemName" + _PaymentDetailId).val(_IMENO);

                var _ItemName = $("#ItemName" + _PaymentDetailId).val();
                var PaymentDetailCRUDViewModel = {};
                PaymentDetailCRUDViewModel.Id = _PaymentDetailId;
                PaymentDetailCRUDViewModel.ItemName = _ItemName;
                UpdatePaymentDetailIMENo(PaymentDetailCRUDViewModel);

                var ItemSerialNumber = {};
                ItemSerialNumber.PaymentDetailId = _PaymentDetailId;
                ItemSerialNumber.SerialNumber = result;
                var result = Add(ItemSerialNumber);

                //Open pop up again
                var _Quantity = $("#Quantity" + _PaymentDetailId).val();
                var _CountItemSerialNumber = CountItemSerialNumber(_PaymentDetailId);
                if (parseFloat(_Quantity) > parseFloat(_CountItemSerialNumber)) {
                    AddSerialNumberPopup(_PaymentDetailId);
                }               
            }

        },
        allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire({
                title: 'Alert',
                imageUrl: result.value.avatar_url
            })
        }
    })
}

function UpdatePaymentDetailIMENo(PaymentDetailCRUDViewModel) {
    $.ajax({
        type: "POST",
        url: "/Payment/UpdatePaymentDetailIMENo",
        data: PaymentDetailCRUDViewModel,
        dataType: "json",
        success: function (result) {
            //toastr.success("Update item successfully. Item ID: " + result.Id, 'Success');
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}