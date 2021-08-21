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

            int tries = 0;
            while (aptumServer.gameManager.joinCodeGameMap.ContainsKey(joinCode) || joinCode == -1)
            {
                if (tries > 100) throw new Exception("Need larger join code!");
                joinCode = aptumServer.rand.Next(1000);
                tries++;
            }
            aptumServer.gameManager.joinCodeGameMap.Add(joinCode, this);

            pieceGenSeed = aptumServer.rand.Next();
            pieceGenerator = new PieceGenerator(pieceGenSeed);

            players.Add(leader);
            leader.board.AddPieceGenerator(pieceGenerator);
        }

        public void Join(AptumPlayer player)
        {
            if (full) return;

            players.Add(player);
            player.board.AddPieceGenerator(pieceGenerator);
        }

        public void Tick(long id)
        {

        }

        public bool ContainsPlayerId(int id) { return players.Exists((player) => player.id == id); }
        public AptumPlayer GetPlayerFromId(int id) { return players.Find((player) => player.id == id); }

        public void PlacePieceFromSlot(int id, int slot, int rootX, int rootY)
        {
            AptumPlayer aptumPlayer = GetPlayerFromId(id);
            if (aptumPlayer is null) return;
            aptumPlayer.board.PlaceSlot(slot, (rootX, rootY));
        }

        public string[] GetPlayerNames()
        {
            List<string> playerNames = new List<string>();
            foreach (AptumPlayer player in players)
                playerNames.Add(player.name);
            return playerNames.ToArray();
        }

        public int[] GetPlayerIds()
        {
            List<int> playerIds = new List<int>();
            foreach (AptumPlayer player in players)
                playerIds.Add(player.id);
            return playerIds.ToArray();
        }

        public void KickPlayer(int id)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (id == players[i].id)
                {
                    players.RemoveAt(id);
                    if (i == 0) KillGame();
                    return;
                }
            }
        }

        public void KillGame()
        {

        }
    }
}
