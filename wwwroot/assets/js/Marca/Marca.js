function mostrarFormulario(accion, Id = null, Nombre = '', Activo = '') {
    const modalTitle = document.getElementById('titelid-modal');
    const recordId = document.getElementById('recordId');
    const NombreInput = document.getElementById('Nombre');
    const ActivoInput = document.getElementById('Activo');

    if (accion === 'actualizar') {
        modalTitle.textContent = 'Actualizar Datos';
        recordId.value = Id;
        NombreInput.value = Nombre;
        ActivoInput.checked = Activo; // Establecer el valor de Estado según el parámetro pasado
    } else {
        modalTitle.textContent = 'Registrar Datos';
        recordId.value = '';
        NombreInput.value = '';
        ActivoInput.checked = false; // Dejar como false por defecto en nuevo registro
    }

    $('#detelle-modal').modal('show');
}

function mostrarDetalle(Id, Nombre, Activo) {
    const modalBody = document.querySelector('#dialog1 .modal-body');
    modalBody.innerHTML = `
        <p><strong>Id:</strong> ${Id}</p>
        <p><strong>Nombre de área:</strong> ${Nombre}</p>
        <p><strong>Estado:</strong> ${Activo == 'True' ? 'Activo' : 'Inactivo'}
    `;
    $('#dialog1').modal('show');
}


function EliminarArea(Id, Nombre) {
    if (confirm('¿Estás seguro de que deseas eliminar el área ' + Nombre + '?')) {
        $.ajax({
            url: "/Marca/eliminararea",
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
