using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public NEntity CreateNote(CreateNoteModel createNote, long userId);
        public IQueryable<NEntity> RetrieveNote(long userId);
    }
}
