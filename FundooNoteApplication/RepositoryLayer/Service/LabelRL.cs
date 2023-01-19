using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        FundooContext fundoo;

        public LabelRL(FundooContext fundoo)
        {
            this.fundoo = fundoo;
        }

        public LabelEntity AddLabel(long noteId, long userId, LabelNameModel labelName)
        {
            try
            {
                var noteResult = fundoo.NoteTable.Where(x => x.NoteId == noteId).FirstOrDefault();
                var userResult = fundoo.UserTable.Where(x => x.UserId == userId).FirstOrDefault();

                if (noteResult != null && userResult != null)
                {
                    LabelEntity objLabel = new LabelEntity();
                    objLabel.LabelName = labelName.LabelName;
                    objLabel.NoteId = noteResult.NoteId;
                    objLabel.UserId = userResult.UserId;
                    fundoo.Add(objLabel);
                    fundoo.SaveChanges();
                    return objLabel;
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

        public IQueryable<LabelEntity> RetrieveLabelUsingLabelId(long labelId)
        {
            try
            {
                var result = fundoo.LabelTable.Where(x => x.LabelId == labelId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<LabelEntity> RetrieveLabelUsingNoteId(long noteId)
        {
            try
            {
                var result = fundoo.LabelTable.Where(x => x.NoteId == noteId);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LabelEntity EditLabel(long labelId, LabelNameModel labelName)
        {
            try
            {
                var result = fundoo.LabelTable.Where(x => x.LabelId == labelId).FirstOrDefault();
                if(result != null)
                {
                    result.LabelName = labelName.LabelName;
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
    }
}
