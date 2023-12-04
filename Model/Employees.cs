using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetAnnuaire.Model
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
        public int? SiteId { get; set; }
        public string? City { get; set; }
        [JsonProperty("services")]
        public string Service { get; set; }
        [NotMapped] // Cette annotation indique de ne pas mapper cette propriété à la base de données
        public bool IsSelected { get; set; }
    }

    public class CreateEmployeeModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MobilePhone { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public int ServiceId { get; set; }
        public int SiteId { get; set; }
    }
}
