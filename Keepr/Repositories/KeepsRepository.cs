using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetAll()
    {
      string sql = @"
        SELECT 
            k.*,
            COUNT(vk.id) AS kept,
            a.*
        FROM keeps k
        LEFT JOIN vaultkeeps vk ON vk.KeepId = k.id
        JOIN accounts a ON a.id = k.creatorId
        GROUP BY(k.id);
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }).ToList();
    }

    internal Keep GetById(int id)
    {
      string sql = @"
    SELECT 
        k.*,
        COUNT(vk.id) AS kept,
        a.*
    FROM keeps k 
    LEFT JOIN vaultkeeps vk ON vk.KeepId = k.id
    JOIN accounts a ON a.id = k.creatorId
    WHERE k.id = @id
    GROUP BY(k.id);
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { id }).FirstOrDefault();
    }

    internal Keep Create(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (creatorId, name, description, img)
      VALUES
      (@creatorId, @name, @description, @img);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newKeep);
      newKeep.Id = id;
      return newKeep;
    }

    internal List<Keep> GetProfileKeeps(string id)
    {
      string sql = @"
      SELECT
        k.*,
        a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE creatorId = @id;
      ";
      return _db.Query<Keep, Account, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { id }).ToList();
    }

    internal Keep Update(Keep keepData)
    {
      string sql = @"
      UPDATE keeps SET
      name = @name,
      description = @description,
      img = @img,
      views = @views,
      shares = @shares
      WHERE id = @id;
      ";
      _db.Execute(sql, keepData);
      return keepData;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM keeps WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }
  }
}