$(document).ready(function () {
    LoadDataTables();
    GetPaymentSummaryData();
});


var CustomRangeDataFilter = function () {
    var _StartDate = $("#StartDate").val();
    var _EndDate = $("#EndDate").val();
    location.href = "/Payment/Index?StartDate= " + _StartDate + "&EndDate= " + _EndDate;
};


var LoadDataTables = function () {
    document.title = 'Payment';

    $("#tblPayments").DataTable({
        paging: true,
        select: true,
        "order": [[0, "desc"]],
        dom: 'Bfrtip',


        buttons: [
            'pageLength',
            {
                extend: 'collection',
                text: 'Export',
                buttons: [
                    {
                        extend: 'pdfHtml5',
                        customize: function (doc) {
                            //doc.content[1].margin = [100, 0, 100, 0];
                            //Remove the title created by datatTables
                            doc.content.splice(0, 1);
                            //Create a date string that we use in the footer. Format is dd-mm-yyyy
                            var now = new Date();
                            var jsDate = now.getDate() + '-' + (now.getMonth() + 1) + '-' + now.getFullYear();

                            doc.pageMargins = [20, 60, 20, 30];
                            // Set the font size fot the entire document
                            doc.defaultStyle.fontSize = 7;
                            // Set the fontsize for the table header
                            doc.styles.tableHeader.fontSize = 10;


                            doc['header'] = (function () {
                                return {
                                    columns: [
                                        {
                                            alignment: 'left',  //center
                                            italics: true,
                                            text: 'Payment Summary Report',
                                            fontSize: 18,
                                            margin: [0, 0]
                                        }
                                    ],
                                    margin: 20
                                }
                            });

                            // Create a footer object with 2 columns
                            doc['footer'] = (function (page, pages) {
                                return {
                                    columns: [
                                        {
                                            alignment: 'left',
                                            text: ['Created on: ', { text: jsDate.toString() }]
                                        },
                                        {
                                            alignment: 'right',
                                            text: ['page ', { text: page.toString() }, ' of ', { text: pages.toString() }]
                                        }
                                    ],
                                    margin: 5
                                }
                            });
                            // Change dataTable layout (Table styling)
                            // To use predefined layouts uncomment the line below and comment the custom lines below
                            // doc.content[0].layout = 'lightHorizontalLines'; // noBorders , headerLineOnly
                            var objLayout = {};
                            objLayout['hLineWidth'] = function (i) { return .5; };
                            objLayout['vLineWidth'] = function (i) { return .5; };
                            objLayout['hLineColor'] = function (i) { return '#aaa'; };
                            objLayout['vLineColor'] = function (i) { return '#aaa'; };
                            objLayout['paddingLeft'] = function (i) { return 4; };
                            objLayout['paddingRight'] = function (i) { return 4; };
                            doc.content[0].layout = objLayout;
                        },


                        orientation: 'portrait', // landscape
                        pageSize: 'A4',
                        pageMargins: [0, 0, 0, 0], // try #1 setting margins
                        margin: [0, 0, 0, 0], // try #2 setting margins
                        text: '<u>PDF</u>',
                        key: { // press E for export PDF
                            key: 'e',
                            altKey: false
                        },
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8], //column id visible in PDF
                            modifier: {
                                // DataTables core
                                order: 'index',  // 'current', 'applied', 'index',  'original'
                                page: 'all',      // 'all',     'current'
                                search: 'none'     // 'none',    'applied', 'removed'
                            }
                        }
                    },
                    'copyHtml5',
                    'excelHtml5',
                    /*'csvHtml5',*/
                    {
                        extend: 'csvHtml5',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8],
                            page: 'all'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8],
                            page: 'all'
                        }
                    }
                ]
            }
        ],


        "processing": true,
        "serverSide": true,
        "filter": true, //Search Box
        "orderMulti": false,
        "stateSave": true,

        "ajax": {
            "url": "/Payment/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },


        "columns": [{
            data: "Id",
            "name": "Id",
            render: function (data, type, row) {
                return "<a href='#' onclick=Details('" + row.Id + "');>" + row.InvoiceNo + "</a>";
            }
        },
        {
            data: "CustomerId",
            "name": "CustomerId",
            render: function (data, type, row) {
                return "<a href='#' onclick=ViewCustomerDetails('" + row.CustomerId + "');>" + row.CustomerName + "</a>";
            }
        },
        { "data": "SubTotal", "name": "SubTotal" },
        { "data": "DiscountAmount", "name": "DiscountAmount" },
        { "data": "VATAmount", "name": "VATAmount" },
        { "data": "GrandTotal", "name": "GrandTotal" },
        { "data": "PaidAmount", "name": "PaidAmount" },
        { "data": "DueAmount", "name": "DueAmount" },
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
        {
            data: null, render: function (data, type, row) {
                return "<a href='#' onclick=POSReport('" + row.Id + "');><i class='fa fa-print' aria-hidden='true'>POS</i></a>";
            }
        },
        {
            data: null,
            render: function (data, type, row) {
                if (row.DueAmount == 0) {
                    return "<button class='btn btn-xs btn-info'><span>'" + row.PaymentStatusDisplay + "'</span><i class='fa fa-check-circle' aria-hidden='true''></i></button>";
                } else {
                    return "<button class='btn btn-xs btn-dark'><span>'" + row.PaymentStatusDisplay + "'</span><i class='fa fa-flag' aria-hidden='true''></i></button>";
                }

            }
        },
        {
            data: null,
            render: function (data, type, row) {
                return "<a href='#' class='btn btn-success btn-xs' onclick=PrintPaymentInvoice('" + row.Id + "');>Invoice</a>";
            }
        },
        {
            data: null,
            render: function (data, type, row) {
                if (row.DueAmount == 0 && row.GrandTotal > 0) {
                    return null;
                } else {
                    return "<a href='#' class='btn btn-info btn-xs' onclick=AddEdit('" + row.Id + "');>Edit</a>";
                }
            }
        },
        {
            data: "Id", "name": "Id", render: function (data, type, row) {
                return "<a href='#' class='btn btn-sm btn-primary' onclick=GetSalesRetrurn('" + row.Id + "');><i class='fa fa-arrow-circle-left' aria-hidden='true'></i></a>";
            }
        },
        {
            data: null,
            render: function (data, type, row) {
                return "<a href='#' class='btn btn-danger btn-xs' onclick=Delete('" + row.Id + "'); >Delete</a>";
            }
        }
        ],

        'columnDefs': [{
            'targets': [9, 10, 11, 12, 13],
            'orderable': false,
        }],

        "lengthMenu": [
            [20, 10, 15, 25, 50, 100, 200],
            [20, 10, 15, 25, 50, 100, 200]
        ]
    });
};