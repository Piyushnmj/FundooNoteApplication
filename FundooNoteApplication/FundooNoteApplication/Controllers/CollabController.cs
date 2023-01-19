using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        ICollabBL objICollabBL;
        public CollabController(ICollabBL objICollabBL)
        {
            this.objICollabBL = objICollabBL;
        }

        [HttpPost]
        [Route("AddCollaborator")]
        public IActionResult AddCollaborator(long noteId, EmailModel email)
        {
            try
            {
                var result = objICollabBL.AddCollaborator(noteId, email);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Collaborator email added Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to add email" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("RetrieveCollaborator")]
        public IActionResult RetrieveCollaborator(long noteId)
        {
            try
            {
                var result = objICollabBL.RetrieveCollaborator(noteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Email retrieved Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to retrieve" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("RetrieveCollaboratorId")]
        public IActionResult RetrieveCollaboratorId(long collabId)
        {
            try
            {
                var result = objICollabBL.RetrieveCollaboratorUsingCollabId(collabId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Email retrieved Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to retrieve" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("DeleteCollaborator")]
        public IActionResult DeleteCollaborator(CollaboratorIdModel collabId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objICollabBL.DeleteCollaborator(collabId);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Collaborator Deleted Successfully"});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to delete" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
