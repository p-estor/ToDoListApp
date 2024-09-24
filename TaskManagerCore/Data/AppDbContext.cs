
using Microsoft.EntityFrameworkCore;
using TaskManagerCore.Models;

namespace TaskManagerCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        // Definimos la tabla tareas
        public DbSet<Tareas> Tareas { get; set; }
    }
}
