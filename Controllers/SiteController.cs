using APIAnnuaire.Models;
using APIAnnuaire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;


[ApiController]
[Route("api/[controller]")]
public class SiteController : ControllerBase
{
    private readonly APIDbContext _context;

    public SiteController(APIDbContext context)
    {
        _context = context;
    }

    [HttpGet("Sites")]
    public async Task<ActionResult<IEnumerable<Sites>>> GetSites()
    {
        try
        {
            var sites = await _context.Sites.ToListAsync();

            if (sites == null || !sites.Any())
            {
                return NotFound("Aucun site trouvé.");
            }

            return Ok(sites);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Une erreur interne du serveur s'est produite : {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Sites>> GetSiteById(int id)
    {
        try
        {
            var site = await _context.Sites.FindAsync(id);

            if (site == null)
            {
                return NotFound($"Aucun site trouvé avec l'ID {id}.");
            }

            return Ok(site);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Une erreur interne du serveur s'est produite : {ex.Message}");
        }
    }

    [HttpPost("AddSite")]
    public async Task<ActionResult<Sites>> AddSite([FromBody] Sites newSite)
    {
        try
        {
            if (newSite == null)
            {
                return BadRequest("Données du site non fournies.");
            }

            // Trouver le plus grand SiteId existant
            int maxSiteId = await _context.Sites.MaxAsync(s => s.SiteId);

            // Incrémenter le SiteId
            newSite.SiteId = maxSiteId + 1;

            // Ajouter le nouveau site
            _context.Sites.Add(newSite);
            await _context.SaveChangesAsync();

            return Ok(newSite);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Une erreur interne du serveur s'est produite : {ex.Message}");
        }
    }

    [HttpPut("UpdateSite/{id}")]
    public async Task<IActionResult> UpdateSite(int id, [FromBody] Sites updatedSite)
    {
        try
        {
            var existingSite = await _context.Sites.FindAsync(id);

            if (existingSite == null)
            {
                return NotFound($"Aucun site trouvé avec l'ID {id}.");
            }

            existingSite.City = updatedSite.City;
            // Ajoutez d'autres propriétés à mettre à jour selon vos besoins.

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Une erreur interne du serveur s'est produite : {ex.Message}");
        }
    }


    [HttpDelete("DeleteSite/{id}")]
    public async Task<ActionResult> DeleteSite(int id)
    {
        try
        {
            var siteToDelete = await _context.Sites.FindAsync(id);

            if (siteToDelete == null)
            {
                return NotFound($"Aucun site trouvé avec l'ID {id}.");
            }

            _context.Sites.Remove(siteToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Une erreur interne du serveur s'est produite : {ex.Message}");
        }
    }
}
