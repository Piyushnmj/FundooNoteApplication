using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class CreateNoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string BackgroundColour { get; set; }
        public string ImagePath { get; set; }
        public bool IsArchived { get; set; }
        public bool IsPinned { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsTrashed { get; set; }
        public DateTime TimeNoteCreated { get; set; }
        public DateTime TimeNoteUpdated { get; set; }
    }
}
