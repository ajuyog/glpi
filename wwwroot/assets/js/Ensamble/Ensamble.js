document.addEventListener("DOMContentLoaded", function () {
    ListatipoElement();
    ListaMarca();
});

function detelleEnsamble(accion, Id = null, IdElementType = '', IdMarca = '', NumeroSerial = '', Estado = '', Descripcion = '', Renting = '') {
    // Obtener los elementos del DOM
    const modalTitle = document.getElementById('titelid-modal');
    const recordId = document.getElementById('recordId');
    const IdElementTypeInput = document.getElementById('IdElementType');
    const IdMarcaInput = document.getElementById('IdMarca');
    const NumeroSerialInput = document.getElementById('NumeroSerial');
    const EstadoInput = document.getElementById('Estado');
    const DescripcionInput = document.getElementById('Descripcion');
    const RentingInput = document.getElementById('Renting');


    if (accion === 'actualizar') {
        modalTitle.textContent = 'Actualizar Datos';
        recordId.value = Id; 
        IdElementTypeInput.value = IdElementType;
        $("#IdElementType").trigger("change"); 
        IdMarcaInput.value = IdMarca;
        $("#IdMarca").trigger("change"); 
        NumeroSerialInput.value = NumeroSerial;
        EstadoInput.checked = Estado === 'true';
        DescripcionInput.value = Descripcion;
        RentingInput.checked = Renting === 'true'; // Aseguramos que el valor es booleano

    } else {

        modalTitle.textContent = 'Registrar Datos';
        recordId.value = ''; 
        IdElementTypeInput.value = '';
        $("#IdElementType").trigger("change"); // Resetear el select
        IdMarcaInput.value = '';
        $("#IdMarca").trigger("change"); // Resetear el select
        NumeroSerialInput.value = '';       
        EstadoInput.checked = false;
        DescripcionInput.value = '';      
        RentingInput.checked = false;
    }

    // Mostrar el modal
    var myModal = new bootstrap.Modal(document.getElementById('detelleEnsamble'));
    myModal.show();
}


function mostrarDetalle(Id, IdElementType, IdMarca, NumeroSerial, Estado, Descripcion, Renting) {
    const modalBody = document.querySelector('#dialog1 .modal-body');
    modalBody.innerHTML = `
        <p><strong>Id:</strong> ${Id}</p>
        <p><strong>Tio de elemento:</strong> ${IdElementType}</p>
        <p><strong>Marca:</strong> ${IdMarca}</p>
        <p><strong>Número Serial:</strong> ${NumeroSerial}</p>
        <p><p><strong>Estado:</strong> ${Estado == 'True' ? 'Activo' : 'Inactivo'}
        <p><strong>Descripción:</strong> ${Descripcion}</p>
        <p><strong>Renting:</strong> ${Renting == 'True' ? 'Própiedad de Confival' : 'Propiedad de TMK'}</p>
    `;
    $('#dialog1').modal('show');
}

function EliminarArea(Id, IdElementType) {
    if (confirm('¿Estás seguro de que deseas eliminar el área ' + IdElementType + '?')) {
        $.ajax({
            url: "/Ensamble/Eliminararea",
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
function ListatipoElement() {

    $.ajax({
        url: "/Ensamble/ListatipoElement",
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
function ListaMarca() {
    $.ajax({
        url: "/Ensamble/ListaMarca",
        type: "GET",
        success: function (data) {
            console.log(data);
            const select = $('#IdMarca');
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



