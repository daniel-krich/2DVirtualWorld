using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace My2DWorldShared.Data
{
    public class UsersSessionCollection
    {
        public List<UserSession> Sessions { get; } = new List<UserSession>();
    }
}
