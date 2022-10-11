using System;
using System.Collections.Generic;
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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;
    private readonly VaultKeepsService _vaultKeepsService;
    private readonly Auth0Provider _auth0Provider;

    public VaultsController(VaultsService vaultsService, VaultKeepsService vaultKeepsService, Auth0Provider auth0Provider)
    {
      _vaultsService = vaultsService;
      _vaultKeepsService = vaultKeepsService;
      _auth0Provider = auth0Provider;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetById(int id)
    {
      try
      {
        Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        Vault vault = _vaultsService.GetById(id, user?.Id);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<VaultKeepViewModel>>> GetVaultKeeps(int id)
    {
      try
      {
        Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        List<VaultKeepViewModel> vaultKeepViewModel = _vaultKeepsService.GetVaultKeeps(id, user?.Id);
        return Ok(vaultKeepViewModel);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
      try
      {
        Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        vaultData.CreatorId = user.Id;
        Vault vault = _vaultsService.Create(vaultData);
        vault.Creator = user;
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault vaultData)
    {
      try
      {
        Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        vaultData.Id = id;
        Vault vault = _vaultsService.Update(vaultData, user);
        return Ok(vault);
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
        string message = _vaultsService.Delete(id, user);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}