﻿var dataTable;

$(document).ready(function () {
    LoadList();
});

function LoadList() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/adminCommonArea/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "assetName", "width": "15%" },
            { "data": "purchasePrice", "width": "15%" },
            { "data": "description", "width": "15%" },
            { "data": "status", "width": "15%" },
            { "data": "date", "width": "15%" },
            {
                "data": "commonAreaAssetId",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/admin/commonareaassets/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-edit"></i> Edit
                                </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onClick=Delete('/api/adminCommonArea/'+${data})>
                                    <i class="far fa-trash-alt"></i> Delete
                                </a>
                             </div>`;
                }, "width": "25%"
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