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
        public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep newVK)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVK.CreatorId = userInfo.Id;
                return Ok(_service.CreateVaultKeep(newVK, userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteVaultKeep(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_service.DeleteVaultKeep(id, userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}