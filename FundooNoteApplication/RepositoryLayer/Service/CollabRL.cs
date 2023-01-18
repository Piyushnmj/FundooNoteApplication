﻿using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class CollabRL : ICollabRL
    {
        FundooContext fundoo;

        public CollabRL(FundooContext fundoo)
        {
            this.fundoo = fundoo;
        }

        public CollabEntity AddCollaborator(long noteId, EmailModel email)
        {
            try
            {
                var noteResult = fundoo.NoteTable.Where(x => x.NoteId == noteId).FirstOrDefault();
                var emailResult = fundoo.UserTable.Where(x => x.Email == email.Email).FirstOrDefault();

                if (emailResult != null && noteResult != null)
                {
                    CollabEntity objCollab = new CollabEntity();
                    objCollab.Email = emailResult.Email;
                    objCollab.NoteId = noteResult.NoteId;
                    objCollab.UserId = emailResult.UserId;
                    fundoo.Add(objCollab);
                    fundoo.SaveChanges();
                    return objCollab;
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