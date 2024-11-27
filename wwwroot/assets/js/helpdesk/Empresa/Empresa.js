function detelleEmpresa(accion, Id = null, Nombre = '', NitEmpresa = '') {
    const modalTitle = document.getElementById('titelid-modal');
    const recordId = document.getElementById('recordId');
    const NombreInput = document.getElementById('Nombre');
    const NitEmpresaInput = document.getElementById('NitEmpresa');

    //Actualiza datos 
    if (accion === 'actualizar') {
        modalTitle.textContent = 'Actualizar Datos';
        recordId.value = Id;
        NombreInput.value = Nombre;
        $("#Nombre").trigger("change");
        NitEmpresaInput.value = NitEmpresa;
        $("#NitEmpresa").trigger("change");
    //Registra datos
    } else {
        modalTitle.textContent = 'Registrar Datos';
        recordId.value = '';
        NombreInput.value = '';
        $("#Nombre").trigger("change");
        NitEmpresaInput.value = '';
        $("#NitEmpresa").trigger("change");
    }

    var myModal = new bootstrap.Modal(document.getElementById('detelleEmpresa-modal'));
    myModal.show();
}


//Visualizacion de datos
function mostrarDetalle(Id, Nombre, NitEmpresa) {
    const modalBody = document.querySelector('#dialog1 .modal-body');
    modalBody.innerHTML = `
        <p><strong>Id:</strong> ${Id}</p>
        <p><strong>Nombre:</strong> ${Nombre}</p>
        <p><strong>NIT:</strong> ${NitEmpresa}</p>
    `;
    $('#dialog1').modal('show');
}


//Elimina regsitros de la tabla
function EliminarEmpresa(Id, Nombre) {
    if (confirm('¿Estás seguro de que deseas eliminar el área ' + Nombre + '?')) {
        $.ajax({
            url: "/Empresa/EliminarEmpresa",
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
