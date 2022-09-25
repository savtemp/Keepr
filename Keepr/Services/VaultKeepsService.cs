using System;
using System.Collections.Generic;
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

    internal List<VaultKeepViewModel> GetVaultKeeps(int id, string userId)
    {
      Vault vault = _vaultsService.GetById(id, userId);
      List<VaultKeepViewModel> vaultKeeps = _vaultKeepRepo.GetVaultKeeps(id);
      if (vault == null)
      {
        throw new Exception($"No Vault at id: {id}.");
      }
      if (vault.isPrivate == true || vault.CreatorId != userId)
      {
        throw new Exception($"{vault.Name} is private, you cannot view {vault.Name} keeps.");
      }
      return vaultKeeps;
    }

    // NOTE You get the cultMember (vaultKeep) from the Cult (vault)
    internal VaultKeep Create(VaultKeep vaultKeepData, string userId)
    {
      Vault vault = _vaultsService.GetById(vaultKeepData.VaultId, userId);
      if (vault.CreatorId != userId)
      {
        throw new Exception($"You cannot create a Keep in {vault.Name}, you are not the Vault creator.");
      }
      return _vaultKeepRepo.Create(vaultKeepData);
    }


    internal string Delete(int id, string userId)
    {
      VaultKeep vaultKeep = _vaultKeepRepo.GetById(id);
      if (vaultKeep == null)
      {
        throw new Exception("No VaultKeep by that Id.");
      }
      Vault vault = _vaultsService.GetById(vaultKeep.VaultId, userId);
      if (vault.CreatorId != userId)
      {
        throw new Exception($"Cannot delete {vault.Name}, you are not the creator.");
      }
      _vaultKeepRepo.Delete(id);
      return $"{vault.Name} was deleted.";
    }
  }
}