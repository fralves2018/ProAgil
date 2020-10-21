using Microsoft.EntityFrameworkCore;
using ProAgil.WebAPI2.Model;

namespace ProAgil.WebAPI2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<Evento> Eventos { get; set; }
    }
}