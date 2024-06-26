using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using CodeWorks.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly VaultsService _vaultsService;
    private readonly Auth0Provider _auth0Provider;

    public AccountController(AccountService accountService, VaultsService vaultsService, Auth0Provider auth0Provider)
    {
      _accountService = accountService;
      _vaultsService = vaultsService;
      _auth0Provider = auth0Provider;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // NOTE similar to AllSpice Recipes in AccountController example
    [HttpGet("vaults")]
    [Authorize]
    public async Task<ActionResult<List<Vault>>> GetAccountVaults()
    {
      try
      {
        Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        List<Vault> vaults = _vaultsService.GetAccountVaults(userInfo.Id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}