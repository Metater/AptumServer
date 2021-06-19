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
using AptumServer.Utils;

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
            packetProcessor.SubscribeReusable<RequestStartGamePacket, NetPeer>(OnRequestStartGameReceived);
            packetProcessor.SubscribeReusable<RequestPlacePiecePacket, NetPeer>(OnRequestPlacePieceReceived);
            packetProcessor.SubscribeReusable<RequestPlayAgainPacket, NetPeer>(OnRequestPlayAgainReceived);
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

        private void OnRequestCreateLobbyReceived(RequestCreateLobbyPacket packet, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            bool inGame = aptumServer.ClientIdInGame(clientId);
            if (!inGame && aptumServer.TryJoinGame(clientId, packet.Name, packet.JoinCode, out AptumGame aptumGame))
            {

            }
        }
        private void OnRequestJoinLobbyPacketReceived(RequestJoinLobbyPacket packet, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            bool inGame = aptumServer.ClientIdInGame(clientId);
            if (!inGame && aptumServer.TryJoinGame(clientId, packet.Name, packet.JoinCode, out AptumGame aptumGame))
            {
                
            }
            else
            {
                ClientDenyPacket clientDenyPacket = new ClientDenyPacket
                { ClientDenyBitField = (long)ClientDenyType.JoinLobby };
                peer.Send(packetProcessor.Write(clientDenyPacket), DeliveryMethod.ReliableOrdered);

            }
        }
        private void OnRequestStartGameReceived(RequestStartGamePacket packet, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            bool inGame = aptumServer.ClientIdInGame(clientId);
        }
        private void OnRequestPlacePieceReceived(RequestPlacePiecePacket packet, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            if (packet.SlotToPlacePieceFrom >= 0 && packet.SlotToPlacePieceFrom <= 3)
            {
                if (aptumServer.GetGameWithClientId(clientId, out AptumGame aptumGame))
                
                    AptumPlayer aptumPlayer = aptumGame.GetPlayerFromId(clientId);
                    if (aptumPlayer.board.CheckPieceFit((packet.RootX, packet.RootY), PieceDictionary.GetPiece(aptumPlayer.piecePool[packet.SlotToPlacePieceFrom].Item1)))
                    {
                        aptumGame.PlacePieceFromSlot(clientId, packet.SlotToPlacePieceFrom, packet.RootX, packet.RootY);
                    }
                }
            }
        }
        private void OnRequestPlayAgainReceived(RequestPlayAgainPacket packet, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            bool inGame = aptumServer.ClientIdInGame(clientId);
        }
    }
}
