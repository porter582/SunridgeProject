var dataTable;

$(document).ready(function () {
    LoadList();
});

function LoadList() {
    var lotNum;
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/api/hoaLots/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "lotNumber", "render": function (data) {
                    lotNum = data;
                    return `${data}`;
                }, "width": "6%"
            },
            { "data": "streetAddress", "width": "16%" },
            { "data": "userName", "width": "10%" },
            {
                "data": "taxId", "render": function (data) {
                    return `<a href="http://www3.co.weber.ut.us/psearch/tax_summary.php?id=${data}" target="_blank">${data}</a>`;  
                },
                "width": "9%"
            },
            { "data": "lotInventory", "width": "14%" },
            {
                "data": "id",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/admin/hOALots/upsert?id=${data}" class="btn btn-info text-white" style="cursor:pointer; width:80px;">
                                    <i class="far fa-edit"></i> Edit
                                </a>
                                <a href="/admin/hOALots/filesIndex?id=${data}" class="btn btn-dark text-white" style="cursor:pointer; width:80px;">
                                    <i class="far fa-file-alt"></i> Files
                                </a>
                             </div>`;
                }, "width": "15%"
            },
            {
                "data": "primaryOwnerId",
                "render": function (data) {
                    if (data) {
                        return ` <div class="text-center">
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:100px;" data-toggle="tooltip" data-placement="top" title="Removes Owner from lot, then Archives owner so you can add new Owner(s) to lot." onClick=Delete('/api/hoaLots/${lotNum}')>
                                    <i class="far fa-trash-alt"></i> Primary
                                </a>
                             </div>`;
                    } else {
                        return ` <div class="text-center">
                                <button class="btn btn-danger text-white" style="cursor:pointer; width:110px;" data-toggle="tooltip" data-placement="top" title="Removes Owner from lot, then Archives owner so you can add new Owner(s) to lot." disabled>
                                    <i class="far fa-trash-alt"></i> Primary
                                </button>
                             </div>`;
                    }
                }, "width": "10%"
            },
            {
                "data": "secondaryOwnerId",
                "render": function (data) {
                    if (data) {
                        return ` <div class="text-center">
                                <a class="btn btn-danger text-white" style="cursor:pointer; width:120px;" data-toggle="tooltip" data-placement="top" title="Removes Owner from lot, then Archives owner so you can add new Owner(s) to lot." onClick=Delete('/api/hoaLots2/${lotNum}')>
                                    <i class="far fa-trash-alt"></i> Secondary
                                </a>
                             </div>`;
                    } else {
                        return ` <div class="text-center">
                                <button class="btn btn-danger text-white" style="cursor:pointer; width:120px;" data-toggle="tooltip" data-placement="top" title="Removes Owner from lot, then Archives owner so you can add new Owner(s) to lot." disabled>
                                    <i class="far fa-trash-alt"></i> Secondary
                                </button>
                             </div>`;
                    }
                }, "width": "10%"
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
        title: "Are you sure you want to Remove and Archive?",
        text: "You will remove this owner from the lot AND archive them from the system!",
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
                        console.log("success");
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}