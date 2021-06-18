using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using LiteNetLib;
using LiteNetLib.Utils;
using AptumServer.Packets;
using AptumServer.Types;
using AptumServer.GameData;

namespace AptumServer
{
    public class AptumServerListener : INetEventListener
    {
        private AptumServer aptumServer;
        private NetManager server;

        public NetPacketProcessor packetProcessor = new NetPacketProcessor();

        public AptumServerListener()
        {
            packetProcessor.SubscribeReusable<RequestCreateLobbyPacket, NetPeer>(OnRequestCreateLobbyReceived);
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
            aptumServer.peerClientIdMap.AddPeer(peer);
            RequestCreateLobbyPacket startLobbyPacket = new RequestCreateLobbyPacket
            {
                LeaderName = "Server"
            };
            peer.Send(packetProcessor.Write(startLobbyPacket), DeliveryMethod.ReliableOrdered);
        }
        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {

        }

        private void OnRequestCreateLobbyReceived(RequestCreateLobbyPacket requestCreateLobbyPacket, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            bool inGame = aptumServer.ClientIdInGame(clientId);
            if (!inGame)
            {

            }
            else
            {
                ClientDenyPacket clientDenyPacket = new ClientDenyPacket
                { ClientDenyBitField = (long)ClientDenyType.StartLobby };
                peer.Send(packetProcessor.Write(clientDenyPacket), DeliveryMethod.ReliableOrdered);
            }
        }
        private void OnRequestStartGameReceived(RequestStartGamePacket requestStartGamePacket, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            bool inGame = aptumServer.ClientIdInGame(clientId);
        }
        private void OnRequestPlacePieceReceived(RequestPlacePiecePacket requestPlacePiecePacket, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            bool deny = true;
            if (aptumServer.GetGameWithClientId(clientId, out AptumGame aptumGame))
            {
                AptumBoard aptumBoard = aptumGame.GetBoardFromId(clientId);
                if (aptumBoard.CheckPieceFit())
            }
            if (deny)
            {
                ClientDenyPacket clientDenyPacket = new ClientDenyPacket
                { ClientDenyBitField = (long)ClientDenyType.PlacePiece };
                peer.Send(packetProcessor.Write(clientDenyPacket), DeliveryMethod.ReliableOrdered);
            }
        }
        private void OnRequestPlayAgainReceived(RequestPlayAgainPacket requestPlayAgainPacket, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            bool inGame = aptumServer.ClientIdInGame(clientId);
        }
    }
}
