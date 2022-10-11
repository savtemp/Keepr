using System;
using System.Threading.Tasks;
using CodeWorks.Utils;
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
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly Auth0Provider _auth0Provider;

    public VaultKeepsController(VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider)
    {
      _vaultKeepsService = vaultKeepsService;
      _auth0Provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
    {
      try
      {
        Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        vaultKeepData.CreatorId = user.Id;
        VaultKeep newVaultKeep = _vaultKeepsService.Create(vaultKeepData, user.Id);
        return newVaultKeep;
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        string message = _vaultKeepsService.Delete(id, user.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}