using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planets.Objects
{
    [Serializable]
    internal class Response
    {
        public String typ { get; set; }
        public bool responseBool { get; set; }
        public string responseObj { get; set; }
    }
}
