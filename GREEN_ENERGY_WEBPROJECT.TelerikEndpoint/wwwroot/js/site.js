// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var href = window.location.href;
if (href.indexOf('culture') > -1) {
    $('#culture').val(href.replace(/(.*)culture=([^&]*)/, '$2'));
}
function cultureChange(e) {
    var value = this.value();
    if (href.indexOf('culture') > -1) {
        href = href.replace(/culture=([^&]*)/, 'culture=' + value);
    } else {
        href += href.indexOf('?') > -1 ? '&culture=' + value : '?culture=' + value;
    }
    window.location.href = href;
}

function onTileResize(e) {

    var container = e.item || e.container;
    if (container) {

        // for widgets that do not auto resize
        // https://docs.telerik.com/kendo-ui/styles-and-layout/using-kendo-in-responsive-web-pages
        kendo.resize(container, true);
    }

}

function populateTotals(e) {
    // we are using the aggregates of the grid to populate our totals
    var aggregates = e.sender.dataSource.aggregates();

    $("#total-streams").html(kendo.toString(aggregates.Streams.sum, "n0"));
    $("#total-downloads").html(kendo.toString(aggregates.Downloads.sum, "n0"));
    $("#total-reach").html(kendo.toString(aggregates.Reach.sum, "n0"));
    $("#total-revenue").html(kendo.toString(aggregates.Views.sum / 100, "c0"));
}
function getCategory(data) {
    return data.category;
}