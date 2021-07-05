using System;
using System.Collections.Generic;
using System.Text;

namespace AptumShared.Packets
{
    public class DenyPacket
    {
        public long DenyBitField { get; set; }
    }
    public class CreatedLobbyPacket
    {
        public int JoinCode { get; set; }
    }
    public class JoinedLobbyPacket
    {

    }
    public class UpdatePlayersPacket
    {
        public string[] PlayerNames { get; set; }
    }
    public class StartGamePacket
    {
        public int PieceGenerationSeed { get; set; }
    }
    public class PiecePlacedPacket
    {
        public int PlayerIndex { get; set; }
        public int SlotToPlacePieceFrom { get; set; }
        public int RootX { get; set; }
        public int RootY { get; set; }
    }
    public class GameEndedPacket
    {
        public int LeaderScore { get; set; }
        public int OtherScore { get; set; }
    }
    public class PlayAgainPacket
    {
        public int PlayerIndex { get; set; }
        public bool PlayAgain { get; set; }
    }
    public class LobbyClosePacket
    {
        public int LobbyCloseReason { get; set; }
    }
}
