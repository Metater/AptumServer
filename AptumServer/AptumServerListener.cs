using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using LiteNetLib;
using LiteNetLib.Utils;
using AptumShared.Packets;
using AptumServer.GameData;
using AptumShared;
using AptumShared.Enums;

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
            packetProcessor.SubscribeReusable<RequestJoinLobbyPacket, NetPeer>(OnRequestJoinLobbyPacketReceived);
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
        }
        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {

        }

        private void OnRequestCreateLobbyReceived(RequestCreateLobbyPacket packet, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            bool inGame = aptumServer.ClientIdInGame(clientId);
            if (!inGame && )
        }
        private void OnRequestJoinLobbyPacketReceived(RequestJoinLobbyPacket packet, NetPeer peer)
        {
            int clientId = aptumServer.peerClientIdMap.GetClientId(peer);
            bool inGame = aptumServer.ClientIdInGame(clientId);
            if (!inGame && aptumServer.TryJoinGame(new AptumPlayer(clientId, packet.Name, new AptumBoard()), packet.JoinCode, out AptumGame aptumGame))
            {
                ClientJoinedPacket clientJoinedPacket = new ClientJoinedPacket
                { Name = packet.Name };
                aptumServer.BroadcastToPlayersInGameBut(aptumGame, clientId, packetProcessor.Write(clientJoinedPacket), DeliveryMethod.ReliableOrdered);
            }
            else
            {
                ClientDenyPacket clientDenyPacket = new ClientDenyPacket
                { ClientDenyBitField = (long)ClientDenyReason.JoinLobby };
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
                {
                    AptumPlayer aptumPlayer = aptumGame.GetPlayerFromId(clientId);
                    /*
                    if (aptumPlayer.board.CheckPieceFit((packet.RootX, packet.RootY), PieceDictionary.GetPiece(aptumPlayer.piecePool[packet.SlotToPlacePieceFrom].Item1)))
                    {
                        aptumGame.PlacePieceFromSlot(clientId, packet.SlotToPlacePieceFrom, packet.RootX, packet.RootY);
                    }
                    */
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
