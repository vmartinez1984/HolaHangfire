using Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HolaHangfire.Repositorio
{
    public class AppDbContext: DbContext
    {
        private IConfiguration _configuration;

        public AppDbContext()
        {

        }

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString;

                connectionString = _configuration.GetConnectionString("DefaultConnection");
                //connectionString = "Server=localhost; Port=13306; Database=HelpDesk; Uid=root; Pwd=;";
                //win
                //connectionString = "Server=localhost; Port=3306; Database=Hangfire; Uid=root; Pwd=;";
                //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                //connectionString = "Server=.; Database=IntroAHangfire; user id=sa; pwd=macross#7";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<Persona> Persona { get; set; }
    }
}