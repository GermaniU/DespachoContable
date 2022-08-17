
$(document).ready(function () {
    $('#modalBusqueda').on('hidden.bs.modal', function () {
        $("#Nombre").val("");
        $(".drop").val('')
        $("#rfc").val("");
    })

    $("#buscar").modal('hide');
});