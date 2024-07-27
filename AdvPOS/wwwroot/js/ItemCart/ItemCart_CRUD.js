var AddtoCart = function (_ItemId) {
  var TempItem = listPaymentDetail.filter(
    (item) => item.Id == parseFloat(_ItemId)
  );

  var TempItemDTO = {};
  TempItemDTO.ItemId = TempItem[0].Id;
  TempItemDTO.ItemName = TempItem[0].Name;
  TempItemDTO.Quantity = 1;
  TempItemDTO.UnitPrice = TempItem[0].SellPrice;
  TempItemDTO.ItemVAT = TempItem[0].NormalVAT;
  TempItemDTO.TotalAmount = TempItem[0].SellPrice;

  var _TotalPriceWithVAT = TempItem[0].SellPrice + (TempItem[0].NormalVAT / 100) * TempItem[0].SellPrice;
  TempItemDTO.TotalPriceWithVAT = _TotalPriceWithVAT;

  var IsAddNewColumn = true;
  if (listItemCart.length > 0) {
    for (let i = 0; i < listItemCart.length; i++) {
      if (listItemCart[i].ItemId === parseFloat(_ItemId)) {             
        var _Quantity = $("#Quantity" + _ItemId).val();
        $("#Quantity" + listItemCart[i].ItemId).val(parseFloat(_Quantity) + 1);
        _Quantity = $("#Quantity" + _ItemId).val();
        
        $("#TotalAmount" + listItemCart[i].ItemId).html((_TotalPriceWithVAT * _Quantity).toFixed(2));       
        listItemCart[i].Quantity = _Quantity;
        listItemCart[i].TotalAmount = _Quantity * listItemCart[i].UnitPrice;
        listItemCart[i].TotalPriceWithVAT = _Quantity * _TotalPriceWithVAT;
        SubAndGrandTotal();
        localStorage.setItem("listItemCart", JSON.stringify(listItemCart));

        IsAddNewColumn = false;
        toastr.options.positionClass = "toast-bottom-right";
        toastr.success(
          "Item added successfully. Item Id: " +
            _ItemId +
            ", Total Quantity: " +
            listItemCart[i].Quantity
        );
        return;
      }
    }
  }

  if (IsAddNewColumn) {
    listItemCart.push(TempItemDTO);
    AddTableRow(TempItemDTO, "#tblItemCart");
  }

  SubAndGrandTotal();
  $("#shoppingcart").html(listItemCart.length);
  toastr.options.positionClass = "toast-bottom-right";
  toastr.success("Item added successfully. Item Id: " + _ItemId);
  localStorage.setItem("listItemCart", JSON.stringify(listItemCart));
};

var ItemCartDetail = function () {
  $("#ItemCartModal").modal("show");
};

var ItemCheckout = function () {
  if (listItemCart.length < 1) {
    FieldValidationAlert(
      "#tblItemCart",
      "Please add at least one item.",
      "info"
    );
    return;
  }

  $("#btnCheckout").val("Creating Invoice...");
  $("#btnCheckout").attr("disabled", "disabled");

  $("#btnCheckout").LoadingOverlay("show", {
    background: "rgba(165, 190, 100, 0.5)",
  });
  $("#btnCheckout").LoadingOverlay("show");
  $.ajax({
    ContentType: "application/json; charset=utf-8",
    dataType: "json",
    type: "POST",
    url: "/ItemCart/CreateDraftItemCart",
    data: { listPaymentDetail: listItemCart },
    success: function (result) {
      $("#btnCheckout").LoadingOverlay("hide", true);
      $("#btnCheckout").val("Checkout");
      $("#btnCheckout").removeAttr("disabled");

      ClearAllCartItem();
      var url = "/Payment/AddEdit?id=" + result.Id;
      $("#titleExtraBigModal").html("Add Payment");
      loadExtraBigModal(url);
    },
    error: function (errormessage) {
      $("#btnCheckout").val("Send");
      $("#btnCheckout").removeAttr("disabled");
      SwalSimpleAlert(errormessage.responseText, "warning");
    },
  });
};

$("#ItemSearch").keyup(function () {
  var filter = $(this).val(),
    count = 0;
  $("#results div").each(function () {
    if ($(this).text().search(new RegExp(filter, "i")) < 0) {
      $(this).hide();
    } else {
      $(this).show();
      count++;
    }
  });
});

var GetByItemTest = function () {
  var _ItemSearch = $("#ItemSearch").val();
  $.ajax({
    type: "GET",
    url: "/Items/CartItemBy?_Name=" + _ItemSearch,
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
      $("#divItemList").load(location.href + "#divItemList");
    },
    error: function (errormessage) {
      SwalSimpleAlert(errormessage.responseText, "warning");
    },
  });
};

