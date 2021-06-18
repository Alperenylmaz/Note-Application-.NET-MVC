using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNote.Entity.Messages
{
    public class ErrorMessageObj
    {
        public ErrorMessages Code { get; set; }
        public string Message { get; set; }
    }
}
