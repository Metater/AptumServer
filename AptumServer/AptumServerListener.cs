using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using LiteNetLib;
using LiteNetLib.Utils;
using AptumServer.Packets;
using AptumServer.Packets.ServerBound;
using AptumServer.Packets.ClientBound;
using AptumServer.Types;

namespace AptumServer
{
    public class AptumServerListener : INetEventListener
    {
        private AptumServer aptumServer;
        private NetManager server;

        public NetPacketProcessor packetProcessor = new NetPacketProcessor();

        public AptumServerListener()
        {
            packetProcessor.SubscribeReusable<RequestCreateLobbyPacket, NetPeer>(OnStartLobbyPacketReceived);
        }

        public void NetManager(AptumServer aptumServer, NetManager server)
        {
            this.aptumServer = aptumServer;
            this.server = server;
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {
            if (server.ConnectedPeersCount < 10)
                request.AcceptIfKey("Aptum");
            else
                request.Reject();
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {

        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {

        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            Console.WriteLine("[Server] received data. Processing...");
            packetProcessor.ReadAllPackets(reader, peer);
            reader.Recycle();
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {

        }

        public void OnPeerConnected(NetPeer peer)
        {
            aptumServer.clientManager.peerClientIdMap.AddPeer(peer);
            RequestCreateLobbyPacket startLobbyPacket = new RequestCreateLobbyPacket
            {
                LeaderName = "Server"
            };
            peer.Send(packetProcessor.Write(startLobbyPacket), DeliveryMethod.ReliableOrdered);
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {

        }

        private void OnStartLobbyPacketReceived(RequestCreateLobbyPacket startLobbyPacket, NetPeer peer)
        {
            if (!aptumServer.ClientIdInGame(aptumServer.clientManager.peerClientIdMap.GetClientId(peer)))
            {

            }
            else // deny
            {
                ClientDenyPacket clientDenyPacket = new ClientDenyPacket
                {
                    ClientDenyBitField = (long)ClientDenyType.StartLobby
                };
                peer.Send(packetProcessor.Write(clientDenyPacket), DeliveryMethod.ReliableOrdered);
            }
            Console.WriteLine("[Server] ReceivedPacket:\n" + startLobbyPacket.LeaderName);
        }
    }
}
