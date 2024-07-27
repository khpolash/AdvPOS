var lat;
var long;
var SetGeolocation = function () {
    if (navigator.geolocation) {
        parseFloat(navigator.geolocation.getCurrentPosition(showPosition));
    } else {
        lat = "Geolocation is not supported by this browser.";
        long = "Geolocation is not supported by this browser.";
    }
    function showPosition(position) {
        lat = position.coords.latitude;
        long = position.coords.longitude;
    }
};


var LoginAction = function () {
    if (!$("#frmLogin").valid()) {
        return;
    }
    $('#Latitude').val(lat);
    $('#Longitude').val(long);

    var _frmLogin = $("#frmLogin").serialize();
    $("#btnUserLogin").LoadingOverlay("show", {
        background: "rgba(165, 190, 100, 0.5)"
    });
    $("#btnUserLogin").LoadingOverlay("show");

    $.ajax({
        type: "POST",
        url: "/Account/Login",
        data: _frmLogin,
        success: function (result) {
            //console.log(result); result: IsLockedOut: false, IsNotAllowed: false, RequiresTwoFactor: false, Succeeded: true
            if (result.IsSuccess) {
                SaveUserInfoFromBrowser();
                location.href = "/Dashboard/Index";
            }
            else {
                toastr.error(result.AlertMessage);
                $("#btnUserLogin").LoadingOverlay("hide", true);
            }
        },
        error: function (errormessage) {
            $("#btnUserLogin").LoadingOverlay("hide", true);
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}


var SignOutAction = function () {
    $('#Latitude').val(lat);
    $('#Longitude').val(long);

    var _UserProfileId = sessionStorage.getItem("UserProfileId");
    var _Email = sessionStorage.getItem("Email");

    var LoginViewModel = {};
    LoginViewModel.Latitude = lat;
    LoginViewModel.Longitude = long;
    LoginViewModel.UserProfileId = _UserProfileId;
    LoginViewModel.Email = _Email;

    $("#btnSignOut").LoadingOverlay("show", {
        background: "rgba(165, 190, 100, 0.5)"
    });
    $("#btnSignOut").LoadingOverlay("show");
    $.ajax({
        type: "POST",
        url: "/Account/UserSignOut",
        data: LoginViewModel,
        dataType: "json",
        success: function (result) {
            if (result == true) {
                location.href = "/Account/Login";
            }
            else {
                $("#btnSignOut").LoadingOverlay("hide", true);
            }
        },
        error: function (errormessage) {
            $("#btnSignOut").LoadingOverlay("hide", true);
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var SaveUserInfoFromBrowser = function () {
    var parser = new UAParser();
    var UserInfoFromBrowser = {};
    var _GetBrowserUniqueID = GetBrowserUniqueID();

    UserInfoFromBrowser.BrowserUniqueID = _GetBrowserUniqueID;
    UserInfoFromBrowser.Lat = lat;
    UserInfoFromBrowser.Long = long;
    UserInfoFromBrowser.TimeZone = new Date();
    UserInfoFromBrowser.BrowserMajor = parser.getResult().browser.major;
    UserInfoFromBrowser.BrowserName = parser.getResult().browser.name;
    UserInfoFromBrowser.BrowserVersion = parser.getResult().browser.version;

    UserInfoFromBrowser.CPUArchitecture = parser.getResult().cpu.architecture;
    UserInfoFromBrowser.DeviceModel = parser.getResult().device.model;
    UserInfoFromBrowser.DeviceType = parser.getResult().device.type;
    UserInfoFromBrowser.DeviceVendor = parser.getResult().device.vendor;

    UserInfoFromBrowser.EngineName = parser.getResult().engine.name;
    UserInfoFromBrowser.EngineVersion = parser.getResult().engine.version;
    UserInfoFromBrowser.OSName = parser.getResult().os.name;
    UserInfoFromBrowser.OSVersion = parser.getResult().os.version;
    UserInfoFromBrowser.UA = parser.getResult().ua;

    var _UserEmailAddress = $('#UserEmailAddress').val();
    UserInfoFromBrowser.CreatedBy = _UserEmailAddress;
    UserInfoFromBrowser.ModifiedBy = _UserEmailAddress;

    $.ajax({
        type: "POST",
        url: "/Account/SaveUserInfoFromBrowser",
        data: UserInfoFromBrowser,
        dataType: "json",
        success: function (result) {
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
};

var GetBrowserUniqueID = function () {
    var navigator_info = window.navigator;
    var screen_info = window.screen;
    var uid = navigator_info.mimeTypes.length;
    uid += navigator_info.userAgent.replace(/\D+/g, '');
    uid += navigator_info.plugins.length;
    uid += screen_info.height || '';
    uid += screen_info.width || '';
    uid += screen_info.pixelDepth || '';
    return uid;
}
