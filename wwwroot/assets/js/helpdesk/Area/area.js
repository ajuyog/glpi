function mostrarFormulario(accion, Id = null, Nombre = '') {
    const modalTitle = document.getElementById('titelid-modal');
    const recordId = document.getElementById('recordId');
    const NombreInput = document.getElementById('Nombre');

    if (accion === 'actualizar') {
        modalTitle.textContent = 'Actualizar Datos';
        recordId.value = Id; 
        NombreInput.value = Nombre; 
    }
    else {
        modalTitle.textContent = 'Registrar Datos';
        recordId.value = ''; 
        NombreInput.value = ''; 

    }
    $('#detelle-modal').modal('show'); 
}

function mostrarDetalle(Id, Nombre) {
    const modalBody = document.querySelector('#dialog1 .modal-body');
    modalBody.innerHTML = `
                        <p><strong>Id:</strong> ${Id}</p>
                        <p><strong>Nombre:</strong> ${Nombre}</p>
                    `;
    $('#dialog1').modal('show');
}

function EliminarArea(id, Nombre) {
    if (confirm('¿Estás seguro de que deseas eliminar el área ' + Nombre + '?')) {
        $.ajax({
            url: "/Area/eliminararea",
            type: 'DELETE',
            data: { deleteid: id },
            success: function (usuario) {
                if (usuario) {
                    //$(`tr#${id}`).html("");
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

