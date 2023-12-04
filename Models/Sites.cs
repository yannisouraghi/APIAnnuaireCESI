using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIAnnuaire.Models
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
