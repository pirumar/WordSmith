using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace galleryArea.Models
{
    public class OperationResult<T>
    {
        public OperationResult()
        {
            IsSuccesful = true;
            Message = string.Empty;
            Data = default(T);
        }

        public bool IsSuccesful { get; set; }
        public string Message { get; set; }
        // public string ErrCode { get; set; }
        public T Data { get; set; }
    }
}