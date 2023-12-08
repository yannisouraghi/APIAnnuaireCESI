using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIAnnuaire.Models;

namespace APIAnnuaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly APIDbContext _context; // Utilisation du contexte de base de données

        public EmployeeController(APIDbContext context) // Injection du contexte de base de données via le constructeur
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employees>> Get()
        {
            var employees = _context.Employees.ToList(); // Charger les employés depuis la base de données

            return employees;
        }

        [HttpGet("Search")]
        public ActionResult<IEnumerable<object>> SearchEmployees(string Lastname = null, string site = null, string service = null, string Firstname = null)
        {
            var query = _context.Employees
                .Include(e => e.Services)
                .Include(e => e.Sites)
                .AsQueryable();

            if (!string.IsNullOrEmpty(Lastname))
            {
                query = query.Where(e => EF.Functions.Like(e.LastName, $"%{Lastname}%"));
            }

            if (!string.IsNullOrEmpty(site))
            {
                query = query.Where(e => EF.Functions.Like(e.Sites.City, $"%{site}%"));
            }

            if (!string.IsNullOrEmpty(service))
            {
                query = query.Where(e => e.Services != null && EF.Functions.Like(e.Services.Service, $"%{service}%"));
            }
            if (!string.IsNullOrEmpty(Firstname))
            {
                query = query.Where(e => EF.Functions.Like(e.FirstName, $"%{Firstname}%"));
            }

            var employees = query.Select(e => new
            {
                e.EmployeeId,
                e.FirstName,
                e.LastName,
                e.Department,
                e.Email,
                e.PhoneNumber,
                e.MobilePhone,
                e.JobTitle,
                e.JobDescription,
                Services = e.Services != null ? e.Services.Service : null,
                City = e.Sites != null ? e.Sites.City : null,
                Adress = e.Sites != null ? e.Sites.Address : null,
                ZipCode = e.Sites != null ? e.Sites.ZipCode : null, 
                Country = e.Sites != null ? e.Sites.Country : null
            }).ToList();

            return employees;
        }

        [HttpPost("AddEmployee")]
        public ActionResult Post([FromBody] CreateEmployeeModel employees)
        {
                Employees employee = new Employees
                {
                    FirstName = employees.FirstName,
                    LastName = employees.LastName,
                    Department = employees.Department,
                    Email = employees.Email,
                    PhoneNumber = employees.PhoneNumber,
                    MobilePhone = employees.MobilePhone,
                    JobTitle = employees.JobTitle,
                    JobDescription = employees.JobDescription,
                    ServiceId = employees.ServiceId,
                    SiteId = employees.SiteId
                };

                // Ajoutez le nouvel employé à la table Employees
                _context.Employees.Add(employee);

                // Enregistrez les modifications dans la base de données
                _context.SaveChanges();

                // Renvoyez une réponse HTTP appropriée
                return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                // Recherchez l'employé avec l'ID spécifié
                var employee = _context.Employees.Find(id);

                // Si l'employé n'est pas trouvé, retournez une réponse NotFound
                if (employee == null)
                {
                    return NotFound();
                }

                // Supprimez l'employé de la table Employees
                _context.Employees.Remove(employee);

                // Enregistrez les modifications dans la base de données
                _context.SaveChanges();

                // Renvoyez une réponse HTTP appropriée
                return NoContent();
            }
            catch (Exception ex)
            {
                // Gérez les erreurs et renvoyez une réponse d'erreur appropriée si nécessaire
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        [HttpPut("UpdateEmployee/{id}")]
        public ActionResult UpdateEmployee(int id, [FromBody] CreateEmployeeModel updatedEmployee)
        {
            try
            {
                // Recherchez l'employé avec l'ID spécifié
                var employee = _context.Employees.Find(id);

                // Si l'employé n'est pas trouvé, retournez une réponse NotFound
                if (employee == null)
                {
                    return NotFound();
                }

                // Mettez à jour les propriétés de l'employé avec les nouvelles valeurs
                employee.FirstName = updatedEmployee.FirstName;
                employee.LastName = updatedEmployee.LastName;
                employee.Department = updatedEmployee.Department;
                employee.Email = updatedEmployee.Email;
                employee.PhoneNumber = updatedEmployee.PhoneNumber;
                employee.MobilePhone = updatedEmployee.MobilePhone;
                employee.JobTitle = updatedEmployee.JobTitle;
                employee.JobDescription = updatedEmployee.JobDescription;
                employee.ServiceId = updatedEmployee.ServiceId;
                employee.SiteId = updatedEmployee.SiteId;

                // Enregistrez les modifications dans la base de données
                _context.SaveChanges();

                // Renvoyez l'employé mis à jour
                return Ok(employee);
            }
            catch (Exception ex)
            {
                // Gérez les erreurs et renvoyez une réponse d'erreur appropriée si nécessaire
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }


    }
}
