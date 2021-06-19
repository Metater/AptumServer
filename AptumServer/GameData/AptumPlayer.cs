using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.GameData
{
    public class AptumPlayer
    {
        public int id;
        public string name;
        public AptumBoard board;
        public (int, int)[] piecePool = new (int, int)[3];
        public int nextPieceIndex;
    }
}
