const modelo_base = {
    id_empleado : "",
    dui : "",
    nombre : "",
    apellido : "",
    email : "",
    usuario : "",
    sueldo_base : "",
    id_puesto : "",
    url_imagen : ""
}
let tablaData;

$(document).ready(function () {
    tablaData = $('#tbdata').DataTable({
        responsive: true,
         "ajax": {
             "url": '/Usuario/Lista',
             "type": "GET",
             "datatype": "json"
         },
         "columns": [
             { "data": "idEmpleado", "visible": false, "searchable": false },
             { "data": "urlImagen", render: function (data) { return '<img style="height:60px" src=${data} class="rounded mx-auto d-block"/>' } },
             { "data": "nombre" },
             { "data": "apellido" },
             { "data": "email" },
             { "data": "usuario" },
             { "data": "sueldoBase" },
             {
                 "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                     '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                 "orderable": false,
                 "searchable": false,
                 "width": "80px"
             }
         ],
         order: [[0, "desc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Usuarios',
                exportOptions: {
                    columns: [2,0,3,4,5]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    });
});
