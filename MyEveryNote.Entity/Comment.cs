using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEveryNote.Entity
{
    [Table("Comments")]
    public class Comment : MyEntityBase
    {
        [Required, StringLength(300)]
        public string Text { get; set; }

        public virtual Note Note { get; set; }
        public virtual EveryNoteUser Owner { get; set; }
    }
}
