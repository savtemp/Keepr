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
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _keepsService;
    private readonly Auth0Provider _auth0Provider;

    public KeepsController(KeepsService keepsService, Auth0Provider auth0Provider)
    {
      _keepsService = keepsService;
      _auth0Provider = auth0Provider;
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAll()
    {
      try
      {
        List<Keep> keeps = _keepsService.GetAll();
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try
      {
        Keep keep = _keepsService.GetById(id);
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        newKeep.CreatorId = user.Id;
        Keep keep = _keepsService.Create(newKeep);
        keep.Creator = user;
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Update(int id, [FromBody] Keep update)
    {
      try
      {
        Account user = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
        update.Id = id;
        Keep keep = _keepsService.Update(update, user.Id);
        return Ok(keep);
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
        string message = _keepsService.Delete(id, user.Id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}