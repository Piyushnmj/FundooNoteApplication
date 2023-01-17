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

        public IQueryable<NEntity> RetrieveAllNotes(long userId)
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

        public IQueryable<NEntity> RetrieveNote(long userId, NoteIdModel noteIdModel)
        {
            try
            {
                var result = fundoo.NoteTable.Where(x => x.UserId == userId && x.NoteId == noteIdModel.noteId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NEntity UpdateNote(long userId, NoteIdModel noteIdModel, CreateNoteModel createNoteModel)
        {
            try
            {
                var result = fundoo.NoteTable.Where(x => x.UserId == userId && x.NoteId == noteIdModel.noteId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = createNoteModel.Title;
                    result.Description = createNoteModel.Description;
                    result.Reminder= createNoteModel.Reminder;
                    result.BackgroundColour= createNoteModel.BackgroundColour;
                    result.ImagePath= createNoteModel.ImagePath;
                    result.IsArchived= createNoteModel.IsArchived;
                    result.IsPinned= createNoteModel.IsPinned;
                    result.IsDeleted= createNoteModel.IsDeleted;
                    result.TimeNoteCreated= createNoteModel.TimeNoteCreated;
                    result.TimeNoteUpdated= createNoteModel.TimeNoteUpdated;
                    fundoo.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNote(long userId, NoteIdModel noteIdModel)
        {
            try
            {
                var result = fundoo.NoteTable.Where(x => x.UserId == userId && x.NoteId == noteIdModel.noteId).FirstOrDefault();
                if (result != null)
                {
                    fundoo.NoteTable.Remove(result);
                    fundoo.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ArchiveNote(long userId, NoteIdModel noteIdModel)
        {
            try
            {
                var result = fundoo.NoteTable.Where(x => x.UserId == userId && x.NoteId == noteIdModel.noteId).FirstOrDefault();
                if (!result.IsArchived == true)
                {
                    result.IsArchived = true;
                    fundoo.SaveChanges();
                    return true;
                }
                else
                {
                    result.IsArchived = false;
                    fundoo.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
