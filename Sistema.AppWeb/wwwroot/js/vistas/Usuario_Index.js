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

    fetch("/Usuario/ListaRoles").then(respose => {
        return respose.ok ? respose.json() : Promise.reject(respose);
    })
        .then(resposeJson => {
        if (resposeJson.length > 0) {
            resposeJson.forEach((item) => {
                $("#cboPuesto").append(
                    $("<option>").val(item.idRol).text(item.descripcion)
                )
            })
        }
    })

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
             //{ "data": "idPuesto", "visible": false, "searchable": false },
             //{ "data": "dui", "visible": false, "searchable": false },
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



function mostrarModal(modelo = modelo_base) {
    $("#txtDUI").val(modelo.dui);
    $("#txtNombre").val(modelo.nombre);
    $("#txtApellido").val(modelo.apellido);
    $("#txtEmail").val(modelo.email);
    $("#txtUsuario").val(modelo.usuario);
    $("#txtSueldoBase").val(modelo.sueldo_base);
    $("#cboPuesto").val(modelo.id_puesto == 1 ? $("#cboPuesto option:first").val : modelo.id_puesto);
    $("#txtContrasena").val("");
    $("#imgEmpleado").attr("src", modelo.url_imagen);


    $("#modalData").modal("show");
}

$("#btnNuevo").click(function () {
    mostrarModal()
})