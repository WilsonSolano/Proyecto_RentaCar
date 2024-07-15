const modelo_base = {
    idEmpleado: 0,
    dui: "",
    nombre: "",
    apellido: "",
    email: "",
    usuario: "",
    sueldoBase: 0,
    idPuesto: 1,
    urlImagen: "",
    es_activo: 1,
};
let tablaData;
let filaSeleccionada;
const fileImagenPreview = document.getElementById("txtFoto");
const tagImg = document.getElementById("imgUsuario");

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
             { "data": "urlImagen", render: function (data) { return `<img style="height:60px" src=${data} class="rounded mx-auto d-block"/>` } },
             { "data": "dui" },
             { "data": "nombre" },
             { "data": "apellido" },
             { "data": "email" },
             { "data": "usuario" },
             { "data": "sueldoBase" },
             { "data": "descripcion" },         
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
    $("#txtId").val(modelo.idEmpleado);
    $("#txtDUI").val(modelo.dui);
    $("#txtNombre").val(modelo.nombre);
    $("#txtApellido").val(modelo.apellido);
    $("#txtEmail").val(modelo.email);
    $("#txtUsuario").val(modelo.usuario);
    $("#txtSueldoBase").val(modelo.sueldoBase);
    $("#cboPuesto").val(modelo.idPuesto == 1 ? $("#cboPuesto option:first").val() : modelo.idPuesto);
    $("#imgUsuario").attr("src", modelo.urlImagen);


    $("#modalData").modal("show");
}


$("#btnNuevo").click(function () {
    mostrarModal()
})

$("#tbdata tbody").on("click", ".btn-editar", function () {
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    }
    else {
        filaSeleccionada = $(this).closest("tr");
    }

    const data = tablaData.row(filaSeleccionada).data()

    mostrarModal(data);
    console.log(data);
})

$("#btnGuardar").click(function () {
    const inputs = $("input.input-validar").serializeArray();
    const inputEmpty = inputs.filter((item) => item.value.trim() == "");

    if (inputEmpty.length > 0) {
        const mensaje = `Debe completar el campo : "${inputEmpty[0].name}"`;
        toastr.warning("", mensaje);
        $(`input[name="${inputEmpty[0].name}"]`).focus();
        return;
    }

    const modelo = structuredClone(modelo_base);

    modelo.idEmpleado = parseInt($("#txtId").val());
    modelo.nombre = $("#txtNombre").val();
    modelo.dui = $("#txtDUI").val();
    modelo.apellido = $("#txtApellido").val();
    modelo.email = $("#txtEmail").val();
    modelo.usuario = $("#txtUsuario").val();
    modelo.sueldoBase = $("#txtSueldoBase").val();
    modelo.idPuesto = $("#cboPuesto").val();
    modelo.esActivo = $("#cboEstado").val();

    const inputFoto = document.getElementById("txtFoto");

    const formData = new FormData();

    formData.append("foto", inputFoto.files[0]);
    formData.append("modelo", JSON.stringify(modelo));

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idEmpleado == 0) {
        fetch("/Usuario/Crear", {
            method: "POST",
            body: formData
        }).then(response => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (responseJson.estado) {
                tablaData.row.add(responseJson.objeto).draw(false);
                $('#txtFoto').val('');
                $("#modalData").modal("hide");
                swal("¡Listo!", "Empleado Creado", "success");
            } else {
                swal("Lo Sentimos", responseJson.mensaje, "error");
            }
        }).catch(error => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
            $('#txtFoto').val('');
            swal("Error", "No se pudo crear el empleado", "error");
            console.error('Error:', error);
        });
    } else {
        fetch("/Usuario/Editar", {
            method: "PUT",
            body: formData
        }).then(response => {
            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
            return response.ok ? response.json() : Promise.reject(response);
        }).then(responseJson => {
            if (responseJson.estado) {
                tablaData.row(filaSeleccionada).data(responseJson.objeto).draw(false);
                filaSeleccionada = null
                $('#txtFoto').val('');
                $("#modalData").modal("hide");
                swal("¡Listo!", "Empleado Fue Modificado", "success");
            } else {
                swal("Lo Sentimos", responseJson.mensaje, "error");
            }
        }).catch(error => {
            $('#txtFoto').val('');
            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
            swal("Error", "No se pudo crear el empleado", "error");
            console.error('Error:', error);
        });
    }
});

//Logica para el el preview de la imagen seleccionada

fileImagenPreview.addEventListener('change', e => {
    if (e.target.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            tagImg.src = e.target.result;
        }

        reader.readAsDataURL(e.target.files[0])
    } else {
        tagImg.src = "";
    }
})











