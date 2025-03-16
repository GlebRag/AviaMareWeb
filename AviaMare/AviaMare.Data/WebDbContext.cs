using Microsoft.EntityFrameworkCore;
using AviaMare.Data.Models;
using static System.Net.Mime.MediaTypeNames;
using System;
using Microsoft.Identity.Client.AppConfig;

namespace AviaMare.Data
{
    public class WebDbContext : DbContext
    {

        public const string CONNECTION_STRING = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = AviaMare; Integrated Security = True;";
        public DbSet<UserData> Users { get; set; }
        public DbSet<PlaneData> Planes { get; set; }
        public DbSet<TicketData> Tickets { get; set; }

        public DbSet<ChatMessageData> ChatMessages { get; set; }

        public WebDbContext() { }

        public WebDbContext(DbContextOptions<WebDbContext> contextOptions)
            : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
            // base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TicketData>()
                .HasMany(x => x.Buyers)
                .WithOne(x => x.Ticket)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TicketData>()
                .HasMany(x => x.UsersWhoLikeIt)
                .WithMany(x => x.TicketsWhichUsersLike);

            modelBuilder.Entity<UserData>()
                .HasMany(x => x.ChatMessages)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
