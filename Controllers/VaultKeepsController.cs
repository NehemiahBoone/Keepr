using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _service;
        public VaultKeepsController(VaultKeepsService service)
        {
            _service = service;
        }

    [HttpPost]
    public ActionResult<string> CreateVaultKeep([FromBody] VaultKeep newVK)
    {
        try
        {
            _service.CreateVaultKeep(newVK);
            return Ok("Created vaultkeep... from vaultKeepsController");
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult<string> DeleteVaultKeep(int id)
    {
        try
        {
            _service.DeleteVaultKeep(id);
            return Ok("Deleted vaultkeep... from vaultKeepsController");
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    }
}