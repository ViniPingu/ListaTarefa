using ListaTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaTarefas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }
        public DbSet<Tarefas> Tarefas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { CategoriaID = "trabalho", Nome = "Trabalho"},
                new Categoria { CategoriaID = "casa", Nome = "Casa" },
                new Categoria { CategoriaID = "faculdade", Nome = "Faculdade" },
                new Categoria { CategoriaID = "compras", Nome = "Compras" },
                new Categoria { CategoriaID = "academia", Nome = "Academia" }
                );

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusID = "aberto", Nome = "Aberto" },
                new Status { StatusID = "completo", Nome = "Completo"}
                );

            base.OnModelCreating(modelBuilder);


        }

    }
}
