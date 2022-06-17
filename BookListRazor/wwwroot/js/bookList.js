let dataTable;

$(document).ready(() => {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        'ajax': {
            'url': '/api/Book',
            'type': 'GET',
            'datatype': 'json'
        },
        'columns': [
            {'data': 'name', 'width': '20%'},
            {'data': 'author', 'width': '20%'},
            {'data': 'isbn', 'width': '20%'},
            {
                'data': 'id',
                'render': (data) => {
                    return `
                    <div class="text-center">
                        <a href="/BookList/Upsert?id=${data}" class="btn btn-success">Edit</a>
                        <a onclick="deleteBook('/api/book?id=${data}')" class="btn btn-danger">Delete</a>
                    </div>
                    `
                }, 'width': '40%'
            }
        ],
        'language': {
            'emptyTable': 'no data found'
        }, 'width': '100%'
    })
}

function deleteBook(url) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then(willDelete => {
        if (willDelete)
            $.ajax({
                type: "DELETE",
                url: url,
                success: (data) => {
                    if (!data.success)
                        toastr.error(data.message);

                    toastr.success(data.message)
                    dataTable.ajax.reload()
                }
            })
    })
}