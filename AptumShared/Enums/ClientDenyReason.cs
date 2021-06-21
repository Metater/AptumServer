using System;
using System.Collections.Generic;
using System.Text;

namespace AptumShared.Enums
{
    [Flags]
    public enum ClientDenyReason : long
    {
        JoinLobby = 1 << 0,
    }
}
