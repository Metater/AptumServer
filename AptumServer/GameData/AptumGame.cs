using AptumServer.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AptumServer.GameData
{
    public class AptumGame : ITickable
    {
        private AptumServer aptumServer;

        public AptumPlayer leader = new AptumPlayer();
        public int joinCode;
        public bool full = false;
        public AptumPlayer other = new AptumPlayer();
        public bool started = false;

        private PieceGenerator pieceGenerator;

        public AptumGame(AptumServer aptumServer, int leaderId, string leaderName)
        {
            this.aptumServer = aptumServer;
            leader.id = leaderId;
            leader.name = leaderName;
            leader.board = new AptumBoard(leaderId);
            joinCode = aptumServer.rand.Next(1000);
        }

        public void Join(int otherId, string otherName)
        {
            if (full) return;

            other.id = otherId;
            other.name = otherName;
            other.board = new AptumBoard(otherId);

            pieceGenerator = new PieceGenerator(aptumServer.rand.Next());

            for (int i = 0; i < 3; i++)
            {
                (int, int) piece = pieceGenerator.GetPieceAtIndex(i);
                leader.piecePool[i] = piece;
                other.piecePool[i] = piece;
            }

            leader.nextPieceIndex = 3;
            other.nextPieceIndex = 3;
        }

        public void Tick(long id)
        {

        }

        public bool ContainsClientId(int id)
        {
            if (full)
            {
                if (id == leader.id || id == other.id) return true;
            }
            else
            {
                if (id == leader.id) return true;
            }
            return false;
        }

        public AptumPlayer GetPlayerFromId(int id)
        {
            if (id == leader.id) return leader;
            else if (id == other.id) return other;
            return null;
        }

        public void PlacePieceFromSlot(int id, int slot, int rootX, int rootY)
        {
            AptumPlayer aptumPlayer = GetPlayerFromId(id);
            if (aptumPlayer is null) return;
            aptumPlayer.board.PlacePiece((rootX, rootY), PieceDictionary.GetPiece(aptumPlayer.piecePool[slot].Item1));
            aptumPlayer.piecePool[slot] = pieceGenerator.GetPieceAtIndex(aptumPlayer.nextPieceIndex);
            aptumPlayer.nextPieceIndex++;

            // Do wipe check

        }
    }
}
