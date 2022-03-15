using BioscoopPortaalMVC.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BioscoopPortaalMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}


        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>(m => {
                m.Property(m => m.Name).IsRequired().HasMaxLength(100);

                m.HasOne(m => m.Director)
                    .WithMany(d => d.Movies)
                    .HasForeignKey(m => m.DirectorId);
            });
        }

    }


}
