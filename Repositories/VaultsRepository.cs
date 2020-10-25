using System;
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

        private readonly string populateCreator = @"SELECT
            vault.*,
            profile.*
            FROM vaults vault
            JOIN profiles profile on vault.creatorId = profile.Id ";
        
        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }
        
        internal Vault GetById(int id)
        {
            string sql = populateCreator + "WHERE vault.id = @id";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => {vault.Creator = profile; return vault;}, new {id}, splitOn: "id").FirstOrDefault();
        }

        internal int Create(Vault newVault)
        {
            string sql = @"
                INSERT INTO vaults
                (creatorId, name, description, isPrivate)
                VALUES
                (@CreatorId, @Name, @Description, @IsPrivate);
                SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newVault);
        }

        internal void DeleteVault(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id";
            _db.Execute(sql, new {id});
        }

        internal IEnumerable<Vault> GetAllByCreatorId(string id)
        {
            string sql = populateCreator + "WHERE creatorId = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => {vault.Creator = profile; return vault;}, new {id}, splitOn: "id");
        }
    }
}