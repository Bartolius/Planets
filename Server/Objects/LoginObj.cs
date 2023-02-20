using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects
{
    [Serializable]
    class LoginObj
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
