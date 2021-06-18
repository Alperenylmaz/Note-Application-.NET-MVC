using MyEveryNote.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNote.Common
{
    public class DefaultCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            
            return "system";
        }
    }
}
