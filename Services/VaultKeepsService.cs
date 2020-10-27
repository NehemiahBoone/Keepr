using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }
        internal void CreateVaultKeep(VaultKeep newVK)
        {
            _repo.CreateVaultKeep(newVK);
        }

        internal void DeleteVaultKeep(int id)
        {
            var data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id... from vaultkeepsService");
            }
            _repo.DeleteVaultKeep(id);
        }
    }
}