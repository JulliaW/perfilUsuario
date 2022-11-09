using Microsoft.EntityFrameworkCore;
using perfilUsuario.Models;

namespace perfilUsuario.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
