using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.Packets.ClientBound
{
    public class PiecePlacedPacket
    {
        public int SlotToPlacePieceFrom { get; set; }
        public int RootX { get; set; }
        public int RootY { get; set; }
    }
}