//const modelo_base = {
//    idEmpleado : 0,
//    dui : "",
//    nombre : "",
//    apellido : "",
//    email : "",
//    usuario : "",
//    sueldoBase : "",
//    idPuesto : 1,
//    urlImagen: "",
//    "es_activo": 1,
//}
//let tablaData;

//$(document).ready(function () {

//    fetch("/Usuario/ListaRoles").then(respose => {
//        return respose.ok ? respose.json() : Promise.reject(respose);
//    })
//        .then(resposeJson => {
//        if (resposeJson.length > 0) {
//            resposeJson.forEach((item) => {
//                $("#cboPuesto").append(
//                    $("<option>").val(item.idRol).text(item.descripcion)
//                )
//            })
//        }
//    })

//    tablaData = $('#tbdata').DataTable({
//        responsive: true,
//         "ajax": {
//             "url": '/Usuario/Lista',
//             "type": "GET",
//             "datatype": "json"
//         },
//         "columns": [
//             { "data": "idEmpleado", "visible": false, "searchable": false },
//             { "data": "urlImagen", render: function (data) { return '<img style="height:60px" src=${data} class="rounded mx-auto d-block"/>' } },
//             { "data": "nombre" },
//             { "data": "apellido" },
//             { "data": "email" },
//             { "data": "usuario" },
//             { "data": "sueldoBase" },
//             //{ "data": "idPuesto", "visible": false, "searchable": false },
//             //{ "data": "dui", "visible": false, "searchable": false },
//             {
//                 "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
//                     '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
//                 "orderable": false,
//                 "searchable": false,
//                 "width": "80px"
//             }
//         ],
//         order: [[0, "desc"]],
//        dom: "Bfrtip",
//        buttons: [
//            {
//                text: 'Exportar Excel',
//                extend: 'excelHtml5',
//                title: '',
//                filename: 'Reporte Usuarios',
//                exportOptions: {
//                    columns: [2,0,3,4,5]
//                }
//            }, 'pageLength'
//        ],
//        language: {
//            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
//        },
//    });
//});



//function mostrarModal(modelo = modelo_base) {
//    $("txtId").val(modelo.id_empleado);
//    $("#txtDUI").val(modelo.dui);
//    $("#txtNombre").val(modelo.nombre);
//    $("#txtApellido").val(modelo.apellido);
//    $("#txtEmail").val(modelo.email);
//    $("#txtUsuario").val(modelo.usuario);
//    $("#txtSueldoBase").val(modelo.sueldo_base);
//    $("#cboPuesto").val(modelo.id_puesto == 1 ? $("#cboPuesto option:first").val : modelo.id_puesto);
//    $("#txtContrasena").val("");
//    $("#imgEmpleado").attr("src", modelo.url_imagen);


//    $("#modalData").modal("show");
//}


//$("#btnNuevo").click(function () {
//    mostrarModal()
//})

//$("#btnGuardar").click(function () {
//    const inputs = $("input.input-validar").serializeArray();
//    const inputEmpy = inputs.filter((item) => item.value.trim() == "");

//    if (inputEmpy.length > 0) {
//        const mensaje = `Debe completar el campo : "${inputEmpy[0].name}"`;

//        toastr.warning("", mensaje);

//        $(`input[name="${inputEmpy[0].name}"]`).focus();
//        return;
//    }

//    const modelo = structuredClone(modelo_base);

//    modelo["idEmpleado"] = parseInt($("txtId").val());
//    modelo["nombre"] = $("#txtNombre").val();
//    modelo["dui"] = $("#txtDUI").val();
//    modelo["apellido"] = $("#txtApellido").val();
//    modelo["email"] = $("#txtEmail").val();
//    modelo["usuario"] = $("#txtUsuario").val();
//    modelo["sueldoBase"] = $("#txtSueldoBase").val();
//    modelo["idPuesto"] = $("#cboPuesto").val();
//    modelo["es_activo"] = $("#cboEstado").val();

//    const inputFoto = document.getElementById("txtFoto");

//    const formData = new FormData();

//    formData.append("foto", inputFoto.files[0]);
//    formData.append("modelo", JSON.stringify(modelo));

//    $("#modalData").find("div.modal-content").LoadingOverlay("show");

//    if (modelo.id_empleado == 0) {
//        fetch("/Usuario/Crear", {
//            method: "POST",
//            body: formData
//        }).then(respose => {
//            $("#modalData").find("div.modal-content").LoadingOverlay("hide");
//            return respose.ok ? respose.json() : Promise.reject(respose);
//        }).then(responseJson => {
//            if (responseJson.estado) {
//                tablaData.row.add(responseJson.objeto).draw(false);
//                $("#modalData").modal("hide");
//                swal("¡Listo!", "Empleado Creado", "Success")
//            } else {
//                swal("Lo Sentimos", responseJson.mensaje, "error")
//            }
//        })
//    }
//})

