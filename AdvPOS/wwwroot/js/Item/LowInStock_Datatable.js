$(document).ready(function () {
    document.title = 'Low In Stock List';

    $("#tblLowInStock").DataTable({
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
            "url": "/Items/GetLowInStock",
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "Id", "name": "Id" },
            {
                data: "Name", "name": "Name", render: function (data, type, row) {
                    return "<a href='#' onclick=ViewItem('" + row.Id + "');>" + row.Name + "</a>";
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='d-block' onclick=ViewImageByURLOnly('" + row.ImageURL + "');><div class='image'><img src='" + row.ImageURL + "' class='img-circle elevation-2 imgCustom50px' alt='Asset Image'></div></a>";
                }
            },
            { "data": "StockKeepingUnit", "name": "StockKeepingUnit" },
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
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-link btn-xs' onclick=UpdateQuantity('" + row.Id + "');><span class='glyphicon glyphicon-edit'></span>Add Qnt</a>";
                }
            },
        ],
        "columnDefs": [{
            'targets': [11],
            'orderable': false,
        }],
        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
    });

});
