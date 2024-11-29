document.getElementById('generate-pdf2').addEventListener('click', function () {
    var element = document.getElementById('content-to-pdf');  // Seleccionar el div con el contenido HTML

    if (element) {
        // Obtener la fecha actual
        var today = new Date();
        var date = today.toLocaleDateString();  // Formato de la fecha según configuración local

        var docDefinition = {
            header: {
                columns: [
                    { text: 'Reporte de Centro de Costos', style: 'header' },
                    { text: 'Fecha: ' + date, style: 'date', alignment: 'right' }
                ]
            },
            content: [],
            styles: {
                header: {
                    fontSize: 18,
                    bold: true,
                    alignment: 'left',
                    margin: [20, 10, 0, 10]  // Ajusta el margen según sea necesario
                },
                date: {
                    fontSize: 12,
                    alignment: 'right',
                    margin: [0, 10, 20, 10]  // Ajusta el margen según sea necesario
                },
                tableHeader: {
                    bold: true,
                    fontSize: 14,
                    color: 'black',
                    fillColor: '#d9e1f2',  // Color de fondo para los encabezados de las columnas
                    alignment: 'center',
                    margin: [0, 5, 0, 5]
                },
                tableBody: {
                    fontSize: 12,
                    color: 'black',
                    padding: 5,
                    alignment: 'center'
                },
                title: {
                    fontSize: 16,
                    bold: true,
                    alignment: 'center',
                    margin: [0, 10, 0, 10]
                }
            }
        };

        // Obtener todas las secciones (divs) con las tablas
        var sections = element.querySelectorAll('.col-12');

        sections.forEach(function (section, index) {
            // Buscar el primer encabezado h1 dentro de cada sección
            var titleElement = section.querySelector('h1');
            var sectionTitle = titleElement ? titleElement.innerText : "Sección " + (index + 1); // Usar nombre por defecto si no hay h1

            // Añadir el nombre de la sección como un título
            docDefinition.content.push({
                text: sectionTitle,
                style: 'title'
            });

            // Obtener la tabla de esta sección
            var table = section.querySelector('table');
            var tableData = [];

            // Extraer las filas de la tabla
            var rows = table.querySelectorAll('tr');
            rows.forEach(function (row, rowIndex) {
                var rowData = [];
                var cells = row.querySelectorAll('th, td'); // Para las celdas, incluidas las de los encabezados
                cells.forEach(function (cell) {
                    rowData.push(cell.innerText); // Agregar el texto de cada celda
                });
                tableData.push(rowData);
            });

            // Agregar la tabla al contenido del PDF centrada
            docDefinition.content.push({
                table: {
                    headerRows: 1,  // Indica que la primera fila es de encabezados
                    body: tableData,
                    layout: {
                        fillColor: function (rowIndex, node, columnIndex) {
                            return rowIndex === 0 ? '#d9e1f2' : null;  // Color de fondo para los encabezados
                        },
                        hLineWidth: function (i, node) {
                            return 1;  // Grosor de las líneas horizontales
                        },
                        vLineWidth: function (i, node) {
                            return 1;  // Grosor de las líneas verticales
                        },
                        hLineColor: function (i, node) {
                            return '#000000';  // Color de las líneas horizontales
                        },
                        vLineColor: function (i, node) {
                            return '#000000';  // Color de las líneas verticales
                        },
                        paddingLeft: function (i, node) { return 10; },
                        paddingRight: function (i, node) { return 10; },
                        paddingTop: function (i, node) { return 5; },
                        paddingBottom: function (i, node) { return 5; },
                    }
                },
                style: 'tableBody',
                alignment: 'center'  // Centrar la tabla
            });

            // Añadir un salto de línea entre las tablas para separar visualmente
            docDefinition.content.push({ text: '\n' });
        });

        // Generar el PDF
        pdfMake.createPdf(docDefinition).download('ReporteCentrocostos_' + date + '.pdf');
    } else {
        console.error('Element with id "content-to-pdf" not found.');
    }
});
// Al hacer clic en el botón, generamos el PDF
//document.getElementById('generate-pdf').addEventListener('click', function () {
//    var element = document.getElementById('content-to-pdf');  // Seleccionar el div que contiene el contenido HTML

//    // Uso html2pdf.js para generar el PDF
//    html2pdf()
//        .from(element)  // Convertir el contenido del div seleccionado
//        .save('Registro.pdf');  // Nombre del archivo PDF a descargar
//});

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

//$('#generate-pdf2222').on('click', function () {
//    // Seleccionar el contenedor de contenido HTML
//    var element = document.getElementById('content-to-pdf');

//    // Usar el contenido de `#content-to-pdf` para crear un PDF
//    var content = [];

//    // Iterar sobre cada sección dentro del `div#content-to-pdf`
//    $(element).find('.row .col-12').each(function () {
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

//    // Definir el formato del PDF
//    var docDefinition = {
//        content: content
//    };

//    // Generar y descargar el PDF
//    pdfMake.createPdf(docDefinition).download('contenido.pdf');
//});


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


