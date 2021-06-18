using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib.Utils;

namespace AptumServer.Packets.ServerBound
{
    public class RequestCreateLobbyPacket
    {
        public string LeaderName { get; set; }
        public int RequestedGameMode { get; set; }
    }
}
