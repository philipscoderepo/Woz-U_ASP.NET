using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Lesson9_HandsOn.Models;

namespace Lesson9_HandsOn.Models
{
    public partial class LastDeletedContext : DbContext
    {
        public LastDeletedContext()
        {
        }

        public LastDeletedContext(DbContextOptions<LastDeletedContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<LastDeleted> LastDeleted { get; set; }
    }
}
