var UpdateUnitPrice = function () {
    var _ItemId = $("#ItemId option:selected").text();

    var splitArray = _ItemId.split(":");
    GeneratePriceDropdown(splitArray);

    var _IsVat = $("#IsVat").val();
    var _ItemVAT;
    if (_IsVat == "Yes") {
        _ItemVAT = parseFloat(splitArray[10]);
        $("#ItemVAT").val(_ItemVAT);
    }
    else {
        $("#ItemVAT").val(0);
    }

    onchangeQuantity();
}

var GeneratePriceDropdown = function (splitArray) {
    document.getElementById("UnitPrice").options.length = 0;
    var _UnitPrice = $("#UnitPrice");
    var ItemPriceData = { "1": "Cost:" + splitArray[4], "2": "Normal:" + splitArray[2], "3": "Trade:" + splitArray[6], "4": "Premium:" + splitArray[8] };
    $.each(ItemPriceData, function (index, value) {
        $("<option/>", {
            value: index,
            text: value
        }).appendTo(_UnitPrice);
    });
}


var onchangeUnitPrice = function () {
    onchangeQuantity();
}

var onchangeQuantity = function () {
    var _Quantity = $("#Quantity").val();
    var _UnitPrice = $("#UnitPrice option:selected").text();
    var splitUnitPrice = _UnitPrice.split(":");
    _UnitPrice = splitUnitPrice[1];

    var _ItemDiscount = $("#ItemDiscount").val();
    var _ItemVAT = $("#ItemVAT").val();


    var _ItemDiscountAmount = (parseFloat(_ItemDiscount) / 100) * parseFloat(_UnitPrice);

    _UnitPrice = parseFloat(_UnitPrice) - _ItemDiscountAmount;
    _UnitPrice = parseFloat(_UnitPrice) + (parseFloat(_ItemVAT) / 100) * parseFloat(_UnitPrice);
    var _TotalAmount = parseFloat(_UnitPrice) * parseFloat(_Quantity);
    $("#TotalAmount").val(_TotalAmount.toFixed(2));
}

var onchangeItemDiscount = function () {
    onchangeQuantity();
}


var onkeydownChargeAmount = function () {
    if (event.keyCode == 13) {
        event.preventDefault();
        AddPaymentDetail();
    }
}


var AddPaymentsDetailsHTMLRow = function () {
    AddPaymentDetail();
    $("#ItemBarcode").val('');
}

