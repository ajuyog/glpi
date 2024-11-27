// Al hacer clic en el botón, generamos el PDF
document.getElementById('generate-pdf').addEventListener('click', function () {
    var element = document.getElementById('content-to-pdf');  // Seleccionar el div que contiene el contenido HTML

    // Uso html2pdf.js para generar el PDF
    html2pdf()
        .from(element)  // Convertir el contenido del div seleccionado
        .save('Registro.pdf');  // Nombre del archivo PDF a descargar
});

//$('#generate-pdf2').on('click', function () {
//    // Array para almacenar el contenido del PDF
//    var content = [];

//    // Iterar sobre cada sección (Área TI, Área Comercial, etc.)
//    $('#content-to-pdf .row .col-12').each(function () {
//        var title = $(this).find('h1').text();  // Extraer el título (Área TI, Área Comercial, etc.)
//        var table = [];
//        var tableHeader = [];
//        var tableRows = [];

//        // Agregar título al contenido del PDF
//        content.push({ text: title, fontSize: 18, bold: true, margin: [0, 20, 0, 10] });

//        // Extraer las filas de la tabla
//        $(this).find('table thead tr th').each(function () {
//            tableHeader.push($(this).text());  // Extraer el texto de los encabezados
//        });

//        $(this).find('table tbody tr').each(function () {
//            var row = [];
//            $(this).find('td').each(function () {
//                row.push($(this).text());  // Extraer el texto de cada celda
//            });
//            tableRows.push(row);  // Agregar la fila
//        });

//        // Si tenemos una tabla, agregarla al contenido del PDF
//        if (tableHeader.length > 0 && tableRows.length > 0) {
//            table.push(tableHeader);  // Agregar encabezados
//            table = table.concat(tableRows);  // Agregar las filas

//            // Agregar la tabla al contenido
//            content.push({
//                table: {
//                    headerRows: 1,
//                    body: table
//                },
//                layout: 'lightHorizontalLines',  // Para un estilo simple
//                margin: [0, 0, 0, 20]
//            });
//        }
//    });

//    // Definimos el formato del PDF
//    var docDefinition = {
//        content: content
//    };

//    // Generar y descargar el PDF
//    pdfMake.createPdf(docDefinition).download('contenido.pdf');
//});

$('#generate-pdf2222').on('click', function () {
    // Seleccionar el contenedor de contenido HTML
    var element = document.getElementById('content-to-pdf');

    // Usar el contenido de `#content-to-pdf` para crear un PDF
    var content = [];

    // Iterar sobre cada sección dentro del `div#content-to-pdf`
    $(element).find('.row .col-12').each(function () {
        var title = $(this).find('h1').text();  // Extraer el título (Área TI, Área Comercial, etc.)
        var table = [];
        var tableHeader = [];
        var tableRows = [];

        // Agregar título al contenido del PDF
        content.push({ text: title, fontSize: 18, bold: true, margin: [0, 20, 0, 10] });

        // Extraer las filas de la tabla
        $(this).find('table thead tr th').each(function () {
            tableHeader.push($(this).text());  // Extraer el texto de los encabezados
        });

        $(this).find('table tbody tr').each(function () {
            var row = [];
            $(this).find('td').each(function () {
                row.push($(this).text());  // Extraer el texto de cada celda
            });
            tableRows.push(row);  // Agregar la fila
        });

        // Si tenemos una tabla, agregarla al contenido del PDF
        if (tableHeader.length > 0 && tableRows.length > 0) {
            table.push(tableHeader);  // Agregar encabezados
            table = table.concat(tableRows);  // Agregar las filas

            // Agregar la tabla al contenido
            content.push({
                table: {
                    headerRows: 1,
                    body: table
                },
                layout: 'lightHorizontalLines',  // Para un estilo simple
                margin: [0, 0, 0, 20]
            });
        }
    });

    // Definir el formato del PDF
    var docDefinition = {
        content: content
    };

    // Generar y descargar el PDF
    pdfMake.createPdf(docDefinition).download('contenido.pdf');
});


//document.getElementById('generate-pdf2').addEventListener('click', function () {
//    var element = document.getElementById('content-to-pdf');  // Select the div with the content

//    // Check if the element exists
//    if (element) {
//        var docDefinition = {
//            content: [  // Fix the typo 'ontent' to 'content'
//                {
//                    text: element.innerText,  // Extract text content from the element
//                    fontSize: 15
//                }
//            ]
//        };

//        // Generate the PDF
//        pdfMake.createPdf(docDefinition).download('mi_archivo.pdf');
//    } else {
//        console.error('Element with id "contenido" not found.');
//    }
//});

document.getElementById('generate-pdf2').addEventListener('click', function () {
    var element = document.getElementById('content-to-pdf');  // Seleccionar el div con el contenido HTML

    if (element) {
        var docDefinition = {
            content: [
                {
                    text: element.innerText,  // Solo texto (sin formato ni estructura)
                    fontSize: 15
                }
            ]
        };

        // Aquí, si necesitas aplicar más estructura HTML, puedes construir el contenido más detallado
        // Ejemplo para listas:
        var lists = ["hola", "jose"];
        var ul = element.querySelectorAll("ul");
        ul.forEach(function (ulElement) {
            var list = { ul: [] };  // Crear una lista
            ulElement.querySelectorAll("li").forEach(function (liElement) {
                list.ul.push({ text: liElement.innerText });  // Agregar elementos de lista
            });
            lists.push(list);
        });

        // Agregar las listas al documento (si existen)
        docDefinition.content.push(...lists);

        // Generar el PDF
        pdfMake.createPdf(docDefinition).download('mi_archivo.pdf');
    } else {
        console.error('Element with id "content-to-pdf" not found.');
    }
});


