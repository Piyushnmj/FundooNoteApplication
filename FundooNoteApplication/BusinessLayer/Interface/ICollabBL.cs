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
        public IEnumerable<CollabEntity> RetrieveCollaborator(long noteId, long userId);
        public bool DeleteCollaborator(long noteId, CollaboratorIdModel collabId);
    }
}
