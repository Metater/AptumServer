using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.Packets.ClientBound
{
    public class StartGamePacket
    {
        public int PieceGenerationSeed { get; set; }
    }
}
