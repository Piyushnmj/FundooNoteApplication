using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        ILabelBL objILabelBL;

        public LabelController(ILabelBL objILabelBL)
        {
            this.objILabelBL = objILabelBL;
        }

        [HttpPost]
        [Route("AddLabel")]
        public IActionResult AddLabel(long noteId, LabelNameModel labelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userId").Value);
                var result = objILabelBL.AddLabel(noteId, userId, labelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label added Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to add Label" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("RetrieveLabel-UsingLabelId")]
        public IActionResult RetrieveLabelUsingLabelId(long labelId)
        {
            try
            {
                var result = objILabelBL.RetrieveLabelUsingLabelId(labelId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Retrieve Successfull", data = result });
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

        [HttpGet]
        [Route("RetrieveLabel-UsingNoteId")]
        public IActionResult RetrieveLabelUsingNoteId(long noteId)
        {
            try
            {
                var result = objILabelBL.RetrieveLabelUsingLabelId(noteId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Retrieve Successfull", data = result });
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
        [Route("EditLabel")]
        public IActionResult EditLabel(long labelId, LabelNameModel labelName)
        {
            try
            {
                var result = objILabelBL.EditLabel(labelId, labelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label Name Changed", data = result });
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

        [HttpDelete]
        [Route("DeleteLabel")]
        public IActionResult DeleteLabel(long labelId)
        {
            try
            {
                var result = objILabelBL.DeleteLabel(labelId);
                if (result == true)
                {
                    return this.Ok(new { success = true, message = "Label Deleted", data = result });
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
    }
}
