using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Web;

namespace Facebook.Models.BaseEntity
{
    public class FacebookDbContext : DbContext
    {
        public FacebookDbContext() : base("FacebookConnectionString")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer<FacebookDbContext>(null);
            modelBuilder.Entity<Connection>()
             .HasKey(x => x.ID);
            modelBuilder.Entity<Connection>()
                .Property(x => x.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Connection>()
                .Property(x => x.UserName)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute { IsUnique = true }))
                .HasMaxLength(20);

            base.OnModelCreating(modelBuilder);
        }
        
        public DbSet<Comment> Comments {get;set;}
        public DbSet<Like> Likes {get;set;}
        public DbSet<Post> Posts {get;set;}
        public DbSet<RelationShip> RelationShips {get;set;}
        public DbSet<Share> Shares {get;set;}
        public DbSet<Status> Statuses {get;set;}
        public DbSet<User> Users { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Connection> Connections { get; set; }
    }
}