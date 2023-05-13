using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephone_ISS_ACS.ReportService.DataAccessLayer.Context;
using Telephone_ISS_ACS.ReportService.DataAccessLayer.Entities;

namespace Telephone_ISS_ACS.ReportService.DataAccessLayer.Concrete
{
    public class ReportServices
    {
        private ReportContext context;

        public ReportServices(ReportContext _context)
        {
            context = _context;
        }

        public async Task<List<ReportDTO>> GetAllReports()
        {
             return await context.ReportDTO.ToListAsync();
        }

        public async Task Add(ReportDTO _entry)
        {
            await context.ReportDTO.AddAsync(_entry);
            await context.SaveChangesAsync();
        }

  
        public async Task Delete(Guid _id)
        {
            var tempReport = context.ReportDTO.Where(r => r.Id == _id).FirstOrDefault();
            context.ReportDTO.Remove(tempReport);
            await context.SaveChangesAsync();
        }

       
    }
}
