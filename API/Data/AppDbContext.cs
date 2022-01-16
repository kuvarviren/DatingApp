﻿using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserLike> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserLike>().HasKey(k => new { k.SouceUserId, k.LikedUserId });
            modelBuilder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s => s.SouceUserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Message>()
                .HasOne(u=>u.Recipient)
                .WithMany(m=>m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Message>()
               .HasOne(u => u.Sender)
               .WithMany(m => m.MessagesSent)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
