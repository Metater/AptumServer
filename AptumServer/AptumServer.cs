using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib;
using LiteNetLib.Utils;
using AptumServer.GameData;
using AptumServer.Packets;

namespace AptumServer
{
    public class AptumServer : ITickable
    {
        public AptumServerListener listener;
        public NetManager server;

        public PeerClientIdMap peerClientIdMap = new PeerClientIdMap();
        public List<AptumGame> games = new List<AptumGame>();

        public Random rand = new Random();
        
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
            foreach (AptumGame aptumGame in games)
            {
                if (aptumGame.ContainsClientId(id)) return true;
            }
            return false;
        }

        public bool GetGameWithClientId(int id, out AptumGame outAptumGame)
        {
            foreach (AptumGame aptumGame in games)
            {
                if (aptumGame.ContainsClientId(id))
                {
                    outAptumGame = aptumGame;
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
    }
}
