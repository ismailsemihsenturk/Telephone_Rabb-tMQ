using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Telephone_ISS_ACS.UserService.DataAccessLayer.Concrete;
using Telephone_ISS_ACS.UserService.DataAccessLayer.Context;
using Telephone_ISS_ACS.UserService.DataAccessLayer.Entities;

namespace Telephone_ISS_ACS_UserService.PresentationLayer.Controllers
{

    public class UserController : Controller
    {
        private PhoneBookService phoneBookService;
        private PhoneBookContext context;

        public UserController(PhoneBookContext _context)
        {
            context = _context;
            phoneBookService = new PhoneBookService(context);
        }


        public IActionResult Index()
        {
            return View();
        }

        [Route("GetAllEntries")]
        [HttpGet]
        public async Task<IActionResult> GetAllEntries()
        {
            try
            {
                var phoneBookList = await phoneBookService.GetAllEntries();
                return Ok(phoneBookList);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add(PhoneBookEntry _phoneBookEntry, [FromBody] List<ContactInformation> _contactInfoEntry)
        {
            try
            {
                var entry = await phoneBookService.Add(_phoneBookEntry, _contactInfoEntry);
                return Ok(entry);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }

        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update(PhoneBookEntry _phoneBookEntry, [FromBody] List<ContactInformation> _contactInfoEntry)
        {
            try
            {
                var entry = await phoneBookService.Update(_phoneBookEntry, _contactInfoEntry);
                return Ok(entry);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid _id)
        {
            try
            {
                await phoneBookService.Delete(_id);
                return StatusCode(201);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }


        [Route("GetReportList")]
        [HttpGet]
        public async Task<IActionResult> GetReportList()
        {
            try
            {
                var reportList = await phoneBookService.ReportList();
                var reportBytes = JsonSerializer.SerializeToUtf8Bytes(reportList);
                await ReportProducer.SendToConsumer(reportBytes);
                return StatusCode(201);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
