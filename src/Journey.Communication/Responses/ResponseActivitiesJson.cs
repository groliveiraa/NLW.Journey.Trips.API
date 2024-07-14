using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Communication.Responses
{
    public class ResponseActivitiesJson
    {
        public IList<ResponseActivityJson> Activities { get; set; } = [];
    }
}
