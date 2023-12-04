using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAnnuaire.Model
{
    public class Sites
    {
        [Key]
        public int SiteId { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Service { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
    }
}
