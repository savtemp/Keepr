using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _keepsRepo;

    public KeepsService(KeepsRepository keepsRepo)
    {
      _keepsRepo = keepsRepo;
    }

    internal List<Keep> GetAll()
    {
      return _keepsRepo.GetAll();
    }

    internal Keep GetById(int id)
    {
      Keep keep = _keepsRepo.GetById(id);
      if (keep == null)
      {
        throw new Exception("There is no keep by that id.");
      }
      return keep;
    }

    internal Keep Create(Keep newKeep)
    {
      return _keepsRepo.Create(newKeep);
    }

    internal Keep Update(Keep update, string userId)
    {
      Keep original = GetById(update.Id);
      if (original.CreatorId != userId)
      {
        throw new Exception("You are not the creator of this keep, you cannot update this keep.");
      }
      original.Name = update.Name ?? original.Name;
      original.Description = update.Description ?? original.Description;
      original.Img = update.Img ?? original.Img;
      return _keepsRepo.Update(original);
    }

    internal string Delete(int id, string userId)
    {
      Keep original = GetById(id);
      if (original.CreatorId != userId)
      {
        throw new Exception("You are not the creator of this keep, you cannot delete this keep.");
      }
      _keepsRepo.Delete(id);
      return $"The Keep {original.Name} has been deleted.";
    }
  }
}