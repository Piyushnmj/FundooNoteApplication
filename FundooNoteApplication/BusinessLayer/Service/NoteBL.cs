using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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

        public IQueryable<NEntity> RetrieveAllNotes(long userId)
        {
            try
            {
                return noteINoteRL.RetrieveAllNotes(userId);
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
                return noteINoteRL.RetrieveNote(userId, noteIdModel);
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
                return noteINoteRL.UpdateNote(userId, noteIdModel, createNoteModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNote(long userId, long noteId)
        {
            try
            {
                return noteINoteRL.DeleteNote(userId, noteId);
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
                return noteINoteRL.ArchiveNote(userId, noteIdModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PinNote(long userId, NoteIdModel noteIdModel)
        {
            try
            {
                return noteINoteRL.PinNote(userId, noteIdModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool TrashNote(long userId, NoteIdModel noteIdModel)
        {
            try
            {
                return noteINoteRL.TrashNote(userId, noteIdModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NEntity BackgroundColour(long userId, long noteId, string BackgroundColour)
        {
            try
            {
                return noteINoteRL.BackgroundColour(userId, noteId, BackgroundColour);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ImageUpload(long userId, NoteIdModel noteIdModel, IFormFile img)
        {
            try
            {
                return noteINoteRL.ImageUpload(userId, noteIdModel, img);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
