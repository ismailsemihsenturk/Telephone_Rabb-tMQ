using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephone_ISS_ACS.ReportService.DataAccessLayer.Entities;

namespace Telephone_ISS_ACS.ReportService.DataAccessLayer.Context
{
    public class ReportContext : DbContext
    {
        public DbSet<ReportDTO> ReportDTO { get; set; }

        public ReportContext(DbContextOptions<ReportContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                 => optionsBuilder.UseNpgsql("Host=localhost;Database=report;Username=postgres;Password=123456");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportDTO>().ToTable("Report");
        }
    }
}
