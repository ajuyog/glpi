document.addEventListener("DOMContentLoaded", function () {
    ListaArea();
    ListaPersonas();
});

function detellePersonas(accion, Id = null, userId = '', idArea = '', Identificacion = '', Estado = '') {
    const modalTitle = document.getElementById('titelid-modal');
    const recordId = document.getElementById('recordId');
    const userIdInput = document.getElementById('userId');
    const idAreaInput = document.getElementById('idArea');
    const identificacionInput = document.getElementById('identificacion');
    const EstadoInput = document.getElementById('estado');

    if (accion === 'actualizar') {
        modalTitle.textContent = 'Actualizar Datos';
        recordId.value = Id;
        userIdInput.value = userId;
        $("#userId").trigger("change");
        idAreaInput.value = idArea;
        $("#idArea").trigger("change");
        identificacionInput.value = Identificacion;
        EstadoInput.checked = Estado; // Establecer el valor de Estado según el parámetro pasado
    } else {
        modalTitle.textContent = 'Registrar Datos';
        recordId.value = '';
        userIdInput.value = '';
        $("#userId").trigger("change");
        idAreaInput.value = '';
        $("#idArea").trigger("change");
        identificacionInput.value = '';
        EstadoInput.checked = false; // Dejar como false por defecto en nuevo registro
    }

    var myModal = new bootstrap.Modal(document.getElementById('detellePersonas-modal'));
    myModal.show();
}

function mostrarDetalle(Id, UserName, AreaName, userId,idArea, Identificacion, Estado) {
    const modalBody = document.querySelector('#dialog1 .modal-body');
    modalBody.innerHTML = `
        <p><strong>Id:</strong> ${Id}</p>
        <p><strong>Nombre:</strong> ${UserName}</p>
        <p><strong>Área:</strong> ${AreaName}</p>
        <p><strong>Identificación:</strong> ${Identificacion}</p>
        <p><strong>Estado:</strong> ${Estado == 'True' ? 'Activo' : 'Inactivo'}
    `;
    $('#dialog1').modal('show');
}


function EliminarArea(Id, Nombre) {
    if (confirm('¿Estás seguro de que deseas eliminar el área ' + Nombre + '?')) {
        $.ajax({
            url: "/Personas/eliminararea",
            type: 'DELETE',
            data: { deleteid: Id },
            success: function (usuario) {
                if (usuario) {
                    $(`tr#${id}`).remove();
                    alert('El área fue eliminada correctamente.');
                } else {
                    alert('Hubo un error al eliminar el área.');
                }
            },
            error: function (xhr, status, error) {
                console.log('Error:', error);
                alert('Hubo un error al intentar eliminar el área.');
            }
        });
    }
}

function ListaArea() {
    $.ajax({
        url: "/Personas/List",
        type: "GET",
        success: function (data) {
            const select = $('#idArea');
            select.empty();
            select.append('<option value="">Selecciona</option>');
            data.forEach(function (item) {
                const option = $('<option></option>')
                    .val(item.id)
                    .text(`${item.nombre}`);
                select.append(option);
            });
        },
        error: function (xhr, status, error) {
            console.log('Error:', error);
            alert('Hubo un error al cargar los datos.');
        }
    });
}
function ListaPersonas() {
    $.ajax({
        url: "/Personas/ListaPersonas",
        type: "GET",
        success: function (data) {
            const select = $('#userId');
            select.empty();
            select.append('<option value="">Selecciona</option>');
            data.forEach(function (item) {
                const option = $('<option></option>')
                    .val(item.id)
                    .text(`${item.normalizedUserName}`);
                select.append(option);
            });
        },
        error: function (xhr, status, error) {
            console.log('Error:', error);
            alert('Hubo un error al cargar los datos.');
        }
    });
}
