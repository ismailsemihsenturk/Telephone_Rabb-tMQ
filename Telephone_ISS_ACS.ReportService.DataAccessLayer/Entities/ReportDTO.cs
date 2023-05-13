using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephone_ISS_ACS.ReportService.DataAccessLayer.Entities
{
    public class ReportDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
    
        public int PhoneNumberCount { get; set; }

        public int UserCount { get; set; }

        public string Location { get; set; }

    }
}
