var ViewItem = function (Id) {
    var url = "/Items/ViewItem?Id=" + Id;
    $('#titleBigModal').html("Item Details ");
    loadBigModal(url);
};

var UpdateQuantity = function (id) {
    var url = "/Items/UpdateQuantity?id=" + id;
    $('#titleBigModal').html("Update Quantity. Item ID: " + id);
    loadBigModal(url);
};

var AddEdit = function (id) {
    var url = "/Items/AddEdit?id=" + id;
    if (id > 0) {
        $('#titleExtraBigModal').html("Edit Item");
    } else {
        $('#titleExtraBigModal').html("Add Item");
    }
    loadExtraBigModal(url);
};

var Save = function () {
    if (!$("#frmItem").valid()) {
        return;
    }

    $.ajax({
        type: "POST",
        url: "/Items/AddEdit",
        data: PreparedFormObj(),
        processData: false,
        contentType: false,
        success: function (result) {
            if (result.IsSuccess) {
                Swal.fire({
                    title: result.AlertMessage,
                    icon: "success"
                }).then(function () {
                    document.getElementById("btnClose").click();
                    if (result.CurrentURL == "/") {
                        setTimeout(function () {
                            $("#tblHome").load("/ #tblHome");
                        }, 1000);
                    } else if (result.CurrentURL == "/UserProfile/Index") {
                        $("#divUserProfile").load("/UserProfile/Index #divUserProfile");
                    } else {
                        $('#tblItem').DataTable().ajax.reload();
                    }
                });
            } else {
                Swal.fire({
                    title: result.AlertMessage,
                    icon: "warning"
                }).then(function () {
                    setTimeout(function () {
                        $('#Name').focus();
                    }, 400);
                });
            }
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var DeleteItem = function (id) {
    if (DemoUserAccountLockAll() == 1) return;
    
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
                url: "/Items/Delete?id=" + id,
                success: function (result) {
                    var message = "Item has been deleted successfully. Item Code: " + result.Code;
                    Swal.fire({
                        title: message,
                        icon: 'info',
                        onAfterClose: () => {
                            $('#tblItem').DataTable().ajax.reload();
                        }
                    });
                }
            });
        }
    });
};

var AddBulkItem = function () {
    $('#titleMediumModal').html("Add Bulk Item");
    loadMediumModal("/Items/AddBulkItem");
};

var SaveBulkItem = function () {
    if ($("#AttachmentFile").val() === "" || $("#AttachmentFile").val() === null) {
        Swal.fire({
            title: 'Attachment File field can not be null or empty.',
            icon: "warning",
            onAfterClose: () => {
                $("#AttachmentFile").focus();
            }
        });
        return;
    }


    var formData = new FormData()
    formData.append('AttachmentFile', $('#AttachmentFile')[0].files[0])

    $("#btnSaveBulkItem").val("Adding...");
    $('#btnSaveBulkItem').attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "/Items/SaveBulkItem",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            Swal.fire({
                title: result + ', Item added',
                type: "success",
                onAfterClose: () => {
                    $("#AttachmentFile").val(null);
                    $("#btnSaveBulkItem").val("Add Bulk Item");
                    $('#btnSaveBulkItem').removeAttr('disabled');

                    $("#AttachmentFile").focus();
                }
            });
        }
    });
};


var UpdateBarcode = function () {
    var _Code = $("#Code").val();
    if (_Code.length > 10) {
        FieldValidationAlert('#Code', 'Max lenght is 10.', "warning");
        return;
    }

    Swal.fire({
        title: 'Barcode Updated',
        icon: "success"
    }).then(function () {
        $("#Barcode").JsBarcode(_Code);
    });
}


var PreparedFormObj = function () {
    var _FormData = new FormData()
    _FormData.append('Id', $("#Id").val())
    _FormData.append('CreatedDate', $("#CreatedDate").val())
    _FormData.append('CreatedBy', $("#CreatedBy").val())
    _FormData.append('Name', $("#Name").val())
    _FormData.append('SupplierId', $("#SupplierId").val())
    _FormData.append('MeasureId', $("#MeasureId").val())
    _FormData.append('MeasureValue', $("#MeasureValue").val())

    _FormData.append('CostPrice', $("#CostPrice").val())
    _FormData.append('NormalPrice', $("#NormalPrice").val())
    _FormData.append('TradePrice', $("#TradePrice").val())
    _FormData.append('PremiumPrice', $("#PremiumPrice").val())
    _FormData.append('OtherPrice', $("#OtherPrice").val())

    _FormData.append('CostVAT', $("#CostVAT").val())
    _FormData.append('NormalVAT', $("#NormalVAT").val())
    _FormData.append('TradeVAT', $("#TradeVAT").val())
    _FormData.append('PremiumVAT', $("#PremiumVAT").val())
    _FormData.append('OtherVAT', $("#OtherVAT").val())


    _FormData.append('OldUnitPrice', $("#OldUnitPrice").val())
    _FormData.append('OldSellPrice', $("#OldSellPrice").val())
    _FormData.append('Quantity', $("#Quantity").val())
    _FormData.append('CategoriesId', $("#CategoriesId").val())
    _FormData.append('WarehouseId', $("#WarehouseId").val())
    _FormData.append('Note', $("#Note").val())
    _FormData.append('UpdateQntType', $("#UpdateQntType").val())

    _FormData.append('UpdateQntNote', $("#UpdateQntNote").val())
    _FormData.append('StockKeepingUnit', $("#StockKeepingUnit").val())
    _FormData.append('ManufactureDate', $("#ManufactureDate").val())
    _FormData.append('ExpirationDate', $("#ExpirationDate").val())
    _FormData.append('Code', $("#Code").val())
    _FormData.append('Barcode', $('#Barcode').attr('src'))
    _FormData.append('ProductLevel', $("#ProductLevel").val())
    _FormData.append('VatPercentage', $("#VatPercentage").val())
    _FormData.append('ImageURLDetails', $('#ImageURLDetails')[0].files[0])
    return _FormData;
}

var ViewItemHistory = function (ItemId) {
    var url = "/ItemsHistory/ViewItemHistory?ItemId=" + ItemId;
    $('#titleExtraBigModal').html("Item History. Item ID: " + ItemId);
    loadExtraBigModal(url);
};