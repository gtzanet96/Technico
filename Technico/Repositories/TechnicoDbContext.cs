using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technico.Models;

namespace Technico.Repositories;

public class TechnicoDbContext : DbContext //εδώ ορίζουμε ποια απ τα μοντέλα που έχουμε θα γίνουν πίνακες
{
    public DbSet<PropertyOwner> PropertyOwner { get; set; }
    public DbSet<PropertyItem> PropertyItem { get; set; }
    public DbSet<Repair> Repair { get; set; }
    public DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Data Source=(local); Initial Catalog=technico-2024; Integrated Security = True; TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Repair>()
            .HasOne(r => r.PropertyOwner)
            .WithMany(po => po.Repairs)
            .HasForeignKey(r => r.PropertyOwnerId)
            .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete

        modelBuilder.Entity<Repair>()
            .HasOne(r => r.PropertyItem)
            .WithMany(pi => pi.Repairs)
            .HasForeignKey(r => r.PropertyItemId)
            .OnDelete(DeleteBehavior.Restrict); // Prevents cascading delete
    }
}
