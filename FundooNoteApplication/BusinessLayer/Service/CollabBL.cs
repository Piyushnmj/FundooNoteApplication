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

        public IQueryable<CollabEntity> RetrieveCollaborator(long noteId)
        {
            try
            {
                return collabICollabRL.RetrieveCollaborator(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<CollabEntity> RetrieveCollaboratorUsingCollabId(long collabId)
        {
            try
            {
                return collabICollabRL.RetrieveCollaboratorUsingCollabId(collabId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
