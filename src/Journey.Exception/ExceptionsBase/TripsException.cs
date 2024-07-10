using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Exception.ExceptionsBase
{
    public class TripsException : SystemException
    {
        public TripsException(string message) : base(message)
        {
            
        }
    }
}
