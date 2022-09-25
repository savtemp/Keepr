using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault GetById(int id)
    {
      string sql = @"
    SELECT 
        v.*,
        a.*
    FROM vaults v
    JOIN accounts a ON v.creatorId = a.id
    WHERE v.id = @id; 
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { id }).FirstOrDefault();

    }

    // REVIEW there is no Img on UML do I need to add it in - there is one in the postman test
    internal Vault Create(Vault vaultData)
    {
      string sql = @"
      INSERT INTO vaults
      (creatorId, name, description, img, isPrivate)
      VALUES
      (@creatorId, @name, @description, @img, @isPrivate);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      return vaultData;
    }

    internal Vault Update(Vault update)
    {
      string sql = @"
        UPDATE vaults SET
        name = @name,
        description = @description,
        img = @img,
        isPrivate = @isPrivate
        WHERE id = @id;
        ";
      _db.Execute(sql, update);
      return update;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaults WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }

    internal List<Vault> GetProfileVaults(string id)
    {
      string sql = @"
      SELECT * FROM vaults WHERE creatorId = @id;
      ";
      return _db.Query<Vault>(sql, new { id }).ToList();
    }
  }
}