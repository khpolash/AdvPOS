$(document).ready(function () {
    document.title = 'Item Cart';

    $("#tblItemList").DataTable({
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

        "ajax": {
            "url": "/ItemCart/GetDataTableItemCartList",
            "type": "POST",
            "datatype": "json"
        },

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
            },
            {
                data: null, render: function (data, type, row) {
                    if (row[3].Name != 'Empty') {
                        var _GetItemDivTemplate = GetItemDivTemplate(row[3]);
                        return _GetItemDivTemplate;
                    }
                    else {
                        return "<div class='info-box col-12 col-sm-12'></div>"
                    }
                }
            }
        ],
        'columnDefs': [{
            'targets': [0, 1, 2, 3],
            'orderable': false,
        }],
        "lengthMenu": [[5, 10, 15, 20, 25, 50, 100, 200], [5, 10, 15, 20, 25, 50, 100, 200]]
    });

});

