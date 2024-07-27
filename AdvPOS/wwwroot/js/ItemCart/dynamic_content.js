var GetItemDivTemplate = function (data) {
    var divContent = '';
    divContent += "<div class='info-box col-12 col-sm-12'>"
        + "<span class='info-box-icon bg-info elevation-1'>"
        + "<img src='" + data.ImageURL + "' class='img-circle elevation-1' alt='Asset Image'>"
        + "</span>"
        + "<div class='info-box-content'>"
        + "<span class='info-box-text'>"
        + "<a href='#' onclick=ViewItem('" + data.Id + "');>" + data.Name + "</a>"
        + "</span>"
        + "<span class='info-box-number'>"
        + "Price:&nbsp;" + data.SellPrice + " &nbsp;(" + data.Quantity + ")"
        + "</span>"

        + "<span class='info-box-text'>"
        + "<label class='control-label small' hidden>" + data.Barcode + "<label/>"
        + "<label class='control-label small' hidden>" + data.CategoriesName + "<label/>"
        + "</span>"

        + "<span class='info-box-text'>"
        + "<a href='#' onclick=AddtoCart('" + data.Id + "'); class='fa fa-plus-square fa-2x'></a></span>"
        + "</div>"
        + "</div>";

    return divContent;
}