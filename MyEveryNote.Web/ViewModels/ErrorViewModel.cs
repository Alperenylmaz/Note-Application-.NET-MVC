using MyEveryNote.Entity.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEveryNote.Web.ViewModels
{
    public class ErrorViewModel : NotifyViewModelBase<ErrorMessageObj>
    {
        public ErrorViewModel()
        {
            Items = new List<ErrorMessageObj>() { };
        }
    }
}