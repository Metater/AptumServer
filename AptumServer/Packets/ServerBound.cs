using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.Packets
{
    public class RequestCreateLobbyPacket
    {
        public string LeaderName { get; set; }
        public int RequestedGameMode { get; set; }
    }
    public class RequestStartGamePacket
    {

    }
    public class RequestPlacePiecePacket
    {
        public int SlotToPlacePieceFrom { get; set; }
        public int RootX { get; set; }
        public int RootY { get; set; }
    }
    public class RequestPlayAgainPacket
    {
        public bool PlayAgain { get; set; }
    }
}
