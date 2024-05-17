using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class ContentTypeException:Exception
    {
        public ContentTypeException(string propqName,string? message) : base(message)
        {
            PropertyName = propqName;
        }

        public string PropertyName { get; set; }

    }
}
