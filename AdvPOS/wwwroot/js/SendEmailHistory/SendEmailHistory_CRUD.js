var Details = function (id) {
    var url = "/SendEmailHistory/Details?id=" + id;
    $('#titleExtraBigModal').html("Send Email History Details");
    loadExtraBigModal(url);
};
