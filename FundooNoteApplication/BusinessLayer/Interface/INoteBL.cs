using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        public NEntity CreateNote(CreateNoteModel createNote, long userId);
        public IQueryable<NEntity> RetrieveNote(long userId);
        public bool DeleteNote(long noteId);
    }
}