var AddTableRow = function (item, TableName) {
  var tBody = $(TableName + " > TBODY")[0];
  var row = tBody.insertRow(-1);

  var cell = $(row.insertCell(-1));
  cell.html(item.ItemId);

  var cell = $(row.insertCell(-1));
  cell.html(item.ItemName);

  var cell = $(row.insertCell(-1));
  var txtQuantity = $("<input />");
  txtQuantity.attr("style", "width:60px;");
  txtQuantity.attr("type", "number");
  txtQuantity.attr("min", "1");
  txtQuantity.attr("id", "Quantity" + item.ItemId);
  txtQuantity.attr("onclick", "UpdateQuantity(this);");
  txtQuantity.val(item.Quantity);
  cell.append(txtQuantity);

  var cell = $(row.insertCell(-1));
  cell.html(item.UnitPrice);

  var cell = $(row.insertCell(-1));
  var txtNormalVAT = $("<input />");
  txtNormalVAT.attr("style", "width:60px;");
  txtNormalVAT.attr("type", "number");
  txtNormalVAT.attr("min", "0");
  txtNormalVAT.attr("id", "ItemVAT" + item.ItemId);
  txtNormalVAT.attr("onclick", "UpdateNormalVAT(this);");
  txtNormalVAT.val(item.ItemVAT);
  cell.append(txtNormalVAT);

  var cell = $(row.insertCell(-1));
  var txtTotalAmount = $("<span />");
  txtTotalAmount.attr("id", "TotalAmount" + item.ItemId);
  txtTotalAmount.html(item.TotalPriceWithVAT.toFixed(2));
  cell.append(txtTotalAmount);

  cell = $(row.insertCell(-1));
  var btnRemove = $("<input />");
  btnRemove.attr("type", "button");
  btnRemove.attr("class", "btn btn-danger btn-xs");
  btnRemove.attr("onclick", "Remove(this);");
  btnRemove.val(" X ");
  cell.append(btnRemove);
};

function Remove(button) {
  var row = $(button).closest("TR");
  var table = $("#tblItemCart")[0];
  table.deleteRow(row[0].rowIndex);
  var _ItemId = $("TD", row).eq(0).html();

  for (let i = 0; i < listItemCart.length; i++) {
    if (listItemCart[i].ItemId === parseFloat(_ItemId)) {
      listItemCart.splice(i, 1);
    }
  }

  SubAndGrandTotal();
  toastr.success("Item removed successfully.");
  $("#shoppingcart").html(listItemCart.length);
  localStorage.setItem("listItemCart", JSON.stringify(listItemCart));
}

var ClearAllCartItem = function () {
  if (listItemCart.length < 1) {
    SwalSimpleAlert("Cart alredy empty.", "info");
    return;
  }
  localStorage.removeItem("listItemCart");
  listItemCart = [];
  $("#tblItemCart > tbody").empty();
  $("#shoppingcart").html(0);
  toastr.success("Cart clean successfully.");
};

var ClearAllCartItemWithAlert = function () {
  if (listItemCart.length < 1) {
    SwalSimpleAlert("Cart alredy empty.", "info");
    return;
  }
  Swal.fire({
    title: 'Are you sure to clear the current cart?',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes'
  }).then((result) => {
      if (result.value) {
        localStorage.removeItem("listItemCart");
        listItemCart = [];
        $("#tblItemCart > tbody").empty();
        $("#shoppingcart").html(0);
        toastr.success("Cart clean successfully.");
      }
  });
};

var GetAllCartItem = function () {
  $.ajax({
    type: "GET",
    url: "/ItemCart/GetAllCartItem",
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (result) {
      listPaymentDetail = result;
    },
    error: function (errormessage) {
      SwalSimpleAlert(errormessage.responseText, "warning");
    },
  });
};

var SubAndGrandTotal = function () {
  let _SubTotal = 0;
  let _GrandTotal = 0;
  for (let i = 0; i < listItemCart.length; i++) {
    _SubTotal = _SubTotal + listItemCart[i].TotalAmount;
    _GrandTotal = _GrandTotal + listItemCart[i].TotalPriceWithVAT;
  }
  $("#ItemChartSubTotal").text("Sub Total: " + _SubTotal.toFixed(2));
  $("#ItemChartGrandTotal").text("Grand Total: " + _GrandTotal.toFixed(2));
};

var UpdateQuantity = function (button) {
  CommonCal(button);
};

var UpdateNormalVAT = function (button) {
  CommonCal(button);
};

var CommonCal = function (button) {
  var row = $(button).closest("TR");
  var _ItemId = $("TD", row).eq(0).html();
  var _Quantity = $("#Quantity" + _ItemId).val();
  var _UnitPrice = $("TD", row).eq(3).html();
  var _NormalVAT = $("#ItemVAT" + _ItemId).val();

  var _TotalPrice = parseFloat(_Quantity) * parseFloat(_UnitPrice);
  var _TotalPriceWithVAT =
    parseFloat(_TotalPrice) +
    (parseFloat(_NormalVAT) / 100) * parseFloat(_TotalPrice);

  $("TD", row).eq(5).html(_TotalPriceWithVAT.toFixed(2));
  for (let i = 0; i < listItemCart.length; i++) {
    if (listItemCart[i].ItemId === parseFloat(_ItemId)) {
      listItemCart[i].Quantity = _Quantity;
      listItemCart[i].ItemVAT = _NormalVAT;
      listItemCart[i].TotalAmount = _TotalPrice;
      listItemCart[i].TotalPriceWithVAT = _TotalPriceWithVAT;
    }
  }
  localStorage.setItem("listItemCart", JSON.stringify(listItemCart));
  SubAndGrandTotal();
};
