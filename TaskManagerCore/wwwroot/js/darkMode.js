
document.addEventListener("DOMContentLoaded", function () {
    const toggleButton = document.getElementById("toggle-dark-mode");
    const body = document.body;

    // Verificar si el usuario ya tiene una preferencia de modo oscuro
    const darkMode = localStorage.getItem("dark-mode");

    if (darkMode === "enabled") {
        body.classList.add("dark-mode");
        toggleButton.textContent = "Light Mode";
    }

    toggleButton.addEventListener("click", function () {
        body.classList.toggle("dark-mode");

        // Guardar la preferencia en localStorage
        if (body.classList.contains("dark-mode")) {
            localStorage.setItem("dark-mode", "enabled");
            toggleButton.textContent = "Light Mode";
        } else {
            localStorage.setItem("dark-mode", "disabled");
            toggleButton.textContent = "Dark Mode";
        }
    });
});

