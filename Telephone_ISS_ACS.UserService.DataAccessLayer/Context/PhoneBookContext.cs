using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephone_ISS_ACS.UserService.DataAccessLayer.Entities;

namespace Telephone_ISS_ACS.UserService.DataAccessLayer.Context
{
    public class PhoneBookContext : DbContext
    {
        public DbSet<PhoneBookEntry> PhoneBook { get; set; }
        public DbSet<ContactInformation> ContactInformation { get; set; }

        public PhoneBookContext(DbContextOptions<PhoneBookContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=phonebook;Username=postgres;Password=123456");



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhoneBookEntry>().ToTable("PhoneBookEntry");
            modelBuilder.Entity<ContactInformation>().ToTable("ContactInformation");

            modelBuilder.Entity<PhoneBookEntry>()
                .HasMany(e => e.ContactInformation)
                .WithOne(e => e.PhoneBookEntry)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(e => e.PhoneBookEntryId)
                .HasConstraintName("PhoneBookEntry_Id");
            
        }
    }

}

