using MyEveryNote.BusinessLayer.Abstract;
using MyEveryNote.DataAccessLayer.EntityFramework;
using MyEveryNote.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEveryNote.BusinessLayer
{
    public class NoteManager : ManagerBase<Note>
    {
        public object ModifiedOn { get; set; }
        public object LikeCount { get; set; }
    }
}
