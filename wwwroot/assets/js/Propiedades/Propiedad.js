
document.addEventListener("DOMContentLoaded", function () {
    ListaEnsamble();
});
function detellepropiedades(accion, Id = null, Propiedad = '', IdEnsamble = '') {
    const modalTitle = document.getElementById('titelid-modal');
    const recordId = document.getElementById('recordId');
    const PropiedadInput = document.getElementById('Propiedad');
    const IdEnsambleInput = document.getElementById('IdEnsamble');

    if (accion === 'actualizar') {
        modalTitle.textContent = 'Actualizar Datos';
        recordId.value = Id;
        PropiedadInput.value = Propiedad;
        IdEnsambleInput.value = IdEnsamble;

    }
    else {
        modalTitle.textContent = 'Registrar Datos';
        recordId.value = '';
        PropiedadInput.value = '';
        IdEnsambleInput.value = '';

    }
    var myModal = new bootstrap.Modal(document.getElementById('detellepropiedades'));
    myModal.show();
}

function mostrarDetalle(Id, Propiedad, EnsambleName, IdEnsamble) {
    const modalBody = document.querySelector('#dialog1 .modal-body');
    modalBody.innerHTML = `
       <p><strong>Id:</strong> ${Id}</p>
       <p><strong>Propiedad:</strong> ${Propiedad}</p>
       <p><strong>Ensamble:</strong> ${EnsambleName}</p>
       `;
    $('#dialog1').modal('show');
}

function EliminarArea(Id, Propiedad) {
    if (confirm('¿Estás seguro de que deseas eliminar el área ' + Propiedad + '?')) {
        $.ajax({
            url: "/Propiedades/Eliminararea",
            type: 'DELETE',
            data: { deleteid: Id },
            success: function (usuario) {
                if (usuario) {
                    //$(`tr#${id}`).html("");
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

function ListaEnsamble() {
    $.ajax({
        url: "/Propiedades/ListaEnsamble",
        type: "GET",
        success: function (data) {
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

