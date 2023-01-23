using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
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
    public class CollabController : ControllerBase
    {
        ICollabBL objICollabBL;
        FundooContext fundoo;
        IDistributedCache distributedCache;
        public CollabController(ICollabBL objICollabBL, FundooContext fundoo, IDistributedCache distributedCache)
        {
            this.objICollabBL = objICollabBL;
            this.fundoo = fundoo;
            this.distributedCache = distributedCache;
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

        [HttpGet]
        [Route("Redis-RetrieveCollaborators")]
        public async Task<IActionResult> GetAllCollaboratorsUsingRedisCache()
        {
            var cacheKey = "collabList";
            string serializedCollabList;
            var collabList = new List<CollabEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                collabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
            }
            else
            {
                collabList = await fundoo.CollaboratorTable.ToListAsync();
                serializedCollabList = JsonConvert.SerializeObject(collabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(collabList);
        }
    }
}
