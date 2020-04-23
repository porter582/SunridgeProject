var dataTable;

$(document).ready(function () {
    LoadList();
});

function LoadList() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/ownerIndex/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "13%" },
            { "data": "username", "width": "13%" },
            { "data": "emergencyContactName", "width": "13%" },
            { "data": "emergencyContactPhone", "width": "13%" },
            { "data": "lots", "width": "13%" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/admin/owners/upsert?id=${data}" class="btn btn-info text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-edit"></i> Edit
                                </a>
                                <a href="/admin/owners/passwordResetIndex?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:160px;">
                                    <i class="fas fa-lock-open"></i> Reset Password
                                </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onClick=Delete('/api/ownerIndex/${data}')>
                                    <i class="far fa-trash-alt"></i> Delete
                                </a>
                             </div>`;
                }, "width": "35%"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

function Delete(url) {
    console.log("made it");
    swal({
        title: "Are you sure you want to delete?",
        text: "User will be removed!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}