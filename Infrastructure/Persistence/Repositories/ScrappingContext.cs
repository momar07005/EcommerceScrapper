using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Repositories
{
    public class ScrappingContext : DbContext
    {
        public ScrappingContext(DbContextOptions<ScrappingContext> options) : base(options)
        {
        }

        public DbSet<Request> Requests { get; set; }
        
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().HasKey(review => review.ReviewId);
        }
    }
}
