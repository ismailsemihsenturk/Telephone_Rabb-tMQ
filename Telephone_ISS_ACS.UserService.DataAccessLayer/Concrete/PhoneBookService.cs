using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephone_ISS_ACS.UserService.DataAccessLayer.Entities;
using Telephone_ISS_ACS.UserService.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Telephone_ISS_ACS.UserService.DataAccessLayer.Abstract;
using System.Web.Http;

namespace Telephone_ISS_ACS.UserService.DataAccessLayer.Concrete
{
    public class PhoneBookService : IPhoneBookService
    {
        private PhoneBookContext context;

        public PhoneBookService(PhoneBookContext _context)
        {
            context = _context;
        }

        public async Task<List<PhoneBookEntry>> GetAllEntries()
        {
            return await context.PhoneBook.Include(p => p.ContactInformation).ToListAsync();
        }


        public async Task<PhoneBookEntry> Add(PhoneBookEntry _phoneBookEntry, List<ContactInformation> _contactInfoEntry)
        {
            _phoneBookEntry.Id = Guid.NewGuid();
            await context.PhoneBook.AddAsync(_phoneBookEntry);

            _contactInfoEntry.ForEach(async c => { 
                c.PhoneBookEntryId = _phoneBookEntry.Id; 
                c.PhoneBookEntry = _phoneBookEntry; 
                c.Id = Guid.NewGuid(); 
                await context.ContactInformation.AddAsync(c); 
            });
            await context.SaveChangesAsync();

            _phoneBookEntry.ContactInformation = _contactInfoEntry;
            context.PhoneBook.Update(_phoneBookEntry);
            await context.SaveChangesAsync();

            return _phoneBookEntry;
        }

        public async Task<PhoneBookEntry> Update(PhoneBookEntry _phoneBookEntry, List<ContactInformation> _contactInfoEntry)
        {
            var tempPhoneEntry = context.PhoneBook.Where(p => p.Id ==_phoneBookEntry.Id).FirstOrDefault();
            tempPhoneEntry.FirstName = _phoneBookEntry.FirstName;
            tempPhoneEntry.LastName = _phoneBookEntry.LastName;
            tempPhoneEntry.Company = _phoneBookEntry.Company;
            tempPhoneEntry.ContactInformation = _contactInfoEntry;

            _contactInfoEntry.ForEach(c =>
            {
                var tempPhone = context.PhoneBook.Where(p => p.Id == c.PhoneBookEntryId).FirstOrDefault();
                c.PhoneBookEntry = tempPhone;
                context.ContactInformation.Update(c);
            });
            context.PhoneBook.Update(tempPhoneEntry);
            await context.SaveChangesAsync();

            return tempPhoneEntry;
        }

        public async Task Delete(Guid _id)
        {
            var tempEntry = context.PhoneBook.Where(p => p.Id == _id).FirstOrDefault();
            context.PhoneBook.Remove(tempEntry);
            await context.SaveChangesAsync();
        }

        public async Task<List<PhoneBookEntry>> ReportList()
        {
            var reportIdList = await context.ContactInformation.Where(c => c.Type == ContactType.Location && c.Info != null).Select(c => c.PhoneBookEntryId).ToListAsync();
            var reportList = await context.PhoneBook.Where(p => reportIdList.Contains(p.Id)).Include(p => p.ContactInformation).ToListAsync();
            return reportList;
        }
    }

}
