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
    public DbSet<PropertyOwner> PropertyOwners { get; set; }
    public DbSet<PropertyItem> PropertyItems { get; set; }
    public DbSet<Repair> Repairs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Data Source=(local); Initial Catalog=technico-2024; Integrated Security = True; TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Adding Unique constraints to vat and e9
        modelBuilder
            .Entity<PropertyOwner>()
            .HasIndex(o => o.VAT)
            .IsUnique();

        modelBuilder
            .Entity<PropertyItem>()
            .HasIndex(i => i.PropertyIdentificationNumber)
            .IsUnique();

        // Setting PropertyOwnerId to null if the owner is deleted
        modelBuilder.Entity<Repair>()
        .HasOne(r => r.PropertyOwner)
        .WithMany(po => po.Repairs)
        .HasForeignKey(r => r.PropertyOwnerId)
        .OnDelete(DeleteBehavior.SetNull); 

        // Renaming the tables to plural
        modelBuilder.Entity<PropertyOwner>().ToTable("PropertyOwners");
        modelBuilder.Entity<PropertyItem>().ToTable("PropertyItems");
        modelBuilder.Entity<Repair>().ToTable("Repairs");
    }
}
