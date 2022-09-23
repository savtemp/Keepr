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
            a.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId;
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
        a.*
    FROM keeps k 
    JOIN accounts a ON a.id = k.creatorId
    WHERE k.id = @id;
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

    internal Keep Update(Keep keepData)
    {
      string sql = @"
      UPDATE keeps SET
      name = @name,
      description = @description,
      img = @img
      WHERE id = @id
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