var AddHTMLTableRow = function (result) {
    var tBody = $("#tblPaymentDetail > TBODY")[0];
    var row = tBody.insertRow(-1);

    var cell = $(row.insertCell(-1));
    cell.html(result.Id);

    var cell = $(row.insertCell(-1));
    var _ItemName = $("#ItemId option:selected").text();
    var splitArray = _ItemName.split(":");
    var inputItemName = $("<textarea />");
    inputItemName.attr("rows", "1");
    inputItemName.attr('style', 'width:400px;');
    inputItemName.attr('id', 'ItemName' + result.Id);
    inputItemName.val(splitArray[0]);
    cell.append(inputItemName);


    var cell = $(row.insertCell(-1));
    var txtQuantity = $("<input />");
    txtQuantity.attr('style', 'width:70px;');
    txtQuantity.attr('type', 'number');
    txtQuantity.attr('min', '1');
    txtQuantity.attr('id', 'Quantity' + result.Id);
    txtQuantity.attr("onchange", "UpdateItemDynamicControl(this);");
    txtQuantity.val($("#Quantity").val());
    cell.append(txtQuantity);

    var cell = $(row.insertCell(-1));
    var _UnitPrice = $("#UnitPrice option:selected").text();
    var splitUnitPrice = _UnitPrice.split(":");
    _UnitPrice = splitUnitPrice[1];

    var txtUnitPrice = $("<input />");
    txtUnitPrice.attr('style', 'width:70px;');
    txtUnitPrice.attr('type', 'number');
    txtUnitPrice.attr('min', '1');
    txtUnitPrice.attr('id', 'UnitPrice' + result.Id);
    txtUnitPrice.attr("onchange", "UpdateItemDynamicControl(this);");
    txtUnitPrice.val(parseFloat(_UnitPrice));
    cell.append(txtUnitPrice);

    var _IsVat = $("#IsVat").val();
    if (_IsVat == "Yes") {
        var cell = $(row.insertCell(-1));
        cell.html($("#ItemVAT").val());
    }
    
    var cell = $(row.insertCell(-1));
    var txtItemDiscount = $("<input />");
    txtItemDiscount.attr('type', 'number');
    txtItemDiscount.attr('style', 'width:70px;');
    txtItemDiscount.attr('id', 'ItemDiscount' + result.Id);
    txtItemDiscount.attr("onchange", "UpdateItemDynamicControl(this);");
    txtItemDiscount.val($("#ItemDiscount").val());
    cell.append(txtItemDiscount);

    var cell = $(row.insertCell(-1));
    var txtTotalAmount = $("<label />");
    txtTotalAmount.attr('class', 'not-bold');
    txtTotalAmount.attr('id', 'TotalAmount' + result.Id);
    txtTotalAmount.text($("#TotalAmount").val());
    cell.append(txtTotalAmount);


    cell = $(row.insertCell(-1));
    var btnUpdate = $("<input />");
    btnUpdate.attr("type", "button");
    btnUpdate.attr('class', 'btn btn-success btn-xs');
    btnUpdate.attr('id', 'btnUpdatePaymentsDetail' + result.Id);
    btnUpdate.attr("onclick", "UpdatePaymentDetail(this);");
    btnUpdate.val("Update");
    cell.append(btnUpdate);

    var btnRemove = $("<input />");
    btnRemove.attr("type", "button");
    btnRemove.attr('class', 'btn btn-danger btn-xs');
    btnRemove.attr("onclick", "Remove(this);");
    btnRemove.val(" X ");
    cell.append(btnRemove);

    var btnAddIMENumber = $("<a />");
    btnAddIMENumber.attr('class', 'btn fa fa-id-card fa-3x');
    btnAddIMENumber.attr("onclick", "AddItemSerialNumber(this);");
    cell.append(btnAddIMENumber);

    $("#ItemId").focus();
    ClearInvoiceItemTableRowData();
}


