﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteBL objINoteBL;
        public NoteController(INoteBL objINoteBL)
        {
            this.objINoteBL= objINoteBL;
        }

        [Authorize]
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
        public IActionResult Delete(NoteIdModel noteIdModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objINoteBL.DeleteNote(userId, noteIdModel);
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
    }
}
