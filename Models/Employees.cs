using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace APIAnnuaire.Models
{
    public class Employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Department { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MobilePhone { get; set; }
        public string? JobTitle { get; set; }
        public string? JobDescription { get; set; }

        // Clé étrangère pour Site
        public int? SiteId { get; set; }
        public Sites? Sites { get; set; }

        // Clé étrangère pour Service
        public int? ServiceId { get; set; }
        public Services? Services { get; set; }
    }

    public class CreateEmployeeModel
    {
        [Required]
        [JsonProperty("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonProperty("lastName")]
        public string LastName { get; set; } = string.Empty;

        [JsonProperty("department")]
        public string Department { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;

        [JsonProperty("mobilePhone")]
        public string MobilePhone { get; set; } = string.Empty;

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; } = string.Empty;

        [JsonProperty("jobDescription")]
        public string JobDescription { get; set; } = string.Empty;

        [JsonProperty("siteId")]
        public int? SiteId { get; set; }

        [JsonProperty("serviceId")]
        public int? ServiceId { get; set; }
    }



}
