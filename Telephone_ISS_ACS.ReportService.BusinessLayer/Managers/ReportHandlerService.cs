using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telephone_ISS_ACS.ReportService.DataAccessLayer.Entities;
using Telephone_ISS_ACS.UserService.DataAccessLayer.Entities;
using System.Net.Http.Headers;

namespace Telephone_ISS_ACS.ReportService.BusinessLayer.Managers
{
    public class ReportHandlerService
    {
        private ReportDTO reportDTO;

        public ReportHandlerService()
        {

        }

        public async Task HandleReport(List<PhoneBookEntry> _reportList)
        {
            HttpClient client = new HttpClient();
           
            List<ReportDTO> reportDtoList = new List<ReportDTO>();

            var locationList = _reportList.Select(r => r.ContactInformation.Where(c => c.Type == ContactType.Location).Select(c => c.Info).First()).ToList();
            var locationEntryIdList = _reportList.Select(r => r.ContactInformation.Where(c => c.Type == ContactType.Location).Select(c => c.PhoneBookEntryId).First()).ToList();

            locationList.ForEach(location =>
            {
                reportDTO = new ReportDTO();
                reportDTO.Id = Guid.NewGuid();
                reportDTO.Location = location;
                reportDTO.UserCount = _reportList.Sum(entry => entry.ContactInformation.Count(info => info.Type == ContactType.Location && info.Info == location));
                reportDTO.PhoneNumberCount = _reportList.Where(entry => entry.ContactInformation.Any(info => info.Type == ContactType.Location && info.Info == location))
                    .SelectMany(entry => entry.ContactInformation)
                    .Count(info => info.Type == ContactType.PhoneNumber);
                reportDtoList.Add(reportDTO);
            });

            try
            {           
                Uri uri =  new Uri("https://localhost:44352/Add");

                for(int i = 0; i< reportDtoList.Count; i++)
                {                
                    var data = reportDtoList[i];                           
                    HttpResponseMessage response = await client.PostAsJsonAsync(uri, data);
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }




            //var tempWhere = _reportList.Where(entry => entry.ContactInformation.Any(info => info.Type == ContactType.Location && info.Info == "ANKARA"));

            //var tempWhereSelectMany = _reportList.Where(entry => entry.ContactInformation.Any(info => info.Type == ContactType.Location && info.Info == "ANKARA"))
            //        .SelectMany(entry => entry.ContactInformation);

            //var tempFinal = _reportList.Where(entry => entry.ContactInformation.Any(info => info.Type == ContactType.Location && info.Info == "ANKARA"))
            //        .SelectMany(entry => entry.ContactInformation)
            //        .Count(info => info.Type == ContactType.PhoneNumber);

        }
    }
}
