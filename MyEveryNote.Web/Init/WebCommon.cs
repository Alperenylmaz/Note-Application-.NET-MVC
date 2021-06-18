using MyEveryNote.Common;
using MyEveryNote.Entity;
using MyEveryNote.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEveryNote.Web.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            EveryNoteUser user = CurrentSession.User;

            if (user != null)
            {
                return user.Username;
            }
            else
            {
                return "system";
            }
           
        }
    }
}