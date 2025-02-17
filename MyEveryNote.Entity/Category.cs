﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEveryNote.Entity
{
    [Table("Categories")]
    public class Category : MyEntityBase
    {
        [DisplayName("Title"),Required(ErrorMessage ="Title must be entered."), StringLength(50)]
        public string Title { get; set; }

        [DisplayName("Description"),StringLength(150)]
        public string Description { get; set; }

        public virtual List<Note> Notes { get; set; }

        public Category()
        {
            Notes = new List<Note>();
        }
    }
}
