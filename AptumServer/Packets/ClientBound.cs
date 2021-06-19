using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.Packets
{
    public class ClientDenyPacket
    {
        public long ClientDenyBitField { get; set; }
    }
    public class CreatedLobbyPacket
    {
        public int JoinCode { get; set; }
    }
    public class ClientJoinedPacket
    {
        public string Name { get; set; }
    }
    public class StartGamePacket
    {
        public int PieceGenerationSeed { get; set; }
    }
    public class PiecePlacedPacket
    {
        public int SlotToPlacePieceFrom { get; set; }
        public int RootX { get; set; }
        public int RootY { get; set; }
    }
    public class LineWipedPacket
    {
        public int Index { get; set; }
        public bool Horizontal { get; set; }
    }
    public class GameEndedPacket
    {
        public int LeaderScore { get; set; }
        public int OtherScore { get; set; }
    }
    public class ClientPlayAgainPacket
    {
        public bool PlayAgain { get; set; }
    }
    public class LobbyClosePacket
    {
        public int LobbyCloseReason { get; set; }
    }
}
