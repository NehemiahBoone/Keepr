using System;
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

        private readonly string populateCreator = @"SELECT
            keep.*,
            profile.*
            FROM keeps keep
            JOIN profiles profile on keep.creatorId = profile.Id ";

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Keep> GetAll()
        {
            string sql = populateCreator;
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
        }

        internal Keep GetById(int keepId)
        {
            string sql = populateCreator + "WHERE keep.id = @keepId";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { keepId }, splitOn: "id").FirstOrDefault();
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
            string sql = @"
                UPDATE keeps
                SET
                creatorId = @CreatorId,
                name = @Name,
                description = @Description,
                img = @Img,
                views = @Views,
                shares = @Shares,
                keeps = @Keeps
                WHERE id = @Id;";
            _db.Execute(sql, editedKeep);
            return editedKeep;
        }

        internal void DeleteKeep(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @id";
            _db.Execute(sql, new { id });
        }

        internal IEnumerable<KeepVaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            string sql = @"
            SELECT k.*, 
            vk.id AS VaultKeepId,
            profile.*
            FROM vaultkeeps vk
            JOIN keeps k ON k.Id = vk.keepId
            JOIN profiles profile on k.creatorId = profile.id
            WHERE vaultId = @id";
            return _db.Query<KeepVaultKeepViewModel, Profile, KeepVaultKeepViewModel>(sql, (k, profile) => { k.Creator = profile; return k; }, new { id }, splitOn: "id");
        }

        internal IEnumerable<Keep> GetAllByCreatorId(string id)
        {
            string sql = populateCreator + "WHERE creatorId = @id;";
            return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { id }, splitOn: "id");
        }

        internal Keep EditKeepViews(Keep editedKeep)
        {
            string sql = @"
                UPDATE keeps
                SET
                creatorId = @CreatorId,
                name = @Name,
                description = @Description,
                img = @Img,
                views = @Views,
                shares = @Shares,
                keeps = @Keeps
                WHERE id = @Id;";
            _db.Execute(sql, editedKeep);
            return editedKeep;
        }
    }
}