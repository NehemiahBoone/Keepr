using System;
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


        internal object GetAll(string email)
        {
            throw new NotImplementedException();
        }


        internal object GetById(string email, int id)
        {
            throw new NotImplementedException();
        }


        internal Keep CreateKeep(Keep newKeep)
        {
            throw new NotImplementedException();
        }


        internal object EditKeep(Keep editedKeep, Profile userInfo)
        {
            throw new NotImplementedException();
        }


        internal object DeleteKeep(int id, Profile userInfo)
        {
            throw new NotImplementedException();
        }
    }
}