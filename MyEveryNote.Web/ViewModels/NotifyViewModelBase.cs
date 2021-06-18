using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEveryNote.Web.ViewModels
{
    public class NotifyViewModelBase<T>
    {
        public List<T> Items { get; set; }
        public string Header { get; set; }
        public string Title { get; set; }
        public bool IsRedirecting { get; set; }
        public string RedirectingURL { get; set; }
        public int RedirectingTimeout { get; set; }

        public NotifyViewModelBase()
        {
            Header = "Yönlendiriliyorsunuz.";
            Title = "Geçersiz İşlem.";
            IsRedirecting = true;
            RedirectingURL = "/Home/Index";
            RedirectingTimeout = 10000;
        }
    }
}