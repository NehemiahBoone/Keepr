using System;
using System.Collections.Generic;
using System.Linq;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{    
    public class VaultsService
    {
        private readonly VaultsRepository _repo;

        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal Vault GetById(string userId, int id)
        {
            Vault original = _repo.GetById(id);
            if(original == null)
            {
                throw new Exception("Invalid Id... from vaultsService");
            }

            if(original.CreatorId != userId && original.IsPrivate == false)
            {
                throw new Exception("Access Denied NOT YOURS... from vaultsService");
            }

            return original;
        }

        internal Vault CreateVault(Vault newVault)
        {
            newVault.Id = _repo.Create(newVault);
            return newVault;
        }

        internal object DeleteVault(int id, string userId)
        {
            Vault original = _repo.GetById(id);
            if(original == null)
            {
                throw new Exception("Invalid Id... from vaultsService");
            }

            if(original.CreatorId != userId)
            {
                throw new Exception("Access Denied NOT YOURS... from vaultsService");
            }

            _repo.DeleteVault(id);
            return "Successfully deleted... from vaultsService";
        }

        internal IEnumerable<Vault> GetAllByCreatorId(string id)
        {
            return _repo.GetAllByCreatorId(id).ToList().FindAll(v => v.CreatorId == id || !v.IsPrivate);
        }
    }
}