using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity AddLabel(long noteId, long userId, LabelNameModel labelName);
        public IQueryable<LabelEntity> RetrieveLabelUsingLabelId(long labelId);
        public IQueryable<LabelEntity> RetrieveLabelUsingNoteId(long noteId);
        public LabelEntity EditLabel(long labelId, LabelNameModel labelName);
    }
}
