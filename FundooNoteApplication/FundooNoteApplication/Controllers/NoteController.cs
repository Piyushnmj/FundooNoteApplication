using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Context;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteBL objINoteBL;
        FundooContext fundoo;
        IDistributedCache distributedCache;
        public NoteController(INoteBL objINoteBL, FundooContext fundoo, IDistributedCache distributedCache)
        {
            this.objINoteBL= objINoteBL;
            this.fundoo=fundoo;
            this.distributedCache=distributedCache;
        }

        [HttpPost]
        [Route("CreateNote")]
        public IActionResult Create(CreateNoteModel createNote)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objINoteBL.CreateNote(createNote, userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Created Successfully" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to Create Note" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("RetrieveAllNotes")]
        public IActionResult RetrieveAllNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objINoteBL.RetrieveAllNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Notes Retrieved", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to Retrieve Notes" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("RetrieveNote")]
        public IActionResult RetrieveNote([FromQuery]NoteIdModel noteIdModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objINoteBL.RetrieveNote(userId, noteIdModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Retrieved", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to Retrieve Note" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("UpdateNote")]
        public IActionResult UpdateNote([FromQuery]NoteIdModel noteIdModel, CreateNoteModel createNoteModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objINoteBL.UpdateNote(userId, noteIdModel, createNoteModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Updated", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to Update Note" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteNote")]
        public IActionResult Delete(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objINoteBL.DeleteNote(userId, noteId);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Note Deleted" });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to Delete Note" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Archive-Unarchive")]
        public IActionResult Archive(NoteIdModel noteIdModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objINoteBL.ArchiveNote(userId, noteIdModel);
                if(result != null)
                {
                    if (result == true)
                    {
                        return this.Ok(new { success = true, message = "Note Archived" });
                    }
                    else
                    {
                        return this.Ok(new { success = true, message = "Note Unarchived", data = result });
                    }
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Pin-UnPin")]
        public IActionResult Pin(NoteIdModel noteIdModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objINoteBL.PinNote(userId, noteIdModel);
                if(result != null)
                {
                    if (result == true)
                    {
                        return this.Ok(new { success = true, message = "Note Pinned", data = result });
                    }
                    else
                    {
                        return this.Ok(new { success = true, message = "Note Unpinned" });
                    }
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Trash-Restore")]
        public IActionResult Trash(NoteIdModel noteIdModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objINoteBL.TrashNote(userId, noteIdModel);
                if (result != null)
                {
                    if (result == true)
                    {
                        return this.Ok(new { success = true, message = "Note Trashed", data = result });
                    }
                    else
                    {
                        return this.Ok(new { success = true, message = "Note Restored" });
                    }
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("ChangeColour")]
        public IActionResult ChangeBackgroundColour(long noteId, string BackgroundColour)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objINoteBL.BackgroundColour(userId, noteId, BackgroundColour);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Colour Updated", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to Update Colour" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("UploadImage")]
        public IActionResult ImageUpload([FromQuery]NoteIdModel noteIdModel, IFormFile image)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objINoteBL.ImageUpload(userId, noteIdModel, image);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Image Upload Successfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something went wrong" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("Redis-RetrieveNotes")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "notesList";
            string serializedNotesList;
            var notesList = new List<NEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                notesList = JsonConvert.DeserializeObject<List<NEntity>>(serializedNotesList);
            }
            else
            {
                notesList = await fundoo.NoteTable.ToListAsync();
                serializedNotesList = JsonConvert.SerializeObject(notesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(notesList);
        }
    }
}
