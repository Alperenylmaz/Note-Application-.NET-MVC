using MyEveryNote.Entity.Messages;
using System.Collections.Generic;

namespace MyEveryNote.BusinessLayer.Results
{
    public class BusinessLayerResult<T> where T : class
    {
        public List<ErrorMessageObj> Errors { get; set; }
        public T Result { get; set; }

        public BusinessLayerResult()
        {
            Errors = new List<ErrorMessageObj>();
        }

        public void AddErrorMessage(ErrorMessages err, string msg)
        {
            Errors.Add(new ErrorMessageObj() { Code = err, Message = msg });
        }
    }
}
