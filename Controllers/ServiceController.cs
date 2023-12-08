using APIAnnuaire.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAnnuaire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly APIDbContext _context;

        public ServiceController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet("Services")]
        public async Task<ActionResult<IEnumerable<Services>>> GetServices()
        {
            try
            {
                var services = await _context.Services.ToListAsync();

                if (services == null || services.Count == 0)
                {
                    return NotFound("Aucun service trouvé.");
                }

                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne du serveur s'est produite : {ex.Message}");
            }
        }

        [HttpGet("Services/{id}")]
        public async Task<ActionResult<Services>> GetServiceById(int id)
        {
            try
            {
                var service = await _context.Services.FindAsync(id);

                if (service == null)
                {
                    return NotFound($"Aucun service trouvé avec l'ID {id}.");
                }

                return Ok(service);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne du serveur s'est produite : {ex.Message}");
            }
        }

        [HttpPost("AddService")]
        public async Task<ActionResult<Services>> AddService([FromBody] Services newService)
        {
            try
            {
                if (newService == null)
                {
                    return BadRequest("Données du service non fournies.");
                }

                _context.Services.Add(newService);
                await _context.SaveChangesAsync();

                return Ok(newService);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne du serveur s'est produite : {ex.Message}");
            }
        }

        [HttpPut("UpdateService/{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] Services updatedService)
        {
            try
            {
                var existingService = await _context.Services.FindAsync(id);

                if (existingService == null)
                {
                    return NotFound($"Aucun service trouvé avec l'ID {id}.");
                }

                existingService.Service = updatedService.Service;
                // Ajoutez d'autres propriétés à mettre à jour selon vos besoins.

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne du serveur s'est produite : {ex.Message}");
            }
        }


        [HttpDelete("DeleteService/{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                var serviceToDelete = await _context.Services.FindAsync(id);

                if (serviceToDelete == null)
                {
                    return NotFound($"Aucun service trouvé avec l'ID {id}.");
                }
                var employees = await _context.Employees.Where(e => e.ServiceId == id).ToListAsync();
                if (employees.Any())
                {
                    return BadRequest($"Le site {serviceToDelete.Service} ne peut pas être supprimé car il est associé à {employees.Count} employés.");
                }

                _context.Services.Remove(serviceToDelete);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur interne du serveur s'est produite : {ex.Message}");
            }
        }
    }
}
