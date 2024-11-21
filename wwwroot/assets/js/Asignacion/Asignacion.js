document.addEventListener("DOMContentLoaded", function () {
    ListaPersonas();
    ListaEnsamble();
});

function detelleAsignacion(accion, Id = null, IdPersona = '', IdEnsamble = '') {
    // Obtener los elementos del DOM
    const modalTitle = document.getElementById('titelid-modal');
    const recordId = document.getElementById('recordId');
    const IdPersonaInput = document.getElementById('IdPersona');
    const IdEnsambleInput = document.getElementById('IdEnsamble');

    if (accion === 'actualizar') {
        modalTitle.textContent = 'Actualizar Datos';
        recordId.value = Id;
        IdPersonaInput.value = IdPersona;
        IdEnsambleInput.value = IdEnsamble;

    } else {

        modalTitle.textContent = 'Registrar Datos';
        recordId.value = '';
        IdPersonaInput.value = '';
        $("#IdPersona").trigger("change"); // Resetear el select
        IdEnsambleInput.value = '';
        $("#IdEnsamble").trigger("change"); // Resetear el select
    }

    // Mostrar el modal
    var myModal = new bootstrap.Modal(document.getElementById('detelleAsignacion'));
    myModal.show();
}


function mostrarDetalle(Id, IdPersona, IdEnsamble) {
    const modalBody = document.querySelector('#dialog1 .modal-body');
    modalBody.innerHTML = `
        <p><strong>Id:</strong> ${Id}</p>
        <p><strong>Tio de elemento:</strong> ${IdPersona}</p>
        <p><strong>Marca:</strong> ${IdEnsamble}</p>
    `;
    $('#dialog1').modal('show');
}

function EliminarArea(Id, IdElementType) {
    if (confirm('¿Estás seguro de que deseas eliminar el área ' + IdElementType + '?')) {
        $.ajax({
            url: "/Asignacion/Eliminararea",
            type: 'DELETE',
            data: { deleteid: Id },
            success: function (usuario) {
                if (usuario) {
                    $(`tr#${Id}`).remove();
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
function ListaPersonas() {

    $.ajax({
        url: "/Asignacion/ListaPersonas",
        type: "GET",
        success: function (data) {
            const select = $('#IdPersona');
            select.empty();
            select.append('<option value="">Selecciona</option>');
            data.forEach(function (item) {

                const option = $('<option></option>')
                    .val(item.id)
                    .text(`${item.userName}`);
                select.append(option);
            });
        },
        error: function (xhr, status, error) {
            console.log('Error:', error);
            alert('Hubo un error al cargar los datos.');
        }
    });
}
function ListaEnsamble() {
    $.ajax({
        url: "/Asignacion/ListaEnsamble",
        type: "GET",
        success: function (data) {
            console.log(data);
            const select = $('#IdEnsamble');
            select.empty();
            select.append('<option value="">Selecciona</option>');
            data.forEach(function (item) {

                const option = $('<option></option>')
                    .val(item.id)
                    .text(`${item.idElementType}`);
                select.append(option);
            });
        },
        error: function (xhr, status, error) {
            console.log('Error:', error);
            alert('Hubo un error al cargar los datos.');
        }
    });
}



