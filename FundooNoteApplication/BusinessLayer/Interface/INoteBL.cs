using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
        public IQueryable<NEntity> RetrieveAllNotes(long userId);
        public IQueryable<NEntity> RetrieveNote(long userId, NoteIdModel noteIdModel);
        public NEntity UpdateNote(long userId, NoteIdModel noteIdModel, CreateNoteModel createNoteModel);
        public bool ArchiveNote(long userId, NoteIdModel noteIdModel);
        public bool PinNote(long userId, NoteIdModel noteIdModel);
        //public bool DeleteNote(long userId, NoteIdModel noteIdModel);
        public bool DeleteNote(long userId, long noteId);
        public bool TrashNote(long userId, NoteIdModel noteIdModel);
        //public NEntity BackgroundColour(long userId, NoteIdModel noteIdModel, BackgroundColourModel backgroundColour);
        public NEntity BackgroundColour(long userId, long noteId, string BackgroundColour);
        public string ImageUpload(long userId, NoteIdModel noteIdModel, IFormFile img);
    }
}
