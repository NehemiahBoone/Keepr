using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        private readonly string populateCreator = @"
            SELECT
            keep.*,
            profile.*
            FROM keeps keep
            JOIN profiles profile on keep.creatorId = profile.Id";
        
        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Keep> GetAll()
        {
            string sql = populateCreator;
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => {keep.Creator = profile; return keep;}, splitOn: "id");
        }

        internal Keep GetById(int keepId)
        {
            throw new NotImplementedException();
        }

        internal int CreateKeep(Keep newKeep)
        {
            string sql = @"
                INSERT INTO keeps
                (creatorId, name, description, img, views, shares, keeps)
                VALUES
                (@CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
                SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newKeep);
        }

        internal Keep EditKeep(Keep editedKeep)
        {
            throw new NotImplementedException();
        }

        internal void DeleteKeep(int id)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<Keep> GetAllByCreatorId(string queryProfileId)
        {
            throw new NotImplementedException();
        }
    }
}