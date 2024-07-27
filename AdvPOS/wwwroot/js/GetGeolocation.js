
var lat;
var long;

var GetGeolocation = function () {
    $("#btnUserLogin").prop("disabled", true);
    setTimeout(function () {
        $("#btnUserLogin").removeAttr('disabled');
    }, 1000);

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

var SendGeolocation = function () {
    $('#Latitude').val(lat);
    $('#Longitude').val(long);
    SaveUserInfoFromBrowser();
    
    //btnUserLogin
    $("#btnUserLogin").LoadingOverlay("show", {
        background: "rgba(165, 190, 100, 0.5)"
    });
    $("#btnUserLogin").LoadingOverlay("show");
};

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
            //console.log(result)                   
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
};

var GetBrowserUniqueID = function (){
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
