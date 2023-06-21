using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApiEntities.Models;

namespace WebApiEntities.Context;

public partial class DbUsersContext : DbContext
{
    public DbUsersContext()
    {
    }

    public DbUsersContext(DbContextOptions<DbUsersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=.\\DB\\DbUsers.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Users_Id").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName).HasColumnName("First_Name");
            entity.Property(e => e.LastName).HasColumnName("Last_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
