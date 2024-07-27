var SMTPEmailSettingDetails = function (id) {
    var url = "/EmailSetting/SMTPEmailSettingDetails?id=" + id;
    $('#titleBigModal').html("SMTP Email Setting Details ");
    loadBigModal(url);
};

var SMTPEmailSettingAddEdit = function (id) {
    var url = "/EmailSetting/SMTPEmailSettingAddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edit SMTP Email Setting ");
    }
    else {
        $('#titleMediumModal').html("Add SMTP Email Setting ");
    }
    loadMediumModal(url);
};


var SendGridSettingDetails = function (id) {
    var url = "/EmailSetting/SendGridSettingDetails?id=" + id;
    $('#titleBigModal').html("SendGrid Setting Details ");
    loadBigModal(url);
};

var SendGridSettingAddEdit = function (id) {
    var url = "/EmailSetting/SendGridSettingAddEdit?id=" + id;
    if (id > 0) {
        $('#titleMediumModal').html("Edit SendGrid Setting");
    }
    else {
        $('#titleMediumModal').html("Add SendGrid Setting ");
    }
    loadMediumModal(url);
};

var SaveSMTP = function () {
    if (!$("#frmSMTPEmailSettingAddEdit").valid()) {
        return;
    }
    var _frmSMTPEmailSettingAddEdit = $("#frmSMTPEmailSettingAddEdit").serialize();
    $.ajax({
        type: "POST",
        url: "/EmailSetting/SMTPEmailSettingAddEdit",
        data: _frmSMTPEmailSettingAddEdit,
        success: function (result) {
            Swal.fire({
                title: result,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                setTimeout(function () {
                    $("#tblSMTP").load("/EmailSetting/Index #tblSMTP");
                }, 100);
            });
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var SaveSendGrid = function () {
    if (!$("#frmSendGridSettingAddEdit").valid()) {
        return;
    }
    var _frmSendGridSettingAddEdit = $("#frmSendGridSettingAddEdit").serialize();
    $.ajax({
        type: "POST",
        url: "/EmailSetting/SendGridSettingAddEdit",
        data: _frmSendGridSettingAddEdit,
        success: function (result) {
            Swal.fire({
                title: result,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                setTimeout(function () {
                    $("#tblSendGrid").load("/EmailSetting/Index #tblSendGrid");
                }, 100);
            });
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}
