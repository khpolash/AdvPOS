var GetDropDownDataBase = function (ItemName, _url) {
    $.ajax({
        type: "GET",
        url: _url,
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                var _ItemName = document.getElementById(ItemName);
                var _option = document.createElement("option");
                _option.text = data[i].Name;
                _option.value = data[i].Id;
                _ItemName.add(_option);
            }
        },
        error: function (response) {
            SwalSimpleAlert(response.responseText, "warning");
        }
    });
};

var LoadSelectTwoDropdown = function (ItemName, _url) {
    $('.' + ItemName + '').select2({
        ajax: {
            type: "GET",
            url: _url,
            dataType: 'json'
        },
    });
}
