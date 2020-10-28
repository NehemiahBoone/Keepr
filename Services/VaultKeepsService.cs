using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsRepository _vaultsRepo;
        public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vaultsRepo)
        {
            _repo = repo;
            _vaultsRepo = vaultsRepo;
        }
        internal VaultKeep CreateVaultKeep(VaultKeep newVK, string userId)
        {
            Vault data = _vaultsRepo.GetById(newVK.VaultId);

            if (data.CreatorId != userId)
            {
                throw new Exception("You cannot add a keep to vault that you dont own... from vaultkeepsService");
            }

            newVK.Id = _repo.CreateVaultKeep(newVK);
            return newVK;
        }

        internal object DeleteVaultKeep(int id, string userId)
        {
            VaultKeep data = _repo.GetById(id);
            if (data == null)
            {
                throw new Exception("Invalid Id... from vaultkeepsService");
            }

            if (data.CreatorId != userId)
            {
                throw new Exception("Access Denied NOT YOURS... from vaultkeepsService");
            }
            _repo.DeleteVaultKeep(id);
            return "Successfully deleted... from vaultkeepsService";
        }
    }
}