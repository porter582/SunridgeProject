var dataTable;

$(document).ready(function () {
    loadList();
})

function loadList() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/classifieds/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "category", "width": "15%" },
            { "data": "itemname", "width": "15%" },
            { "data": "price", "width": "10%" },
            { "data": "description", "width": "15%" },
            { "data": "phone", "width": "15%" },
            { "data": "email", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/pages/admin/classifieds/upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px;">
                                    <i class="far fa-edit"></i> Edit
                                </a>
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" onclick=Delete('/api/classifieds/'+${data})>
                                    <i class="far fa-trash-alt"></i> Delete
                                </a>
                </div>`;
                }, "width": "15%"
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