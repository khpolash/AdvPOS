var GetItemByCategories = function () {
    var _CategoriesId = $("#CategoriesId").val();
    //SwalSimpleAlert(_CategoriesId, "info");
    //$('#tblItemCartSideInvoice').DataTable().ajax.reload();       
    $.ajax({
        type: "GET",
        url: "/ItemCart/GetItemByCategories?CategoriesId=" + _CategoriesId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        //contentType: "application/json; charset=utf-8",
        datatype: "json",
        success: function (data) {
            console.log(data);
            LoadDataTable(data);
        },
        error: function (response) {
            SwalSimpleAlert(response.responseText, "warning");
        }
    });
}


var LoadDataTable = function (jsondata) {
    $("#tblItemCartSideInvoice").DataTable({
        paging: true,
        select: true,
        "order": [[0, "desc"]],
        dom: 'Bfrtip',

        buttons: [
            'pageLength',
        ],

        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "stateSave": true,

        destroy: true,
        data: jsondata,

        "columns": [
            {
                data: null, render: function (data, type, row) {
                    var _GetItemDivTemplate = GetItemDivTemplate(row[0]);
                    return _GetItemDivTemplate;
                }
            },
            {
                data: null, render: function (data, type, row) {
                    if (row[1].Name != 'Empty') {
                        var _GetItemDivTemplate = GetItemDivTemplate(row[1]);
                        return _GetItemDivTemplate;
                    }
                    else {
                        return "<div class='info-box col-12 col-sm-12'></div>"
                    }
                }
            },
            {
                data: null, render: function (data, type, row) {
                    if (row[2].Name != 'Empty') {
                        var _GetItemDivTemplate = GetItemDivTemplate(row[2]);
                        return _GetItemDivTemplate;
                    }
                    else {
                        return "<div class='info-box col-12 col-sm-12'></div>"
                    }
                }
            }
        ],
        'columnDefs': [{
            'targets': [0, 1, 2],
            'orderable': false,
        }],
        "lengthMenu": [[3, 5, 10, 15, 20, 25, 50, 100, 200], [3, 5, 10, 15, 20, 25, 50, 100, 200]]
    });
}
