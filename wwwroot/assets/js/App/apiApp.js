
$("#btn-datalle").on("click", function () {
    console.log("usuarios");

    $("#detelle-modal").modal("show");
})

$(".btn-datalle").on("click", function () {
    console.log("usuarios");
})


function detalle(id) {

    if (id == 0) {
        $("#titelid-modal").text(`Inserta Informacion Nueva`);
        $("#titelid-modal").text(`Inserta Informacion Nueva`);
        $("#guardar").removeClass("try");
        $("#cerrar").text("Cancelar");
        $("#detelle-modal").modal("show");
        var count = 0;
        for (var i = 0; i < 16 && count < 16; i++) {
            count++;
            var t = `codSecundario${count}`;
            $(`#${t}`).attr("disabled", false);
        }


    } else {
        $("#titelid-modal").text(`Detalle Del Id ${id}`);
        $.ajax({
            url: "/FromProof/obtedellausua",
            type: 'Get',
            data: { id: id },
            success: function (usuario) {

                console.log(usuario);

                if (usuario.id != null) {
                    var count = 0;
                    for (var i in usuario) {+
                        count++;
                        var t = `codSecundario${count}`;
                        $(`#${t}`).attr("disabled", true).val(usuario[t] || "");
                    }

                    $("#guardar").addClass("try");
                    $("#cerrar").text("Cerrar");
                    $("#detelle-modal").modal("show");
                }
                else {
                    alert("Error al obtener los datos del usuario.");
                }
            },
            error: function (xhr, status, error) {
                console.error("Error en la solicitud:", error);
                alert("Error al obtener los datos del usuario.");
            }
        });
    }
}



$("#detelle-modal").on("hidden.bs.modal", function () {
    // Limpiar el formulario
    $("#from-usuarios")[0].reset();
});
