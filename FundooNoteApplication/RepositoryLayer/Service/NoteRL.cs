using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRL : INoteRL
    {
        FundooContext fundoo;
        public NoteRL(FundooContext fundoo)
        {
            this.fundoo = fundoo;
        }

        public NEntity CreateNote(CreateNoteModel createNote, long userId)
        {
            try
            {
                NEntity objNEntity = new NEntity();

                objNEntity.UserId = userId;
                objNEntity.Title = createNote.Title;
                objNEntity.Description = createNote.Description;
                objNEntity.Reminder = createNote.Reminder;
                objNEntity.BackgroundColour = createNote.BackgroundColour;
                objNEntity.ImagePath = createNote.ImagePath;
                objNEntity.IsArchived = createNote.IsArchived;
                objNEntity.IsPinned = createNote.IsPinned;
                objNEntity.IsDeleted = createNote.IsDeleted;
                objNEntity.TimeNoteCreated = createNote.TimeNoteCreated;
                objNEntity.TimeNoteUpdated = createNote.TimeNoteUpdated;

                fundoo.NoteTable.Add(objNEntity);

                var result = fundoo.SaveChanges();
                if (result > 0)
                {
                    return objNEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<NEntity> RetrieveNote(long userId)
        {
            try
            {
                var result = fundoo.NoteTable.Where(x => x.UserId == userId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
