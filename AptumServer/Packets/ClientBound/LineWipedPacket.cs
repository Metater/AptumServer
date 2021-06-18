using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.Packets.ClientBound
{
    public class LineWipedPacket
    {
        public int Index { get; set; }
        public bool Horizontal { get; set; }
    }
}
