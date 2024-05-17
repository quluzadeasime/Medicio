using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class DoctorNotFoundException:Exception
    {
        public DoctorNotFoundException(string propName,string? message) : base(message)
        {
            PropertyName = propName;
        }

        public string PropertyName { get; set; }
    }
}
