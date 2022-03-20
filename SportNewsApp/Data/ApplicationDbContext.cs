using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportNewsApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportNewsApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(a => a.Articles)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(builder);
        }
    }
}
