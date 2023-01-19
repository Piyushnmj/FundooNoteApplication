using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelILabelRL;

        public LabelBL(ILabelRL labelILabelRL)
        {
            this.labelILabelRL = labelILabelRL;
        }

        public LabelEntity AddLabel(long noteId, long userId, LabelNameModel labelName)
        {
            try
            {
                return labelILabelRL.AddLabel(noteId, userId, labelName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
