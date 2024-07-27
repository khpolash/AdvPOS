var AddNewCustomer = function() {
    activaTab('divAddNewCustomer');
};

var SaveCustomerInfo = function() {
    if (!FieldValidation('#Name')) {
        FieldValidationAlert('#Name', 'Name is Required.', "warning");
        return;
    }

    var CustomerInfoCRUDViewModel = {
        Name: $("#Name").val(),
        CompanyName: $("#CompanyName").val(),
        Type: $("#Type").val(),
        Phone: $("#Phone").val(),
        Email: $("#Email").val(),
        Address: $("textarea#Address").val(),
        AddressPostcode: $("#AddressPostcode").val(),
        BillingAddress: $("textarea#BillingAddress").val(),
        BillingAddressPostcode: $("#BillingAddressPostcode").val(),
        UseThisAsBillingAddress: $("#UseThisAsBillingAddress").val(),
        Notes: $("#Notes").val(),
    };

    $.ajax({
        type: "POST",
        url: "/CustomerInfo/AddEdit",
        data: CustomerInfoCRUDViewModel,
        success: function(result) {
            $('#CustomerId').append($('<option>', {
                value: result.Id,
                text: result.Name
            }));
            $('#CustomerId').val(result.Id);
            GetCustomerHistory(result.Id);           
            toastr.success(result.AlertMessage, 'Success');
        },
        error: function(errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
    activaTab('divBasicInfo');
};

var AddNewSupplier = function() {
    activaTab('divAddNewCustomer');
};

var SaveSupplier = function() {
    if (!FieldValidation('#Name')) {
        FieldValidationAlert('#Name', 'Name is Required.', "warning");
        return;
    }

    var SupplierCRUDViewModel = {
        Name: $("#Name").val(),
        ContactPerson: $("#ContactPerson").val(),
        Email: $("#Email").val(),
        Phone: $("#Phone").val(),
        Address: $("#Address").val(),
    };

    $.ajax({
        type: "POST",
        url: "/Supplier/AddEdit",
        data: SupplierCRUDViewModel,
        success: function(result) {
            $('#SupplierId').append($('<option>', {
                value: result.Id,
                text: result.Name
            }));
            $('#SupplierId').val(result.Id);           
            toastr.success(result.AlertMessage, 'Success');
        },
        error: function(errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
    activaTab('divBasicInfo');
};
