var ViewItem = function (ItemId) {
    var url = "/Items/ViewItem?Id=" + ItemId;
    $('#titleBigModal').html("Item Details ");
    loadBigModal(url);
};

var ViewItemHistory = function (ItemId) {
    var url = "/ItemsHistory/ViewItemHistory?ItemId=" + ItemId;
    $('#titleExtraBigModal').html("Item History. Item ID: " + ItemId);
    loadExtraBigModal(url);
};