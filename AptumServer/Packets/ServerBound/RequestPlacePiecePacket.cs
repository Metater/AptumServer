using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.Packets.ServerBound
{
    public class RequestPlacePiecePacket
    {
        public int SlotToPlacePieceFrom { get; set; }
        public int RootX { get; set; }
        public int RootY { get; set; }
    }
}
