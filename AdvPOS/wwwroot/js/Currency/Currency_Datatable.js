$(document).ready(function () {
    document.title = 'Currency';

    $("#tblCurrency").DataTable({
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
            "url": "/Currency/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },


        "columns": [
            {
                data: "Id", "name": "Id", render: function (data, type, row) {
                    return "<a href='#' onclick=Details('" + row.Id + "');>" + row.Id + "</a>";
                }
            },
            { "data": "Name", "name": "Name" },
            { "data": "Code", "name": "Code" },
            { "data": "Symbol", "name": "Symbol" },
            { "data": "Country", "name": "Country" },
            { "data": "Description", "name": "Description" },
            {
                data: null, render: function (data, type, row) {
                    if (row.IsDefaultDisplay == 'Yes') {
                        return "<button class='btn btn-xs btn-success'><span>Yes</span ><i class='fa fa-check-circle' aria-hidden='true'></i></button>";
                    }
                    else {
                        return "<button class='btn btn-xs'><span>No</span ><i class='fa fa-flag' aria-hidden='true'></i></button>";
                    }
                }
            },
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
                    return "<a href='#' class='btn btn-info btn-xs' onclick=AddEdit('" + row.Id + "');>Edit</a>";
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-danger btn-xs' onclick=Delete('" + row.Id + "'); >Delete</a>";
                }
            }
        ],

        'columnDefs': [{
            'targets': [8, 9],
            'orderable': false,
        }],

        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
    });

});

