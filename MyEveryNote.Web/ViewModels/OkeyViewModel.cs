using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEveryNote.Web.ViewModels
{
    public class OkeyViewModel : NotifyViewModelBase<string>
    {
        public OkeyViewModel()
        {
            Title = "İşlem Başarılı";
            Items = new List<string> { };
            
        }
    }
}