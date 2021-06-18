using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEveryNote.Entity
{
    [Table("Likes")]
    public class Liked
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Note Note { get; set; }
        public virtual EveryNoteUser LikedUser { get; set; }
    }
}
