using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib;
using LiteNetLib.Utils;
using AptumServer.GameData;
using AptumShared.Packets;
using AptumShared;

namespace AptumServer
{
    public class AptumServer : ITickable
    {
        public AptumServerListener listener;
        public NetManager server;

        public PeerClientIdMap peerClientIdMap = new PeerClientIdMap();
        public List<AptumGame> games = new List<AptumGame>();

        public Random rand = new Random();

        public Dictionary<int, AptumGame> joinCodeGameMap = new Dictionary<int, AptumGame>();
        
        public AptumServer()
        {
            listener = new AptumServerListener();
            server = new NetManager(listener);
            listener.NetManager(this, server);
            server.Start(12733);
        }

        public void Tick(long id)
        {
            
        }

        public bool ClientIdInGame(int id)
        {
            return games.Exists((aptumGame) => aptumGame.ContainsClientId(id));
        }

        public void CreateLobby(AptumPlayer leader)
        {
            games.Add(new AptumGame(this, leader));
        }

        public bool GetGameWithClientId(int id, out AptumGame outAptumGame)
        {
            outAptumGame = games.Find((aptumGame) => aptumGame.ContainsClientId(id));
            return outAptumGame != null;
        }

        public bool GetGameWithJoinCode(int joinCode, out AptumGame outAptumGame)
        {
            return joinCodeGameMap.TryGetValue(joinCode, out outAptumGame);
        }

        public bool TryJoinGame(AptumPlayer player, int joinCode, out AptumGame outAptumGame)
        {
            if (GetGameWithJoinCode(joinCode, out AptumGame aptumGame))
            {
                if (!aptumGame.started && !aptumGame.full)
                {
                    outAptumGame = aptumGame;
                    aptumGame.Join(player);
                    return true;
                }
            }
            outAptumGame = null;
            return false;
        }

        public void AddClient(NetPeer peer)
        {
            peerClientIdMap.AddPeer(peer);
        }

        public void BroadcastToPlayersInGame(AptumGame aptumGame, byte[] data, DeliveryMethod deliveryMethod)
        {
            foreach (AptumPlayer aptumPlayer in aptumGame.players)
            {
                peerClientIdMap.GetPeer(aptumPlayer.id).Send(data, deliveryMethod);
            }
        }
        public void BroadcastToPlayersInGameBut(AptumGame aptumGame, int excludedId, byte[] data, DeliveryMethod deliveryMethod)
        {
            foreach (AptumPlayer aptumPlayer in aptumGame.players)
            {
                if (aptumPlayer.id == excludedId) continue;
                peerClientIdMap.GetPeer(aptumPlayer.id).Send(data, deliveryMethod);
            }
        }
    }
}
