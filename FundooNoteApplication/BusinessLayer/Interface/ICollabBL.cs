using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity AddCollaborator(long noteId, EmailModel email);
        public IQueryable<CollabEntity> RetrieveCollaborator(long noteId);
        public IQueryable<CollabEntity> RetrieveCollaboratorUsingCollabId(long collabId);
        public bool DeleteCollaborator(CollaboratorIdModel collabId);
    }
}
