﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEveryNote.Entity
{
    [Table("Users")]
    public class EveryNoteUser : MyEntityBase
    {
        [ScaffoldColumn(false)]
        public string ProfileImageFileName { get; set; }

        [DisplayName("İsim"),StringLength(40)]
        public string Name { get; set; }

        [DisplayName("Soyisim"), StringLength(40)]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı"),Required, StringLength(40)]
        public string Username { get; set; }

        [DisplayName("Email"),Required, StringLength(40)]
        public string Email { get; set; }

        [DisplayName("Şifre"), Required, StringLength(40),]
        public string Password { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [DisplayName("Is Admin")]
        public bool IsAdmin { get; set; }

        [Required,ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }


        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
