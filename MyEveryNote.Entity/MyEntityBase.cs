using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEveryNote.Entity
{
    public class MyEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime CreationOn { get; set; }

        [Required]
        public DateTime ModifiedOn { get; set; }

        [Required, StringLength(30)]
        public string ModifiedUsername { get; set; }
    }
}
