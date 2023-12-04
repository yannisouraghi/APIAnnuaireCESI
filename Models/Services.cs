using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIAnnuaire.Models
{
    public class Services
    {
        [Key]
        public int ServiceId { get; set; }
        public string? Service { get; set; }
    }
}
