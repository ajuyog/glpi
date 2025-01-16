// JavaScript para manejar la apertura y cierre de la modal
const openModalBtn = document.getElementById('openModalBtn');
const closeModalBtn = document.getElementById('closeModalBtn');
const modal = document.getElementById('modalsystemTicket');

openModalBtn.addEventListener('click', () => {
    modal.style.display = 'flex'; // Mostrar la modal
});

closeModalBtn.addEventListener('click', () => {
    modal.style.display = 'none'; // Ocultar la modal
});

window.addEventListener('click', (e) => {
    if (e.target === modal) {
        modal.style.display = 'none';
    }
});
document.addEventListener('DOMContentLoaded', function () {
    const rows = document.querySelectorAll('.clickable-row');
    rows.forEach(row => {
        row.addEventListener('click', function () {
            const nombre = this.getAttribute('data-nombre');
            const solicitud = this.getAttribute('data-solicitud');
            const descripcion = this.getAttribute('data-descripcion');

            // Actualiza el contenido del modal
            document.getElementById('modalNombre').textContent = nombre;
            document.getElementById('modalSolicitud').textContent = solicitud;
            document.getElementById('modalDescripcion').textContent = descripcion;

            // Muestra el modal
            const modal = new bootstrap.Modal(document.getElementById('detalleModal'));
            modal.show();
        });
    });
});
//const solicitudSelect = document.getElementById('solicitud');
//const errorPortatilDiv = document.getElementById('errorPortatil');
//const errorTecladoMouseDiv = document.getElementById('errorTecladoMouse');
//const errormonitorDiv = document.getElementById('errormonitor');
//const erroraplicacionesEmailDiv = document.getElementById('erroraplicacionesEmail');
//const erroraplicacionesDiv = document.getElementById('errorSolicitudAplicaciones'); // Cambié el ID aquí

//solicitudSelect.addEventListener('change', () => {
//    console.log('Selector cambiado:', solicitudSelect.value); // Esto es para depuración.

//    // Oculta todos los selectores inicialmente.
//    errorPortatilDiv.classList.add('hidden');
//    errorTecladoMouseDiv.classList.add('hidden');
//    errormonitorDiv.classList.add('hidden');
//    erroraplicacionesEmailDiv.classList.add('hidden');
//    erroraplicacionesDiv.classList.add('hidden');

//    // Muestra los campos correspondientes
//    if (solicitudSelect.value === 'portatil') {
//        errorPortatilDiv.classList.remove('hidden');
//    } else if (solicitudSelect.value === 'tecladoMouse') {
//        errorTecladoMouseDiv.classList.remove('hidden');
//    } else if (solicitudSelect.value === 'monitor') {
//        errormonitorDiv.classList.remove('hidden');
//    } else if (solicitudSelect.value === 'aplicacionesEmail') {
//        erroraplicacionesEmailDiv.classList.remove('hidden');
//        erroraplicacionesDiv.classList.remove('hidden'); // Muestra ambos selectores
//    } else if (solicitudSelect.value === 'aplicaciones') {
//        erroraplicacionesDiv.classList.remove('hidden');
//        erroraplicacionesEmailDiv.classList.remove('hidden'); // Muestra ambos selectores
//    }
//});
function ListaSolicitud() {
    $.ajax({
        url: "/MesaAyuda/ListaSolicitud",
        type: "GET",
        success: function (data) {
            const select = $('#ListaSolicitud');
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