let IsDemoUser = 0;
let IsAddress = 1;
var DemoUserAccountLockAll = function () {
    if (IsDemoUser == 1) {
        SwalSimpleAlert("You are not allowed to change demo user info", "warning");
        return IsDemoUser;
    }
    else {
        return IsDemoUser;
    }
}

var SetHeaderElements = function (ElementName) {
    if (IsAddress == 1) {
        var _WhatsAppElement = WhatsAppElement();
        var _BuyNowElement = BuyNowElement();
        $('#' + ElementName).empty().append(_BuyNowElement + _WhatsAppElement);
    }
}

var SetLoginInfo = function (divName) {
    if (IsAddress == 1) {
        var _GetLoginInfoElement = GetLoginInfoElement();
        var _GetContactDetails = GetContactDetails();
        $('#' + divName).empty().append(_GetLoginInfoElement + _GetContactDetails);
    }
}

var SetFooterElements = function (divName) {
    if (IsAddress == 1) {
        var _CopyrightElement = CopyrightElement();
        var _GetContactDetails = GetContactDetails();
        $('#' + divName).empty().append(_GetContactDetails + _CopyrightElement);
    }
}


var GetContactDetails = function () {
    var content = '';
    var _WhatsAppElement = WhatsAppElement();
    content += 'If you want to modify or add new features, you can always contact us at, <br />'
        + _WhatsAppElement + '<br />'
        + '📢 Telegram: <a href="#" id="linkTelegram" onclick="CopyToClipboard(this)">shahed71bd</a><br />'
        + '📢 Skype: <a href="#" id="linkSkype" onclick="CopyToClipboard(this)">shahedatomap</a><br />'
        + '🙏 Email: <a href="#" id="linkEmail" onclick="CopyToClipboard(this)">shahedbddev@gmail.com</a><br />'
        + '♾️ Web: <a href="https://netspeedm.com/" target="_blank">Zero Byte Solutions</a>';

    return content;
}

var CopyrightElement = function () {
    var elements = '';
    elements += '<hr /><strong>Copyright &copy; 2021-2022 '
        + '<a href="https://github.com/shahedbd" target="_blank">Zero Byte</a>. </strong>'
        + 'All rights reserved'
        + '<a href="https://www.youtube.com/channel/UCdHAVwuNUtfqZRFVI6qf7mg?sub_confirmation=1" target="_blank">'
        + '<img src="/images/dashboard/scnow2.png" class="imgCustom150px" alt="User Image">'
        + '</a>'
        + '';
    return elements;
}

var WhatsAppElement = function () {
    var elements = '';
    elements = '<span>📲 WhatsApp: </span><a href="https://wa.me/8801674411603" target="_blank">8801674411603</a>';
    return elements;
}

var BuyNowElement = function () {
    var elements = '';
    elements += '<a href="https://1.envato.market/AoMzyj" target="_blank">'
        + '<img src="/images/dashboard/buynow.png" class="imgCustom150px" alt="User Image">'
        + '</a>';
    return elements;
}

var CopyToClipboard = function (Element) {
    var _Element = $('#' + Element.id).html();
    navigator.clipboard.writeText(_Element);
    toastr.success('Address copy to clipboard, successfully.');
}


var GetLoginInfoElement = function () {
    var elements = '';
    elements += ''
        + '<h6 class="ms-1">Login Info</h6>'
        + '<table border="1">'
        + '<thead><tr><th>Email/Password</th><th></th></tr></thead>'
        + '<tbody><tr>'
        + '<td><label id="lblEmail">admin@gmail.com</label>/<label id="lblPassword">123</label></td>'
        + '<td><button class="btn btn-sm btn-info" onclick="GetLoginCred()">Copy</button>'
        + '<a href="#" onclick="GetLoginCredClear()">Clear</a></td>'
        + '</tr></tbody>'
        + '</table>'
        + '<br /><br />';
    return elements;
}

var GetLoginCred = function () {
    var _lblEmail = $("#lblEmail").text();
    var _lblPassword = $("#lblPassword").text();
    $("#Email").val(_lblEmail);
    $("#Password").val(_lblPassword);
}

var GetLoginCredClear = function () {
    $("#Email").val('');
    $("#Password").val('');
    $("#Email").focus();
}



