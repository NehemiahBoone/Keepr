using System;
using System.Data;
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
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => {vault.Creator = profile; return vault}, new {id}, splitOn: "id").FirstOrDefault();
        }

        internal int Create(Vault newVault)
        {
            throw new NotImplementedException();
        }

        internal void DeleteVault(int id)
        {
            throw new NotImplementedException();
        }
    }
}