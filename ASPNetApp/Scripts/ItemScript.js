$(document).ready(function () {
    $('#items').dataTable({
        "columns": [null, null, null, null, { "bSortable": false }],
        "ajax": loadDataItem(),
        "responsive": true
    });
});
function loadDataItem() {
    debugger;
    $.ajax({
        url: "/Items/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            debugger;
            var html = '';
            const obj = JSON.parse(result);
            $.each(obj, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Stock + '</td>';
                html += '<td>' + item.Price + '</td>';
                html += '<td>' + item.Supplier.Name + '</td>';
                html += '<td><a href="#" class="btn btn-success" onclick="return GetById(\'' + item.Id + '\')"><i class="fa fa-pencil"> Edit</i></a> ';
                html += '| <a href="#" class="btn btn-danger" onclick="Delete(' + item.Id + ')"><i class="fa fa-trash"> Delete</i></a></td > ';
                html += '</tr>';
            });
            $('.itemtbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}