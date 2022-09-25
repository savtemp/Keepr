using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep GetById(int id)
    {
      string sql = @"
      SELECT * FROM vaultkeeps WHERE id = @id;
      ";
      return _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
    }

    // NOTE GetCultists example isn't many to many need to add another table that joins Account,VaultKeeps, and Keeps (vaultKeeps already has the vaultId)
    // NOTE Keeps need to come before VaultKeeps because you need to access Keeps before you can access the VaultKeepViewModel
    internal List<VaultKeepViewModel> GetVaultKeeps(int id)
    {
      string sql = @"
      SELECT
        a.*,
        k.*,
        vk.id AS vaultKeepId
      FROM vaultkeeps vk
      JOIN keeps k ON vk.keepId = k.id
      JOIN accounts a ON vk.creatorId = a.id
      WHERE vk.vaultId = @id;
      ";
      return _db.Query<Profile, VaultKeepViewModel, VaultKeepViewModel>(sql, (profile, vaultKeepViewModel) =>
      {
        vaultKeepViewModel.Creator = profile;
        return vaultKeepViewModel;
      }, new { id }).ToList();
    }

    internal VaultKeep Create(VaultKeep vaultKeepData)
    {
      string sql = @"
      INSERT INTO vaultkeeps
      (creatorId, vaultId, keepId)
      VALUES
      (@creatorId, @vaultId, @keepId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = id;
      return vaultKeepData;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaultkeeps WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }
  }
}