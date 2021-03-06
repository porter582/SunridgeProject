﻿var dataTable;

$(document).ready(function () {
    loadList();
})

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/report/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "username", "width": "10%" },
            { "data": "formType", "width": "15%" },
            { "data": "description", "width": "25%" },
            { "data": "listingDate", "width": "25%" },
            { "data": "resolvedDate", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/Admin/reports/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-edit"></i> Edit
                                </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=Delete('/api/report/'+${data})>
                                    <i class="far fa-trash-alt"></i> Delete
                                </a>
                </div>`;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
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
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}