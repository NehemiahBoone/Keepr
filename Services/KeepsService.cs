using System;
using System.Collections.Generic;
using System.Linq;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;
        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }


        internal IEnumerable<Keep> GetAll()
        {
            return _repo.GetAll();
        }


        internal Keep GetById(int keepId)
        {
            var data = _repo.GetById(keepId);
            if (data == null)
            {
                throw new Exception("Invalid Id... from keepsService");
            }

            return data;
        }


        internal Keep CreateKeep(Keep newKeep)
        {
            newKeep.Id = _repo.CreateKeep(newKeep);
            return newKeep;
        }


        internal Keep EditKeep(Keep editedKeep)
        {
            Keep original = _repo.GetById(editedKeep.Id);
            if (original == null)
            {
                throw new Exception("Invalid Id... from keepsService");
            }

            editedKeep.Name = editedKeep.Name == null ? original.Name : editedKeep.Name;
            editedKeep.Description = editedKeep.Description == null ? original.Description : editedKeep.Description;
            editedKeep.Img = editedKeep.Img == null ? original.Img : editedKeep.Img;
            editedKeep.Views = editedKeep.Views == 0 ? original.Views : editedKeep.Views;
            editedKeep.Keeps = editedKeep.Keeps == 0 ? original.Keeps : editedKeep.Keeps;
            editedKeep.Creator = editedKeep.Creator == null ? original.Creator : editedKeep.Creator;

            return _repo.EditKeep(editedKeep);
        }

        internal IEnumerable<KeepVaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            return _repo.GetKeepsByVaultId(id);
        }

        internal object DeleteKeep(int id, string userId)
        {
            Keep original = _repo.GetById(id);
            if (original == null)
            {
                throw new Exception("Invalid Id... from keepsService");
            }

            if (original.CreatorId != userId)
            {
                throw new Exception("Access Denied NOT YOURS... from keepsService");
            }

            _repo.DeleteKeep(id);
            return "Successfully Deleted... from keepsService";
        }

        internal IEnumerable<Keep> GetAllByCreatorId(string id)
        {
            return _repo.GetAllByCreatorId(id);
        }
    }
}