using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity AddLabel(long noteId, long userId, LabelNameModel labelName);
        public IQueryable<LabelEntity> RetrieveLabelUsingLabelId(long labelId);
        public IQueryable<LabelEntity> RetrieveLabelUsingNoteId(long noteId);
        public LabelEntity EditLabel(long labelId, LabelNameModel labelName);
        public bool DeleteLabel(long labelId);
    }
}
