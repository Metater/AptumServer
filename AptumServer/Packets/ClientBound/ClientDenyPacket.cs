using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib.Utils;

namespace AptumServer.Packets.ClientBound
{
    public class ClientDenyPacket
    {
        public long ClientDenyBitField { get; set; }
    }
}
