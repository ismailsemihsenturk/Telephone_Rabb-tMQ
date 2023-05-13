using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Telephone_ISS_ACS.UserService.DataAccessLayer.Entities
{
    public class ContactInformation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public Guid? PhoneBookEntryId { get; set; }
        [ForeignKey("PhoneBookEntryId")]
        [JsonIgnore]
        public PhoneBookEntry PhoneBookEntry { get; set; }

        [Required]
        public ContactType Type { get; set; }
       
        public string Info { get; set; }

    }
}
