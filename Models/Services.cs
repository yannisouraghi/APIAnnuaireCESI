using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIAnnuaire.Models
{
    public class Services
    {
        [Key]
        public int ServiceId { get; set; }
        [JsonProperty("service")]
        public string? Service { get; set; }
    }
}
