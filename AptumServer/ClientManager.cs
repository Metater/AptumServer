using System;
using System.Collections.Generic;
using System.Text;
using LiteNetLib;

namespace AptumServer
{
    public class ClientManager : ITickable
    {
        private AptumServer aptumServer;

        public PeerClientIdMap peerClientIdMap = new PeerClientIdMap();

        public ClientManager(AptumServer aptumServer)
        {
            this.aptumServer = aptumServer;
        }

        public void AddClient(NetPeer peer)
        {
            peerClientIdMap.AddPeer(peer);
        }

        public void Tick(long id)
        {

        }
    }
}
