using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEveryNote.Entity.ValueObjects
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Username { set; get; }

        [DisplayName("Email"), 
            Required(ErrorMessage = "{0} alanı boş geçilemez."), 
            StringLength(60,ErrorMessage ="{0} max. {1} karakter olmalı."),
            EmailAddress(ErrorMessage ="Lütfen {0} alanı için geçerli bir giriş yapınız")]
        public string Email { set; get; }
        
        [DisplayName("Şifre"), 
            Required(ErrorMessage = "{0} alanı boş geçilemez."), 
            DataType(DataType.Password), 
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Password { set; get; }

        [DisplayName("Şifre Tekrar"), 
            Required(ErrorMessage = "{0} alanı boş geçilemez."), 
            DataType(DataType.Password), 
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı."),
            Compare("Password", ErrorMessage ="Şifreler Eşleşmiyor.")]
        public string RePassword { set; get; }
    }
}