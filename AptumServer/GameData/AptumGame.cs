using System;
using System.Collections.Generic;
using System.Text;
using AptumShared;
using AptumShared.Utils;

namespace AptumServer.GameData
{
    public class AptumGame : ITickable
    {
        private AptumServer aptumServer;

        public AptumPlayer leader;
        public int joinCode;
        public bool full = false;
        public AptumPlayer other;
        public bool started = false;

        public AptumGame(AptumServer aptumServer, int leaderId, string leaderName)
        {
            this.aptumServer = aptumServer;
            leader = new AptumPlayer(leaderId, leaderName, new AptumBoard());
            joinCode = -1;
            while (!aptumServer.joinCodes.Contains(joinCode))
            {
                joinCode = aptumServer.rand.Next(1000);
                aptumServer.joinCodes.Add(joinCode);
            }
        }

        public void Join(int otherId, string otherName)
        {
            if (full) return;

            other = new AptumPlayer(otherId, otherName, new AptumBoard());

            int pieceGenSeed = aptumServer.rand.Next();
            PieceGenerator pieceGenerator = new PieceGenerator(pieceGenSeed);
            leader.board.AddPieceGenerator(pieceGenerator);
            other.board.AddPieceGenerator(pieceGenerator);
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
            aptumPlayer.board.PlacePiece((rootX, rootY), PieceDictionary.GetPiece(aptumPlayer.piecePool[slot]));
            aptumPlayer.piecePool[slot] = pieceGenerator.GetPieceAtIndex(aptumPlayer.nextPieceIndex);
            aptumPlayer.nextPieceIndex++;

            // Do wipe check

        }
    }
}
