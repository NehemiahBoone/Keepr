using System;
using System.Data;
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
        internal int CreateVaultKeep(VaultKeep newVK)
        {
            string sql = @"
            INSERT INTO vaultkeeps
            (creatorId, vaultId, keepId)
            VALUES
            (@CreatorId, @VaultId, @KeepId);
            SELECT LAST_INSERT_ID();";
            return _db.ExecuteScalar<int>(sql, newVK);
        }

        internal void DeleteVaultKeep(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }

        internal VaultKeep GetById(int id)
        {
            string sql = "SELECT * FROM vaultkeeps WHERE id = @id;";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }
    }
}