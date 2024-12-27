document.getElementById('generate-pdf2').addEventListener('click', function () {
    var element = document.getElementById('content-to-pdf');

    // Mostrar contenido si está oculto
    element.style.display = 'block';

    html2pdf()
        .from(element)
        .save('Registro.pdf')
        .catch(function (error) {
            console.error("Error al generar el PDF:", error);
        })
        .finally(function () {
            // Volver a ocultar el contenido después de generar el PDF
            element.style.display = 'none';
        });
});

document.getElementById('preview-pdf-btn').addEventListener('click', function () {
    var element = document.getElementById('content-to-pdf');
    var iframe = document.getElementById('pdf-preview-iframe');

    // Mostrar contenido si está oculto
    element.style.display = 'block';

    html2pdf()
        .from(element)
        .outputPdf('datauristring')
        .then(function (pdfAsString) {
            iframe.src = pdfAsString;

            // Mostrar el modal con el iframe
            var modal = new bootstrap.Modal(document.getElementById('pdf-preview-modal'));
            modal.show();
        })
        .catch(function (error) {
            console.error("Error al previsualizar el PDF:", error);
        })
        .finally(function () {
            // Volver a ocultar el contenido después de previsualizar
            element.style.display = 'none';
        });
});