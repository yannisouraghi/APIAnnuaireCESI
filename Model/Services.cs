using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAnnuaire.Model
{
    public class Services
    {
        [Key]
        public int ServiceId { get; set; }
        public string? Service { get; set; }
    }
}
