document.addEventListener("DOMContentLoaded", function () {
    ListaIdensamble();
});

//Muestra la informacion de las modales para actualizar y registrar  
function detelleFacturacion(accion, id = null, VlrNeto = '', IdEnsamble = '', Descripcion = '', Fecha = '') {
    const modalTitle = document.getElementById('titelid-modal');
    const recordId = document.getElementById('recordId');
    const VlrNetoInput = document.getElementById('VlrNeto');
    const IdEnsambleInput = document.getElementById('IdEnsamble');
    const DescripcionInput = document.getElementById('Descripcion');
    const FechaInput = document.getElementById('Fecha');

    if (accion === 'actualizar') {
        modalTitle.textContent = 'Actualizar Datos';
        recordId.value = id;
        VlrNetoInput.value = VlrNeto;
        IdEnsambleInput.value = IdEnsamble;
        $(IdEnsambleInput).val(IdEnsamble).trigger('change');
        DescripcionInput.value = Descripcion;
        FechaInput.value = Fecha;
    } else {
        modalTitle.textContent = 'Registrar Datos';
        recordId.value = '';
        VlrNetoInput.value = '';
        IdEnsambleInput.value = '';
        $(IdEnsambleInput).val('').trigger('change');
        DescripcionInput.value = '';
        FechaInput.value = '';
    }

    // Mostrar el modal
    var myModal = new bootstrap.Modal(document.getElementById('detelleFacturacion-modal'));
    myModal.show();
}


//Muestra la informacion
function mostrarDetalle(Id, VlrNeto, IdEnsamble, Descripcion, Fecha) {
    const modalBody = document.querySelector('#dialog1 .modal-body');
    modalBody.innerHTML = `
        <p><strong>Id:</strong> ${Id}</p>
        <p><strong>Valor Neto:</strong> ${VlrNeto}</p>
        <p><strong>Ensamble:</strong> ${IdEnsamble}</p>
        <p><strong>Descripción:</strong> ${Descripcion}</p>
        <p><strong>Fecha:</strong> ${Fecha}</p>
    `;
    $('#dialog1').modal('show');
}
//Elimina el regsitro
function EliminarFactura(Id) {
    if (confirm('¿Estás seguro de que deseas eliminar el área ' + VlrNeto + '?')) {
        $.ajax({
            url: "/FacturacionTMK/EliminarFactura",
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
//Trae la lista para mostrarla en el selct 
function ListaIdensamble() {
    $.ajax({
        url: "/FacturacionTMK/ListaIdensamble",
        type: "GET",
        success: function (data) {
            const select = $('#IdEnsamble');
            select.empty();
            select.append('<option value="">Selecciona</option>');
            data.forEach(function (item) {
                const option = $('<option></option>')
                    .val(item.id)
                    .text(`${item.idElementType} - ${item.numeroSerial}`);
                select.append(option);
            });
        },
        error: function (xhr, status, error) {
            console.log('Error:', error);
            alert('Hubo un error al cargar los datos.');
        }
    });
}
