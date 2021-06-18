using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEveryNote.Web.ViewModels
{
    public class WarningViewModel : NotifyViewModelBase<string>
    {
        public WarningViewModel()
        {
            Title = "Warning";
        }
    }
}