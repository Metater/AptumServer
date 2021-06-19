using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.Types
{
    [Flags]
    public enum ClientDenyType : long
    {
        JoinLobby = 1 << 0,
    }
}
