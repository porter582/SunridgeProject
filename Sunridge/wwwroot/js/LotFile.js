var dataTable;

$(document).ready(function () {
    LoadList();
});

function LoadList() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/LotFile/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "description", "width": "25%" },
            { "data": "fileURL",
                "render": function (data) {
                    return `<a href="/docs/lotFiles/${data}" download>${data}</a>`;
                }
                ,"width": "25%"
            },
            {
                "data": "fileId",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/admin/hOALots/filesUpsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-edit"></i> Edit
                                </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onClick=Delete('/api/LotFile/'+${data})>
                                    <i class="far fa-trash-alt"></i> Delete
                                </a>
                             </div>`;
                }, "width": "25%"
            }
        ],
        "language": {
            "emptyTable": "no files found for this lot."
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