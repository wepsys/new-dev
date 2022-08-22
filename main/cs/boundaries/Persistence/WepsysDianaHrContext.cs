using Microsoft.EntityFrameworkCore;
using System;
using Wepsys.DianaHr.Core.Entities;

namespace Wepsys.DianaHr.Persistence
{
    /// <summary>
    /// Represents Wepsys DianaHr DbContext
    /// </summary>
    public sealed class WepsysDianaHrContext : DbContext
    {
        /// <summary>
        /// Creates an instance of <see cref="WepsysDianaHrContext"/>
        /// </summary>
        /// <param name="options">DbContext options</param>
        public WepsysDianaHrContext(DbContextOptions<WepsysDianaHrContext> options) : 
            base(options)
        {            
            User = Set<User>();
        }

        /// <summary>
        /// Represents a <see cref="DbSet{TEntity}"/> of type <see cref="User"/>
        /// </summary>
        public DbSet<User> User { get; }

        /// <inheritdoc cref="DbContext.OnModelCreating"/>   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder, nameof(modelBuilder));

            modelBuilder.HasDefaultSchema("dianahr");

            modelBuilder.Entity<User>();
        }
    }
}
