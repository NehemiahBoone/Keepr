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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _service;
        public VaultsController(VaultsService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetVaultById(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_service.GetById(userInfo.Id, id))
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newVault.CreatorId = userInfo.Id;
                Vault created = _service.CreateVault(newVault);
                created.Creator = userInfo;
                return Ok(created);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> DeleteVault(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_service.DeleteVault(id, userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}