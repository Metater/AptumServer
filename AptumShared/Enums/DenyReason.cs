using System;
using System.Collections.Generic;
using System.Text;

namespace AptumShared.Enums
{
    [Flags]
    public enum DenyReason : long
    {
        JoinLobby = 1 << 0,
    }
}
