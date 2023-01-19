using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual UEntity User { get; set; }
        [ForeignKey("Note")]
        public long NoteId { get; set; }
        public virtual NEntity Note { get; set; }
    }
}
