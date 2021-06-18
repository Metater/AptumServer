using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.Types
{
    [Flags]
    public enum ClientDenyType : long
    {
        StartLobby = 1 << 0,
        JoinLobby = 1 << 1,
        PlacePiece = 1 << 2,
    }
}
