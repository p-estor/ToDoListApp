$(document).ready(function () {
    // Manejo de clic en los enlaces de paginación
    $(document).on("click", ".pagination-link", function (event) {
        event.preventDefault(); // Evita la recarga de la página

        var pageNumber = $(this).data("page"); // Obtener el número de página desde el atributo data-page

        $.ajax({
            url: '/Tareas/Index', // Asegúrate de que esta URL es correcta
            type: 'GET',
            data: { pageNumber: pageNumber }, // Envía el número de página
            success: function (response) {
                // Reemplazar el contenido del contenedor con las tareas obtenidas
                $("#tareas-container").html(response); // Reemplaza el contenido en lugar de agregar
            },
            error: function () {
                alert("Could not load the task list.");
            }
        });
    });
});
