document.addEventListener("DOMContentLoaded", function () {
    ListaTipoelemento();
});
function detelletipoelemet(accion, id = null, Nombre = '', IdElementType = '') {
    const modalTitle = document.getElementById('titelid-modal');
    const recordId = document.getElementById('recordId');
    const NombreInput = document.getElementById('Nombre');
    const IdElementTypeInput = document.getElementById('IdElementType');

    if (accion === 'actualizar') {
        modalTitle.textContent = 'Actualizar Datos';
        recordId.value = id;
        NombreInput.value = Nombre;
        IdElementTypeInput.value = IdElementType;
    } else {
        modalTitle.textContent = 'Registrar Datos';
        recordId.value = '';  
        NombreInput.value = '';
        IdElementTypeInput.value = '';
        $("#IdElementType").trigger("change");
    }

    
    var myModal = new bootstrap.Modal(document.getElementById('detelletipoelemet-modal'));
    myModal.show();
}


function mostrarDetalle(Id, Nombre, IdElementType) {
    const modalBody = document.querySelector('#dialog1 .modal-body');
    modalBody.innerHTML = `
        <p><strong>Id:</strong> ${Id}</p>
        <p><strong>Nombre:</strong> ${Nombre}</p>
        <p><strong>Tipo de elemento:</strong> ${IdElementType}</p>
    `;
    $('#dialog1').modal('show');
}

function EliminarElemento(Id, nombre) {
    if (confirm('¿Estás seguro de que deseas eliminar el área ' + nombre + '?')) {
        $.ajax({
            url: "/ElementType/EliminarElemento",
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
function ListaTipoelemento() {
    $.ajax({
        url: "/ElementType/ListaTipoelemento",
        type: "GET",
        success: function (data) {
            const select = $('#IdElementType');
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


