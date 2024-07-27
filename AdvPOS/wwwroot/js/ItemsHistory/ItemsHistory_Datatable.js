$(document).ready(function () {
    document.title = 'Items History List';

    $("#tblItemsHistory").DataTable({
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
            "url": "/ItemsHistory/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            {
                data: "ItemId", "name": "ItemId", render: function (data, type, row) {
                    return "<a href='#' onclick=ViewItemHistory('" + row.ItemId + "');>History-" + row.ItemId + "</a>";
                }
            },
            {
                data: "ItemName", "name": "ItemName", render: function (data, type, row) {
                    return "<a href='#' onclick=ViewItem('" + row.ItemId + "');>" + row.ItemName + "</a>";
                }
            },         
            { "data": "CostPrice", "name": "CostPrice", "autoWidth": true },
            { "data": "NormalPrice", "name": "NormalPrice", "autoWidth": true },
            { "data": "OldUnitPrice", "name": "OldUnitPrice" },
            { "data": "OldQuantity", "name": "OldQuantity", "autoWidth": true },
            { "data": "NewQuantity", "name": "NewQuantity", "autoWidth": true },
            { "data": "TranQuantity", "name": "TranQuantity", "autoWidth": true },
            { "data": "Action", "name": "Action", "autoWidth": true },
            {
                "data": "CreatedDate",
                "name": "CreatedDate",
                "autoWidth": true,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
        ],
        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]],

        "columnDefs": [{
            "width": 50, targets: 0,
            "width": 50, targets: 1,
            "width": 50, targets: 5,
            "width": 50, targets: 6,
            "width": 50, targets: 7,
        }],

        fixedColumns: true
    });

});
