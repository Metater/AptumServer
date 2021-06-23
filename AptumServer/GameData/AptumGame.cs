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

        public List<AptumPlayer> players = new List<AptumPlayer>();
        public int joinCode = -1;
        public bool full = false;
        public bool started = false;

        private PieceGenerator pieceGenerator;
        private int pieceGenSeed;

        public AptumGame(AptumServer aptumServer, AptumPlayer leader)
        {
            this.aptumServer = aptumServer;
            players.Add(leader);

            int tries = 0;
            while (!aptumServer.joinCodeGameMap.ContainsKey(joinCode))
            {
                if (tries >= 100) throw new Exception("Need larger join code!");
                joinCode = aptumServer.rand.Next(1000);
                aptumServer.joinCodeGameMap.Add(joinCode, this);
                tries++;
            }

            pieceGenSeed = aptumServer.rand.Next();
            pieceGenerator = new PieceGenerator(pieceGenSeed);

            players[0].board.AddPieceGenerator(pieceGenerator);
        }

        public void Join(AptumPlayer player)
        {
            if (full) return;

            players.Add(player);

            int pieceGenSeed = aptumServer.rand.Next();
            PieceGenerator pieceGenerator = new PieceGenerator(pieceGenSeed);
            players[1].board.AddPieceGenerator(pieceGenerator);
        }

        public void Tick(long id)
        {

        }

        public bool ContainsClientId(int id)
        {
            return players.Exists((player) => player.id == id);
        }

        public AptumPlayer GetPlayerFromId(int id)
        {
            return players.Find((player) => player.id == id);
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

        public bool TryJoinGame()
        {

        }
    }
}
