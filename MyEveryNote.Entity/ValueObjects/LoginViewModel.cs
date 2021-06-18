using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNote.Entity.ValueObjects
{
    public class LoginViewModel
    {
        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Username { set; get; }

        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."),DataType(DataType.Password)]
        public string Password { set; get; }
    }
}
