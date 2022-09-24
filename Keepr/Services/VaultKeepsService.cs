using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepRepo;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository vaultKeepRepo, VaultsService vaultsService)
    {
      _vaultKeepRepo = vaultKeepRepo;
      _vaultsService = vaultsService;
    }

    // NOTE You get the cultMember (vaultKeep) from the Cult (vault)
    // FIXME errors up the wazoo 
    internal VaultKeep Create(VaultKeep vaultKeep)
    {
      _vaultsService.GetById(vaultKeep.VaultId);
      return _vaultKeepRepo.Create(vaultKeep);
    }

    internal string Remove(int id, Account user)
    {
      VaultKeep vaultKeep = _vaultKeepRepo.GetById(id);
      if (vaultKeep == null)
      {
        throw new Exception("No VaultKeep by that Id.");
      }
      Vault vault = _vaultsService.GetById(vaultKeep.VaultId);
      if (vault.CreatorId != user.Id)
      {
        throw new Exception("You cannot do that.");
      }
      _vaultKeepRepo.Remove(id);
      return $"You have removed something.";
    }
  }
}