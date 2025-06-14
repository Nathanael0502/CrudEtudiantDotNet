namespace MonProjetMVC.Data;

using Microsoft.EntityFrameworkCore;
using MonProjetMVC.Models;

public class AppDbContext:DbContext
{
    public DbSet<Etudiant>  Etudiants {get; set;}
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {}
}