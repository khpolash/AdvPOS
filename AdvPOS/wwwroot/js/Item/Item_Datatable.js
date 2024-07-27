﻿$(document).ready(function () {
    document.title = 'Items List';

    $("#tblItem").DataTable({
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
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10], //column id visible in PDF
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
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
                            page: 'all'
                        }
                    },
                    {
                        extend: 'print',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10],
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
            "url": "/Items/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "Id", "name": "Id" },
            {
                data: "Name",
                "name": "Name",
                render: function (data, type, row) {
                    return "<a href='#' onclick=ViewItem('" + row.Id + "');>" + row.Name + "</a>";
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a href='#' class='d-block' onclick=ViewImageByURLOnly('" + row.ImageURL + "');><div class='image'><img src='" + row.ImageURL + "' class='img-circle elevation-2 imgCustom50px' alt='Asset Image'></div></a>";
                }
            },
            { "data": "Code", "name": "Code" },
            { "data": "CategoriesDisplay", "name": "CategoriesDisplay" },
            { "data": "SupplierDisplay", "name": "SupplierDisplay", "autoWidth": true },
            { "data": "MeasureDisplay", "name": "MeasureDisplay" },
            { "data": "CostPrice", "name": "CostPrice" },
            { "data": "NormalPrice", "name": "NormalPrice" },
            { "data": "Quantity", "name": "Quantity" },
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
                data: null,
                render: function (data, type, row) {
                    return "<a href='#' onclick=ViewItemHistory('" + row.Id + "');><i class='fa fa-history' aria-hidden='true'></i></a>";
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a href='#' class='btn btn-link btn-xs' onclick=UpdateQuantity('" + row.Id + "');>Add Qnt</a>";
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a href='#' class='btn btn-info btn-xs' onclick=AddEdit('" + row.Id + "');>Edit</a>";
                }
            },
            {
                data: null,
                render: function (data, type, row) {
                    return "<a href='#' class='btn btn-danger btn-xs' onclick=DeleteItem('" + row.Id + "'); >Delete</a>";
                }
            }
        ],
        "columnDefs": [{
            'targets': [11, 12, 13, 14],
            'orderable': false,
        }],

        "lengthMenu": [
            [15, 10, 15, 20, 50, 100, 200],
            [20, 10, 15, 25, 50, 100, 200]
        ]
    });

});