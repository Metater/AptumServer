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
        public GameManager gameManager;


        public PeerClientIdMap peerClientIdMap = new PeerClientIdMap();
        public List<AptumServerPlayer> players = new List<AptumServerPlayer>();

        public Random rand = new Random();

        public AptumServer()
        {
            gameManager = new GameManager(this);
            listener = new AptumServerListener(this);
            server = new NetManager(listener);
            listener.AddNetManagerReference(server);
            server.Start(12733);
            Console.WriteLine("[Core] Server listening for connections on port 12733");
        }

        public void Tick(long id)
        {
            
        }

        public int AddClient(NetPeer peer)
        {
            int clientId = peerClientIdMap.AddPeer(peer);
            AptumPlayer player = new AptumPlayer(clientId, "Unknown", new AptumBoard());
            players.Add(new AptumServerPlayer(player));
            return clientId;
        }

        public bool TryGetPlayer(int clientId, AptumServerPlayer outPlayer)
        {
            foreach (AptumServerPlayer player in players)
            {
                if (player.player.id == clientId)
                {
                    outPlayer = player;
                    return true;
                }
            }
            outPlayer = null;
            return false;
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
