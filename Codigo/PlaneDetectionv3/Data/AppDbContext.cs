using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlaneDetectionv2.Models;

namespace PlaneDetectionv2.Data;

public class AppDbContext : DbContext
{
    public DbSet<Prediction> Predictions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite("Data Source=predictions.db")
            .LogTo(Console.WriteLine, LogLevel.Information) // Add this line for logging
            .EnableSensitiveDataLogging(); // Optional for debugging purposes;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prediction>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ImageData).HasColumnType("BLOB");
            entity.Property(e => e.PredictionData).IsRequired();
            entity.Property(e => e.Timestamp).IsRequired();
        });
    }
}