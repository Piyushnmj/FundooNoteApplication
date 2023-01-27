using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace RepositoryLayer.Service
{
    public class NoteRL : INoteRL
    {
        FundooContext fundoo;
        IConfiguration configuration;
        public NoteRL(FundooContext fundoo, IConfiguration configuration)
        {
            this.fundoo = fundoo;
            this.configuration = configuration;
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="createNote">The create note.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
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
                objNEntity.IsTrashed = createNote.IsTrashed;
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

        /// <summary>
        /// Retrieves all notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieves a single note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteIdModel">The note identifier model.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteIdModel">The note identifier model.</param>
        /// <param name="createNoteModel">The create note model.</param>
        /// <returns></returns>
        public NEntity UpdateNote(long userId, NoteIdModel noteIdModel, CreateNoteModel createNoteModel)
        {
            try
            {
                var result = fundoo.NoteTable.Where(x => x.UserId == userId && x.NoteId == noteIdModel.noteId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = createNoteModel.Title;
                    result.Description = createNoteModel.Description;
                    result.Reminder = createNoteModel.Reminder;
                    result.BackgroundColour = createNoteModel.BackgroundColour;
                    result.ImagePath = createNoteModel.ImagePath;
                    result.IsArchived = createNoteModel.IsArchived;
                    result.IsPinned = createNoteModel.IsPinned;
                    result.IsDeleted = createNoteModel.IsDeleted;
                    result.IsTrashed = createNoteModel.IsTrashed;
                    result.TimeNoteCreated = createNoteModel.TimeNoteCreated;
                    result.TimeNoteUpdated = createNoteModel.TimeNoteUpdated;
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

        /// <summary>
        /// Permanently deletes the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteIdModel">The note identifier model.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Archives and unarchives the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteIdModel">The note identifier model.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Pins and unpins the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteIdModel">The note identifier model.</param>
        /// <returns></returns>
        public bool PinNote(long userId, NoteIdModel noteIdModel)
        {
            try
            {
                var result = fundoo.NoteTable.Where(x => x.UserId == userId && x.NoteId == noteIdModel.noteId).FirstOrDefault();
                if (!result.IsPinned == true)
                {
                    result.IsPinned = true;
                    fundoo.SaveChanges();
                    return true;
                }
                else
                {
                    result.IsPinned = false;
                    fundoo.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Trashes and restores the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteIdModel">The note identifier model.</param>
        /// <returns></returns>
        public bool TrashNote(long userId, NoteIdModel noteIdModel)
        {
            try
            {
                var result = fundoo.NoteTable.Where(x => x.UserId == userId && x.NoteId == noteIdModel.noteId).FirstOrDefault();
                if (!result.IsTrashed == true)
                {
                    result.IsTrashed = true;
                    fundoo.SaveChanges();
                    return true;
                }
                else
                {
                    result.IsTrashed = false;
                    fundoo.SaveChanges();
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Change background colour.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteIdModel">The note identifier model.</param>
        /// <param name="backgroundColour">The background colour.</param>
        /// <returns></returns>
        public NEntity BackgroundColour(long userId, NoteIdModel noteIdModel, BackgroundColourModel backgroundColour)
        {
            try
            {
                var result = fundoo.NoteTable.Where(x => x.UserId == userId && x.NoteId == noteIdModel.noteId).FirstOrDefault();
                if(result.BackgroundColour != null)
                {
                    result.BackgroundColour = backgroundColour.BackgroundColour;
                    fundoo.NoteTable.Update(result);
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

        /// <summary>
        /// Upload an image in the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="noteIdModel">The note identifier model.</param>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public string ImageUpload(long userId, NoteIdModel noteIdModel, IFormFile image)
        {
            try
            {
                var result = fundoo.NoteTable.Where(x => x.UserId == userId && x.NoteId == noteIdModel.noteId).FirstOrDefault();
                if(result != null)
                {
                    Account account = new Account(
                        this.configuration["CloudinarySettings:CloudName"],
                        this.configuration["CloudinarySettings:ApiKey"],
                        this.configuration["CloudinarySettings:ApiSecret"]
                        );
                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadpicture = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadresult = cloudinary.Upload(uploadpicture);
                    string imgpath = uploadresult.Url.ToString();
                    result.ImagePath = imgpath;
                    fundoo.SaveChanges();
                    return "Image Uploaded Successfully";
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
    }
}
