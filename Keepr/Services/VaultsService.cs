using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsRepo;

    public VaultsService(VaultsRepository vaultsRepo)
    {
      _vaultsRepo = vaultsRepo;
    }

    internal Vault GetById(int id, string userId)
    {
      Vault vault = _vaultsRepo.GetById(id);
      if (vault == null)
      {
        throw new Exception($"No Vault at id: {id}.");
      }
      if (vault.isPrivate == true && vault.CreatorId != userId)
      {
        throw new Exception($"{vault.Name} is private.");
      }
      return vault;
    }

    internal Vault Create(Vault vaultData)
    {
      return _vaultsRepo.Create(vaultData);
    }

    internal Vault Update(Vault update, Account user)
    {
      Vault original = GetById(update.Id, user.Id);
      if (original.CreatorId != user.Id)
      {
        throw new Exception($"Cannot update {original.Name} you are not the creator.");
      }
      original.Name = update.Name ?? original.Name;
      original.Description = update.Description ?? original.Description;
      original.Img = update.Img ?? original.Img;
      original.isPrivate = update.isPrivate ?? original.isPrivate;

      return _vaultsRepo.Update(original);
    }

    internal string Delete(int id, Account user)
    {
      Vault original = GetById(id, user.Id);
      if (original.CreatorId != user.Id)
      {
        throw new Exception($"Cannot delete {original.Name}, you are not the creator.");
      }
      _vaultsRepo.Delete(id);
      return $"{original.Name} was deleted.";
    }

    // NOTE we should not see a users private vaults
    // FIXME always passing userId as a parameter, need to change this so that you don't see private vaults
    internal List<Vault> GetProfileVaults(string userId)
    {
      List<Vault> vaults = _vaultsRepo.GetProfileVaults(userId);
      // NOTE per Mick code notes this line is supposed to filter out private vaults that you are not the owner of
      vaults = vaults.FindAll(v => v.isPrivate == true || v.CreatorId == userId);
      return vaults;
    }

    internal List<Vault> GetAccountVaults(string id)
    {
      List<Vault> vaults = _vaultsRepo.GetProfileVaults(id);
      return vaults;
    }
  }
}