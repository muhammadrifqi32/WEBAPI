$(document).ready(function () {
    $('#items').dataTable({
        "columnDefs": [{
            "orderable": false,
            "targets": 4,
        }],
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
                html += '<td><button type="button" class="btn btn-warning" id="Update" onclick="return GetbyId(' + item.Id + ')"><i class="fa fa-pencil"> Edit</i></button> ';
                html += '<button type="button" class="btn btn-danger" id="Delete" onclick="return Delete(' + item.Id + ')" ><i class="fa fa-trash"> Delete</i></button ></td > ';
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

function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#Email').val('');
    $('#Update').hide();
    $('#Save').show();
}

function ResetTable() {
    $('#suppliers').dataTable().destroy();
    $('#suppliers').dataTable({
        "ajax": loadDataSupplier()
    })
}


function Save() {
    //if ($('#Name').val() == 0) {
    //    Swal.fire({
    //        position: 'center',
    //        type: 'error',
    //        title: 'Please Full Fill The Name',
    //        showConfirmButton: false,
    //        timer: 1500
    //    });
    //} else if ($('#Email').val() == 0) {
    //    Swal.fire({
    //        position: 'center',
    //        type: 'error',
    //        title: 'Please Full Fill The Email',
    //        showConfirmButton: false,
    //        timer: 1500
    //    });
    //} else {
        var item = new Object();
        item.Id = $('#Id').val();
        item.Name = $('#Name').val();
        item.Stock = $('#Stock').val();
        item.Price = $('#Stock').val();
        item.Supplier = $('#Supplier').val();
        $.ajax({
            type: 'POST',
            url: '/Items/InsertOrUpdate/',
            data: supplier
        }).then((result) => {
            //debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'Insert Successfully',
                });
                ResetTable()
            }
            else {
                Swal.fire('Error', 'Insert Fail', 'error');
                ClearScreen();
            }
        });
    //}
}

function GetbyId(Id) {
    //debugger;
    $('#Name');
    $('#Stock');
    $('#Price');
    $('#Supplier');
    $.ajax({
        url: "/Items/GetbyId/" + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: false,
        success: function (result) {
            const obj = JSON.parse(result);
            $('#Id').val(obj.Id);
            $('#Name').val(obj.Name);
            $('#Stock').val(obj.Email);
            $('#Price').val(obj.Price);
            $('#Supplier').val(obj.Supplier);

            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function Delete(Id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            debugger;
            $.ajax({
                url: "/Items/Delete/",
                type: "POST",
                data: { id: Id },
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire({
                        position: 'center',
                        type: 'success',
                        title: 'Delete Successfully'
                    });
                    ResetTable()
                }
                else {
                    Swal.fire('Error', 'Update Fail', 'error');
                    ClearScreen();
                }
            });
        }
    })
}