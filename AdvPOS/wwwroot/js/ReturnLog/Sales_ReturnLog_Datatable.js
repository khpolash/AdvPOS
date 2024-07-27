$(document).ready(function () {
    document.title = 'Return Log';

    $("#tblReturnLog").DataTable({
        paging: true,
        select: true,
        "order": [[0, "desc"]],
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
            "url": "/ReturnLog/GetSalesRetrurnDataTable",
            "type": "POST",
            "datatype": "json"
        },


        "columns": [
            {
                data: "Id", "name": "Id", render: function (data, type, row) {
                    return "<a href='#' class='fa fa-eye' onclick=ReturnLogDetails('" + row.Id + "');>" + row.Id + "</a>";
                }
            },
            {
                data: "RefId", "name": "RefId", render: function (data, type, row) {
                    return "<a href='#' class='fa fa-eye' onclick=GetSalesReturnItemDetails('" + row.RefId + "');>" + row.RefId + "</a>";
                }
            },
            { "data": "InvoiceNo", "name": "InvoiceNo" },
            { "data": "CustomerDisplay", "name": "CustomerDisplay" },
            { "data": "Note", "name": "Note" },

            {
                "data": "CreatedDate",
                "name": "CreatedDate",
                "autoWidth": true,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.length > 1 ? month : month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            }
        ],

        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
    });

});

