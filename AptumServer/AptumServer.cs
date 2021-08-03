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
        public List<AptumPlayer> clients = new List<AptumPlayer>();

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
