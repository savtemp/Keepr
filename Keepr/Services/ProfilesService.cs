using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _profilesRepo;

    public ProfilesService(ProfilesRepository profilesRepo)
    {
      _profilesRepo = profilesRepo;
    }

    internal Profile GetProfileById(string id)
    {
      Profile profile = _profilesRepo.GetProfileById(id);
      if (profile == null)
      {
        throw new Exception($"{profile.Id} does not exist.");
      }
      return profile;
    }
  }
}