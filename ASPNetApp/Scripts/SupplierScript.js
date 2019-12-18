$(document).ready(function () {
    $('#suppliers').dataTable({
        "columnDefs": [{
            "orderable": false,
            "targets": 2,
        }],
        "ajax": loadDataSupplier(),
        "responsive": true
    });
});
function loadDataSupplier() {
    $.ajax({
        url: "/Suppliers/List",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (result) {
            var html = '';
            const obj = JSON.parse(result);
            $.each(obj, function (key, supplier) {
                html += '<tr>';
                html += '<td>' + supplier.Name + '</td>';
                html += '<td>' + supplier.Email + '</td>';
                html += '<td><button type="button" class="btn btn-warning" id="Update" onclick="return GetbyId(' + supplier.Id + ')"><i class="fa fa-pencil"> Edit</i></button> ';
                html += '<button type="button" class="btn btn-danger" id="Delete" onclick="return Delete(' + supplier.Id + ')" ><i class="fa fa-trash"> Delete</i></button ></td > ';
                html += '</tr>';
                html += '</tr>';
                html += '</tr>';
            });
            $('.suppliertbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Save() {
    if ($('#Name').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Name',
            showConfirmButton: false,
            timer: 1500
        });
    } else if ($('#Email').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Email',
            showConfirmButton: false,
            timer: 1500
        });
    } else {
        var supplier = new Object();
        supplier.Id = $('#Id').val();
        supplier.Name = $('#Name').val();
        supplier.Email = $('#Email').val();
        $.ajax({
            type: 'POST',
            url: '/Suppliers/InsertOrUpdate/',
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
    }
}

function Update() {
    if ($('#Name').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Name',
            showConfirmButton: false,
            timer: 1500
        });
    } else {
        var data = new Object();
        data.Id = $('#Id').val();
        data.Name = $('#Name').val();
        data.Email = $('#Email').val();
        $.ajax({
            url: "/Suppliers/InsertOrUpdate/",
            dataType: "json",
            data: data,
        }).then((result) => {
            //debugger;
            $('#myModal').hide();
            if (result.StatusCode == 200) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'Update Successfully',
                    showConfirmButton: false,
                    timer: 1500
                });
                ResetTable()
            }
            else {
                Swal.fire('Error', 'Update Fail', 'error');
                ClearScreen();
            }
        });
    }
}

function GetbyId(Id) {
    //debugger;
    $('#Name');
    $('#Email');
    $.ajax({
        url: "/Suppliers/GetbyId/" + Id,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        async: false,
        success: function (result) {
            const obj = JSON.parse(result);
            $('#Id').val(obj.Id);
            $('#Name').val(obj.Name);
            $('#Email').val(obj.Email);

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
                url: "/Suppliers/Delete/",
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