function LoadTableRowFromDB(item, index) {
    var tBody = $("#tblPaymentDetail > TBODY")[0];
    var row = tBody.insertRow(-1);

    var cell = $(row.insertCell(-1));
    cell.html(item.Id);

    var cell = $(row.insertCell(-1));
    var inputItemName = $("<textarea />");
    inputItemName.attr("rows", "1");
    inputItemName.attr('style', 'width:450px;');
    inputItemName.attr('id', 'ItemName' + item.Id);
    inputItemName.val(item.ItemName);
    cell.append(inputItemName);

    var cell = $(row.insertCell(-1));
    var txtQuantity = $("<input />");
    txtQuantity.attr('style', 'width:70px;');
    txtQuantity.attr('type', 'number');
    txtQuantity.attr('min', '1');
    txtQuantity.attr('id', 'Quantity' + item.Id);
    txtQuantity.attr("onchange", "UpdateItemDynamicControl(this);");
    txtQuantity.val(item.Quantity);
    cell.append(txtQuantity);

    var cell = $(row.insertCell(-1));
    var txtUnitPrice = $("<input />");
    txtUnitPrice.attr('style', 'width:70px;');
    txtUnitPrice.attr('type', 'number');
    txtUnitPrice.attr('min', '1');
    txtUnitPrice.attr('id', 'UnitPrice' + item.Id);
    txtUnitPrice.attr("onchange", "UpdateItemDynamicControl(this);");
    txtUnitPrice.val(item.UnitPrice);
    cell.append(txtUnitPrice);

    var _IsVat = $("#IsVat").val();
    if (_IsVat == "Yes") {
        var cell = $(row.insertCell(-1));
        cell.html(item.ItemVAT);
    }   

    var cell = $(row.insertCell(-1));
    var txtItemDiscount = $("<input />");
    txtItemDiscount.attr('type', 'number');
    txtItemDiscount.attr('style', 'width:70px;');
    txtItemDiscount.attr('id', 'ItemDiscount' + item.Id);
    txtItemDiscount.attr("onchange", "UpdateItemDynamicControl(this);");
    txtItemDiscount.val(item.ItemDiscount);
    cell.append(txtItemDiscount);

    var cell = $(row.insertCell(-1));
    var txtTotalAmount = $("<label />");
    txtTotalAmount.attr('class', 'not-bold');
    txtTotalAmount.attr('id', 'TotalAmount' + item.Id);
    txtTotalAmount.text(item.TotalAmount);
    cell.append(txtTotalAmount);

    //Add Button cell.
    cell = $(row.insertCell(-1));
    var btnUpdate = $("<input />");
    btnUpdate.attr("type", "button");
    btnUpdate.attr('class', 'btn btn-success btn-xs');
    btnUpdate.attr('id', 'btnUpdatePaymentsDetail' + item.Id);
    btnUpdate.attr("onclick", "UpdatePaymentDetail(this);");
    btnUpdate.val("Update");
    cell.append(btnUpdate);

    var btnRemove = $("<input />");
    btnRemove.attr("type", "button");
    btnRemove.attr('class', 'btn btn-danger btn-xs');
    btnRemove.attr("onclick", "Remove(this);");
    btnRemove.val(" X ");
    cell.append(btnRemove);

    var btnAddIMENumber = $("<a />");
    btnAddIMENumber.attr('class', 'btn fa fa-id-card fa-3x');
    btnAddIMENumber.attr("onclick", "AddItemSerialNumber(this);");
    cell.append(btnAddIMENumber);
}


var ClearInvoiceItemTableRowData = function () {
    $("#PaymentsDetailsId").val("");
    $('#ItemId').val(0).trigger('change');
    $("#Quantity").val("1");
    $("#UnitPrice").val("");
    $("#ItemVAT").val("");
    $("#ItemDiscount").val(0);
    $("#TotalAmount").val("");
}

function Remove(button) {
    Swal.fire({
        title: 'Do you want to delete this item?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            var row = $(button).closest("TR");
            var table = $("#tblPaymentDetail")[0];
            table.deleteRow(row[0].rowIndex);
            var _PaymentDetailId = $("TD", row).eq(0).html();

            var item = {};
            item.Id = _PaymentDetailId;
            item.PaymentsId = $("#Id").val();
            DeletePaymentDetail(item);
        }
    });
}

var UpdateItemDynamicControl = function (button)
{
    var row = $(button).closest("TR");
    var _ItemId = $("TD", row).eq(0).html();
    var _Quantity = $("#Quantity" + _ItemId).val();
    var _UnitPrice = $("#UnitPrice" + _ItemId).val();   
    var _ItemDiscount = $("#ItemDiscount" + _ItemId).val();

    var _IsVat = $("#IsVat").val();
    var _NormalVAT;
    if (_IsVat == "Yes") {
        _NormalVAT = $("TD", row).eq(4).html();
    }
    else {
        _NormalVAT = 0;
    }

    var _TotalPrice = parseFloat(_Quantity) * parseFloat(_UnitPrice);
    var _DiscountTotalAmount = (_ItemDiscount/100) * _TotalPrice;
    var _DiscountedTotalPrice = parseFloat(_TotalPrice) - parseFloat(_DiscountTotalAmount);

    var _TotalPriceWithVAT = parseFloat(_DiscountedTotalPrice) + (parseFloat(_NormalVAT) / 100) * parseFloat(_DiscountedTotalPrice);
    
    if (_IsVat == "Yes") {
        $("TD", row).eq(6).html(_TotalPriceWithVAT.toFixed(2));
    }
    else {
        $("TD", row).eq(5).html(_TotalPriceWithVAT.toFixed(2));
    }
}