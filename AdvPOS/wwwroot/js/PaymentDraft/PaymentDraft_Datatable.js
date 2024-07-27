$(document).ready(function() {
    document.title = 'Payment Draft';

    $("#tblPaymentDraft").DataTable({
        paging: true,
        select: true,
        "order": [
            [0, "desc"]
        ],
        dom: 'Bfrtip',


        buttons: [
            'pageLength',
        ],


        "processing": true,
        "serverSide": true,
        "filter": true, //Search Box
        "orderMulti": false,
        "stateSave": true,

        "ajax": {
            "url": "/PaymentDraft/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },


        "columns": [{
                data: "Id",
                "name": "Id",
                render: function(data, type, row) {
                    return "<a href='#' onclick=Details('" + row.Id + "');>" + row.InvoiceNo + "</a>";
                }
            },
            {
                data: "CustomerId",
                "name": "CustomerId",
                render: function(data, type, row) {
                    return "<a href='#' onclick=ViewCustomerDetails('" + row.CustomerId + "');>" + row.CustomerName + "</a>";
                }
            },
            { "data": "Discount", "name": "Discount" },
            { "data": "VAT", "name": "VAT" },
            { "data": "SubTotal", "name": "SubTotal" },
            { "data": "GrandTotal", "name": "GrandTotal" },
            { "data": "PaidAmount", "name": "PaidAmount" },
            { "data": "DueAmount", "name": "DueAmount" },
            {
                "data": "CreatedDate",
                "name": "CreatedDate",
                "autoWidth": true,
                "render": function(data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            {
                data: null,
                render: function(data, type, row) {
                    return "<a href='#' class='btn btn-info btn-xs' onclick=AddEdit('" + row.Id + "');>Edit</a>";
                }
            },
            {
                data: null,
                render: function(data, type, row) {
                    return "<a href='#' class='btn btn-danger btn-xs' onclick=Delete('" + row.Id + "'); >Delete</a>";
                }
            }
        ],

        'columnDefs': [{
            'targets': [9, 10],
            'orderable': false,
        }],

        "lengthMenu": [
            [20, 10, 15, 25, 50, 100, 200],
            [20, 10, 15, 25, 50, 100, 200]
        ]
    });

});