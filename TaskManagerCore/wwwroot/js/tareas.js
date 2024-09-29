$(document).on('click', '.toggle-status', function (event) {
    event.preventDefault(); // Evita la recarga de la página

    var button = $(this); // El botón clickeado
    var tareaId = button.data("id"); // ID de la tarea que se va a modificar
    var statusCell = $("td.status[data-id='" + tareaId + "']"); // Celda de estado correspondiente

    $.ajax({
        url: '/Tareas/Completar/' + tareaId, // Usa la ruta correcta para alternar el estado
        type: 'POST',
        success: function (response) {
            // Cambia el estado de la tarea en la celda de estado
            if (response.isCompleted) {
                statusCell.text("Completed");
                button.removeClass("btn-success").addClass("btn-warning");
                button.text("Mark as pending"); // Cambia el texto del botón
            } else {
                statusCell.text("Waiting");
                button.removeClass("btn-warning").addClass("btn-success");
                button.text("Mark as completed"); // Cambia el texto del botón
            }
        },
        error: function () {
            alert("Error al cambiar el estado de la tarea.");
        }
    });
});
