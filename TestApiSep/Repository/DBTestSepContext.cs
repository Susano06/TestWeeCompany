using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestApiSep.Models;

namespace TestApiSep.Repository
{
    // La clase hereda de DbContext (clase base de Entity Framework Core)
    public class DBTestSepContext : DbContext
    {
        //Inicializa el contexto con la configuración proporcionada
        public DBTestSepContext(DbContextOptions<DBTestSepContext> options) : base(options) { }

        public DbSet<Participante> Participantes { get; set; }
    }
}
