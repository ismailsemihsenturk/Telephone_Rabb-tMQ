using Microsoft.AspNetCore.Mvc;
using Telephone_ISS_ACS.ReportService.DataAccessLayer.Context;
using Telephone_ISS_ACS.ReportService.DataAccessLayer.Concrete;
using System.Threading.Tasks;
using System.Collections.Generic;
using Telephone_ISS_ACS.ReportService.DataAccessLayer.Entities;
using System;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace Telephone_ISS_ACS.ReportService.PresentationLayer.Controllers
{
    public class ReportController : Controller
    {
        private ReportContext context;
        private ReportServices reportService;

        public ReportController(ReportContext _context)
        {
            context = _context;
            reportService = new ReportServices(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("GetAllReports")]
        [HttpGet]
        public async Task<IActionResult> GetAllReports()
        {
            try
            {
                var reportList = await reportService.GetAllReports();
                return Ok(reportList);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ReportDTO _reportDTOEntry)
        {
            try
            {
                await reportService.Add(_reportDTOEntry);

                var data = "Your report is ready.";
                HttpClient client = new HttpClient();
                Uri uri = new Uri($"https://localhost:44352/Ready");
                HttpResponseMessage response = await client.PostAsJsonAsync(uri, data);

                return StatusCode(201);
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
                await reportService.Delete(_id);
                return StatusCode(201);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }

        [Route("Preparing")]
        [HttpPost]
        public async Task<IActionResult> Preparing([FromBody] string message)
        {
            try
            { 
                return Ok(message);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }

        [Route("Ready")]
        [HttpPost]
        public async Task<IActionResult> Ready([FromBody] string message)
        {
            try
            {
                return Ok(message);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }

        }




    }
}
