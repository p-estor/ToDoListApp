namespace TaskManagerCore.Models
{
    public class Paginacion
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }

        // Propiedad para calcular el total de páginas
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalItems / ItemsPerPage);
            }
        }
    }

    public class PaginacionViewModel
    {
        public List<Tareas> Tareas { get; set; } // Lista de tareas
        public Paginacion Paginacion { get; set; } // Detalles de paginación
    }
}
