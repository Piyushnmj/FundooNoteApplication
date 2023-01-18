using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity AddCollaborator(long noteId, EmailModel email);
    }
}
