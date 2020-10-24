using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<ActionResult<string>> CreateVaultKeep([FromBody] VaultKeep newVK)
    {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            newVK.CreatorId = userInfo.Id;
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