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
    public class NoteBL : INoteBL
    {
        INoteRL noteINoteRL;
        public NoteBL(INoteRL noteINoteRL)
        {
            this.noteINoteRL = noteINoteRL;
        }

        public NEntity CreateNote(CreateNoteModel createNote, long userId)
        {
            try
            {
                return noteINoteRL.CreateNote(createNote, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<NEntity> RetrieveNote(long userId)
        {
            try
            {
                return noteINoteRL.RetrieveNote(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
