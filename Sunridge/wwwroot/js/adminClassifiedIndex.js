﻿var dataTable;

$(document).ready(function () {
    LoadList();
});

function LoadList() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/adminClassifiedIndex/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "classifiedcategory", "width": "15%" },
            { "data": "itemName", "width": "15%" },
            { "data": "price", "width": "10%" },
            { "data": "description", "width": "10%" },
            { "data": "phone", "width": "15%" },
            { "data": "email", "width": "15%" },
            {
                "data": "classifiedListingId",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/admin/classifieds/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-edit"></i> Edit
                                </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onClick=Delete('/api/adminClassifiedIndex/'+${data})>
                                    <i class="far fa-trash-alt"></i> Delete
                                </a>
                             </div>`;
                }, "width": "20%"
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
        title: "Are you sure you want to delete?",
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
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}