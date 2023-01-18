using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
        ICollabRL collabICollabRL;
        public CollabBL(ICollabRL collabICollabRL)
        {
            this.collabICollabRL = collabICollabRL;
        }

        public CollabEntity AddCollaborator(long noteId, EmailModel email)
        {
            try
            {
                return collabICollabRL.AddCollaborator(noteId, email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<CollabEntity> RetrieveCollaborator(long noteId, long userId)
        {
            try
            {
                return collabICollabRL.RetrieveCollaborator(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCollaborator(long noteId, CollaboratorIdModel collabId)
        {
            try
            {
                return collabICollabRL.DeleteCollaborator(noteId, collabId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
