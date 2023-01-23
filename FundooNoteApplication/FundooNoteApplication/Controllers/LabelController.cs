using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Entities;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        ILabelBL objILabelBL;
        FundooContext fundoo;
        IDistributedCache distributedCache;
        public LabelController(ILabelBL objILabelBL, FundooContext fundoo, IDistributedCache distributedCache)
        {
            this.objILabelBL = objILabelBL;
            this.fundoo = fundoo;
            this.distributedCache = distributedCache;
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

        [HttpGet]
        [Route("Redis-RetrieveLabel")]
        public async Task<IActionResult> GetAllLabelssUsingRedisCache()
        {
            var cacheKey = "labelList";
            string serializedLabelList;
            var labelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                labelList = await fundoo.LabelTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(labelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(labelList);
        }
    }
}
