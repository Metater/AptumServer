using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.Packets.ClientBound
{
    public class GameEndedPacket
    {
        public int LeaderScore { get; set; }
        public int OtherScore { get; set; }
    }
}
