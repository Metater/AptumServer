using System;
using System.Collections.Generic;
using System.Text;

namespace AptumShared.Packets
{
    public class RequestCreateLobbyPacket
    {
        public string LeaderName { get; set; }
    }
    public class RequestJoinLobbyPacket
    {
        public int JoinCode  { get; set; }
        public string Name { get; set; }
    }
    public class RequestStartGamePacket
    {
        public int RequestedGameMode { get; set; }
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
    // Add request leave lobby packet, to close game later
}
