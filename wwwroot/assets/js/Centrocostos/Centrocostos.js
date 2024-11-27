// Al hacer clic en el botón, generamos el PDF
document.getElementById('generarPdfBtn').addEventListener('click', function () {
    var element = document.getElementById('contenido');  // Seleccionar el div que contiene el contenido HTML

// Uso html2pdf.js para generar el PDF
html2pdf()
.from(element)  // Convertir el contenido del div seleccionado
.save('Registro.pdf');  // Nombre del archivo PDF a descargar
});
