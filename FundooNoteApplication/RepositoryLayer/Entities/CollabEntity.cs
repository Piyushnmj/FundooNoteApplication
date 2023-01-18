using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace RepositoryLayer.Entities
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        public string Email { get; set; }
        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual UEntity User { get; set; }
        [ForeignKey("Note")]
        public long NoteId { get; set; }
        public virtual NEntity Note { get; set; }
    }
}
