var datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "ajax": {
            "url": "/Project/GetEntity"
        },
        "columns": [
            { "data": "titleProject", "width": "20%" },
            { "data": "descProject", "width": "40%" },
            { "data": "contentProject", "width": "40%" },
            { "data": "dateProject", "width": "40%" },
            { "data": "CategoryId", "width": "40%" },
            { "data": "AdminId", "width": "40%" },
            {
                "data": "active",
                "render": function (data) {
                    if (data == true) {
                        return "Activo";
                    }
                    else {
                        return "Inactivo";
                    }
                }, "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="Project/Create/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                <i class="fas fa-edit"></i>
                            </a>
                            <a class="btn btn-danger text-white" style="cursor:pointer">
                                <i class="fas fa-trash"></i>
                            </a>
                        </div>
                        `;
                }, "width": "20%"
            }
        ]
    });
}