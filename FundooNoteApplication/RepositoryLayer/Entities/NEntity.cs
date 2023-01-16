using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class NEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string BackgroundColour { get; set; }
        public string ImagePath { get; set; }
        public bool IsArchived { get; set; }
        public bool IsPinned { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime TimeNoteCreated { get; set; }
        public DateTime TimeNoteUpdated { get; set;}
        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual UEntity User { get; set; }
    }
}
