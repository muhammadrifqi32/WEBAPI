$(document).ready(function () {
    $('#items').dataTable({
        "columns": [null, null, null, null, { "bSortable": false }],
        "ajax": loadDataItem(),
        "responsive": true
    });
    LoadSupplier();
});
function loadDataItem() {
    //debugger;
    $.ajax({
        url: "/Items/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            //debugger;
            var html = '';
            //const obj = JSON.parse(result);
            $.each(result, function (key, item) {
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

var Suppliers = []
function LoadSupplier(element) {
    //debugger;
    if (Suppliers.length == 0) {
        $.ajax({
            type: "Get",
            url: "/Suppliers/LoadSupplier",
            success: function (data) {  
                Suppliers = data;
                renderSupplier(element);
            }
        })
    }
    else {
        renderSupplier(element);
    }
}

function renderSupplier(element) {
    //debugger;
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select Supplier'));
    $.each(Suppliers, function (i, val) {
        $ele.append($('<option/>').val(val.Id).text(val.Name));
    })
}
LoadSupplier($('#Supplier'));